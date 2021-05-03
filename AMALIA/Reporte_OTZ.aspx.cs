using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AMALIAFW;
using System.Data;
using System.Web.Services;
using SpreadsheetLight;


namespace AMALIA
{
    public partial class Reporte_OTZ : System.Web.UI.Page
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

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "relojitofalse", "<script>javascript:relojito(false);</script>", false);
            }
        }

        protected void B_FILTRAR_Click(object sender, EventArgs e)
        {
            GeneraExcel("FECHA");
        }
        protected void B_FILTRAR2_Click(object sender, EventArgs e)
        {
            GeneraExcel("TODAS");
        }
        protected void B_FILTRAR_NUM_OTZ_Click(object sender, EventArgs e)
        {
            GeneraExcel("NUMOTZ");
        }
        public void GeneraExcel(string opcion)
        {
            bool todobien = true;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "relojitofalse", "<script>javascript:relojito(false);</script>", false);
            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();
            if (opcion == "FECHA")
            {
                if (FILTRA_FECHA_DESDE.Text != "" || FILTRA_FECHA_HASTA.Text != "")
                {
                    string desde = FILTRA_FECHA_DESDE.Text;
                    string hasta = FILTRA_FECHA_HASTA.Text;
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
                            hasta = " and fecha_inicio <= convert(date, '" + DateTime.Parse(hasta).ToString("dd/MM/yyyy") + "', 103) ";
                        }
                    }
                    catch (Exception ex)
                    {
                        hasta = "";
                    }
                    string filtro = desde + hasta;

                    dt = db.consultar("select * from V_OTZ_EXCEL where 1=1 " + filtro + "  order by OTZ desc");
                }
                else
                {
                    todobien = !todobien;
                    alert("Ingrese fechas para filtrar.", 0);
                }
              
            }
            else if (opcion == "NUMOTZ")
            {
                if (FILTRA_OTZ_DESDE.Text != "" || FILTRA_OTZ_HASTA.Text != "")
                {

                    string otz_desde = FILTRA_OTZ_DESDE.Text;
                    string otz_hasta = FILTRA_OTZ_HASTA.Text;
                    try
                    {
                        if (otz_desde != "")
                        {
                            otz_desde = " and OTZ >= " + otz_desde;
                        }
                    }
                    catch (Exception ex)
                    {
                        otz_desde = "";
                    }

                    try
                    {
                        if (otz_hasta != "")
                        {
                            otz_hasta = " and OTZ <= " + otz_hasta;
                        }
                    }
                    catch (Exception ex)
                    {
                        otz_hasta = "";
                    }

                    string filtro = otz_desde + otz_hasta;
                    dt = db.consultar("select * from V_OTZ_EXCEL where 1=1 " + filtro + " order by OTZ desc");
                }
                else
                {
                    todobien = !todobien;
                    alert("Ingrese los numeros de OTZ a filtrar.", 0);
                }
            }
            else
            {
                // TODAS
                dt = db.consultar("select * from V_OTZ_EXCEL order by OTZ desc");
            }
            if (todobien)
            {
                // EXCEL
                SLDocument sl = new SLDocument();

                SLStyle Negrita = sl.CreateStyle();
                Negrita.Font.Bold = true;

                int contador_fila = 1;
                int contador_columna = 1;
                int maximo_largo = dt.Columns.Count;

                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName != "fecha_inicio")
                    {
                        sl.SetCellValue(contador_fila, contador_columna, dc.ColumnName);
                        sl.SetCellStyle(contador_fila, contador_columna, Negrita);
                        contador_columna++;
                    }
                }
                contador_fila++;
                foreach (DataRow dr in dt.Rows)
                {
                    contador_columna = 1;
                    foreach (DataColumn dataColumn in dt.Columns)
                    {
                        if (dataColumn.ColumnName != "fecha_inicio")
                        {
                            if (dataColumn.ColumnName == "VALOR" || dataColumn.ColumnName == "ESTADIA" || dataColumn.ColumnName == "ENTRADA"
                                  || dataColumn.ColumnName == "DOBLE CONDUCTOR" || dataColumn.ColumnName == "CARGA DESCARGA" || dataColumn.ColumnName == "OTROS" || dataColumn.ColumnName == "FLETE TERCERO"
                                  || dataColumn.ColumnName == "DIFERENCIA FACTURA" || dataColumn.ColumnName == "TOTAL")
                            {
                                sl.SetCellValue(contador_fila, contador_columna, Convert.ToInt32(dr[dataColumn].ToString()));
                            }
                            else
                            {
                                sl.SetCellValue(contador_fila, contador_columna, dr[dataColumn].ToString());
                            }
                            contador_columna++;
                        }
                    }
                    contador_fila++;
                }

                // FIN EXCEL
                sl.AutoFitColumn(1, maximo_largo);
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment; filename=Reporte_OTZ.xlsx");
                sl.SaveAs(Response.OutputStream);
                Response.End();
            }
            
        }

        protected void alert(string mensaje, int flag)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "mosnoti", "<script>javascript:MostrarNotificacion('" + mensaje + "', " + flag + ");</script>", false);
        }
    }
}