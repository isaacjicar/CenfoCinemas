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
            Console.WriteLine("3. Actualizar usuario");
            Console.WriteLine("4. Eliminar usuario");
            Console.WriteLine("5. Registrar película");
            Console.WriteLine("6. Actualizar usuario");
            Console.WriteLine("7. Actualizar película");
            Console.WriteLine("8. Eliminar película");
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
                    ActualizarUsuario(sqlDao);
                    break;
                case "4":
                    EliminarUsuarios(sqlDao);
                    break;
                case "5":
                    RegistrarPelicula(sqlDao);
                    break;
                case "6":
                    
                    break;
                case "7":
                    break;
                case "8":
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
        var uCrud = new UserCrudFactory();
        uCrud.Create(user);
     
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
        string title = Console.ReadLine();
        Console.Write("Descripción: ");
        string description = Console.ReadLine();
        Console.Write("Fecha de lanzamiento (yyyy-MM-dd): ");
        string releaseDate = Console.ReadLine();
        Console.Write("Género: ");
        string genre = Console.ReadLine();
        Console.Write("Director: ");
        string director = Console.ReadLine();

        var movieOperation = new SqlOperations();
        movieOperation.ProcedureName = "CRE_MOVIES_PR";
        movieOperation.AddStringParameter("P_Title", title);
        movieOperation.AddStringParameter("P_Description", description);
        movieOperation.AddStringParameter("P_ReleaseDate", releaseDate);
        movieOperation.AddStringParameter("P_Genre", genre);
        movieOperation.AddStringParameter("P_Director", director);

        sqlDao.ExecuteProcedure(movieOperation);
        Console.WriteLine("Película registrada exitosamente.\n");
    }



}