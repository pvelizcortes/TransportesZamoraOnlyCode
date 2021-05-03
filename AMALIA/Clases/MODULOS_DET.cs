using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace AMALIAFW
{
    public class OBJ_MODULOS_DET
    {
        public int ID_MODULO_DETALLE { get; set; }
        public int ID_EMPRESA { get; set; }
        public int ID_MODULO { get; set; }
        public int ID_MODULO_SECCION { get; set; }
        public string LABEL { get; set; }
        public int ID_TABLA { get; set; }
        public int ID_TABLA_DETALLE { get; set; }
        public int ID_INPUT { get; set; }
        public int ORDEN { get; set; }
        public int ES_OBLIGATORIO { get; set; }
        public int TIPO_DETALLE { get; set; }
        public int ACTIVO { get; set; }
        public DateTime FECHA_CREACION { get; set; }  
        
        // AGREGAR EN TODAS LAS CLASES
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public class FN_MODULOS_DET
    {
        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "F_MODULO_DETALLE";
        private static string nombre_llave = "ID_MODULO_DETALLE";
        private static string value_insert = " ID_EMPRESA, ID_MODULO, ID_MODULO_SECCION, LABEL, ID_TABLA, ID_TABLA_DETALLE, ID_INPUT, ORDEN, ES_OBLIGATORIO, TIPO_DETALLE, ACTIVO, FECHA_CREACION ";
        private static string value_insert2 = " @ID_EMPRESA, @ID_MODULO, @ID_MODULO_SECCION, @LABEL, @ID_TABLA, @ID_TABLA_DETALLE, @ID_INPUT, @ORDEN, @ES_OBLIGATORIO, @TIPO_DETALLE, @ACTIVO, @FECHA_CREACION ";
        private static string value_update =
        " ID_EMPRESA = @ID_EMPRESA, ID_MODULO = @ID_MODULO, ID_MODULO_SECCION = @ID_MODULO_SECCION, LABEL = @LABEL, ID_TABLA = @ID_TABLA, ID_TABLA_DETALLE = @ID_TABLA_DETALLE, " +
        " ID_INPUT = @ID_INPUT, ORDEN = @ORDEN, ES_OBLIGATORIO = @ES_OBLIGATORIO, TIPO_DETALLE = @TIPO_DETALLE, ACTIVO = @ACTIVO, FECHA_CREACION = @FECHA_CREACION " +
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

        public static void INSERT(ref OBJ_MODULOS_DET objeto)
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
                        cmd.Parameters.AddWithValue("@ID_MODULO", objeto.ID_MODULO);
                        cmd.Parameters.AddWithValue("@ID_MODULO_SECCION", objeto.ID_MODULO_SECCION);
                        cmd.Parameters.AddWithValue("@LABEL", objeto.LABEL);
                        cmd.Parameters.AddWithValue("@ID_TABLA", objeto.ID_TABLA);
                        cmd.Parameters.AddWithValue("@ID_TABLA_DETALLE", objeto.ID_TABLA_DETALLE);
                        cmd.Parameters.AddWithValue("@ID_INPUT", objeto.ID_INPUT);
                        cmd.Parameters.AddWithValue("@ORDEN", objeto.ORDEN);
                        cmd.Parameters.AddWithValue("@ES_OBLIGATORIO", objeto.ES_OBLIGATORIO);
                        cmd.Parameters.AddWithValue("@TIPO_DETALLE", objeto.TIPO_DETALLE);
                        cmd.Parameters.AddWithValue("@ACTIVO", objeto.ACTIVO);
                        cmd.Parameters.AddWithValue("@FECHA_CREACION", objeto.FECHA_CREACION);
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_MODULO_DETALLE = scope;
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

        public static void UPDATE(ref OBJ_MODULOS_DET objeto)
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
                        cmd.Parameters.AddWithValue("@ID_MODULO", objeto.ID_MODULO);
                        cmd.Parameters.AddWithValue("@ID_MODULO_SECCION", objeto.ID_MODULO_SECCION);
                        cmd.Parameters.AddWithValue("@LABEL", objeto.LABEL);
                        cmd.Parameters.AddWithValue("@ID_TABLA", objeto.ID_TABLA);
                        cmd.Parameters.AddWithValue("@ID_TABLA_DETALLE", objeto.ID_TABLA_DETALLE);
                        cmd.Parameters.AddWithValue("@ID_INPUT", objeto.ID_INPUT);
                        cmd.Parameters.AddWithValue("@ORDEN", objeto.ORDEN);
                        cmd.Parameters.AddWithValue("@ES_OBLIGATORIO", objeto.ES_OBLIGATORIO);
                        cmd.Parameters.AddWithValue("@TIPO_DETALLE", objeto.TIPO_DETALLE);
                        cmd.Parameters.AddWithValue("@ACTIVO", objeto.ACTIVO);
                        cmd.Parameters.AddWithValue("@FECHA_CREACION", objeto.FECHA_CREACION);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_MODULO_DETALLE);
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

        public static void DELETE(ref OBJ_MODULOS_DET objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_MODULO_DETALLE);
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

        public static void LLENAOBJETO(ref OBJ_MODULOS_DET objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_MODULO_DETALLE);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {    
                        objeto.ID_MODULO_DETALLE = int.Parse(reader["ID_MODULO_DETALLE"].ToString());
                        objeto.ID_EMPRESA = int.Parse(reader["ID_EMPRESA"].ToString());
                        objeto.ID_MODULO = int.Parse(reader["ID_MODULO"].ToString());
                        objeto.ID_MODULO_SECCION = int.Parse(reader["ID_MODULO_SECCION"].ToString());
                        objeto.LABEL = reader["LABEL"].ToString();
                        objeto.ID_TABLA = int.Parse(reader["ID_TABLA"].ToString());
                        objeto.ID_TABLA_DETALLE = int.Parse(reader["ID_TABLA_DETALLE"].ToString());
                        objeto.ID_INPUT = int.Parse(reader["ID_INPUT"].ToString());
                        objeto.ORDEN = int.Parse(reader["ORDEN"].ToString());
                        objeto.ES_OBLIGATORIO = int.Parse(reader["ES_OBLIGATORIO"].ToString());
                        objeto.TIPO_DETALLE = int.Parse(reader["TIPO_DETALLE"].ToString());
                        objeto.ACTIVO = int.Parse(reader["ACTIVO"].ToString());
                        objeto.FECHA_CREACION = DateTime.Parse(reader["FECHA_CREACION"].ToString());
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

        public static void PREPARAOBJETO(ref OBJ_MODULOS_DET objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //
                objeto.LABEL = "VACIO";
                objeto.ES_OBLIGATORIO = 0;
                objeto.ACTIVO = 1;
                objeto.FECHA_CREACION = DateTime.Now;
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
