using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using Datos;

namespace Negocio
{
    public class nCampo
    {
        private dCampo Campo_DB { get; set; }

        public nCampo()
        {
            Campo_DB = new dCampo();
        }

        public bool Registrar_Campo(eCampo Campo, out string message)//Se comprueba en la base de datos si existe ese campo
        {
            if (!Campo_DB.existCampo(Campo)) {
                return Campo_DB.Insertar(Campo, out message);
            }
            else
            {
                message = "YA EXISTE ESE CAMPO EN ESE CURSO";
                return false;
            }
        }

        public bool Actualizar_Nota(eCampo Campo)
        {
            return Campo_DB.Actualizar(Campo.Tipo, Campo.Numero, Campo.Curso.Codigo + "-0" + Campo.Curso.Vez, Campo.Nota);
        }

        public ObservableCollection<eCampo> GetCampos_(string ID_Curso)//Obtengo los campos dependiendo del curso
        {
            return Campo_DB.GetCampos_(ID_Curso);
        }
    }
}
