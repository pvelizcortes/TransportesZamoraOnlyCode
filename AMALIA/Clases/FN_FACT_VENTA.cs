using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_FACT_VENTA
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_FACT_VENTA { get; set; }

        public string num_factura { get; set; }
        public int total { get; set; }
        public string estado { get; set; }
        public int id_usuario { get; set; }
        public DateTime fecha_pago { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        public DateTime fecha_emision { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_FACT_VENTA
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "FACT_VENTA";
        private static string nombre_llave = "ID_FACT_VENTA";
        private static string value_insert = "num_factura, total, estado, id_usuario, fecha_pago, fecha_actualizacion, fecha_emision";
        private static string value_insert2 = "@num_factura, @total, @estado, @id_usuario, @fecha_pago, @fecha_actualizacion, @fecha_emision";
        private static string value_update = "num_factura = @num_factura, " +
        "total = @total, " +
        "estado = @estado, " +
        "id_usuario = @id_usuario, " +
        "fecha_pago = @fecha_pago, " +
        "fecha_actualizacion = @fecha_actualizacion, " +
        "fecha_emision = @fecha_emision " +
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

        public static void INSERT(ref OBJ_FACT_VENTA objeto)
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
                        cmd.Parameters.AddWithValue("@num_factura", objeto.num_factura);
                        cmd.Parameters.AddWithValue("@total", objeto.total);
                        cmd.Parameters.AddWithValue("@estado", objeto.estado);
                        cmd.Parameters.AddWithValue("@id_usuario", objeto.id_usuario);
                        cmd.Parameters.AddWithValue("@fecha_pago", objeto.fecha_pago);
                        cmd.Parameters.AddWithValue("@fecha_actualizacion", objeto.fecha_actualizacion);
                        cmd.Parameters.AddWithValue("@fecha_emision", objeto.fecha_emision);
                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_FACT_VENTA  = scope;

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

        public static void UPDATE(ref OBJ_FACT_VENTA objeto)
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
                        cmd.Parameters.AddWithValue("@num_factura", objeto.num_factura);
                        cmd.Parameters.AddWithValue("@total", objeto.total);
                        cmd.Parameters.AddWithValue("@estado", objeto.estado);
                        cmd.Parameters.AddWithValue("@id_usuario", objeto.id_usuario);
                        cmd.Parameters.AddWithValue("@fecha_pago", objeto.fecha_pago);
                        cmd.Parameters.AddWithValue("@fecha_actualizacion", objeto.fecha_actualizacion);
                        cmd.Parameters.AddWithValue("@fecha_emision", objeto.fecha_emision);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_FACT_VENTA );
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

        public static void DELETE(ref OBJ_FACT_VENTA objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_FACT_VENTA );
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

        public static void LLENAOBJETO(ref OBJ_FACT_VENTA objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_FACT_VENTA );

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        objeto.num_factura = reader["num_factura"].ToString();
                        objeto.total = int.Parse(reader["total"].ToString());
                        objeto.estado = reader["estado"].ToString();
                        objeto.id_usuario = int.Parse(reader["id_usuario"].ToString());
                        objeto.fecha_pago = DateTime.Parse(reader["fecha_pago"].ToString());
                        objeto.fecha_actualizacion = DateTime.Parse(reader["fecha_actualizacion"].ToString());
                        objeto.fecha_emision = DateTime.Parse(reader["fecha_emision"].ToString());
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

        public static void LLENACONFACTURA(ref OBJ_FACT_VENTA objeto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connex))
                {
                    conn.Open();
                    string sql = @"SELECT * from " + nombre_tabla + " where num_factura = @num_factura";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    // CAMBIAR AQUI //
                    // **************************************** **************************************** **************************************** //   
                    cmd.Parameters.AddWithValue("@num_factura", objeto.num_factura);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        objeto.ID_FACT_VENTA = int.Parse(reader["ID_FACT_VENTA"].ToString());
                        objeto.total = int.Parse(reader["total"].ToString());
                        objeto.estado = reader["estado"].ToString();
                        objeto.id_usuario = int.Parse(reader["id_usuario"].ToString());
                        objeto.fecha_pago = DateTime.Parse(reader["fecha_pago"].ToString());
                        objeto.fecha_actualizacion = DateTime.Parse(reader["fecha_actualizacion"].ToString());
                        objeto.fecha_emision = DateTime.Parse(reader["fecha_emision"].ToString());
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

        public static void PREPARAOBJETO(ref OBJ_FACT_VENTA objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.num_factura = " ";
                objeto.total = 0;
                objeto.estado = " ";
                objeto.id_usuario = 0;
                objeto.fecha_pago = DateTime.Parse("01/01/1900");
                objeto.fecha_actualizacion = DateTime.Parse("01/01/1900");
                objeto.fecha_emision = DateTime.Parse("01/01/1900");
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
