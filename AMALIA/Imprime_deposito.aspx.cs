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

    public partial class Imprime_deposito : System.Web.UI.Page
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
                if (Request.QueryString["id_deposito"] != null)
                {
                    id_gt = Request.QueryString["id_deposito"].ToString();
                }
                string nombre_pdf = "deposito_" + usuario.usuario + "_" + DateTime.Now.ToString("dd/MM/yyyy") + id_gt;
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
                        OBJ_DEPOSITO_ENC lagt = new OBJ_DEPOSITO_ENC();
                        lagt.ID_DEPOSITO = int.Parse(id_gt);
                        FN_DEPOSITO_ENC.LLENAOBJETO(ref lagt);
                        if (lagt._respok)
                        {       
                            // DT
                            DataTable dt = FN_DEPOSITO_DETALLE.LLENADTVISTA(" where id_deposito = " + lagt.ID_DEPOSITO);
                            // INFO
                            doc.SetPageSize(PageSize.A4);
                            doc.SetMargins(10f, 10f, 20f, 150f);
                            doc.AddTitle("Transportes Zamora");
                            doc.AddCreator("Transportes Zamora");
                            // FONTS
                            doc.Open();
                            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
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
                            agregacelda(ref titulo, "INFORME DEPOSITOS", titulo_font, 0, "c");
                            agregacelda(ref titulo, " ", subtitulo_font, 0, "i");
                            agregacelda(ref titulo, "Nº OPERACIÓN:" + lagt.num_operacion.ToString(), subtitulo_font, 0, "c");
                            SaltoLinea(ref doc);                       
                            doc.Add(titulo);
                            SaltoLinea(ref doc);
                            espacio.AddCell(blanco);
                            espacio.AddCell(blanco);
                            espacio.AddCell(blanco);
                            doc.Add(espacio);
                            // DEPOSITOS
                            // **************************************************************************************
                            PdfPTable tDet = new PdfPTable(8);
                            tDet.LockedWidth = true;
                            tDet.TotalWidth = 575f;
                            float[] widths6 = new float[] { 50f,50f, 100f, 60f, 150, 50f, 50f, 70f };
                            tDet.SetWidths(widths6);
                         
                            agregacelda(ref tDet, "FECHA", _smallFont_bold, 1, "c");
                            agregacelda(ref tDet, "VIAJE", _smallFont_bold, 1, "c");
                            agregacelda(ref tDet, "CONDUCTOR", _smallFont_bold, 1, "c");
                            agregacelda(ref tDet, "TIPO", _smallFont_bold, 1, "c");
                            agregacelda(ref tDet, "GLOSA", _smallFont_bold, 1, "c");
                            agregacelda(ref tDet, "VALOR", _smallFont_bold, 1, "d");
                            agregacelda(ref tDet, "MONTO DEPOSITADO", _smallFont_bold, 1, "d");
                            agregacelda(ref tDet, "ESTADO", _smallFont_bold, 1, "c");

                            // Foreach Detalle
                            foreach (DataRow dr in dt.Rows)
                            {
                                agregacelda(ref tDet, Convert.ToDateTime(dr["fecha"].ToString()).ToString("dd/MM/yyyy"), _standardFont, 1, "c");
                                agregacelda(ref tDet, dr["num_viaje"].ToString().ToUpper(), _standardFont, 1, "i");
                                agregacelda(ref tDet, dr["nombre_conductor"].ToString().ToUpper(), _standardFont, 1, "i");
                                agregacelda(ref tDet, dr["tipo"].ToString().ToUpper(), _standardFont, 1, "c");
                                agregacelda(ref tDet, dr["comentario"].ToString().ToUpper(), _smallFont, 1, "i");
                                agregacelda(ref tDet, "$ " + Convert.ToInt32(dr["valor"].ToString()).ToString("#,##0"), _standardFont, 1, "d");
                                agregacelda(ref tDet, "$ " + Convert.ToInt32(dr["monto_depositado"].ToString()).ToString("#,##0"), _standardFont, 1, "d");
                                agregacelda(ref tDet, dr["estado"].ToString().ToUpper(), _standardFont, 1, "c");            
                            }
                            //
                            doc.Add(tDet);
                            SaltoLinea(ref doc);
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
            //td.BorderWidthTop = borderBot;
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
    }
}