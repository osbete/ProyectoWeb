using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;
using MySql.Data.MySqlClient;

using System.Data;

namespace Datos
{
    public class UsuarioDao
    {    
        public bool Login(String usuario, String contra)
        {
            try
            {
                String sentencia = "SELECT * FROM usuarios " +
                    " WHERE usuario = @usuario AND " +
                    "password = @contra";

                MySqlCommand comando = new MySqlCommand(sentencia);
                
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@contra", contra);

                Conexion con = new Conexion();
                
                DataTable dtUsuarios = con.ejecutarConsulta(comando);
                
                if(dtUsuarios.Rows.Count==0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                //Solo intentar cerrar la conexión cuando si se encuentra abierta
                if (Conexion.conexion != null)
                {
                    Conexion.conexion.Close();
                }
            }
        }

        public string nomUsuario(String usu)
        {
            String nombre=null;
            try
            {
                String sentencia = "SELECT nombreCompleto FROM usuarios WHERE usuario = @usuario";

                MySqlCommand comando = new MySqlCommand(sentencia);

                comando.Parameters.AddWithValue("@usuario", usu);

                Conexion con = new Conexion();

                DataTable dtUsuarios = con.ejecutarConsulta(comando);

                if (dtUsuarios.Rows.Count == 0)
                {
                    
                    return nombre;
                }
                else
                {
                    DataRow fila = dtUsuarios.Rows[0];
                    nombre = fila["nombreCompleto"].ToString();
                    return nombre;
                }
            }
            catch (Exception ex)
            {
                return nombre;
            }
            finally
            {
                //Solo intentar cerrar la conexión cuando si se encuentra abierta
                if (Conexion.conexion != null)
                {
                    Conexion.conexion.Close();
                }
            }    
        }                
    }
}
