using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using PapeleriaMerida.Models;

namespace PapeleriaMerida.DAL
{
    public class EmpresaDAL
    {
        ConexionDAL oConexionDAL;
        public EmpresaDAL() { oConexionDAL = new ConexionDAL(); }

        public EmpresaModel Obtener_Empresa()
        {
            var Empresa = new EmpresaModel();
            String StrBuscar = string.Format("select * from Empresa");
            DataTable Datos = oConexionDAL.TablaConnsulta(StrBuscar);
            DataRow row = Datos.Rows[0];
            Empresa.idEmpresa = Convert.ToInt32(row["idEmpresa"]);
            Empresa.nombre = row["nombre"].ToString();
            Empresa.descripcion = row["descripcion"].ToString();
            Empresa.direccion = row["direccion"].ToString();
            Empresa.telefono = row["telefono"].ToString();
            Empresa.correo = row["correo"].ToString();
            Empresa.sitio = row["sitio"].ToString();
            Empresa.mision = row["mision"].ToString();
            Empresa.vision = row["vision"].ToString();
            Empresa.valores = row["valores"].ToString();
            Empresa.statusEmpresa = Convert.ToInt32(row["statusEmpresa"]);
            return Empresa;
        }

        public int Modificar(string telefono, string direccion, string correo, string sitio, string descripcion, string mision, string vision, string valores)
        {
            string query = "UPDATE [dbo].[Empresa]SET [descripcion] = '" + descripcion + "',[direccion] = '" + direccion + "',[telefono] = '" + telefono + "',[correo] = '" + correo + "',[sitio] = '" + sitio + "',[mision] = '" + mision + "',[vision] = '" + vision + "',[valores] = '"+valores+"' WHERE idEmpresa=1";
            return oConexionDAL.EjecutarSQL(query);
        }


    }
}