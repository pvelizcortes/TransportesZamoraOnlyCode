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

namespace AMALIA
{
    public partial class OrdenesdeTrabajo : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Orden de Trabajo";
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
                    LlenarGrilla();
                    LlnearCombos();
                    if (Request.QueryString["id_ot"] != null)
                    {
                        T_ID.Text = Request.QueryString["id_ot"].ToString();
                        COMPLETAR_DETALLE(int.Parse(T_ID.Text));
                    }

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ot_grid", "<script>javascript:Datatables();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ot_combo", "<script>javascript:ComboPro();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "relojot", "<script>javascript:relojito(false);</script>", false);
            }
        }
        public void LlenarGrilla()
        {
            G_PRINCIPAL.DataSource = FN_OT.LLENADT();
            G_PRINCIPAL.DataBind();
        }

        public void LlenarLog()
        {
            DBUtil db = new DBUtil();
            G_LOG_OT.DataSource = db.consultar("select * from v_ot_log where id_ot = " + T_ID.Text + " order by id_log desc ");
            G_LOG_OT.DataBind();
        }
        public void LlenarRepuestos()
        {
            DBUtil db = new DBUtil();
            G_REPUESTOS.DataSource = db.consultar("select * from V_OT_REPUESTOS where id_ot = " + T_ID.Text);
            G_REPUESTOS.DataBind();
        }
        public void LlenarRepex()
        {
            DBUtil db = new DBUtil();
            G_REPEX.DataSource = db.consultar("select * from MANT_OT_REPEX where id_ot = " + T_ID.Text);
            G_REPEX.DataBind();
        }
        public void LlnearCombos()
        {
            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();
            CB_ESTADO_OT.DataSource = db.consultar("select id_estado, nombre_estado from  mant_ot_estados");
            CB_ESTADO_OT.DataValueField = "id_estado";
            CB_ESTADO_OT.DataTextField = "nombre_estado";
            CB_ESTADO_OT.DataBind();

            CB_CATEGORIA.DataSource = db.consultar("select nombre_categoria, id_producto_cat from MANT_PRODUCTO_CAT");
            CB_CATEGORIA.DataValueField = "id_producto_cat";
            CB_CATEGORIA.DataTextField = "nombre_categoria";
            CB_CATEGORIA.DataBind();
            CB_CATEGORIA.Items.Insert(0, new ListItem("-- SELECCIONE --", "-1"));
            CB_CATEGORIA.SelectedValue = "-1";

            CB_MARCA.DataSource = db.consultar("select nombre_marca, id_marca from MANT_REPUESTOS_MARCAS");
            CB_MARCA.DataValueField = "id_marca";
            CB_MARCA.DataTextField = "nombre_marca";
            CB_MARCA.DataBind();
            CB_MARCA.Items.Insert(0, new ListItem("-- SELECCIONE --", "-1"));
            CB_MARCA.SelectedValue = "-1";

            CB_REPEX_PROVEEDOR.DataSource = db.consultar("select nom_proveedor, id_proveedor from MANT_PROVEEDORES");
            CB_REPEX_PROVEEDOR.DataValueField = "id_proveedor";
            CB_REPEX_PROVEEDOR.DataTextField = "nom_proveedor";
            CB_REPEX_PROVEEDOR.DataBind();
            CB_REPEX_PROVEEDOR.Items.Insert(0, new ListItem("-- SELECCIONE --", "-1"));
            CB_REPEX_PROVEEDOR.SelectedValue = "-1";

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
                }
                //Borrar
                if (e.CommandName == "Borrar")
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
        public void COMPLETAR_DETALLE(int id)
        {
            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();
            dt = FN_OT.LLENADT(" where id_ot = " + id);

            if (dt.Rows.Count > 0)
            {
                LIMPIARCAMPOS();
                T_ID.Text = id.ToString();
                ESTADO_I.Text = dt.Rows[0]["id_estado_ot"].ToString();
                DIV_FECHA.InnerHtml = DateTime.Parse(dt.Rows[0]["fecha_creacion"].ToString()).ToString("dd/MM/yyyy");
                DIV_NOMBRE_CONDUCTOR.InnerHtml = dt.Rows[0]["nombre_completo"].ToString();
                DIV_CAMION.InnerText = dt.Rows[0]["patente"].ToString();
                DIV_RAMPLA.InnerText = dt.Rows[0]["patente_rampla"].ToString();
                DIV_OBSERVACION.InnerText = dt.Rows[0]["descripcion"].ToString();
                DIV_NUM_OT.InnerText = id.ToString();

                if (dt.Rows[0]["urgencia"].ToString() == "1")
                {
                    DIV_PRIORIDAD_ICON.InnerHtml = "  <i class=' fa fa-eye col-green fa-2x'></i>";
                    DIV_PRIORIDAD.InnerHtml = "BAJA";
                }
                else if (dt.Rows[0]["urgencia"].ToString() == "2")
                {
                    DIV_PRIORIDAD_ICON.InnerHtml = "  <i class=' fa fa-bell col-blue fa-2x'></i>";
                    DIV_PRIORIDAD.InnerHtml = "NORMAL";
                }
                else if (dt.Rows[0]["urgencia"].ToString() == "3")
                {
                    DIV_PRIORIDAD_ICON.InnerHtml = "  <i class=' fa fa-exclamation col-orange fa-2x'></i>";
                    DIV_PRIORIDAD.InnerHtml = "ALTA";
                }
                else if (dt.Rows[0]["urgencia"].ToString() == "4")
                {
                    DIV_PRIORIDAD_ICON.InnerHtml = "  <i class=' fa fa-exclamation-triangle col-red fa-2x'></i>";
                    DIV_PRIORIDAD.InnerHtml = "URGENTE";
                }
                LlenarLog();
                LlenarRepuestos();
                LlenarRepex();
                UP_GASTO_GENERAL.Update();
                AbreModalGasto();
            }
        }

        public void AbreModalGasto()
        {
            // ABRE MODAL
            ScriptManager.RegisterStartupScript(this, typeof(Page), "abremodalgasto", "<script>javascript:GASTOGENERAL();</script>", false);
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

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            MakeAccessible(G_PRINCIPAL);
            MakeAccessible(G_REPUESTOS);
            MakeAccessible(G_LOG_OT);
            MakeAccessible(G_REPEX);
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

        #endregion

        protected void G_PRINCIPAL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string id_ot = G_PRINCIPAL.DataKeys[e.Row.RowIndex].Values[0].ToString();
                string id_estado = G_PRINCIPAL.DataKeys[e.Row.RowIndex].Values[1].ToString();
                string nombre_estado = G_PRINCIPAL.DataKeys[e.Row.RowIndex].Values[2].ToString();

                //// ESTADO
                //HtmlGenericControl html_prioridad = (HtmlGenericControl)e.Row.FindControl("div_prioridad");
                HtmlGenericControl html_estado = (HtmlGenericControl)e.Row.FindControl("div_estado");

                switch (id_estado)
                {
                    case "1":
                        html_estado.InnerHtml = "<span class='badge badge-block col-grey'><i class='fa fa-plus'></i> " + nombre_estado + "</span>";
                        break;
                    case "2":
                        html_estado.InnerHtml = "<span class='badge badge-block col-orange'><i class='fa fa-eye'></i> " + nombre_estado + "</span>";
                        break;
                    case "3":
                        html_estado.InnerHtml = "<span class='badge badge-block col-orange'><i class='fa fa-dolly'></i> " + nombre_estado + "</span>";
                        break;
                    case "4":
                        html_estado.InnerHtml = "<span class='badge badge-block col-light-blue'><i class='fa fa-tools'></i> " + nombre_estado + "</span>";
                        break;
                    case "5":
                        html_estado.InnerHtml = "<span class='badge badge-block col-red'><i class='fa fa-times'></i> " + nombre_estado + "</span>";
                        break;
                    case "6":
                        html_estado.InnerHtml = "<span class='badge badge-block col-green'><i class='fa fa-clipboard-check'></i> " + nombre_estado + "</span>";
                        break;
                    case "7":
                        html_estado.InnerHtml = "<span class='badge badge-block col-green'><i class='fa fa-external-link-alt'></i> " + nombre_estado + "</span>";
                        break;
                    case "8":
                        html_estado.InnerHtml = "<span class='badge badge-block col-green'><i class='fa fa-truck-moving'></i> " + nombre_estado + "</span>";
                        break;
                    case "9":
                        html_estado.InnerHtml = "<span class='badge badge-block col-green'><i class='fa fa-check-circle'></i> " + nombre_estado + "</span>";
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
            }
        }


        public void CERRARMODAL()
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "cerramodal", "<script>javascript:cerramodal();</script>", false);
        }

        protected void B_GUARDAR_MODAL_Click(object sender, EventArgs e)
        {
            OBJ_OT req = new OBJ_OT();
            req.id_ot = int.Parse(T_ID.Text);

            FN_OT.LLENAOBJETO(ref req);
            if (req._respok)
            {
                req.id_estado_ot = int.Parse(CB_ESTADO_OT.SelectedValue);
                req.fecha_actualizacion = DateTime.Now;
                FN_OT.UPDATE(ref req);
                if (req._respok)
                {
                    OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                    usuario.usuario = HttpContext.Current.User.Identity.Name;
                    FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);
                    DBUtil db = new DBUtil();
                    db.Scalar(" insert into mant_ot_log (id_ot, fecha, observacion, estado_i, estado_f, id_usuario) values (" + req.id_ot + ", getdate(), 'Cambio de Estado', " + ESTADO_I.Text + ", " + CB_ESTADO_OT.SelectedValue + ", " + usuario.ID_USUARIO + "); ");
                    ESTADO_I.Text = CB_ESTADO_OT.Text;
                }
                LlenarGrilla();
                UP_PRINCIPAL.Update();
                CERRARMODAL();
                alert("Cambio de estado con éxito", 1);
            }
        }

        protected void B_AGREGAR_REPUESTO_Click(object sender, EventArgs e)
        {
            try
            {
                if (T_CANTIDAD_REPUESTOS.Text == "")
                {
                    alert("Ingrese una cantidad", 0);
                }
                else if (CB_REPUESTO.SelectedValue == "-1")
                {
                    alert("Seleccione un repuesto", 0);
                }
                else if (T_VALOR_REPUESTOS.Text == "")
                {
                    alert("Ingrese un valor para el repuesto", 0);
                }
                else
                {
                    DBUtil db = new DBUtil();
                    DataTable dt = new DataTable();
                    OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                    usuario.usuario = HttpContext.Current.User.Identity.Name;
                    FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);
                    string id = T_ID.Text;
                    string sql = "";
                    sql += "insert into mant_ot_productos (id_ot, id_producto, cantidad, valor) VALUES " +
                            " (" + id + ", " + CB_REPUESTO.SelectedValue + ", " + T_CANTIDAD_REPUESTOS.Text + ", " + T_VALOR_REPUESTOS.Text + "); ";
                    db.Scalar(sql);
                    db.Scalar(" insert into mant_ot_log (id_ot, fecha, observacion, estado_i, estado_f, id_usuario) values (" + id + ", getdate(), 'Se agrega repuesto: " + CB_REPUESTO.SelectedItem.Text + " | VALOR: $" + int.Parse(T_VALOR_REPUESTOS.Text).ToString("#,##0") + "', " + ESTADO_I.Text + ", " + ESTADO_I.Text + ", " + usuario.ID_USUARIO + "); ");
                    
                    dt = db.consultar("select * from mant_producto_stock where id_producto = " + CB_REPUESTO.SelectedValue);
                    int stock_actual = 0;
                    int restar = int.Parse(T_CANTIDAD_REPUESTOS.Text);
                    if (dt.Rows.Count > 0)
                    {
                        // EDITAR STOCK
                         stock_actual = int.Parse(dt.Rows[0]["cantidad"].ToString());
                         restar = int.Parse(T_CANTIDAD_REPUESTOS.Text);
                    }
                    db.Scalar(" insert into mant_stock_log (entra_sale, id_producto, cantidad, cantidad_inicial, cantidad_final, precio_compra, id_ot, doc_compra, fecha, usuario, motivo) values (2, " + CB_REPUESTO.SelectedValue + ", " + T_CANTIDAD_REPUESTOS.Text + ", " + stock_actual + ", " + (stock_actual - restar) + ", -1, " + id + ", 'N/A', getdate(), " + usuario.ID_USUARIO + ", 'ORDEN DE TRABAJO'); ");
                    db.Scalar(" update MANT_PRODUCTO_STOCK set cantidad = (cantidad - " + T_CANTIDAD_REPUESTOS.Text + ") where id_producto = " + CB_REPUESTO.SelectedValue + " ");
                    LlenarLog();
                    LlenarRepuestos();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void G_REPUESTOS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Borrar")
            {
                DBUtil db = new DBUtil();
                DataTable dt = new DataTable();
                OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                usuario.usuario = HttpContext.Current.User.Identity.Name;
                FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);
                int id = int.Parse((G_REPUESTOS.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                string nombre_repuesto = G_REPUESTOS.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString();
                int cantidad = int.Parse((G_REPUESTOS.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[3].ToString()));
                int id_producto = int.Parse((G_REPUESTOS.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[2].ToString()));

                db.Scalar(" delete from mant_ot_productos where id_ot_producto = " + id);
                db.Scalar(" insert into mant_ot_log (id_ot, fecha, observacion, estado_i, estado_f, id_usuario) values (" + T_ID.Text + ", getdate(), 'Se elimina repuesto: " + nombre_repuesto + "', " + ESTADO_I.Text + ", " + ESTADO_I.Text + ", " + usuario.ID_USUARIO + "); ");
                dt = db.consultar("select * from mant_producto_stock where id_producto = " + id_producto.ToString());
                int stock_actual = 0;
                
                if (dt.Rows.Count > 0)
                {
                    // EDITAR STOCK
                    stock_actual = int.Parse(dt.Rows[0]["cantidad"].ToString());                    
                }
                db.Scalar(" insert into mant_stock_log (entra_sale, id_producto, cantidad, cantidad_inicial, cantidad_final, precio_compra, id_ot, doc_compra, fecha, usuario, motivo) values (1, " + id_producto.ToString() + ", " + cantidad.ToString() + ", " + stock_actual + ", " + (stock_actual + cantidad) + ", -1, " + T_ID.Text + ", 'N/A', getdate(), " + usuario.ID_USUARIO + ", 'ORDEN DE TRABAJO (SACA REPUESTO)'); ");
                db.Scalar(" update MANT_PRODUCTO_STOCK set cantidad = (cantidad + " + cantidad.ToString() + ") where id_producto = " + id_producto.ToString() + " ");
                LlenarLog();
                LlenarRepuestos();
            }
        }

        public void LlenarComboRepuesto()
        {
            string sql_query = "";
            if (CB_CATEGORIA.SelectedValue != "-1")
            {
                sql_query += " and cat_producto = " + CB_CATEGORIA.SelectedValue;
            }
            if (CB_MARCA.SelectedValue != "-1")
            {
                sql_query += " and id_marca = " + CB_MARCA.SelectedValue;
            }

            DBUtil db = new DBUtil();
            CB_REPUESTO.DataSource = db.consultar("select id_producto, nom_producto from mant_producto where 1=1 " + sql_query);
            CB_REPUESTO.DataValueField = "id_producto";
            CB_REPUESTO.DataTextField = "nom_producto";
            CB_REPUESTO.DataBind();
            CB_REPUESTO.Items.Insert(0, new ListItem("-- SELECCIONE --", "-1"));
        }

        protected void CB_CATEGORIA_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarComboRepuesto();
        }

        protected void CB_MARCA_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarComboRepuesto();
        }

        protected void B_AGREGAR_COMENTARIO_Click(object sender, EventArgs e)
        {
            OBJ_USUARIOS usuario = new OBJ_USUARIOS();
            usuario.usuario = HttpContext.Current.User.Identity.Name;
            FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);
            DBUtil db = new DBUtil();
            db.Scalar(" insert into mant_ot_log (id_ot, fecha, observacion, estado_i, estado_f, id_usuario) values (" + T_ID.Text + ", getdate(), '" + T_OBSERVACION.Text + "', " + ESTADO_I.Text + ", " + ESTADO_I.Text + ", " + usuario.ID_USUARIO + "); ");
            LlenarLog();
        }

        protected void B_AGREGAR_REPEX_Click(object sender, EventArgs e)
        {
            try
            {
                if (T_REPEX_VALOR.Text == "")
                {
                    alert("Ingrese un valor", 0);
                }
                else if (T_REPEX_FACTURA.Text == "")
                {
                    alert("Ingrese una factura", 0);
                }
                else if (CB_REPEX_PROVEEDOR.SelectedValue == "-1")
                {
                    alert("Seleccione un proveedor", 0);
                }
                else
                {
                    DBUtil db = new DBUtil();
                    OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                    usuario.usuario = HttpContext.Current.User.Identity.Name;
                    FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);
                    OBJ_REPEX repex = new OBJ_REPEX();

                    repex.id_ot = int.Parse(T_ID.Text);
                    repex.id_proveedor = int.Parse(CB_REPEX_PROVEEDOR.SelectedValue);
                    repex.nom_proveedor = CB_REPEX_PROVEEDOR.SelectedItem.Text;
                    repex.num_factura = T_REPEX_FACTURA.Text;
                    repex.valor = int.Parse(T_REPEX_VALOR.Text);
                    repex.fecha = DateTime.Now;

                    FN_REPEX.INSERT(ref repex);
                    if (repex._respok)
                    {
                        
                        db.Scalar(" insert into mant_ot_log (id_ot, fecha, observacion, estado_i, estado_f, id_usuario) values (" + T_ID.Text + ", getdate(), 'Reparacion externa: FACT." + T_REPEX_FACTURA.Text + " | VALOR: $" + repex.valor.ToString("#,##0") + "', " + ESTADO_I.Text + ", " + ESTADO_I.Text + ", " + usuario.ID_USUARIO + "); ");
                    }
                    LlenarLog();
                    LlenarRepex();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void G_REPEX_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Borrar")
            {
                DBUtil db = new DBUtil();
                OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                usuario.usuario = HttpContext.Current.User.Identity.Name;
                FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);
                int id = int.Parse((G_REPEX.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));

                db.Scalar(" delete from mant_ot_repex where id_ot_repex = " + id);
                db.Scalar(" insert into mant_ot_log (id_ot, fecha, observacion, estado_i, estado_f, id_usuario) values (" + T_ID.Text + ", getdate(), 'Se elimina reparacion externa', " + ESTADO_I.Text + ", " + ESTADO_I.Text + ", " + usuario.ID_USUARIO + "); ");

                LlenarLog();
                LlenarRepex();
            }
        }
    }

}