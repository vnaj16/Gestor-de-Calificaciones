using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

namespace Datos
{
    public class DataBase
    {
        //private static string Cadena_Conexion { get; set; }

        public DataBase()
        {
            //Cadena_Conexion = string.Format(@"Server = tcp:ARTHUR\VNAJ_DB01,49156; DataBase = dbControlNotas; User Id = u_dbControlNotas; Password = 123456");
        }

        /*static string GetDataSource()
        {
            FileStream Archivo_Test = new FileStream("DataSource.txt", FileMode.Open);//Se guarda en bin/debug//
            Archivo_Test.Seek(0, SeekOrigin.Begin); // Ubico el posicionador en el inicio para leer desde ahi
            byte[] Buffer = new byte[(int)Archivo_Test.Length]; // Creo el arreglo de bytes donde guardare lo que saque del archivo
            Archivo_Test.Read(Buffer, 0, (int)Archivo_Test.Length);

            Archivo_Test.Close();

            return ASCIIEncoding.ASCII.GetString(Buffer); //Transoformo los bytes a strings
        }*/

        private SqlConnection Conexion { get; set; }

        public SqlConnection Conectar()
        {
            try
            {
                Conexion = new SqlConnection(GetConnectionString());

                Conexion.Open();

                return Conexion;
            }
            catch(Exception e) { return null; }
        }

        public void Desconectar() { Conexion.Close(); }

        public static string GetConnectionString(string ID = "Default")
        {
            return ConfigurationManager.ConnectionStrings[ID].ConnectionString;//Retorno la cadena de conexion que hay en el App.config
        }
    }
}
