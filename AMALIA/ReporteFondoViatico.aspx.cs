using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AMALIAFW;
using System.Data;
using System.Web.Services;


namespace AMALIA
{
    public partial class ReporteFondoViatico : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Reporte FXR VIATICO";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("Login.aspx");
                }


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "grid", "<script>javascript:Datatables();</script>", false);
            }
        }

        #region ---------------- NO CAMBIAR ---------------- 
        public void LIMPIARCAMPOS()
        {
            CleanControl(this.Controls);
        }

        public void CleanControl(ControlCollection controles)
        {
            foreach (Control control in controles)
            {
                if (control is TextBox)
                    ((TextBox)control).Text = string.Empty;
                else if (control is DropDownList)
                    ((DropDownList)control).ClearSelection();
                else if (control is RadioButtonList)
                    ((RadioButtonList)control).ClearSelection();
                else if (control is CheckBoxList)
                    ((CheckBoxList)control).ClearSelection();
                else if (control is RadioButton)
                    ((RadioButton)control).Checked = false;
                else if (control is CheckBox)
                    ((CheckBox)control).Checked = false;
                else if (control.HasControls())
                    CleanControl(control.Controls);
            }
        }

        protected void alert(string mensaje, int flag)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "mosnoti", "<script>javascript:MostrarNotificacion('" + mensaje + "', " + flag + ");</script>", false);
        }
        #endregion


        public string REPORTE()
        {
            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();
            DataTable dt_conductores = new DataTable();
            string html_tabla = "";

            dt_conductores = db.consultar("select * from conductor  where activo = 'ACTIVO';");
            html_tabla += "<table id='G_PRINCIPAL' class='table table-bordered tablaprincipal table-hover js-exportable'>";

            html_tabla += "<thead>";
            html_tabla += "<tr>";
            html_tabla += "<th>CONDUCTOR</th>";
            html_tabla += "<th>ULTIMA GT</th>";
            html_tabla += "<th>PATENTE GT</th>";
            html_tabla += "<th>ESTADO GT</th>";
            html_tabla += "<th>FXR</th>";
            html_tabla += "<th>VXR</th>";
            html_tabla += "<th>TOTAL</th>";
            html_tabla += "</tr>";
            html_tabla += "</thead>";

            html_tabla += "<tbody>";
            foreach (DataRow dr_conductor in dt_conductores.Rows)
            {
                string queryGt = @"select top(1)gt.num_correlativo, gt.id_conductor, cm.patente, egt.nombre_estado_gt, '" + dr_conductor["nombre_completo"].ToString() + "' as nombre_completo from enc_gt gt " +
                    " left join camion cm on gt.id_camion = cm.id_camion " +
                    " left join estados_gt egt on egt.id_estado_gt = gt.id_estado " +
                    " where gt.id_conductor = " + dr_conductor["id_conductor"].ToString() + " and gt.id_gt > 13155 " +
                    " order by id_gt desc";

                DataTable dt_gt = db.consultar(queryGt);


                foreach (DataRow dr_gt in dt_gt.Rows)
                {
                    var ObjOne = getFXRViatico(Convert.ToInt64(dr_gt["id_conductor"].ToString()));
                    var ObjTwo = getSaldosDescuentosNoDepositados(Convert.ToInt64(dr_gt["id_conductor"].ToString()));

                    long fxrFinal = ObjOne.Item1 + (ObjTwo.Item1 - ObjTwo.Item3);
                    long ViaticoFinal = ObjOne.Item2 + (ObjTwo.Item2 - ObjTwo.Item4);

                    html_tabla += "<tr>";
                    html_tabla += "<td>" + dr_gt["nombre_completo"].ToString() + "</td>";
                    html_tabla += "<td>" + dr_gt["num_correlativo"].ToString() + "</td>";
                    html_tabla += "<td>" + dr_gt["patente"].ToString() + "</td>";
                    html_tabla += "<td>" + dr_gt["nombre_estado_gt"].ToString() + "</td>";
                    html_tabla += "<td> $ " + fxrFinal.ToString("#,##0") + "</td>";
                    html_tabla += "<td> $ " + ViaticoFinal.ToString("#,##0") + "</td>";
                    html_tabla += "<td> $ " + (fxrFinal + ViaticoFinal).ToString("#,##0") + "</td>";
                }
                html_tabla += "</tr>";
            }
            html_tabla += "</tbody>";
            return html_tabla;
        }

        public Tuple<long, long> getFXRViatico(long id_conductor)
        {
            long sumaFXR = 0;
            long sumaViatico = 0;
            DBUtil db = new DBUtil();
            string query = "select SUM(X.fondoporrendir) as 'fondoporrendir', SUM(X.viatico) as 'viatico'  from " +
                         " (" +
                         " select  " +
                         "  gt.id_conductor, gt.id_gt " +
                         " , (select isnull(SUM(valor), 0) from deposito_detalle where num_viaje = gt.num_correlativo and tipo = 'FONDO POR RENDIR' and estado = 'DEPOSITADO') as 'fondoporrendir' " +
                         " , (select isnull(SUM(valor), 0) from deposito_detalle where num_viaje = gt.num_correlativo and tipo = 'VIATICO' and estado = 'DEPOSITADO') as 'viatico' " +
                         " , (SELECT ISNULL(SUM(VALOR),0) FROM GASTO_GENERAL WHERE TIPO_GASTO = 2 AND ID_GT = gt.id_gt) as 'gastoviatico'  " +
                         " , (SELECT ISNULL(SUM(VALOR),0) FROM GASTO_GENERAL WHERE TIPO_GASTO <> 2 AND ID_GT = gt.id_gt) as 'gastosgenerales' " +
                         " FROM enc_gt gt where gt.id_gt > 13155  " +
                         " ) as X where 1=1 and X.id_conductor = " + id_conductor +
                         " and (X.gastosgenerales + X.gastoviatico) = 0 group by X.fondoporrendir, X.viatico  ";

            DataTable dt = new DataTable();
            dt = db.consultar(query);
            foreach (DataRow dr in dt.Rows)
            {
                long fxr = Convert.ToInt64(dr["fondoporrendir"].ToString());
                long viatico = Convert.ToInt64(dr["viatico"].ToString());
                sumaFXR += fxr;
                sumaViatico += viatico;
            }
            return Tuple.Create(sumaFXR, sumaViatico);
        }

        public Tuple<long, long, long, long> getSaldosDescuentosNoDepositados(long id_conductor)
        {
            long sumadescuentoFxr = 0;
            long sumadescuentoViatico = 0;
            long sumaSaldoFxr = 0;
            long sumaSaldoViatico = 0;

            DBUtil db = new DBUtil();
            string query = "select X.* from " +
                         " (" +
                         "  select  " +
                         "  gt.id_conductor " +
                        " , (select isnull(SUM(valor), 0) from deposito_detalle where  id_conductor = " + id_conductor + " and tipo = 'DESCUENTO FONDO POR RENDIR' and estado = 'NO DEPOSITADO') as 'descuentofxr' " +
                        " , (select isnull(SUM(valor), 0) from deposito_detalle where  id_conductor = " + id_conductor + " and tipo = 'DESCUENTO VIATICO' and estado = 'NO DEPOSITADO') as 'descuentoviatico' " +
                        " , (select isnull(SUM(valor), 0) from deposito_detalle where  id_conductor = " + id_conductor + " and tipo = 'SALDO FONDO POR RENDIR' and estado = 'NO DEPOSITADO') as 'saldofxr' " +
                        " , (select isnull(SUM(valor), 0) from deposito_detalle where  id_conductor = " + id_conductor + " and tipo = 'SALDO VIATICO' and estado = 'NO DEPOSITADO') as 'saldoviatico' " +
                         " FROM enc_gt gt  where gt.id_gt > 13155 " +
                         " ) as X where 1=1 and X.id_conductor = " + id_conductor;


            DataTable dt = new DataTable();
            dt = db.consultar(query);
            foreach (DataRow dr in dt.Rows)
            {
                long descuentoFxr = Convert.ToInt64(dr["descuentofxr"].ToString());
                long descuentoViatico = Convert.ToInt64(dr["descuentoviatico"].ToString());
                long SaldoFxr = Convert.ToInt64(dr["saldofxr"].ToString());
                long SaldoViatico = Convert.ToInt64(dr["saldoviatico"].ToString());

                sumadescuentoFxr += descuentoFxr;
                sumadescuentoViatico += descuentoViatico;
                sumaSaldoFxr += SaldoFxr;
                sumaSaldoViatico += SaldoViatico;
            }

            return Tuple.Create(sumadescuentoFxr, sumadescuentoViatico, sumaSaldoFxr, sumaSaldoViatico);
        }        

        protected void B_FILTRAR_Click(object sender, EventArgs e)
        {
            DIV_TABLA.InnerHtml = REPORTE();
        }
    }
}