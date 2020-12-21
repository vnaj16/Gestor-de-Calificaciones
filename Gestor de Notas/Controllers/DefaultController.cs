using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Gestor_de_Notas.Controllers
{
    [ApiController]
    [Route("/")]
    public class DefaultController
    {

        [HttpGet]
        public string Index()
        {
            return "Running...";
        }
    }
}
