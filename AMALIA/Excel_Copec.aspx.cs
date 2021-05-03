using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AMALIAFW;
using AMALIA;
using System.Data.OleDb;

namespace AMALIA
{
    public partial class Excel_Copec : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void B_CARGAR_EXCEL_Click1(object sender, EventArgs e)
        {
            try
            {
                if (SUBIR_EXCEL.HasFile)
                {
                    DBUtil db = new DBUtil();
                    string ServerPath = HttpContext.Current.Server.MapPath("~").ToString();
                    string subPath = ServerPath + "EXCEL_PARAM";
                    bool exists = System.IO.Directory.Exists(subPath);
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(subPath);
                    }
                    var fileSavePath = System.IO.Path.Combine(subPath, SUBIR_EXCEL.FileName);
                    SUBIR_EXCEL.SaveAs(fileSavePath);

                    //            
                    String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                   fileSavePath +
                                    ";Extended Properties='Excel 12.0 XML;HDR=YES;';";
                    //
                    OleDbConnection con = new OleDbConnection(constr);
                    OleDbCommand oconn = new OleDbCommand("Select * From [copec$]", con);
                    con.Open();

                    OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                    DataTable data = new DataTable();
                    sda.Fill(data);
                    con.Close();

                    //
                    db.Scalar("truncate table carga_combustible_temp");
                    int cont = 0;
                    string sql_insert = "";

                    foreach (DataRow dr in data.Rows)
                    {
                        try
                        {
                            if (cont > 0)
                            {
                                sql_insert += "" +
                                " insert into carga_combustible_temp " +
                                " values (" +
                                 " '" + dr[1].ToString() + "', " + // PATENTE
                                " '" + dr[7].ToString() + "', " + // ESTACION
                                " '" + dr[3].ToString() + "', " + // TARJETA
                                " convert(date, '" + dr[4].ToString() + "', 103), " + // FECHA
                                " '" + dr[5].ToString() + "', " + // HORA
                                " '" + dr[8].ToString() + "', " +// GUIA
                                " '0', " + // ROLLO
                                " " + dr[12].ToString() + ", " + // KILOMETRAJE
                                " " + dr[10].ToString().Replace(",", ".") + ", " + // LITROS
                                " " + dr[11].ToString().Replace(",", ".") + ", " + // PRECIO
                                " " + dr[14].ToString().Replace(",", ".") + ", " + // PRECIO LT
                                " '" + dr[13].ToString().Replace(",", ".") + "', " + // KM LT
                                " '" + dr[9].ToString().Replace(",", ".") + "' " + // PRECIO / KM
                                "); ";
                            }
                            cont++;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    db.Scalar(sql_insert);
                    // CAMPOS YA INSERTADOS

                    DataTable dt_vista = new DataTable();
                    dt_vista = db.consultar(" select distinct cct2.patente, " +
                                            " (select convert(date, MIN(fecha), 103) from carga_combustible_temp cct where cct.patente = cct2.patente) as fecha_menor, " +
                                            " (select convert(date, MAX(fecha), 103) from carga_combustible_temp cct where cct.patente = cct2.patente) as fecha_mayor " +
                                            " from carga_combustible_temp cct2 " +
                                            " group by patente, fecha " +
                                            " order by patente");

                    string html = "<table id='tablitapro' class='table table-bordered'>";
                    html += "<thead><tr>";
                    html += "<th>&nbsp;</th>";
                    html += "<th>PATENTE</th>";
                    html += "<th>FECHA</th>";
                    html += "<th>ESTACION</th>";
                    html += "<th>GUIA</th>";
                    html += "<th>LITROS</th>";
                    html += "<th>MONTO</th>";
                    html += "<th>ODOMETRO (KM)</th>";
                    html += "<th>KM/LT. (KM)</th>";
                    html += "</tr></thead>";
                    html += "<tbody>";

                    bool cambiacolor = false;
                    // RECORRO POR PATENTE Y FECHAS LA INFORMACION DE LA BD
                    foreach (DataRow dr in dt_vista.Rows)
                    {
                        try
                        {
                            // SELECT IGUALES
                            DataTable dt_iguales = db.consultar("select * from V_copec where convert(date, fecha, 103) >= convert(date, '" + DateTime.Parse(dr["fecha_menor"].ToString()).ToString("dd/MM/yyyy") + "', 103) " +
                                                                "and convert(date, fecha, 103) <= convert(date, '" + DateTime.Parse(dr["fecha_mayor"].ToString()).ToString("dd/MM/yyyy") + "', 103) and patente = '" + dr["patente"].ToString() + "' order by fecha ");
                            // SELECT EXCEL
                            DataTable dt_excel = db.consultar("select * from V_COPEC_EXCEL where convert(date, fecha2, 103) >= convert(date, '" + DateTime.Parse(dr["fecha_menor"].ToString()).ToString("dd/MM/yyyy") + "', 103) " +
                                                            "and convert(date, fecha2, 103) <= convert(date, '" + DateTime.Parse(dr["fecha_mayor"].ToString()).ToString("dd/MM/yyyy") + "', 103) and patente2 = '" + dr["patente"].ToString() + "'  order by fecha2 ");
                            // SELECT DB
                            DataTable dt_db = db.consultar("select * from V_COPEC_DB  where convert(date, fecha, 103) >= convert(date, '" + DateTime.Parse(dr["fecha_menor"].ToString()).ToString("dd/MM/yyyy") + "', 103) " +
                                                            "and convert(date, fecha, 103) <= convert(date, '" + DateTime.Parse(dr["fecha_mayor"].ToString()).ToString("dd/MM/yyyy") + "', 103) and patente = '" + dr["patente"].ToString() + "' order by fecha ");


                            //html += "<tr><td colspan=\"8\" class='text-center'><h6><b>Patente: " + dr["patente"].ToString() + " del " + DateTime.Parse(dr["fecha_menor"].ToString()).ToString("dd/MM/yyyy") + " al " + DateTime.Parse(dr["fecha_mayor"].ToString()).ToString("dd/MM/yyyy") + "</b><h6></td></tr>";
                            foreach (DataRow dr2 in dt_iguales.Rows)
                            {
                                if (cambiacolor)
                                {
                                    html += "<tr style='background-color:#f2f2f2'>";
                                }
                                else
                                {
                                    html += "<tr>";
                                }
                                html += "<td><span class='badge badge-success'>CUADRAN</span></td>";
                                html += "<td>" + dr2["patente"].ToString() + "</td>";
                                html += "<td>" + DateTime.Parse(dr2["fecha"].ToString()).ToString("dd/MM/yyyy") + "</td>";
                                html += "<td>" + dr2["estacion"].ToString() + "</td>";
                                html += "<td>" + dr2["guia"].ToString() + "</td>";
                                html += "<td>" + dr2["litros2"].ToString() + "</td>";
                                html += "<td>" + dr2["precio"].ToString() + "</td>";
                                html += "<td>" + dr2["kilometraje"].ToString() + "</td>";
                                html += "<td>" + dr2["km_lit"].ToString() + "</td>";
                                html += "</tr>";
                            }
                            foreach (DataRow dr2 in dt_excel.Rows)
                            {
                                if (cambiacolor)
                                {
                                    html += "<tr style='background-color:#f2f2f2'>";
                                }
                                else
                                {
                                    html += "<tr>";
                                }

                                html += "<td><span class='badge badge-primary'>SOLO EXCEL</span></td>";
                                html += "<td>" + dr2["patente2"].ToString() + "</td>";
                                html += "<td>" + DateTime.Parse(dr2["fecha2"].ToString()).ToString("dd/MM/yyyy") + "</td>";
                                html += "<td>" + dr2["estacion"].ToString() + "</td>";
                                html += "<td>" + dr2["guia2"].ToString() + "</td>";
                                html += "<td>" + dr2["litros2"].ToString() + "</td>";
                                html += "<td>" + dr2["precio2"].ToString() + "</td>";
                                html += "<td>" + dr2["kilometraje2"].ToString() + "</td>";
                                html += "<td>" + dr2["km_lit"].ToString() + "</td>";
                                html += "</tr>";
                            }
                            foreach (DataRow dr2 in dt_db.Rows)
                            {
                                if (cambiacolor)
                                {
                                    html += "<tr style='background-color:#f2f2f2'>";
                                }
                                else
                                {
                                    html += "<tr>";
                                }
                                html += "<td><span class='badge badge-danger'>SOLO TZAMORA</span></td>";
                                html += "<td>" + dr2["patente"].ToString() + "</td>";
                                html += "<td>" + DateTime.Parse(dr2["fecha"].ToString()).ToString("dd/MM/yyyy") + "</td>";
                                html += "<td>" + dr2["nombre_estacion"].ToString() + "</td>";
                                html += "<td>" + dr2["guia"].ToString() + "</td>";
                                html += "<td>" + dr2["litros_cargados"].ToString() + "</td>";
                                html += "<td>" + dr2["precio"].ToString() + "</td>";
                                html += "<td>" + dr2["kilometraje"].ToString() + "</td>";
                                html += "<td>" + dr2["rendimiento"].ToString() + "</td>";
                                html += "</tr>";
                            }

                            cambiacolor = !cambiacolor;
                        }
                        catch (Exception ex)
                        {
                            T_ERRORES.Text += ex.Message;
                        }
                    }
                    html += "</tbody>";
                    html += "</table>";

                    AQUI_HTML.InnerHtml = html;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "modulogtprincipal", "<script>javascript:Datatables();</script>", false);
                }
            }
            catch (Exception ex)
            {
                T_ERRORES.Text += ex.Message;
            }
        }
    }
}