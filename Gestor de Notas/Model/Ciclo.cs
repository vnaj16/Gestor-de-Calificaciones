using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestor_de_Notas.Model
{
    public class Ciclo
    {
        public int CicloId { get; set; }
        public int CicloCantidadCursos { get; set; }
        public float CicloPromedio { get; set; }
        public float CicloPromedioBeca { get; set; }
        public List<Curso> Cursos { get; set; }
    }
}
