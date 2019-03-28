using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PapeleriaMerida.Models;
using System.Data;

namespace PapeleriaMerida.DAL
{
    public class ProductoDAL
    {
        ConexionDAL oConexionDAL;
        public ProductoDAL() { oConexionDAL = new ConexionDAL(); }

        public int Agregar(string nombre,string descripcion,int idMarca,string imagen)
        {
            string query = "INSERT INTO [padmin].[Producto]([nombreProd],[desProd],[idMarca1],[imagenProd],[statusProd]) VALUES ('"+nombre+"','"+descripcion+"',"+idMarca+",'"+imagen+"',1)";
            return oConexionDAL.EjecutarSQL(query);
        }


        public List<MarcasModel>ListaMarcas()
        {
            string query = "select idMarca,nomMarca from Marcas where statusMarca=1";
            var result = oConexionDAL.TablaConnsulta(query);
            List<MarcasModel> listaMarcas = new List<MarcasModel>();
            foreach(DataRow marca in result.Rows)
            {
                var marcas = new MarcasModel();
                marcas.idMarca = int.Parse(marca[0].ToString());
                marcas.nomMarca = marca[1].ToString();
                listaMarcas.Add(marcas);

            }
            return listaMarcas;
        }

        public DataTable Mostrar()
        {
            return oConexionDAL.TablaConnsulta("select idProd,nombreProd,desProd,idMarca1,nomMarca,imagenProd,statusProd From Producto inner join Marcas on idMarca1=idMarca where statusProd=1");
        }

        public DataTable MostrarProductosCategoria(int id)
        {
            return oConexionDAL.TablaConnsulta("select * from Producto where statusProd=1 and idMarca1="+id);
        }

        public ProductoModel ObtenerProductoSeleccionado(int id)
        {
            var Producto = new ProductoModel();
            string StrBuscar = string.Format("select idProd,nombreProd,desProd,idMarca1,nomMarca,imagenProd,statusProd From Producto inner join Marcas on idMarca1=idMarca where idProd=" + id);
            DataTable Datos = oConexionDAL.TablaConnsulta(StrBuscar);
            DataRow row = Datos.Rows[0];
            Producto.idProd = Convert.ToInt32(row["idProd"]);
            Producto.nombreProd = row["nombreProd"].ToString();
            Producto.desProd = row["desProd"].ToString();
            Producto.idMarca1 = Convert.ToInt32(row["idMarca1"]);
            Producto.nomMarca = row["nomMarca"].ToString();
            Producto.imagenProd = row["imagenProd"].ToString();
            Producto.statusProd = Convert.ToInt32(row["statusProd"]);
            return Producto;
        }


        public int ModificarConImagen(string nombreProd,string desProd,int idMarca1,string imagenProd,int id)
        {
            string query = "UPDATE [padmin].[Producto]SET [nombreProd] = '"+nombreProd+"' ,[desProd] = '"+desProd+"' ,[idMarca1] = "+idMarca1+",[imagenProd] ='"+imagenProd+"' WHERE idProd="+id;
            return oConexionDAL.EjecutarSQL(query);
        }
        public int ModificarSinImagen(string nombreProd, string desProd, int idMarca1, int id)
        {
            string query = "UPDATE [padmin].[Producto]SET [nombreProd] = '" + nombreProd + "' ,[desProd] = '" + desProd + "' ,[idMarca1] = " + idMarca1 + " WHERE idProd=" + id;
            return oConexionDAL.EjecutarSQL(query);
        }

    }
}