using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_CONDUCTOR
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_cONDUCTOR { get; set; }

        public string rut { get; set; }
        public string nombre_completo { get; set; }
        public string telefono { get; set; }
        public string telefono2 { get; set; }
        public string direccion { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string activo { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_CONDUCTOR
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "conductor";
        private static string nombre_llave = "id_conductor";
        private static string value_insert = " rut, nombre_completo, telefono, telefono2, direccion, fecha_nacimiento, activo ";
        private static string value_insert2 = " @rut, @nombre_completo, @telefono, @telefono2, @direccion, @fecha_nacimiento, @activo ";
        private static string value_update =
        " rut = @rut, " +
        " nombre_completo = @nombre_completo, " +
        " telefono = @telefono, " +
        " telefono2 = @telefono2, " +
        " direccion = @direccion, " +
        " fecha_nacimiento = @fecha_nacimiento, " +
        " activo = @activo " + // <----------- EL ULTIMO SIN COMA PLS <3

        " WHERE " + nombre_llave + " = @" + nombre_llave + " ";
        // **************************************** **************************************** **************************************** //


        public static DataTable LLENADT(string sql_where = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connex))
            {
                conn.Open();
                string sql = @"SELECT *  from " + nombre_tabla + sql_where;
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter ap = new SqlDataAdapter(cmd);
                ap.Fill(dt);
            }
            return dt;
        }

        public static void INSERT(ref OBJ_CONDUCTOR objeto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connex))
                {
                    conn.Open();
                    string sql = @"insert into " + nombre_tabla +
                    " ( " + value_insert + ") values" +
                    " (" + value_insert2 + "); SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // CAMBIAR AQUI //
                        // **************************************** **************************************** **************************************** // 
                        // id_perfil, nombre_completo, correo, telefono, direccion, usuario, pass, activo
                        cmd.Parameters.AddWithValue("@rut", objeto.rut);
                        cmd.Parameters.AddWithValue("@nombre_completo", objeto.nombre_completo);
                        cmd.Parameters.AddWithValue("@telefono", objeto.telefono);
                        cmd.Parameters.AddWithValue("@telefono2", objeto.telefono2);
                        cmd.Parameters.AddWithValue("@direccion", objeto.direccion);
                        cmd.Parameters.AddWithValue("@fecha_nacimiento", objeto.fecha_nacimiento);
                        cmd.Parameters.AddWithValue("@activo", objeto.activo);

                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_cONDUCTOR = scope;

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

        public static void UPDATE(ref OBJ_CONDUCTOR objeto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connex))
                {
                    conn.Open();
                    string sql = @"update " + nombre_tabla + " set " +
                    value_update;
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // CAMBIAR AQUI //
                        // **************************************** **************************************** **************************************** //   
                        cmd.Parameters.AddWithValue("@rut", objeto.rut);
                        cmd.Parameters.AddWithValue("@nombre_completo", objeto.nombre_completo);
                        cmd.Parameters.AddWithValue("@telefono", objeto.telefono);
                        cmd.Parameters.AddWithValue("@telefono2", objeto.telefono2);
                        cmd.Parameters.AddWithValue("@direccion", objeto.direccion);
                        cmd.Parameters.AddWithValue("@fecha_nacimiento", objeto.fecha_nacimiento);
                        cmd.Parameters.AddWithValue("@activo", objeto.activo);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_cONDUCTOR);
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

        public static void DELETE(ref OBJ_CONDUCTOR objeto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connex))
                {
                    conn.Open();
                    string sql = @"DELETE FROM " + nombre_tabla + " WHERE " + nombre_llave + " = @" + nombre_llave + "";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // CAMBIAR AQUI //
                        // **************************************** **************************************** **************************************** //   
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_cONDUCTOR);
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

        public static void LLENAOBJETO(ref OBJ_CONDUCTOR objeto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connex))
                {
                    conn.Open();
                    string sql = @"SELECT * from " + nombre_tabla + " where " + nombre_llave + " = @" + nombre_llave;
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    // CAMBIAR AQUI //
                    // **************************************** **************************************** **************************************** //   
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_cONDUCTOR);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // rut, nombre_completo, telefono, telefono2, direccion, fecha_nacimiento, activo
                        objeto.rut = reader["rut"].ToString();
                        objeto.nombre_completo = reader["nombre_completo"].ToString();
                        objeto.telefono = reader["telefono"].ToString();
                        objeto.telefono2 = reader["telefono2"].ToString();
                        objeto.direccion = reader["direccion"].ToString();
                        objeto.fecha_nacimiento = DateTime.Parse(reader["fecha_nacimiento"].ToString());
                        objeto.activo = reader["activo"].ToString();
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

        public static void PREPARAOBJETO(ref OBJ_CONDUCTOR objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.rut = "NO";
                objeto.nombre_completo = "SIN NOMBRE";
                objeto.telefono = "NO";
                objeto.telefono2 = "NO";
                objeto.direccion = "NO";
                objeto.fecha_nacimiento = DateTime.Now;
                objeto.activo = "ACTIVO";
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
