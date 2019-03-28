using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PapeleriaMerida.Models
{
    public class ProductoModel
    {
        public int idProd { get; set; }
        public string nombreProd { get; set; }
        public string desProd { get; set;}
        public int idMarca1 { get; set; }
        public string nomMarca { get; set; }
        public string imagenProd { get; set; }
        public int statusProd { get; set; }
        public List<MarcasModel> ListaMarcas { get; set; }


    }
}