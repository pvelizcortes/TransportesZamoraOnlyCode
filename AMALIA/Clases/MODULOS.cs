using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace AMALIAFW
{
    public class OBJ_MODULOS
    {
        public int ID_MODULO { get; set; }
        public int ID_EMPRESA { get; set; }
        public string NOMBRE_MODULO { get; set; }
        public int TIPO_MODULO { get; set; }
        public string WEB_APP { get; set; }      
        public DateTime FECHA_CREACION { get; set; }
        public string ACTIVO { get; set; }
        // AGREGAR EN TODAS LAS CLASES
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public class FN_MODULOS
    {
        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "usuarios";
        private static string nombre_llave = "ID_MODULO";
        private static string value_insert = " ID_EMPRESA, NOMBRE_MODULO, TIPO_MODULO, WEB_APP, FECHA_CREACION, ACTIVO ";
        private static string value_insert2 = " @ID_EMPRESA, @NOMBRE_MODULO, @TIPO_MODULO, @WEB_APP, @FECHA_CREACION, @ACTIVO ";
        private static string value_update =
        " ID_EMPRESA = @ID_EMPRESA, NOMBRE_MODULO = @NOMBRE_MODULO, TIPO_MODULO = @TIPO_MODULO, WEB_APP = @WEB_APP, FECHA_CREACION = @FECHA_CREACION, " +
        " ACTIVO = @ACTIVO " +
        " WHERE " + nombre_llave + " = @" + nombre_llave + " ";
        // **************************************** **************************************** **************************************** //

        public static DataTable LLENADT(string sql_where = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                string sql = @"SELECT *  from " + nombre_tabla + " " + sql_where;
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter ap = new SqlDataAdapter(cmd);
                ap.Fill(dt);
            }
            return dt;
        }

        public static void INSERT(ref OBJ_MODULOS objeto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    conn.Open();
                    string sql = @"insert into " + nombre_tabla +
                    " (" + value_insert + ") values" +
                    " (" + value_insert2 + "); SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // CAMBIAR AQUI //
                        // **************************************** **************************************** **************************************** //                        
                        cmd.Parameters.AddWithValue("@ID_EMPRESA", objeto.ID_EMPRESA);
                        cmd.Parameters.AddWithValue("@NOMBRE_MODULO", objeto.NOMBRE_MODULO);
                        cmd.Parameters.AddWithValue("@TIPO_MODULO", objeto.TIPO_MODULO);
                        cmd.Parameters.AddWithValue("@WEB_APP", objeto.WEB_APP);                        
                        cmd.Parameters.AddWithValue("@ACTIVO", objeto.ACTIVO);
                        cmd.Parameters.AddWithValue("@FECHA_CREACION", objeto.FECHA_CREACION);
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_MODULO = scope;
                        // **************************************** **************************************** **************************************** // 
                        objeto._respok = true;
                        objeto._respdet = " Creación exitosa ";
                    }
                }
            }
            catch (Exception ex)
            {
                objeto._respok = false;
                objeto._respdet = ex.Message;
            }
        }

        public static void UPDATE(ref OBJ_MODULOS objeto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    conn.Open();
                    string sql = @"update " + nombre_tabla + " set " +
                    value_update;
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // CAMBIAR AQUI //
                        // **************************************** **************************************** **************************************** //   
                        cmd.Parameters.AddWithValue("@ID_EMPRESA", objeto.ID_EMPRESA);
                        cmd.Parameters.AddWithValue("@NOMBRE_MODULO", objeto.NOMBRE_MODULO);
                        cmd.Parameters.AddWithValue("@TIPO_MODULO", objeto.TIPO_MODULO);
                        cmd.Parameters.AddWithValue("@WEB_APP", objeto.WEB_APP);                        
                        cmd.Parameters.AddWithValue("@ACTIVO", objeto.ACTIVO);
                        cmd.Parameters.AddWithValue("@FECHA_CREACION", objeto.FECHA_CREACION);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_MODULO);
                        // **************************************** **************************************** **************************************** // 
                        cmd.ExecuteNonQuery();
                        objeto._respok = true;
                        objeto._respdet = " Modificación exitosa ";
                    }
                }
            }
            catch (Exception ex)
            {
                objeto._respok = false;
                objeto._respdet = ex.Message;
            }
        }

        public static void DELETE(ref OBJ_MODULOS objeto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    conn.Open();
                    string sql = @"DELETE FROM " + nombre_tabla + " WHERE " + nombre_llave + " = @" + nombre_llave + "";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // CAMBIAR AQUI //
                        // **************************************** **************************************** **************************************** //   
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_MODULO);
                        // **************************************** **************************************** **************************************** //  
                        cmd.ExecuteNonQuery();
                        objeto._respok = true;
                        objeto._respdet = " Eliminación exitosa ";
                    }
                }
            }
            catch (Exception ex)
            {
                objeto._respok = false;
                objeto._respdet = ex.Message;
            }
        }

        public static void LLENAOBJETO(ref OBJ_MODULOS objeto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    conn.Open();
                    string sql = @"SELECT * from " + nombre_tabla + " where " + nombre_llave + " = @" + nombre_llave;
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    // CAMBIAR AQUI //
                    // **************************************** **************************************** **************************************** //   
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_MODULO);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        objeto.ID_MODULO = int.Parse(reader["ID_MODULO"].ToString());
                        objeto.ID_EMPRESA = int.Parse(reader["ID_EMPRESA"].ToString());
                        objeto.NOMBRE_MODULO = reader["NOMBRE_MODULO"].ToString();
                        objeto.TIPO_MODULO = int.Parse(reader["TIPO_MODULO"].ToString());
                        objeto.WEB_APP = reader["WEB_APP"].ToString();
                        objeto.FECHA_CREACION = DateTime.Parse(reader["FECHA_CREACION"].ToString());
                        objeto.ACTIVO = reader["ACTIVO"].ToString();
                        // **************************************** **************************************** **************************************** //   
                        objeto._respok = true;
                        objeto._respdet = " Objeto llenado con exito ";
                    }
                }
            }
            catch (Exception ex)
            {
                objeto._respok = false;
                objeto._respdet = ex.Message;
            }
        }

        public static void PREPARAOBJETO(ref OBJ_MODULOS objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //   
                objeto.ID_EMPRESA = 0;
                objeto.TIPO_MODULO = 0;
                objeto.WEB_APP = "WEB + APP";
                objeto.NOMBRE_MODULO = "VACIO";
                objeto.FECHA_CREACION = DateTime.Now;
                objeto.ACTIVO = "ACTIVO";
                // **************************************** **************************************** **************************************** //   
                objeto._respok = true;
                objeto._respdet = " Objeto preparado con exito ";
            }
            catch (Exception ex)
            {
                objeto._respok = false;
                objeto._respdet = ex.Message;
            }
        }
    }
}
