using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.IO;
using System.Net;
using AMALIAFW;
using System.Web;



namespace CRM
{

    public partial class Imprime_hallazgo : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Document doc = new Document();
            try
            {

                DBUtil db = new DBUtil();
                OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                usuario.usuario = HttpContext.Current.User.Identity.Name;
                FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);
                //
                string path = Server.MapPath("PDF_GT");
                string id_gt = "";
                if (Request.QueryString["num_hallazgo"] != null)
                {
                    id_gt = Request.QueryString["num_hallazgo"].ToString();
                }
                string nombre_pdf = "hallazgo_" + usuario.usuario + "_" + DateTime.Now.ToString("dd/MM/yyyy") + id_gt;
                string n_file = "/" + nombre_pdf.Replace(",", "").Replace(".", "").Replace(" ", "").Replace("/", "") + ".pdf";
                try
                {
                    System.IO.File.Delete(path + n_file);
                }
                catch (System.IO.IOException ex)
                {

                }

                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path + n_file, FileMode.Create));

                try
                {
                    if (id_gt != "")
                    {
                        OBJ_HALLAZGO lagt = new OBJ_HALLAZGO();
                        lagt.ID_HALLAZGO = int.Parse(id_gt);
                        FN_HALLAZGO.LLENAOBJETO(ref lagt);
                        if (lagt._respok)
                        {
                            ITextEvents PageEventHandler = new ITextEvents()
                            {
                                ImageFooter = iTextSharp.text.Image.GetInstance(Server.MapPath("~//banner.jpg"))
                            };
                            writer.PageEvent = PageEventHandler;
                      
                            // DT
                            DataTable dt = FN_HALLAZGO.LLENADT(" where id_hallazgo = " + lagt.ID_HALLAZGO);
                            // INFO
                            doc.SetPageSize(PageSize.A4);
                            doc.SetMargins(10f, 10f, 20f, 150f);
                            doc.AddTitle("Transportes Zamora");
                            doc.AddCreator("Transportes Zamora");
                            // FONTS
                            doc.Open();
                            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                            iTextSharp.text.Font _standardFont_bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                            iTextSharp.text.Font titulo_font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                            iTextSharp.text.Font subtitulo_font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                            // LETRA CHICA
                            iTextSharp.text.Font _smallFont_bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                            iTextSharp.text.Font _smallFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                            iTextSharp.text.Font letragigante = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                            // ESPACIO
                            PdfPTable espacio = new PdfPTable(1);
                            PdfPCell blanco = new PdfPCell(new Phrase(" ", _standardFont));
                            blanco.BorderWidth = 0;
                            espacio.AddCell(blanco);

                            // TITULO      
                            // **************************************************************************************                
                            PdfPTable titulo = new PdfPTable(2);
                            titulo.LockedWidth = true;
                            titulo.TotalWidth = 575f;
                            float[] widths1 = new float[] { 280f, 285f };
                            titulo.SetWidths(widths1);

                            // AGREGAR LOGOS   
                            iTextSharp.text.Image imagen = creaimagen("~/logozamora.png", 150, "i");
                            // PRIMERA IMAGEN
                            agregaimagen(ref titulo, imagen, 0, "c");                                 
                            agregacelda(ref titulo, "DETECCION DE HALLAZGO", titulo_font, 0, "i");

                            SaltoLinea(ref doc);                       
                            doc.Add(titulo);

                            SaltoLinea(ref doc);
                            //ENCABEZADO
                            // **************************************************************************************             
                            PdfPTable tTitulo0 = new PdfPTable(1);
                            tTitulo0.WidthPercentage = 90;
            

                            PdfPTable tEncright = new PdfPTable(2);
                            tEncright.TotalWidth = 575f;
                            float[] widths11 = new float[] { 220f, 345f };
                            tEncright.SetWidths(widths11);
                    
                            agregacelda(ref tEncright, "Nº REF.", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncright, lagt.num_referencia.ToString(), _standardFont_bold, 0, "i");

                            agregacelda(ref tEncright, "REMITENTE", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncright, lagt.nombre_remitente.ToUpper(), _standardFont, 0, "i");

                            agregacelda(ref tEncright, "FECHA ENVÍO ", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncright, lagt.fecha_envio.ToString("dd/MM/yyyy"), _standardFont, 0, "i");

                            agregatabla(ref tTitulo0, tEncright, 1);

                            doc.Add(tTitulo0);
                            SaltoLinea(ref doc);                         

                            PdfPTable tTitulo1 = new PdfPTable(1);
                            tTitulo1.WidthPercentage = 90;
                          

                            PdfPTable tEncLeft = new PdfPTable(2);

                            tEncLeft.TotalWidth = 575f;
                            float[] widths12 = new float[] { 220f, 345f };
                            tEncLeft.SetWidths(widths12);

                            agregacelda(ref tEncLeft, "IDENTIFICACIÓN", subtitulo_font, 1, "i", 1, 0, true, "yellow",2);                            

                            agregacelda(ref tEncLeft, "AREA", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncLeft, lagt.area, _standardFont, 0, "i");

                            //agregacelda(ref tEncLeft, "LUGAR", _standardFont_bold, 0, "i");
                            //agregacelda(ref tEncLeft, lagt.lugar, _standardFont, 0, "i");

                            agregacelda(ref tEncLeft, "DETECCION", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncLeft, lagt.deteccion, _standardFont, 0, "i");

                            agregacelda(ref tEncLeft, "TIPO", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncLeft, lagt.tipo, _standardFont, 0, "i");

                            agregacelda(ref tEncLeft, "ORIGEN", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncLeft, lagt.origen, _standardFont, 0, "i");

                            if (lagt.destino != " ")
                            {
                                agregacelda(ref tEncLeft, "DESTINO", _standardFont_bold, 0, "i");
                                agregacelda(ref tEncLeft, lagt.destino, _standardFont, 0, "i");
                            }               

                            agregacelda(ref tEncLeft, "NOMBRE DE QUIEN DETECTA", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncLeft, lagt.nombre_deteccion, _standardFont, 0, "i");


                            //agregacelda(ref tEncLeft, "CARGO", _standardFont_bold, 0, "i");
                            //agregacelda(ref tEncLeft, lagt.cargo, _standardFont, 0, "i");

                            agregatabla(ref tTitulo1, tEncLeft, 1, "i");

                            doc.Add(tTitulo1);
                            SaltoLinea(ref doc);
                            //doc.Add(tEncLeft);

                            PdfPTable tTitulo11 = new PdfPTable(1);
                            tTitulo11.WidthPercentage = 90;                     

                            PdfPTable tEncLeft1 = new PdfPTable(2);

                            tEncLeft1.TotalWidth = 575f;
                            float[] widths13 = new float[] { 220f, 345f };
                            tEncLeft1.SetWidths(widths13);

                            agregacelda(ref tEncLeft1, "DETECCIÓN DE HALLAZGO", subtitulo_font, 1, "i", 1, 0, true, "yellow", 2);

                            agregacelda(ref tEncLeft1, "DETECCION DEL HALLAZGO", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncLeft1, lagt.deteccion_hallazgo, _standardFont, 0, "i");

                            agregacelda(ref tEncLeft1, " ", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncLeft1, " ", _standardFont_bold, 0, "i");

                            agregacelda(ref tEncLeft1, "DOCUMENTO DE REFERENCIA", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncLeft1, lagt.doc_referencia, _standardFont, 0, "i");

                            agregacelda(ref tEncLeft1, " ", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncLeft1, " ", _standardFont_bold, 0, "i");

                            agregacelda(ref tEncLeft1, "ACCION INMEDIATA", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncLeft1, lagt.accion_inmediata, _standardFont, 0, "i");

                            agregacelda(ref tEncLeft1, " ", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncLeft1, " ", _standardFont_bold, 0, "i");

                            agregacelda(ref tEncLeft1, "EMPRESA INVOLUCRADA", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncLeft1, lagt.empresainvolucrada, _standardFont, 0, "i");

                            agregatabla(ref tTitulo11, tEncLeft1, 1, "c");
           
                            doc.Add(tTitulo11);
                       
                            SaltoLinea(ref doc);
                            SaltoLinea(ref doc);
                            SaltoLinea(ref doc);
                            SaltoLinea(ref doc);
                            SaltoLinea(ref doc);
                            SaltoLinea(ref doc);
                            PdfPTable tFirma = new PdfPTable(1);
                            tFirma.WidthPercentage = 30;
                            PdfPCell td2 = new PdfPCell(new Phrase("FIRMA ", _standardFont_bold));                     
                            td2.Border = 0;
                            td2.BorderWidthTop = 1;
                            td2.HorizontalAlignment = Element.ALIGN_CENTER;
                            tFirma.AddCell(td2);
                            doc.Add(tFirma);
                            // FIN
                            doc.Close();
                            writer.Close();

                            string pdfPath = Server.MapPath("~/PDF_GT/" + n_file);
                            WebClient client = new WebClient();
                            Byte[] buffer = client.DownloadData(pdfPath);
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-length", buffer.Length.ToString());
                            Response.BinaryWrite(buffer);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                catch (Exception ex)
                {
                    doc.Close();
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                doc.Close();
            }
        }

        public void SaltoLinea(ref Document x)
        {
            PdfPTable espacio = new PdfPTable(1);
            PdfPCell blanco = new PdfPCell(new Phrase(" "));
            blanco.BorderWidth = 0;
            espacio.AddCell(blanco);
            x.Add(espacio);
        }

        public iTextSharp.text.Image creaimagen(string url, int size, string alineamiento = "i")
        {
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(Server.MapPath(url));
            imagen.BorderWidth = 0;
            if (alineamiento == "i")
            {
                imagen.Alignment = Element.ALIGN_LEFT;
            }
            else if (alineamiento == "c")
            {
                imagen.Alignment = Element.ALIGN_CENTER;
            }
            else if (alineamiento == "d")
            {
                imagen.Alignment = Element.ALIGN_RIGHT;
            }
            float percentage = 0.0f;
            percentage = 100 / imagen.Width;
            imagen.ScalePercent(percentage * size);
            return imagen;
        }

        public void agregatabla(ref PdfPTable tabla, PdfPTable tabla_a_insertar, int borde = 0, string alineamiento = "i")
        {
            // BORDE 0 (sin borde) , 1 Con borde
            // ALINEAMIENTO i = Izq , C = Center , D = Derecha
            PdfPCell td = new PdfPCell(tabla_a_insertar);
            if (alineamiento == "i")
            {
                td.HorizontalAlignment = Element.ALIGN_LEFT;
            }
            else if (alineamiento == "c")
            {
                td.HorizontalAlignment = Element.ALIGN_CENTER;
            }
            else if (alineamiento == "d")
            {
                td.HorizontalAlignment = Element.ALIGN_RIGHT;
            }
            td.BorderWidth = borde;
            tabla.AddCell(td);
        }

        public void agregaimagen(ref PdfPTable tabla, iTextSharp.text.Image imagen, int borde = 0, string alineamiento = "i")
        {
            // BORDE 0 (sin borde) , 1 Con borde
            // ALINEAMIENTO i = Izq , C = Center , D = Derecha
            PdfPCell td = new PdfPCell(imagen);
            if (alineamiento == "i")
            {
                td.HorizontalAlignment = Element.ALIGN_LEFT;
            }
            else if (alineamiento == "c")
            {
                td.HorizontalAlignment = Element.ALIGN_CENTER;
            }
            else if (alineamiento == "d")
            {
                td.HorizontalAlignment = Element.ALIGN_RIGHT;
            }
            td.BorderWidth = borde;
            tabla.AddCell(td);
        }
        /// <summary>
        /// Agregar Celda
        /// </summary>
        /// <param name="tabla"></param>
        /// <param name="texto"></param>
        /// <param name="fuente"></param>
        /// <param name="borde">1 para borde</param>
        /// <param name="alineamiento">i izquierda, c centro, d derecha</param>
        /// <param name="tamaño"></param>
        public void agregacelda(ref PdfPTable tabla, string texto, iTextSharp.text.Font fuente, int borde = 0, string alineamiento = "i", int tamaño = 100, int borderBot = 0, bool color = false, string nameColor = "", int colspan = 1)
        {
            PdfPCell td = new PdfPCell(new Phrase(texto, fuente));
            if (alineamiento == "i")
            {
                td.HorizontalAlignment = Element.ALIGN_LEFT;
            }
            else if (alineamiento == "c")
            {
                td.HorizontalAlignment = Element.ALIGN_CENTER;
            }
            else if (alineamiento == "d")
            {
                td.HorizontalAlignment = Element.ALIGN_RIGHT;
            }
            td.BorderWidth = borde;
            td.BorderWidthTop = borderBot;
            td.Colspan = colspan;

            if (color)
            {
                if (nameColor == "blue")
                td.BackgroundColor = new iTextSharp.text.BaseColor(System.Drawing.Color.Blue);
                if (nameColor == "red")
                    td.BackgroundColor = new iTextSharp.text.BaseColor(System.Drawing.Color.Red);
                if (nameColor == "yellow")
                    td.BackgroundColor = new iTextSharp.text.BaseColor(System.Drawing.Color.Orange);
                if (nameColor == "green")
                    td.BackgroundColor = new iTextSharp.text.BaseColor(System.Drawing.Color.Green);
                if (nameColor == "black")
                    td.BackgroundColor = new iTextSharp.text.BaseColor(System.Drawing.Color.Black);   
            }
          
            tabla.AddCell(td);
        }
        public class ITextEvents : PdfPageEventHelper
        {
            public Image ImageFooter { get; set; }
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                ImageFooter.SetAbsolutePosition(0, 0);
            }
            public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
            {
                float percentage = 0f;
                percentage = 595 / ImageFooter.Width;
                ImageFooter.ScalePercent(percentage * 100);
                writer.DirectContent.AddImage(ImageFooter);
            }
        }
    }
}