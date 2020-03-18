using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace Datos
{
    public class dCurso
    {
        private DataBase DB { get; set; }


        public dCurso()
        {
            DB = new DataBase();
        }

        public bool Insertar(eCurso obj, out string message)
        {//Correcto
            try
            {
                SqlConnection Conexion = DB.Conectar();
                string INSERT = string.Format("Insert into Curso Values('{0}','{1}','{2}',{3},{4},{7},{5},'{6}')", 
                    obj.Codigo+"-0"+obj.Vez,obj.Codigo,obj.Nombre,obj.Creditos,obj.Numero_Campos,obj.Vez,obj.Ciclo.Periodo,obj.Promedio);
                SqlCommand Comando = new SqlCommand(INSERT, Conexion);
                Comando.ExecuteNonQuery();

                message = "Curso Registrado";
                return true;
            }
            catch (Exception e) { message = e.Message; return false; }
            finally { DB.Desconectar(); }
        }

        public void GetCamposRegistrados(eCurso obj, out int Numero_Campos_Registrados, out int Porcentaje_Campos_Registrados)
        {
            Numero_Campos_Registrados = 0;
            Porcentaje_Campos_Registrados = 0;
            try
            {
                SqlConnection Conexion = DB.Conectar();
                string SELECT = string.Format("select COUNT(*), SUM(Peso) from Campo group by ID_Curso having ID_Curso = '{0}'", obj.Codigo + "-0" + obj.Vez);
                SqlCommand Comando = new SqlCommand(SELECT, Conexion);
                SqlDataReader Reader = Comando.ExecuteReader();

                while (Reader.Read())//Si es diferente de cero
                {
                    Numero_Campos_Registrados = Convert.ToInt32(Reader[0]);
                    Porcentaje_Campos_Registrados = Convert.ToInt32(Reader[1]);
                }


                Reader.Close();
            }
            catch (Exception e)
            {
            }
            finally
            {
                DB.Desconectar();
            }
        }
        /*
         SELECT SUM(Peso) 
FROM Campo 
WHERE ID_Curso = 'SI407-01'
             */

        public float GetPorcentajePorCompletar(eCurso obj)
        {
            float Porcentaje_Completado = 0;
            try
            {
                SqlConnection Conexion = DB.Conectar();
                string SELECT = string.Format("SELECT SUM(Peso) FROM Campo WHERE ID_Curso = '{0}'", obj.Codigo + "-0" + obj.Vez);
                SqlCommand Comando = new SqlCommand(SELECT, Conexion);
                SqlDataReader Reader = Comando.ExecuteReader();

                while (Reader.Read())//Si es diferente de cero
                {
                    Porcentaje_Completado = Convert.ToSingle(Reader[0]);
                }

                Reader.Close();
                return 100 - Porcentaje_Completado;
            }
            catch (Exception e)
            {
                return 100 - Porcentaje_Completado;
            }
            finally
            {
                DB.Desconectar();
            }

        }

        public bool Actualizar(string ID_Curso, float Promedio)//Guardo el nuevo promedio del curso
        {//Correcto
            try
            {
                SqlConnection Conexion = DB.Conectar();
                string UPDATE = string.Format("UPDATE Curso Set Promedio = {0} where ID ='{1}'", Promedio, ID_Curso);
                SqlCommand Comando = new SqlCommand(UPDATE, Conexion);
                int rows = Comando.ExecuteNonQuery();
                return true;
            }
            catch (Exception e) { return false; }
            finally { DB.Desconectar(); }
        }

        public List<eCurso> GetCursos()
        {//Correcto
            try
            {
                List<eCurso> Lista_Cursos = new List<eCurso>();
                eCurso aux = null;

                SqlConnection Conexion = DB.Conectar();
                string SELECT = "SELECT * FROM CURSO";
                SqlCommand Comando = new SqlCommand(SELECT, Conexion);
                SqlDataReader Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    aux = new eCurso();
                    aux.Codigo = (string)Reader["Codigo"];
                    aux.Creditos = (int)Reader["Creditos"];
                    aux.Nombre = (string)Reader["Nombre"];
                    aux.Numero_Campos = (int)Reader["Cantidad_Campos"];
                    aux.Promedio = Convert.ToSingle(Reader["Promedio"]);
                    aux.Vez = (int)Reader["Vez"];
                    aux.Ciclo.Periodo = (string)Reader["ID_Ciclo"];

                    Lista_Cursos.Add(aux);
                }


                Reader.Close();
                return Lista_Cursos;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                DB.Desconectar();
            }
        }//Para Analytics

        public eCurso GetCurso(string ID_Curso, out string message)
        {//Correcto
            try
            {
                eCurso aux = null;

                SqlConnection Conexion = DB.Conectar();
                string SELECT = string.Format("SELECT * FROM CURSO WHERE ID = '{0}'", ID_Curso);
                SqlCommand Comando = new SqlCommand(SELECT, Conexion);
                SqlDataReader Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    aux = new eCurso();
                    aux.Codigo = (string)Reader["Codigo"];
                    aux.Creditos = (int)Reader["Creditos"];
                    aux.Nombre = (string)Reader["Nombre"];
                    aux.Numero_Campos = (int)Reader["Cantidad_Campos"];
                    aux.Promedio = Convert.ToSingle(Reader["Promedio"]);
                    aux.Vez = (int)Reader["Vez"];
                    aux.Ciclo.Periodo = (string)Reader["ID_Ciclo"];
                }


                Reader.Close();
                message = "Correcto";
                return aux;
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
        }//En observación       

        /*public List<eCurso> GetCursos(string ID_Ciclo)//Lo recomendable seria obtener todos los cursos de un determinado ciclo
        {//Correcto
            try
            {
                List<eCurso> Lista_Cursos = new List<eCurso>();
                eCurso aux = null;

                SqlConnection Conexion = DB.Conectar();
                string SELECT = string.Format("SELECT * FROM CURSO WHERE ID_Ciclo = '{0}'", ID_Ciclo);
                SqlCommand Comando = new SqlCommand(SELECT, Conexion);
                SqlDataReader Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    aux = new eCurso();
                    aux.Codigo = (string)Reader["Codigo"];
                    aux.Creditos = (int)Reader["Creditos"];
                    aux.Nombre = (string)Reader["Nombre"];
                    aux.Numero_Campos = (int)Reader["Cantidad_Campos"];
                    aux.Promedio = Convert.ToSingle(Reader["Promedio"]);
                    aux.Vez = (int)Reader["Vez"];
                    aux.Ciclo.Periodo = ID_Ciclo;

                    Lista_Cursos.Add(aux);
                }


                Reader.Close();
                return Lista_Cursos;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                DB.Desconectar();
            }
        }*/

        public ObservableCollection<eCurso> GetCursos(string ID_Ciclo)
        {//Correcto
            try
            {
                ObservableCollection<eCurso> ObservableCollectiona_Cursos = new ObservableCollection<eCurso>();
                eCurso aux = null;

                SqlConnection Conexion = DB.Conectar();
                string SELECT = string.Format("SELECT * FROM CURSO WHERE ID_Ciclo = '{0}'", ID_Ciclo);
                SqlCommand Comando = new SqlCommand(SELECT, Conexion);
                SqlDataReader Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    aux = new eCurso();
                    aux.Codigo = (string)Reader["Codigo"];
                    aux.Creditos = (int)Reader["Creditos"];
                    aux.Nombre = (string)Reader["Nombre"];
                    aux.Numero_Campos = (int)Reader["Cantidad_Campos"];
                    aux.Promedio = Convert.ToSingle(Reader["Promedio"]);
                    aux.Vez = (int)Reader["Vez"];
                    aux.Ciclo.Periodo = ID_Ciclo;

                    ObservableCollectiona_Cursos.Add(aux);
                }


                Reader.Close();
                return ObservableCollectiona_Cursos;
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
    }
}
