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
                return Ok("Usuario creado correctamente" + user);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
        public ActionResult RetrieveByEmail(String email )
        {
            try
            {
                var um = new UserManager();
                var userByEmail = um.RetrieveByEmail(email);
                if(userByEmail == null)
                {
                    return NotFound($"El correo {email} que estas buscando no se encuentra");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveByUserCode")]
        public ActionResult RetrieveByUserCode(String userCode)
        {
            try
            {
                var um = new UserManager();
                var userByUserCode = um.RetrieveByUserCode(userCode);

                if(userByUserCode == null)
                {
                    return NotFound($"El codigo de usuario {userCode} que estas buscando no se encuentra ");
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
