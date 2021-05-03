using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AMALIAFW;


namespace AMALIA
{
    public partial class Amalia : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session.Timeout = 2;       
            //Session["Testing"] = "Sesión activa: ";
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

                    if (us.id_perfil == 1)
                    {
                        // ADMIN
                        DASHBOARD.Attributes.Add("style", "display:inline");
                        //VIAJES.Attributes.Add("style", "display:inline");
                        REPORTES.Attributes.Add("style", "display:inline");
                        MANTENIMIENTO.Attributes.Add("style", "display:inline");
                        REPUESTOS.Attributes.Add("style", "display:inline");
                        CONFIGURACION.Attributes.Add("style", "display:inline");
                        FACTURAS.Attributes.Add("style", "display:inline");
                 
                    }
                    if (us.id_perfil == 2)
                    {
                        // OPERACIONES
                        //VIAJES.Attributes.Add("style", "display:inline");                      
                    }

                    if (us.id_perfil == 3)
                    {
                        // ADMINISTRATIVO
                        //VIAJES.Attributes.Add("style", "display:inline");
                        REPORTES.Attributes.Add("style", "display:inline");
                    }

                    if (us.id_perfil == 4)
                    {
                        // FINANZAS
                        REPORTES.Attributes.Add("style", "display:inline");
                    }
                    if (us.id_perfil == 5)
                    {
                        // FINANZAS
                        MANTENIMIENTO.Attributes.Add("style", "display:inline");
                        REPUESTOS.Attributes.Add("style", "display:inline");
                    }
                    if (us.id_perfil == 6)
                    {
                        // LEONEL
                        DASHBOARD.Attributes.Add("style", "display:inline");
                        REPORTES.Attributes.Add("style", "display:inline");
                        MANTENIMIENTO.Attributes.Add("style", "display:inline");
                        REPUESTOS.Attributes.Add("style", "display:inline");
                        CONFIGURACION.Attributes.Add("style", "display:inline");
                        MENU_LEONEL.Attributes.Add("style", "display:inline");
                    }
                    if (us.usuario == "lacosta")
                    {
                        REPORTES.Attributes.Add("style", "display:inline");
                    }
                }
            }

        }

    }
}