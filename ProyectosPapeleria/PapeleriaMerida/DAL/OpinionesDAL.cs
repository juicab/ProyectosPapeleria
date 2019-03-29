using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PapeleriaMerida.Models;
using System.Data;

namespace PapeleriaMerida.DAL
{
    public class OpinionesDAL
    {
        ConexionDAL oConexionDAL;
        public OpinionesDAL() { oConexionDAL = new ConexionDAL(); }
        public int Agregar(string opinionCliente,string imagenCliente)
        {
            string query = "INSERT INTO [padmin].[Opiniones]([titulo],[opinionCliente],[imagenCliente],[statusOpinion]) VALUES('Cliente','"+opinionCliente+"','"+imagenCliente+"',1)";
            return oConexionDAL.EjecutarSQL(query);
        }

        public DataTable Mostrar()
        {
            return oConexionDAL.TablaConnsulta("select * from Opiniones where statusOpinion=1");
        }




    }
}