using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PapeleriaMerida.Models;
using System.Data;

namespace PapeleriaMerida.DAL
{
    public class MarcaDAL
    {
        ConexionDAL oConexionDAL;
        public MarcaDAL() { oConexionDAL = new ConexionDAL(); }

        public int Agregar(string nombre, string descripcion,string url)
        {
            string query = "INSERT INTO [padmin].[Marca]([nomMarca],[descMarca],[urlMarca],[statusmarca]) VALUES('"+nombre+"','"+descripcion+"','"+url+"',1)";
            return oConexionDAL.EjecutarSQL(query);
        }
        public int ModificarConURL(string nombre, string descripcion, string url,int id)
        {
            string query = "UPDATE [padmin].[Marca] SET [nomMarca] ='"+nombre+"',[descMarca] = '"+descripcion+"',[urlMarca] ='"+url+"' WHERE idMarca="+id+"";
            return oConexionDAL.EjecutarSQL(query);
        }
        public int ModificarSinURL(string nombre, string descripcion, int id)
        {
            string query = "UPDATE [padmin].[Marca] SET [nomMarca] ='" + nombre + "',[descMarca] = '" + descripcion + "' WHERE idMarca=" + id + "";
            return oConexionDAL.EjecutarSQL(query);
        }

        public int Eliminar(int id)
        {
            string query = "UPDATE [padmin].[Marca] SET  [statusmarca] = 0 WHERE idMarca="+id+"";
            return oConexionDAL.EjecutarSQL(query);
        }

        public DataTable Mostrar()
        {
            return oConexionDAL.TablaConnsulta("select * from Marca where statusmarca=1");
        }

        public MarcaModel ObtenerMarcaSeleccionada(int id)
        {
            var Marca = new MarcaModel();
            String StrBuscar = string.Format("select * from Marca where idMarca=" + id + "");
            DataTable Datos = oConexionDAL.TablaConnsulta(StrBuscar);
            DataRow row = Datos.Rows[0];
            Marca.idMarca = Convert.ToInt32(row["idMarca"]);
            Marca.nomMarca = row["nomMarca"].ToString();
            Marca.descMarca = row["descMarca"].ToString();
            Marca.urlMarca = row["urlMarca"].ToString();
            Marca.statusmarca = Convert.ToInt32(row["statusmarca"]);
            return Marca;
        }
    }
}