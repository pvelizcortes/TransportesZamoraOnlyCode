using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_GD_CAMION
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_CAMION { get; set; }

        public string status { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        public DateTime venc_rev_tecnica { get; set; }
        public DateTime venc_perm_circulacion { get; set; }
        public DateTime vec_seguro_obligatorio { get; set; }
        public int kilometraje { get; set; }
        public string doc_rev_tecnica_bd { get; set; }
        public string doc_rev_tecnica_real { get; set; }
        public string nombre_faena { get; set; }
        public DateTime venc_faena { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }

    }


    public static class FN_GD_CAMION
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "GD_CAMION";
        private static string nombre_llave = "ID_CAMION";
        private static string value_insert = "id_camion, status, fecha_actualizacion, venc_rev_tecnica, venc_perm_circulacion, vec_seguro_obligatorio, kilometraje, doc_rev_tecnica_bd, doc_rev_tecnica_real, nombre_faena, venc_faena";
        private static string value_insert2 = "@id_camion, @status, @fecha_actualizacion, @venc_rev_tecnica, @venc_perm_circulacion, @vec_seguro_obligatorio, @kilometraje, @doc_rev_tecnica_bd, @doc_rev_tecnica_real, @nombre_faena, @venc_faena";
        private static string value_update = "id_camion = @id_camion, " +
        "status = @status, " +
        "fecha_actualizacion = @fecha_actualizacion, " +
        "venc_rev_tecnica = @venc_rev_tecnica, " +
        "venc_perm_circulacion = @venc_perm_circulacion, " +
        "vec_seguro_obligatorio = @vec_seguro_obligatorio, " +
        "kilometraje = @kilometraje, " +
        "doc_rev_tecnica_bd = @doc_rev_tecnica_bd, " +
        "doc_rev_tecnica_real = @doc_rev_tecnica_real, " +
        "nombre_faena = @nombre_faena, " +
        "venc_faena = @venc_faena " +
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

        public static void INSERT(ref OBJ_GD_CAMION objeto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connex))
                {
                    conn.Open();
                    string sql = @"insert into " + nombre_tabla +
                    " ( " + value_insert + ") values" +
                    " (" + value_insert2 + ");";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // CAMBIAR AQUI //
                        // **************************************** **************************************** **************************************** // 
                        //  patente, num_chasis, num_motor, marca, ano, carga, activo
                        cmd.Parameters.AddWithValue("@id_camion", objeto.ID_CAMION);
                        cmd.Parameters.AddWithValue("@status", objeto.status);
                        cmd.Parameters.AddWithValue("@fecha_actualizacion", objeto.fecha_actualizacion);
                        cmd.Parameters.AddWithValue("@venc_rev_tecnica", objeto.venc_rev_tecnica);
                        cmd.Parameters.AddWithValue("@venc_perm_circulacion", objeto.venc_perm_circulacion);
                        cmd.Parameters.AddWithValue("@vec_seguro_obligatorio", objeto.vec_seguro_obligatorio);
                        cmd.Parameters.AddWithValue("@kilometraje", objeto.kilometraje);
                        cmd.Parameters.AddWithValue("@doc_rev_tecnica_bd", objeto.doc_rev_tecnica_bd);
                        cmd.Parameters.AddWithValue("@doc_rev_tecnica_real", objeto.doc_rev_tecnica_real);
                        cmd.Parameters.AddWithValue("@nombre_faena", objeto.nombre_faena);
                        cmd.Parameters.AddWithValue("@venc_faena", objeto.venc_faena);
                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        //objeto.ID_CAMION = scope;

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

        public static void UPDATE(ref OBJ_GD_CAMION objeto)
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
                        cmd.Parameters.AddWithValue("@status", objeto.status);
                        cmd.Parameters.AddWithValue("@fecha_actualizacion", objeto.fecha_actualizacion);
                        cmd.Parameters.AddWithValue("@venc_rev_tecnica", objeto.venc_rev_tecnica);
                        cmd.Parameters.AddWithValue("@venc_perm_circulacion", objeto.venc_perm_circulacion);
                        cmd.Parameters.AddWithValue("@vec_seguro_obligatorio", objeto.vec_seguro_obligatorio);
                        cmd.Parameters.AddWithValue("@kilometraje", objeto.kilometraje);
                        cmd.Parameters.AddWithValue("@doc_rev_tecnica_bd", objeto.doc_rev_tecnica_bd);
                        cmd.Parameters.AddWithValue("@doc_rev_tecnica_real", objeto.doc_rev_tecnica_real);
                        cmd.Parameters.AddWithValue("@nombre_faena", objeto.nombre_faena);
                        cmd.Parameters.AddWithValue("@venc_faena", objeto.venc_faena);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_CAMION);
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

        public static void DELETE(ref OBJ_GD_CAMION objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_CAMION);
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

        public static void LLENAOBJETO(ref OBJ_GD_CAMION objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_CAMION);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // patente, num_chasis, num_motor, marca, ano, carga, activo    
                        objeto.status = reader["status"].ToString();
                        objeto.fecha_actualizacion = DateTime.Parse(reader["fecha_actualizacion"].ToString());
                        objeto.venc_rev_tecnica = DateTime.Parse(reader["venc_rev_tecnica"].ToString());
                        objeto.venc_perm_circulacion = DateTime.Parse(reader["venc_perm_circulacion"].ToString());
                        objeto.vec_seguro_obligatorio = DateTime.Parse(reader["vec_seguro_obligatorio"].ToString());
                        objeto.kilometraje = int.Parse(reader["kilometraje"].ToString());
                        objeto.doc_rev_tecnica_bd = reader["doc_rev_tecnica_bd"].ToString();
                        objeto.doc_rev_tecnica_real = reader["doc_rev_tecnica_real"].ToString();
                        objeto.nombre_faena = reader["nombre_faena"].ToString();
                        objeto.venc_faena = DateTime.Parse(reader["venc_faena"].ToString());

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

        public static void PREPARAOBJETO(ref OBJ_GD_CAMION objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.ID_CAMION = 0;
                objeto.status = " ";
                objeto.fecha_actualizacion = DateTime.Parse("01/01/1900");
                objeto.venc_rev_tecnica = DateTime.Parse("01/01/1900");
                objeto.venc_perm_circulacion = DateTime.Parse("01/01/1900");
                objeto.vec_seguro_obligatorio = DateTime.Parse("01/01/1900");
                objeto.kilometraje = 0;
                objeto.doc_rev_tecnica_bd = " ";
                objeto.doc_rev_tecnica_real = " ";
                objeto.nombre_faena = " ";
                objeto.venc_faena = DateTime.Parse("01/01/1900");
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
