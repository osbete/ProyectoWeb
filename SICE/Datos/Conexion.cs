using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Datos
{
    public class Conexion
    {
        public static MySqlConnection conexion = null;

        public bool Conectar() {
            String servidor = Datos.Properties.Resources.servidor;
            String usuario = Datos.Properties.Resources.usuario;
            String contrasenia = Datos.Properties.Resources.contrasenia;
            String puerto = Datos.Properties.Resources.puerto;

            String parametrosConexion = "Server="+servidor+";Uid="+usuario+";Pwd="+contrasenia+ ";Database=bd_sice_pw;Port=" + puerto+";";
            conexion = new MySqlConnection(parametrosConexion);
            //Intentamos conectarnos
            try
            {
                conexion.Open();
                return true;
            }
            catch (Exception ex) {
                //throw ex;
                return false;
            }

        }

        public DataTable ejecutarConsulta(MySqlCommand com) {
            try { 
                if (Conectar())
                {
                    com.Connection = conexion;

                    MySqlDataAdapter objAdapter =
                        new MySqlDataAdapter(com);
                                        
                    DataTable resultado=new DataTable();
                    //LLenar el objeto con el resultado de la consulta
                    objAdapter.Fill(resultado);
                    return resultado;
                }
                else {
                    return null;
                }
            }
            catch (Exception ex) {
                return null;
            }
            finally {
                //Solo intentar cerrar la conexión cuando si se encuentra abierta
                if (conexion!=null)
                    conexion.Close();   
            }

        }

        public DataTable ejecutarConsulta(String sentencia)
        {
            try
            {
                if (Conectar())
                {
                    
                    MySqlDataAdapter objAdapter =
                        new MySqlDataAdapter(sentencia,conexion);


                    DataTable resultado = new DataTable();
                    //LLenar el objeto con el resultado de la consulta
                    objAdapter.Fill(resultado);
                    return resultado;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                //Solo intentar cerrar la conexión cuando si se encuentra abierta
                if (conexion != null)
                    conexion.Close();
            }

        }

        /// <summary>
        /// Ejecuta una sentencia en la base de datos
        /// </summary>
        /// <param name="sentencia"></param>
        /// <param name="esInsert"></param>
        /// <returns></returns>
        public int ejecutarSentencia(String sentencia, bool esInsert)
        {
            int resultado = 0;
            try
            {
                if (Conectar())
                {
                    MySqlCommand objComando =
                        new MySqlCommand(sentencia, conexion);


                    if (esInsert)
                    {
                        //Obtiene el último id ingresado por el insert
                        resultado = int.Parse(objComando.ExecuteScalar().ToString());

                    }
                    else
                    {
                        objComando.ExecuteNonQuery();
                        resultado = 1;
                    }


                    return resultado;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                //Solo intentar cerrar la conexión cuando si se encuentra abierta
                if (conexion != null)
                    conexion.Close();
            }
        }
    }
}
