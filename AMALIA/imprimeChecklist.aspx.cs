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

namespace CRM
{

    public partial class imprimeChecklist : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            #region NO CAMBIAR
            Document doc = new Document();
            DBUtil db = new DBUtil();
            OBJ_CHECKLISTS_COMPLETADOS lagt = new OBJ_CHECKLISTS_COMPLETADOS();
            OBJ_USUARIOS usuario = new OBJ_USUARIOS();
            usuario.usuario = HttpContext.Current.User.Identity.Name;
            FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);
            //
            string path = Server.MapPath("PDF_CHECKLIST");
            string id_gt = "";
            string sendmail = "";
            if (Request.QueryString["id_checklist_completado"] != null)
            {
                id_gt = Request.QueryString["id_checklist_completado"].ToString();
            }
            if (Request.QueryString["sendmail"] != null)
            {
                sendmail = Request.QueryString["sendmail"].ToString();
            }
            string nombre_pdf = "checklist_" + usuario.usuario + "_" + DateTime.Now.ToString("dd/MM/yyyy") + id_gt;
            string n_file = "/" + nombre_pdf.Replace(",", "").Replace(".", "").Replace(" ", "").Replace("/", "") + ".pdf";
            try
            {
                System.IO.File.Delete(path + n_file);
            }
            catch (System.IO.IOException ex)
            {

            }
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path + n_file, FileMode.Create));
            #endregion
            if (id_gt != "")
            {
                lagt.ID_CHECKLIST_COMPLETADO = int.Parse(id_gt);
                FN_CHECKLISTS_COMPLETADOS.LLENAOBJETO(ref lagt);
                if (lagt._respok)
                {
                    doc.SetPageSize(PageSize.A4);
                    doc.SetMargins(10f, 10f, 20f, 150f);
                    doc.AddTitle("Transportes Zamora");
                    doc.AddCreator("Transportes Zamora");
                    // FONTS
                    doc.Open();
                    CHECKLIST chk = new CHECKLIST();
                    doc = chk.ArmarChecklist(doc, lagt);
                    doc.Close();
                    writer.Close();

                    string pdfPath = Server.MapPath("~/PDF_CHECKLIST/" + n_file);

                    if (sendmail == "SI")
                    {
                        string html = "";
                        html += "<h4>Checklist Completado</h4>";
                        html += "<p>Conductor: " + lagt.nombre_conductor + " " + lagt.rut + "</p>";
                        html += "<p>Camión: " + lagt.patente_camion + ", Rampla: " + lagt.patente_rampla + "</p>";
                        html += "<p>Observación: " + lagt.observacion + "</p>";
                        html += "<hr>";
                        html += "<a href='https://app.transporteszamora.com/CHECKLISTS.aspx?id=" + lagt.ID_CHECKLIST_COMPLETADO + "'> <b>Click aquí para ver el Checklist </b> </a>";
                        html += "(Se adjunta PDF).";

                        Correo cr = new Correo();
                        List<string> correosZamora = new List<string>();
                        correosZamora.Add("Crojas@tzamora.cl");
                        correosZamora.Add("Coordinacion@tzamora.cl");
                        correosZamora.Add("xrsfirex@gmail.com");
                        correosZamora.Add("Laguirre@tzamora.cl");
                        cr.EnviarCorreo("tzamoracorreos@gmail.com", "Fzamora@tzamora.cl", "Checklist Completado por " + lagt.nombre_conductor + " Camion: " + lagt.patente_camion, html, correosZamora, pdfPath);
                    }
                    else
                    {
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
        }
    }
}