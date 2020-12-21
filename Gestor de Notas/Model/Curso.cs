using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestor_de_Notas.Model
{
    public class Curso
    {
        public int CursoId { get; set; }
        public string CursoCodigo { get; set; }
        public string CursoNombre { get; set; }
        public int CursoCreditos { get; set; }
        public int CursoCantidadCampos { get; set; }
        public float CursoPromedio { get; set; }
        public int CursoVez { get; set; }
        public Ciclo Ciclo { get; set; }
        public int CicloId { get; set; }
        public List<Campo> Campos { get; set; }
    }
}
