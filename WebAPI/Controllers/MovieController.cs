using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {

        [HttpPost]
        [Route("Create")]
        public ActionResult create (Movie movie)
        {
            try
            {
                var um = new MovieManager();
                um.Create(movie);
                return Ok("Película creada correctamente: " + movie.Title);

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
                var um = new MovieManager();
                var listResults = um.RetrieveAll();

                return Ok(listResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveByTitle")]
        public ActionResult RetrieveByTitle(String title)
        {
            try
            {
                var um = new MovieManager();
                var userByTitle = um.RetrieveByTitle(title);
                if (userByTitle == null)
                {
                    return NotFound($"El Titulo de la pelicula {title} que estas buscando no se encuentra");
                }
                return Ok(userByTitle);
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
                var um = new MovieManager();
                var userById = um.RetrieveById(id);
                if (userById == null)
                {
                    return NotFound($"El Titulo id {id} que estas buscando no se encuentra");
                }
                return Ok(userById);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(Movie movie)
        {
            try
            {
                var um = new MovieManager();
                um.Update(movie);
                return Ok(movie);

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
                var um = new MovieManager();
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
