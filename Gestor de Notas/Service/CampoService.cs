using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestor_de_Notas.Dto;

namespace Gestor_de_Notas.Service
{
    public interface CampoService
    {
        CampoDto Create(CampoCreateDto model);
        void Update(CampoUpdateDto model, string Tipo, int Numero, int Id);
        void Delete(string Tipo, int Numero, int Id);
    }
}
