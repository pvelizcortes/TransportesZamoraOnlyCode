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

    public partial class Imprime_gt_new : System.Web.UI.Page
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
                if (Request.QueryString["num_gt"] != null)
                {
                    id_gt = Request.QueryString["num_gt"].ToString();
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
                        double aux_km_inicial = 0;
                        OBJ_ENC_GT lagt = new OBJ_ENC_GT();
                        lagt.ID_GT = int.Parse(id_gt);
                        FN_ENC_GT.LLENAOBJETO(ref lagt);
                        if (lagt._respok)
                        {
                            // VALORES DEPOSITOS DETALLE
                            DataTable dtValores = db.consultar("select " +
                                " (select isnull(SUM(valor), 0) as 'valor' from deposito_Detalle where estado = 'DEPOSITADO' " +
                                " and num_viaje = " + lagt.num_correlativo + " and tipo = 'FONDO POR RENDIR') as 'fondoporrendir' " +
                                " , (select isnull(SUM(valor), 0) as 'valor' from deposito_Detalle where estado = 'DEPOSITADO' " +
                                " and num_viaje = " + lagt.num_correlativo + " and tipo = 'VIATICO') as 'viatico' " +
                                " , (select isnull(SUM(valor), 0) as 'valor' from deposito_Detalle where estado = 'DEPOSITADO' " +
                                " and num_viaje = " + lagt.num_correlativo + " and tipo = 'SALDO FONDO POR RENDIR') as 'saldofondoporrendir' " +
                                " , (select isnull(SUM(valor), 0) as 'valor' from deposito_Detalle where estado = 'DEPOSITADO' " +
                                " and num_viaje = " + lagt.num_correlativo + " and tipo = 'SALDO VIATICO') as 'saldoviatico' ");

                            int FondoPorRendir = int.Parse(dtValores.Rows[0]["fondoporrendir"].ToString());
                            int Viatico = int.Parse(dtValores.Rows[0]["viatico"].ToString());                           

                            // VALORES SALDOS GT
                            DataTable dtGastos = db.consultar(" select (SELECT ISNULL(SUM(VALOR),0) FROM GASTO_GENERAL WHERE TIPO_GASTO = 2 AND ID_GT = " + lagt.ID_GT + ") as 'gastoviatico' " +
                            " , (SELECT ISNULL(SUM(VALOR), 0) FROM GASTO_GENERAL WHERE TIPO_GASTO <> 2 AND ID_GT = " + lagt.ID_GT + ") as 'gastosinviatico'; ");

                            int gastoFXR = int.Parse(dtGastos.Rows[0]["gastosinviatico"].ToString());
                            int gastosViaticos = int.Parse(dtGastos.Rows[0]["gastoviatico"].ToString());

                            int SaldoFondoPorRendir = FondoPorRendir - gastoFXR;
                            int SaldoViatico = Viatico - gastosViaticos;

                            // DT
                            DataTable dt = FN_ENC_GT.LLENADTVISTA(" where id_gt = " + id_gt);
                            // INFO
                            doc.SetPageSize(PageSize.A4);
                            doc.SetMargins(10f, 5f, 5f, 5f);
                            doc.AddTitle("Transportes Zamora");
                            doc.AddCreator("Transportes Zamora");
                            // FONTS
                            doc.Open();
                            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                            iTextSharp.text.Font _standardFont_bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                            iTextSharp.text.Font titulo_font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                            iTextSharp.text.Font subtitulo_font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                            // LETRA CHICA
                            iTextSharp.text.Font _smallFont_bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                            iTextSharp.text.Font _smallFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                            // ESPACIO
                            PdfPTable espacio = new PdfPTable(1);
                            PdfPCell blanco = new PdfPCell(new Phrase(" ", _standardFont));
                            blanco.BorderWidth = 0;
                            espacio.AddCell(blanco);

                            // TITULO      
                            // **************************************************************************************                
                            PdfPTable titulo = new PdfPTable(3);
                            titulo.WidthPercentage = 100;
                            PdfPCell td1 = new PdfPCell(new Phrase("TRANSPORTES ZAMORA", titulo_font));
                            td1.HorizontalAlignment = Element.ALIGN_LEFT;
                            td1.BorderWidth = 0;
                            titulo.AddCell(td1);
                            PdfPCell td2 = new PdfPCell(new Phrase("GUÍA DE TRANSPORTE", titulo_font));
                            td2.HorizontalAlignment = Element.ALIGN_CENTER;
                            td2.BorderWidth = 0;
                            titulo.AddCell(td2);
                            PdfPCell td3 = new PdfPCell(new Phrase("GT: " + lagt.num_correlativo.ToString(), titulo_font));
                            td3.HorizontalAlignment = Element.ALIGN_RIGHT;
                            td3.BorderWidth = 0;
                            titulo.AddCell(td3);

                            doc.Add(titulo);
                            doc.Add(espacio);


                            //ENCABEZADO
                            // **************************************************************************************
                            PdfPTable encabezado = new PdfPTable(4);
                            encabezado.WidthPercentage = 100;

                            PdfPCell enc_td1 = new PdfPCell(new Phrase("Fecha Inicio:", _standardFont_bold));
                            enc_td1.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td1.BorderWidth = 0;
                            encabezado.AddCell(enc_td1);
                            PdfPCell enc_td11 = new PdfPCell(new Phrase(dt.Rows[0]["f_inicio"].ToString(), _standardFont));
                            enc_td11.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td11.BorderWidth = 0;
                            encabezado.AddCell(enc_td11);

                            PdfPCell enc_td2 = new PdfPCell(new Phrase("Fecha Termino:", _standardFont_bold));
                            enc_td2.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td2.BorderWidth = 0;
                            encabezado.AddCell(enc_td2);
                            PdfPCell enc_td22 = new PdfPCell(new Phrase(dt.Rows[0]["f_termino"].ToString(), _standardFont));
                            enc_td22.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td22.BorderWidth = 0;
                            encabezado.AddCell(enc_td22);

                            PdfPCell enc_td3 = new PdfPCell(new Phrase("Conductor:", _standardFont_bold));
                            enc_td3.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td3.BorderWidth = 0;
                            encabezado.AddCell(enc_td3);
                            PdfPCell enc_td33 = new PdfPCell(new Phrase(dt.Rows[0]["conductor1"].ToString(), _standardFont));
                            enc_td33.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td33.BorderWidth = 0;
                            encabezado.AddCell(enc_td33);

                            PdfPCell enc_td4 = new PdfPCell(new Phrase("Camión:", _standardFont_bold));
                            enc_td4.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td4.BorderWidth = 0;
                            encabezado.AddCell(enc_td4);
                            PdfPCell enc_td44 = new PdfPCell(new Phrase(dt.Rows[0]["patente_camion"].ToString(), _standardFont));
                            enc_td44.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td44.BorderWidth = 0;
                            encabezado.AddCell(enc_td44);

                            PdfPCell enc_td5 = new PdfPCell(new Phrase("Km Inicial:", _standardFont_bold));
                            enc_td5.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td5.BorderWidth = 0;
                            encabezado.AddCell(enc_td5);
                            PdfPCell enc_td55 = new PdfPCell(new Phrase(int.Parse(dt.Rows[0]["km_inicial"].ToString()).ToString("#,##0"), _standardFont));
                            aux_km_inicial = double.Parse(dt.Rows[0]["km_inicial"].ToString());
                            enc_td55.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td55.BorderWidth = 0;
                            encabezado.AddCell(enc_td55);

                            PdfPCell enc_td6 = new PdfPCell(new Phrase("Km Final:", _standardFont_bold));
                            enc_td6.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td6.BorderWidth = 0;
                            encabezado.AddCell(enc_td6);
                            PdfPCell enc_td66 = new PdfPCell(new Phrase(int.Parse(dt.Rows[0]["km_final"].ToString()).ToString("#,##0"), _standardFont));
                            enc_td66.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td66.BorderWidth = 0;
                            encabezado.AddCell(enc_td66);

                            PdfPCell enc_td9 = new PdfPCell(new Phrase("Kms Recorridos:", _standardFont_bold));
                            enc_td9.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td9.BorderWidth = 0;
                            encabezado.AddCell(enc_td9);
                            PdfPCell enc_td99 = new PdfPCell(new Phrase(int.Parse(dt.Rows[0]["total_km"].ToString()).ToString("#,##0"), _standardFont));
                            enc_td99.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td99.BorderWidth = 0;
                            encabezado.AddCell(enc_td99);

                            PdfPCell enc_td0 = new PdfPCell(new Phrase("Rendimiento:", _standardFont_bold));
                            enc_td0.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td0.BorderWidth = 0;
                            encabezado.AddCell(enc_td0);
                            PdfPCell enc_td00 = new PdfPCell(new Phrase(dt.Rows[0]["rendimiento"].ToString(), _standardFont));
                            enc_td00.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td00.BorderWidth = 0;
                            encabezado.AddCell(enc_td00);
                            //
                            PdfPCell enc_td7 = new PdfPCell(new Phrase("Fondo por Rendir:", _standardFont_bold));
                            enc_td7.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td7.BorderWidth = 0;
                            encabezado.AddCell(enc_td7);
                            PdfPCell enc_td77 = new PdfPCell(new Phrase("$ " + FondoPorRendir.ToString("#,##0"), _standardFont));
                            enc_td77.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td77.BorderWidth = 0;
                            encabezado.AddCell(enc_td77);

                            PdfPCell enc_tdB = new PdfPCell(new Phrase("Viáticos:", _standardFont_bold));
                            enc_tdB.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_tdB.BorderWidth = 0;
                            encabezado.AddCell(enc_tdB);
                            PdfPCell enc_tdBV = new PdfPCell(new Phrase("$ " + Viatico.ToString("#,##0"), _standardFont));
                            enc_tdBV.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_tdBV.BorderWidth = 0;
                            encabezado.AddCell(enc_tdBV);
                            //                          

                            PdfPCell enc_td8 = new PdfPCell(new Phrase("Saldo Fondo x Rendir:", _standardFont_bold));
                            enc_td8.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td8.BorderWidth = 0;
                            encabezado.AddCell(enc_td8);
                            PdfPCell enc_td88 = new PdfPCell(new Phrase("$ " + SaldoFondoPorRendir.ToString("#,##0"), _standardFont));
                            enc_td88.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td88.BorderWidth = 0;
                            encabezado.AddCell(enc_td88);
                          


                            PdfPCell enc_td_saldoviatico = new PdfPCell(new Phrase("Saldo Viatico:", _standardFont_bold));
                            enc_td_saldoviatico.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td_saldoviatico.BorderWidth = 0;
                            encabezado.AddCell(enc_td_saldoviatico);
                            PdfPCell enc_td_saldoviatico2 = new PdfPCell(new Phrase("$ " + SaldoViatico.ToString("#,##0"), _standardFont));
                            enc_td_saldoviatico2.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td_saldoviatico2.BorderWidth = 0;
                            encabezado.AddCell(enc_td_saldoviatico2);


                            PdfPCell enc_tdA2 = new PdfPCell(new Phrase("Dinero Devuelto:", _standardFont_bold));
                            enc_tdA2.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_tdA2.BorderWidth = 0;
                            encabezado.AddCell(enc_tdA2);
                            PdfPCell enc_tdAA2 = new PdfPCell(new Phrase("$ " + int.Parse(dt.Rows[0]["dinero_devuelto"].ToString()).ToString("#,##0"), _standardFont));
                            enc_tdAA2.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_tdAA2.BorderWidth = 0;
                            encabezado.AddCell(enc_tdAA2);

                            PdfPCell enc_td111 = new PdfPCell(new Phrase("Estado:", _standardFont_bold));
                            enc_td111.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td111.BorderWidth = 0;
                            encabezado.AddCell(enc_td111);
                            PdfPCell enc_td1111 = new PdfPCell(new Phrase(dt.Rows[0]["entregado"].ToString(), _standardFont));
                            enc_td1111.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_td1111.BorderWidth = 0;
                            encabezado.AddCell(enc_td1111);


                            PdfPCell enc_tdF = new PdfPCell(new Phrase("Observación:", _standardFont_bold));
                            enc_tdF.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_tdF.BorderWidth = 0;
                            encabezado.AddCell(enc_tdF);
                            PdfPCell enc_tdFF = new PdfPCell(new Phrase(dt.Rows[0]["observacion"].ToString(), _standardFont));
                            enc_tdFF.HorizontalAlignment = Element.ALIGN_LEFT;
                            enc_tdFF.BorderWidth = 0;
                            encabezado.AddCell(enc_tdFF);

                           
                            doc.Add(encabezado);
                            doc.Add(espacio);
                            // OTZ       
                            // **************************************************************************************     
                            PdfPTable TIT_OTZ = new PdfPTable(1);
                            TIT_OTZ.WidthPercentage = 100;
                            PdfPCell tittd = new PdfPCell(new Phrase("DETALLE DE FLETES (OTZ)", _standardFont_bold));
                            tittd.HorizontalAlignment = Element.ALIGN_LEFT;
                            tittd.BorderWidth = 0;
                            TIT_OTZ.AddCell(tittd);
                            doc.Add(TIT_OTZ);
                            // DETALLE
                            PdfPTable tablaOTZ = new PdfPTable(8);
                            tablaOTZ.WidthPercentage = 100;

                            PdfPCell otz_td1 = new PdfPCell(new Phrase("Otz", _standardFont_bold));
                            otz_td1.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaOTZ.AddCell(otz_td1);
                            PdfPCell otz_td2 = new PdfPCell(new Phrase("Origen", _standardFont_bold));
                            otz_td2.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaOTZ.AddCell(otz_td2);
                            PdfPCell otz_td3 = new PdfPCell(new Phrase("Destino", _standardFont_bold));
                            otz_td3.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaOTZ.AddCell(otz_td3);
                            PdfPCell otz_td4 = new PdfPCell(new Phrase("Cliente", _standardFont_bold));
                            otz_td4.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaOTZ.AddCell(otz_td4);
                            PdfPCell otz_td5 = new PdfPCell(new Phrase("Guia", _standardFont_bold));
                            otz_td5.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaOTZ.AddCell(otz_td5);
                            PdfPCell otz_td6 = new PdfPCell(new Phrase("F. Inicio", _standardFont_bold));
                            otz_td6.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaOTZ.AddCell(otz_td6);
                            PdfPCell otz_td7 = new PdfPCell(new Phrase("F. Termino", _standardFont_bold));
                            otz_td7.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaOTZ.AddCell(otz_td7);
                            PdfPCell otz_td8 = new PdfPCell(new Phrase("Valor", _standardFont_bold));
                            otz_td8.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaOTZ.AddCell(otz_td8);

                            DataTable dt_otz = new DataTable();
                            dt_otz = FN_ENC_OTZ.LLENADTVISTA(" where id_gt = " + id_gt);
                            if (dt_otz.Rows.Count > 0)
                            {
                                int suma = 0;
                                foreach (DataRow dr_otz in dt_otz.Rows)
                                {
                                    PdfPCell td_otz1 = new PdfPCell(new Phrase(dr_otz["CORRELATIVO_OTZ"].ToString(), _standardFont_bold));
                                    td_otz1.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tablaOTZ.AddCell(td_otz1);
                                    PdfPCell td_otz2 = new PdfPCell(new Phrase(dr_otz["C_ORIGEN"].ToString(), _standardFont));
                                    td_otz2.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tablaOTZ.AddCell(td_otz2);
                                    PdfPCell td_otz3 = new PdfPCell(new Phrase(dr_otz["C_DESTINO"].ToString(), _standardFont));
                                    td_otz3.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tablaOTZ.AddCell(td_otz3);
                                    PdfPCell td_otz4 = new PdfPCell(new Phrase(dr_otz["NOMBRE_CLIENTE"].ToString(), _standardFont));
                                    td_otz4.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tablaOTZ.AddCell(td_otz4);
                                    PdfPCell td_otz5 = new PdfPCell(new Phrase(dr_otz["GUIA"].ToString(), _standardFont));
                                    td_otz5.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tablaOTZ.AddCell(td_otz5);
                                    PdfPCell td_otz6 = new PdfPCell(new Phrase(dr_otz["F_INICIO"].ToString(), _standardFont));
                                    td_otz6.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tablaOTZ.AddCell(td_otz6);
                                    PdfPCell td_otz7 = new PdfPCell(new Phrase(dr_otz["F_FINAL"].ToString(), _standardFont));
                                    td_otz7.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tablaOTZ.AddCell(td_otz7);
                                    PdfPCell td_otz8 = new PdfPCell(new Phrase("$ " + int.Parse(dr_otz["SUMA_OTZ"].ToString()).ToString("#,##0"), _standardFont));
                                    td_otz8.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    tablaOTZ.AddCell(td_otz8);

                                    suma += int.Parse(dr_otz["SUMA_OTZ"].ToString());
                                }
                                PdfPCell td_suma1 = new PdfPCell(new Phrase("Total Flete: ", subtitulo_font));
                                td_suma1.HorizontalAlignment = Element.ALIGN_RIGHT;
                                td_suma1.BorderWidth = 0;
                                td_suma1.Colspan = 7;
                                tablaOTZ.AddCell(td_suma1);

                                PdfPCell td_suma = new PdfPCell(new Phrase("$ " + suma.ToString("#,##0"), subtitulo_font));
                                td_suma.HorizontalAlignment = Element.ALIGN_RIGHT;
                                tablaOTZ.AddCell(td_suma);
                            }

                            doc.Add(tablaOTZ);
                            doc.Add(espacio);
                            // COMBUSTIBLE       
                            // **************************************************************************************  
                            PdfPTable TIT_COM = new PdfPTable(1);
                            TIT_COM.WidthPercentage = 100;
                            PdfPCell comtd = new PdfPCell(new Phrase("DETALLE CARGA DE COMBUSTIBLE", _standardFont_bold));
                            comtd.HorizontalAlignment = Element.ALIGN_LEFT;
                            comtd.BorderWidth = 0;
                            TIT_COM.AddCell(comtd);
                            doc.Add(TIT_COM);
                            // DETALLE

                            PdfPTable tablaCombustible = new PdfPTable(9);
                            tablaCombustible.WidthPercentage = 100;

                            PdfPCell comb_td1 = new PdfPCell(new Phrase("Estación", _standardFont_bold));
                            comb_td1.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaCombustible.AddCell(comb_td1);
                            PdfPCell comb_td2 = new PdfPCell(new Phrase("Fecha", _standardFont_bold));
                            comb_td2.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaCombustible.AddCell(comb_td2);
                            PdfPCell comb_td3 = new PdfPCell(new Phrase("Guia", _standardFont_bold));
                            comb_td3.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaCombustible.AddCell(comb_td3);
                            PdfPCell comb_td4 = new PdfPCell(new Phrase("Rollo", _standardFont_bold));
                            comb_td4.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaCombustible.AddCell(comb_td4);
                            PdfPCell comb_td5 = new PdfPCell(new Phrase("Kms.", _standardFont_bold));
                            comb_td5.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaCombustible.AddCell(comb_td5);
                            PdfPCell comb_td7 = new PdfPCell(new Phrase("Precio", _standardFont_bold));
                            comb_td7.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaCombustible.AddCell(comb_td7);
                            PdfPCell comb_td6 = new PdfPCell(new Phrase("Litros", _standardFont_bold));
                            comb_td6.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaCombustible.AddCell(comb_td6);
                            PdfPCell comb_td8 = new PdfPCell(new Phrase("Kms Rec.", _standardFont_bold));
                            comb_td8.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaCombustible.AddCell(comb_td8);
                            PdfPCell comb_td10 = new PdfPCell(new Phrase("Rend.", _standardFont_bold));
                            comb_td10.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaCombustible.AddCell(comb_td10);

                            DataTable dt_combustible = new DataTable();
                            dt_combustible = FN_CARGA_COMBUSTIBLE.LLENADTVISTA(" where id_gt = " + id_gt);
                            if (dt_combustible.Rows.Count > 0)
                            {
                                int suma = 0;
                                bool primero = true;
                                double kms_rec = 0;
                                for (int i = 0; i < dt_combustible.Rows.Count; i++)
                                {
                                    DataRow dr_combustible = dt_combustible.Rows[i];
                                    PdfPCell td_otz1 = new PdfPCell(new Phrase(dr_combustible["NOMBRE_ESTACION"].ToString(), _standardFont));
                                    td_otz1.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tablaCombustible.AddCell(td_otz1);
                                    PdfPCell td_otz2 = new PdfPCell(new Phrase(DateTime.Parse(dr_combustible["FECHA"].ToString()).ToString("dd/MM/yyyy"), _standardFont));
                                    td_otz2.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tablaCombustible.AddCell(td_otz2);
                                    PdfPCell td_otz3 = new PdfPCell(new Phrase(dr_combustible["GUIA"].ToString(), _standardFont));
                                    td_otz3.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tablaCombustible.AddCell(td_otz3);
                                    PdfPCell td_otz4 = new PdfPCell(new Phrase(dr_combustible["ROLLO"].ToString(), _standardFont));
                                    td_otz4.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tablaCombustible.AddCell(td_otz4);
                                    PdfPCell td_otz5 = new PdfPCell(new Phrase(int.Parse(dr_combustible["KILOMETRAJE"].ToString()).ToString("#,##0"), _standardFont));
                                    td_otz5.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tablaCombustible.AddCell(td_otz5);
                                    PdfPCell td_otz8 = new PdfPCell(new Phrase("$ " + int.Parse(dr_combustible["PRECIO"].ToString()).ToString("#,##0"), _standardFont));
                                    td_otz8.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    tablaCombustible.AddCell(td_otz8);

                                    if (primero)
                                    {
                                        kms_rec = double.Parse(dr_combustible["KILOMETRAJE"].ToString()) - aux_km_inicial;
                                        primero = false;
                                    }
                                    else
                                    {
                                        DataRow dr_combustible2 = dt_combustible.Rows[i - 1];
                                        kms_rec = double.Parse(dr_combustible["KILOMETRAJE"].ToString()) - double.Parse(dr_combustible2["KILOMETRAJE"].ToString());
                                    }

                                    double rendimiento = kms_rec / double.Parse(dr_combustible["LITROS_CARGADOS"].ToString());

                                    PdfPCell td_otz6 = new PdfPCell(new Phrase(dr_combustible["LITROS_CARGADOS"].ToString(), _standardFont));
                                    td_otz6.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tablaCombustible.AddCell(td_otz6);

                                    PdfPCell td_otz9 = new PdfPCell(new Phrase(kms_rec.ToString(), _standardFont));
                                    td_otz9.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tablaCombustible.AddCell(td_otz9);

                                    PdfPCell td_otz10 = new PdfPCell(new Phrase(rendimiento.ToString("0.##"), _standardFont));
                                    td_otz10.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tablaCombustible.AddCell(td_otz10);


                                    suma += int.Parse(dr_combustible["PRECIO"].ToString());
                                }
                                PdfPCell td_suma1 = new PdfPCell(new Phrase("Total Combustible: ", subtitulo_font));
                                td_suma1.HorizontalAlignment = Element.ALIGN_RIGHT;
                                td_suma1.BorderWidth = 0;
                                td_suma1.Colspan = 5;
                                tablaCombustible.AddCell(td_suma1);

                                PdfPCell td_suma = new PdfPCell(new Phrase("$ " + suma.ToString("#,##0"), subtitulo_font));
                                td_suma.HorizontalAlignment = Element.ALIGN_RIGHT;
                                tablaCombustible.AddCell(td_suma);

                                PdfPCell td_suma2 = new PdfPCell(new Phrase(" ", subtitulo_font));
                                td_suma2.HorizontalAlignment = Element.ALIGN_RIGHT;
                                td_suma2.BorderWidth = 0;
                                td_suma2.Colspan = 3;
                                tablaCombustible.AddCell(td_suma2);

                            }

                            doc.Add(tablaCombustible);
                            doc.Add(espacio);
                            // GASTOS GENERALES       
                            // **************************************************************************************       
                            PdfPTable TIT_GASTO = new PdfPTable(1);
                            TIT_GASTO.WidthPercentage = 100;
                            PdfPCell gastotd = new PdfPCell(new Phrase("DETALLE DE GASTOS GENERALES", _standardFont_bold));
                            gastotd.HorizontalAlignment = Element.ALIGN_LEFT;
                            gastotd.BorderWidth = 0;
                            TIT_GASTO.AddCell(gastotd);
                            doc.Add(TIT_GASTO);
                            // DETALLE

                            PdfPTable tablaGastoGeneral = new PdfPTable(3);
                            tablaGastoGeneral.WidthPercentage = 100;

                            PdfPCell gasto_td1 = new PdfPCell(new Phrase("Tipo Gasto", _standardFont_bold));
                            gasto_td1.HorizontalAlignment = Element.ALIGN_LEFT;
                            tablaGastoGeneral.AddCell(gasto_td1);
                            PdfPCell gasto_td2 = new PdfPCell(new Phrase("Detalle", _standardFont_bold));
                            gasto_td2.HorizontalAlignment = Element.ALIGN_LEFT;
                            tablaGastoGeneral.AddCell(gasto_td2);
                            PdfPCell gasto_td3 = new PdfPCell(new Phrase("Valor", _standardFont_bold));
                            gasto_td3.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaGastoGeneral.AddCell(gasto_td3);

                            DataTable dt_gasto = new DataTable();
                            dt_gasto = FN_GASTO_GENERAL.LLENADTVISTA(" where id_gt = " + id_gt);
                            if (dt_gasto.Rows.Count > 0)
                            {
                                int suma = 0;
                                foreach (DataRow dr_combustible in dt_gasto.Rows)
                                {
                                    PdfPCell td_otz1 = new PdfPCell(new Phrase(dr_combustible["NOMBRE_TIPO_GASTO"].ToString(), _standardFont));
                                    td_otz1.HorizontalAlignment = Element.ALIGN_LEFT;
                                    tablaGastoGeneral.AddCell(td_otz1);
                                    PdfPCell td_otz2 = new PdfPCell(new Phrase(dr_combustible["DETALLE"].ToString(), _standardFont));
                                    td_otz2.HorizontalAlignment = Element.ALIGN_LEFT;
                                    tablaGastoGeneral.AddCell(td_otz2);
                                    PdfPCell td_otz8 = new PdfPCell(new Phrase("$ " + int.Parse(dr_combustible["VALOR"].ToString()).ToString("#,##0"), _standardFont));
                                    td_otz8.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    tablaGastoGeneral.AddCell(td_otz8);

                                    suma += int.Parse(dr_combustible["VALOR"].ToString());
                                }

                                PdfPCell td_suma1 = new PdfPCell(new Phrase("Total Gastos: ", subtitulo_font));
                                td_suma1.HorizontalAlignment = Element.ALIGN_RIGHT;
                                td_suma1.BorderWidth = 0;
                                td_suma1.Colspan = 2;
                                tablaGastoGeneral.AddCell(td_suma1);

                                PdfPCell td_suma = new PdfPCell(new Phrase("$ " + suma.ToString("#,##0"), subtitulo_font));
                                td_suma.HorizontalAlignment = Element.ALIGN_RIGHT;
                                tablaGastoGeneral.AddCell(td_suma);
                            }
                            doc.Add(tablaGastoGeneral);
                            doc.Add(espacio);

                            // RESUMEN FINAL
                            PdfPTable tablafinal = new PdfPTable(1);
                            tablafinal.WidthPercentage = 100;

                            PdfPCell final_td1 = new PdfPCell(new Phrase("Saldo Final: $ " + int.Parse(dt.Rows[0]["saldo_total"].ToString()).ToString("#,##0"), titulo_font));
                            final_td1.HorizontalAlignment = Element.ALIGN_RIGHT;
                            tablafinal.AddCell(final_td1);

                            doc.Add(tablafinal);

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
    }
}