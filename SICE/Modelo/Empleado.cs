using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class  Empleado
    {
               
            public int Id { get; set; }
            public String Nombre { get; set; }
            public String Apellido { get; set; }
            public String Puesto { get; set; }

            public Empleado() { }

            public Empleado(int Id, String Nombre, String Apellido, String Puesto)
            {
                this.Id = Id;
                this.Nombre = Nombre;
                this.Apellido = Apellido;
                this.Puesto = Puesto;

            }
        }
    }
