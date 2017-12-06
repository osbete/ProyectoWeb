using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;
using MySql.Data.MySqlClient;

//Importal el paquete que contiene la clase DataTable
using System.Data;

namespace Datos
{
    public class CategoriaDao
    {
        public List<Categoria> MostrarTodo()
        {
            List<Categoria> lista = new List<Categoria>();

            try
            {
                String sentencia = "SELECT * FROM Categories ORDER BY CategoryName";
                Conexion con = new Conexion();
                DataTable dtCategorias = con.ejecutarConsulta(sentencia);
                Categoria objCategoria = null;

                //Recorrer las filas obtenidas por la consulta
                //para llenar la lista de Categorias
                foreach (DataRow fila in dtCategorias.Rows)
                {
                    objCategoria = new Categoria(
                        int.Parse(fila["CategoryId"].ToString()),
                        fila["CategoryName"].ToString(),
                        fila["Description"].ToString()
                        
                    );

                    lista.Add(objCategoria);

                }
                return lista;
            }
            catch (Exception ex)
            {
                return lista;
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

        public Categoria MostrarUno(int IdCategoria)
        {
            Categoria objCategoria = null;

            try
            {
                String sentencia = "SELECT * FROM Categories WHERE CategoryID = " + IdCategoria;
                Conexion con = new Conexion();
                DataTable dtCategorias = con.ejecutarConsulta(sentencia);
                
                //Verificar si la consulta regresó resultados
                // para llenar el objeto
                if (dtCategorias!=null && dtCategorias.Rows.Count>0)
                {
                    //Se obtiene la fila de la categoria buscada
                    DataRow fila = dtCategorias.Rows[0];

                    objCategoria = new Categoria(
                        int.Parse(fila["CategoryId"].ToString()),
                        fila["CategoryName"].ToString(),
                        fila["Description"].ToString()
                    );
                }
                return objCategoria;
            }
            catch (Exception ex)
            {
                return objCategoria;
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
       
        /// <summary>
        /// Inserta el Categoria recibido como parámetro en la BD
        /// </summary>
        /// <param name="Categoria">Objeto con los datos del Categoria
        /// a guardar</param>
        /// <returns>Regresa un valor positivo con la clave del 
        /// Categoria que se almacenó, en caso de error regresa 0</returns>
        public int Agregar(Categoria categoria)
        {
            try
            {
                //Arma la sentencia Insert y una consulta para obtener el último id generado
                String sentencia = String.Format("INSERT INTO categories (CategoryName,Description) VALUES({0},{1}); SELECT DISTINCT LAST_INSERT_ID() FROM Categories;",
                    categoria.NombreCategoria,
                    categoria.Descripcion);
                
                Conexion con = new Conexion();

                return con.ejecutarSentencia(sentencia, true);
            }
            catch (Exception ex)
            {
                return 0;
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

        public bool Eliminar(int IdCategoria)
        {
         
            try
            {
                String sentencia = "DELETE FROM Categories WHERE CategoryID = " + IdCategoria;
                Conexion con = new Conexion();
                
                return bool.Parse(con.ejecutarSentencia(sentencia,false).ToString());
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

        public bool Editar(Categoria categoria)
        {
            try
            {
                String sentencia = String.Format("UPDATE SET CategoryName = @, Description = {1} WHERE CategoryID = {2}", 
                    categoria.NombreCategoria,
                    categoria.Descripcion, 
                    categoria.IdCategoria);
                
                Conexion con = new Conexion();

                return bool.Parse(con.ejecutarSentencia(sentencia, false).ToString());
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


        public Empleado login(String usuario, String contra)
        {
            Empleado objEmpleado = null;

            try
            {
                //Se parametriza la sentencia insertando en la cadena de la
                //consulta los parámetros antecedidos por una @, cuando
                //se colocan los parámetros no es necesario colocar las
                //comillas para los campos de cadena
                String sentencia = "SELECT * FROM Employees " +
                    " WHERE FirstName = @usuario AND " +
                    "LastName = @password";

                MySqlCommand comando = new MySqlCommand(sentencia);

                //Se carga cada valos a la colección de parámetros del comando

                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@password", contra);
                
                Conexion con = new Conexion();

                //En lugar de la sentencia en cadena se envía el comando
                DataTable dtEmpleados = con.ejecutarConsulta(comando);

                //Verificar si la consulta regresó resultados
                // para llenar el objeto
                if (dtEmpleados != null && dtEmpleados.Rows.Count > 0)
                {
                    //Se obtiene la fila de la Empleado buscada
                    DataRow fila = dtEmpleados.Rows[0];

                    objEmpleado = new Empleado();
                    objEmpleado.Id = int.Parse(fila["EmployeeId"].ToString());
                    objEmpleado.Nombre = fila["FirstName"].ToString();
                    objEmpleado.Apellido = fila["LastName"].ToString();
                    objEmpleado.Puesto = fila["title"].ToString();
                    
                }
                return objEmpleado;
            }
            catch (Exception ex)
            {
                return objEmpleado;
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
