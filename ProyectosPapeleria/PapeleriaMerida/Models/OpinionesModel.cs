using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PapeleriaMerida.Models
{
    public class OpinionesModel
    {
        public int idOpinion { get; set;}
        public string titulo { get; set;}
        public string opinionCliente { get; set; }
        public string imagenCliente { get; set; }
        public int statusOpinion { get; set; }
    }
}