using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestor_de_Notas.Dto
{
    public class CursoDto
    {
        public int CursoId { get; set; }
        public string CursoCodigo { get; set; }
        public string CursoNombre { get; set; }
        public int CursoCreditos { get; set; }
        public int CursoCantidadCampos { get; set; }
        public float CursoPromedio { get; set; }
        public int CursoVez { get; set; }
        public CicloDto Ciclo { get; set; }
        public int CicloId { get; set; }
        public List<CampoDto> Campos { get; set; }
    }
    public class CursoCreateDto
    {
        public string CursoCodigo { get; set; }
        public string CursoNombre { get; set; }
        public int CursoCreditos { get; set; }
        public int CicloId { get; set; }
    }
    public class CursoUpdateDto
    {
        public string CursoCodigo { get; set; }
        public string CursoNombre { get; set; }
        public int CursoCreditos { get; set; }
        public int CursoCantidadCampos { get; set; }
        public float CursoPromedio { get; set; }
        public int CursoVez { get; set; }
        public int CicloId { get; set; }
    }
}
