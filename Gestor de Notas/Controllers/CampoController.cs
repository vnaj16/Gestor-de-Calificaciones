using Gestor_de_Notas.Dto;
using Gestor_de_Notas.Service;
using Microsoft.AspNetCore.Mvc;

namespace Gestor_de_Notas.Controllers
{
    [ApiController]
    [Route("campos")]
    public class CampoController : ControllerBase
    {
        private readonly CampoService camposervice;
        public CampoController(CampoService _camposervice)
        {
            camposervice = _camposervice;
        }
        [HttpPost]
        public ActionResult Create(CampoCreateDto campo)
        {
            camposervice.Create(campo);
            return Ok();
        }



        [HttpPut("{id}/{tipo}/{numero}")]
        public ActionResult Update(CampoUpdateDto model, int id, string tipo, int numero )
        {
            camposervice.Update(model, tipo, numero, id);
            return NoContent();
        }



        [HttpDelete("{id}/{tipo}/{numero}")]
        public ActionResult Delete(int id, string tipo, int numero)
        {
            camposervice.Delete(tipo, numero, id);
            return NoContent();
        }
    }
}