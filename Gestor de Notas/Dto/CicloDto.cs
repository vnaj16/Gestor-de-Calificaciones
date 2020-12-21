using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestor_de_Notas.Dto
{
    public class CicloDto
    {
        public int CicloId { get; set; }
        public int CicloCantidadCursos { get; set; }
        public float CicloPromedio { get; set; }
        public float CicloPromedioBeca { get; set; }
        public List<CursoDto> Cursos { get; set; }
    }
    public class CicloCreateDto
    {
        public int CicloCantidadCursos { get; set; }
        public float CicloPromedio { get; set; }
        public float CicloPromedioBeca { get; set; }
    }
    public class CicloUpdateDto
    {
        public int CicloCantidadCursos { get; set; }
        public float CicloPromedio { get; set; }
        public float CicloPromedioBeca { get; set; }
    }

}
