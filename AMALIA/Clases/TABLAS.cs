using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace AMALIAFW
{
    public class OBJ_TABLAS
    {
        public int ID_TABLA { get; set; }
        public int ID_EMPRESA { get; set; }
        public string NOMBRE_TABLA { get; set; }
        public DateTime FECHA_CREACION { get; set; }
        public string ACTIVO { get; set; }
        // AGREGAR EN TODAS LAS CLASES
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public class FN_TABLAS
    {
        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "F_TABLA";
        private static string nombre_llave = "ID_TABLA";
        private static string value_insert = " ID_EMPRESA, NOMBRE_TABLA, FECHA_CREACION, ACTIVO ";
        private static string value_insert2 = " @ID_EMPRESA, @NOMBRE_TABLA, @FECHA_CREACION, @ACTIVO ";
        private static string value_update =
        " ID_EMPRESA = @ID_EMPRESA, NOMBRE_TABLA = @NOMBRE_TABLA, FECHA_CREACION = @FECHA_CREACION, " +
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

        public static void INSERT(ref OBJ_TABLAS objeto)
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
                        cmd.Parameters.AddWithValue("@NOMBRE_TABLA", objeto.NOMBRE_TABLA);
                        cmd.Parameters.AddWithValue("@ACTIVO", objeto.ACTIVO);
                        cmd.Parameters.AddWithValue("@FECHA_CREACION", objeto.FECHA_CREACION);
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_TABLA = scope;
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

        public static void UPDATE(ref OBJ_TABLAS objeto)
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
                        cmd.Parameters.AddWithValue("@NOMBRE_TABLA", objeto.NOMBRE_TABLA);
                        cmd.Parameters.AddWithValue("@ACTIVO", objeto.ACTIVO);
                        cmd.Parameters.AddWithValue("@FECHA_CREACION", objeto.FECHA_CREACION);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_TABLA);
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

        public static void DELETE(ref OBJ_TABLAS objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_TABLA);
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

        public static void LLENAOBJETO(ref OBJ_TABLAS objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_TABLA);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        objeto.ID_TABLA = int.Parse(reader["ID_TABLA"].ToString());
                        objeto.ID_EMPRESA = int.Parse(reader["ID_EMPRESA"].ToString());
                        objeto.NOMBRE_TABLA = reader["NOMBRE_TABLA"].ToString();
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

        public static void PREPARAOBJETO(ref OBJ_TABLAS objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //   
                objeto.ID_EMPRESA = 0;
                objeto.NOMBRE_TABLA = "VACIO";
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
