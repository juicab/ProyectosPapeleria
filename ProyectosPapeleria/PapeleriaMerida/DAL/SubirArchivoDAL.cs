using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Threading;

namespace PapeleriaMerida.DAL
{
    public class SubirArchivoDAL
    {
        public string confirmacion { get; set; }
        public string error { get; set; }
        string server = "ftp://107.180.0.7";
        string user = "ph17069950603";
        string pass = "Cs18191819";
        string rutadestino = "/papeleriamerida.com.mx/admin/docs";

        public void SubirArchivo(string ruta,HttpPostedFileBase file)
        {
            try
            {
                file.SaveAs(ruta);
                this.confirmacion = "guardado";
            }
            catch(Exception)
            {
                this.error = "error";
            }
        }

        public void Upload(string RutaArchivoCompu, string nombredestino)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(server + rutadestino + "/" +nombredestino);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(user, pass);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = true;
                FileStream stream = File.OpenRead(RutaArchivoCompu);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(buffer, 0, buffer.Length);
                reqStream.Flush();
                reqStream.Close();
                this.confirmacion = "guardado";
            }
            catch (Exception )
            {
                this.error = "error";
            }
        }

    }
}