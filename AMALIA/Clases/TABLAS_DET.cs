using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace AMALIAFW
{
    public class OBJ_TABLA_DET
    {
        public int ID_TABLA_DETALLE { get; set; }
        public int ID_TABLA { get; set; }
        public int ID_EMPRESA { get; set; }
        public string NOMBRE_COLUMNA { get; set; }
        public int ID_TIPO_PARAMETRO { get; set; }
        public string ACTIVO { get; set; }
        // AGREGAR EN TODAS LAS CLASES
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public class FN_TABLA_DET
    {
        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "F_TABLA_DETALLE";
        private static string nombre_llave = "ID_TABLA_DETALLE";
        private static string value_insert = " ID_EMPRESA, ID_TABLA, NOMBRE_COLUMNA, ID_TIPO_PARAMETRO, ACTIVO ";
        private static string value_insert2 = " @ID_EMPRESA, @ID_TABLA, @NOMBRE_COLUMNA, @ID_TIPO_PARAMETRO, @ACTIVO ";
        private static string value_update =
        " ID_EMPRESA = @ID_EMPRESA, ID_TABLA = @ID_TABLA, NOMBRE_COLUMNA = @NOMBRE_COLUMNA, ID_TIPO_PARAMETRO = @ID_TIPO_PARAMETRO, " +
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

        public static void INSERT(ref OBJ_TABLA_DET objeto)
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
                        cmd.Parameters.AddWithValue("@NOMBRE_COLUMNA", objeto.NOMBRE_COLUMNA);
                        cmd.Parameters.AddWithValue("@ACTIVO", objeto.ACTIVO);
                        cmd.Parameters.AddWithValue("@ID_TABLA", objeto.ID_TABLA);
                        cmd.Parameters.AddWithValue("@ID_TIPO_PARAMETRO", objeto.ID_TIPO_PARAMETRO);
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_TABLA_DETALLE = scope;
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

        public static void UPDATE(ref OBJ_TABLA_DET objeto)
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
                        cmd.Parameters.AddWithValue("@NOMBRE_COLUMNA", objeto.NOMBRE_COLUMNA);
                        cmd.Parameters.AddWithValue("@ACTIVO", objeto.ACTIVO);
                        cmd.Parameters.AddWithValue("@ID_TABLA", objeto.ID_TABLA);
                        cmd.Parameters.AddWithValue("@ID_TIPO_PARAMETRO", objeto.ID_TIPO_PARAMETRO);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_TABLA_DETALLE);
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

        public static void DELETE(ref OBJ_TABLA_DET objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_TABLA_DETALLE);
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

        public static void LLENAOBJETO(ref OBJ_TABLA_DET objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_TABLA_DETALLE);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        objeto.ID_TABLA_DETALLE = int.Parse(reader["ID_TABLA_DETALLE"].ToString());
                        objeto.ID_TABLA = int.Parse(reader["ID_TABLA"].ToString());
                        objeto.ID_EMPRESA = int.Parse(reader["ID_EMPRESA"].ToString());
                        objeto.NOMBRE_COLUMNA = reader["NOMBRE_COLUMNA"].ToString();                      
                        objeto.ACTIVO = reader["ACTIVO"].ToString();
                        objeto.ID_TIPO_PARAMETRO = int.Parse(reader["ID_TIPO_PARAMETRO"].ToString());
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

        public static void PREPARAOBJETO(ref OBJ_TABLA_DET objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //   
                objeto.ID_EMPRESA = 0;
                objeto.ID_TABLA = 0;
                objeto.NOMBRE_COLUMNA = "VACIO";
                objeto.ID_TIPO_PARAMETRO = 0;
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
