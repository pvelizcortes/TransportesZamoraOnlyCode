using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;

namespace AMALIAFW
{
    public class SPVars
    {
        public object nombre { get; set; }
        public object valor { get; set; }
    }

    public class VariablesJson
    {
        private string nombre;
        private string valor;

        //' PROPIEDADES ''
        public string name
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string value
        {
            get { return valor; }
            set { valor = value; }
        }
    }

    public class JSON
    {
        private JSON()
        {

        }
        public static string Form(VariablesJson[] variables, string name)
        {
            VariablesJson vars = null;
            foreach (VariablesJson vars_loopVariable in variables)
            {
                vars = vars_loopVariable;
                if (vars.name == name)
                    return vars.value;
            }
            return string.Empty;
        }
    }


    public class Correo
    {
        public string EnviarCorreo(string from, string to, string asunto, string mensaje, List<string> CC, string ruta = "", string from_alias = "")
        {
            /*-------------------------MENSAJE DE CORREO----------------------*/
            using (System.Net.Mail.MailMessage MailSetup = new System.Net.Mail.MailMessage())
            {

                MailSetup.Subject = asunto;
                MailSetup.To.Add(to);
                foreach (string x in CC)
                {
                    MailSetup.CC.Add(x);
                }
                if (from_alias != "")
                {
                    MailSetup.From = new System.Net.Mail.MailAddress(from, from_alias);
                }
                else
                {
                    MailSetup.From = new System.Net.Mail.MailAddress(from);
                }

                MailSetup.Body = mensaje;
                MailSetup.IsBodyHtml = true;
                MailSetup.BodyEncoding = System.Text.Encoding.UTF8;
                if (ruta != "")
                {
                    System.IO.FileInfo toDownload = new System.IO.FileInfo(ruta);
                    if (toDownload.Exists)
                    {
                        MailSetup.Attachments.Add(new System.Net.Mail.Attachment(ruta));
                    }
                }
                SmtpClient SMTP = new SmtpClient("smtp.gmail.com");

                SMTP.Port = 587;
                SMTP.EnableSsl = true;
                SMTP.UseDefaultCredentials = false;
                SMTP.Credentials = new System.Net.NetworkCredential("tzamoracorreos@gmail.com", "FelipeZamora4321");
                SMTP.Send(MailSetup);

                // SMTP
                return "OK";

            }
        }
    }

    public class DBUtil
    {
        private string cadena = ConfigurationManager.ConnectionStrings["default"].ToString();
        public SqlConnection cn;

        private SqlCommandBuilder cmb;
        private void conectar()
        {
            cn = new SqlConnection(cadena);
        }
        public DBUtil()
        {
            conectar();
        }


        private SqlCommand comando;
        public bool insertar(string sql)
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                    cn.BeginTransaction();
                    comando = new SqlCommand(sql, cn);
                    int i = comando.ExecuteNonQuery();
                    if (i > 0)
                    {
                        comando.Transaction.Commit();
                        cn.Close();
                        return true;
                    }
                    else
                    {
                        comando.Transaction.Rollback();
                        cn.Close();
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (cn != null)
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        comando.Transaction.Rollback();
                        cn.Close();
                    }
                }

                return false;
            }
        }

        public bool eliminar(string tabla, string condicion)
        {

            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                    cn.BeginTransaction();
                    string sql = "delete from " + tabla + "where " + condicion;
                    comando = new SqlCommand(sql, cn);
                    int i = comando.ExecuteNonQuery();
                    cn.Close();
                    if (i > 0)
                    {
                        comando.Transaction.Commit();
                        cn.Close();
                        return true;
                    }
                    else
                    {
                        comando.Transaction.Rollback();
                        cn.Close();
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (cn != null)
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        comando.Transaction.Rollback();
                        cn.Close();
                    }
                }
                return false;
            }
        }

        public bool actualizar(string tabla, string campos, string condicion)
        {

            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                    cn.BeginTransaction();
                    string sql = "update " + tabla + "set " + campos + " where " + condicion;
                    comando = new SqlCommand(sql, cn);
                    int i = comando.ExecuteNonQuery();
                    cn.Close();
                    if (i > 0)
                    {
                        comando.Transaction.Commit();
                        cn.Close();
                        return true;
                    }
                    else
                    {
                        comando.Transaction.Rollback();
                        cn.Close();
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (cn != null)
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        comando.Transaction.Rollback();
                        cn.Close();
                    }
                }
                return false;
            }
        }

        public DataTable ds = new DataTable();

        public SqlDataAdapter da;
        public DataTable consultar(string sql)
        {
            da = new SqlDataAdapter(sql, cn);
            DataSet dts = new DataSet();
            da.Fill(dts, sql);
            DataTable dt = new DataTable();
            dt = dts.Tables[0];
            return dt;
        }

        public object Scalar(string sql)
        {
            cn.Open();
            comando = new SqlCommand(sql, cn);
            object obj = comando.ExecuteScalar();
            cn.Close();
            return obj;
        }

        public object Scalar2(string sql, List<SPVars> toSP = null)
        {
            cn.Open();
            comando = new SqlCommand(sql, cn);
            if (toSP != null)
            {
                foreach (SPVars ob in toSP)
                {
                    comando.Parameters.AddWithValue("@" + ob.nombre, ob.valor);
                }
            }
            object obj = comando.ExecuteScalar();
            cn.Close();
            return obj;
        }

        public DataTable consultar2(string tabla)
        {
            string sql = "select * from " + tabla;
            da = new SqlDataAdapter(sql, cn);
            DataSet dts = new DataSet();
            da.Fill(dts, tabla);
            DataTable dt = new DataTable();
            dt = dts.Tables[tabla];
            return dt;
        }

        public DataTable consultar3(string sql, List<SPVars> toSP = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (toSP != null)
                {
                    foreach (SPVars ob in toSP)
                    {
                        cmd.Parameters.AddWithValue("@" + ob.nombre, ob.valor);
                    }
                }
                SqlDataAdapter ap = new SqlDataAdapter(cmd);
                ap.Fill(dt);
            }
            return dt;
        }

        public DataTable LlamaSP(string Nombre, List<SPVars> toSP = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                SqlCommand sqlComm = new SqlCommand(Nombre, conn);
                if (toSP != null)
                {
                    foreach (SPVars ob in toSP)
                    {
                        sqlComm.Parameters.AddWithValue("@" + ob.nombre, ob.valor);
                    }
                }

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(dt);
                return dt;
            }
        }
    }

    public class Utiles
    {
        public void CargarCombo(DropDownList DropDown, string tabla, string display, string value, string filtro = "")
        {
            DBUtil db = new DBUtil();
            DataTable dt;
            string query = "";

            if (filtro != "")
            {
                query += "select " + value + "," + display + " from " + tabla + " where " + filtro;
            }
            else
            {
                query += "select " + value + "," + display + " from " + tabla;
            }

            dt = db.consultar(query);
            dt.Rows.Add(new Object[] { -1, "-- Seleccione --" });

            DropDown.DataSource = dt;
            DropDown.DataTextField = display;
            DropDown.DataValueField = value;
            DropDown.DataBind();
            DropDown.SelectedValue = "-1";
            DropDown.Enabled = true;
        }
    }

   
}
