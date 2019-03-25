using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PapeleriaMerida.Models;
using System.Data;

namespace PapeleriaMerida.DAL
{
    public class CatalogosDAL
    {
        ConexionDAL oConexionDAL;
        public CatalogosDAL() { oConexionDAL = new ConexionDAL(); }

        public DataTable Mostrar()
        {
            return oConexionDAL.TablaConnsulta("select idCatalogo,nombreCat,docuCat,FORMAT(fechaCat, 'dd/MMMMM/yyyy', 'ES-ES' ) as 'fechaCat',statusCat from Catalogo where statusCat=1");
        }


        public CatalogoModel ObtenerCatPapeleria()
        {
            var CatPapeleria = new CatalogoModel();
            String StrBuscar = string.Format("select * from Catalogo where idCatalogo=1");
            DataTable Datos = oConexionDAL.TablaConnsulta(StrBuscar);
            DataRow row = Datos.Rows[0];
            CatPapeleria.idCatalogo = Convert.ToInt32(row["idCatalogo"]);
            CatPapeleria.nombreCat = row["nombreCat"].ToString();
            CatPapeleria.docuCat = row["docuCat"].ToString();
            DateTime f = Convert.ToDateTime(row["fechaCat"]);
            string dia = f.Day.ToString();
            string mes = MesEscrito( f.Month.ToString());
            string an = f.Year.ToString();
            string fet = dia + "-" + mes + "-" + an;
            CatPapeleria.fechaCat = fet.ToString();
            CatPapeleria.statusCat = Convert.ToInt32(row["statusCat"]);
            return CatPapeleria;
        }

        public CatalogoModel ObtenerCatNovedades()
        {
            var CatPapeleria = new CatalogoModel();
            String StrBuscar = string.Format("select * from Catalogo where idCatalogo=2");
            DataTable Datos = oConexionDAL.TablaConnsulta(StrBuscar);
            DataRow row = Datos.Rows[0];
            CatPapeleria.idCatalogo = Convert.ToInt32(row["idCatalogo"]);
            CatPapeleria.nombreCat = row["nombreCat"].ToString();
            CatPapeleria.docuCat = row["docuCat"].ToString();
            DateTime f = Convert.ToDateTime(row["fechaCat"]);
            string dia = f.Day.ToString();
            string mes = MesEscrito(f.Month.ToString());
            string an = f.Year.ToString();
            string fet = dia + "-" + mes + "-" + an;
            CatPapeleria.fechaCat = fet.ToString();
            CatPapeleria.statusCat = Convert.ToInt32(row["statusCat"]);
            return CatPapeleria;
        }
        public CatalogoModel ObtenerCatBolsas()
        {
            var CatPapeleria = new CatalogoModel();
            String StrBuscar = string.Format("select * from Catalogo where idCatalogo=3");
            DataTable Datos = oConexionDAL.TablaConnsulta(StrBuscar);
            DataRow row = Datos.Rows[0];
            CatPapeleria.idCatalogo = Convert.ToInt32(row["idCatalogo"]);
            CatPapeleria.nombreCat = row["nombreCat"].ToString();
            CatPapeleria.docuCat = row["docuCat"].ToString();
            DateTime f = Convert.ToDateTime(row["fechaCat"]);
            string dia = f.Day.ToString();
            string mes = MesEscrito(f.Month.ToString());
            string an = f.Year.ToString();
            string fet = dia + "-" + mes + "-" + an;
            CatPapeleria.fechaCat = fet.ToString();
            CatPapeleria.statusCat = Convert.ToInt32(row["statusCat"]);
            return CatPapeleria;
        }
        public CatalogoModel ObtenerCatPapeles()
        {
            var CatPapeleria = new CatalogoModel();
            String StrBuscar = string.Format("select * from Catalogo where idCatalogo=4");
            DataTable Datos = oConexionDAL.TablaConnsulta(StrBuscar);
            DataRow row = Datos.Rows[0];
            CatPapeleria.idCatalogo = Convert.ToInt32(row["idCatalogo"]);
            CatPapeleria.nombreCat = row["nombreCat"].ToString();
            CatPapeleria.docuCat = row["docuCat"].ToString();
            DateTime f = Convert.ToDateTime(row["fechaCat"]);
            string dia = f.Day.ToString();
            string mes = MesEscrito(f.Month.ToString());
            string an = f.Year.ToString();
            string fet = dia + "-" + mes + "-" + an;
            CatPapeleria.fechaCat = fet.ToString();
            CatPapeleria.statusCat = Convert.ToInt32(row["statusCat"]);
            return CatPapeleria;
        }
        public CatalogoModel ObtenerCatStickers()
        {
            var CatPapeleria = new CatalogoModel();
            String StrBuscar = string.Format("select * from Catalogo where idCatalogo=5");
            DataTable Datos = oConexionDAL.TablaConnsulta(StrBuscar);
            DataRow row = Datos.Rows[0];
            CatPapeleria.idCatalogo = Convert.ToInt32(row["idCatalogo"]);
            CatPapeleria.nombreCat = row["nombreCat"].ToString();
            CatPapeleria.docuCat = row["docuCat"].ToString();
            DateTime f = Convert.ToDateTime(row["fechaCat"]);
            string dia = f.Day.ToString();
            string mes = MesEscrito(f.Month.ToString());
            string an = f.Year.ToString();
            string fet = dia + "-" + mes + "-" + an;
            CatPapeleria.fechaCat = fet.ToString();
            CatPapeleria.statusCat = Convert.ToInt32(row["statusCat"]);
            return CatPapeleria;
        }


        public int ModificarPapeleria(string url)
        {
            string fechas = DateTime.Now.Date.ToString();
            string query = "UPDATE [dbo].[Catalogo] SET [docuCat] = '"+url+"' ,[fechaCat] = '"+fechas+"' WHERE idCatalogo=1";
            return oConexionDAL.EjecutarSQL(query);
        }

        public int ModificarNovedades(string url)
        {
            string fechas = DateTime.Now.Date.ToString();
            string query = "UPDATE [dbo].[Catalogo] SET [docuCat] = '" + url + "' ,[fechaCat] = '" + fechas + "' WHERE idCatalogo=2";
            return oConexionDAL.EjecutarSQL(query);
        }
        public int ModificarBolsas(string url)
        {
            string fechas = DateTime.Now.Date.ToString();
            string query = "UPDATE [dbo].[Catalogo] SET [docuCat] = '" + url + "' ,[fechaCat] = '" + fechas + "' WHERE idCatalogo=3";
            return oConexionDAL.EjecutarSQL(query);
        }
        public int ModificarPapeles(string url)
        {
            string fechas = DateTime.Now.Date.ToString();
            string query = "UPDATE [dbo].[Catalogo] SET [docuCat] = '" + url + "' ,[fechaCat] = '" + fechas + "' WHERE idCatalogo=4";
            return oConexionDAL.EjecutarSQL(query);
        }
        public int ModificarStickers(string url)
        {
            string fechas = DateTime.Now.Date.ToString();
            string query = "UPDATE [dbo].[Catalogo] SET [docuCat] = '" + url + "' ,[fechaCat] = '" + fechas + "' WHERE idCatalogo=5";
            return oConexionDAL.EjecutarSQL(query);
        }


        public string MesEscrito(string num)
        {
            string dia;
            if(num=="1")
            {
                dia = "Enero";
                return dia;
            }
            else
            {
                if(num=="2")
                {
                    dia = "Febrero";
                    return dia;
                }
                else
                {
                    if(num=="3")
                    {
                        dia = "Marzo";
                        return dia;
                    }
                    else
                    {
                        if(num=="4")
                        {
                            dia = "Abril";
                            return dia;
                        }
                        else
                        {
                            if(num=="5")
                            {
                                dia = "Mayo";
                                return dia;
                            }
                            else
                            {
                                if(num=="6")
                                {
                                    dia = "Junio";
                                    return dia;
                                }
                                else
                                {
                                    if(num=="7")
                                    {
                                        dia = "Julio";
                                        return dia;
                                    }
                                    else
                                    {
                                        if(num=="8")
                                        {
                                            dia = "Agosto";
                                            return dia;
                                        }
                                        else
                                        {
                                            if(num=="9")
                                            {
                                                dia = "Septiembre";
                                                return dia;
                                            }
                                            else
                                            {
                                                if(num=="10")
                                                {
                                                    dia = "Octubre";
                                                    return dia;
                                                }
                                                else
                                                {
                                                    if(num=="11")
                                                    {
                                                        dia = "Noviembre";
                                                        return dia;
                                                    }
                                                    else
                                                    {
                                                        if(num=="12")
                                                        {
                                                            dia = "Diciembre";
                                                            return dia;
                                                        }
                                                        else
                                                        {
                                                            dia = "Error";
                                                            return dia;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

    }
}