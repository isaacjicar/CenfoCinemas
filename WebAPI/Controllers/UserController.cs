using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("create")]
        public ActionResult create(User user)
        {
            try
            {
                var um = new UserManager();
                um.Create(user);

                return Ok(new { message = "Usuario creado correctamente", data = user });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpGet]
        [Route("RetrieveAll")]
        public ActionResult RetrieveAll()
        {
            try
            {
                var um = new UserManager();
                var listResults = um.RetrieveAll();

                return Ok(listResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveById")]
        public ActionResult RetrieveById(int id)
        {
            try
            {
                var um = new UserManager();
                var userById = um.RetrieveById(id);
                if (userById == null)
                {
                    return NotFound($"El Usuario con el Id {id} no fue encotrado");
                }

                return Ok(userById);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route("RetrieveByEmail")]
        public ActionResult RetrieveByEmail(string email)
        {
            try
            {
                var um = new UserManager();

                var userByEmail = um.RetrieveAll()
                                    .FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

                if (userByEmail == null)
                {
                    return NotFound($"El correo {email} que estás buscando no se encuentra");
                }

                return Ok(userByEmail); // ✅ Envía el usuario como JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet]
        [Route("RetrieveByUserCode")]
        public ActionResult RetrieveByUserCode(string userCode)
        {
            try
            {
                var um = new UserManager();

                var userByUserCode = um.RetrieveAll()
                                       .FirstOrDefault(u => u.UserCode.ToLower() == userCode.ToLower());

                if (userByUserCode == null)
                {
                    return NotFound($"El código de usuario {userCode} que estás buscando no se encuentra.");
                }

                return Ok(userByUserCode);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut]
        [Route("Update")]
        public ActionResult Update(User user)
        {
            try
            {
                var um = new UserManager();
                um.Update(user);
                return Ok(user);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                var um = new UserManager();
                um.Delete(id);
                return Ok(id);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
