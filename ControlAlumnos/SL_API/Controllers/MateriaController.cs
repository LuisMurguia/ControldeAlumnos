using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_API.Controllers
{
    public class MateriaController : ApiController
    {

        // GET: api/Materia
        [HttpGet]
        [Route("api/Materia")]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Materia.GetAllEF();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }



        // GET: api/Materia/5
        [HttpGet]
        [Route("api/Materia/{IdMateria}")]
        public IHttpActionResult GetById(int IdMateria)
        {
            ML.Result result = BL.Materia.GetByIdWebAPI(IdMateria);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }


        }


        // POST: api/Materia
        [HttpPost]
        [Route("api/Materia/Add")]
        public IHttpActionResult Post([FromBody] ML.Materia materia)
        {
            ML.Result result = BL.Materia.AddWebAPI(materia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }

        }

            // PUT: api/Materia/5
            [HttpPut]
            [Route("api/Materia/Update/{IdMateria}")]
            public IHttpActionResult Put(int IdMateria, [FromBody] ML.Materia materia)
            {
                materia.IdMateria = IdMateria;
                ML.Result result = BL.Materia.UpdateWebAPI(materia);
                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }


        // DELETE: api/Materia/5
        [HttpDelete]
        [Route("api/Materia/Delete/{IdMateria}")]
        public IHttpActionResult Delete(int IdMateria)
        {

            ML.Result result = BL.Materia.DeleteWebAPI(IdMateria);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
