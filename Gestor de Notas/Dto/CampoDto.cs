using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestor_de_Notas.Dto
{
    public class CampoDto
    {
        public string CampoTipo { get; set; }
        public int CampoNumero { get; set; }
        public string CampoDescripcion { get; set; }
        public float CampoPeso { get; set; }
        public float CampoNota { get; set; }
        public bool CampoRellenado { get; set; }
        public CursoDto Curso { get; set; }
        public int CursoId { get; set; }
    }
    public class CampoCreateDto
    {
        public string CampoTipo { get; set; }
        public int CampoNumero { get; set; }
        public string CampoDescripcion { get; set; }
        public float CampoPeso { get; set; }
        public int CursoId { get; set; }
    }
    public class CampoUpdateDto
    {
        public string CampoTipo { get; set; }
        public int CampoNumero { get; set; }
        public string CampoDescripcion { get; set; }
        public float CampoPeso { get; set; }
        public float CampoNota { get; set; }
        public bool CampoRellenado { get; set; }
    }
}
