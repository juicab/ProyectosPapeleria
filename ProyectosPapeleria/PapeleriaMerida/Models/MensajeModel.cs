using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PapeleriaMerida.Models
{
    public class MensajeModel
    {
        public int idMensaje { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string asunto { get; set; }
        public string telefono { get; set; }
        public string mensaje { get; set; }
        public string pagina { get; set; }
        public int statusMen { get; set; }

        public int Mensajes { get; set; }


    }
}