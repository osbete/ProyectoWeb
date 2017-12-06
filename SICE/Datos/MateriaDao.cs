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
    public class MateriaDao
    {
        public List<Materia> MostrarTodo()
        {
            List<Materia> lista = new List<Materia>();

            try
            {
                String sentencia = "SELECT * FROM materias ORDER BY id;";
                Conexion con = new Conexion();
                DataTable dtMaterias = con.ejecutarConsulta(sentencia);

                Materia objMateria = null;

                //Recorrer las filas obtenidas por la consulta
                //para llenar la lista de Categorias
                foreach (DataRow fila in dtMaterias.Rows)
                {
                    objMateria = new Materia(
                        int.Parse(fila["id"].ToString()),
                        fila["nombre"].ToString(),
                        fila["creditos"].ToString(),
                        int.Parse(fila["idCarrera"].ToString())

                    );
                    lista.Add(objMateria);
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

        public Materia MostrarUno(int IdMateria)
        {
            Materia objMateria = null;

            try
            {
                String sentencia = "SELECT * FROM materias WHERE id = " + IdMateria;
                Conexion con = new Conexion();
                DataTable dtMaterias = con.ejecutarConsulta(sentencia);

                //Verificar si la consulta regresó resultados
                // para llenar el objeto
                if (dtMaterias != null && dtMaterias.Rows.Count > 0)
                {
                    //Se obtiene la fila de la categoria buscada
                    DataRow fila = dtMaterias.Rows[0];

                    objMateria = new Materia(
                        int.Parse(fila["id"].ToString()),
                        fila["nombre"].ToString(),
                        fila["creditos"].ToString(),
                        int.Parse(fila["idCarrera"].ToString())
                    );
                }
                return objMateria;
            }
            catch (Exception ex)
            {
                return objMateria;
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
        public int Agregar(Materia materia)
        {
            try
            {
                //Arma la sentencia Insert y una consulta para obtener el último id generado
                String sentencia = String.Format("INSERT INTO materias (nombre,creditos,idCarrera) VALUES(" +
                    "SELECT DISTINCT LAST_INSERT_ID() FROM materias;",
                    materia.NombreMateria,
                    materia.Creditos,
                    materia.idCarrera);

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

        public bool Eliminar(int IdMateria)
        {
            try
            {
                String sentencia = "DELETE FROM materias WHERE id = " + IdMateria;
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

        public bool Editar(Materia materia)
        {
            try
            {
                String sentencia = String.Format("UPDATE SET nombre = @, creditos = {1}," +
                    " idCarrera = {3}  WHERE id = {2}",
                    materia.NombreMateria,
                    materia.Creditos,
                    materia.IdMateria,
                    materia.idCarrera);

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
    }
}
