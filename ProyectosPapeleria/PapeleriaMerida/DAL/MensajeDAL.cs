using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PapeleriaMerida.Models;
using System.Data;

namespace PapeleriaMerida.DAL
{
    public class MensajeDAL
    {
        ConexionDAL oConexionDAL;
        public MensajeDAL() { oConexionDAL = new ConexionDAL(); }
        // INSERT INTO [dbo].[Mensaje]([nombre],[correo],[asunto],[telefono],[pagina],[mensaje],[statusMen]) VALUES (nombre,correo,asunto,telefono,pagina,mensaje,statusMen)
        public int Agregar(string nombre,string correo,string asunto,string telefono,string mensaje)
        {
            string query = "INSERT INTO [dbo].[Mensaje]([nombre],[correo],[asunto],[telefono],[pagina],[mensaje],[statusMen]) VALUES ('"+nombre+"','"+correo+"','"+asunto+"','"+telefono+"','Papeleria Merida','"+mensaje+"',1)";
            return oConexionDAL.EjecutarSQL(query);
        }

        public DataTable Mostrar()
        {
            return oConexionDAL.TablaConnsulta("select * from Mensaje where statusMen = 1");
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