using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AMALIAFW;
using System.Data;
using System.Web.UI.HtmlControls;

namespace AMALIA
{
    public partial class Depositos : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Deposito";
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
                    FN_USUARIOS.BUSCARCONUSUARIO(ref us);

                    if (us.usuario == "festay" || us.usuario == "gestay" || us.usuario == "mzapata")
                    {
                        divAdmin.Visible = true;
                        bAgregarDetalle.Visible = true;
                        B_NUEVO.Visible = true;
                    }
                    if (us.id_perfil == 2 || us.id_perfil == 4 || us.usuario == "jbrantes")
                    {
                        bAgregarDetalle.Visible = true;
                        B_NUEVO.Visible = true;
                    }
                    if (us.usuario == "festay" || us.usuario == "gestay")
                    {
                        bBorrarDeposito.Visible = true;
                    }
                    //if (us.usuario == "jbrantes")
                    //{
                    //    cbTipo.Items.Clear();
                    //    cbTipo.Items.Add(new ListItem("SALDO", "SALDO"));
                    //    cbTipo.Items.Add(new ListItem("DESCUENTO", "DESCUENTO"));
                    //}

                    CargarCombos();
                    LlenarGrillaInicio();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "gridoc", "<script>javascript:Datatables();</script>", false);
            }
        }

        public void LlenarGrillaInicio()
        {
            FILTRA_FECHA_DESDE.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            LlenarGrilla(true);
            // Si es Admin
            //divAdmin.Visible = true;
        }

        public void LlenarGrilla(bool inicio)
        {
            DataTable dt = new DataTable();
            if (inicio)
            {
                dt = FN_DEPOSITO_ENC.LLENADTVISTAINICIO();
            }
            else
            {
                string filtro = " where 1=1 ";
                if (FILTRA_FECHA_DESDE.Text != "")
                {
                    filtro += " and fecha >= convert(date, '" + DateTime.Parse(FILTRA_FECHA_DESDE.Text).ToString("dd/MM/yyyy") + "', 103) ";
                }
                if (FILTRA_FECHA_HASTA.Text != "")
                {
                    filtro += " and fecha <= convert(date, '" + DateTime.Parse(FILTRA_FECHA_HASTA.Text).ToString("dd/MM/yyyy") + "', 103) ";
                }
                if (CB_USUARIOS.SelectedValue != "-1")
                {
                    filtro += " and usuario = '" + CB_USUARIOS.SelectedValue + "'";
                }
                if (CB_CONDUCTOR.SelectedValue != "-1")
                {
                    filtro += " and id_conductor = '" + CB_CONDUCTOR.SelectedValue + "'";
                }
                if (CB_TIPO.SelectedValue != "-1")
                {
                    filtro += " and tipo = '" + CB_TIPO.SelectedValue + "'";
                }
                if (CB_ESTADO.SelectedValue != "-1")
                {
                    filtro += " and estado = '" + CB_ESTADO.SelectedValue + "'";
                }
                dt = FN_DEPOSITO_ENC.LLENADTVISTA(filtro);
            }

            G_PRINCIPAL.DataSource = dt;
            G_PRINCIPAL.DataBind();
        }


        public void CargarCombos()
        {
            DBUtil db = new DBUtil();

            CB_USUARIOS.DataSource = db.consultar("select id_usuario, nombre_completo from usuarios order by nombre_completo");
            CB_USUARIOS.DataTextField = "nombre_completo";
            CB_USUARIOS.DataValueField = "id_usuario";
            CB_USUARIOS.DataBind();
            CB_USUARIOS.Items.Insert(0, new ListItem("-- SELECCIONE --", "-1"));

            DataTable dtConductor = db.consultar("select id_conductor, nombre_completo from conductor order by nombre_completo");
            CB_CONDUCTOR.DataSource = dtConductor;
            CB_CONDUCTOR.DataTextField = "nombre_completo";
            CB_CONDUCTOR.DataValueField = "id_conductor";
            CB_CONDUCTOR.DataBind();
            CB_CONDUCTOR.Items.Insert(0, new ListItem("-- SELECCIONE --", "-1"));

            cbConductor.DataSource = FN_CONDUCTOR.LLENADT(" where activo = 'ACTIVO' order by NOMBRE_COMPLETO ");
            cbConductor.DataTextField = "nombre_completo";
            cbConductor.DataValueField = "id_conductor";
            cbConductor.DataBind();
            cbConductor.Items.Insert(0, new ListItem("-- SELECCIONE --", "-1"));

            
        }

        protected void B_NUEVO_Click(object sender, EventArgs e)
        {
            LIMPIARCAMPOS();

            OBJ_DEPOSITO_ENC enc = new OBJ_DEPOSITO_ENC();
            enc.usuario = HttpContext.Current.User.Identity.Name;
            FN_DEPOSITO_ENC.getCorrelativo(ref enc);
            if (enc._respok)
            {
                T_ID.Text = enc.ID_DEPOSITO.ToString();
                tNumOperacion.Text = enc.num_operacion.ToString();
                PANEL_ENC.Visible = true;
                PANEL_PRINCIPAL.Visible = false;
                COMPLETAR_DETALLE(enc.ID_DEPOSITO);
                divDetalle.Visible = false;
            }

        }

        protected void G_PRINCIPAL_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //editar
                if (e.CommandName == "Editar")
                {
                    OBJ_USUARIOS us = new OBJ_USUARIOS();
                    us.usuario = HttpContext.Current.User.Identity.Name;
                    FN_USUARIOS.BUSCARCONUSUARIO(ref us);

                    if (us.id_perfil == 2 || us.usuario == "festay" || us.usuario == "mzapata" || us.id_perfil == 4 || us.usuario == "gestay")
                    {
                        int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                        int idDetalle = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString()));
                        COMPLETAR_DETALLE(id, idDetalle);
                    }
                    else
                    {
                        alert("Ud no tiene permisos para editar depositos", 0);
                    }

                }
                //borrar
                if (e.CommandName == "Borrar")
                {
                    int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    OBJ_DEPOSITO_ENC enc = new OBJ_DEPOSITO_ENC();
                    enc.ID_DEPOSITO = id;
                    FN_DEPOSITO_ENC.DELETE(ref enc);
                    if (enc._respok)
                    {
                        DBUtil db = new DBUtil();
                        db.Scalar("delete from deposito_detalle where id_deposito = " + id.ToString());
                        LlenarGrilla(false);
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void COMPLETAR_DETALLE(int id, int id_detalle = 0)
        {
            LIMPIARCAMPOS();
            OBJ_DEPOSITO_ENC fact = new OBJ_DEPOSITO_ENC();
            fact.ID_DEPOSITO = id;

            FN_DEPOSITO_ENC.LLENAOBJETO(ref fact);
            if (fact._respok)
            {
                T_ID.Text = id.ToString();
                tNumOperacion.Text = fact.num_operacion.ToString();
                LlenarGrillaDetalle(id);
                PANEL_ENC.Visible = true;
                PANEL_PRINCIPAL.Visible = false;
                divDetalle.Visible = false;
                if (id_detalle > 0)
                {
                    if (HttpContext.Current.User.Identity.Name == "festay" || HttpContext.Current.User.Identity.Name == "gestay")
                    {
                        COMPLETAR_DETALLE2(id_detalle);
                    }
                    divDetalle.Visible = true;
                }
            }
        }

        #region ---------------- NO CAMBIAR ---------------- 
        public void LIMPIARCAMPOS()
        {
            CleanControl(this.Controls);
            tFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        public void CleanControl(ControlCollection controles)
        {
            foreach (Control control in controles)
            {
                if (control is TextBox)
                {
                    if (control.ID == "FILTRA_FECHA_DESDE" || control.ID == "FILTRA_FECHA_HASTA")
                    {

                    }
                    else
                    {
                        ((TextBox)control).Text = string.Empty;
                    }
                }

                else if (control is DropDownList)
                {
                    if (control.ID == "CB_USUARIOS" || control.ID == "CB_CONDUCTOR" || control.ID == "CB_TIPO" || control.ID == "CB_ESTADO")
                    {

                    }
                    else
                    {
                        ((DropDownList)control).ClearSelection();
                    }
                }

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
            MakeAccessible(gDetalle);
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


        protected void B_GUARDAROC_Click(object sender, EventArgs e)
        {


        }

        protected void B_VOLVER_Click(object sender, EventArgs e)
        {
            PANEL_PRINCIPAL.Visible = true;
            PANEL_ENC.Visible = false;
        }

        protected void B_FILTRAR_Click(object sender, EventArgs e)
        {
            LlenarGrilla(false);
        }

        public void LlenarGrillaDetalle(int id_deposito)
        {
            try
            {
                gDetalle.DataSource = FN_DEPOSITO_DETALLE.LLENADTVISTA(" where id_deposito = " + id_deposito);
                gDetalle.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void bAgregarDetalle_Click(object sender, EventArgs e)
        {
            DBUtil db = new DBUtil();
            OBJ_USUARIOS usuario = new OBJ_USUARIOS();
            usuario.usuario = HttpContext.Current.User.Identity.Name;

            if (tNumViaje.Text == "")
            {
                alert("Ingrese un numero de viaje.", 0);
            }
            else if (tFecha.Text == "")
            {
                alert("Ingrese fecha de plazo de entrega.", 0);
            }
            else if (tValor.Text == "")
            {
                alert("Ingrese el solicitante.", 0);
            }
            else if (cbConductor.SelectedValue == "-1")
            {
                alert("Seleccione un conductor.", 0);
            }            
            else
            {
                
                FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);

                OBJ_DEPOSITO_DETALLE fac = new OBJ_DEPOSITO_DETALLE();
                FN_DEPOSITO_DETALLE.PREPARAOBJETO(ref fac);
                if (T_ID_DETALLE.Text == "")
                {
                    // NUEVO   
                    fac.id_deposito = int.Parse(T_ID.Text);
                    fac.num_viaje = int.Parse(tNumViaje.Text);
                    fac.fecha_viaje = DateTime.Parse(tFecha.Text);
                    fac.id_conductor = int.Parse(cbConductor.SelectedValue);
                    fac.nombre_conductor = cbConductor.SelectedItem.Text;
                    fac.tipo = cbTipo.SelectedValue;
                    fac.valor = int.Parse(tValor.Text);
                    fac.comentario = tComentario.Text;
                    fac.estado = "NO DEPOSITADO";
                    // ADMIN
                    if (divAdmin.Visible)
                    {
                        fac.comentario_admin = tComentarioAdmin.Text;
                        if (tMontoDepositadoAdmin.Text == "")
                        {
                            fac.monto_depositado = 0;
                        }
                        else
                        {
                            fac.monto_depositado = int.Parse(tMontoDepositadoAdmin.Text);
                        }

                        fac.estado = cbEstadoAdmin.SelectedValue;
                        fac.usuario_admin = HttpContext.Current.User.Identity.Name;
                    }

                    FN_DEPOSITO_DETALLE.INSERT(ref fac);
                    if (fac._respok)
                    {
                        CalcularDineroEntregado(fac.num_viaje);
                        LlenarGrilla(false);
                        LlenarGrillaDetalle(fac.id_deposito);
                        CambiaConductorEnGt(fac.id_conductor, fac.num_viaje);
                        alert("Guardado con éxito", 1);

                    }
                    else
                    {
                        alert("Problemas al guardar el deposito", 0);
                    }
                }
                else
                {
                    fac.ID_DETALLE_DEPOSITO = int.Parse(T_ID_DETALLE.Text);
                    FN_DEPOSITO_DETALLE.LLENAOBJETO(ref fac);
                    if (fac._respok)
                    {
                        fac.id_deposito = int.Parse(T_ID.Text);
                        fac.num_viaje = int.Parse(tNumViaje.Text);
                        fac.fecha_viaje = DateTime.Parse(tFecha.Text);
                        fac.id_conductor = int.Parse(cbConductor.SelectedValue);
                        fac.nombre_conductor = cbConductor.SelectedItem.Text;
                        fac.tipo = cbTipo.SelectedValue;
                        fac.valor = int.Parse(tValor.Text);
                        fac.comentario = tComentario.Text;
                        fac.estado = "NO DEPOSITADO";
                        // ADMIN
                        if (divAdmin.Visible)
                        {
                            fac.comentario_admin = tComentarioAdmin.Text;
                            if (tMontoDepositadoAdmin.Text == "")
                            {
                                fac.monto_depositado = 0;
                            }
                            else
                            {
                                fac.monto_depositado = int.Parse(tMontoDepositadoAdmin.Text);
                            }

                            fac.estado = cbEstadoAdmin.SelectedValue;
                            fac.usuario_admin = HttpContext.Current.User.Identity.Name;
                        }
                        // MODIFICAR
                        FN_DEPOSITO_DETALLE.UPDATE(ref fac);
                        if (fac._respok)
                        {
                            CalcularDineroEntregado(fac.num_viaje);
                            LlenarGrilla(false);
                            LlenarGrillaDetalle(fac.id_deposito);
                            CambiaConductorEnGt(fac.id_conductor, fac.num_viaje);
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Problemas al modificar el deposito", 0);
                        }
                    }
                }
                LimpiarCamposDetalle();
                divDetalle.Visible = false;
            }
        }

        protected void gDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (HttpContext.Current.User.Identity.Name == "festay" || HttpContext.Current.User.Identity.Name == "gestay")
                {
                    //editar
                    if (e.CommandName == "Editar")
                    {
                        int id = int.Parse((gDetalle.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                        COMPLETAR_DETALLE2(id);
                    }
                    //borrar
                    if (e.CommandName == "Borrar")
                    {

                        int id = int.Parse((gDetalle.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                        int idDeposito = int.Parse((gDetalle.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString()));
                        int CorrelativoGT = int.Parse((gDetalle.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[4].ToString()));
                        OBJ_DEPOSITO_DETALLE enc = new OBJ_DEPOSITO_DETALLE();
                        enc.ID_DETALLE_DEPOSITO = id;
                        FN_DEPOSITO_DETALLE.DELETE(ref enc);
                        if (enc._respok)
                        {
                            CalcularDineroEntregado(CorrelativoGT);
                            alert("Deposito eliminado con éxito", 1);
                            LlenarGrillaDetalle(idDeposito);
                            LlenarGrilla(false);
                        }
                    }
                }
                else
                {
                    alert("Ud no tiene permisos para borrar/editar depositos", 0);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void bAprobarTodas_Click(object sender, EventArgs e)
        {
            DBUtil db = new DBUtil();
            int idDeposito = int.Parse(T_ID.Text);
            DataTable DtGt = db.consultar("select distinct num_viaje from deposito_detalle where id_deposito = " + idDeposito);
            db.Scalar("update deposito_detalle set estado = 'DEPOSITADO', usuario_admin = '" + HttpContext.Current.User.Identity.Name + "', monto_depositado = valor, fecha_admin = getdate() where id_deposito = " + idDeposito);
            foreach(DataRow dr in DtGt.Rows)
            {
                int numCorrelativo = Convert.ToInt32(dr[0].ToString());
                CalcularDineroEntregado(numCorrelativo);
            }
            LlenarGrillaDetalle(idDeposito);
            LlenarGrilla(false);
            alert("El estado de los depositos fue cambiado correctamente.", 1);
        }

        protected void bNuevoDetalle_Click(object sender, EventArgs e)
        {
            LimpiarCamposDetalle();
            lblDetalle.Text = "Agregando nuevo deposito.";
            divDetalle.Visible = true;
        }

        public void COMPLETAR_DETALLE2(int id)
        {
            LimpiarCamposDetalle();
            OBJ_DEPOSITO_DETALLE fact = new OBJ_DEPOSITO_DETALLE();
            fact.ID_DETALLE_DEPOSITO = id;

            FN_DEPOSITO_DETALLE.LLENAOBJETO(ref fact);
            if (fact._respok)
            {
                lblDetalle.Text = "Editando deposito.";
                T_ID_DETALLE.Text = id.ToString();
                tNumViaje.Text = fact.num_viaje.ToString();
                tFecha.Text = fact.fecha_viaje.ToString("yyyy-MM-dd");
                cbConductor.SelectedValue = fact.id_conductor.ToString();
                cbTipo.SelectedValue = fact.tipo;
                tValor.Text = fact.valor.ToString();
                tComentario.Text = fact.comentario;
                divDetalle.Visible = true;
                if (divAdmin.Visible)
                {
                    tComentarioAdmin.Text = fact.comentario_admin;
                    if (fact.monto_depositado == 0)
                    {
                        tMontoDepositadoAdmin.Text = fact.valor.ToString();
                    }
                    tMontoDepositadoAdmin.Text = fact.monto_depositado.ToString();
                    cbEstadoAdmin.SelectedValue = fact.estado;
                }
            }
        }

        public void LimpiarCamposDetalle()
        {
            T_ID_DETALLE.Text = "";
            tNumViaje.Text = "";
            cbConductor.SelectedValue = "-1";
            cbTipo.SelectedIndex = 0;
            tValor.Text = "";
            tComentario.Text = "";
            tComentarioAdmin.Text = "";
            tMontoDepositadoAdmin.Text = "0";
            cbEstadoAdmin.SelectedValue = "NO DEPOSITADO";
        }

        protected void gDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string estado = gDetalle.DataKeys[e.Row.RowIndex].Values[2].ToString();
                string diferencia = gDetalle.DataKeys[e.Row.RowIndex].Values[3].ToString();

                // ESTADO
                HtmlGenericControl spnHtml = (HtmlGenericControl)e.Row.FindControl("div_estado");
                if (estado == "DEPOSITADO")
                {
                    spnHtml.InnerHtml = "<span class='badge badge-success'>DEPOSITADO</span>";
                }
                else if (estado == "NO DEPOSITADO")
                {
                    spnHtml.InnerHtml = "<span class='badge badge-danger'>NO DEPOSITADO</span>";
                }
                else if (estado == "DESCONTADO")
                {
                    spnHtml.InnerHtml = "<span class='badge badge-warning'>DESCONTADO</span>";
                }
                else
                {
                    spnHtml.InnerHtml = "<span class='badge badge-primary'>NULO</span>";
                }
                // DIFERENCIA
                int saldo_ = int.Parse(diferencia);
                if (saldo_ >= 0)
                {
                    e.Row.Cells[12].ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    e.Row.Cells[12].ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void G_PRINCIPAL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string estado = G_PRINCIPAL.DataKeys[e.Row.RowIndex].Values[2].ToString();
                string diferencia = G_PRINCIPAL.DataKeys[e.Row.RowIndex].Values[3].ToString();

                // ESTADO
                HtmlGenericControl spnHtml = (HtmlGenericControl)e.Row.FindControl("div_estado");
                if (estado == "DEPOSITADO")
                {
                    spnHtml.InnerHtml = "<span class='badge badge-success'>DEPOSITADO</span>";
                }
                else if (estado == "NO DEPOSITADO")
                {
                    spnHtml.InnerHtml = "<span class='badge badge-danger'>NO DEPOSITADO</span>";
                }
                else if (estado == "DESCONTADO")
                {
                    spnHtml.InnerHtml = "<span class='badge badge-warning'>DESCONTADO</span>";
                }
                else
                {
                    spnHtml.InnerHtml = "<span class='badge badge-primary'>NULO</span>";
                }
                // DIFERENCIA
                int saldo_ = int.Parse(diferencia);
                if (saldo_ >= 0)
                {
                    e.Row.Cells[12].ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    e.Row.Cells[12].ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void bBorrarDeposito_Click(object sender, EventArgs e)
        {
            DBUtil db = new DBUtil();
            int idDeposito = int.Parse(T_ID.Text);
            DataTable dt = new DataTable();
            DataTable dtCorrelativos = db.consultar("select distinct num_viaje from deposito_detalle where id_deposito = " + idDeposito);
            db.Scalar("delete from deposito_detalle where id_deposito = " + idDeposito);
            db.Scalar("delete from deposito_enc where id_deposito = " + idDeposito);           
            foreach(DataRow dr in dtCorrelativos.Rows)
            {
                int numCorrelativo = Convert.ToInt32(dr[0].ToString());
                CalcularDineroEntregado(numCorrelativo);                    
            }

            LlenarGrilla(false);
            alert("La operación y sus solicitudes fueron eliminadas correctamente.", 1);
            PANEL_PRINCIPAL.Visible = true;
            PANEL_ENC.Visible = false;
        }


        private void CalcularDineroEntregado(int correlativoGt)
        {
            try
            {
                DBUtil db = new DBUtil();
                int valor = Convert.ToInt32(db.Scalar(" select ISNULL(sum(valor),0) as 'valor'  from deposito_detalle where num_viaje = " + correlativoGt +
                                                        " and tipo != 'SALDO' " +
                                                        " and tipo != 'DESCUENTO' " +
                                                        " and estado = 'DEPOSITADO' ").ToString());
                db.Scalar("update enc_gt set dinero_entregado = " + valor + " where num_correlativo = " + correlativoGt);
                int saldo_dinero = int.Parse(db.Scalar("select (dinero_entregado - total_gastos - dinero_devuelto) from enc_gt where num_correlativo = " + correlativoGt).ToString());
                db.Scalar("update enc_gt set saldo_dinero_entregado = " + saldo_dinero + " where num_correlativo = " + correlativoGt);
            }
            catch(Exception ex)
            {

            }         
        }

        private void CambiaConductorEnGt(int id_conductor, int correlativo_gt)
        {
            DBUtil db = new DBUtil();
            db.Scalar("update enc_gt set id_conductor = " + id_conductor + " where num_correlativo = " + correlativo_gt);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DBUtil db = new DBUtil();
            string sql = "  SELECT distinct num_viaje " +
                          " FROM DEPOSITO_DETALLE " +
                          " where num_viaje >= 3993  " +
                          " and tipo not in ('SALDO', 'DESCUENTO') " +
                          " and estado = 'DEPOSITADO' " +
                          " order by num_viaje desc ";

            DataTable dt = new DataTable();
            dt = db.consultar(sql);

            foreach(DataRow dr in dt.Rows)
            {
                int numCorrelativo = Convert.ToInt32(dr[0].ToString());
                CalcularDineroEntregado(numCorrelativo);
            }
        }
    }   
}