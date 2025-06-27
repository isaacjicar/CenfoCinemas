using DataAccess.CRUD;
using DTOs;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class MovieManager : BaseManager
    {
        private readonly UserCrudFactory _uCrud = new UserCrudFactory();
        public async Task Create(Movie movie)
        {
            try
            {
           
                if (movie.ReleaseDate == default ||
                    string.IsNullOrWhiteSpace(movie.Genre) ||
                    string.IsNullOrWhiteSpace(movie.Director))
                {
                    throw new Exception("Datos de la película no válidos");
                }

                if (!string.IsNullOrWhiteSpace(movie.Title) && ExistsTitle(movie.Title))
                {
                    throw new Exception("La película ya está registrada");
                }

          
                var mCrud = new MovieCrudFactory();
                mCrud.Create(movie);
                var user = _uCrud.RetrieveAll<User>();
                var tareas = user.Select(u => SendMovieAnnouncementEmail(u, movie));
                await Task.WhenAll(tareas);


            }
            catch (Exception ex)
            {
                ManagerExection(ex);
            }
         }
        public List<Movie> RetrieveAll()
        {
            var uCrud = new MovieCrudFactory();
            return uCrud.RetrieveAll<Movie>();
        }
        public Movie RetrieveByTitle(string title)
        {
            var uCrud = new MovieCrudFactory();
            var udto = new Movie { Title = title };
            return uCrud.RetrieveByTitle<Movie>(udto);
        }

        public Movie RetrieveById(int Id)
        {
            var uCrud = new MovieCrudFactory();
            return uCrud.RetrieveById<Movie>(Id);
        }

        public Movie Update(Movie movie)
        {
            try
            {
                var uCrud = new MovieCrudFactory();
                var existingMovie = uCrud.RetrieveById<Movie>(movie.id);

                if (existingMovie == null)
                {
                    throw new Exception("La película no existe");
                }

                var movieWithSameTitle = uCrud.RetrieveByTitle<Movie>(new Movie { Title = movie.Title });

                if (movieWithSameTitle != null && movieWithSameTitle.id != movie.id)
                {
                    throw new Exception("Ya existe otra película con el mismo título");
                }

                uCrud.Update(movie);
                return RetrieveById(movie.id);
            }
            catch (Exception ex)
            {
                ManagerExection(ex);
                return null;
            }
        }


        public Movie Delete(int id)
        {
            try
            {
                var uCrud = new MovieCrudFactory();
                var movie = uCrud.RetrieveById<Movie>(id);
                if (movie != null)
                {
                    uCrud.Delete(movie);
                    return movie;
                }
                else
                {
                    throw new Exception("La pelicula no existe");
                }
            }
            catch (Exception ex)
            {
                ManagerExection(ex);
                return null;
            }
        }

        private bool ExistsTitle(string title)
        {
            var mCrud = new MovieCrudFactory();
            var movieParam = new Movie { Title = title };
            var movie = mCrud.RetrieveByTitle<Movie>(movieParam);
            return movie != null;
        }


        private async Task SendMovieAnnouncementEmail(User user, Movie movie)
        {
            try
            {
                var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

                if (apiKey == null)
                    Console.WriteLine(" apiKey es null");
                else if (apiKey == "")
                    Console.WriteLine(" apiKey está vacía");
                else
                    Console.WriteLine(" apiKey cargada: " + apiKey);


                if (string.IsNullOrEmpty(apiKey))
                {
                    Console.WriteLine(" La API Key no fue encontrada en las variables de entorno.");
                    throw new Exception("API Key no encontrada en variables de entorno");
                }

                Console.WriteLine(" API KEY encontrada: " + apiKey);

                var client = new SendGridClient(apiKey);
                var from_email = new EmailAddress("isaacjiemmenez@gmail.com", "CenfoCinemas");
                var to_email = new EmailAddress(user.Email, user.Name);
                var subject = "Se a registado de peliculas";
                var plainTextContent = $"Hola {user.Name}, tenemos una nueva película: {movie.Title} dirigida por {movie.Director}.";
                var htmlContent = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            color: #333;
            padding: 20px;
        }}
        .container {{
            background-color: #ffffff;
            padding: 20px;
            border-radius: 10px;
            border: 1px solid #dddddd;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }}
        h1 {{
            color: #e50914;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>🎬 ¡Nueva película registrada!</h1>
        <p>Hola <strong>{user.Name}</strong>,</p>
        <p>Estamos emocionados de anunciarte que hemos agregado una nueva película a nuestro catálogo:</p>
        <ul>
            <li><strong>Título:</strong> {movie.Title}</li>
            <li><strong>Género:</strong> {movie.Genre}</li>
            <li><strong>Director:</strong> {movie.Director}</li>
            <li><strong>Fecha de estreno:</strong> {movie.ReleaseDate:yyyy-MM-dd}</li>
        </ul>
        <p>{movie.description}</p>
        <br/>
        <p>Gracias por ser parte de <strong>CenfoCinemas</strong>.</p>
    </div>
</body>
</html>";


                var msg = MailHelper.CreateSingleEmail(from_email, to_email, subject, plainTextContent, htmlContent);


                Console.WriteLine(" Enviando correo...");
                var response = await client.SendEmailAsync(msg);

                Console.WriteLine(" SendGrid Status: " + response.StatusCode);
                string responseBody = await response.Body.ReadAsStringAsync();
                Console.WriteLine(" SendGrid Response Body: " + responseBody);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    Console.WriteLine(" Correo enviado exitosamente.");
                }
                else
                {
                    Console.WriteLine(" Error al enviar el correo. Código: " + response.StatusCode);
                    throw new Exception("Error al enviar el correo: " + responseBody);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Se produjo una excepción al intentar enviar el correo:");
                Console.WriteLine(" Mensaje: " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine(" Inner: " + ex.InnerException.Message);
                throw;
            }
        }

    }
}
