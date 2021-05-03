using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_REPEX
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_OT_REPEX { get; set; }

        public int id_ot { get; set; }
        public int id_proveedor { get; set; }
        public string nom_proveedor { get; set; }
        public string num_factura { get; set; }
        public int valor { get; set; }
        public DateTime fecha { get; set; }

        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_REPEX
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "mant_ot_repex";
        private static string nombre_llave = "ID_OT_REPEX";
        private static string value_insert = " id_ot, id_proveedor, nom_proveedor, num_factura, valor, fecha ";
        private static string value_insert2 = " @id_ot, @id_proveedor, @nom_proveedor, @num_factura, @valor, @fecha ";
        private static string value_update =
        " id_ot = @id_ot, " +
        " id_proveedor = @id_proveedor, " +
        " nom_proveedor = @nom_proveedor, " +
        " num_factura = @num_factura, " +
        " valor = @valor, " +
        " fecha = @fecha " +// <----------- EL ULTIMO SIN COMA PLS <3
        " WHERE " + nombre_llave + " = @" + nombre_llave + " ";
        // **************************************** **************************************** **************************************** //


        public static DataTable LLENADT(string sql_where = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connex))
            {
                conn.Open();
                string sql = @"SELECT *  from MANT_OT_REPEX " + sql_where;
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter ap = new SqlDataAdapter(cmd);
                ap.Fill(dt);
            }
            return dt;
        }

        public static void INSERT(ref OBJ_REPEX objeto)
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
                        //  id_ot, id_proveedor, nom_proveedor, num_factura, valor, fecha 
                        cmd.Parameters.AddWithValue("@id_ot", objeto.id_ot);
                        cmd.Parameters.AddWithValue("@id_proveedor", objeto.id_proveedor);
                        cmd.Parameters.AddWithValue("@nom_proveedor", objeto.nom_proveedor);
                        cmd.Parameters.AddWithValue("@num_factura", objeto.num_factura);
                        cmd.Parameters.AddWithValue("@valor", objeto.valor);
                        cmd.Parameters.AddWithValue("@fecha", objeto.fecha);
                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_OT_REPEX = scope;

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

        public static void UPDATE(ref OBJ_REPEX objeto)
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
                        cmd.Parameters.AddWithValue("@id_ot", objeto.id_ot);
                        cmd.Parameters.AddWithValue("@id_proveedor", objeto.id_proveedor);
                        cmd.Parameters.AddWithValue("@nom_proveedor", objeto.nom_proveedor);
                        cmd.Parameters.AddWithValue("@num_factura", objeto.num_factura);
                        cmd.Parameters.AddWithValue("@valor", objeto.valor);
                        cmd.Parameters.AddWithValue("@fecha", objeto.fecha);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_OT_REPEX);
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

        public static void DELETE(ref OBJ_REPEX objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_OT_REPEX);
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

        public static void LLENAOBJETO(ref OBJ_REPEX objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_OT_REPEX);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // id_ot, id_proveedor, nom_proveedor, num_factura, valor, fecha 
                        objeto.id_ot = int.Parse(reader["id_ot"].ToString());
                        objeto.id_proveedor = int.Parse(reader["id_proveedor"].ToString());
                        objeto.nom_proveedor = reader["nom_proveedor"].ToString();
                        objeto.num_factura = reader["num_factura"].ToString();
                        objeto.valor = int.Parse(reader["valor"].ToString());
                        objeto.fecha = Convert.ToDateTime((reader["fecha"].ToString()));

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

        public static void PREPARAOBJETO(ref OBJ_REPEX objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.id_ot = -1;
                objeto.id_proveedor = -1;
                objeto.nom_proveedor = "N/A";
                objeto.num_factura = "0";
                objeto.valor = 0;
                objeto.fecha = DateTime.Now;
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
