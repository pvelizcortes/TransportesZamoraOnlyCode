using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
//using CreatePDF;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using AMALIAFW;
using System.Net.Mail;


namespace CRM
{

    public partial class Imprime_ocpv : System.Web.UI.Page
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
                if (Request.QueryString["num_oc"] != null)
                {
                    id_gt = Request.QueryString["num_oc"].ToString();
                }
                string nombre_pdf = usuario.usuario + "_" + DateTime.Now.ToString("dd/MM/yyyy") + id_gt;
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
                        OBJ_OCPV_ENC lagt = new OBJ_OCPV_ENC();
                        lagt.ID_OC = int.Parse(id_gt);
                        FN_OCPV_ENC.LLENAOBJETO(ref lagt);
                        if (lagt._respok)
                        {
                            // DT
                            DataTable dt = FN_OCPV_ENC.getDetalleOC(lagt.ID_OC);
                            // INFO
                            doc.SetPageSize(PageSize.A4);
                            doc.SetMargins(10f, 8f, 8f, 8f);
                            doc.AddTitle("AgroComercial Zamora");
                            doc.AddCreator("AgroComercial Zamora");
                            // FONTS
                            doc.Open();
                            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                            iTextSharp.text.Font _standardFont_bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                            iTextSharp.text.Font titulo_font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                            iTextSharp.text.Font subtitulo_font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
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
                            float[] widths1 = new float[] { 210f, 365f };
                            titulo.SetWidths(widths1);

                            // AGREGAR LOGOS   
                            iTextSharp.text.Image imagen = creaimagen("~/logoocagro.jpg", 100, "i");
                            // PRIMERA IMAGEN
                            agregaimagen(ref titulo, imagen, 0, "c");
                            agregacelda(ref titulo, "ORDEN DE COMPRA", titulo_font, 0, "i");

                            SaltoLinea(ref doc);
                            SaltoLinea(ref doc);
                            doc.Add(titulo);

                            SaltoLinea(ref doc);
                            //ENCABEZADO
                            // **************************************************************************************
                            PdfPTable TablaEnc = new PdfPTable(2);
                            TablaEnc.LockedWidth = true;
                            TablaEnc.TotalWidth = 575f;
                            float[] widths2 = new float[] { 375f, 200f };
                            TablaEnc.SetWidths(widths2);

                            // IZQ
                            PdfPTable tEncLeft = new PdfPTable(2);
                            tEncLeft.LockedWidth = true;
                            tEncLeft.TotalWidth = 375f;
                            float[] widths5 = new float[] { 90f, 285f };
                            tEncLeft.SetWidths(widths5);

                            agregacelda(ref tEncLeft, "RUT", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncLeft, "76.749.251-0", _standardFont, 0, "i");

                            agregacelda(ref tEncLeft, "RAZON SOCIAL", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncLeft, "AGROCOMERCIAL ZAMORA SPA", _standardFont, 0, "i");


                            agregacelda(ref tEncLeft, "TELEFONO", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncLeft, "+569 66533042", _standardFont, 0, "i");

                            //agregacelda(ref tEncLeft, "GIRO", _standardFont_bold, 0, "i");
                            //agregacelda(ref tEncLeft, " ", _standardFont, 0, "i");

                            agregacelda(ref tEncLeft, "DIRECCION", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncLeft, "FERIA MAYORISTA FEMACAL, #568", _standardFont, 0, "i");

                            agregacelda(ref tEncLeft, "CIUDAD", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncLeft, "LA CALERA", _standardFont, 0, "i");

                            // DER
                            PdfPTable tEncright = new PdfPTable(2);
                            tEncright.WidthPercentage = 100;
                            agregacelda(ref tEncright, "Nº", letragigante, 0, "i");
                            agregacelda(ref tEncright, lagt.correlativo_oc.ToString(), letragigante, 0, "c");

                            agregacelda(ref tEncright, "FECHA", _standardFont_bold, 0, "i");
                            agregacelda(ref tEncright, lagt.fecha_oc.ToString("dd/MM/yyyy"), _standardFont, 0, "c");

                            agregatabla(ref TablaEnc, tEncLeft);
                            agregatabla(ref TablaEnc, tEncright);

                            doc.Add(TablaEnc);
                            SaltoLinea(ref doc);

                            //ENCABEZADO 2
                            // **************************************************************************************
                            PdfPTable TablaEnc2 = new PdfPTable(2);
                            TablaEnc2.WidthPercentage = 100;

                            // IZQ
                            PdfPTable tEncLeft2 = new PdfPTable(2);
                            tEncLeft2.LockedWidth = true;
                            tEncLeft2.TotalWidth = 280f;
                            float[] widths3 = new float[] { 80f, 200f };
                            tEncLeft2.SetWidths(widths3);

                            agregacelda(ref tEncLeft2, "PROVEEDOR", _standardFont_bold, 1, "i");
                            agregacelda(ref tEncLeft2, dt.Rows[0]["razon_social"].ToString(), _standardFont, 1, "i");

                            agregacelda(ref tEncLeft2, "RUT", _standardFont_bold, 1, "i");
                            agregacelda(ref tEncLeft2, dt.Rows[0]["rut"].ToString(), _standardFont, 1, "i");

                            agregacelda(ref tEncLeft2, "DIRECCION", _standardFont_bold, 1, "i");
                            agregacelda(ref tEncLeft2, dt.Rows[0]["direccion"].ToString(), _standardFont, 1, "i");

                            agregacelda(ref tEncLeft2, "COMUNA", _standardFont_bold, 1, "i");
                            agregacelda(ref tEncLeft2, dt.Rows[0]["comuna"].ToString(), _standardFont, 1, "i");

                            agregacelda(ref tEncLeft2, "CIUDAD", _standardFont_bold, 1, "i");
                            agregacelda(ref tEncLeft2, dt.Rows[0]["ciudad"].ToString(), _standardFont, 1, "i");

                            agregacelda(ref tEncLeft2, "CONTACTO", _standardFont_bold, 1, "i");
                            agregacelda(ref tEncLeft2, dt.Rows[0]["prov_contacto"].ToString(), _standardFont, 1, "i");

                            agregacelda(ref tEncLeft2, "FONO", _standardFont_bold, 1, "i");
                            agregacelda(ref tEncLeft2, dt.Rows[0]["fono"].ToString(), _standardFont, 1, "i");

                            agregacelda(ref tEncLeft2, "BANCO", _standardFont_bold, 1, "i");
                            agregacelda(ref tEncLeft2, dt.Rows[0]["datosbancarios"].ToString(), _standardFont, 1, "i");

                            // DER
                            PdfPTable tEncright2 = new PdfPTable(2);
                            tEncright2.LockedWidth = true;
                            tEncright2.TotalWidth = 280f;
                            float[] widths4 = new float[] { 80f, 200f };
                            tEncright2.SetWidths(widths4);

                            agregacelda(ref tEncright2, "DESTINO OC", _standardFont_bold, 1, "i");
                            agregacelda(ref tEncright2, lagt.destino, _standardFont, 1, "i");

                            agregacelda(ref tEncright2, "CONTACTO OC", _standardFont_bold, 1, "i");
                            agregacelda(ref tEncright2, lagt.contacto, _standardFont, 1, "i");

                            agregacelda(ref tEncright2, "EMAIL OC", _standardFont_bold, 1, "i");
                            agregacelda(ref tEncright2, lagt.email, _standardFont, 1, "i");

                            agregacelda(ref tEncright2, "PLAZO ENTREGA", _standardFont_bold, 1, "i");
                            agregacelda(ref tEncright2, lagt.plazo_entrega.ToString("dd/MM/yyyy"), _standardFont, 1, "i");

                            agregacelda(ref tEncright2, "OBSERVACION", _standardFont_bold, 1, "i");
                            agregacelda(ref tEncright2, lagt.obs_aprobacion, _standardFont, 1, "i");

                            agregatabla(ref TablaEnc2, tEncLeft2);
                            agregatabla(ref TablaEnc2, tEncright2);

                            doc.Add(TablaEnc2);
                            SaltoLinea(ref doc);

                            // DATOS BANCARIOS
                            // **************************************************************************************
                            PdfPTable tBanco = new PdfPTable(4);
                            tBanco.WidthPercentage = 100;

                            agregacelda(ref tEncright2, "BANCO", _standardFont_bold, 0, "c");
                            agregacelda(ref tEncright2, "TIPO CUENTA", _standardFont_bold, 0, "c");
                            agregacelda(ref tEncright2, "Nº CUENTA", _standardFont_bold, 0, "c");
                            agregacelda(ref tEncright2, "RUT", _standardFont_bold, 0, "c");
                            agregacelda(ref tEncright2, "EMAIL", _standardFont_bold, 0, "c");

                            agregacelda(ref tEncright2, dt.Rows[0]["banco"].ToString(), _standardFont, 0, "c");
                            agregacelda(ref tEncright2, dt.Rows[0]["tipo_cuenta"].ToString(), _standardFont, 0, "c");
                            agregacelda(ref tEncright2, dt.Rows[0]["num_cuenta"].ToString(), _standardFont, 0, "c");
                            agregacelda(ref tEncright2, dt.Rows[0]["rut_cuenta"].ToString(), _standardFont, 0, "c");
                            agregacelda(ref tEncright2, dt.Rows[0]["prov_email"].ToString(), _standardFont, 0, "c");

                            doc.Add(tBanco);
                            SaltoLinea(ref doc);
                            // DETALLE OC
                            // **************************************************************************************
                            PdfPTable tDet = new PdfPTable(6);
                            tDet.LockedWidth = true;
                            tDet.TotalWidth = 575f;
                            float[] widths6 = new float[] { 50f, 50f, 50f, 265f, 80f, 80f };
                            tDet.SetWidths(widths6);

                            agregacelda(ref tDet, "LI", _standardFont_bold, 1, "c");
                            agregacelda(ref tDet, "CANT", _standardFont_bold, 1, "c");
                            agregacelda(ref tDet, "UM", _standardFont_bold, 1, "c");

                            agregacelda(ref tDet, "GLOSA", _standardFont_bold, 1, "c");
                            agregacelda(ref tDet, "UNITARIO", _standardFont_bold, 1, "c");
                            agregacelda(ref tDet, "NETO", _standardFont_bold, 1, "c");

                            // Foreach Detalle
                            foreach (DataRow dr in dt.Rows)
                            {
                                agregacelda(ref tDet, Convert.ToInt32(dr["li"].ToString()).ToString("#,##0"), _standardFont, 1, "c");
                                agregacelda(ref tDet, Convert.ToInt32(dr["cant"].ToString()).ToString("#,##0"), _standardFont, 1, "c");
                                agregacelda(ref tDet, dr["um"].ToString(), _standardFont, 1, "c");

                                agregacelda(ref tDet, dr["glosa"].ToString(), _standardFont, 1, "i");
                                agregacelda(ref tDet, Convert.ToDouble(dr["unitario"].ToString()).ToString("#,##0"), _standardFont, 1, "d");
                                agregacelda(ref tDet, Convert.ToDouble(dr["neto"].ToString()).ToString("#,##0"), _standardFont, 1, "d");
                            }
                            //
                            doc.Add(tDet);
                            SaltoLinea(ref doc);
                            // SOLICITANTE -- NETO IVA TOTAL
                            // **************************************************************************************
                            PdfPTable tSoli = new PdfPTable(2);
                            tSoli.LockedWidth = true;
                            tSoli.TotalWidth = 575f;
                            float[] widths7 = new float[] { 415f, 160f };
                            tSoli.SetWidths(widths7);

                            PdfPTable tSoli1 = new PdfPTable(1);
                            tSoli1.WidthPercentage = 100;

                            agregacelda(ref tSoli1, "Solicitante: " + lagt.solicitante, _standardFont_bold, 0, "c");
                            agregacelda(ref tSoli1, " ", _standardFont, 0, "c");
                            agregacelda(ref tSoli1, " ", _standardFont, 0, "c");


                            agregacelda(ref tSoli1, "Observacion: ", _standardFont, 0, "i");
                            agregacelda(ref tSoli1, "Sr. Proveedor recuerde que siempre enviar su factura en la cual debe incluir en las Referencias el Numero de ", _standardFont, 0, "i");
                            agregacelda(ref tSoli1, "orden de compra, Guia de despacho, Orden de Servicio y/o Estado de Pago si corresponde. En caso que la ", _standardFont, 0, "i");
                            agregacelda(ref tSoli1, "moneda sea distinta al Peso (CLP), debe Todas sus Facturas con sus respaldos deben ser enviadas a la ", _standardFont, 0, "i");
                            agregacelda(ref tSoli1, "casilla Facturacion@tzamora.cl en un solo archivo en PDF ", _standardFont, 0, "i");
                            agregacelda(ref tSoli1, "Importante: ", _standardFont, 0, "i");
                            agregacelda(ref tSoli1, "No se cursara la cancelacion de la factura de cualquier tipo que no cumpla con la documentacion requerida ", _standardFont, 0, "i");
                            agregacelda(ref tSoli1, "antes mencionado. ", _standardFont, 0, "i");

                            PdfPTable tSoli2 = new PdfPTable(2);
                            tSoli2.LockedWidth = true;
                            tSoli2.TotalWidth = 160f;
                            float[] widths8 = new float[] { 80f, 80f };
                            tSoli2.SetWidths(widths8);

                            agregacelda(ref tSoli2, "NETO", _standardFont_bold, 1, "i");
                            agregacelda(ref tSoli2, Convert.ToDouble(dt.Rows[0]["neto_enc"].ToString()).ToString("#,##0"), _standardFont_bold, 1, "d");

                            agregacelda(ref tSoli2, "IVA", _standardFont_bold, 1, "i");
                            agregacelda(ref tSoli2, Convert.ToDouble(dt.Rows[0]["iva_enc"].ToString()).ToString("#,##0"), _standardFont_bold, 1, "d");

                            agregacelda(ref tSoli2, "TOTAL", _standardFont_bold, 1, "i");
                            agregacelda(ref tSoli2, Convert.ToDouble(dt.Rows[0]["total_enc"].ToString()).ToString("#,##0"), _standardFont_bold, 1, "d");

                            agregacelda(ref tSoli2, " ", _standardFont_bold, 0, "i");
                            agregacelda(ref tSoli2, " ", _standardFont_bold, 0, "d");

                            agregacelda(ref tSoli2, " ", _standardFont_bold, 0, "i");
                            agregacelda(ref tSoli2, " ", _standardFont_bold, 0, "d");

                            agregacelda(ref tSoli2, " ", _standardFont_bold, 0, "i");
                            agregacelda(ref tSoli2, " ", _standardFont_bold, 0, "d");

                            agregacelda(ref tSoli2, " ", _standardFont_bold, 0, "i");
                            agregacelda(ref tSoli2, " ", _standardFont_bold, 0, "d");

                            PdfPCell td1 = new PdfPCell(new Phrase("Vº Bº ", _standardFont_bold));
                            td1.Colspan = 2;
                            td1.BorderWidth = 0;
                            td1.HorizontalAlignment = Element.ALIGN_CENTER;
                            tSoli2.AddCell(td1);

                            PdfPCell td2 = new PdfPCell(new Phrase("GERENTE GENERAL ", _standardFont_bold));
                            td2.Colspan = 2;
                            td2.Border = 0;
                            td2.BorderWidthTop = 1;
                            td2.HorizontalAlignment = Element.ALIGN_CENTER;
                            tSoli2.AddCell(td2);

                            agregatabla(ref tSoli, tSoli1, 0, "i");
                            agregatabla(ref tSoli, tSoli2, 0, "i");

                            doc.Add(tSoli);
                            SaltoLinea(ref doc);
                            // TABLA PAGOS
                            // **************************************************************************************
                            PdfPTable tPago = new PdfPTable(3);
                            tPago.LockedWidth = true;
                            tPago.TotalWidth = 575f;
                            float[] widths9 = new float[] { 350f, 100f, 125f };
                            tPago.SetWidths(widths9);

                            PdfPTable tPago1 = new PdfPTable(6);
                            tPago1.WidthPercentage = 100;

                            int dias = 30;
                            for (int i = 1; i <= 5; i++)
                            {
                                agregacelda(ref tPago1, i + "º PAGO", _standardFont, 1, "i");
                                agregacelda(ref tPago1, "", _standardFont, 1, "i");
                                if (i == 1)
                                {
                                    agregacelda(ref tPago1, "DIA", _standardFont, 1, "i");
                                }
                                else
                                {
                                    agregacelda(ref tPago1, dias.ToString(), _standardFont, 1, "i");
                                    dias += 30;
                                }
                                agregacelda(ref tPago1, "", _standardFont, 1, "i");
                                agregacelda(ref tPago1, "", _standardFont, 1, "i");
                                agregacelda(ref tPago1, "", _standardFont, 1, "i");

                            }
                            agregatabla(ref tPago, tPago1, 1, "i");
                            agregacelda(ref tPago, "FACTURA", _standardFont_bold, 1, "c");
                            agregacelda(ref tPago, "", _standardFont_bold, 1, "c");

                            doc.Add(tPago);
                            SaltoLinea(ref doc);
                            // TABLA AUTORIZADOS
                            // **************************************************************************************
                            PdfPTable tAUT = new PdfPTable(2);
                            tAUT.WidthPercentage = 100;


                            SaltoLinea(ref doc);

                            agregacelda(ref tAUT, "Autorización Mauricio Zapata", _standardFont_bold, 0, "c");
                            agregacelda(ref tAUT, "Autorización Felipe Zamora", _standardFont_bold, 0, "c");

                            if (dt.Rows[0]["aprobado_mz"].ToString() == "SI")
                            {
                                agregacelda(ref tAUT, "Autorizada", _standardFont_bold, 0, "c");
                            }
                            else if (dt.Rows[0]["aprobado_mz"].ToString() == "NO")
                            {
                                agregacelda(ref tAUT, "No Autorizada", _standardFont_bold, 0, "c");
                            }
                            else if (dt.Rows[0]["aprobado_mz"].ToString() == "RECHAZADA")
                            {
                                agregacelda(ref tAUT, "Rechazada", _standardFont_bold, 0, "c");
                            }

                            if (dt.Rows[0]["aprobado_fz"].ToString() == "SI")
                            {
                                agregacelda(ref tAUT, "Autorizada", _standardFont_bold, 0, "c");
                            }
                            else if (dt.Rows[0]["aprobado_fz"].ToString() == "NO")
                            {
                                agregacelda(ref tAUT, "No Autorizada", _standardFont_bold, 0, "c");
                            }
                            else if (dt.Rows[0]["aprobado_fz"].ToString() == "RECHAZADA")
                            {
                                agregacelda(ref tAUT, "Rechazada", _standardFont_bold, 0, "c");
                            }

                            doc.Add(tAUT);

                            SaltoLinea(ref doc);
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
        public void agregacelda(ref PdfPTable tabla, string texto, iTextSharp.text.Font fuente, int borde = 0, string alineamiento = "i", int tamaño = 100)
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
            tabla.AddCell(td);
        }
    }
}