using DataAccess.CRUD;
using DTOs;
using EllipticCurve;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class UserManager : BaseManager
    {
        public async Task Create(User user)
        {
            try
            {
                if (IsOver18(user))
                {
                    var uCrud = new UserCrudFactory();
                    var uExist = uCrud.RetrieveByUserCode<User>(user);
                    if (uExist == null)
                    {
                        uExist = uCrud.RetrieveByEmail<User>(user);
                        if (uExist == null)
                        {
                            uCrud.Create(user);
                            await SendWelcomeEmail(user);
                        }
                        else
                        {
                            throw new Exception("Este correo electronico ya esta resgistrado");
                        }
                    }
                    else
                    {
                        throw new Exception("El codigo de usuario no esta disponible");
                    }

                }
                else
                {
                    throw new Exception("Usuario no puede registrarse porque no cumple el requisito minimo de la edad");
                }
            }
            catch (Exception ex)
            {
                ManagerExection(ex);
            }

        }

        public List<User> RetrieveAll()
        {
            var uCrud = new UserCrudFactory();
            return uCrud.RetrieveAll<User>();
        }

        public User RetrieveById(int id)
        {
            var uCrud = new UserCrudFactory();
            return uCrud.RetrieveById<User>(id);
        }

        public User RetrieveByUserCode(string userCode)
        {
            var uCrud = new UserCrudFactory();
            var udto = new User { UserCode = userCode };
            return uCrud.RetrieveByUserCode<User>(udto);
        }

        public User RetrieveByEmail(string email)
        {
            var uCrud = new UserCrudFactory();
            var udto = new User { Email = email };
            return uCrud.RetrieveByEmail<User>(udto);
        }

        public User Update(User user)
        {
            try
            {
                if (IsOver18(user))
                {
                    var uCrud = new UserCrudFactory();
                    var existingUser = uCrud.RetrieveById<User>(user.id);

                    if (existingUser != null)
                    {
                        var emailConflict = uCrud.RetrieveByEmail<User>(user);
                        if (emailConflict == null || emailConflict.id == user.id)
                        {
                            uCrud.Update(user);
                            return RetrieveById(user.id);
                        }
                        else
                        {
                            throw new Exception("Este correo electronico ya esta registrado por otro usuario");
                        }
                    }
                    else
                    {
                        throw new Exception("El usuario no existe");
                    }
                }
                else
                {
                    throw new Exception("Usuario no puede actualizarse porque no cumple el requisito minimo de la edad");
                }
            }
            catch (Exception ex)
            {
                ManagerExection(ex);
                return null;
            }
        }

        public User Delete(int id)
        {
            try
            {
                var uCrud = new UserCrudFactory();
                var user = uCrud.RetrieveById<User>(id);
                if (user != null)
                {
                    uCrud.Delete(user);
                    return user;
                }
                else
                {
                    throw new Exception("El usuario no existe");
                }
            }
            catch (Exception ex)
            {
                ManagerExection(ex);
                return null;
            }
        }
        private bool IsOver18(User user)
        {
            var curretDate = DateTime.Now;
            int age = curretDate.Year - user.BirthDate.Year;
            if (user.BirthDate > curretDate.AddYears(-age).Date)
            {
                age--;
            }
            return age >= 18;
        }

        private async Task SendWelcomeEmail(User user)
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
                var subject = "Bienvenido a CenfoCinemas";
                var plainTextContent = $"Hola {user.Name}, gracias por registrarte.";
                var htmlContent = $"<strong>Hola {user.Name}</strong><br/>Gracias por registrarte en CenfoCinemas.";

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
