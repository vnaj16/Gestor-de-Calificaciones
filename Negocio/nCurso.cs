using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Entidades;
using Datos;

namespace Negocio
{
    public class nCurso
    {
        private dCurso Curso_DB { get; set; }

        public nCurso()
        {
            Curso_DB = new dCurso();
        }

        public bool Registrar_Curso(eCurso Curso, out string message)
        {//En observacion
            /*nCampo Campo_N = new nCampo();

            if (status)
            {
                foreach (eCampo x in Curso.Campos)
                {
                    status = Campo_N.Registrar_Campo(x, out message);
                }
            }*/
            return Curso_DB.Insertar(Curso, out message); ;
        
        }

        /*public List<eCurso> GetCursos(string ID_Ciclo)
        {
            return Curso_DB.GetCursos(ID_Ciclo);
        }*/

        public bool Actualizar_Nota(eCurso Curso)
        {
            return Curso_DB.Actualizar(Curso.Codigo + "-0" + Curso.Vez,Curso.Promedio);
        }

        public ObservableCollection<eCurso> GetCursos(string ID_Ciclo)
        {
            return Curso_DB.GetCursos(ID_Ciclo);
        }

        public void GetCamposRegistrados(eCurso obj, out int Numero_Campos_Registrados, out int Porcentaje_Campos_Registrados)
        {
            Curso_DB.GetCamposRegistrados(obj, out Numero_Campos_Registrados, out Porcentaje_Campos_Registrados);
        }

        public float GetPorcentajePorCompletar(eCurso obj)
        {
            return Curso_DB.GetPorcentajePorCompletar(obj);
        }
    }
}
