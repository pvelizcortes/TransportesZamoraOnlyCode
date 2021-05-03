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
    public partial class COMPRA : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Compra";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombos();             
                FECHA_COMPRA.Text = DateTime.Now.ToString("yyyy-MM-dd");
                FITRO_FECHA.Text = DateTime.Now.ToString("yyyy-MM-dd");
                LlenarGrilla();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "grid", "<script>javascript:Datatables();</script>", false);
            }
        }

        public void LlenarGrilla()
        {
            DBUtil db = new DBUtil();
            G_PRINCIPAL.DataSource = db.consultar("select * from V_PV_COMPRAS where fecha_compra = convert(date, '" + DateTime.Parse(FITRO_FECHA.Text).ToString("dd/MM/yyyy") + "', 103);");
            G_PRINCIPAL.DataBind();
        }

        public void CargarCombos()
        {
            DBUtil db = new DBUtil();

            CB_PRODUCTO.DataSource = db.consultar("select id_producto, nombre_producto from pv_producto order by nombre_producto");
            CB_PRODUCTO.DataTextField = "nombre_producto";
            CB_PRODUCTO.DataValueField = "id_producto";
            CB_PRODUCTO.DataBind();
            CB_PRODUCTO.Items.Insert(0, new ListItem("-- SELECCIONE --", "0"));

            CB_CATEGORIA.DataSource = db.consultar("select id_Cat_producto, nombre_categoria from pv_producto_cat where id_producto = -1 order by nombre_categoria");
            CB_CATEGORIA.DataTextField = "nombre_categoria";
            CB_CATEGORIA.DataValueField = "id_Cat_producto";
            CB_CATEGORIA.DataBind();
            CB_CATEGORIA.Items.Insert(0, new ListItem("-- SELECCIONE --", "0"));

            CB_PROVEEDOR.DataSource = db.consultar("select id_proveedor, nombre from pv_proveedor order by nombre ");
            CB_PROVEEDOR.DataTextField = "nombre";
            CB_PROVEEDOR.DataValueField = "id_proveedor";
            CB_PROVEEDOR.DataBind();
            CB_PROVEEDOR.Items.Insert(0, new ListItem("-- SELECCIONE --", "0"));
        }

        protected void B_GUARDAR_Click(object sender, EventArgs e)
        {
            try
            {
                DBUtil db = new DBUtil();
                if (T_ID.Text != "")
                {
                    // EDITAR
                    List<SPVars> vars = new List<SPVars>();
                    vars.Add(new SPVars { nombre = "fecha_compra", valor = FECHA_COMPRA.Text });
                    vars.Add(new SPVars { nombre = "id_producto", valor = CB_PRODUCTO.SelectedValue });
                    vars.Add(new SPVars { nombre = "id_proveedor", valor = CB_PROVEEDOR.SelectedValue });
                    vars.Add(new SPVars { nombre = "id_cat_producto", valor = CB_CATEGORIA.SelectedValue });
                    vars.Add(new SPVars { nombre = "tipo_embalaje", valor = CB_EMBALAJE.SelectedValue });
                    vars.Add(new SPVars { nombre = "forma_pago", valor = CB_FORMA_PAGO.SelectedValue });
                    vars.Add(new SPVars { nombre = "cantidad", valor = T_CANTIDAD.Text });
                    vars.Add(new SPVars { nombre = "precio", valor = T_PRECIO.Text });
                    vars.Add(new SPVars { nombre = "total", valor = T_TOTAL.Text });
                    vars.Add(new SPVars { nombre = "comision", valor = T_COMISION.Text });
                    vars.Add(new SPVars { nombre = "flete", valor = T_FLETE.Text });
                    vars.Add(new SPVars { nombre = "carga", valor = T_CARGA.Text });
                    vars.Add(new SPVars { nombre = "otros", valor = T_OTROS.Text });
                    vars.Add(new SPVars { nombre = "detalle_pago", valor = T_DETALLE_PAGO.Text });

                    //string sql = "insert into PV_COMPRA VALUES (id_proveedor = @id_cliemte, fecha = @fecha, id_producto = @id_producto, id_cat_producto = @id_cat_producto, tipo_embalaje = @tipo_embalaje, cantidad = @cantidad, precio = @precio, total = @total, forma_pago = @forma_pago, detalle_pago = @detalle_pago )";
                    string sql = "update PV_COMPRA set id_proveedor = @id_proveedor, fecha_compra = @fecha_compra, id_producto = @id_producto, id_cat_producto = @id_cat_producto, tipo_embalaje = @tipo_embalaje, cantidad = @cantidad, precio = @precio, total = @total, comision = @comision, flete = @flete, carga = @carga, otros = @otros, forma_pago = @forma_pago, detalle_pago = @detalle_pago where id_compra = " + T_ID.Text;
                    db.Scalar2(sql, vars);
                    alert(objeto_mantenedor_global + " editada con éxito", 1);
                    CB_PROVEEDOR.Focus();
                    // STOCK AUXILIAR
                    int existe2 = int.Parse(db.Scalar("select count(1) from pv_stock where id_producto = " + t_aux_prod.Text + " and id_cat_producto = " + t_aux_cat.Text + " and tipo_embalaje = '" + t_aux_embalaje.Text + "'").ToString());

                    if (existe2 > 0)
                    {
                        int cantidad = int.Parse(db.Scalar("select cantidad from pv_stock where id_producto = " + t_aux_prod.Text + " and id_cat_producto = " + t_aux_cat.Text + " and tipo_embalaje = '" + t_aux_embalaje.Text + "'").ToString());
                        cantidad = cantidad - int.Parse(t_aux_cantidad.Text);
                        string sql_stock = "update PV_STOCK set cantidad = " + cantidad + " where id_producto = " + t_aux_prod.Text + " and id_cat_producto = " + t_aux_cat.Text + " and tipo_embalaje = '" + t_aux_embalaje.Text + "'";
                        db.Scalar2(sql_stock, vars);
                    }
                    else
                    {
                        int canttt = (-1 * int.Parse(t_aux_cantidad.Text));
                        string sql_stock = "insert into PV_STOCK VALUES (" + t_aux_prod.Text + ", " + t_aux_cat.Text + ", '" + t_aux_embalaje.Text  + "', " + canttt.ToString() + ")";
                        db.Scalar2(sql_stock, vars);
                    }

                    // STOCK
                    int existe = int.Parse(db.Scalar("select count(1) from pv_stock where id_producto = " + CB_PRODUCTO.SelectedValue + " and id_cat_producto = " + CB_CATEGORIA.SelectedValue + " and tipo_embalaje = '" + CB_EMBALAJE.SelectedValue + "'").ToString());

                    if (existe > 0)
                    {
                        int cantidad = int.Parse(db.Scalar("select cantidad from pv_stock where id_producto = " + CB_PRODUCTO.SelectedValue + " and id_cat_producto = " + CB_CATEGORIA.SelectedValue + " and tipo_embalaje = '" + CB_EMBALAJE.SelectedValue + "'").ToString());
                        cantidad += int.Parse(T_CANTIDAD.Text);
                        string sql_stock = "update PV_STOCK set cantidad = " + cantidad + " where id_producto = " + CB_PRODUCTO.SelectedValue + " and id_cat_producto = " + CB_CATEGORIA.SelectedValue + " and tipo_embalaje = '" + CB_EMBALAJE.SelectedValue + "'";
                        db.Scalar2(sql_stock, vars);
                    }
                    else
                    {
                        string sql_stock = "insert into PV_STOCK VALUES (@id_producto, @id_cat_producto, @tipo_embalaje, @cantidad)";
                        db.Scalar2(sql_stock, vars);
                    }
                }
                else
                {
                    // NUEVO
                    List<SPVars> vars = new List<SPVars>();
                    vars.Add(new SPVars { nombre = "fecha_compra", valor = FECHA_COMPRA.Text });
                    vars.Add(new SPVars { nombre = "id_producto", valor = CB_PRODUCTO.SelectedValue });
                    vars.Add(new SPVars { nombre = "id_proveedor", valor = CB_PROVEEDOR.SelectedValue });
                    vars.Add(new SPVars { nombre = "id_cat_producto", valor = CB_CATEGORIA.SelectedValue });
                    vars.Add(new SPVars { nombre = "tipo_embalaje", valor = CB_EMBALAJE.SelectedValue });
                    vars.Add(new SPVars { nombre = "forma_pago", valor = CB_FORMA_PAGO.SelectedValue });
                    vars.Add(new SPVars { nombre = "cantidad", valor = T_CANTIDAD.Text });
                    vars.Add(new SPVars { nombre = "precio", valor = T_PRECIO.Text });
                    vars.Add(new SPVars { nombre = "total", valor = T_TOTAL.Text });
                    vars.Add(new SPVars { nombre = "comision", valor = T_COMISION.Text });
                    vars.Add(new SPVars { nombre = "flete", valor = T_FLETE.Text });
                    vars.Add(new SPVars { nombre = "carga", valor = T_CARGA.Text });
                    vars.Add(new SPVars { nombre = "otros", valor = T_OTROS.Text });
                    vars.Add(new SPVars { nombre = "detalle_pago", valor = T_DETALLE_PAGO.Text });

                    //string sql = "insert into PV_COMPRA VALUES (id_proveedor = @id_cliemte, fecha = @fecha, id_producto = @id_producto, id_cat_producto = @id_cat_producto, tipo_embalaje = @tipo_embalaje, cantidad = @cantidad, precio = @precio, total = @total, forma_pago = @forma_pago, detalle_pago = @detalle_pago )";
                    string sql = "insert into PV_COMPRA VALUES (@id_proveedor, @fecha_compra, @id_producto, @id_cat_producto,  @tipo_embalaje, @cantidad, @precio, @comision, @flete, @carga, @otros,  @total,  @forma_pago,  @detalle_pago )";
                    db.Scalar2(sql, vars);
                    alert(objeto_mantenedor_global + " creada con éxito", 1);
                    // STOCK
                    int existe = int.Parse(db.Scalar("select count(1) from pv_stock where id_producto = " + CB_PRODUCTO.SelectedValue + " and id_cat_producto = " + CB_CATEGORIA.SelectedValue + " and tipo_embalaje = '" + CB_EMBALAJE.SelectedValue + "'").ToString());
                    
                    if (existe > 0)
                    {
                        int cantidad = int.Parse(db.Scalar("select cantidad from pv_stock where id_producto = " + CB_PRODUCTO.SelectedValue + " and id_cat_producto = " + CB_CATEGORIA.SelectedValue + " and tipo_embalaje = '" + CB_EMBALAJE.SelectedValue + "'").ToString());
                        cantidad += int.Parse(T_CANTIDAD.Text);
                        string sql_stock = "update PV_STOCK set cantidad = " + cantidad + " where id_producto = " + CB_PRODUCTO.SelectedValue + " and id_cat_producto = " + CB_CATEGORIA.SelectedValue + " and tipo_embalaje = '" + CB_EMBALAJE.SelectedValue + "'";
                        db.Scalar2(sql_stock, vars);
                    }
                    else
                    {
                        string sql_stock = "insert into PV_STOCK VALUES (@id_producto, @id_cat_producto, @tipo_embalaje, @cantidad)";
                        db.Scalar2(sql_stock, vars);
                    }        
                }
                LlenarGrilla();
            }
            catch (Exception ex)
            {
                alert("Por favor ingrese todos los datos", 0);
            }

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
                    int cantidad_ = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString()));
                    int id_producto = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[2].ToString()));
                    int id_cat_producto = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[3].ToString()));
                    string tipo_embalaje = (G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[4].ToString());

                    DBUtil db = new DBUtil();
                    int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    db.Scalar("delete from PV_COMPRA where id_Compra = " + id);

                    // STOCK
                    int existe = int.Parse(db.Scalar("select count(1) from pv_stock where id_producto = " + id_producto + " and id_cat_producto = " + id_cat_producto + " and tipo_embalaje = '" + tipo_embalaje + "'").ToString());
                    if (existe > 0)
                    {
                        int cantidad = int.Parse(db.Scalar("select cantidad from pv_stock where id_producto = " + id_producto + " and id_cat_producto = " + id_cat_producto + " and tipo_embalaje = '" + tipo_embalaje + "'").ToString());
                        cantidad = (cantidad - cantidad_);
                        string sql_stock = "update PV_STOCK set cantidad = " + cantidad + " where id_producto = " + id_producto + " and id_cat_producto = " + id_cat_producto + " and tipo_embalaje = '" + tipo_embalaje + "'";
                        db.Scalar(sql_stock);
                    }
                    else
                    {
                        int cantidad2 = (-1 * cantidad_);
                        string sql_stock = "insert into PV_STOCK VALUES (" + id_producto + ", " + id_cat_producto + ", " + tipo_embalaje + ", " + cantidad2 + ")";
                        db.Scalar(sql_stock);
                    }

                    LlenarGrilla();
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

            dt = db.consultar("select * from PV_COMPRA where id_Compra = " + id);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    LIMPIARCAMPOS();
                    T_ID.Text = id.ToString();
                    CB_PRODUCTO.SelectedValue = dr["id_producto"].ToString();
                    t_aux_prod.Text = dr["id_producto"].ToString(); 

                    CB_CATEGORIA.DataSource = db.consultar("select id_Cat_producto, nombre_categoria from pv_producto_cat where id_producto = " + CB_PRODUCTO.SelectedValue + " order by nombre_categoria ");
                    CB_CATEGORIA.DataTextField = "nombre_categoria";
                    CB_CATEGORIA.DataValueField = "id_Cat_producto";
                    CB_CATEGORIA.DataBind();
                    CB_CATEGORIA.Items.Insert(0, new ListItem("-- SELECCIONE --", "0"));
                    CB_CATEGORIA.SelectedValue = dr["id_cat_producto"].ToString();
                    t_aux_cat.Text = dr["id_cat_producto"].ToString();

                    CB_EMBALAJE.SelectedValue = dr["tipo_embalaje"].ToString();
                    t_aux_embalaje.Text = dr["tipo_embalaje"].ToString();
                    CB_PROVEEDOR.SelectedValue = dr["id_proveedor"].ToString();
                    CB_FORMA_PAGO.SelectedValue = dr["forma_pago"].ToString();
                    FECHA_COMPRA.Text = DateTime.Parse(dr["fecha_compra"].ToString()).ToString("yyyy-MM-dd");
                    T_CANTIDAD.Text = dr["cantidad"].ToString();
                    t_aux_cantidad.Text = dr["cantidad"].ToString();
                    T_PRECIO.Text = dr["precio"].ToString();
                    T_TOTAL.Text = dr["total"].ToString();
                    T_COMISION.Text = dr["comision"].ToString();
                    T_FLETE.Text = dr["flete"].ToString();
                    T_CARGA.Text = dr["carga"].ToString();
                    T_OTROS.Text = dr["otros"].ToString();
                    T_DETALLE_PAGO.Text = dr["detalle_pago"].ToString();

                    LBL_ESTADO.Text = "EDITANDO COMPRA ID: " + T_ID.Text;
                    AbreModalGasto();
                }
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
                {
                    if (control.ID != "FITRO_FECHA")
                    {
                        ((TextBox)control).Text = string.Empty;
                    }
                }
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

        protected void CB_PRODUCTO_SelectedIndexChanged(object sender, EventArgs e)
        {
            DBUtil db = new DBUtil();

            CB_CATEGORIA.DataSource = db.consultar("select id_Cat_producto, nombre_categoria from pv_producto_cat where id_producto = " + CB_PRODUCTO.SelectedValue + " order by nombre_categoria ");
            CB_CATEGORIA.DataTextField = "nombre_categoria";
            CB_CATEGORIA.DataValueField = "id_Cat_producto";
            CB_CATEGORIA.DataBind();
            CB_CATEGORIA.Items.Insert(0, new ListItem("-- SELECCIONE --", "0"));
        }

        protected void B_LIMPIAR_CAMPOS_Click(object sender, EventArgs e)
        {
            LIMPIARCAMPOS();
            FECHA_COMPRA.Text = DateTime.Now.ToString("yyyy-MM-dd");
            T_ID.Text = string.Empty;
            LBL_ESTADO.Text = "INGRESANDO NUEVA COMPRA";
            AbreModalGasto();
        }

        public void AbreModalGasto()
        {
            // ABRE MODAL
            ScriptManager.RegisterStartupScript(this, typeof(Page), "abremodalgasto", "<script>javascript:GASTOGENERAL();</script>", false);
        }

        protected void B_FILTRAR_Click(object sender, EventArgs e)
        {
            LlenarGrilla();
        }
    }
}