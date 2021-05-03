using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AMALIAFW;
using AMALIA;
using System.Web.Services;

namespace AMALIA
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                OBJ_USUARIOS us = new OBJ_USUARIOS();
                us.usuario = HttpContext.Current.User.Identity.Name;
                FN_USUARIOS.BUSCARCONUSUARIO(ref us);

                if (us.id_perfil == 1)
                {
                    DIV_DASH.Visible = true;
                }
                DBUtil db = new DBUtil();
                DataTable dt_resumen = new DataTable();     

                dt_resumen = db.consultar("select * from gt_dashboard (" + DateTime.Now.Month + ", " + DateTime.Now.Year + ");");
                LBL_ANO.InnerHtml = DateTime.Now.ToString("MM/yyyy");

                if (dt_resumen.Rows.Count > 0)
                {
                    if (dt_resumen.Rows[0]["CONTADOR"].ToString() != "0")
                    {
                        L_SALDO_TOTAL.InnerHtml = "$ " + int.Parse(dt_resumen.Rows[0]["SALDO_TOTAL"].ToString()).ToString("#,##0");
                        L_FLETE.InnerHtml = "+$ " + int.Parse(dt_resumen.Rows[0]["TOTAL_FLETE"].ToString()).ToString("#,##0");
                        L_COMBUSTIBLE.InnerHtml = "-$ " + int.Parse(dt_resumen.Rows[0]["TOTAL_PRECIO_COMBUSTIBLE"].ToString()).ToString("#,##0");
                        L_GASTO_GENERAL.InnerHtml = "-$ " + int.Parse(dt_resumen.Rows[0]["TOTAL_GASTOS"].ToString()).ToString("#,##0");
                        L_KM_RECORRIDOS.InnerHtml = "<div class='number count-to' data-from='0' data-to='" + dt_resumen.Rows[0]["TOTAL_KM"].ToString() + "' data-speed='2000' data-fresh-interval='1000'>" + dt_resumen.Rows[0]["TOTAL_KM"].ToString() + "</div>";
                        L_TOTAL_LITROS.InnerHtml = "<div class='number count-to' data-from='0' data-to='" + dt_resumen.Rows[0]["TOTAL_LITROS2"].ToString() + "' data-speed='2000' data-fresh-interval='1000'>" + dt_resumen.Rows[0]["TOTAL_LITROS2"].ToString() + "</div>";
                        try
                        {
                            string x = db.Scalar("select top(1) stock_estanque from comb_log order by id_log desc").ToString();
                            L_STOCK_ESTANQUE.InnerHtml = "<div class='number count-to' data-from='0' data-to='" + x + "' data-speed='2000' data-fresh-interval='1000'>" + x + "</div>";
                        }
                        catch(Exception ex)
                        {

                        }
                        

                    }                    
                }
            }
        }
        [WebMethod]
        public static string KeepAlive(string id_checklist)
        {
            HttpContext.Current.Session.Timeout = 6;
            HttpContext.Current.Session["keepalive"] = DateTime.Now.ToString();
            return DateTime.Now.ToString("HH:mm:ss");
        }
    }
}