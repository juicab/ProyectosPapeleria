using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PapeleriaMerida.Models;
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
            return oConexionDAL.TablaConnsulta("select * from Mensaje  where pagina ='Papeleria Merida' and statusMen = 1");
        }


    }
}