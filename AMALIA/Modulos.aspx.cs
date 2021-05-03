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
    public partial class Modulos : System.Web.UI.Page
    {
        public static string modulo_global = "Modulo";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DBUtil db = new DBUtil();
                CB_TIPO_MODULO.DataSource = db.consultar("select id_tipo_modulo, nombre_tipo_modulo from F_TIPOS_MODULOS; ");
                CB_TIPO_MODULO.DataValueField = "id_tipo_modulo";
                CB_TIPO_MODULO.DataTextField = "nombre_tipo_modulo";
                CB_TIPO_MODULO.DataBind();
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
            G_PRINCIPAL.DataSource = FN_MODULOS.LLENADT(" where ID_EMPRESA = " + Session["ID_EMPRESA"]);
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
                    OBJ_MODULOS tabla = new OBJ_MODULOS();
                    tabla.ID_MODULO = id;
                    FN_MODULOS.DELETE(ref tabla);
                    if (tabla._respok)
                    {
                        DBUtil db = new DBUtil();
                        db.Scalar("delete from F_LISTADO_DETALLE where ID_MODULO = " + tabla.ID_MODULO);
                        alert(modulo_global + " eliminado con éxito", 0);
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
            OBJ_MODULOS modulo = new OBJ_MODULOS();

            modulo.ID_MODULO = id;
            FN_MODULOS.LLENAOBJETO(ref modulo);
            if (modulo._respok)
            {
                LIMPIARCAMPOS();
                T_ID.Text = id.ToString();
                T_NOMBRE_MODULO.Text = modulo.NOMBRE_MODULO;
                CB_TIPO_MODULO.SelectedValue = modulo.TIPO_MODULO.ToString();
                CB_WEBAPP.SelectedValue = modulo.WEB_APP;
                //// DETALLE               
                //dt = LLENADTDETALLE("GUARDADO", id.ToString());
                //G_DETALLE1.DataSource = dt;
                //G_DETALLE1.DataBind();
                //PANEL_COLUMNAS.Visible = true;
            }
        }

        protected void B_GUARDAR_Click(object sender, EventArgs e)
        {
            OBJ_MODULOS modulo = new OBJ_MODULOS();
            if (T_ID.Text != "")
            {
                // EDITAR
                modulo.ID_MODULO = int.Parse(T_ID.Text);
                FN_MODULOS.LLENAOBJETO(ref modulo);
                if (modulo._respok)
                {
                    modulo.NOMBRE_MODULO = T_NOMBRE_MODULO.Text;
                    modulo.ID_EMPRESA = int.Parse(Session["ID_EMPRESA"].ToString());
                    modulo.TIPO_MODULO = int.Parse(CB_TIPO_MODULO.SelectedValue);
                    modulo.WEB_APP = CB_WEBAPP.SelectedValue;
                    modulo.ACTIVO = "ACTIVO";
                    modulo.FECHA_CREACION = DateTime.Now;

                    FN_MODULOS.UPDATE(ref modulo);
                    if (modulo._respok)
                    {
                        //RECORRECOLUMNAS();
                        //COMPLETAR_DETALLE(tabla.ID_MODULO);
                        alert(modulo_global + " modificado con éxito", 1);
                    }
                }
            }
            else
            {
                FN_MODULOS.PREPARAOBJETO(ref modulo);
                if (modulo._respok)
                {
                    modulo.NOMBRE_MODULO = T_NOMBRE_MODULO.Text;
                    modulo.ID_EMPRESA = int.Parse(Session["ID_EMPRESA"].ToString());
                    modulo.TIPO_MODULO = int.Parse(CB_TIPO_MODULO.SelectedValue);
                    modulo.WEB_APP = CB_WEBAPP.SelectedValue;
                    modulo.ACTIVO = "ACTIVO";
                    modulo.FECHA_CREACION = DateTime.Now;

                    FN_MODULOS.INSERT(ref modulo);
                    if (modulo._respok)
                    {
                        T_ID.Text = modulo.ID_MODULO.ToString();
                        alert(modulo_global + " creado con éxito", 1);
                        // AGREGA_NUEVA_COLUMNA();
                        //PANEL_COLUMNAS.Visible = true;
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
                    //int id = int.Parse((G_DETALLE1.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));

                    //DataTable dt = (DataTable)Session["LISTADOS_DTDETALLE1"];
                    //dt.Rows[Convert.ToInt32(e.CommandArgument)]["ACCION"] = "POR BORRAR";
                    //dt.AcceptChanges();
                    //dt = GRIDTODT(G_DETALLE1, dt);
                    //Session["LISTADOS_DTDETALLE1"] = dt;
                    //G_DETALLE1.DataSource = dt;
                    //G_DETALLE1.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public DataTable LLENADTDETALLE(string accion, string id)
        {
            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();
            dt = db.consultar(" select ID_MODULO_DETALLE, ID_EMPRESA, ID_MODULO, VALOR_ITEM, NOMBRE_ITEM, ORDEN, ACCION = '" + accion + "' from F_LISTADO_DETALLE where ID_MODULO = " + id + " order by ORDEN; ");
            Session["LISTADOS_DTDETALLE1"] = dt;
            return dt;
        }

        protected void G_DETALLE1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // ESTADO
                switch (e.Row.Cells[7].Text)
                {
                    case "NUEVO":
                        e.Row.Cells[7].Text = "<span class='badge badge-success'><b>NUEVO</b></span>";
                        break;
                    case "POR BORRAR":
                        e.Row.Cells[7].Text = "<span class='badge badge-danger'><b>POR BORRAR</b></span>";
                        break;
                    default:
                        e.Row.Cells[7].Text = "<span class='badge badge-primary'><b>GUARDADO</b></span>";
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
            //DataTable dt = (DataTable)(Session["LISTADOS_DTDETALLE1"]);
            //dt = GRIDTODT(G_DETALLE1, dt);

            //DataRow blankRow = dt.NewRow();
            //blankRow["ID_MODULO_DETALLE"] = -1;
            //blankRow["ID_EMPRESA"] = Session["ID_EMPRESA"];
            //blankRow["ID_MODULO"] = T_ID.Text;
            //blankRow["NOMBRE_ITEM"] = "NUEVO ITEM";
            //blankRow["VALOR_ITEM"] = "1";
            //blankRow["ORDEN"] = dt.Rows.Count + 1;
            //blankRow["ACCION"] = "NUEVO";
            //dt.Rows.Add(blankRow);
            //Session["LISTADOS_DTDETALLE1"] = dt;

            //G_DETALLE1.DataSource = dt;
            //G_DETALLE1.DataBind();
        }

        public DataTable GRIDTODT(GridView grilla, DataTable dt)
        {
            foreach (GridViewRow row in grilla.Rows)
            {
                string ID_TABLA_DETALLE = grilla.DataKeys[Convert.ToInt32(row.RowIndex)].Values[0].ToString();
                TextBox t_orden = (TextBox)row.FindControl("T_POSICION_ITEM");
                TextBox t_valor = (TextBox)row.FindControl("T_VALOR_ITEM");
                TextBox t_nombre = (TextBox)row.FindControl("T_NOMBRE_ITEM");

                dt.Rows[row.RowIndex]["NOMBRE_ITEM"] = t_nombre.Text;
                dt.Rows[row.RowIndex]["VALOR_ITEM"] = t_valor.Text;
                dt.Rows[row.RowIndex]["ORDEN"] = t_orden.Text;
            }
            dt.AcceptChanges();
            return dt;
        }

        public void RECORRECOLUMNAS()
        {
            //foreach (GridViewRow row in G_DETALLE1.Rows)
            //{
            //    string ID_TABLA_DETALLE = G_DETALLE1.DataKeys[Convert.ToInt32(row.RowIndex)].Values[0].ToString();

            //    if (row.Cells[7].Text == "POR BORRAR" && ID_TABLA_DETALLE != "-1")
            //    {
            //        OBJ_LISTADO_DETALLE det = new OBJ_LISTADO_DETALLE();
            //        det.ID_MODULO_DETALLE = int.Parse(ID_TABLA_DETALLE);
            //        FN_LISTADO_DETALLE.DELETE(ref det);
            //    }
            //    else
            //    {
            //        OBJ_LISTADO_DETALLE det = new OBJ_LISTADO_DETALLE();
            //        TextBox t_orden = (TextBox)row.FindControl("T_POSICION_ITEM");
            //        TextBox t_valor = (TextBox)row.FindControl("T_VALOR_ITEM");
            //        TextBox t_nombre = (TextBox)row.FindControl("T_NOMBRE_ITEM");


            //        det.ID_EMPRESA = int.Parse(Session["ID_EMPRESA"].ToString());
            //        det.ID_MODULO = int.Parse(T_ID.Text);
            //        det.VALOR_ITEM = t_valor.Text;
            //        det.NOMBRE_ITEM = t_nombre.Text;
            //        det.ORDEN = int.Parse(t_orden.Text);

            //        if (row.Cells[7].Text == "NUEVO")
            //        {
            //            FN_LISTADO_DETALLE.INSERT(ref det);
            //        }
            //        else
            //        {
            //            det.ID_MODULO_DETALLE = int.Parse(ID_TABLA_DETALLE);
            //            FN_LISTADO_DETALLE.UPDATE(ref det);
            //        }
            //    }
            //}
        }
        #endregion

        #region GLOBAL
        protected void B_NUEVO_Click(object sender, EventArgs e)
        {
            LIMPIARCAMPOS();
            //G_DETALLE1.DataSource = LLENADTDETALLE("NUEVO", "-1");
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
            //MakeAccessible(G_DETALLE1);
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
                switch (e.Row.Cells[5].Text)
                {
                    case "ACTIVO":
                        e.Row.Cells[5].Text = "<span class='badge badge-success amalia-control'><b>ACTIVO</b></span>";
                        break;
                    case "INACTIVO":
                        e.Row.Cells[5].Text = "<span class='badge badge-danger amalia-control'><b>INACTIVO</b></span>";
                        break;
                    default:
                        e.Row.Cells[5].Text = "<span class='badge badge-primary amalia-control'><b></b></span>";
                        break;
                }
            }
        }
    }
}