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

namespace CRM
{

    public partial class DemoRendicion : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            Panel1.Visible = !Panel1.Visible;
            Panel2.Visible = !Panel2.Visible;
        }  
    }
}