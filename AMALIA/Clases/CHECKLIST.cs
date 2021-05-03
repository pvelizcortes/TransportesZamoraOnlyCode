
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.IO;
using System.Net;
using AMALIAFW;
using System.Web;
using System.Collections;
using System.Collections.Generic;

namespace AMALIAFW
{  
    public class CHECKLIST
    {
        public Document ArmarChecklist(iTextSharp.text.Document doc, OBJ_CHECKLISTS_COMPLETADOS lagt)
        {
            DBUtil db = new DBUtil();
            OBJ_CHECKLISTS chk = new OBJ_CHECKLISTS();
            chk.ID_CHECKLIST = lagt.id_checklist;
            FN_CHECKLISTS.LLENAOBJETO(ref chk);

            // DT
            DataTable dt = FN_CHECKLISTS_COMPLETADOS.LLENADT(" where id_checklist_completado = " + lagt.ID_CHECKLIST_COMPLETADO);
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font _standardFont_bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

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
            agregacelda(ref titulo, "CHECKLIST", titulo_font, 0, "i");

            SaltoLinea(ref doc);
            doc.Add(titulo);

            SaltoLinea(ref doc);
            //ENCABEZADO
            // **************************************************************************************             
            PdfPTable tTitulo0 = new PdfPTable(1);
            tTitulo0.WidthPercentage = 90;


            PdfPTable tEncright = new PdfPTable(4);
            tEncright.TotalWidth = 100;
            float[] widths11 = new float[] { 17f, 33f, 18f, 32f };
            tEncright.SetWidths(widths11);

            agregacelda(ref tEncright, "Nº REF.", _standardFont_bold, 0, "i");
            agregacelda(ref tEncright, ": " + lagt.ID_CHECKLIST_COMPLETADO.ToString(), _standardFont_bold, 0, "i");

            //agregacelda(ref tEncright, "NOMBRE DEL FORMULARIO", _standardFont_bold, 0, "i");
            //agregacelda(ref tEncright, chk.nombre_checklist.ToUpper(), _standardFont, 0, "i");

            agregacelda(ref tEncright, "INSPECCIONA", _standardFont_bold, 0, "i");
            agregacelda(ref tEncright, ": " + lagt.nombre_inspeccion.ToUpper(), _standardFont, 0, "i");

            agregacelda(ref tEncright, "CONDUCTOR", _standardFont_bold, 0, "i");
            agregacelda(ref tEncright, ": " + lagt.nombre_conductor.ToUpper() + ", " + lagt.rut, _standardFont, 0, "i");

            agregacelda(ref tEncright, "LUGAR PROVEEDOR", _standardFont_bold, 0, "i");
            agregacelda(ref tEncright, ": " + lagt.nombre_Proveedor.ToUpper(), _standardFont, 0, "i");

            agregacelda(ref tEncright, "PATENTE CAMIÓN", _standardFont_bold, 0, "i");
            agregacelda(ref tEncright, ": " + lagt.patente_camion.ToUpper(), _standardFont, 0, "i");

            agregacelda(ref tEncright, "FECHA ENVÍO ", _standardFont_bold, 0, "i");
            agregacelda(ref tEncright, ": " + lagt.fecha.ToString("dd/MM/yyyy"), _standardFont, 0, "i");

            agregacelda(ref tEncright, "PATENTE RAMPLA", _standardFont_bold, 0, "i");
            agregacelda(ref tEncright, ": " + lagt.patente_rampla.ToUpper(), _standardFont, 0, "i");


            agregacelda(ref tEncright, " ", _standardFont_bold, 0, "i");
            agregacelda(ref tEncright, " ", _standardFont, 0, "i");

            agregacelda(ref tEncright, "OBSERVACION", _standardFont_bold, 0, "i");
            agregacelda(ref tEncright, ": " + lagt.observacion.ToUpper(), _standardFont, 0, "i", colspan: 3);


            agregatabla(ref tTitulo0, tEncright, 1);

            doc.Add(tTitulo0);

            SaltoLinea(ref doc);

            PdfPTable tTitulo1 = new PdfPTable(1);
            tTitulo1.WidthPercentage = 90;

            PdfPTable tEncLeft = new PdfPTable(2);

            tEncLeft.TotalWidth = 575f;
            float[] widths12 = new float[] { 430f, 135f };
            tEncLeft.SetWidths(widths12);

            agregacelda(ref tEncLeft, "RESPUESTAS", subtitulo_font, 1, "i", 1, 0, true, "yellow", 2);

            DataTable Respuestas = db.consultar("select * from V_CHECKLISTS_RESPUESTAS WHERE ID_CHECKLIST_COMPLETADO = " + lagt.ID_CHECKLIST_COMPLETADO);
            if (Respuestas.Rows.Count > 0)
            {
                foreach (DataRow dr in Respuestas.Rows)
                {
                    agregacelda(ref tEncLeft, dr["nombre_pregunta"].ToString(), _standardFont_bold, 1, "i");
                    agregacelda(ref tEncLeft, dr["respuesta"].ToString(), _standardFont, 1, "c");
                }
            }

            agregatabla(ref tTitulo1, tEncLeft, 1, "i");
            doc.Add(tTitulo1);
            SaltoLinea(ref doc);

            // Agregar imagenes
            PdfPTable tTitulo2 = new PdfPTable(1);
            tTitulo2.WidthPercentage = 90;
            agregacelda(ref tTitulo2, "IMAGENES ADJUNTAS", subtitulo_font, 1, "i", 1, 0, true, "yellow");
            doc.Add(tTitulo2);

            PdfPTable tImagenes = new PdfPTable(3);
            tImagenes.WidthPercentage = 90;

            DataTable dtImagenes = new DataTable();
            dtImagenes = FN_CHECKLISTS_IMAGENES.LLENADT(" where id_checklist_completado = " + lagt.ID_CHECKLIST_COMPLETADO);
            int contImages = 0;
            if (dtImagenes.Rows.Count > 0)
            {
                foreach (DataRow dr in dtImagenes.Rows)
                {
                    try
                    {
                        // AGREGAR LOGOS   
                        iTextSharp.text.Image imagenAdjunta = creaimagen("~/Checklist/" + lagt.ID_CHECKLIST_COMPLETADO + "/" + dr["nombreGuardado"].ToString(), 120, "c");
                        // PRIMERA IMAGEN
                        agregaimagen(ref tImagenes, imagenAdjunta, 1, "c");
                        contImages++;
                        if (contImages == 3)
                            contImages = 0;
                    }
                    catch (Exception ex)
                    {

                    }

                }
                if (contImages == 1)
                {
                    agregacelda(ref tImagenes, " ", _standardFont_bold, 0, "i");
                    agregacelda(ref tImagenes, " ", _standardFont_bold, 0, "i");
                }
                if (contImages == 2)
                {
                    agregacelda(ref tImagenes, " ", _standardFont_bold, 0, "i");
                }
            }
            //
            doc.Add(tImagenes);

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

            return doc;
        }

        private void SaltoLinea(ref Document x)
        {
            PdfPTable espacio = new PdfPTable(1);
            PdfPCell blanco = new PdfPCell(new Phrase(" "));
            blanco.BorderWidth = 0;
            espacio.AddCell(blanco);
            x.Add(espacio);
        }

        private iTextSharp.text.Image creaimagen(string url, int size, string alineamiento = "i")
        {
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(url));
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

        private void agregatabla(ref PdfPTable tabla, PdfPTable tabla_a_insertar, int borde = 0, string alineamiento = "i")
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

        private void agregaimagen(ref PdfPTable tabla, iTextSharp.text.Image imagen, int borde = 0, string alineamiento = "i")
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
        private void agregacelda(ref PdfPTable tabla, string texto, iTextSharp.text.Font fuente, int borde = 0, string alineamiento = "i", int tamaño = 100, int borderBot = 0, bool color = false, string nameColor = "", int colspan = 1)
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
        private class ITextEvents : PdfPageEventHelper
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
