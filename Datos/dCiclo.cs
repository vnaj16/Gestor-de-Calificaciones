using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using System.Data.SqlClient;

namespace Datos
{
    public class dCiclo
    {
        private DataBase DB { get; set; }

        public dCiclo()
        {
            DB = new DataBase();
        }

        public float getPromedioAcumulado()
        {
            try
            {
                float PromedioAcumulado = 0;
                SqlConnection Conexion = DB.Conectar();
                string EXEC = "exec GetPromedioAcumulado";
                SqlCommand Comando = new SqlCommand(EXEC, Conexion);

                SqlDataReader Reader = Comando.ExecuteReader();


                while (Reader.Read())
                {
                    PromedioAcumulado = Convert.ToSingle(Reader[0]);
                }

                Reader.Close();
                return PromedioAcumulado;
            }
            catch (Exception e)
            {
                return 0.0f;
            }
            finally { DB.Desconectar(); }
        }

        public bool Insertar(eCiclo obj, out string message)
        {//Correcto
            try
            {
                SqlConnection Conexion = DB.Conectar();
                string INSERT = string.Format("Insert into Ciclo(ID,[Cantidad_Cursos],Promedio) Values('{0}',{1},{2})", obj.Periodo, obj.Numero_Cursos, obj.Promedio);
                SqlCommand Comando = new SqlCommand(INSERT, Conexion);
                Comando.ExecuteNonQuery();

                message = string.Format("Ciclo {0} Registrado", obj.Periodo);
                return true;
            }
            catch (Exception e) { message = e.Message; return false; }
            finally { DB.Desconectar(); }
        }


        public bool Actualizar(string ID_Ciclo, float Promedio)//Guardo en la base de datos el nuevo promedio
        {//Correcto
            try
            {
                SqlConnection Conexion = DB.Conectar();
                string UPDATE = string.Format("UPDATE Ciclo Set Promedio = {0} where ID ='{1}'", Promedio, ID_Ciclo);
                SqlCommand Comando = new SqlCommand(UPDATE, Conexion);
                int rows = Comando.ExecuteNonQuery();

                return true;
            }
            catch (Exception e) { return false; }
            finally { DB.Desconectar(); }
        }

        public bool Actualizar_PromedioBeca(eCiclo obj, out string message)//Guardo en la base de datos el promedio para la beca
        {//Correcto
            try
            {
                SqlConnection Conexion = DB.Conectar();
                string UPDATE = string.Format("UPDATE Ciclo Set Promedio_Beca = {0} where ID ='{1}'", obj.Promedio_Beca, obj.Periodo);
                SqlCommand Comando = new SqlCommand(UPDATE, Conexion);
                int rows = Comando.ExecuteNonQuery();
                message = "Promedio de Beca registrado";
                return true;
            }
            catch (Exception e) { message = e.Message; return false; }
            finally { DB.Desconectar(); }
        }

        public int GetCursosRegistrados(string ID_Ciclo)
        {
            try
            {
                int Cursos = 0;

                SqlConnection Conexion = DB.Conectar();
                string SELECT = string.Format("select count(*) from Curso Group by ID_Ciclo Having ID_Ciclo = '{0}'", ID_Ciclo);
                SqlCommand Comando = new SqlCommand(SELECT, Conexion);
                SqlDataReader Reader = Comando.ExecuteReader();

                while (Reader.Read())//Si es diferente de cero
                {
                    Cursos = Convert.ToInt32(Reader[0]);
                }


                Reader.Close();
                return Cursos;
            }
            catch (Exception e)
            {
                return 0;
            }
            finally
            {
                DB.Desconectar();
            }
        }

        public List<eCiclo> GetCiclos()
        {//Correcto
            try
            {
                List<eCiclo> Lista_Ciclos = new List<eCiclo>();
                eCiclo aux = null;

                SqlConnection Conexion = DB.Conectar();
                string SELECT = "SELECT * FROM Ciclo";
                SqlCommand Comando = new SqlCommand(SELECT, Conexion);
                SqlDataReader Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    aux = new eCiclo();

                    aux.Periodo = (string)Reader["ID"];
                    aux.Promedio = Convert.ToSingle(Reader["Promedio"]);
                    aux.Numero_Cursos = (int)Reader["Cantidad_Cursos"];
                    try
                    {
                        aux.Promedio_Beca = Convert.ToSingle(Reader["Promedio_Beca"]);
                    }
                    catch (Exception ex)
                    {
                        aux.Promedio_Beca = 0;
                    }


                    Lista_Ciclos.Add(aux);
                }


                Reader.Close();
                return Lista_Ciclos;
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

        public List<string> GetIDCiclos()
        {
            try
            {
                List<string> Lista_IDCiclos = new List<string>();

                SqlConnection Conexion = DB.Conectar();
                string SELECT = "SELECT ID FROM Ciclo";
                SqlCommand Comando = new SqlCommand(SELECT, Conexion);
                SqlDataReader Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    string ID = (string)Reader["ID"];
                    Lista_IDCiclos.Add(ID);
                }


                Reader.Close();
                return Lista_IDCiclos;
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

        public eCiclo GetCiclo(string ID_Ciclo, out string message)//Lo recomendable seria obtener un solo ciclo, depende de la opcion escogida
        {//Correcto
            try
            {
                eCiclo aux = null;

                SqlConnection Conexion = DB.Conectar();
                string SELECT = string.Format("SELECT * FROM Ciclo WHERE ID = '{0}'", ID_Ciclo);
                SqlCommand Comando = new SqlCommand(SELECT, Conexion);
                SqlDataReader Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    aux = new eCiclo();

                    aux.Periodo = ID_Ciclo;
                    aux.Promedio = Convert.ToSingle(Reader["Promedio"]);
                    aux.Numero_Cursos = (int)Reader["Cantidad_Cursos"];
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
        }
    }
}
