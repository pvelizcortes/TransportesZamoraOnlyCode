using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_REQUERIMIENTOS
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_REQUERIMIENTO { get; set; }

        public DateTime fecha { get; set; }
        public int id_camion { get; set; }
        public int id_rampla { get; set; }
        public int id_usuario { get; set; }
        public string solicitante { get; set; }
        public string descripcion { get; set; }
        public string urgencia { get; set; }
        public int id_ot { get; set; }
        public string resolucion { get; set; }
        public int id_conductor { get; set; }
        public string estado { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_REQUERIMIENTOS
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "MANT_REQUERIMIENTO";
        private static string nombre_llave = "ID_REQUERIMIENTO";
        private static string value_insert = "fecha, id_camion, id_rampla, id_usuario, solicitante, descripcion, urgencia, id_ot, resolucion, id_conductor, estado";
        private static string value_insert2 = "@fecha, @id_camion, @id_rampla, @id_usuario, @solicitante, @descripcion, @urgencia, @id_ot, @resolucion, @id_conductor, @estado";
        private static string value_update = "fecha = @fecha, " +
        "id_camion = @id_camion, " +
        "id_rampla = @id_rampla, " +
        "id_usuario = @id_usuario, " +
        "solicitante = @solicitante, " +
        "descripcion = @descripcion, " +
        "urgencia = @urgencia, " +
        "id_ot = @id_ot, " +
        "resolucion = @resolucion, " +
        "id_conductor = @id_conductor, " +
        "estado = @estado " +
        " WHERE " + nombre_llave + " = @" + nombre_llave + " ";
        // **************************************** **************************************** **************************************** //


        public static DataTable LLENADT(string sql_where = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connex))
            {
                conn.Open();
                string sql = @"SELECT *  from  V_REQUERIMIENTOS " + sql_where;
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter ap = new SqlDataAdapter(cmd);
                ap.Fill(dt);
            }
            return dt;
        }

        public static void INSERT(ref OBJ_REQUERIMIENTOS objeto)
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
                        // fecha, id_camion,, id_usuario, solicitante, descripcion, urgencia, id_ot, resolucion, id_conductor, estado
                        cmd.Parameters.AddWithValue("@fecha", objeto.fecha);
                        cmd.Parameters.AddWithValue("@id_camion", objeto.id_camion);
                        cmd.Parameters.AddWithValue("@id_rampla", objeto.id_rampla);
                        cmd.Parameters.AddWithValue("@id_usuario", objeto.id_usuario);
                        cmd.Parameters.AddWithValue("@solicitante", objeto.solicitante);
                        cmd.Parameters.AddWithValue("@descripcion", objeto.descripcion);
                        cmd.Parameters.AddWithValue("@urgencia", objeto.urgencia);
                        cmd.Parameters.AddWithValue("@id_ot", objeto.id_ot);
                        cmd.Parameters.AddWithValue("@resolucion", objeto.resolucion);
                        cmd.Parameters.AddWithValue("@id_conductor", objeto.id_conductor);
                        cmd.Parameters.AddWithValue("@estado", objeto.estado);
                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_REQUERIMIENTO = scope;

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

        public static void UPDATE(ref OBJ_REQUERIMIENTOS objeto)
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
                        cmd.Parameters.AddWithValue("@fecha", objeto.fecha);
                        cmd.Parameters.AddWithValue("@id_camion", objeto.id_camion);
                        cmd.Parameters.AddWithValue("@id_rampla", objeto.id_rampla);
                        cmd.Parameters.AddWithValue("@id_usuario", objeto.id_usuario);
                        cmd.Parameters.AddWithValue("@solicitante", objeto.solicitante);
                        cmd.Parameters.AddWithValue("@descripcion", objeto.descripcion);
                        cmd.Parameters.AddWithValue("@urgencia", objeto.urgencia);
                        cmd.Parameters.AddWithValue("@id_ot", objeto.id_ot);
                        cmd.Parameters.AddWithValue("@resolucion", objeto.resolucion);
                        cmd.Parameters.AddWithValue("@id_conductor", objeto.id_conductor);
                        cmd.Parameters.AddWithValue("@estado", objeto.estado);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_REQUERIMIENTO);
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

        public static void DELETE(ref OBJ_REQUERIMIENTOS objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_REQUERIMIENTO);
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

        public static void LLENAOBJETO(ref OBJ_REQUERIMIENTOS objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_REQUERIMIENTO);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // fecha, id_camion, id_usuario, solicitante, descripcion, urgencia, id_ot, resolucion, id_conductor, estado
                        objeto.fecha = DateTime.Parse(reader["fecha"].ToString());
                        objeto.id_camion = int.Parse(reader["id_camion"].ToString());
                        objeto.id_rampla = int.Parse(reader["id_rampla"].ToString());
                        objeto.id_usuario = int.Parse(reader["id_usuario"].ToString());
                        objeto.solicitante = reader["solicitante"].ToString();
                        objeto.descripcion = reader["descripcion"].ToString();
                        objeto.urgencia = reader["urgencia"].ToString();
                        objeto.id_ot = int.Parse(reader["id_ot"].ToString());
                        objeto.resolucion = reader["resolucion"].ToString();
                        objeto.id_conductor = int.Parse(reader["id_conductor"].ToString());
                        objeto.estado = reader["estado"].ToString();
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

        public static void PREPARAOBJETO(ref OBJ_REQUERIMIENTOS objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** // 
                objeto.fecha = DateTime.Parse("01/01/1900");
                objeto.id_camion = 0;
                objeto.id_rampla = 0;
                objeto.id_usuario = 0;
                objeto.solicitante = " ";
                objeto.descripcion = " ";
                objeto.urgencia = " ";
                objeto.id_ot = 0;
                objeto.resolucion = " ";
                objeto.id_conductor = 0;
                objeto.estado = " ";
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
