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
    public class WSLogin : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod]
        public string getUsuario(String usuario, String contra)
        {
            String nombreCompleto = "";
            JavaScriptSerializer serializador = new JavaScriptSerializer();
            bool log = new UsuarioDao().Login(usuario,contra);

            if (log)
            {
                nombreCompleto = new UsuarioDao().nomUsuario(usuario);
                return serializador.Serialize(nombreCompleto);
            }
            else
            {
                return serializador.Serialize(log);
            }            
        }
    }
}
