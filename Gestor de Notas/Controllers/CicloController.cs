using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestor_de_Notas.Dto;
using Gestor_de_Notas.Service;
using Microsoft.AspNetCore.Mvc;

namespace Gestor_de_Notas.Controllers
{
    [ApiController]
    [Route("ciclos")]
    public class CicloController : ControllerBase
    {
        private readonly CicloService cicloservice;
        public CicloController(CicloService _cicloservice)
        {
            cicloservice = _cicloservice;
        }
        //put cambiar entero  / !!  patch partes


        [HttpPost]
        public ActionResult Create(CicloCreateDto ciclo)
        {
            cicloservice.Create(ciclo);
            return Ok();
        }

        

        [HttpPut("{id}")]
        public ActionResult Update(int id, CicloUpdateDto model)
        {
            cicloservice.Update(model,id);
            return NoContent();
        }



        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            cicloservice.Delete(id);
            return NoContent();
        }

    }
}
