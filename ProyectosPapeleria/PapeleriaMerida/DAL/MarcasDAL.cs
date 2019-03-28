using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PapeleriaMerida.Models;
using System.Data;

namespace PapeleriaMerida.DAL
{
    public class MarcasDAL
    {
        ConexionDAL oConexionDAL;
        public MarcasDAL() { oConexionDAL = new ConexionDAL(); }

        public int Agregar(string nombre, string descripcion, string slogan,string logo,string banner)
        {
            string query = "INSERT INTO [padmin].[Marcas]([nomMarca],[descMarca],[sloganMarca],[logoMarca],[bannerMarca],[statusMarca]) VALUES ('"+nombre+"','"+descripcion+"','"+slogan+"','"+logo+"','"+banner+"',1)";
            return oConexionDAL.EjecutarSQL(query);
        }

        public DataTable Mostrar()
        {
            return oConexionDAL.TablaConnsulta("select * from Marcas where statusMarca=1");
        }

        public DataTable MostrarParaMaster()
        {
            return oConexionDAL.TablaConnsulta("select idMarca,nomMarca from Marcas where statusMarca=1");
        }

        public MarcasModel ObtenerMarcaSeleccionada(int id)
        {
            var Marca = new MarcasModel();
            String StrBuscar = string.Format("select * from Marcas where idMarca="+id+"");
            DataTable Datos = oConexionDAL.TablaConnsulta(StrBuscar);
            DataRow row = Datos.Rows[0];
            Marca.idMarca = Convert.ToInt32(row["idMarca"]);
            Marca.nomMarca = row["nomMarca"].ToString();
            Marca.descMarca = row["descMarca"].ToString();
            Marca.sloganMarca = row["sloganMarca"].ToString();
            Marca.logoMarca = row["logoMarca"].ToString();
            Marca.bannerMarca = row["bannerMarca"].ToString();
            Marca.statusMarca = Convert.ToInt32(row["statusMarca"]);
            return Marca;
        }

        public int ModificarCompleto(string nombre, string descripcion, string slogan, string logo, string banner,int id)
        {
            string query = "UPDATE [padmin].[Marcas] SET [nomMarca] = '"+nombre+"' ,[descMarca] = '"+descripcion+"' ,[sloganMarca] = '"+slogan+"' ,[logoMarca] = '"+logo+"' ,[bannerMarca] = '"+banner+"' WHERE idMarca="+id+"";
            return oConexionDAL.EjecutarSQL(query);
        }
        public int ModificarSoloDatos(string nombre,string descripcion,string slogan,int id)
        {
            string query = "UPDATE [padmin].[Marcas] SET [nomMarca] = '" + nombre + "' ,[descMarca] = '" + descripcion + "' ,[sloganMarca] = '" + slogan + "' WHERE idMarca=" + id + "";
            return oConexionDAL.EjecutarSQL(query);
        }

        public int ModificarDatosConLogo(string nombre, string descripcion, string slogan, string logo, int id)
        {
            string query = "UPDATE [padmin].[Marcas] SET [nomMarca] = '" + nombre + "' ,[descMarca] = '" + descripcion + "' ,[sloganMarca] = '" + slogan + "' ,[logoMarca] = '" + logo + "' WHERE idMarca=" + id + "";
            return oConexionDAL.EjecutarSQL(query);
        }

        public int ModificarDatosConBanner(string nombre, string descripcion, string slogan, string banner, int id)
        {
            string query = "UPDATE [padmin].[Marcas] SET [nomMarca] = '" + nombre + "' ,[descMarca] = '" + descripcion + "' ,[sloganMarca] = '" + slogan + "' ,[bannerMarca] = '" + banner + "' WHERE idMarca=" + id + "";
            return oConexionDAL.EjecutarSQL(query);
        }

        public int Eliminar(int id)
        {
            string query = "UPDATE [padmin].[Marcas] SET [statusMarca] = 0 WHERE idMarca="+id;
            return oConexionDAL.EjecutarSQL(query);
        }


    }
}