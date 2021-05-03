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
    public partial class Ingresar_Requerimiento : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Requerimiento";
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
                }
            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, typeof(Page), "combopro", "<script>javascript:ComboPro();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "relojreq2", "<script>javascript:relojito(false);</script>", false);
            }
        }

        public void LlenarCombo()
        {
            DBUtil db = new DBUtil();

            CB_CAMION.DataSource = FN_CAMION.LLENADT(" where activo = 'ACTIVO' order by PATENTE ");
            CB_CAMION.DataTextField = "PATENTE";
            CB_CAMION.DataValueField = "ID_CAMION";
            CB_CAMION.DataBind();
            CB_CAMION.Items.Insert(0, new ListItem("-- SELECCIONE --", "0"));

            CB_RAMPLA.DataSource = FN_RAMPLA.LLENADT(" where activo = 'ACTIVO' order by PATENTE ");
            CB_RAMPLA.DataTextField = "PATENTE";
            CB_RAMPLA.DataValueField = "ID_RAMPLA";
            CB_RAMPLA.DataBind();
            CB_RAMPLA.Items.Insert(0, new ListItem("-- SELECCIONE --", "0"));

            CB_CONDUCTOR.DataSource = FN_CONDUCTOR.LLENADT(" where activo = 'ACTIVO' order by NOMBRE_COMPLETO ");
            CB_CONDUCTOR.DataTextField = "NOMBRE_COMPLETO";
            CB_CONDUCTOR.DataValueField = "ID_CONDUCTOR";
            CB_CONDUCTOR.DataBind();
            CB_CONDUCTOR.Items.Insert(0, new ListItem("-- SELECCIONE --", "0"));
        }

        protected void B_NUEVO_Click(object sender, EventArgs e)
        {
            LIMPIARCAMPOS();          
            UP_PRINCIPAL.Update();
        }

        protected void B_GUARDAR_Click(object sender, EventArgs e)
        {

            if (CB_CAMION.SelectedValue == "0" && CB_RAMPLA.SelectedValue == "0")
            {
                alert("Seleccione un camión o rampla.", 0);
                CB_CAMION.Focus();
            }
            else if (CB_CONDUCTOR.SelectedValue == "0")
            {
                alert("Seleccione un conductor.", 0);
                CB_CONDUCTOR.Focus();
            }
            else if (T_DESCRIPCION.Text == "")
            {
                alert("Ingrese una descripcion.", 0);
                T_DESCRIPCION.Focus();
            }
            else
            {
                OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                usuario.usuario = HttpContext.Current.User.Identity.Name;
                FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);
                OBJ_REQUERIMIENTOS objeto_mantenedor = new OBJ_REQUERIMIENTOS();
                if (T_ID.Text != "")
                {
                    // EDITAR
                    objeto_mantenedor.ID_REQUERIMIENTO = int.Parse(T_ID.Text);
                    FN_REQUERIMIENTOS.LLENAOBJETO(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        //objeto_mantenedor.usuario = T_USUARIO.Text;
                        objeto_mantenedor.id_camion = int.Parse(CB_CAMION.SelectedValue.ToString());
                        objeto_mantenedor.id_rampla = int.Parse(CB_RAMPLA.SelectedValue.ToString());
                        objeto_mantenedor.id_conductor = int.Parse(CB_CONDUCTOR.SelectedValue.ToString());
                        objeto_mantenedor.descripcion = T_DESCRIPCION.Text;
                        objeto_mantenedor.urgencia = CB_PRIORIDAD.SelectedValue;                                  
                        objeto_mantenedor.solicitante = CB_CONDUCTOR.Text;                                    
                        FN_REQUERIMIENTOS.UPDATE(ref objeto_mantenedor);
                        if (objeto_mantenedor._respok)
                        {
                            alert(objeto_mantenedor_global + " modificado con éxito", 1);
                           
                        }
                    }
                }
                else
                {
                    FN_REQUERIMIENTOS.PREPARAOBJETO(ref objeto_mantenedor);
                    // NUEVO                
                    objeto_mantenedor.id_camion = int.Parse(CB_CAMION.SelectedValue.ToString());
                    objeto_mantenedor.id_rampla = int.Parse(CB_RAMPLA.SelectedValue.ToString());
                    objeto_mantenedor.id_conductor = int.Parse(CB_CONDUCTOR.SelectedValue.ToString());
                    objeto_mantenedor.descripcion = T_DESCRIPCION.Text;
                    objeto_mantenedor.urgencia = CB_PRIORIDAD.SelectedValue;
                    objeto_mantenedor.fecha = DateTime.Now;
                    objeto_mantenedor.id_usuario = usuario.ID_USUARIO;
                    objeto_mantenedor.solicitante = CB_CONDUCTOR.Text;
                    objeto_mantenedor.id_ot = 0;
                    objeto_mantenedor.resolucion = "";
                    objeto_mantenedor.estado = "NUEVO REQUERIMIENTO";
                    //      
                    FN_REQUERIMIENTOS.INSERT(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        T_ID.Text = objeto_mantenedor.ID_REQUERIMIENTO.ToString();
                        alert(objeto_mantenedor_global + " creado con éxito", 1);
                        LIMPIARCAMPOS();
                    }
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

        #endregion

    }
}