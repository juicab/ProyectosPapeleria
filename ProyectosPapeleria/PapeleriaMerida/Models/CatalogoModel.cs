using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PapeleriaMerida.Models
{
    public class CatalogoModel
    {
        public int idCatalogo { get; set; }
        public  string nombreCat { get; set; }
        public string docuCat { get; set; }
        public string fechaCat { get; set; }
        public int statusCat { get; set; }


    }
}