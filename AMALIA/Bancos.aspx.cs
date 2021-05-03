using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AMALIAFW;
using System.Data;

namespace AMALIA
{
    public partial class Bancos : System.Web.UI.Page
    {
        public static string bancos_global = "banco";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DBUtil db = new DBUtil();
                Session["BANCOS_DTTIPOSDATOS"] = db.consultar("select id_tipo_parametro, nombre_tipo_parametro from F_TIPOS_PARAMETROS; ");
                LLENAR_TABLA();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "grid", "<script>javascript:Datatables();</script>", false);
            }
        }

        #region LISTADO
        private void LLENAR_TABLA()
        {
            G_PRINCIPAL.DataSource = FN_TABLAS.LLENADT(" where ID_EMPRESA = " + Session["ID_EMPRESA"]);
            G_PRINCIPAL.DataBind();
        }

        protected void G_PRINCIPAL_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //editar
                if (e.CommandName == "Editar")
                {
                    int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    COMPLETAR_DETALLE(id);
                    // MOSTRAR / OCULTAR PANEL
                    PANEL_PRINCIPAL.Visible = false;
                    PANEL_DETALLE1.Visible = true;
                }
                //Borrar
                if (e.CommandName == "Borrar")
                {
                    int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    OBJ_TABLAS tabla = new OBJ_TABLAS();
                    tabla.ID_TABLA = id;
                    FN_TABLAS.DELETE(ref tabla);
                    if (tabla._respok)
                    {
                        DBUtil db = new DBUtil();
                        db.Scalar("delete from F_TABLA_DETALLE where id_tabla = " + tabla.ID_TABLA);
                        alert(bancos_global + " eliminado con éxito", 0);
                        LLENAR_TABLA();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region DETALLE
        public void COMPLETAR_DETALLE(int id)
        {
            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();
            OBJ_TABLAS tabla = new OBJ_TABLAS();

            tabla.ID_TABLA = id;
            FN_TABLAS.LLENAOBJETO(ref tabla);
            if (tabla._respok)
            {
                LIMPIARCAMPOS();
                T_ID.Text = id.ToString();
                T_NOMBRE_TABLA.Text = tabla.NOMBRE_TABLA;
                // DETALLE               
                dt = db.consultar(" select ID_TABLA_DETALLE, ID_EMPRESA, ID_TABLA, NOMBRE_COLUMNA, ID_TIPO_PARAMETRO, ACTIVO, ACCION = 'GUARDADO' from F_TABLA_DETALLE where ID_TABLA = " + id);
                G_DETALLE1.DataSource = dt;
                G_DETALLE1.DataBind();
                Session["BANCOS_DTDETALLE1"] = dt;

                PANEL_COLUMNAS.Visible = true;
            }
        }
        protected void B_GUARDAR_Click(object sender, EventArgs e)
        {
            OBJ_TABLAS tabla = new OBJ_TABLAS();
            if (T_ID.Text != "")
            {
                // EDITAR
                tabla.ID_TABLA = int.Parse(T_ID.Text);
                FN_TABLAS.LLENAOBJETO(ref tabla);
                if (tabla._respok)
                {
                    tabla.NOMBRE_TABLA = T_NOMBRE_TABLA.Text;
                    FN_TABLAS.UPDATE(ref tabla);
                    if (tabla._respok)
                    {
                        RECORRECOLUMNAS();
                        COMPLETAR_DETALLE(tabla.ID_TABLA);
                        alert(bancos_global + " modificado con éxito", 1);
                    }
                }
            }
            else
            {
                FN_TABLAS.PREPARAOBJETO(ref tabla);
                if (tabla._respok)
                {
                    tabla.NOMBRE_TABLA = T_NOMBRE_TABLA.Text;
                    tabla.ID_EMPRESA = int.Parse(Session["ID_EMPRESA"].ToString());
                    FN_TABLAS.INSERT(ref tabla);
                    if (tabla._respok)
                    {
                        T_ID.Text = tabla.ID_TABLA.ToString();
                        alert(bancos_global + " creado con éxito", 1);
                        AGREGA_NUEVA_COLUMNA();
                        PANEL_COLUMNAS.Visible = true;
                    }
                }
            }
        }

        protected void B_VOLVER_Click(object sender, EventArgs e)
        {
            LLENAR_TABLA();
            // MOSTRAR / OCULTAR PANEL
            PANEL_PRINCIPAL.Visible = true;
            PANEL_DETALLE1.Visible = false;
        }

        protected void G_DETALLE1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Borrar")
                {
                    int id = int.Parse((G_DETALLE1.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));

                    DataTable dt = (DataTable)Session["BANCOS_DTDETALLE1"];
                    dt.Rows[Convert.ToInt32(e.CommandArgument)]["ACCION"] = "POR BORRAR";
                    dt.AcceptChanges();
                    dt = GRIDTODT(G_DETALLE1, dt);
                    Session["BANCOS_DTDETALLE1"] = dt;
                    G_DETALLE1.DataSource = dt;
                    G_DETALLE1.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void NUEVODTDETALLE()
        {
            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();
            dt = db.consultar(" select ID_TABLA_DETALLE, ID_EMPRESA, ID_TABLA, NOMBRE_COLUMNA, ID_TIPO_PARAMETRO, ACTIVO, ACCION = 'NUEVO' from F_TABLA_DETALLE where ID_TABLA_DETALLE = -1; ");
            Session["BANCOS_DTDETALLE1"] = dt;
        }

        protected void G_DETALLE1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // COMBO TIPO DE DATOS
                DropDownList DTPARAMETROS = (e.Row.FindControl("CB_TIPO_PARAMETRO") as DropDownList);
                DataTable dt = (DataTable)(Session["BANCOS_DTTIPOSDATOS"]);
                DTPARAMETROS.DataSource = dt;
                DTPARAMETROS.DataValueField = "id_tipo_parametro";
                DTPARAMETROS.DataTextField = "nombre_tipo_parametro";
                DTPARAMETROS.DataBind();
                string tipo_parametro = (G_DETALLE1.DataKeys[Convert.ToInt32(e.Row.RowIndex)].Values[1].ToString());
                if (tipo_parametro != "")
                {
                    DTPARAMETROS.SelectedValue = tipo_parametro;
                }
                // ESTADO
                switch (e.Row.Cells[6].Text)
                {
                    case "NUEVO":
                        e.Row.Cells[6].Text = "<span class='badge badge-success amalia-control'><b>NUEVO</b></span>";
                        break;
                    case "POR BORRAR":
                        e.Row.Cells[6].Text = "<span class='badge badge-danger amalia-control'><b>POR BORRAR</b></span>";
                        break;
                    default:
                        e.Row.Cells[6].Text = "<span class='badge badge-primary amalia-control'><b>GUARDADO</b></span>";
                        break;
                }
            }
        }

        protected void B_AGREGAR_COLUMNA_Click(object sender, EventArgs e)
        {
            AGREGA_NUEVA_COLUMNA();
        }

        public void AGREGA_NUEVA_COLUMNA()
        {
            DataTable dt = (DataTable)(Session["BANCOS_DTDETALLE1"]);
            dt = GRIDTODT(G_DETALLE1, dt);

            DataRow blankRow = dt.NewRow();
            blankRow["ID_TABLA_DETALLE"] = -1;
            blankRow["ID_EMPRESA"] = Session["ID_EMPRESA"];
            blankRow["ID_TABLA"] = T_ID.Text;
            blankRow["NOMBRE_COLUMNA"] = "NUEVA COLUMNA";
            blankRow["ID_TIPO_PARAMETRO"] = 1;
            blankRow["ACTIVO"] = "ACTIVO";
            blankRow["ACCION"] = "NUEVO";
            dt.Rows.Add(blankRow);
            Session["BANCOS_DTDETALLE1"] = dt;

            G_DETALLE1.DataSource = dt;
            G_DETALLE1.DataBind();
        }

        public DataTable GRIDTODT(GridView grilla, DataTable dt)
        {
            foreach (GridViewRow row in grilla.Rows)
            {
                string ID_TABLA_DETALLE = grilla.DataKeys[Convert.ToInt32(row.RowIndex)].Values[0].ToString();
                TextBox t_nombre = (TextBox)row.FindControl("T_NOMBRE_COLUMNA");
                DropDownList cb_parametro = (DropDownList)row.FindControl("CB_TIPO_PARAMETRO");
                dt.Rows[row.RowIndex]["NOMBRE_COLUMNA"] = t_nombre.Text;
                dt.Rows[row.RowIndex]["ID_TIPO_PARAMETRO"] = cb_parametro.SelectedValue;
            }
            dt.AcceptChanges();
            return dt;
        }

        public void RECORRECOLUMNAS()
        {
            foreach (GridViewRow row in G_DETALLE1.Rows)
            {
                string ID_TABLA_DETALLE = G_DETALLE1.DataKeys[Convert.ToInt32(row.RowIndex)].Values[0].ToString();

                if (row.Cells[6].Text == "POR BORRAR" && ID_TABLA_DETALLE != "-1")
                {
                    OBJ_TABLA_DET det = new OBJ_TABLA_DET();
                    det.ID_TABLA_DETALLE = int.Parse(ID_TABLA_DETALLE);
                    FN_TABLA_DET.DELETE(ref det);
                }
                else
                {
                    OBJ_TABLA_DET det = new OBJ_TABLA_DET();
                    TextBox t_nombre = (TextBox)row.FindControl("T_NOMBRE_COLUMNA");
                    DropDownList cb_parametro = (DropDownList)row.FindControl("CB_TIPO_PARAMETRO");

                    det.ID_EMPRESA = int.Parse(Session["ID_EMPRESA"].ToString());
                    det.ID_TABLA = int.Parse(T_ID.Text);
                    det.ID_TIPO_PARAMETRO = int.Parse(cb_parametro.SelectedValue);
                    det.NOMBRE_COLUMNA = t_nombre.Text;
                    det.ACTIVO = "ACTIVO";

                    if (row.Cells[6].Text == "NUEVO")
                    {
                        FN_TABLA_DET.INSERT(ref det);
                    }
                    else
                    {
                        det.ID_TABLA_DETALLE = int.Parse(ID_TABLA_DETALLE);
                        FN_TABLA_DET.UPDATE(ref det);
                    }
                }
            }
        }
        #endregion

        #region GLOBAL
        protected void B_NUEVO_Click(object sender, EventArgs e)
        {
            LIMPIARCAMPOS();
            NUEVODTDETALLE();
            G_DETALLE1.DataSource = null;
            PANEL_DETALLE1.Visible = true;
            PANEL_PRINCIPAL.Visible = false;
            PANEL_COLUMNAS.Visible = false;
            UP_PRINCIPAL.Update();
        }

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

        #endregion

        #region REGION: RENDER - TABLAS
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            MakeAccessible(G_PRINCIPAL);
            MakeAccessible(G_DETALLE1);
        }
        protected override void Render(HtmlTextWriter writer)
        {
            Page.ClientScript.RegisterForEventValidation(this.UniqueID);
            base.Render(writer);
        }
        public static void MakeAccessible(GridView grid)
        {
            if (grid.Rows.Count <= 0) return;
            grid.UseAccessibleHeader = true;
            grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            grid.PagerStyle.CssClass = "GridPager";
            if (grid.ShowFooter)
                grid.FooterRow.TableSection = TableRowSection.TableFooter;
        }

        protected void alert(string mensaje, int flag)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "mosnoti", "<script>javascript:MostrarNotificacion('" + mensaje + "', " + flag + ");</script>", false);
        }
        #endregion

        protected void G_PRINCIPAL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // ESTADO
                switch (e.Row.Cells[3].Text)
                {
                    case "ACTIVO":
                        e.Row.Cells[3].Text = "<span class='badge badge-success amalia-control'><b>ACTIVO</b></span>";
                        break;
                    case "INACTIVO":
                        e.Row.Cells[3].Text = "<span class='badge badge-danger amalia-control'><b>INACTIVO</b></span>";
                        break;
                    default:
                        e.Row.Cells[3].Text = "<span class='badge badge-primary amalia-control'><b></b></span>";
                        break;
                }
            }
        }
    }
}