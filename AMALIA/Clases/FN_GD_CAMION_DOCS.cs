using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_GD_CAMION_DOCS
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_DETALLE_DOCUMENTO { get; set; }

        public int id_gd_camion { get; set; }
        public string nombre_documento { get; set; }
        public DateTime fecha_documento { get; set; }
        public DateTime fecha_vencimiento { get; set; }
        public int dias_anticipacion { get; set; }
        public string nombre_archivo_real { get; set; }
        public string nombre_archivo_bd { get; set; }
        public string estado { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_GD_CAMION_DOCS
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "GD_CAMION_DOCS";
        private static string nombre_llave = "ID_DETALLE_DOCUMENTO";
        private static string value_insert = "id_gd_camion, nombre_documento, fecha_documento, fecha_vencimiento, dias_anticipacion, nombre_archivo_real, nombre_archivo_bd, estado, fecha_actualizacion";
        private static string value_insert2 = "@id_gd_camion, @nombre_documento, @fecha_documento, @fecha_vencimiento, @dias_anticipacion, @nombre_archivo_real, @nombre_archivo_bd, @estado, @fecha_actualizacion";
        private static string value_update = "id_gd_camion = @id_gd_camion, " +
        "nombre_documento = @nombre_documento, " +
        "fecha_documento = @fecha_documento, " +
        "fecha_vencimiento = @fecha_vencimiento, " +
        "dias_anticipacion = @dias_anticipacion, " +
        "nombre_archivo_real = @nombre_archivo_real, " +
        "nombre_archivo_bd = @nombre_archivo_bd, " +
        "estado = @estado, " +
        "fecha_actualizacion = @fecha_actualizacion " +
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

        public static void INSERT(ref OBJ_GD_CAMION_DOCS objeto)
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
                        cmd.Parameters.AddWithValue("@id_gd_camion", objeto.id_gd_camion);
                        cmd.Parameters.AddWithValue("@nombre_documento", objeto.nombre_documento);
                        cmd.Parameters.AddWithValue("@fecha_documento", objeto.fecha_documento);
                        cmd.Parameters.AddWithValue("@fecha_vencimiento", objeto.fecha_vencimiento);
                        cmd.Parameters.AddWithValue("@dias_anticipacion", objeto.dias_anticipacion);
                        cmd.Parameters.AddWithValue("@nombre_archivo_real", objeto.nombre_archivo_real);
                        cmd.Parameters.AddWithValue("@nombre_archivo_bd", objeto.nombre_archivo_bd);
                        cmd.Parameters.AddWithValue("@estado", objeto.estado);
                        cmd.Parameters.AddWithValue("@fecha_actualizacion", objeto.fecha_actualizacion);


                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_DETALLE_DOCUMENTO = scope;

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

        public static void UPDATE(ref OBJ_GD_CAMION_DOCS objeto)
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
                        cmd.Parameters.AddWithValue("@id_gd_camion", objeto.id_gd_camion);
                        cmd.Parameters.AddWithValue("@nombre_documento", objeto.nombre_documento);
                        cmd.Parameters.AddWithValue("@fecha_documento", objeto.fecha_documento);
                        cmd.Parameters.AddWithValue("@fecha_vencimiento", objeto.fecha_vencimiento);
                        cmd.Parameters.AddWithValue("@dias_anticipacion", objeto.dias_anticipacion);
                        cmd.Parameters.AddWithValue("@nombre_archivo_real", objeto.nombre_archivo_real);
                        cmd.Parameters.AddWithValue("@nombre_archivo_bd", objeto.nombre_archivo_bd);
                        cmd.Parameters.AddWithValue("@estado", objeto.estado);
                        cmd.Parameters.AddWithValue("@fecha_actualizacion", objeto.fecha_actualizacion);

                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_DETALLE_DOCUMENTO);
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

        public static void DELETE(ref OBJ_GD_CAMION_DOCS objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_DETALLE_DOCUMENTO);
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

        public static void LLENAOBJETO(ref OBJ_GD_CAMION_DOCS objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_DETALLE_DOCUMENTO);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        objeto.id_gd_camion = int.Parse(reader["id_gd_camion"].ToString());
                        objeto.nombre_documento = reader["nombre_documento"].ToString();
                        objeto.fecha_documento = DateTime.Parse(reader["fecha_documento"].ToString());
                        objeto.fecha_vencimiento = DateTime.Parse(reader["fecha_vencimiento"].ToString());
                        objeto.dias_anticipacion = int.Parse(reader["dias_anticipacion"].ToString());
                        objeto.nombre_archivo_real = reader["nombre_archivo_real"].ToString();
                        objeto.nombre_archivo_bd = reader["nombre_archivo_bd"].ToString();
                        objeto.estado = reader["estado"].ToString();
                        objeto.fecha_actualizacion = DateTime.Parse(reader["fecha_actualizacion"].ToString());
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

        public static void PREPARAOBJETO(ref OBJ_GD_CAMION_DOCS objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.id_gd_camion = 0;
                objeto.nombre_documento = " ";
                objeto.fecha_documento = DateTime.Parse("01/01/1900");
                objeto.fecha_vencimiento = DateTime.Parse("01/01/1900");
                objeto.dias_anticipacion = 0;
                objeto.nombre_archivo_real = " ";
                objeto.nombre_archivo_bd = " ";
                objeto.estado = " ";
                objeto.fecha_actualizacion = DateTime.Parse("01/01/1900");
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
