using System;
using System.Web;
using System.Web.UI.WebControls;
using AMALIAFW;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AMALIA
{
    public partial class SUBE_ARCHIVOS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["tipo_doc"] != null)
            {
                if (Request.QueryString["tipo_doc"] == "1")
                {
                    // REV TECNICA
                    string id_camion = Request.QueryString["id_camion"];
                    string guid = Request.QueryString["guid"];

                    var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                    if (httpPostedFile != null)
                    {
                        DBUtil db = new DBUtil();
                        string ServerPath = HttpContext.Current.Server.MapPath("~").ToString();
                        string subPath = ServerPath + "/Documentos/Camiones/DOCS/" + id_camion;
                        bool exists = System.IO.Directory.Exists(subPath);

                        if (!exists)
                        {
                            System.IO.Directory.CreateDirectory(subPath);
                        }
                        var fileSavePath = Path.Combine(subPath, guid + "_" + httpPostedFile.FileName);
                        httpPostedFile.SaveAs(fileSavePath);

                    }
                }
                if (Request.QueryString["tipo_doc"] == "2")
                {
                    // MANTENCION
                    string id_camion = Request.QueryString["id_camion"];
                    string guid = Request.QueryString["guid"];

                    var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                    if (httpPostedFile != null)
                    {
                        DBUtil db = new DBUtil();
                        string ServerPath = HttpContext.Current.Server.MapPath("~").ToString();
                        string subPath = ServerPath + "/Documentos/Camiones/MANT/" + id_camion;
                        bool exists = System.IO.Directory.Exists(subPath);

                        if (!exists)
                        {
                            System.IO.Directory.CreateDirectory(subPath);
                        }
                        var fileSavePath = Path.Combine(subPath, guid + "_" + httpPostedFile.FileName);
                        httpPostedFile.SaveAs(fileSavePath);

                    }
                }
                if (Request.QueryString["tipo_doc"] == "3")
                {
                    // FACTURA COMPRA
                    string id_compra = Request.QueryString["id_compra"];
                    string guid = Request.QueryString["guid"];

                    var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                    if (httpPostedFile != null)
                    {
                        DBUtil db = new DBUtil();
                        string ServerPath = HttpContext.Current.Server.MapPath("~").ToString();
                        string subPath = ServerPath + "/Documentos/FacturaCompra/" + id_compra;
                        bool exists = System.IO.Directory.Exists(subPath);
                        if (!exists)
                        {
                            System.IO.Directory.CreateDirectory(subPath);
                        }
                        var fileSavePath = Path.Combine(subPath, guid + "_" + httpPostedFile.FileName);
                        httpPostedFile.SaveAs(fileSavePath);
                    }
                }

                if (Request.QueryString["tipo_doc"] == "4")
                {
                    // ORDENES DE COMPRA
                    string id_oc = Request.QueryString["id_oc"];
                    string guid = Request.QueryString["guid"];

                    var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                    if (httpPostedFile != null)
                    {
                        DBUtil db = new DBUtil();
                        string ServerPath = HttpContext.Current.Server.MapPath("~").ToString();
                        string subPath = ServerPath + "/Documentos/OC/" + id_oc;
                        bool exists = System.IO.Directory.Exists(subPath);
                        if (!exists)
                        {
                            System.IO.Directory.CreateDirectory(subPath);
                        }
                        var fileSavePath = Path.Combine(subPath, guid + "_" + httpPostedFile.FileName);

                        OBJ_OC_ADJUNTOS adjunto = new OBJ_OC_ADJUNTOS();
                        FN_OC_ADJUNTOS.PREPARAOBJETO(ref adjunto);
                        adjunto.nom_archivo = guid + "_" + httpPostedFile.FileName;
                        adjunto.nom_real = httpPostedFile.FileName;
                        adjunto.id_oc = int.Parse(id_oc);
                        FN_OC_ADJUNTOS.INSERT(ref adjunto);

                        httpPostedFile.SaveAs(fileSavePath);

                    }
                }

                if (Request.QueryString["tipo_doc"] == "5")
                {
                    // ORDENES DE COMPRA
                    string id_oc = Request.QueryString["id_oc"];
                    string guid = Request.QueryString["guid"];

                    var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                    if (httpPostedFile != null)
                    {
                        DBUtil db = new DBUtil();
                        string ServerPath = HttpContext.Current.Server.MapPath("~").ToString();
                        string subPath = ServerPath + "/Documentos/OCPV/" + id_oc;
                        bool exists = System.IO.Directory.Exists(subPath);
                        if (!exists)
                        {
                            System.IO.Directory.CreateDirectory(subPath);
                        }
                        var fileSavePath = Path.Combine(subPath, guid + "_" + httpPostedFile.FileName);

                        OBJ_OCPV_ADJUNTOS adjunto = new OBJ_OCPV_ADJUNTOS();
                        FN_OCPV_ADJUNTOS.PREPARAOBJETO(ref adjunto);
                        adjunto.nom_archivo = guid + "_" + httpPostedFile.FileName;
                        adjunto.nom_real = httpPostedFile.FileName;
                        adjunto.id_oc = int.Parse(id_oc);
                        FN_OCPV_ADJUNTOS.INSERT(ref adjunto);

                        httpPostedFile.SaveAs(fileSavePath);

                    }
                }

                if (Request.QueryString["tipo_doc"] == "6")
                {
                    // HALLAZGO
                    string id_hallazgo = Request.QueryString["id_hallazgo"];
                    string guid = Request.QueryString["guid"];

                    var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                    if (httpPostedFile != null)
                    {
                        DBUtil db = new DBUtil();
                        string ServerPath = HttpContext.Current.Server.MapPath("~").ToString();
                        string subPath = ServerPath + "/Documentos/Hallazgos/" + id_hallazgo;
                        bool exists = System.IO.Directory.Exists(subPath);
                        if (!exists)
                        {
                            System.IO.Directory.CreateDirectory(subPath);
                        }
                        var fileSavePath = Path.Combine(subPath, guid + "_" + httpPostedFile.FileName);

                        OBJ_HALLAZGOS_ADJUNTOS adjunto = new OBJ_HALLAZGOS_ADJUNTOS();
                        FN_HALLAZGOS_ADJUNTOS.PREPARAOBJETO(ref adjunto);
                        adjunto.nom_archivo = guid + "_" + httpPostedFile.FileName;
                        adjunto.nom_real = httpPostedFile.FileName;
                        adjunto.id_hallazgo = int.Parse(id_hallazgo);
                        FN_HALLAZGOS_ADJUNTOS.INSERT(ref adjunto);

                        httpPostedFile.SaveAs(fileSavePath);

                    }
                }

                if (Request.QueryString["tipo_doc"] == "7")
                {
                    //// FOTO CHECKLISTS
                    string id_checklist_completado = Request.QueryString["id_checklist_completado"];
                    string guid = Request.QueryString["guid"];

                    var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                    if (httpPostedFile != null)
                    {
                        DBUtil db = new DBUtil();
                        string ServerPath = HttpContext.Current.Server.MapPath("~").ToString();
                        string subPath = ServerPath + "/Checklist/" + id_checklist_completado;
                        bool exists = System.IO.Directory.Exists(subPath);
                        if (!exists)
                        {
                            System.IO.Directory.CreateDirectory(subPath);
                        }
                        var fileSavePath = Path.Combine(subPath, guid + "_" + httpPostedFile.FileName);

                        OBJ_CHECKLISTS_IMAGENES adjunto = new OBJ_CHECKLISTS_IMAGENES();
                        FN_CHECKLISTS_IMAGENES.PREPARAOBJETO(ref adjunto);
                        adjunto.id_checklist_completado = int.Parse(id_checklist_completado);
                        adjunto.nombreOriginal = httpPostedFile.FileName;
                        adjunto.nombreGuardado = guid + "_" + httpPostedFile.FileName;
                        adjunto.pathImagen = subPath + guid + "_" + httpPostedFile.FileName;
                        FN_CHECKLISTS_IMAGENES.INSERT(ref adjunto);

                        // Low Quality  
                        Stream strm = httpPostedFile.InputStream;
                        var targetFile = fileSavePath;
                        //Based on scalefactor image size will vary  
                        GenerateThumbnails(0.5, strm, targetFile);                       
                        //httpPostedFile.SaveAs(fileSavePath);
                    }
                }
            }
        }

        private void GenerateThumbnails(double scaleFactor, Stream sourcePath, string targetPath)
        {
            using (var image = System.Drawing.Image.FromStream(sourcePath))
            {
                // can given width of image as we want  
                var newWidth = (int)(image.Width * scaleFactor);
                // can given height of image as we want  
                var newHeight = (int)(image.Height * scaleFactor);
                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath, image.RawFormat);
            }
        }


    }
}