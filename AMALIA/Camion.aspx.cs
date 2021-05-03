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
    public partial class Camion : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Camión";
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
            CB_MARCA.DataSource = db.consultar("select * from camion_marca order by nombre_marca_camion");
            CB_MARCA.DataTextField = "NOMBRE_MARCA_CAMION";
            CB_MARCA.DataValueField = "ID_MARCA_CAMION";
            CB_MARCA.DataBind();          
        }

        public void LlenarGrilla()
        {
            G_PRINCIPAL.DataSource = FN_CAMION.LLENADT();
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
            if (T_PATENTE.Text == "")
            {
                alert("Ingrese una patente.", 0);
                T_PATENTE.Focus();
            }
            else
            {
                OBJ_CAMION objeto_mantenedor = new OBJ_CAMION();
                if (T_ID.Text != "")
                {
                    // EDITAR
                    objeto_mantenedor.ID_CAMION = int.Parse(T_ID.Text);
                    FN_CAMION.LLENAOBJETO(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        objeto_mantenedor.patente = T_PATENTE.Text;
                        objeto_mantenedor.num_chasis = T_NUM_CHASIS.Text;
                        objeto_mantenedor.num_motor = T_NUM_MOTOR.Text;
                        objeto_mantenedor.marca = CB_MARCA.SelectedItem.Text;
                        objeto_mantenedor.id_marca_camion = int.Parse(CB_MARCA.SelectedValue);
                        objeto_mantenedor.vin = T_VIN.Text;
                        objeto_mantenedor.modelo = T_MODELO.Text;
                        if (T_ANO.Text != "")
                        {
                            objeto_mantenedor.ano = int.Parse(T_ANO.Text);
                        }
                        objeto_mantenedor.carga = T_CARGA.Text;
                        FN_CAMION.UPDATE(ref objeto_mantenedor);
                        if (objeto_mantenedor._respok)
                        {
                            alert(objeto_mantenedor_global + " modificado con éxito", 1);
                        }
                    }
                }
                else
                {
                    FN_CAMION.PREPARAOBJETO(ref objeto_mantenedor);
                    // NUEVO                
                    objeto_mantenedor.patente = T_PATENTE.Text;
                    objeto_mantenedor.num_chasis = T_NUM_CHASIS.Text;
                    objeto_mantenedor.num_motor = T_NUM_MOTOR.Text;
                    objeto_mantenedor.marca = CB_MARCA.SelectedItem.Text;
                    objeto_mantenedor.id_marca_camion = int.Parse(CB_MARCA.SelectedValue);
                    objeto_mantenedor.vin = T_VIN.Text;
                    objeto_mantenedor.modelo = T_MODELO.Text;
                    if (T_ANO.Text != "")
                    {
                        objeto_mantenedor.ano = int.Parse(T_ANO.Text);
                    }
                    objeto_mantenedor.carga = T_CARGA.Text;
                    objeto_mantenedor.activo = "ACTIVO";
                    //      
                    FN_CAMION.INSERT(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        T_ID.Text = objeto_mantenedor.ID_CAMION.ToString();
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
                    OBJ_CAMION tabla = new OBJ_CAMION();
                    tabla.ID_CAMION = id;
                    FN_CAMION.DELETE(ref tabla);
                    if (tabla._respok)
                    {
                        LlenarGrilla();
                    }
                }
                if (e.CommandName == "Cambiarestado")
                {
                    int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    string estado = G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString();
                    OBJ_CAMION tabla = new OBJ_CAMION();
                    tabla.ID_CAMION = id;
                    FN_CAMION.LLENAOBJETO(ref tabla);
                    if (tabla._respok)
                    {
                        if (estado == "ACTIVO")
                        {
                            tabla.activo = "INACTIVO";
                        }
                        else
                        {
                            tabla.activo = "ACTIVO";
                        }
                        FN_CAMION.UPDATE(ref tabla);
                        if (tabla._respok)
                        {
                            LlenarGrilla();
                        }
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
            OBJ_CAMION objeto_mantenedor = new OBJ_CAMION();

            objeto_mantenedor.ID_CAMION = id;
            FN_CAMION.LLENAOBJETO(ref objeto_mantenedor);
            if (objeto_mantenedor._respok)
            {
                LIMPIARCAMPOS();
                T_ID.Text = id.ToString();
                T_PATENTE.Text = objeto_mantenedor.patente;
                T_NUM_CHASIS.Text = objeto_mantenedor.num_chasis;
                T_NUM_MOTOR.Text = objeto_mantenedor.num_motor;
                T_VIN.Text = objeto_mantenedor.vin;
                CB_MARCA.SelectedValue = objeto_mantenedor.id_marca_camion.ToString();
                T_MODELO.Text = objeto_mantenedor.modelo;
                T_ANO.Text = objeto_mantenedor.ano.ToString();
                T_CARGA.Text = objeto_mantenedor.carga;
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