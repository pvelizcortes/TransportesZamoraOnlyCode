using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AMALIAFW;
using System.Data;
using System.Web;
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Net;
using Image = iTextSharp.text.Image;

namespace CRM
{

    public partial class Checklist : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {

                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void G_ADJUNTOS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //Borrar
                if (e.CommandName == "Borrar")
                {
                    int id = int.Parse((G_ADJUNTOS.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    OBJ_CHECKLISTS_IMAGENES img = new OBJ_CHECKLISTS_IMAGENES();
                    img.ID_IMAGEN = id;
                    FN_CHECKLISTS_IMAGENES.LLENAOBJETO(ref img);
                    if (img._respok)
                    {
                        FN_CHECKLISTS_IMAGENES.DELETE(ref img);
                        if (img._respok)
                        {
                            try
                            {
                                string ServerPath = HttpContext.Current.Server.MapPath("~").ToString();
                                System.IO.File.Delete(ServerPath + "Checklist/" + img.id_checklist_completado + "/" + img.nombreGuardado);
                            }
                            catch (System.IO.IOException ex)
                            {

                            }
                            LlenarGrillaAdjuntos(img.id_checklist_completado);
                            alert("Imagen Adjunta eliminado con éxito", 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                alert("No se pudo eliminar el adjunto", 0);
            }
        }


        public void LlenarGrillaAdjuntos(int id_checklist_completado)
        {
            try
            {
                G_ADJUNTOS.DataSource = FN_CHECKLISTS_IMAGENES.LLENADT(" where id_checklist_completado = " + id_checklist_completado);
                G_ADJUNTOS.DataBind();
            }
            catch (Exception ex)
            {

            }
        }


        protected void alert(string mensaje, int flag)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "mosnoti", "<script>javascript:MostrarNotificacion('" + mensaje + "', " + flag + ");</script>", false);
        }

        protected void b_guardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool editando = false;
                if (pNombreApellido.Text == "" || pPatenteCamion.Text == "" || pRut.Text == "" || pPatenteRampla.Text == "")
                {
                    alert("Por favor llene su informacion de conductor, si algun campo no corresponde escriba NO", 0);
                    pNombreApellido.Focus();
                }
                else
                {
                    OBJ_CHECKLISTS_COMPLETADOS chk = new OBJ_CHECKLISTS_COMPLETADOS();
                    List<OBJ_CHECKLISTS_RESPUESTAS> listResps = new List<OBJ_CHECKLISTS_RESPUESTAS>();

                    if (ID_CHECKLIST_COMPLETADO.Text != "")
                    {
                        // EDITAR
                        editando = true;
                        chk.ID_CHECKLIST_COMPLETADO = int.Parse(ID_CHECKLIST_COMPLETADO.Text);
                        FN_CHECKLISTS_COMPLETADOS.LLENAOBJETO(ref chk);
                        if (chk._respok)
                        {
                            chk.estado = "EDITADO";
                            chk.nombre_conductor = pNombreApellido.Text;
                            chk.rut = pRut.Text;
                            chk.patente_camion = pPatenteCamion.Text;
                            chk.patente_rampla = pPatenteRampla.Text;
                            chk.observacion = tObservacion.Text;
                            chk.nombre_inspeccion = pNombreInspeccion.Text;
                            chk.nombre_Proveedor = pNombreProveedor.Text;
                            FN_CHECKLISTS_COMPLETADOS.UPDATE(ref chk);
                            if (chk._respok)
                            {
                                DBUtil db = new DBUtil();
                                db.Scalar("delete from checklists_respuestas where id_checklist_completado = " + chk.ID_CHECKLIST_COMPLETADO.ToString());
                                listResps = LlenarRespuestas(chk);
                            }
                        }
                    }
                    else
                    {
                        // NUEVO
                        FN_CHECKLISTS_COMPLETADOS.PREPARAOBJETO(ref chk);
                        chk.fecha = DateTime.Now;
                        chk.id_checklist = int.Parse(ID_CHECKLIST.Text);
                        chk.estado = "NUEVO";
                        chk.nombre_conductor = pNombreApellido.Text;
                        chk.rut = pRut.Text;
                        chk.patente_camion = pPatenteCamion.Text;
                        chk.patente_rampla = pPatenteRampla.Text;
                        chk.observacion = tObservacion.Text;
                        chk.nombre_inspeccion = pNombreInspeccion.Text;
                        chk.nombre_Proveedor = pNombreProveedor.Text;
                        FN_CHECKLISTS_COMPLETADOS.INSERT(ref chk);
                        if (chk._respok)
                        {
                            ID_CHECKLIST_COMPLETADO.Text = chk.ID_CHECKLIST_COMPLETADO.ToString();
                            listResps = LlenarRespuestas(chk);
                        }
                    }
                    if (FN_CHECKLISTS_RESPUESTAS.INSERTLIST(listResps))
                    {
                        if (editando)
                        {
                            alert("Checklist editado con éxito", 1);

                        }
                        else
                        {
                            alert("Checklist creado con éxito, favor adjunte las imagenes de ser necesario", 1);
                            b_guardar.Text = "MODIFICAR CHECKLIST";
                            mensaje.Visible = false;


                        }

                    }
                    divSubirArchivo.Visible = true;
                    divFinalizar.Visible = true;
                    LlenarGrillaAdjuntos(chk.ID_CHECKLIST_COMPLETADO);
                }
            }
            catch (Exception ex)
            {
                alert("Ocurrio un error inesperado", 0);
            }
            ScriptManager.RegisterStartupScript(this, typeof(Page), "relojitofalse", "<script>javascript:relojito(false);</script>", false);

        }

        public List<OBJ_CHECKLISTS_RESPUESTAS> LlenarRespuestas(OBJ_CHECKLISTS_COMPLETADOS chk)
        {
            List<OBJ_CHECKLISTS_RESPUESTAS> listResps = new List<OBJ_CHECKLISTS_RESPUESTAS>();

            ID_CHECKLIST_COMPLETADO.Text = chk.ID_CHECKLIST_COMPLETADO.ToString();
            OBJ_CHECKLISTS_RESPUESTAS resp = new OBJ_CHECKLISTS_RESPUESTAS();
            resp.id_checklist_completado = chk.ID_CHECKLIST_COMPLETADO;
            resp.foto = "NO";
            resp.id_pregunta = 1;
            resp.respuesta = preg1.SelectedItem.Text;
            listResps.Add(resp);

            resp = new OBJ_CHECKLISTS_RESPUESTAS();
            resp.id_checklist_completado = chk.ID_CHECKLIST_COMPLETADO;
            resp.foto = "NO";
            resp.id_pregunta = 2;
            resp.respuesta = preg2.SelectedItem.Text;
            listResps.Add(resp);

            resp = new OBJ_CHECKLISTS_RESPUESTAS();
            resp.id_checklist_completado = chk.ID_CHECKLIST_COMPLETADO;
            resp.foto = "NO";
            resp.id_pregunta = 3;
            resp.respuesta = preg3.SelectedItem.Text;
            listResps.Add(resp);

            resp = new OBJ_CHECKLISTS_RESPUESTAS();
            resp.id_checklist_completado = chk.ID_CHECKLIST_COMPLETADO;
            resp.foto = "NO";
            resp.id_pregunta = 4;
            resp.respuesta = preg4.SelectedItem.Text;
            listResps.Add(resp);

            resp = new OBJ_CHECKLISTS_RESPUESTAS();
            resp.id_checklist_completado = chk.ID_CHECKLIST_COMPLETADO;
            resp.foto = "NO";
            resp.id_pregunta = 5;
            resp.respuesta = preg5.SelectedItem.Text;
            listResps.Add(resp);

            resp = new OBJ_CHECKLISTS_RESPUESTAS();
            resp.id_checklist_completado = chk.ID_CHECKLIST_COMPLETADO;
            resp.foto = "NO";
            resp.id_pregunta = 6;
            resp.respuesta = preg6.SelectedItem.Text;
            listResps.Add(resp);

            resp = new OBJ_CHECKLISTS_RESPUESTAS();
            resp.id_checklist_completado = chk.ID_CHECKLIST_COMPLETADO;
            resp.foto = "NO";
            resp.id_pregunta = 7;
            resp.respuesta = preg7.SelectedItem.Text;
            listResps.Add(resp);

            resp = new OBJ_CHECKLISTS_RESPUESTAS();
            resp.id_checklist_completado = chk.ID_CHECKLIST_COMPLETADO;
            resp.foto = "NO";
            resp.id_pregunta = 8;
            resp.respuesta = preg8.SelectedItem.Text;
            listResps.Add(resp);

            resp = new OBJ_CHECKLISTS_RESPUESTAS();
            resp.id_checklist_completado = chk.ID_CHECKLIST_COMPLETADO;
            resp.foto = "NO";
            resp.id_pregunta = 9;
            resp.respuesta = preg9.SelectedItem.Text;
            listResps.Add(resp);

            resp = new OBJ_CHECKLISTS_RESPUESTAS();
            resp.id_checklist_completado = chk.ID_CHECKLIST_COMPLETADO;
            resp.foto = "NO";
            resp.id_pregunta = 10;
            resp.respuesta = preg10.SelectedItem.Text;
            listResps.Add(resp);

            resp = new OBJ_CHECKLISTS_RESPUESTAS();
            resp.id_checklist_completado = chk.ID_CHECKLIST_COMPLETADO;
            resp.foto = "NO";
            resp.id_pregunta = 11;
            resp.respuesta = preg11.SelectedItem.Text;
            listResps.Add(resp);

            resp = new OBJ_CHECKLISTS_RESPUESTAS();
            resp.id_checklist_completado = chk.ID_CHECKLIST_COMPLETADO;
            resp.foto = "NO";
            resp.id_pregunta = 12;
            resp.respuesta = preg12.SelectedItem.Text;
            listResps.Add(resp);

            resp = new OBJ_CHECKLISTS_RESPUESTAS();
            resp.id_checklist_completado = chk.ID_CHECKLIST_COMPLETADO;
            resp.foto = "NO";
            resp.id_pregunta = 13;
            resp.respuesta = preg13.SelectedItem.Text;
            listResps.Add(resp);

            resp = new OBJ_CHECKLISTS_RESPUESTAS();
            resp.id_checklist_completado = chk.ID_CHECKLIST_COMPLETADO;
            resp.foto = "NO";
            resp.id_pregunta = 14;
            resp.respuesta = preg14.SelectedItem.Text;
            listResps.Add(resp);

            resp = new OBJ_CHECKLISTS_RESPUESTAS();
            resp.id_checklist_completado = chk.ID_CHECKLIST_COMPLETADO;
            resp.foto = "NO";
            resp.id_pregunta = 15;
            resp.respuesta = preg15.SelectedItem.Text;
            listResps.Add(resp);

            resp = new OBJ_CHECKLISTS_RESPUESTAS();
            resp.id_checklist_completado = chk.ID_CHECKLIST_COMPLETADO;
            resp.foto = "NO";
            resp.id_pregunta = 16;
            resp.respuesta = preg16.SelectedItem.Text;
            listResps.Add(resp);

            resp = new OBJ_CHECKLISTS_RESPUESTAS();
            resp.id_checklist_completado = chk.ID_CHECKLIST_COMPLETADO;
            resp.foto = "NO";
            resp.id_pregunta = 17;
            resp.respuesta = preg17.Text.Trim() == "" ? "0" : preg17.Text;
            listResps.Add(resp);

            resp = new OBJ_CHECKLISTS_RESPUESTAS();
            resp.id_checklist_completado = chk.ID_CHECKLIST_COMPLETADO;
            resp.foto = "NO";
            resp.id_pregunta = 18;
            resp.respuesta = preg18.Text.Trim() == "" ? "0" : preg18.Text;
            listResps.Add(resp);

            resp = new OBJ_CHECKLISTS_RESPUESTAS();
            resp.id_checklist_completado = chk.ID_CHECKLIST_COMPLETADO;
            resp.foto = "NO";
            resp.id_pregunta = 19;
            resp.respuesta = preg19.Text.Trim() == "" ? "0" : preg19.Text;
            listResps.Add(resp);

            resp = new OBJ_CHECKLISTS_RESPUESTAS();
            resp.id_checklist_completado = chk.ID_CHECKLIST_COMPLETADO;
            resp.foto = "NO";
            resp.id_pregunta = 20;
            resp.respuesta = preg20.Text.Trim() == "" ? "0" : preg20.Text;
            listResps.Add(resp);

            return listResps;
        }

        protected void B_CARGARADJUNTOS_Click(object sender, EventArgs e)
        {
            int id = int.Parse(ID_CHECKLIST_COMPLETADO.Text);
            LlenarGrillaAdjuntos(id);
        }

        protected void b_finalizar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(ID_CHECKLIST_COMPLETADO.Text);
            bool resp = ImprimirChecklist(id, "SI");
            if (resp)
            {
                Panel1.Visible = false;
                FINAL.Visible = true;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "relojitofalse2", "<script>javascript:relojito(false);</script>", false);
            }
        }


        public bool ImprimirChecklist(int id_gt, string sendmail)
        {
            #region No Cambiar
            Document doc = new Document();         
            DBUtil db = new DBUtil();
            OBJ_CHECKLISTS_COMPLETADOS lagt = new OBJ_CHECKLISTS_COMPLETADOS();
            OBJ_USUARIOS usuario = new OBJ_USUARIOS();  
            usuario.usuario = HttpContext.Current.User.Identity.Name;
            FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);        
            string path = Server.MapPath("PDF_CHECKLIST");
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
            if (id_gt > 0)
            {               
                lagt.ID_CHECKLIST_COMPLETADO = id_gt;
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
                    // ENVIAR POR CORREO????
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
            return true;
        }
    }


}