using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_HALLAZGO
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_HALLAZGO { get; set; }

        public long num_referencia { get; set; }
        public string nombre_remitente { get; set; }
        public DateTime fecha_envio { get; set; }
        public string area { get; set; }
        public string lugar { get; set; }
        public string deteccion { get; set; }
        public string tipo { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public string nombre_deteccion { get; set; }
        public string cargo { get; set; }
        public string deteccion_hallazgo { get; set; }
        public string doc_referencia { get; set; }
        public string accion_inmediata { get; set; }
        public string estado { get; set; }
        public string usuario { get; set; }
        public string empresainvolucrada { get; set; }
        public DateTime fecha { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_HALLAZGO
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "HALLAZGOS";
        private static string nombre_llave = "ID_HALLAZGO";
        private static string value_insert = "num_referencia, nombre_remitente, fecha_envio, area, lugar, deteccion, tipo, origen, destino, nombre_deteccion, cargo, deteccion_hallazgo, doc_referencia, accion_inmediata, estado, usuario, fecha, empresainvolucrada";
        private static string value_insert2 = "@num_referencia, @nombre_remitente, @fecha_envio, @area, @lugar, @deteccion, @tipo, @origen, @destino, @nombre_deteccion, @cargo, @deteccion_hallazgo, @doc_referencia, @accion_inmediata, @estado, @usuario, @fecha, @empresainvolucrada";
        private static string value_update = "num_referencia = @num_referencia, " +
        "nombre_remitente = @nombre_remitente, " +
        "fecha_envio = @fecha_envio, " +
        "area = @area, " +
        "lugar = @lugar, " +
        "deteccion = @deteccion, " +
        "tipo = @tipo, " +
        "origen = @origen, " +
                  "destino = @destino, " +
        "nombre_deteccion = @nombre_deteccion, " +
        "cargo = @cargo, " +
        "deteccion_hallazgo = @deteccion_hallazgo, " +
        "doc_referencia = @doc_referencia, " +
        "accion_inmediata = @accion_inmediata, " +
        "estado = @estado, " +
        "usuario = @usuario, " +
        "empresainvolucrada = @empresainvolucrada," +
        "fecha = @fecha " +
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

        public static void INSERT(ref OBJ_HALLAZGO objeto)
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
                        cmd.Parameters.AddWithValue("@num_referencia", objeto.num_referencia);
                        cmd.Parameters.AddWithValue("@nombre_remitente", objeto.nombre_remitente);
                        cmd.Parameters.AddWithValue("@fecha_envio", objeto.fecha_envio);
                        cmd.Parameters.AddWithValue("@area", objeto.area);
                        cmd.Parameters.AddWithValue("@lugar", objeto.lugar);
                        cmd.Parameters.AddWithValue("@deteccion", objeto.deteccion);
                        cmd.Parameters.AddWithValue("@tipo", objeto.tipo);
                        cmd.Parameters.AddWithValue("@origen", objeto.origen);
                        cmd.Parameters.AddWithValue("@destino", objeto.destino);
                        cmd.Parameters.AddWithValue("@nombre_deteccion", objeto.nombre_deteccion);
                        cmd.Parameters.AddWithValue("@cargo", objeto.cargo);
                        cmd.Parameters.AddWithValue("@deteccion_hallazgo", objeto.deteccion_hallazgo);
                        cmd.Parameters.AddWithValue("@doc_referencia", objeto.doc_referencia);
                        cmd.Parameters.AddWithValue("@accion_inmediata", objeto.accion_inmediata);
                        cmd.Parameters.AddWithValue("@estado", objeto.estado);
                        cmd.Parameters.AddWithValue("@usuario", objeto.usuario);
                        cmd.Parameters.AddWithValue("@fecha", objeto.fecha);
                        cmd.Parameters.AddWithValue("@empresainvolucrada", objeto.empresainvolucrada);

                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_HALLAZGO = scope;

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

        public static void UPDATE(ref OBJ_HALLAZGO objeto)
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
                        cmd.Parameters.AddWithValue("@num_referencia", objeto.num_referencia);
                        cmd.Parameters.AddWithValue("@nombre_remitente", objeto.nombre_remitente);
                        cmd.Parameters.AddWithValue("@fecha_envio", objeto.fecha_envio);
                        cmd.Parameters.AddWithValue("@area", objeto.area);
                        cmd.Parameters.AddWithValue("@lugar", objeto.lugar);
                        cmd.Parameters.AddWithValue("@deteccion", objeto.deteccion);
                        cmd.Parameters.AddWithValue("@tipo", objeto.tipo);
                        cmd.Parameters.AddWithValue("@origen", objeto.origen);
                        cmd.Parameters.AddWithValue("@destino", objeto.destino);
                        cmd.Parameters.AddWithValue("@nombre_deteccion", objeto.nombre_deteccion);
                        cmd.Parameters.AddWithValue("@cargo", objeto.cargo);
                        cmd.Parameters.AddWithValue("@deteccion_hallazgo", objeto.deteccion_hallazgo);
                        cmd.Parameters.AddWithValue("@doc_referencia", objeto.doc_referencia);
                        cmd.Parameters.AddWithValue("@accion_inmediata", objeto.accion_inmediata);
                        cmd.Parameters.AddWithValue("@estado", objeto.estado);
                        cmd.Parameters.AddWithValue("@usuario", objeto.usuario);
                        cmd.Parameters.AddWithValue("@fecha", objeto.fecha);
                        cmd.Parameters.AddWithValue("@empresainvolucrada", objeto.empresainvolucrada);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_HALLAZGO);
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

        public static void DELETE(ref OBJ_HALLAZGO objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_HALLAZGO);
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

        public static void LLENAOBJETO(ref OBJ_HALLAZGO objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_HALLAZGO);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        objeto.num_referencia = long.Parse(reader["num_referencia"].ToString());
                        objeto.nombre_remitente = reader["nombre_remitente"].ToString();
                        objeto.fecha_envio = DateTime.Parse(reader["fecha_envio"].ToString());
                        objeto.area = reader["area"].ToString();
                        objeto.lugar = reader["lugar"].ToString();
                        objeto.deteccion = reader["deteccion"].ToString();
                        objeto.tipo = reader["tipo"].ToString();
                        objeto.origen = reader["origen"].ToString();
                        objeto.destino = reader["destino"].ToString();
                        objeto.nombre_deteccion = reader["nombre_deteccion"].ToString();
                        objeto.cargo = reader["cargo"].ToString();
                        objeto.deteccion_hallazgo = reader["deteccion_hallazgo"].ToString();
                        objeto.doc_referencia = reader["doc_referencia"].ToString();
                        objeto.accion_inmediata = reader["accion_inmediata"].ToString();
                        objeto.estado = reader["estado"].ToString();
                        objeto.usuario = reader["usuario"].ToString();
                        objeto.empresainvolucrada = reader["empresainvolucrada"].ToString();
                        objeto.fecha = DateTime.Parse(reader["fecha"].ToString());
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

        public static void PREPARAOBJETO(ref OBJ_HALLAZGO objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.num_referencia = 0;
                objeto.nombre_remitente = " ";
                objeto.fecha_envio = DateTime.Parse("01/01/1900");
                objeto.area = " ";
                objeto.lugar = " ";
                objeto.deteccion = " ";
                objeto.tipo = " ";
                objeto.origen = " ";
                objeto.destino = " ";
                objeto.nombre_deteccion = " ";
                objeto.cargo = " ";
                objeto.deteccion_hallazgo = " ";
                objeto.doc_referencia = " ";
                objeto.accion_inmediata = " ";
                objeto.estado = " ";
                objeto.usuario = " ";
                objeto.empresainvolucrada = " ";
                objeto.fecha = DateTime.Parse("01/01/1900");
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
        // NUWCOA

        public static long getCorrelativo()
        {
            DBUtil db = new DBUtil();
            string correlativo = db.Scalar("select (ISNULL(MAX(num_referencia),0) + 1) from HALLAZGOS").ToString();
            return long.Parse(correlativo);
        }

    }
}
