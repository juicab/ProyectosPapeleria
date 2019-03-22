using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PapeleriaMerida.DAL
{
    public class SubirArchivoDAL
    {
        public string confirmacion { get; set; }
        public string error { get; set; }

        public void SubirArchivo(string ruta,HttpPostedFileBase file)
        {
            try
            {
                file.SaveAs(ruta);
                this.confirmacion = "guardado";
            }
            catch(Exception)
            {
                this.error = "error";
            }
        }



    }
}