using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using Datos;

namespace Negocio
{
    public class nCiclo
    {
        private dCiclo Ciclo_DB { get; set; }

        public nCiclo()
        {
            Ciclo_DB = new dCiclo();
        }

        public bool Registrar_Ciclo(eCiclo Ciclo, out string message)
        {//En Observacion
            //Por cuestiones de prueba, proceder a comentar la seccion del IF


            /*if (status)
            {
                nCurso Curso_N = new nCurso();
                //Registro los cursos
                foreach (eCurso x in Ciclo.Cursos)
                {
                    status= Curso_N.Registrar_Curso(x, out message);
                }
            }*/
            return Ciclo_DB.Insertar(Ciclo, out message); //Registro los datos del ciclo
        }

        public bool Registrar_PromedioBeca(eCiclo Ciclo, out string message)
        {
            return Ciclo_DB.Actualizar_PromedioBeca(Ciclo,out message);
        }

        public bool Actualizar_Nota(eCiclo Ciclo)
        {
            return Ciclo_DB.Actualizar(Ciclo.Periodo, Ciclo.Promedio);
        }

        public List<eCiclo> GetCiclos()
        {
            return Ciclo_DB.GetCiclos();
        }

        public List<string> GetIDCiclos()
        {
            return Ciclo_DB.GetIDCiclos();
        }

        public eCiclo GetCiclo(string ID_Ciclo, out string message)
        {
            return Ciclo_DB.GetCiclo(ID_Ciclo, out message);
        }

        public float getPromedio_Acumulado()
        {
            return Ciclo_DB.getPromedioAcumulado();
        }

        public int GetCursosRegistrados(string ID_Ciclo)
        {
            return Ciclo_DB.GetCursosRegistrados(ID_Ciclo);
        }
    }
}
