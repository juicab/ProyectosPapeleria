using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using PapelesdeRegalo.Models;

namespace PapelesdeRegalo.DAL
{
    public class MensajeDAL
    {
        ConexionDAL oConexionDAL;
        public MensajeDAL() { oConexionDAL = new ConexionDAL(); }
        // INSERT INTO [dbo].[Mensaje]([nombre],[correo],[asunto],[telefono],[pagina],[mensaje],[statusMen]) VALUES (nombre,correo,asunto,telefono,pagina,mensaje,statusMen)
        public int Agregar(string nombre, string correo, string asunto, string telefono, string mensaje)
        {
            string query = "INSERT INTO [dbo].[Mensaje]([nombre],[correo],[asunto],[telefono],[pagina],[mensaje],[statusMen]) VALUES ('" + nombre + "','" + correo + "','" + asunto + "','" + telefono + "','Papeles de Regalo','" + mensaje + "',1)";
            return oConexionDAL.EjecutarSQL(query);
        }

        public DataTable ObtenerCatalogos()
        {
            return oConexionDAL.TablaConnsulta("select * from Catalogo where idCatalogo=3 or idCatalogo =4");
        }

        public DataTable Mostrar()
        {
            return oConexionDAL.TablaConnsulta("select * from Mensaje  where pagina ='Papeleria Merida' and statusMen = 1");
        }

        public MensajeModel ObtenerMensajeSeleccionado(int id)
        {
            var Mensaje = new MensajeModel();
            String StrBuscar = string.Format("select * from Mensaje where idMensaje=" + id);
            DataTable Datos = oConexionDAL.TablaConnsulta(StrBuscar);
            DataRow row = Datos.Rows[0];
            Mensaje.idMensaje = Convert.ToInt32(row["idMensaje"]);
            Mensaje.nombre = row["nombre"].ToString();
            Mensaje.correo = row["correo"].ToString();
            Mensaje.asunto = row["asunto"].ToString();
            Mensaje.telefono = row["telefono"].ToString();
            Mensaje.pagina = row["pagina"].ToString();
            Mensaje.mensaje = row["mensaje"].ToString();
            Mensaje.statusMen = Convert.ToInt32(row["statusMen"]);
            return Mensaje;
        }



    }
}