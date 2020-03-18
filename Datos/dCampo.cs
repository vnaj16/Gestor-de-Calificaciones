using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using System.Data.SqlClient;

namespace Datos
{
    public class dCampo
    {
        private DataBase DB { get; set; }

        public dCampo()
        {
            DB = new DataBase();
        }

        public bool Insertar(eCampo obj, out string message)
        {//Correcto
            try
            {
                SqlConnection Conexion = DB.Conectar();
                string INSERT = string.Format("Insert into Campo Values('{0}',{1},'{2}',{3},{5},'{4}',{6})",
                   obj.Tipo,obj.Numero,obj.Descripcion,obj.Peso,obj.Curso.Codigo+"-0"+obj.Curso.Vez,obj.Nota, 0);
                SqlCommand Comando = new SqlCommand(INSERT, Conexion);
                Comando.ExecuteNonQuery();

                message = "Campo Registrado";
                return true;
            }
            catch (Exception e) { message = e.Message; return false; }
            finally { DB.Desconectar(); }
        }

        public bool Actualizar(string Tipo_Campo,int Numero_Tipo,string ID_Curso, float Nota)
        {//Correcto
            try
            {
                SqlConnection Conexion = DB.Conectar();
                string UPDATE = string.Format("UPDATE Campo Set Nota = {0}, Rellenado = 1 where ID_Curso ='{1}' and Tipo ='{2}' and Numero = {3}",
                    Nota, ID_Curso,Tipo_Campo,Numero_Tipo);
                SqlCommand Comando = new SqlCommand(UPDATE, Conexion);
                int rows = Comando.ExecuteNonQuery();

                return true;
            }
            catch (Exception e) { return false; }
            finally { DB.Desconectar(); }
        }

        public List<eCampo> GetCampos(out string message)//Obtengo todos los campos
        {//Correcto
            try
            {
                List<eCampo> Lista_Campos = new List<eCampo>();
                eCampo aux = null;

                SqlConnection Conexion = DB.Conectar();
                string SELECT = "SELECT * FROM Campo";
                SqlCommand Comando = new SqlCommand(SELECT, Conexion);
                SqlDataReader Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    aux = new eCampo();
                    aux.Tipo = (string)Reader["Tipo"];
                    aux.Numero = (int)Reader["Numero"];
                    aux.Descripcion = (string)Reader["Descripcion"];
                    aux.Peso = (int)Reader["Peso"];
                    aux.Nota = Convert.ToSingle(Reader["Nota"]);
                    aux.Rellenado = (bool)Reader["Rellenado"];

                    /////
                    ///Desfragmentacion de ID (Codigo + Periodo)
                    /////
                    string ID_Curso = (string)Reader["ID_Curso"];
                    int indice = ID_Curso.IndexOf('-');
                    //ma745-01  i=5
                    aux.Curso.Codigo = ID_Curso.Substring(0,indice);
                    aux.Curso.Vez = Convert.ToInt32(ID_Curso.Substring(indice + 1));

                    Lista_Campos.Add(aux);
                }


                Reader.Close();
                message = "Correcto";
                return Lista_Campos;
            }
            catch (Exception e)
            {
                message = e.Message;
                return null;
            }
            finally
            {
                DB.Desconectar();
            }
        }

        public List<eCampo> GetCampos(string ID_Curso)//Obtengo los campos de un determinado curso
        {//Correcto
            try
            {
                List<eCampo> Lista_Campos = new List<eCampo>();
                eCampo aux = null;

                SqlConnection Conexion = DB.Conectar();
                string SELECT = string.Format("SELECT * FROM Campo where ID_Curso = '{0}'",ID_Curso);
                SqlCommand Comando = new SqlCommand(SELECT, Conexion);
                SqlDataReader Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    aux = new eCampo();
                    aux.Tipo = (string)Reader["Tipo"];
                    aux.Numero = (int)Reader["Numero"];
                    aux.Descripcion = (string)Reader["Descripcion"];
                    aux.Peso = (int)Reader["Peso"];
                    aux.Nota = Convert.ToSingle(Reader["Nota"]);
                    aux.Rellenado = (bool)Reader["Rellenado"];

                    /////
                    ///Desfragmentacion de ID (Codigo + Periodo)
                    /////
                    int indice = ID_Curso.IndexOf('-');
                    //ma745-01  i=5
                    aux.Curso.Codigo = ID_Curso.Substring(0, indice);
                    aux.Curso.Vez = Convert.ToInt32(ID_Curso.Substring(indice + 1));

                    Lista_Campos.Add(aux);
                }


                Reader.Close();
        
                return Lista_Campos;
            }
            catch (Exception e)
            {
               
                return null;
            }
            finally
            {
                DB.Desconectar();
            }
        }

        public ObservableCollection<eCampo> GetCampos_(string ID_Curso)//Obtengo los campos de un determinado curso, pero dentro de un ObsCollect
        {//Correcto
            try
            {
                ObservableCollection<eCampo> ObservableCollection_Campos = new ObservableCollection<eCampo>();
                eCampo aux = null;

                SqlConnection Conexion = DB.Conectar();
                string SELECT = string.Format("SELECT * FROM Campo where ID_Curso = '{0}'", ID_Curso);
                SqlCommand Comando = new SqlCommand(SELECT, Conexion);
                SqlDataReader Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    aux = new eCampo();
                    aux.Tipo = (string)Reader["Tipo"];
                    aux.Numero = (int)Reader["Numero"];
                    aux.Descripcion = (string)Reader["Descripcion"];
                    aux.Peso = Convert.ToSingle(Reader["Peso"]);
                    aux.Nota = Convert.ToSingle(Reader["Nota"]);
                    aux.Rellenado = (bool)Reader["Rellenado"];

                    /////
                    ///Desfragmentacion de ID (Codigo + Periodo)
                    /////
                    int indice = ID_Curso.IndexOf('-');
                    //ma745-01  i=5
                    aux.Curso.Codigo = ID_Curso.Substring(0, indice);
                    aux.Curso.Vez = Convert.ToInt32(ID_Curso.Substring(indice + 1));

                    ObservableCollection_Campos.Add(aux);
                }


                Reader.Close();

                return ObservableCollection_Campos;
            }
            catch (Exception e)
            {

                return null;
            }
            finally
            {
                DB.Desconectar();
            }
        }
    
        public bool existCampo(eCampo Campo)
        {//Correcto
            try
            {
                SqlConnection Conexion = DB.Conectar();
                string SELECT = string.Format("SELECT * FROM Campo where Tipo ='{0}' AND Numero = {1} AND ID_Curso ='{2}'",
                    Campo.Tipo,Campo.Numero,Campo.Curso.Codigo+"-0"+ Campo.Curso.Vez);
                SqlCommand Comando = new SqlCommand(SELECT, Conexion);

                SqlDataReader Reader = Comando.ExecuteReader();

                int rows = 0;

                while (Reader.Read())//Si la tabla obtenida tiene filas la leo
                {
                    rows++;
                }


                Reader.Close();

                return rows>=1? true: false; //Si devuelve una fila, quiere decir que si existe
            }
            catch (Exception e) { return false; }
            finally { DB.Desconectar(); }
        }

    }
}
