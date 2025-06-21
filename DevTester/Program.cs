using CoreApp;
using DataAccess.CRUD;
using DataAccess.DAO;
using DTOs;
using Newtonsoft.Json;
using System;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;


public class Program
{
    public static void Main(string[] args)
    {
        var sqlDao = SqlDao.GetInstance();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n===== MENÚ PRINCIPAL =====");
            Console.WriteLine("1. Crear usuario");
            Console.WriteLine("2. Consultar usuario");
            Console.WriteLine("3. Consultar usuario por ID");
            Console.WriteLine("4. Consultar usuario por código");
            Console.WriteLine("5. Actualizar usuario");
            Console.WriteLine("6. Eliminar usuario");
            Console.WriteLine("7. Registrar película");
            Console.WriteLine("8. Consultar peliculas");
            Console.WriteLine("9. Consultar películas por ID");
            Console.WriteLine("10. Actualizar película");
            Console.WriteLine("11. Eliminar película");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CrearUsuario(sqlDao);
                    break;
                case "2":
                    ConsultarUsuario(sqlDao);
                    break;
                case "3":
                    ConsusltarUsuarioPorId(sqlDao);
                    break;
                  
                case "4":
                    ConsualtarUsuarioPorUserCode(sqlDao);
                    break;
                case "5":
                    ActualizarUsuario(sqlDao);
                    break;
                case "6":
                    EliminarUsuarios(sqlDao);
                    break;
                case "7":
                    RegistrarPelicula(sqlDao);
                    break;
                case "8":
                    ConsultarPeliculas(sqlDao);
                    break;
                case "9":
                    ConsultarPeliculasPorId(sqlDao);
                    break;
                case "10":
                    break;
                case "11":
                    break;
                case "0":
                    exit = true;
                    Console.WriteLine("¡Hasta luego!");
                    break;
                default:
                    Console.WriteLine("Opción inválida, intente de nuevo.");
                    break;
            }
        }
    }

    static void CrearUsuario(SqlDao sqlDao)
    {
        Console.WriteLine("\n--- Crear Usuario ---");
        Console.Write("Código de usuario: ");
        var userCode = Console.ReadLine();
        Console.Write("Nombre: ");
        var name = Console.ReadLine();
        Console.Write("Email: ");
        var email = Console.ReadLine();
        Console.Write("Contraseña: ");
        var password = Console.ReadLine();
        Console.Write("Estado: ");
        var status = Console.ReadLine();
        Console.Write("Fecha de nacimiento (yyyy-MM-dd): ");
        var birthDate = DateTime.Parse(Console.ReadLine());


        var user = new User()
        {
            UserCode = userCode,
            Name = name,
            Email = email,
            Password = password,
            Status = status,
            BirthDate = birthDate
        };
        var um = new UserManager();
        um.Create(user);
     
    }

    static void ActualizarUsuario(SqlDao sqlDao)
    {
        Console.WriteLine("\n--- Actualizar Usuario ---");
        Console.Write("Digite el ID del usuario que deseas editar: ");
        int id = int.Parse(Console.ReadLine());  

        Console.Write("Nombre: ");
        string name = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Contraseña: ");
        string password = Console.ReadLine();
        Console.Write("Estado: ");
        string status = Console.ReadLine();
        Console.Write("Fecha de nacimiento (yyyy-MM-dd): ");
        string birthDate = Console.ReadLine();

        var userOperation = new SqlOperations();
        userOperation.ProcedureName = "UP_USER_PR";
        userOperation.Addint64Param("P_Id", id);
        userOperation.AddStringParameter("P_Name", name);
        userOperation.AddStringParameter("P_Email", email);
        userOperation.AddStringParameter("P_Password", password);
        userOperation.AddStringParameter("P_Status", status);
        userOperation.AddStringParameter("P_BirthDate", birthDate);
        sqlDao.ExecuteProcedure(userOperation);
        Console.WriteLine("Usuario actualizado exitosamente.\n");
    }
    static void ConsultarUsuario(SqlDao sqlDao)
    {
       var uCrud = new UserCrudFactory();
        var listUsers = uCrud.RetrieveAll<User>();
        foreach(var user in listUsers)
        {
            Console.WriteLine(JsonConvert.SerializeObject(user));
        }
    }
    static void ConsusltarUsuarioPorId(SqlDao sqlDao)
    {
        Console.WriteLine("\n--- Consultar Usuario por ID ---");
        Console.Write("Ingrese el ID del usuario: ");
        int userId = int.Parse(Console.ReadLine());

        var uCrud = new UserCrudFactory();
        var user = uCrud.RetrieveById<User>(userId);

        if (user != null)
        {
            Console.WriteLine(JsonConvert.SerializeObject(user));

        }
    }

    static void ConsualtarUsuarioPorUserCode(SqlDao sqlDao)
    {
        Console.WriteLine("\n--- Consultar Usuario por Código ---");
        Console.Write("Ingrese el código de usuario: ");
        string userCode = Console.ReadLine();
        var uCrud = new UserCrudFactory();
        var user = uCrud.RetrieveByUserCode<User>(new User { UserCode= userCode});
        if (user != null)
        {
            Console.WriteLine(JsonConvert.SerializeObject(user));
        }
    }

    static void EliminarUsuarios(SqlDao sqlDao)
    {
        Console.WriteLine("\n---Eliminar Usuarios---");
        Console.WriteLine("Ingrese el Id del usuario que desaeas eliminar");
        int userId = int.Parse(Console.ReadLine());
        var userOperation = new SqlOperations();
        userOperation.ProcedureName = "Delete_USER_PR";
        userOperation.Addint64Param("P_Id", userId);
        var results = sqlDao.ExecuteQueryProcedure(userOperation);

    }

    static void RegistrarPelicula(SqlDao sqlDao)
    {
        Console.WriteLine("\n--- Registrar Película ---");
        Console.Write("Título: ");
        var title = Console.ReadLine();
        Console.Write("Descripción: ");
        var description = Console.ReadLine();
        Console.Write("Fecha de lanzamiento (yyyy-MM-dd): ");
        var releaseDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Género: ");
        var genre = Console.ReadLine();
        Console.Write("Director: ");
        var director = Console.ReadLine();

        var movie = new Movie()
        {
            Title = title,
            description = description,
            ReleaseDate = releaseDate,
            Genre = genre,
            Director = director
        };
        var Mom  = new MovieManager();
        Mom.Create(movie);

    }

    static void ConsultarPeliculas(SqlDao sqlDao)
    {
        var movieCrud = new MovieCrudFactory();
        var listMovies = movieCrud.RetrieveAll<Movie>();
        foreach (var movie in listMovies)
        {
            Console.WriteLine(JsonConvert.SerializeObject(movie));
        }
    }

    static void ConsultarPeliculasPorId(SqlDao sqlDao)
    {
        Console.WriteLine("\n--- Consultar Película por ID ---");
        Console.Write("Ingrese el ID de la película: ");
        int movieId = int.Parse(Console.ReadLine());
        var movieCrud = new MovieCrudFactory();
        var movie = movieCrud.RetrieveById<Movie>(movieId);
        if (movie != null)
        {
            Console.WriteLine(JsonConvert.SerializeObject(movie));
        }
    }



}



