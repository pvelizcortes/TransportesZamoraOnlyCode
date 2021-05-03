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
    public partial class GD_NEUMATICOS : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Neumatico";
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
                    LlenarCombo();
                    LlenarGrilla();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "grid", "<script>javascript:Datatables();</script>", false);
            }
        }

        public void LlenarCombo()
        {
            DBUtil db = new DBUtil();

            CB_CAMION.DataSource = FN_CAMION.LLENADT(" where activo = 'ACTIVO' order by PATENTE ");
            CB_CAMION.DataTextField = "PATENTE";
            CB_CAMION.DataValueField = "PATENTE";
            CB_CAMION.DataBind();
            CB_CAMION.Items.Insert(0, new ListItem("-- SELECCIONE --", "0"));

            CB_MARCA.DataSource = db.consultar("select NOMBRE_MARCA_NEUMATICO  from GD_MARCAS_NEUMATICO order by NOMBRE_MARCA_NEUMATICO");
            CB_MARCA.DataTextField = "NOMBRE_MARCA_NEUMATICO";
            CB_MARCA.DataValueField = "NOMBRE_MARCA_NEUMATICO";
            CB_MARCA.DataBind();
        }

        public void LlenarGrilla()
        {
            G_PRINCIPAL.DataSource = FN_GD_NEUMATICOS.LLENADT();
            G_PRINCIPAL.DataBind();
        }

        protected void B_NUEVO_Click(object sender, EventArgs e)
        {
            LIMPIARCAMPOS();
            PANEL_DETALLE1.Visible = true;
            PANEL_PRINCIPAL.Visible = false;
            UP_PRINCIPAL.Update();
        }

        protected void B_GUARDAR_Click(object sender, EventArgs e)
        {
            if (T_NUM_INTERNO.Text == "")
            {
                alert("Ingrese un Num. Interno.", 0);
                T_NUM_INTERNO.Focus();
            }
            else if (CB_MARCA.SelectedValue == "-1")
            {
                alert("Seleccione una Marca.", 0);
                CB_MARCA.Focus();
            }
            else if (CB_CAMION.SelectedValue == "-1")
            {
                alert("Seleccione un camión.", 0);
                CB_CAMION.Focus();
            }
            else if (CB_POSICION.SelectedValue == "-1")
            {
                alert("Seleccione una posición.", 0);
                CB_POSICION.Focus();
            }
            else if (T_FECHA_INGRESO.Text == "")
            {
                alert("Seleccione una fecha de ingreso.", 0);
                T_FECHA_INGRESO.Focus();
            }
            else if (T_KM_INGRESO.Text == "")
            {
                alert("Ingrese los Km. iniciales.", 0);
                T_KM_INGRESO.Focus();
            }
            else
            {
                OBJ_GD_NEUMATICOS objeto_mantenedor = new OBJ_GD_NEUMATICOS();
                if (T_ID.Text != "")
                {
                    // EDITAR
                    objeto_mantenedor.ID_NEUMATICO = int.Parse(T_ID.Text);
                    FN_GD_NEUMATICOS.LLENAOBJETO(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        objeto_mantenedor.patente = CB_CAMION.SelectedItem.Text;
                        objeto_mantenedor.num_interno = int.Parse(T_NUM_INTERNO.Text);
                        objeto_mantenedor.marca = CB_MARCA.SelectedItem.Text;
                        objeto_mantenedor.posicion = int.Parse(CB_POSICION.SelectedValue);
                        objeto_mantenedor.fecha_ingreso = DateTime.Parse(T_FECHA_INGRESO.Text);
                        objeto_mantenedor.km_ingreso = int.Parse(T_KM_INGRESO.Text);
                        objeto_mantenedor.presion = T_PRESION.Text;
                        objeto_mantenedor.prof_izq = T_PROF_IZQ.Text;
                        objeto_mantenedor.prof_der = T_PROF_DER.Text;
                        if (RB_SEMI.Checked)
                        {
                            objeto_mantenedor.lugar_neumatico = "SEMI";
                        }
                        else
                        {
                            objeto_mantenedor.lugar_neumatico = "TRACTO";
                        }
                        objeto_mantenedor.motivo_cambio = CB_MOTIVO_CAMBIO.SelectedItem.Text;
                        FN_GD_NEUMATICOS.UPDATE(ref objeto_mantenedor);
                        if (objeto_mantenedor._respok)
                        {
                            alert(objeto_mantenedor_global + " modificado con éxito", 1);
                        }
                    }
                }
                else
                {
                    FN_GD_NEUMATICOS.PREPARAOBJETO(ref objeto_mantenedor);
                    // NUEVO                
                    objeto_mantenedor.patente = CB_CAMION.SelectedItem.Text;
                    objeto_mantenedor.num_interno = int.Parse(T_NUM_INTERNO.Text);
                    objeto_mantenedor.marca = CB_MARCA.SelectedItem.Text;
                    objeto_mantenedor.posicion = int.Parse(CB_POSICION.SelectedValue);
                    objeto_mantenedor.fecha_ingreso = DateTime.Parse(T_FECHA_INGRESO.Text);
                    objeto_mantenedor.km_ingreso = int.Parse(T_KM_INGRESO.Text);
                    objeto_mantenedor.presion = T_PRESION.Text;
                    objeto_mantenedor.prof_izq = T_PROF_IZQ.Text;
                    objeto_mantenedor.prof_der = T_PROF_DER.Text;
                    if (RB_SEMI.Checked)
                    {
                        objeto_mantenedor.lugar_neumatico = "SEMI";
                    }
                    else
                    {
                        objeto_mantenedor.lugar_neumatico = "TRACTO";
                    }
                    objeto_mantenedor.motivo_cambio = CB_MOTIVO_CAMBIO.SelectedItem.Text;
                    //      
                    FN_GD_NEUMATICOS.INSERT(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        T_ID.Text = objeto_mantenedor.ID_NEUMATICO.ToString();
                        alert(objeto_mantenedor_global + " creado con éxito", 1);
                    }
                }
            }
        }

        protected void B_VOLVER_Click(object sender, EventArgs e)
        {
            LlenarGrilla();
            // MOSTRAR / OCULTAR PANEL
            PANEL_PRINCIPAL.Visible = true;
            PANEL_DETALLE1.Visible = false;
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
                    OBJ_GD_NEUMATICOS tabla = new OBJ_GD_NEUMATICOS();
                    tabla.ID_NEUMATICO = id;
                    FN_GD_NEUMATICOS.DELETE(ref tabla);
                    if (tabla._respok)
                    {
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
            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();
            OBJ_GD_NEUMATICOS objeto_mantenedor = new OBJ_GD_NEUMATICOS();

            objeto_mantenedor.ID_NEUMATICO = id;
            FN_GD_NEUMATICOS.LLENAOBJETO(ref objeto_mantenedor);
            if (objeto_mantenedor._respok)
            {
                LIMPIARCAMPOS();
                T_ID.Text = id.ToString();  
                CB_CAMION.SelectedValue = objeto_mantenedor.patente;
                T_NUM_INTERNO.Text = objeto_mantenedor.num_interno.ToString();
                CB_MARCA.SelectedValue = objeto_mantenedor.marca;
                CB_POSICION.SelectedValue = objeto_mantenedor.posicion.ToString();
                T_FECHA_INGRESO.Text = objeto_mantenedor.fecha_ingreso.ToString("yyyy-MM-dd");
                T_KM_INGRESO.Text = objeto_mantenedor.km_ingreso.ToString();
                T_PRESION.Text = objeto_mantenedor.presion;
                T_PROF_IZQ.Text = objeto_mantenedor.prof_izq;               
                T_PROF_DER.Text = objeto_mantenedor.prof_der;
                CB_MOTIVO_CAMBIO.SelectedValue = objeto_mantenedor.motivo_cambio;
                if (objeto_mantenedor.lugar_neumatico == "TRACTO")
                {
                    RB_TRACTO.Checked = true;
                }
                else
                {
                    RB_SEMI.Checked = true;
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
    }
}