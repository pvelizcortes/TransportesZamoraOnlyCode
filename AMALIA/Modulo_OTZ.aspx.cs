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
    public partial class Modulo_OTZ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DBUtil db = new DBUtil();
                G_OTZ.DataSource = db.consultar(" select top(100) * from V_OTZ order by correlativo_otz desc ");
                G_OTZ.DataBind();
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
            if (FILTRA_OTZ_DESDE.Text != "")
            {
                filtro += " and correlativo_otz >= " + FILTRA_OTZ_DESDE.Text;
            }
            if (FILTRA_OTZ_HASTA.Text != "")
            {
                filtro += " and correlativo_otz <= " + FILTRA_OTZ_HASTA.Text;
            }
            if (FILTRA_GT_DESDE.Text != "")
            {
                filtro += " and correlativo_gt >= " + FILTRA_GT_DESDE.Text;
            }
            if (FILTRA_GT_HASTA.Text != "")
            {
                filtro += " and correlativo_gt <= " + FILTRA_GT_HASTA.Text;
            }

            DBUtil db = new DBUtil();
            G_OTZ.DataSource = db.consultar(" select * from V_OTZ " + filtro + " order by correlativo_otz desc ");
            G_OTZ.DataBind();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "asd12e12e1aaaa2", "<script>javascript:relojito2(false);</script>", false);
        }

        protected void G_OTZ_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                OBJ_ENC_OTZ otz = new OBJ_ENC_OTZ();
                int id = int.Parse((G_OTZ.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                otz.ID_OTZ = id;
                FN_ENC_OTZ.LLENAOBJETO(ref otz);
                if (otz._respok)
                {
                    if (e.CommandName == "Editar")
                    {
                        LimpiarCamposOTZ();
                        COMPLETAR_OTZ(id);
                        TITULO_MODAL_OTZ.InnerHtml = "EDITANDO <b>OTZ</b>: " + otz.correlativo_otz;
                        AbreModalOTZ();
                    }
                    if (e.CommandName == "sol_oc")
                    {
                        TextBox txt = (TextBox)G_OTZ.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("t_d_sol_oc");
                        otz.d_sol_oc = txt.Text;
                        FN_ENC_OTZ.UPDATE(ref otz);
                        if (otz._respok)
                        {
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Error al modificar", 0);
                        }
                    }
                    if (e.CommandName == "d_ot")
                    {
                        TextBox txt = (TextBox)G_OTZ.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("t_d_ot");
                        otz.d_ot = txt.Text;
                        FN_ENC_OTZ.UPDATE(ref otz);
                        if (otz._respok)
                        {
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Error al modificar", 0);
                        }
                    }
                    if (e.CommandName == "guia")
                    {
                        TextBox txt = (TextBox)G_OTZ.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("t_guia");
                        otz.guia = txt.Text;
                        FN_ENC_OTZ.UPDATE(ref otz);
                        if (otz._respok)
                        {
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Error al modificar", 0);
                        }
                    }
                    if (e.CommandName == "d_eepp")
                    {
                        TextBox txt = (TextBox)G_OTZ.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("t_d_eepp");
                        otz.d_eepp = txt.Text;
                        FN_ENC_OTZ.UPDATE(ref otz);
                        if (otz._respok)
                        {
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Error al modificar", 0);
                        }
                    }
                    if (e.CommandName == "d_gasto")
                    {
                        TextBox txt = (TextBox)G_OTZ.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("t_d_gasto");
                        otz.d_gasto = txt.Text;
                        FN_ENC_OTZ.UPDATE(ref otz);
                        if (otz._respok)
                        {
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Error al modificar", 0);
                        }
                    }
                    if (e.CommandName == "d_oc")
                    {
                        TextBox txt = (TextBox)G_OTZ.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("t_d_oc");
                        otz.d_oc = txt.Text;
                        FN_ENC_OTZ.UPDATE(ref otz);
                        if (otz._respok)
                        {
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Error al modificar", 0);
                        }
                    }
                    if (e.CommandName == "d_hes")
                    {
                        TextBox txt = (TextBox)G_OTZ.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("t_d_hes");
                        otz.d_hes = txt.Text;
                        FN_ENC_OTZ.UPDATE(ref otz);
                        if (otz._respok)
                        {
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Error al modificar", 0);
                        }
                    }
                    if (e.CommandName == "d_factura")
                    {
                        TextBox txt = (TextBox)G_OTZ.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("t_d_factura");
                        otz.d_factura = txt.Text;
                        FN_ENC_OTZ.UPDATE(ref otz);
                        if (otz._respok)
                        {
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Error al modificar", 0);
                        }
                    }
                    if (e.CommandName == "valor_viaje")
                    {
                        TextBox txt = (TextBox)G_OTZ.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("t_valor_viaje");
                        otz.valor_viaje = int.Parse(txt.Text);
                        FN_ENC_OTZ.UPDATE(ref otz);
                        if (otz._respok)
                        {
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Error al modificar", 0);
                        }
                    }
                    if (e.CommandName == "estadia")
                    {
                        TextBox txt = (TextBox)G_OTZ.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("t_estadia");
                        otz.estadia = int.Parse(txt.Text);
                        FN_ENC_OTZ.UPDATE(ref otz);
                        if (otz._respok)
                        {
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Error al modificar", 0);
                        }
                    }
                    if (e.CommandName == "entradas")
                    {
                        TextBox txt = (TextBox)G_OTZ.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("t_entradas");
                        otz.entradas = int.Parse(txt.Text);
                        FN_ENC_OTZ.UPDATE(ref otz);
                        if (otz._respok)
                        {
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Error al modificar", 0);
                        }
                    }
                    if (e.CommandName == "doble_conductor")
                    {
                        TextBox txt = (TextBox)G_OTZ.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("t_doble_conductor");
                        otz.doble_conductor = int.Parse(txt.Text);
                        FN_ENC_OTZ.UPDATE(ref otz);
                        if (otz._respok)
                        {
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Error al modificar", 0);
                        }
                    }
                    if (e.CommandName == "carga_descarga")
                    {
                        TextBox txt = (TextBox)G_OTZ.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("t_carga_descarga");
                        otz.carga_descarga = int.Parse(txt.Text);
                        FN_ENC_OTZ.UPDATE(ref otz);
                        if (otz._respok)
                        {
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Error al modificar", 0);
                        }
                    }
                    if (e.CommandName == "flete_de_tercero")
                    {
                        TextBox txt = (TextBox)G_OTZ.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("t_flete_de_tercero");
                        otz.flete_de_tercero = int.Parse(txt.Text);
                        FN_ENC_OTZ.UPDATE(ref otz);
                        if (otz._respok)
                        {
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Error al modificar", 0);
                        }
                    }
                    if (e.CommandName == "otros")
                    {
                        TextBox txt = (TextBox)G_OTZ.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("t_otros");
                        otz.otros = int.Parse(txt.Text);
                        FN_ENC_OTZ.UPDATE(ref otz);
                        if (otz._respok)
                        {
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Error al modificar", 0);
                        }
                    }
                    if (e.CommandName == "detalle_otros")
                    {
                        TextBox txt = (TextBox)G_OTZ.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("t_detalle_otros");
                        otz.detalle_otros = txt.Text;
                        FN_ENC_OTZ.UPDATE(ref otz);
                        if (otz._respok)
                        {
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Error al modificar", 0);
                        }
                    }
                    if (e.CommandName == "d_nombre_tercero")
                    {
                        TextBox txt = (TextBox)G_OTZ.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("t_d_nombre_tercero");
                        otz.d_nombre_tercero = txt.Text;
                        FN_ENC_OTZ.UPDATE(ref otz);
                        if (otz._respok)
                        {
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Error al modificar", 0);
                        }
                    }
                    if (e.CommandName == "d_eepp_tercero")
                    {
                        TextBox txt = (TextBox)G_OTZ.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("t_d_eepp_tercero");
                        otz.d_eepp_tercero = txt.Text;
                        FN_ENC_OTZ.UPDATE(ref otz);
                        if (otz._respok)
                        {
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Error al modificar", 0);
                        }
                    }
                    if (e.CommandName == "d_factura_tercero")
                    {
                        TextBox txt = (TextBox)G_OTZ.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("t_d_factura_tercero");
                        otz.d_factura_tercero = txt.Text;
                        FN_ENC_OTZ.UPDATE(ref otz);
                        if (otz._respok)
                        {
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Error al modificar", 0);
                        }
                    }
                }
                //editar
                //LlenarGrilla();
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
            OBJ_ENC_OTZ objeto_mantenedor = new OBJ_ENC_OTZ();
            objeto_mantenedor.ID_OTZ = id;
            FN_ENC_OTZ.LLENAOBJETO(ref objeto_mantenedor);

            if (objeto_mantenedor._respok)
            {
                T_ID_GT.Text = objeto_mantenedor.id_gt.ToString();
                T_ID_OTZ.Text = id.ToString();
                T_OTZ_GUIA.Text = objeto_mantenedor.guia;
                T_OTZ_ENTRADAS.Text = objeto_mantenedor.entradas.ToString();
                T_OTZ_ESTADIA.Text = objeto_mantenedor.estadia.ToString();
                T_OTZ_DOBLE_CONDUCTOR.Text = objeto_mantenedor.doble_conductor.ToString();
                T_OTZ_CARGA_DESCARGA.Text = objeto_mantenedor.carga_descarga.ToString();
                T_OTZ_FLETE_DE_TERCERO.Text = objeto_mantenedor.flete_de_tercero.ToString();
                T_OTZ_OTROS.Text = objeto_mantenedor.otros.ToString();
                T_OTZ_OTROS_DETALLE.Text = objeto_mantenedor.detalle_otros.ToString();
                T_OTZ_VALOR.Text = objeto_mantenedor.valor_viaje.ToString();
                T_OTZ_SOL_OC.Text = objeto_mantenedor.d_sol_oc;
                T_OTZ_OT.Text = objeto_mantenedor.d_ot;
                T_OTZ_EEPP.Text = objeto_mantenedor.d_eepp;
                T_OTZ_GASTO.Text = objeto_mantenedor.d_gasto;
                T_OTZ_D_OC.Text = objeto_mantenedor.d_oc;
                T_OTZ_HES.Text = objeto_mantenedor.d_hes;
                T_OTZ_FACTURA.Text = objeto_mantenedor.d_factura;
                T_OTZ_NOMBRE_TERCERO.Text = objeto_mantenedor.d_nombre_tercero;
                T_OTZ_EEPP_TERCERO.Text = objeto_mantenedor.d_eepp_tercero;
                T_OTZ_FACTURA_DE_TERCERO.Text = objeto_mantenedor.d_factura_tercero;
                T_OTZ_DIFERENCIA_FACTURA.Text = objeto_mantenedor.diferencia_factura.ToString();
                T_OTZ_OBSERVACION_FACTURA.Text = objeto_mantenedor.observacion_factura;
                UP_OTZ.Update();
            }

        }

        protected void B_GUARDAR_OTZ_Click(object sender, EventArgs e)
        {

            if (T_ID_OTZ.Text != "")
            {
                // EDITAR
                OBJ_ENC_OTZ carga = new OBJ_ENC_OTZ();
                carga.ID_OTZ = int.Parse(T_ID_OTZ.Text);
                FN_ENC_OTZ.LLENAOBJETO(ref carga);
                if (carga._respok)
                {
                    carga.guia = T_OTZ_GUIA.Text;
                    carga.d_sol_oc = T_OTZ_SOL_OC.Text;
                    carga.d_ot = T_OTZ_OT.Text;
                    carga.d_eepp = T_OTZ_EEPP.Text;
                    carga.d_gasto = T_OTZ_GASTO.Text;
                    carga.d_oc = T_OTZ_D_OC.Text;
                    carga.d_hes = T_OTZ_HES.Text;
                    carga.d_factura = T_OTZ_FACTURA.Text;
                    carga.d_nombre_tercero = T_OTZ_NOMBRE_TERCERO.Text;
                    carga.d_eepp_tercero = T_OTZ_EEPP_TERCERO.Text;
                    carga.d_factura_tercero = T_OTZ_FACTURA_DE_TERCERO.Text;
                    carga.observacion_factura = T_OTZ_OBSERVACION_FACTURA.Text;
                    carga.detalle_otros = T_OTZ_OTROS_DETALLE.Text;        

                    if (T_OTZ_ESTADIA.Text != "")
                        carga.estadia = int.Parse(T_OTZ_ESTADIA.Text);
                    if (T_OTZ_ENTRADAS.Text != "")
                        carga.entradas = int.Parse(T_OTZ_ENTRADAS.Text);
                    if (T_OTZ_DOBLE_CONDUCTOR.Text != "")
                        carga.doble_conductor = int.Parse(T_OTZ_DOBLE_CONDUCTOR.Text);
                    if (T_OTZ_CARGA_DESCARGA.Text != "")
                        carga.carga_descarga = int.Parse(T_OTZ_CARGA_DESCARGA.Text);
                    if (T_OTZ_FLETE_DE_TERCERO.Text != "")
                        carga.flete_de_tercero = int.Parse(T_OTZ_FLETE_DE_TERCERO.Text);
                    if (T_OTZ_OTROS.Text != "")
                        carga.otros = int.Parse(T_OTZ_OTROS.Text);
                    if (T_OTZ_DIFERENCIA_FACTURA.Text != "")
                        carga.diferencia_factura = int.Parse(T_OTZ_DIFERENCIA_FACTURA.Text);
                    if (T_OTZ_VALOR.Text != "")
                        carga.valor_viaje = int.Parse(T_OTZ_VALOR.Text);

                    FN_ENC_OTZ.UPDATE(ref carga);
                    if (carga._respok)
                    {
                        DBUtil db = new DBUtil();
                        totalesotz();
                        G_OTZ.DataSource = db.consultar(" select top(100) * from V_OTZ order by correlativo_otz desc ");
                        G_OTZ.DataBind();
                        alert("OTZ modificado con éxito", 1);
                    }
                }
            }

        }

        public void LimpiarCamposOTZ()
        {
            T_ID_OTZ.Text = string.Empty;
            T_OTZ_GUIA.Text = string.Empty;
            T_OTZ_ENTRADAS.Text = string.Empty;
            T_OTZ_ESTADIA.Text = string.Empty;
            T_OTZ_DOBLE_CONDUCTOR.Text = string.Empty;
            T_OTZ_CARGA_DESCARGA.Text = string.Empty;
            T_OTZ_FLETE_DE_TERCERO.Text = string.Empty;
            T_OTZ_OTROS.Text = string.Empty;
            T_OTZ_OTROS_DETALLE.Text = string.Empty;
            T_OTZ_SOL_OC.Text = string.Empty;
            T_OTZ_OT.Text = string.Empty;
            T_OTZ_EEPP.Text = string.Empty;
            T_OTZ_GASTO.Text = string.Empty;
            T_OTZ_D_OC.Text = string.Empty;
            T_OTZ_HES.Text = string.Empty;
            T_OTZ_FACTURA.Text = string.Empty;
            T_OTZ_NOMBRE_TERCERO.Text = string.Empty;
            T_OTZ_FACTURA_DE_TERCERO.Text = string.Empty;
            T_OTZ_DIFERENCIA_FACTURA.Text = string.Empty;
            T_OTZ_OBSERVACION_FACTURA.Text = string.Empty;
            T_OTZ_VALOR.Text = string.Empty;
        }

        public void totalesotz()
        {
            try
            {
                DBUtil db = new DBUtil();
                db.Scalar("update enc_gt set total_flete = (select ISNULL(SUM(valor_viaje + entradas + estadia + doble_conductor + carga_descarga + otros + diferencia_factura - flete_de_tercero),0) as 'suma' from enc_otz where id_gt = " + T_ID_GT.Text + ") where id_gt = " + T_ID_GT.Text);
                int saldo_final = int.Parse(db.Scalar("select (total_flete - total_gastos - total_precio_combustible) from enc_gt where id_gt = " + T_ID_GT.Text).ToString());
                db.Scalar("update enc_gt set saldo_total = " + saldo_final + " where id_gt = " + T_ID_GT.Text);
            }
            catch (Exception ex)
            {

            }
        }
    }
}