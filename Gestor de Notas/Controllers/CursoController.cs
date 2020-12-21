using System.Collections.Generic;
using Gestor_de_Notas.Dto;
using Gestor_de_Notas.Service;
using Microsoft.AspNetCore.Mvc;

namespace Gestor_de_Notas.Controllers
{
    [ApiController]
    [Route("cursos")]
    public class CursoController : ControllerBase
    {
        private readonly CursoService cursoservice;
        public CursoController(CursoService _cursoservice)
        {
            cursoservice = _cursoservice;
        }
        //put cambiar entero  / !!  patch partes


        [HttpPost]
        public ActionResult Create(CursoCreateDto curso)
        {
            cursoservice.Create(curso);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, CursoUpdateDto model)
        {
            cursoservice.Update(model, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            cursoservice.Delete(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<List<CursoDto>> GetAll(int id) => cursoservice.GetCursosCiclo(id);
    }
}