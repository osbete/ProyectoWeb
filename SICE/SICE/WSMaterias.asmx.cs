using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using Datos;
using Modelo;

namespace SICE
{
    /// <summary>
    /// Descripción breve de WSLogin
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WSMateria : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod]
        public string getMateria(int id)
        {
            Materia m = new MateriaDao().MostrarUno(id);
            JavaScriptSerializer serializador = new JavaScriptSerializer();

            return serializador.Serialize(m);
        }

        [WebMethod]
        public string getAllMaterias()
        {
            List<Materia> lista = new MateriaDao().MostrarTodo();
            JavaScriptSerializer serializador = new JavaScriptSerializer();

            return serializador.Serialize(lista);
        }
    }
}
