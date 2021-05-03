using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using AMALIAFW;

namespace AMALIA
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void B_INGRESAR_Click(object sender, EventArgs e)
        {
            OBJ_USUARIOS us = new OBJ_USUARIOS();
            us.usuario = T_USUARIO.Text;
            us.pass = T_PASS.Text;

            FN_USUARIOS.LOGIN(ref us);
            if (us._respok)
            {               
                FormsAuthentication.SetAuthCookie(us.usuario, true);      
                if (us.usuario == "leonel")
                {
                    Response.Redirect("GD_INDEX.aspx");
                }
                else
                {
                    Response.Redirect("Index.aspx");
                }                
            }
            else
            {
                LBL_RESPUESTA.Text = "Credenciales inválidas o Usuario Inactivo.";
            }
        }
    }
}