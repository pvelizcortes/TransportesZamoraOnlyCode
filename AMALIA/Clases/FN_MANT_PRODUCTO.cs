using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_MANT_PRODUCTO
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_PRODUCTO { get; set; }

        public int id_marca { get; set; }
        public string cod_producto { get; set; }
        public int cat_producto { get; set; }
        public string sku { get; set; }
        public string nom_producto { get; set; }

        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_MANT_PRODUCTO
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "mant_producto";
        private static string nombre_llave = "id_producto";
        private static string value_insert = " id_marca, cod_producto, cat_producto, sku, nom_producto ";
        private static string value_insert2 = " @id_marca, @cod_producto, @cat_producto, @sku, @nom_producto ";
        private static string value_update =
        " id_marca = @id_marca, " +
        " cod_producto = @cod_producto, " +
        " cat_producto = @cat_producto, " +
        " sku = @sku, " +
        " nom_producto = @nom_producto " +// <----------- EL ULTIMO SIN COMA PLS <3
        " WHERE " + nombre_llave + " = @" + nombre_llave + " ";
        // **************************************** **************************************** **************************************** //


        public static DataTable LLENADT(string sql_where = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connex))
            {
                conn.Open();
                string sql = @"SELECT *  from V_REPUESTOS "  + sql_where;
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter ap = new SqlDataAdapter(cmd);
                ap.Fill(dt);
            }
            return dt;
        }

        public static void INSERT(ref OBJ_MANT_PRODUCTO objeto)
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
                        //  id_marca, cod_producto, cat_producto, sku, nom_producto
                        cmd.Parameters.AddWithValue("@id_marca", objeto.id_marca);
                        cmd.Parameters.AddWithValue("@cod_producto", objeto.cod_producto);
                        cmd.Parameters.AddWithValue("@cat_producto", objeto.cat_producto);
                        cmd.Parameters.AddWithValue("@sku", objeto.sku);
                        cmd.Parameters.AddWithValue("@nom_producto", objeto.nom_producto);
                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_PRODUCTO = scope;

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

        public static void UPDATE(ref OBJ_MANT_PRODUCTO objeto)
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
                        cmd.Parameters.AddWithValue("@id_marca", objeto.id_marca);
                        cmd.Parameters.AddWithValue("@cod_producto", objeto.cod_producto);
                        cmd.Parameters.AddWithValue("@cat_producto", objeto.cat_producto);
                        cmd.Parameters.AddWithValue("@sku", objeto.sku);
                        cmd.Parameters.AddWithValue("@nom_producto", objeto.nom_producto);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_PRODUCTO);
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

        public static void DELETE(ref OBJ_MANT_PRODUCTO objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_PRODUCTO);
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

        public static void LLENAOBJETO(ref OBJ_MANT_PRODUCTO objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_PRODUCTO);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        //  id_marca, cod_producto, cat_producto, sku, nom_producto
                        objeto.id_marca = int.Parse(reader["id_marca"].ToString());
                        objeto.cod_producto = reader["cod_producto"].ToString();
                        objeto.cat_producto = int.Parse(reader["cat_producto"].ToString());
                        objeto.sku = reader["sku"].ToString();
                        objeto.nom_producto = reader["nom_producto"].ToString();

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

        public static void PREPARAOBJETO(ref OBJ_MANT_PRODUCTO objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.id_marca = 0;
                objeto.cod_producto = "000";
                objeto.cat_producto = 0;
                objeto.sku = "N/A";
                objeto.nom_producto = "N/A";
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
