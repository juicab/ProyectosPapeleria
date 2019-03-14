using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PapeleriaMerida.Models
{
    public class EmpresaModel
    {
        public int idEmpresa { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string sitio { get; set; }
        public string mision { get; set; }
        public string vision { get; set; }
        public string valores { get; set; }
        public int statusEmpresa { get; set; }
    }
}