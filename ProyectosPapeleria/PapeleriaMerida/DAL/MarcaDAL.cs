using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PapeleriaMerida.Models;

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


    }
}