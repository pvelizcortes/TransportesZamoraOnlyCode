using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AMALIAFW;
using AMALIA;

namespace AMALIA
{
    public partial class CombustibleControl : System.Web.UI.Page
    {
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
                    OBJ_USUARIOS us = new OBJ_USUARIOS();
                    us.usuario = HttpContext.Current.User.Identity.Name;               
                    if (us.usuario != "lacosta" && us.usuario != "mzapata" && us.usuario != "felipe" && us.usuario != "festay" && us.usuario != "gestay")
                    {
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        if (us.usuario != "lacosta")
                        {
                            Crear_Nuevo.Visible = false;
                            div_read_only.Disabled = true;
                        }  
                    }
                    DBUtil db = new DBUtil();
                    G_OTZ.DataSource = db.consultar(" select top(200) * from COMB_LOG order by id_log desc ");
                    G_OTZ.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "modulogtotz", "<script>javascript:DT_OTZ();</script>", false);
            }

        }
        protected void B_FILTRAR_Click(object sender, EventArgs e)
        {
            LlenarGrilla();
        }

        public void LlenarGrilla()
        {
            string filtro = " where 1=1 ";
            string desde = FILTRO_FECHA_DESDE.Text;
            string hasta = FILTRO_FECHA_HASTA.Text;
            try
            {
                if (desde != "")
                {
                    desde = " and fecha >= convert(date, '" + DateTime.Parse(desde).ToString("dd/MM/yyyy") + "', 103) ";
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
                    hasta = " and fecha <= convert(date, '" + DateTime.Parse(hasta).ToString("dd/MM/yyyy") + "', 103) ";
                }
            }
            catch (Exception ex)
            {
                hasta = "";
            }
            filtro += desde + hasta;

            DBUtil db = new DBUtil();
            G_OTZ.DataSource = db.consultar(" select * from COMB_LOG " + filtro + " order by id_log desc ");
            G_OTZ.DataBind();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "asd12e12e1aaaa2", "<script>javascript:relojito(false);</script>", false);
        }

        protected void G_OTZ_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                OBJ_COMB_LOG otz = new OBJ_COMB_LOG();
                int id = int.Parse((G_OTZ.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                otz.ID_LOG = id;
                FN_COMB_LOG.LLENAOBJETO(ref otz);
                if (otz._respok)
                {
                    if (e.CommandName == "Editar")
                    {
                        LimpiarCamposOTZ();
                        COMPLETAR_OTZ(id);
                        AbreModalOTZ();
                    }
                    if (e.CommandName == "Borrar")
                    {
                        DBUtil db = new DBUtil();
                        db.Scalar("delete from comb_log where id_log = " + id);
                        if (FILTRO_FECHA_DESDE.Text != "" || FILTRO_FECHA_HASTA.Text != "")
                        {
                            LlenarGrilla();
                        }
                        else
                        {
                            G_OTZ.DataSource = db.consultar(" select top(200) * from COMB_LOG order by id_log desc ");
                            G_OTZ.DataBind();
                        }                    
                     
                    }
                }
            }
            catch (Exception ex)
            {
                alert("Error al guardar revise que el valor sea numerico", 0);
            }
        }

        protected void alert(string mensaje, int flag)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "modulotz", "<script>javascript:MostrarNotificacion('" + mensaje + "', " + flag + ");</script>", false);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            MakeAccessible(G_OTZ);
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

        public void AbreModalOTZ()
        {
            // ABRE MODAL
            ScriptManager.RegisterStartupScript(this, typeof(Page), "abremodalotz", "<script>javascript:OTZ();</script>", false);
        }

        public void COMPLETAR_OTZ(int id)
        {
            OBJ_COMB_LOG objeto_mantenedor = new OBJ_COMB_LOG();
            objeto_mantenedor.ID_LOG = id;
            FN_COMB_LOG.LLENAOBJETO(ref objeto_mantenedor);

            if (objeto_mantenedor._respok)
            {
                T_ID_LOG.Text = id.ToString();
                modal_fecha.Text = objeto_mantenedor.fecha.ToString("yyyy-MM-dd");
                modal_num_guia.Text = objeto_mantenedor.guia.ToString();
                modal_surtidor_inicio.Text = objeto_mantenedor.cont_surtidor_inicial.ToString();
                modal_surtidor_final.Text = objeto_mantenedor.cont_surtidor_final.ToString();
                modal_km_camion.Text = objeto_mantenedor.km_camion.ToString();
                modal_nombre_conductor.Text = objeto_mantenedor.nombre_conductor.ToString();
                modal_patente_camion.Text = objeto_mantenedor.patente_camion.ToString().ToUpper();
                modallitros.Text = objeto_mantenedor.litros.ToString();
                modal_stock_estanque.Text = objeto_mantenedor.stock_estanque.ToString();
                modal_precio.Text = objeto_mantenedor.precio.ToString();
                modal_total.Text = objeto_mantenedor.total.ToString();
                modal_factura_asociada.Text = objeto_mantenedor.factura_asociada;
                modal_observacion.Text = objeto_mantenedor.observacion;
                UP_OTZ.Update();
            }

        }

        protected void B_GUARDAR_OTZ_Click(object sender, EventArgs e)
        {
            try
            {
                OBJ_COMB_LOG objeto_mantenedor = new OBJ_COMB_LOG();
                OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                usuario.usuario = HttpContext.Current.User.Identity.Name;
                FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);
                if (T_ID_LOG.Text != "")
                {
                    // EDITAR           
                    objeto_mantenedor.ID_LOG = int.Parse(T_ID_LOG.Text);
                    FN_COMB_LOG.LLENAOBJETO(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        objeto_mantenedor.id_usuario = usuario.ID_USUARIO;
                        objeto_mantenedor.fecha = Convert.ToDateTime(modal_fecha.Text);
                        objeto_mantenedor.guia = modal_num_guia.Text;
                        objeto_mantenedor.cont_surtidor_inicial = float.Parse(modal_surtidor_inicio.Text);
                        objeto_mantenedor.cont_surtidor_final = float.Parse(modal_surtidor_final.Text);
                        objeto_mantenedor.km_camion = int.Parse(modal_km_camion.Text);
                        objeto_mantenedor.nombre_conductor = modal_nombre_conductor.Text;
                        objeto_mantenedor.patente_camion = modal_patente_camion.Text.ToUpper();
                        objeto_mantenedor.litros = int.Parse(modallitros.Text);
                        objeto_mantenedor.stock_estanque = int.Parse(modal_stock_estanque.Text);
                        objeto_mantenedor.precio = int.Parse(modal_precio.Text);
                        objeto_mantenedor.total = int.Parse(modal_total.Text);
                        objeto_mantenedor.factura_asociada = modal_factura_asociada.Text;
                        objeto_mantenedor.observacion = modal_observacion.Text;


                        FN_COMB_LOG.UPDATE(ref objeto_mantenedor);
                        if (objeto_mantenedor._respok)
                        {
                            DBUtil db = new DBUtil();
                            G_OTZ.DataSource = db.consultar(" select top(200) * from COMB_LOG order by id_log desc ");
                            G_OTZ.DataBind();
                            alert("Control combustible modificado con éxito", 1);
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrarmodalASDFASDFSA", "<script>javascript:CerrarModalOTZ();</script>", false);
                        }
                    }
                }
                else
                {
                    objeto_mantenedor.id_usuario = usuario.ID_USUARIO;
                    objeto_mantenedor.fecha = Convert.ToDateTime(modal_fecha.Text);
                    objeto_mantenedor.guia = modal_num_guia.Text;
                    objeto_mantenedor.cont_surtidor_inicial = float.Parse(modal_surtidor_inicio.Text);
                    objeto_mantenedor.cont_surtidor_final = float.Parse(modal_surtidor_final.Text);
                    objeto_mantenedor.km_camion = int.Parse(modal_km_camion.Text);
                    objeto_mantenedor.nombre_conductor = modal_nombre_conductor.Text;
                    objeto_mantenedor.patente_camion = modal_patente_camion.Text.ToUpper();
                    objeto_mantenedor.litros = int.Parse(modallitros.Text);
                    objeto_mantenedor.stock_estanque = int.Parse(modal_stock_estanque.Text);
                    objeto_mantenedor.precio = int.Parse(modal_precio.Text);
                    objeto_mantenedor.total = int.Parse(modal_total.Text);
                    objeto_mantenedor.factura_asociada = modal_factura_asociada.Text;
                    objeto_mantenedor.observacion = modal_observacion.Text;

                    FN_COMB_LOG.INSERT(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        DBUtil db = new DBUtil();
                        G_OTZ.DataSource = db.consultar(" select top(200) * from COMB_LOG order by id_log desc ");
                        G_OTZ.DataBind();
                        alert("Control combustible creado con éxito", 1);
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrarmodalASDFASDFSA", "<script>javascript:CerrarModalOTZ();</script>", false);

                    }
                }
            }
            catch (Exception ex)
            {
                alert("Debe completar todos los campos", 0);
            }

        }

        public void LimpiarCamposOTZ()
        {
            T_ID_LOG.Text = string.Empty;
            modal_fecha.Text = string.Empty;
            modal_num_guia.Text = string.Empty;
            modal_surtidor_inicio.Text = string.Empty;
            modal_surtidor_final.Text = string.Empty;
            modal_km_camion.Text = string.Empty;
            modal_nombre_conductor.Text = string.Empty;
            modal_patente_camion.Text = string.Empty;
            modallitros.Text = string.Empty;
            modal_stock_estanque.Text = string.Empty;
            modal_precio.Text = string.Empty;
            modal_total.Text = string.Empty;
            modal_factura_asociada.Text = string.Empty;
            modal_observacion.Text = string.Empty;

        }

        protected void Crear_Nuevo_Click(object sender, EventArgs e)
        {
            LimpiarCamposOTZ();
            AbreModalOTZ();
            UP_OTZ.Update();

        }
    }
}