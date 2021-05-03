using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_DEPOSITO_DETALLE
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_DETALLE_DEPOSITO { get; set; }

        public int id_deposito { get; set; }
        public int num_viaje { get; set; }
        public DateTime fecha_viaje { get; set; }
        public int id_conductor { get; set; }
        public string nombre_conductor { get; set; }
        public string tipo { get; set; }
        public string RP { get; set; }
        public int valor { get; set; }
        public int monto_depositado { get; set; }
        public string comentario { get; set; }
        public string estado { get; set; }
        public string usuario_admin { get; set; }
        public string comentario_admin { get; set; }
        public DateTime fecha_admin { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_DEPOSITO_DETALLE
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "DEPOSITO_DETALLE";
        private static string nombre_llave = "ID_DETALLE_DEPOSITO";
        private static string value_insert = "id_deposito, num_viaje, fecha_viaje, id_conductor, nombre_conductor, tipo, RP, valor, monto_depositado, comentario, estado, usuario_admin, comentario_admin, fecha_admin";
        private static string value_insert2 = "@id_deposito, @num_viaje, @fecha_viaje, @id_conductor, @nombre_conductor, @tipo, @RP, @valor, @monto_depositado, @comentario, @estado, @usuario_admin, @comentario_admin, @fecha_admin";
        private static string value_update = "id_deposito = @id_deposito, " +
        "num_viaje = @num_viaje, " +
        "fecha_viaje = @fecha_viaje, " +
        "id_conductor = @id_conductor, " +
        "nombre_conductor = @nombre_conductor, " +
        "tipo = @tipo, " +
        "RP = @RP, " +
        "valor = @valor, " +
        "monto_depositado = @monto_depositado, " +
        "comentario = @comentario, " +
        "estado = @estado, " +
        "usuario_admin = @usuario_admin, " +
        "comentario_admin = @comentario_admin, " +
        "fecha_admin = @fecha_admin " +
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

        public static DataTable LLENADTVISTA(string sql_where = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connex))
            {
                conn.Open();
                string sql = @"SELECT *  from V_DEPOSITOS " + sql_where;
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter ap = new SqlDataAdapter(cmd);
                ap.Fill(dt);
            }
            return dt;
        }

        public static void INSERT(ref OBJ_DEPOSITO_DETALLE objeto)
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
                        cmd.Parameters.AddWithValue("@id_deposito", objeto.id_deposito);
                        cmd.Parameters.AddWithValue("@num_viaje", objeto.num_viaje);
                        cmd.Parameters.AddWithValue("@fecha_viaje", objeto.fecha_viaje);
                        cmd.Parameters.AddWithValue("@id_conductor", objeto.id_conductor);
                        cmd.Parameters.AddWithValue("@nombre_conductor", objeto.nombre_conductor);
                        cmd.Parameters.AddWithValue("@tipo", objeto.tipo);
                        cmd.Parameters.AddWithValue("@RP", objeto.RP);
                        cmd.Parameters.AddWithValue("@valor", objeto.valor);
                        cmd.Parameters.AddWithValue("@monto_depositado", objeto.monto_depositado);
                        cmd.Parameters.AddWithValue("@comentario", objeto.comentario);
                        cmd.Parameters.AddWithValue("@estado", objeto.estado);
                        cmd.Parameters.AddWithValue("@usuario_admin", objeto.usuario_admin);
                        cmd.Parameters.AddWithValue("@comentario_admin", objeto.comentario_admin);
                        cmd.Parameters.AddWithValue("@fecha_admin", objeto.fecha_admin);
                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_DETALLE_DEPOSITO = scope;

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

        public static void UPDATE(ref OBJ_DEPOSITO_DETALLE objeto)
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
                        cmd.Parameters.AddWithValue("@id_deposito", objeto.id_deposito);
                        cmd.Parameters.AddWithValue("@num_viaje", objeto.num_viaje);
                        cmd.Parameters.AddWithValue("@fecha_viaje", objeto.fecha_viaje);
                        cmd.Parameters.AddWithValue("@id_conductor", objeto.id_conductor);
                        cmd.Parameters.AddWithValue("@nombre_conductor", objeto.nombre_conductor);
                        cmd.Parameters.AddWithValue("@tipo", objeto.tipo);
                        cmd.Parameters.AddWithValue("@RP", objeto.RP);
                        cmd.Parameters.AddWithValue("@valor", objeto.valor);
                        cmd.Parameters.AddWithValue("@monto_depositado", objeto.monto_depositado);
                        cmd.Parameters.AddWithValue("@comentario", objeto.comentario);
                        cmd.Parameters.AddWithValue("@estado", objeto.estado);
                        cmd.Parameters.AddWithValue("@usuario_admin", objeto.usuario_admin);
                        cmd.Parameters.AddWithValue("@comentario_admin", objeto.comentario_admin);
                        cmd.Parameters.AddWithValue("@fecha_admin", objeto.fecha_admin);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_DETALLE_DEPOSITO);
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

        public static void DELETE(ref OBJ_DEPOSITO_DETALLE objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_DETALLE_DEPOSITO);
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

        public static void LLENAOBJETO(ref OBJ_DEPOSITO_DETALLE objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_DETALLE_DEPOSITO);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        objeto.id_deposito = int.Parse(reader["id_deposito"].ToString());
                        objeto.num_viaje = int.Parse(reader["num_viaje"].ToString());
                        objeto.fecha_viaje = DateTime.Parse(reader["fecha_viaje"].ToString());
                        objeto.id_conductor = int.Parse(reader["id_conductor"].ToString());
                        objeto.nombre_conductor = reader["nombre_conductor"].ToString();
                        objeto.tipo = reader["tipo"].ToString();
                        objeto.RP = reader["RP"].ToString();
                        objeto.valor = int.Parse(reader["valor"].ToString());
                        objeto.monto_depositado = int.Parse(reader["monto_depositado"].ToString());
                        objeto.comentario = reader["comentario"].ToString();
                        objeto.estado = reader["estado"].ToString();
                        objeto.usuario_admin = reader["usuario_admin"].ToString();
                        objeto.comentario_admin = reader["comentario_admin"].ToString();
                        objeto.fecha_admin = DateTime.Parse(reader["fecha_admin"].ToString());
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

        public static void PREPARAOBJETO(ref OBJ_DEPOSITO_DETALLE objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.id_deposito = 0;
                objeto.num_viaje = 0;
                objeto.fecha_viaje = DateTime.Parse("01/01/1900");
                objeto.id_conductor = 0;
                objeto.nombre_conductor = " ";
                objeto.tipo = " ";
                objeto.RP = " ";
                objeto.valor = 0;
                objeto.monto_depositado = 0;
                objeto.comentario = " ";
                objeto.estado = " ";
                objeto.usuario_admin = " ";
                objeto.comentario_admin = " ";
                objeto.fecha_admin = DateTime.Parse("01/01/1900");
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
