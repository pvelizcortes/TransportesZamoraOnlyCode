using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AMALIAFW;
using AMALIA;

namespace AMALIA
{
    public partial class resumen_GT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                string id_gt = "";
                if (Request.QueryString["num_gt"] != null)
                {
                    id_gt = Request.QueryString["num_gt"].ToString();
                }

                try
                {
                    if (id_gt != "")
                    {
                        OBJ_ENC_GT GT_OBJ = new OBJ_ENC_GT();
                        GT_OBJ.ID_GT = int.Parse(id_gt);
                        FN_ENC_GT.LLENAOBJETO(ref GT_OBJ);
                        if (GT_OBJ._respok)
                        {
                            DataTable dt_vista = new DataTable();
                            dt_vista = FN_ENC_GT.LLENADTVISTA(" where id_gt = " + id_gt);
                            // LLENAR TODO
                            DIV_TITULO.InnerHtml = "<h2>Resumen Guía de Transporte Nº " + GT_OBJ.num_correlativo + "</h2>";
                            DIV_DINERO_ENTREGADO.InnerHtml = "$ " + (GT_OBJ.dinero_entregado + GT_OBJ.sobre_deposito).ToString("#,##0");
                            DIV_DINEROXENTREGAR.InnerHtml = "$ " + GT_OBJ.saldo_dinero_entregado.ToString("#,##0");
                            DIV_KM_RECORRIDOS.InnerHtml = GT_OBJ.total_km.ToString("#,##0");
                            DIV_LITROS_CARGADOS.InnerHtml = GT_OBJ.total_litros.ToString();
                            DIV_RENDIMIENTO.InnerHtml = GT_OBJ.rendimiento.ToString();
                            DIV_SALDO_FINAL.InnerHtml = "$ " + GT_OBJ.saldo_total.ToString("#,##0");
                            //
                            DIV_NOMBRE_CONDUCTOR.InnerHtml = dt_vista.Rows[0]["conductor1"].ToString();
                            DIV_OBSERVACION.InnerHtml = GT_OBJ.observacion;
                            DIV_CAMION.InnerHtml = dt_vista.Rows[0]["patente_camion"].ToString();
                            DIV_RAMPLA.InnerHtml = dt_vista.Rows[0]["patente_rampla"].ToString();
                            DIV_INICIO.InnerHtml = GT_OBJ.fecha_inicio.ToString("dd/MM/yyyy");
                            DIV_TERMINO.InnerHtml = GT_OBJ.fecha_termino.ToString("dd/MM/yyyy");
                            // 
                            DIV_TOTAL_FLETE.InnerHtml = "+$ " + GT_OBJ.total_flete.ToString("#,##0");
                            DIV_TOTAL_COMBUSTIBLE.InnerHtml = "-$ " + GT_OBJ.total_precio_combustible.ToString("#,##0");
                            DIV_TOTAL_GASTOS.InnerHtml = "-$ " + GT_OBJ.total_gastos.ToString("#,##0");
                        }
                        else
                        {

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}