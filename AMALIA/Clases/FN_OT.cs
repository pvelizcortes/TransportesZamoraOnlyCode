using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_OT
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int id_ot { get; set; }
        public int id_requerimiento { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        public int id_estado_ot { get; set; }
        public string nom_estado_ot { get; set; }
        public int id_camion { get; set; }
        public int id_rampla { get; set; }
        public string solicitante { get; set; }
        public int id_usuario { get; set; }
        public string descripcion { get; set; }
        public string urgencia { get; set; }
        public string resolucion { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_OT
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "MANT_OT";
        private static string nombre_llave = "ID_OT";
        private static string value_insert = "id_ot, id_requerimiento, fecha_creacion, fecha_actualizacion, id_estado_ot, nom_estado_ot, id_camion, id_rampla, solicitante, id_usuario, descripcion, urgencia, resolucion";
        private static string value_insert2 = "@id_ot, @id_requerimiento, @fecha_creacion, @fecha_actualizacion, @id_estado_ot, @nom_estado_ot, @id_camion, @id_rampla, @solicitante, @id_usuario, @descripcion, @urgencia, @resolucion";
        private static string value_update = " " +
        "id_requerimiento = @id_requerimiento, " +
        "fecha_creacion = @fecha_creacion, " +
        "fecha_actualizacion = @fecha_actualizacion, " +
        "id_estado_ot = @id_estado_ot, " +
        "nom_estado_ot = @nom_estado_ot, " +
        "id_camion = @id_camion, " +
        "id_rampla = @id_rampla, " +
        "solicitante = @solicitante, " +
        "id_usuario = @id_usuario, " +
        "descripcion = @descripcion, " +
        "urgencia = @urgencia, " +
        "resolucion = @resolucion " +
        " WHERE " + nombre_llave + " = @" + nombre_llave + " ";
        // **************************************** **************************************** **************************************** //


        public static DataTable LLENADT(string sql_where = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connex))
            {
                conn.Open();
                string sql = @"SELECT *  from  V_OT " + sql_where;
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter ap = new SqlDataAdapter(cmd);
                ap.Fill(dt);
            }
            return dt;
        }

        public static void INSERT(ref OBJ_OT objeto)
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
                        // id_requerimiento, fecha_creacion, fecha_actualizacion, id_estado_ot, nom_estado, id_camion, solicitante, id_usuario, descripcion, urgencia, resolucion
                        cmd.Parameters.AddWithValue("@id_ot", objeto.id_ot);
                        cmd.Parameters.AddWithValue("@id_requerimiento", objeto.id_requerimiento);
                        cmd.Parameters.AddWithValue("@fecha_creacion", objeto.fecha_creacion);
                        cmd.Parameters.AddWithValue("@fecha_actualizacion", objeto.fecha_actualizacion);
                        cmd.Parameters.AddWithValue("@id_estado_ot", objeto.id_estado_ot);
                        cmd.Parameters.AddWithValue("@nom_estado_ot", objeto.nom_estado_ot);
                        cmd.Parameters.AddWithValue("@id_camion", objeto.id_camion);
                        cmd.Parameters.AddWithValue("@id_rampla", objeto.id_rampla);
                        cmd.Parameters.AddWithValue("@solicitante", objeto.solicitante);
                        cmd.Parameters.AddWithValue("@id_usuario", objeto.id_usuario);
                        cmd.Parameters.AddWithValue("@descripcion", objeto.descripcion);
                        cmd.Parameters.AddWithValue("@urgencia", objeto.urgencia);
                        cmd.Parameters.AddWithValue("@resolucion", objeto.resolucion);
                        // **************************************** **************************************** **************************************** //  
                        cmd.ExecuteNonQuery();
                        // AQUI TAMBIEN
                        //objeto.id_ot = scope;

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

        public static void UPDATE(ref OBJ_OT objeto)
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
                        cmd.Parameters.AddWithValue("@id_requerimiento", objeto.id_requerimiento);
                        cmd.Parameters.AddWithValue("@fecha_creacion", objeto.fecha_creacion);
                        cmd.Parameters.AddWithValue("@fecha_actualizacion", objeto.fecha_actualizacion);
                        cmd.Parameters.AddWithValue("@id_estado_ot", objeto.id_estado_ot);
                        cmd.Parameters.AddWithValue("@nom_estado_ot", objeto.nom_estado_ot);
                        cmd.Parameters.AddWithValue("@id_camion", objeto.id_camion);
                        cmd.Parameters.AddWithValue("@id_rampla", objeto.id_rampla);
                        cmd.Parameters.AddWithValue("@solicitante", objeto.solicitante);
                        cmd.Parameters.AddWithValue("@id_usuario", objeto.id_usuario);
                        cmd.Parameters.AddWithValue("@descripcion", objeto.descripcion);
                        cmd.Parameters.AddWithValue("@urgencia", objeto.urgencia);
                        cmd.Parameters.AddWithValue("@resolucion", objeto.resolucion);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.id_ot);
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

        public static void DELETE(ref OBJ_OT objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.id_ot);
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

        public static void LLENAOBJETO(ref OBJ_OT objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.id_ot);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // id_requerimiento, fecha_creacion, fecha_actualizacion, id_estado_ot, nom_estado_ot, id_camion, 
                        // solicitante, id_usuario, descripcion, urgencia, resolucion
                        objeto.id_ot = int.Parse(reader["id_ot"].ToString());
                        objeto.id_requerimiento = int.Parse(reader["id_requerimiento"].ToString());
                        objeto.fecha_creacion = DateTime.Parse(reader["fecha_creacion"].ToString());
                        objeto.fecha_actualizacion = DateTime.Parse(reader["fecha_actualizacion"].ToString());
                        objeto.id_estado_ot = int.Parse(reader["id_estado_ot"].ToString());
                        objeto.nom_estado_ot = reader["nom_estado_ot"].ToString();
                        objeto.id_camion = int.Parse(reader["id_camion"].ToString());
                        objeto.id_rampla = int.Parse(reader["id_rampla"].ToString());
                        objeto.solicitante = reader["solicitante"].ToString();
                        objeto.id_usuario = int.Parse(reader["id_usuario"].ToString());
                        objeto.descripcion = reader["descripcion"].ToString();
                        objeto.urgencia = reader["urgencia"].ToString();
                        objeto.resolucion = reader["resolucion"].ToString();
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

        public static void PREPARAOBJETO(ref OBJ_OT objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** // 
                objeto.id_ot = 0;
                objeto.id_requerimiento = 0;
                objeto.fecha_creacion = DateTime.Parse("01/01/1900");
                objeto.fecha_actualizacion = DateTime.Parse("01/01/1900");
                objeto.id_estado_ot = 0;
                objeto.nom_estado_ot = " ";
                objeto.id_camion = 0;
                objeto.id_rampla = 0;
                objeto.solicitante = " ";
                objeto.id_usuario = 0;
                objeto.descripcion = " ";
                objeto.urgencia = " ";
                objeto.resolucion = " ";
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

        public static void INSERT_LOG(ref OBJ_OT objeto, string estado_i, string estado_f)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connex))
                {
                    conn.Open();
                    string sql = @"insert into MANT_OT_LOG " +
                    " (id_ot, fecha, observacion, estado_i, estado_f, id_usuario) values" +
                    " (@id_ot, @fecha, @observacion, @estado_i, @estado_f, @id_usuario); SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // CAMBIAR AQUI //
                        // **************************************** **************************************** **************************************** // 
                        // id_requerimiento, fecha_creacion, fecha_actualizacion, id_estado_ot, nom_estado, id_camion, solicitante, id_usuario, descripcion, urgencia, resolucion
                        cmd.Parameters.AddWithValue("@id_ot", objeto.id_ot);
                        cmd.Parameters.AddWithValue("@fecha", objeto.fecha_creacion);
                        cmd.Parameters.AddWithValue("@observacion", objeto.descripcion);
                        cmd.Parameters.AddWithValue("@estado_i", estado_i);
                        cmd.Parameters.AddWithValue("@estado_f", estado_f);
                        cmd.Parameters.AddWithValue("@id_usuario", objeto.id_usuario);
                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.id_ot = scope;

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
    }
}
