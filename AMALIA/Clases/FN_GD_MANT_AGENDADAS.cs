using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_GD_MANT_AGENDADAS
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_MANT_PENDIENTE { get; set; }

        public int id_gd_camion { get; set; }
        public string nombre_mant_pendiente { get; set; }
        public DateTime fecha_mant { get; set; }
        public string prioridad { get; set; }
        public int aviso_dias_anticipacion { get; set; }
        public string observacion { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_GD_MANT_AGENDADAS
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "GD_MANT_AGENDADAS";
        private static string nombre_llave = "ID_MANT_PENDIENTE";
        private static string value_insert = "id_gd_camion, nombre_mant_pendiente, fecha_mant, prioridad, aviso_dias_anticipacion, observacion";
        private static string value_insert2 = "@id_gd_camion, @nombre_mant_pendiente, @fecha_mant, @prioridad, @aviso_dias_anticipacion, @observacion";
        private static string value_update = "id_gd_camion = @id_gd_camion, " +
        "nombre_mant_pendiente = @nombre_mant_pendiente, " +
        "fecha_mant = @fecha_mant, " +
        "prioridad = @prioridad, " +
        "aviso_dias_anticipacion = @aviso_dias_anticipacion, " +
        "observacion = @observacion " +
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

        public static void INSERT(ref OBJ_GD_MANT_AGENDADAS objeto)
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
                        //  patente, num_chasis, num_motor, marca, ano, carga, activo
                        cmd.Parameters.AddWithValue("@id_gd_camion", objeto.id_gd_camion);
                        cmd.Parameters.AddWithValue("@nombre_mant_pendiente", objeto.nombre_mant_pendiente);
                        cmd.Parameters.AddWithValue("@fecha_mant", objeto.fecha_mant);
                        cmd.Parameters.AddWithValue("@prioridad", objeto.prioridad);
                        cmd.Parameters.AddWithValue("@aviso_dias_anticipacion", objeto.aviso_dias_anticipacion);
                        cmd.Parameters.AddWithValue("@observacion", objeto.observacion);

                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_MANT_PENDIENTE = scope;

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

        public static void UPDATE(ref OBJ_GD_MANT_AGENDADAS objeto)
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
                        cmd.Parameters.AddWithValue("@id_gd_camion", objeto.id_gd_camion);
                        cmd.Parameters.AddWithValue("@nombre_mant_pendiente", objeto.nombre_mant_pendiente);
                        cmd.Parameters.AddWithValue("@fecha_mant", objeto.fecha_mant);
                        cmd.Parameters.AddWithValue("@prioridad", objeto.prioridad);
                        cmd.Parameters.AddWithValue("@aviso_dias_anticipacion", objeto.aviso_dias_anticipacion);
                        cmd.Parameters.AddWithValue("@observacion", objeto.observacion);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_MANT_PENDIENTE);
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

        public static void DELETE(ref OBJ_GD_MANT_AGENDADAS objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_MANT_PENDIENTE);
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

        public static void LLENAOBJETO(ref OBJ_GD_MANT_AGENDADAS objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_MANT_PENDIENTE);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // patente, num_chasis, num_motor, marca, ano, carga, activo
                        objeto.id_gd_camion = int.Parse(reader["id_gd_camion"].ToString());
                        objeto.nombre_mant_pendiente = reader["nombre_mant_pendiente"].ToString();
                        objeto.fecha_mant = DateTime.Parse(reader["fecha_mant"].ToString());
                        objeto.prioridad = reader["prioridad"].ToString();
                        objeto.aviso_dias_anticipacion = int.Parse(reader["aviso_dias_anticipacion"].ToString());
                        objeto.observacion = reader["observacion"].ToString();
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

        public static void PREPARAOBJETO(ref OBJ_GD_MANT_AGENDADAS objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.id_gd_camion = 0;
                objeto.nombre_mant_pendiente = " ";
                objeto.fecha_mant = DateTime.Parse("01/01/1900");
                objeto.prioridad = " ";
                objeto.aviso_dias_anticipacion = 0;
                objeto.observacion = " ";
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

        // NUEVAAAAAAAAAAAAAAAAAAAAAS
        public static DataTable LLENADT_VISTA(string sql_where = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connex))
            {
                conn.Open();
                string sql = @"SELECT *  from V_GD_MANT_CAMIONES " + sql_where;
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter ap = new SqlDataAdapter(cmd);
                ap.Fill(dt);
            }
            return dt;
        }
    }
}
