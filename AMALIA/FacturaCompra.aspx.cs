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
    public partial class FacturaCompra : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Compra";
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
                    CargarCombos();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "grid", "<script>javascript:Datatables();</script>", false);
            }
        }
        protected void CB_CAMION_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_CAMION.SelectedValue != "-1")
            {
                LlenarGrilla();
                LlenarEncabezado();
                PANEL_PRINCIPAL.Visible = true;
                UP_PRINCIPAL.Update();
            }
        }
        public void LlenarGrilla()
        {
            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();
            dt = db.consultar("select * from V_FACTURAS_COMPRA where id_camion = " + CB_CAMION.SelectedValue + " order by fecha_compra desc;");
            G_PRINCIPAL.DataSource = dt;
            G_PRINCIPAL.DataBind();

            string html = "";

            if (dt.Rows.Count > 0)
            {
                html = "<small>Ultimo ingreso: " + dt.Rows[0]["tipo_documento"] + " Nº <b>" + dt.Rows[0]["num_documento"] + "</b>, " + dt.Rows[0]["nom_proveedor"] + ". Ingresada por el usuario: " + dt.Rows[0]["usuario"] + " el " + dt.Rows[0]["fecha_creacion"] + " </small>";               
            }
            else
            {
                html = "" +
                 "<p class='text-muted m-b-0'>Ultimo ingreso: Sin registro </p>";
            }

            ENC_CAMION.InnerHtml = html;
            DIV_PATENTE.InnerHtml = CB_CAMION.SelectedItem.Text;

        }

        public void LlenarEncabezado()
        {
            DBUtil db = new DBUtil();
            double total = Convert.ToDouble(db.Scalar("select isnull(sum(total),0) from FACT_COMPRA where id_camion = " + CB_CAMION.SelectedValue).ToString());
            double total_docs = Convert.ToDouble(db.Scalar("select count(1) from fact_compra_det det inner join fact_compra fact on fact.id_compra = det.id_compra where fact.id_camion = " + CB_CAMION.SelectedValue).ToString());
            double total_compras = Convert.ToDouble(db.Scalar("select count(1) from fact_compra where id_camion = " + CB_CAMION.SelectedValue).ToString());
            double sin_pagar = Convert.ToDouble(db.Scalar("select count(1) from fact_compra where estado != 'PAGADO' and id_camion = " + CB_CAMION.SelectedValue));

            DIV_TOTAL.InnerHtml = "$" + total.ToString("#,##0");
            DIV_NUM_DOCS.InnerHtml = total_docs.ToString("#,##0");
            DIV_NUM_COMPRAS.InnerHtml = total_compras.ToString("#,##0");
            DIV_SIN_PAGAR.InnerHtml = sin_pagar.ToString("#,##0");
        }
        public void CargarCombos()
        {
            // Llenar combo camión
            DBUtil db = new DBUtil();
            CB_CAMION.DataSource = FN_CAMION.LLENADT(" where activo = 'ACTIVO' order by PATENTE ");
            CB_CAMION.DataTextField = "PATENTE";
            CB_CAMION.DataValueField = "ID_CAMION";
            CB_CAMION.DataBind();
            CB_CAMION.Items.Insert(0, new ListItem("-- SELECCIONE CENTRO DE COSTO --", "-1"));
            CB_CAMION.SelectedValue = "-1";

            CB_PROVEEDOR.DataSource = FN_PROVEEDOR.LLENADT(" order by nom_proveedor");
            CB_PROVEEDOR.DataTextField = "NOM_PROVEEDOR";
            CB_PROVEEDOR.DataValueField = "ID_PROVEEDOR";
            CB_PROVEEDOR.DataBind();
            CB_PROVEEDOR.Items.Insert(0, new ListItem("-- SELECCIONE --", "-1"));
            CB_PROVEEDOR.SelectedValue = "-1";
        }

        protected void B_NUEVO_Click(object sender, EventArgs e)
        {
            LIMPIARCAMPOS();
            AbrirModal();
            UPMODAL.Update();
            mensaje_guardar.Visible = true;
            div_cargar_docs.Visible = false;
        }

        protected void B_GUARDAR_Click(object sender, EventArgs e)
        {
            if (CB_CAMION.SelectedValue == "-1")
            {
                alert("Seleccione Centro de Costo.", 0);
            }
            else if (CB_PROVEEDOR.SelectedValue == "-1")
            {
                alert("Seleccione un Proveedor.", 0);
            }
            else if (T_NUM_DOC.Text == "")
            {
                alert("Ingrese un numero de documento.", 0);
            }
            else if (T_TOTAL.Text == "")
            {
                alert("Ingrese un total.", 0);
            }
            else if (FECHA_DOCUMENTO.Text == "")
            {
                alert("Ingrese la fecha de compra.", 0);
            }
            else
            {
                OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                usuario.usuario = HttpContext.Current.User.Identity.Name;
                FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);
                mensaje_guardar.Visible = false;
                OBJ_FACT_COMPRA fac = new OBJ_FACT_COMPRA();

                if (T_ID.Text == "")
                {
                    // NUEVO
                    fac.id_camion = int.Parse(CB_CAMION.SelectedValue);
                    fac.fecha_creacion = DateTime.Now;
                    fac.id_usuario = usuario.ID_USUARIO;
                    fac.tipo_documento = CB_TIPO_DOCUMENTO.SelectedValue;
                    fac.num_documento = T_NUM_DOC.Text;
                    fac.proveedor = int.Parse(CB_PROVEEDOR.SelectedValue);
                    fac.detalle = T_DETALLE.Text;
                    fac.total = int.Parse(T_TOTAL.Text);
                    fac.estado = CB_ESTADO_DOC.SelectedValue;
                    fac.fecha_compra = Convert.ToDateTime(FECHA_DOCUMENTO.Text);
                    FN_FACT_COMPRA.INSERT(ref fac);
                    if (fac._respok)
                    {
                        div_cargar_docs.Visible = true;
                        T_ID.Text = fac.ID_COMPRA.ToString();
                        LlenarGrillaDocs();
                        LlenarGrilla();
                        alert("Guardado con éxito", 1);
                        UP_PRINCIPAL.Update();
                    }
                    else
                    {
                        alert("Problemas al guardar el documento", 0);
                    }
                }
                else
                {
                    fac.ID_COMPRA = int.Parse(T_ID.Text);
                    FN_FACT_COMPRA.LLENAOBJETO(ref fac);
                    if (fac._respok)
                    {
                        fac.tipo_documento = CB_TIPO_DOCUMENTO.SelectedValue;
                        fac.num_documento = T_NUM_DOC.Text;
                        fac.proveedor = int.Parse(CB_PROVEEDOR.SelectedValue);
                        fac.detalle = T_DETALLE.Text;
                        fac.total = int.Parse(T_TOTAL.Text);
                        fac.estado = CB_ESTADO_DOC.SelectedValue;
                        fac.fecha_compra = Convert.ToDateTime(FECHA_DOCUMENTO.Text);
                        // MODIFICAR
                        FN_FACT_COMPRA.UPDATE(ref fac);
                        if (fac._respok)
                        {
                            LlenarGrillaDocs();
                            LlenarGrilla();
                            alert("Modificado con éxito", 1);
                            UP_PRINCIPAL.Update();
                        }
                        else
                        {
                            alert("Problemas al modificar el documento", 0);
                        }
                    }
                }
            }
        }


        public void LlenarGrillaDocs()
        {
            DBUtil db = new DBUtil();
            G_DETALLE_DOCS.DataSource = db.consultar("select * from FACT_COMPRA_DET where id_compra = " + T_ID.Text + ";");
            G_DETALLE_DOCS.DataBind();
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
                    int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    OBJ_FACT_COMPRA fact = new OBJ_FACT_COMPRA();
                    fact.ID_COMPRA = id;
                    FN_FACT_COMPRA.DELETE(ref fact);
                    if (fact._respok)
                    {
                        DBUtil db = new DBUtil();
                        db.Scalar("delete from FACT_COMPRA_DET where id_compra = " + id);
                        alert("Documento de compra eliminado con éxito", 0);
                        LlenarGrilla();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void COMPLETAR_DETALLE(int id)
        {
            LIMPIARCAMPOS();
            OBJ_FACT_COMPRA fact = new OBJ_FACT_COMPRA();
            fact.ID_COMPRA = id;

            FN_FACT_COMPRA.LLENAOBJETO(ref fact);
            if (fact._respok)
            {
                T_ID.Text = fact.ID_COMPRA.ToString();
                CB_TIPO_DOCUMENTO.SelectedValue = fact.tipo_documento;
                T_NUM_DOC.Text = fact.num_documento;
                CB_PROVEEDOR.SelectedValue = fact.proveedor.ToString();
                FECHA_DOCUMENTO.Text = fact.fecha_compra.ToString("yyyy-MM-dd");
                T_DETALLE.Text = fact.detalle;
                T_TOTAL.Text = fact.total.ToString();
                CB_ESTADO_DOC.SelectedValue = fact.estado;
                //
                mensaje_guardar.Visible = false;
                div_cargar_docs.Visible = true;
                LlenarGrillaDocs();
                AbrirModal();
                UPMODAL.Update();
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
                    ((TextBox)control).Text = string.Empty;
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


        public void AbrirModal()
        {
            // ABRE MODAL
            ScriptManager.RegisterStartupScript(this, typeof(Page), "abrirmodal", "<script>javascript:ABREMODAL();</script>", false);
        }

        protected void B_AGREGAR_URL_DOC_Click(object sender, EventArgs e)
        {
            if (T_URL_DOCUMENTO.Text == "" || T_URL_DOCUMENTO.Text.Length <= 3)
            {
                alert("Ingrese un link válido", 0);
            }
            else
            {
                OBJ_FACT_COMPRA_DET facdet = new OBJ_FACT_COMPRA_DET();
                facdet.id_compra = int.Parse(T_ID.Text);
                facdet.fecha_subida = DateTime.Now;
                facdet.extension = " ";
                facdet.nombre_archivo = " ";
                facdet.nombre_archivo_server = " ";
                facdet.path_local = " ";
                facdet.url_doc = T_URL_DOCUMENTO.Text;
                FN_FACT_COMPRA_DET.INSERT(ref facdet);
                if (facdet._respok)
                {
                    alert("Documento adjuntado con éxito", 1);
                    LlenarGrillaDocs();
                }
                else
                {
                    alert("No se pudo adjuntar documento, revise los datos", 0);
                }
            }
        }

        protected void G_DETALLE_DOCS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {
                int id = int.Parse((G_DETALLE_DOCS.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                OBJ_FACT_COMPRA_DET det = new OBJ_FACT_COMPRA_DET();
                det.ID_COMPRA_DET = id;
                FN_FACT_COMPRA_DET.LLENAOBJETO(ref det);
                if (det._respok)
                {
                    Response.Write(String.Format("window.open('{0}','_blank')", det.url_doc));
                }
                else
                {
                    alert("No se pudo encontrar el documento", 0);
                }
            }
            if (e.CommandName == "Borrar")
            {
                int id = int.Parse((G_DETALLE_DOCS.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                OBJ_FACT_COMPRA_DET det = new OBJ_FACT_COMPRA_DET();
                det.ID_COMPRA_DET = id;
                FN_FACT_COMPRA_DET.DELETE(ref det);
                if (det._respok)
                {
                    alert("Eliminado con éxito", 0);
                    LlenarGrillaDocs();
                }
            }
        }

        protected void G_PRINCIPAL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string estado = G_PRINCIPAL.DataKeys[e.Row.RowIndex].Values[1].ToString();
                // ESTADO
                HtmlGenericControl spnHtml = (HtmlGenericControl)e.Row.FindControl("div_estado");
                if (estado == "PAGADO")
                {
                    spnHtml.InnerHtml = "<b><span style='width:90%;background-color:green;color:white;' class='badge badge-success'>PAGADO</span></b>";
                }
                else if (estado == "NO PAGADO")
                {
                    spnHtml.InnerHtml = "<b><span style='width:90%;background-color:red;color:white;' class='badge badge-danger'>NO PAGADO</span></b>";
                }
                else
                {
                    spnHtml.InnerHtml = "<b><span style='width:90%;background-color:gray;color:white;' class='badge badge-default'>SIN ESTADO</span></b>";
                }
            }
        }
    }
}