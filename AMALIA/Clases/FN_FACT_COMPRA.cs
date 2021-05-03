using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_FACT_COMPRA
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_COMPRA { get; set; }

        public int id_camion { get; set; }
        public string tipo_documento { get; set; }
        public string num_documento { get; set; }
        public int proveedor { get; set; }
        public string detalle { get; set; }
        public int total { get; set; }
        public string estado { get; set; }
        public DateTime fecha_compra { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int id_usuario { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_FACT_COMPRA
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "FACT_COMPRA";
        private static string nombre_llave = "ID_COMPRA";
        private static string value_insert = "id_camion, tipo_documento, num_documento, proveedor, detalle, total, estado, fecha_compra, fecha_creacion, id_usuario";
        private static string value_insert2 = "@id_camion, @tipo_documento, @num_documento, @proveedor, @detalle, @total, @estado, @fecha_compra, @fecha_creacion, @id_usuario";
        private static string value_update = "id_camion = @id_camion, " +
        "tipo_documento = @tipo_documento, " +
        "num_documento = @num_documento, " +
        "proveedor = @proveedor, " +
        "detalle = @detalle, " +
        "total = @total, " +
        "estado = @estado, " +
        "fecha_compra = @fecha_compra, " +
        "fecha_creacion = @fecha_creacion, " +
        "id_usuario = @id_usuario " +
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

        public static void INSERT(ref OBJ_FACT_COMPRA objeto)
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
                        cmd.Parameters.AddWithValue("@id_camion", objeto.id_camion);
                        cmd.Parameters.AddWithValue("@tipo_documento", objeto.tipo_documento);
                        cmd.Parameters.AddWithValue("@num_documento", objeto.num_documento);
                        cmd.Parameters.AddWithValue("@proveedor", objeto.proveedor);
                        cmd.Parameters.AddWithValue("@detalle", objeto.detalle);
                        cmd.Parameters.AddWithValue("@total", objeto.total);
                        cmd.Parameters.AddWithValue("@estado", objeto.estado);
                        cmd.Parameters.AddWithValue("@fecha_compra", objeto.fecha_compra);
                        cmd.Parameters.AddWithValue("@fecha_creacion", objeto.fecha_creacion);
                        cmd.Parameters.AddWithValue("@id_usuario", objeto.id_usuario);
                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_COMPRA = scope;

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

        public static void UPDATE(ref OBJ_FACT_COMPRA objeto)
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
                        cmd.Parameters.AddWithValue("@id_camion", objeto.id_camion);
                        cmd.Parameters.AddWithValue("@tipo_documento", objeto.tipo_documento);
                        cmd.Parameters.AddWithValue("@num_documento", objeto.num_documento);
                        cmd.Parameters.AddWithValue("@proveedor", objeto.proveedor);
                        cmd.Parameters.AddWithValue("@detalle", objeto.detalle);
                        cmd.Parameters.AddWithValue("@total", objeto.total);
                        cmd.Parameters.AddWithValue("@estado", objeto.estado);
                        cmd.Parameters.AddWithValue("@fecha_compra", objeto.fecha_compra);
                        cmd.Parameters.AddWithValue("@fecha_creacion", objeto.fecha_creacion);
                        cmd.Parameters.AddWithValue("@id_usuario", objeto.id_usuario);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_COMPRA);
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

        public static void DELETE(ref OBJ_FACT_COMPRA objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_COMPRA);
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

        public static void LLENAOBJETO(ref OBJ_FACT_COMPRA objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_COMPRA);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        objeto.id_camion = int.Parse(reader["id_camion"].ToString());
                        objeto.tipo_documento = reader["tipo_documento"].ToString();
                        objeto.num_documento = reader["num_documento"].ToString();
                        objeto.proveedor = int.Parse(reader["proveedor"].ToString());
                        objeto.detalle = reader["detalle"].ToString();
                        objeto.total = int.Parse(reader["total"].ToString());
                        objeto.estado = reader["estado"].ToString();
                        objeto.fecha_compra = DateTime.Parse(reader["fecha_compra"].ToString());
                        objeto.fecha_creacion = DateTime.Parse(reader["fecha_creacion"].ToString());
                        objeto.id_usuario = int.Parse(reader["id_usuario"].ToString());
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

        public static void PREPARAOBJETO(ref OBJ_FACT_COMPRA objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.id_camion = 0;
                objeto.tipo_documento = " ";
                objeto.num_documento = " ";
                objeto.proveedor = 0;
                objeto.detalle = " ";
                objeto.total = 0;
                objeto.estado = " ";
                objeto.fecha_compra = DateTime.Parse("01/01/1900");
                objeto.fecha_creacion = DateTime.Parse("01/01/1900");
                objeto.id_usuario = 0;
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
