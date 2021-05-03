using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AMALIAFW;
using System.Data;
using System.Web.Services;
using System.Web.UI.HtmlControls;

namespace CRM
{

    public partial class AUT_OC : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                DBUtil db = new DBUtil();
                string idoc = "";
                if (Request.QueryString["idoc"] != null)
                {
                    idoc = Request.QueryString["idoc"].ToString();
                }
                if (idoc != "")
                {
                    OBJ_OC_ENC enc = new OBJ_OC_ENC();
                    enc.ID_OC = int.Parse(idoc);
                    FN_OC_ENC.LLENAOBJETO(ref enc);
                    if (enc._respok)
                    {
                        ID_DET.Text = idoc;
                        LBL_ENCABEZADO.InnerHtml = "<b> &nbsp;&nbsp;&nbsp; <i class='fa fa-file'></i> &nbsp; Orden Compra Nº " + enc.correlativo_oc.ToString() + "</b>";

                        DataTable dt_detalle = FN_OC_DETALLE.LLENADT(" where id_oc = " + enc.ID_OC);
                        OBJ_OC_PROVEEDORES prov = new OBJ_OC_PROVEEDORES();
                        prov.ID_OC_PROVEEDOR = enc.id_proveedor;
                        FN_OC_PROVEEDORES.LLENAOBJETO(ref prov);

                        //ESTADO_ACTUALMZ.InnerHtml = enc.aprobado_mz;
                        ESTADO_ACTUALFZ.InnerHtml = enc.aprobado_fz;
                        proveedor.Text = prov.razon_social;
                        solicitante.Text = enc.solicitante;
                        fechaoc.Text = enc.fecha_oc.ToString("dd/MM/yyyy");
                        plazoentrega.Text = enc.plazo_entrega.ToString("dd/MM/yyyy");

                        string tablahtml = "";
                        tablahtml += "<table style='font-size:12px; width:100% ' class='table table-sm'> <tr> <th>Glosa</th> <th>Neto</th>  <th>IVA</th> <th>Total</th></tr>";
                        foreach (DataRow dr in dt_detalle.Rows)
                        {
                            tablahtml += "<tr>";
                            tablahtml += "<td>" + dr["glosa"].ToString() + "</td>";
                            tablahtml += "<td>$" + double.Parse(dr["neto"].ToString()).ToString("#,##0") + "</td>";
                            tablahtml += "<td>$" + double.Parse(dr["iva"].ToString()).ToString("#,##0") + "</td>";
                            tablahtml += "<td>$" + double.Parse(dr["total"].ToString()).ToString("#,##0") + "</td>";
                            tablahtml += "</tr>";
                        }
                        tablahtml += "</table>";
                        TABLADET.InnerHtml = tablahtml;


                        // ADJUNTOS
                        string adj = "";
                        DataTable dt_adjuntos = FN_OC_ADJUNTOS.LLENADT(" where id_oc = " + enc.ID_OC);
                        foreach(DataRow dr in dt_adjuntos.Rows)
                        {
                            adj += "<a href='Documentos/OC/" + dr["id_oc"].ToString() + "/" + dr["nom_archivo"].ToString() + "' target='_blank'><i class='fa fa-download'></i> " + dr["nom_real"].ToString() + "</a><br>";
                        }
                        TABLAADJUNTOS.InnerHtml = adj;
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void B_AUTORIZAR_Click(object sender, EventArgs e)
        {
            if (T_CLAVE.Text == "0789")
            {
                OBJ_OC_ENC enc = new OBJ_OC_ENC();
                enc.ID_OC = int.Parse(ID_DET.Text);
                FN_OC_ENC.LLENAOBJETO(ref enc);

                //if (enc.aprobado_mz != "SI")
                //{
                //    RESPUESTA_CLAVE.InnerHtml = "MAURICIO ZAPATA AUN NO APRUEBA ESTA ORDEN DE COMPRA";
                //}
                //else
                //{
                    enc.aprobado_fz = "SI";
                    enc.obs_aprobacion = T_MOTIVORECHAZO.Text;
                    FN_OC_ENC.UPDATE(ref enc);
                    RESPUESTA.InnerHtml = "ORDEN DE COMPRA APROBADA CON ÉXITO <i class='fa fa-check text-success'></i>";
                    DIV2.Visible = true;
                    DIV1.Visible = false;
                //}
            }
            //else if (T_CLAVE.Text == "001122")
            //{
            //    OBJ_OC_ENC enc = new OBJ_OC_ENC();
            //    enc.ID_OC = int.Parse(ID_DET.Text);
            //    FN_OC_ENC.LLENAOBJETO(ref enc);
            //    enc.aprobado_mz = "SI";
            //    FN_OC_ENC.UPDATE(ref enc);
            //    RESPUESTA.InnerHtml = "ORDEN DE COMPRA APROBADA CON ÉXITO <i class='fa fa-check text-success'></i>";
            //    DIV2.Visible = true;
            //    DIV1.Visible = false;
            //}
            else
            {
                RESPUESTA_CLAVE.InnerHtml = "CLAVE INCORRECTA, REINTENTE";
            }

        }

        protected void B_RECHAZAR_Click(object sender, EventArgs e)
        {
            if (T_CLAVE.Text == "0789")
            {
                OBJ_OC_ENC enc = new OBJ_OC_ENC();
                enc.ID_OC = int.Parse(ID_DET.Text);
                FN_OC_ENC.LLENAOBJETO(ref enc);

                //if (enc.aprobado_mz != "SI")
                //{
                //    RESPUESTA_CLAVE.InnerHtml = "MAURICIO ZAPATA AUN NO APRUEBA ESTA ORDEN DE COMPRA";
                //}
                //else
                //{
                    enc.aprobado_fz = "RECHAZADA";
                    enc.obs_aprobacion = T_MOTIVORECHAZO.Text;
                    FN_OC_ENC.UPDATE(ref enc);
                    RESPUESTA.InnerHtml = "ORDEN DE COMPRA RECHAZADA <i class='fa fa-times text-danger'></i>";
                    DIV2.Visible = true;
                    DIV1.Visible = false;
                //}
            }
            //else if (T_CLAVE.Text == "001122")
            //{
            //    OBJ_OC_ENC enc = new OBJ_OC_ENC();
            //    enc.ID_OC = int.Parse(ID_DET.Text);
            //    FN_OC_ENC.LLENAOBJETO(ref enc);
            //    enc.aprobado_mz = "RECHAZADA";
            //    enc.obs_aprobacion = T_MOTIVORECHAZO.Text;
            //    FN_OC_ENC.UPDATE(ref enc);
            //    RESPUESTA.InnerHtml = "ORDEN DE COMPRA RECHAZADA <i class='fa fa-times text-danger'></i>";
            //    DIV2.Visible = true;
            //    DIV1.Visible = false;
            //}
            else
            {
                RESPUESTA_CLAVE.InnerHtml = "CLAVE INCORRECTA, REINTENTE";
            }
        }
    }
}