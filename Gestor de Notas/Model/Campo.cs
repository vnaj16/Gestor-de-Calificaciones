using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestor_de_Notas.Model
{
    public class Campo
    {
        public string CampoTipo { get; set; }
        public int CampoNumero { get; set; }
        public string CampoDescripcion { get; set; }
        public float CampoPeso { get; set; }
        public float CampoNota { get; set; }
        public bool CampoRellenado { get; set; }
        public Curso Curso { get; set; }
        public int CursoId { get; set; }
    }
}
