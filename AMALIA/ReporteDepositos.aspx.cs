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
    public partial class ReporteDepositos : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Conductor";
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

        protected void B_FILTRAR_Click(object sender, EventArgs e)
        {
            
            string gt_desde = T_FILTRA_GT.Text;
            string gt_hasta = T_FILTRA_GT2.Text;

            if (gt_desde == "" || gt_hasta == "")
            {
                alert("Debe ingresar un Nº GT desde y hasta para generar el informe", 0);
            }
            else
            {
                DIV_TABLA.InnerHtml = R_GASTOS_GENERALES(gt_desde, gt_hasta);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "mosnoti", "<script>javascript:Datatables();</script>", false);
            }

          
        }
        
        public string R_GASTOS_GENERALES(string gt_desde, string gt_hasta)
        {
            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();
            string html_tabla = "";

            try
            {
                if (gt_desde != "")
                {
                    gt_desde = " and num_correlativo >= " + gt_desde;
                }
            }
            catch (Exception ex)
            {
                gt_desde = "";
            }

            try
            {
                if (gt_hasta != "")
                {
                    gt_hasta = " and num_correlativo <= " + gt_hasta;
                }
            }
            catch (Exception ex)
            {
                gt_hasta = "";
            }

            string filtro = gt_desde + gt_hasta;                                       // (Rendido-(descuento-saldo)) // Antigua Formula  (abs(X.dinero_entregado - abs(X.saldo_dinero_entregado) - X.totaldescuentos + X.totalsaldos) - X.dinero_entregado) 
            string query = "select X.*, (X.saldo_dinero_entregado-(X.totaldescuentos - X.totalsaldos)) as 'totalpendiente' from " +
                            " (" +
                            " select gt.num_correlativo, gt.saldo_dinero_entregado, convert(varchar,gt.fecha_inicio,103) as 'fecha_inicio', convert(varchar,gt.fecha_termino,103) as 'fecha_termino' " +
                            " , gt.dinero_entregado " +
                            " , gt.total_gastos " +
                            " , gt.id_camion " +
                            " , (select isnull(SUM(valor), 0) from deposito_detalle where num_viaje = gt.num_correlativo and tipo = 'DESCUENTO') as 'totaldescuentos' " +
                            " ,(select isnull(SUM(valor), 0) from deposito_detalle where num_viaje = gt.num_correlativo and tipo = 'SALDO') as 'totalsaldos' " +
                            " ,(select NOMBRE_COMPLETO from conductor where id_conductor = gt.id_conductor) as 'nombre_conductor' " +
                            " FROM enc_gt gt " +
                            " ) as X where 1=1 ";
            query += filtro;           

            dt = db.consultar(query);
            if (dt.Rows.Count > 0)
            {
                html_tabla += "<table id='G_PRINCIPAL' class='table table-bordered tablaprincipal table-hover js-exportable'>";
                html_tabla += "<thead>";
                html_tabla += "<tr>";
                html_tabla += "<th>GT</th>";
                html_tabla += "<th>NOMBRE CONDUCTOR</th>";
                html_tabla += "<th>FECHA INICIO</th>";
                html_tabla += "<th>FECHA TERMINO</th>";
                html_tabla += "<th>VALOR TOTAL</th>";
                html_tabla += "<th>RENDIDO</th>";
                html_tabla += "<th>DESCUENTO</th>";
                html_tabla += "<th>SALDO</th>";
                html_tabla += "<th>PENDIENTE</th>";
                html_tabla += "<th>ESTADO</th>";
                html_tabla += "</tr>";
                html_tabla += "</thead>";

                html_tabla += "<tbody>";
                foreach (DataRow dr in dt.Rows)
                {
                    long totalpendiente = Convert.ToInt64(dr["totalpendiente"].ToString());
                    long totalgastos = Convert.ToInt64(dr["saldo_dinero_entregado"].ToString());
                    long totalgastosgenerales = Convert.ToInt64(dr["total_gastos"].ToString());
                    long idCamion = Convert.ToInt64(dr["id_camion"].ToString());

                    html_tabla += "<tr>";
                    html_tabla += "<td>" + dr["num_correlativo"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["nombre_conductor"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["fecha_inicio"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["fecha_termino"].ToString() + "</td>";
                    html_tabla += "<td> $ " + Convert.ToInt64(dr["dinero_entregado"].ToString()).ToString("#,##0") + "</td>";
                    html_tabla += "<td> $ " + totalgastos.ToString("#,##0") + "</td>";
                    html_tabla += "<td> $ " + Convert.ToInt64(dr["totaldescuentos"].ToString()).ToString("#,##0") + "</td>";
                    html_tabla += "<td> $ " + Convert.ToInt64(dr["totalsaldos"].ToString()).ToString("#,##0") + "</td>";
                    html_tabla += "<td> $ " + totalpendiente.ToString("#,##0") + "</td>";
                    if (idCamion == 37)
                    {
                        html_tabla += "<td><span class='badge badge-success'>OK</span></td>";
                    }
                    else if (totalpendiente == 0)
                    {
                        html_tabla += "<td><span class='badge badge-success'>OK</span></td>";
                    }                 
                    else if (totalgastosgenerales == 0)
                    {
                        html_tabla += "<td><span class='badge badge-warning'>PENDIENTE</span></td>";
                    }                                 
                    else 
                    {
                        html_tabla += "<td><span class='badge badge-danger'>ERROR</span></td>";
                    }                
                    
                    html_tabla += "</tr>";
                }
                html_tabla += "</tbody>";
            }
            else
            {
                html_tabla = "<h4>No existen registros para los filtros seleccionados.</h4>";
            }

            return html_tabla;
        }
    }
}