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

        public OpinionesModel ObtenerOpinionSeleccionada(int id)
        {
            var Opinion = new OpinionesModel();
            String StrBuscar = string.Format("select * from Opiniones where statusOpinion=1 and idOpinion="+id);
            DataTable Datos = oConexionDAL.TablaConnsulta(StrBuscar);
            DataRow row = Datos.Rows[0];
            Opinion.idOpinion = Convert.ToInt32(row["idOpinion"]);
            Opinion.titulo = row["titulo"].ToString();
            Opinion.opinionCliente = row["opinionCliente"].ToString();
            Opinion.imagenCliente = row["imagenCliente"].ToString();
            Opinion.statusOpinion = Convert.ToInt32(row["statusOpinion"]);
            return Opinion;
        }

        public int CambiosConImagen(string opinion,string imagen,int id)
        {
            string query = "UPDATE [padmin].[Opiniones] SET[opinionCliente] = '"+opinion+"' , [imagenCliente] = '"+imagen+"' WHERE idOpinion="+id;
            return oConexionDAL.EjecutarSQL(query);
        }

        public int CambiosSinImagen(string opinion, int id)
        {
            string query = "UPDATE [padmin].[Opiniones] SET[opinionCliente] = '" + opinion + "' WHERE idOpinion=" + id;
            return oConexionDAL.EjecutarSQL(query);
        }

        public int Eliminar(int id)
        {
            string query = "UPDATE [padmin].[Opiniones] SET [statusOpinion] = 0 WHERE idOpinion=" + id;
            return oConexionDAL.EjecutarSQL(query);
        }


    }
}