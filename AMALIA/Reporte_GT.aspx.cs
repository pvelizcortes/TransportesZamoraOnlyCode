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
    public partial class Reporte_GT : System.Web.UI.Page
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
                else
                {
                    OBJ_USUARIOS us = new OBJ_USUARIOS();
                    us.usuario = HttpContext.Current.User.Identity.Name;

                    CB_REPORTE.Items.Add(new ListItem("------------- SELECCIONE UN REPORTE ------------------", "-1"));
                    if (us.usuario == "felipe" || us.usuario == "festay" || us.usuario == "gestay")
                    {
                        CB_REPORTE.Items.Add(new ListItem("------------------------ FELIPE ------------------------", "-1"));
                        CB_REPORTE.Items.Add(new ListItem("GT - Detalle de Saldos (Cobrado y Por Cobrar)", "2"));
                        CB_REPORTE.Items.Add(new ListItem("GT - Rendimiento Camiones", "3"));                   
                    }
                    else if (us.usuario == "lacosta")
                    {
                        CB_REPORTE.Items.Add(new ListItem("------------------------ GT ------------------------", "-1"));
                        CB_REPORTE.Items.Add(new ListItem("GT - Diesel Interno", "7"));
                        CB_REPORTE.Items.Add(new ListItem("GT - Gastos de Transporte", "8"));
                    }
                    if (us.usuario == "festay" || us.usuario == "gestay")
                    {
                        CB_REPORTE.Items.Add(new ListItem("------------------------ OTZ'S  ------------------------", "-1"));
                        CB_REPORTE.Items.Add(new ListItem("OTZ - OTZ's por facturar", "5"));
                        CB_REPORTE.Items.Add(new ListItem("OTZ - Total Fletes (Por Facturar y Facturados)", "6"));
                        CB_REPORTE.Items.Add(new ListItem("------------------------ OTROS ------------------------", "-1"));
                        CB_REPORTE.Items.Add(new ListItem("GT - Diesel Interno", "7"));
                        CB_REPORTE.Items.Add(new ListItem("GT - Gastos de Transporte", "8"));
                    }
                    if (us.usuario == "dvander")
                    {
                        CB_REPORTE.Items.Add(new ListItem("GT - Gastos de Transporte", "8"));
                    }
                   
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
            string desde = FILTRA_FECHA_DESDE.Text;
            string hasta = FILTRA_FECHA_HASTA.Text;

            string gt_desde = T_FILTRA_GT.Text;
            string gt_hasta = T_FILTRA_GT2.Text;

            if (CB_REPORTE.SelectedValue == "2")
            {
                //GT - Detalle de Saldos (Cobrado y Por Cobrar)
                DIV_TABLA.InnerHtml = R_Dineroporentregar();
            }
            if (CB_REPORTE.SelectedValue == "3")
            {
                //GT - Conductores con mayores gastos
                DIV_TABLA.InnerHtml = R_RendimientoCamiones(desde, hasta);
            }
            if (CB_REPORTE.SelectedValue == "5")
            {
                // OTZ - OTZ's por facturar
                DIV_TABLA.InnerHtml = R_OTZ_POR_FACTURAR(desde, hasta);
            }
            if (CB_REPORTE.SelectedValue == "6")
            {
                // OTZ - Total Fletes (Por Facturar y Facturados)
                DIV_TABLA.InnerHtml = R_OTZ_POR_FACTURAR2(desde, hasta);
            }
            if (CB_REPORTE.SelectedValue == "7")
            {
                // GT - Diesel Interno
                DIV_TABLA.InnerHtml = R_DIESEL(gt_desde, gt_hasta);
            }
            if (CB_REPORTE.SelectedValue == "8")
            {
                // OTZ - Total Fletes (Por Facturar y Facturados)
                DIV_TABLA.InnerHtml = R_GASTOS_GENERALES(gt_desde, gt_hasta);
            }
            if (CB_REPORTE.SelectedValue == "10")
            {
                // OTZ - Total Fletes (Por Facturar y Facturados)
                DIV_TABLA.InnerHtml = R_GASTOS_GENERALES(gt_desde, gt_hasta);
            }
            ScriptManager.RegisterStartupScript(this, typeof(Page), "mosnoti", "<script>javascript:Datatables();</script>", false);

        }

        public string R_OTZ_POR_FACTURAR2(string desde, string hasta)
        {
            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();
            string html_tabla = "";

            try
            {
                if (desde != "")
                {
                    desde = " and fecha_inicio >= convert(date, '" + DateTime.Parse(desde).ToString("dd/MM/yyyy") + "', 103) ";
                }
            }
            catch (Exception ex)
            {
                desde = "";
            }
            try
            {
                if (hasta != "")
                {
                    hasta = " and fecha_final <= convert(date, '" + DateTime.Parse(hasta).ToString("dd/MM/yyyy") + "', 103) ";
                }
            }
            catch (Exception ex)
            {
                hasta = "";
            }
            string filtro = desde + hasta;

            int por_facturar = int.Parse(db.Scalar("select isnull(sum(suma_otz),0) from V_OTZ where d_factura = '' or d_factura = null " + filtro).ToString());
            int facturado = int.Parse(db.Scalar("select isnull(sum(suma_otz),0) from V_OTZ where d_factura != '' and d_factura is not null " + filtro).ToString());
            html_tabla += "<h2>TOTAL FLETES: $ " + (por_facturar + facturado).ToString("#,##0") + "</h2>";
            html_tabla += "<hr><h2>OTZ's POR FACTURAR: $ " + por_facturar.ToString("#,##0") + "</h2>";
            html_tabla += "<h2>OTZ's FACTURADAS: $ " + facturado.ToString("#,##0") + "</h2>";

            return html_tabla;
        }

        public string R_OTZ_POR_FACTURAR(string desde, string hasta)
        {
            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();
            string html_tabla = "";

            try
            {
                if (desde != "")
                {
                    desde = " and f_inicio >= convert(date, '" + DateTime.Parse(desde).ToString("dd/MM/yyyy") + "', 103) ";
                }
            }
            catch (Exception ex)
            {
                desde = "";
            }
            try
            {
                if (hasta != "")
                {
                    hasta = " and f_final <= convert(date, '" + DateTime.Parse(hasta).ToString("dd/MM/yyyy") + "', 103) ";
                }
            }
            catch (Exception ex)
            {
                hasta = "";
            }
            string filtro = desde + hasta;

            string query =
                "  " +
                " select * from V_OTZ where d_factura = '' or d_factura = null " + filtro;

            int total = int.Parse(db.Scalar("select sum(suma_otz) from V_OTZ where d_factura = '' or d_factura = null " + filtro).ToString());
            dt = db.consultar(query);

            if (dt.Rows.Count > 0)
            {
                html_tabla += "<h2>TOTAL OTZ's POR FACTURAR: $ " + total.ToString("#,##0") + "</h2>";
                html_tabla += "<hr><table id='G_PRINCIPAL' class='table table-bordered tablaprincipal table-hover js-exportable'>";
                html_tabla += "<thead>";
                html_tabla += "<tr>";
                html_tabla += "<th>GT</th>";
                html_tabla += "<th>ESTADO GT</th>";
                html_tabla += "<th>OTZ</th>";
                html_tabla += "<th>CLIENTE</th>";
                html_tabla += "<th>OBRA</th>";
                html_tabla += "<th>ORIGEN</th>";
                html_tabla += "<th>DESTINO</th>";
                html_tabla += "<th>FACTURA</th>";
                html_tabla += "<th>VALOR FLETE</th>";
                html_tabla += "<th>TOTAL</th>";
                html_tabla += "</tr>";
                html_tabla += "</thead>";

                html_tabla += "<tbody>";
                foreach (DataRow dr in dt.Rows)
                {
                    html_tabla += "<tr>";
                    html_tabla += "<td>" + dr["correlativo_gt"].ToString() + "</td>";
                    if (dr["id_estado"].ToString() == "1")
                    {
                        html_tabla += "<td><span class='badge badge-primary'>Abierta</span></td>";
                    }
                    else if (dr["id_estado"].ToString() == "2")
                    {
                        html_tabla += "<td><span class='badge badge-primary'>Viaje Terminado</span></td>";
                    }
                    else
                    {
                        html_tabla += "<td><span class='badge badge-primary'>Cerrada</span></td>";
                    }

                    html_tabla += "<td>" + dr["correlativo_otz"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["nombre_cliente"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["obra"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["c_origen"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["c_destino"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["d_factura"].ToString() + "</td>";
                    html_tabla += "<td style='color:green'><b> $ " + int.Parse(dr["valor_viaje"].ToString()).ToString("#,##0") + " </b></td>";
                    html_tabla += "<td style='color:green'><b> $ " + int.Parse(dr["suma_otz"].ToString()).ToString("#,##0") + " </b></td>";
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

        public string R_Dineroporentregar()
        {
            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();
            string html_tabla = "";

            string query =
                " select num_correlativo, id_estado, conductor1, patente_camion, total_km, dinero_entregado, sobre_deposito, total_gastos, saldo_dinero_entregado, entregado, dinero_devuelto from " +
                " V_TABLA_GT where entregado = 'Pendiente' ";

            dt = db.consultar(query);

            if (dt.Rows.Count > 0)
            {
                html_tabla += "<table id='G_PRINCIPAL' class='table table-bordered tablaprincipal table-hover js-exportable'>";
                html_tabla += "<thead>";
                html_tabla += "<tr>";
                html_tabla += "<th>GT</th>";
                html_tabla += "<th>ESTADO GT</th>";
                html_tabla += "<th>CONDUCTOR</th>";
                html_tabla += "<th>PATENTE</th>";
                html_tabla += "<th>TOTAL KMS.</th>";
                html_tabla += "<th>DINERO ENTREGADO</th>";
                html_tabla += "<th>SOBRE DEPOSITOO</th>";
                html_tabla += "<th>DINERO DEVUELTO</th>";
                html_tabla += "<th>TOTAL GASTOS</th>";
                html_tabla += "<th>SALDO</th>";
                html_tabla += "<th>ESTADO</th>";
                html_tabla += "</tr>";
                html_tabla += "</thead>";

                html_tabla += "<tbody>";
                foreach (DataRow dr in dt.Rows)
                {
                    html_tabla += "<tr>";
                    html_tabla += "<td>" + dr["num_correlativo"].ToString() + "</td>";
                    if (dr["id_estado"].ToString() == "1")
                    {
                        html_tabla += "<td><span class='badge badge-primary'>Abierta</span></td>";
                    }
                    else if (dr["id_estado"].ToString() == "2")
                    {
                        html_tabla += "<td><span class='badge badge-primary'>Viaje Terminado</span></td>";
                    }
                    else
                    {
                        html_tabla += "<td><span class='badge badge-primary'>Cerrada</span></td>";
                    }

                    html_tabla += "<td>" + dr["conductor1"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["patente_camion"].ToString() + "</td>";
                    html_tabla += "<td>" + int.Parse(dr["total_km"].ToString()).ToString("#,##0") + "</td>";
                    html_tabla += "<td style='color:green'>$ " + int.Parse(dr["dinero_entregado"].ToString()).ToString("#,##0") + "</td>";
                    html_tabla += "<td style='color:green'>$ " + int.Parse(dr["sobre_deposito"].ToString()).ToString("#,##0") + "</td>";
                    html_tabla += "<td style='color:green'>$ " + int.Parse(dr["dinero_devuelto"].ToString()).ToString("#,##0") + "</td>";
                    html_tabla += "<td style='color:red'>$ " + int.Parse(dr["total_gastos"].ToString()).ToString("#,##0") + "</td>";
                    try
                    {
                        int saldo = int.Parse(dr["saldo_dinero_entregado"].ToString());
                        if (saldo >= 0)
                        {
                            html_tabla += "<td style='color:green'><b>$ " + saldo.ToString("#,##0") + "</b></td>";
                        }
                        else
                        {
                            html_tabla += "<td style='color:red'><b>$ " + saldo.ToString("#,##0") + "</b></td>";
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    html_tabla += "<td><span class='badge badge-danger'>" + dr["entregado"].ToString() + "</span></td>";
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

        public string R_RendimientoCamiones(string desde, string hasta)
        {
            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();
            string html_tabla = "";

            try
            {
                if (desde != "")
                {
                    desde = " and fecha_inicio >= convert(date, '" + DateTime.Parse(desde).ToString("dd/MM/yyyy") + "', 103) ";
                }
            }
            catch (Exception ex)
            {
                desde = "";
            }
            try
            {
                if (hasta != "")
                {
                    hasta = " and fecha_termino <= convert(date, '" + DateTime.Parse(hasta).ToString("dd/MM/yyyy") + "', 103) ";
                }
            }
            catch (Exception ex)
            {
                hasta = "";
            }

            string filtro = desde + hasta;
            string query =

            " select distinct conductor.id_conductor, GT.id_camion, camion.patente, conductor.nombre_completo " +
            ", (select SUM(total_km) from enc_gt where id_camion = camion.id_camion and id_conductor = conductor.id_conductor " + filtro + ") as total_km " +
            ", (select SUM(total_litros) from enc_gt where id_camion = camion.id_camion and id_conductor = conductor.id_conductor " + filtro + ") as total_litros " +
            ", (select CONVERT(numeric(18, 2), (SUM(total_km) / NULLIF(SUM(TOTAL_LITROS), 0))) from enc_gt where id_camion = camion.id_camion and id_conductor = conductor.id_conductor " + filtro + ") as rendimiento " +
            " from enc_gt GT LEFT JOIN camion on camion.id_camion = GT.id_camion LEFT JOIN conductor on conductor.id_conductor = GT.id_conductor where total_km > 0 and total_km is not null " +
            " group by GT.id_camion, camion.id_camion, camion.patente, conductor.nombre_completo, conductor.id_conductor, total_km, total_litros, rendimiento " +
            " order by rendimiento desc, patente ";

            //" select * from " +
            //" (select cond.rut, cond.nombre_completo, " +
            //" (select count(1) from enc_gt where id_conductor = cond.id_conductor " + filtro + ") as viajes, " +
            //" (select SUM(saldo_dinero_entregado) from enc_gt where id_conductor = cond.id_conductor " + filtro + ") as suma, " +
            //" (select SUM(total_km) from enc_gt where id_conductor = cond.id_conductor " + filtro + ") as total_km, " +
            //" (select SUM(total_gastos) from enc_gt where id_conductor = cond.id_conductor " + filtro + ") as total_gastos, " +
            //" (select SUM(dinero_entregado) from enc_gt where id_conductor = cond.id_conductor " + filtro + ") as dinero_entregado " +
            //" from conductor cond) as x " +
            //" where x.viajes > 0 " +
            //" group by x.rut, x.nombre_completo, x.suma, x.viajes, x.total_km, x.total_gastos, x.dinero_entregado " +
            //" order by x.suma DESC";

            dt = db.consultar(query);
            if (dt.Rows.Count > 0)
            {
                html_tabla += "<table id='G_PRINCIPAL' class='table table-bordered tablaprincipal table-hover js-exportable'>";
                html_tabla += "<thead>";
                html_tabla += "<tr>";
                html_tabla += "<th>PATENTE</th>";
                html_tabla += "<th>CONDUCTOR</th>";
                html_tabla += "<th>KM RECORRIDOS</th>";
                html_tabla += "<th>LITROS CARGADOS</th>";
                html_tabla += "<th>RENDIMIENTO </th>";
                html_tabla += "</tr>";
                html_tabla += "</thead>";

                html_tabla += "<tbody>";
                foreach (DataRow dr in dt.Rows)
                {
                    html_tabla += "<tr>";
                    html_tabla += "<td>" + dr["PATENTE"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["nombre_completo"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["total_km"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["total_litros"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["rendimiento"].ToString() + "</td>";
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

        public string R_DIESEL(string gt_desde, string gt_hasta)
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

            string filtro = gt_desde + gt_hasta;
            string query = "select * from v_combustibles where 1=1 " + filtro;

            dt = db.consultar(query);
            if (dt.Rows.Count > 0)
            {
                html_tabla += "<table id='G_PRINCIPAL' class='table table-bordered tablaprincipal table-hover js-exportable'>";
                html_tabla += "<thead>";
                html_tabla += "<tr>";
                html_tabla += "<th>GT</th>";
                html_tabla += "<th>CONDUCTOR</th>";
                html_tabla += "<th>CAMIÓN</th>";
                html_tabla += "<th>ESTACIÓN</th>";
                html_tabla += "<th>FECHA</th>";
                html_tabla += "<th>GUIA</th>";
                html_tabla += "<th>ROLLO</th>";
                html_tabla += "<th>KM ODOMETRO</th>";
                html_tabla += "<th>LITROS</th>";
                html_tabla += "<th>PRECIO</th>";
                html_tabla += "</tr>";
                html_tabla += "</thead>";

                html_tabla += "<tbody>";
                foreach (DataRow dr in dt.Rows)
                {
                    html_tabla += "<tr>";
                    html_tabla += "<td>" + dr["num_correlativo"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["nombre_completo"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["PATENTE"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["nombre_estacion"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["fechaformat"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["guia"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["rollo"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["kilometraje"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["litros_cargados"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["precio"].ToString() + "</td>";
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

            string filtro = gt_desde + gt_hasta;
            string query = "select * from v_gasto_general where 1=1 " + filtro + " order by num_correlativo";

            dt = db.consultar(query);
            if (dt.Rows.Count > 0)
            {
                html_tabla += "<table id='G_PRINCIPAL' class='table table-bordered tablaprincipal table-hover js-exportable'>";
                html_tabla += "<thead>";
                html_tabla += "<tr>";
                html_tabla += "<th>GT</th>";
                html_tabla += "<th>CONDUCTOR</th>";
                html_tabla += "<th>CAMIÓN</th>";
                html_tabla += "<th>TIPO DE GASTO</th>";
                html_tabla += "<th>DETALLE</th>";
                html_tabla += "<th>($) VALOR</th>";
                html_tabla += "<th>($) DEVUELTO</th>";
                html_tabla += "</tr>";
                html_tabla += "</thead>";

                html_tabla += "<tbody>";
                foreach (DataRow dr in dt.Rows)
                {
                    html_tabla += "<tr>";
                    html_tabla += "<td>" + dr["num_correlativo"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["nombre_completo"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["PATENTE"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["nombre_tipo_gasto"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["detalle"].ToString() + "</td>";
                    html_tabla += "<td> $ " + Convert.ToInt64(dr["valor"].ToString()).ToString("#,##0") + "</td>";
                    html_tabla += "<td> $ " + Convert.ToInt64(dr["dinero_devuelto"].ToString()).ToString("#,##0") + "</td>";
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

        protected void CB_REPORTE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_REPORTE.SelectedValue == "2" || CB_REPORTE.SelectedValue == "3" || CB_REPORTE.SelectedValue == "5" || CB_REPORTE.SelectedValue == "6")
            {
                F_FECHAS.Visible = true;
                F_GT.Visible = false;
            }
            else
            {
                F_FECHAS.Visible = false;
                F_GT.Visible = true;
                if (CB_REPORTE.SelectedValue == "7")
                {
                    DIV_FILTRO_COMBUSTIBLE.Visible = true;
                }
            }
        }

        protected void B_FILTRAR2_Click(object sender, EventArgs e)
        {
            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();
            string html_tabla = "";

            int year = DateTime.Now.Year;
            DateTime firstDay = new DateTime(year, 1, 1);
            DateTime lastDay = new DateTime(year, 12, 31);

            string query = " select * from v_combustibles where id_gt in ( " +
                            " select id_gt from enc_gt where fecha_inicio >= convert(date, '" + firstDay.ToString("dd-MM-yyyy") + "', 103) " +
                            " and fecha_inicio <= convert(date, '" + lastDay.ToString("dd-MM-yyyy") + "', 103) " +
                            " ) order by num_correlativo; ";

            dt = db.consultar(query);
            if (dt.Rows.Count > 0)
            {
                html_tabla += "<table id='G_PRINCIPAL' class='table table-bordered tablaprincipal table-hover js-exportable'>";
                html_tabla += "<thead>";
                html_tabla += "<tr>";
                html_tabla += "<th>GT</th>";
                html_tabla += "<th>CONDUCTOR</th>";
                html_tabla += "<th>CAMIÓN</th>";
                html_tabla += "<th>ESTACIÓN</th>";
                html_tabla += "<th>FECHA</th>";
                html_tabla += "<th>GUIA</th>";
                html_tabla += "<th>ROLLO</th>";
                html_tabla += "<th>KM ODOMETRO</th>";
                html_tabla += "<th>LITROS</th>";
                html_tabla += "<th>PRECIO</th>";
                html_tabla += "</tr>";
                html_tabla += "</thead>";

                html_tabla += "<tbody>";
                foreach (DataRow dr in dt.Rows)
                {
                    html_tabla += "<tr>";
                    html_tabla += "<td>" + dr["num_correlativo"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["nombre_completo"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["PATENTE"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["nombre_estacion"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["fechaformat"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["guia"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["rollo"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["kilometraje"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["litros_cargados"].ToString() + "</td>";
                    html_tabla += "<td>" + dr["precio"].ToString() + "</td>";
                    html_tabla += "</tr>";
                }
                html_tabla += "</tbody>";
            }
            else
            {
                html_tabla = "<h4>No existen registros para los filtros seleccionados.</h4>";
            }

            DIV_TABLA.InnerHtml = html_tabla;
        }
    }
}