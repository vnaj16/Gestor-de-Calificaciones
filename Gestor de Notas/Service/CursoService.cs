using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestor_de_Notas.Dto;

namespace Gestor_de_Notas.Service
{
    public interface CursoService
    {
        CursoDto Create(CursoCreateDto model);
        void Update(CursoUpdateDto model, int Id);
        void Delete(int Id);


        ///delarar implementaciones extra
        public List<CursoDto> GetCursosCiclo(int Id);

    }
}
