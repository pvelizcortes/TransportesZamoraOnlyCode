using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_FACT_COMPRA_DET
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_COMPRA_DET { get; set; }

        public int id_compra { get; set; }
        public string nombre_archivo { get; set; }
        public string nombre_archivo_server { get; set; }
        public string extension { get; set; }
        public string url_doc { get; set; }
        public string path_local { get; set; }
        public DateTime fecha_subida { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_FACT_COMPRA_DET
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "FACT_COMPRA_DET";
        private static string nombre_llave = "ID_COMPRA_DET";
        private static string value_insert = "id_compra, nombre_archivo, nombre_archivo_server, extension, url_doc, path_local, fecha_subida";
        private static string value_insert2 = "@id_compra, @nombre_archivo, @nombre_archivo_server, @extension, @url_doc, @path_local, @fecha_subida";
        private static string value_update = "id_compra = @id_compra, " +
        "nombre_archivo = @nombre_archivo, " +
        "nombre_archivo_server = @nombre_archivo_server, " +
        "extension = @extension, " +
        "url_doc = @url_doc, " +
        "path_local = @path_local, " +
        "fecha_subida = @fecha_subida " +
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

        public static void INSERT(ref OBJ_FACT_COMPRA_DET objeto)
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
                        cmd.Parameters.AddWithValue("@id_compra", objeto.id_compra);
                        cmd.Parameters.AddWithValue("@nombre_archivo", objeto.nombre_archivo);
                        cmd.Parameters.AddWithValue("@nombre_archivo_server", objeto.nombre_archivo_server);
                        cmd.Parameters.AddWithValue("@extension", objeto.extension);
                        cmd.Parameters.AddWithValue("@url_doc", objeto.url_doc);
                        cmd.Parameters.AddWithValue("@path_local", objeto.path_local);
                        cmd.Parameters.AddWithValue("@fecha_subida", objeto.fecha_subida);
                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_COMPRA_DET = scope;

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

        public static void UPDATE(ref OBJ_FACT_COMPRA_DET objeto)
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
                        cmd.Parameters.AddWithValue("@id_compra", objeto.id_compra);
                        cmd.Parameters.AddWithValue("@nombre_archivo", objeto.nombre_archivo);
                        cmd.Parameters.AddWithValue("@nombre_archivo_server", objeto.nombre_archivo_server);
                        cmd.Parameters.AddWithValue("@extension", objeto.extension);
                        cmd.Parameters.AddWithValue("@url_doc", objeto.url_doc);
                        cmd.Parameters.AddWithValue("@path_local", objeto.path_local);
                        cmd.Parameters.AddWithValue("@fecha_subida", objeto.fecha_subida);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_COMPRA_DET);
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

        public static void DELETE(ref OBJ_FACT_COMPRA_DET objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_COMPRA_DET);
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

        public static void LLENAOBJETO(ref OBJ_FACT_COMPRA_DET objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_COMPRA_DET);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        objeto.id_compra = int.Parse(reader["id_compra"].ToString());
                        objeto.nombre_archivo = reader["nombre_archivo"].ToString();
                        objeto.nombre_archivo_server = reader["nombre_archivo_server"].ToString();
                        objeto.extension = reader["extension"].ToString();
                        objeto.url_doc = reader["url_doc"].ToString();
                        objeto.path_local = reader["path_local"].ToString();
                        objeto.fecha_subida = DateTime.Parse(reader["fecha_subida"].ToString());
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

        public static void PREPARAOBJETO(ref OBJ_FACT_COMPRA_DET objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.id_compra = 0;
                objeto.nombre_archivo = " ";
                objeto.nombre_archivo_server = " ";
                objeto.extension = " ";
                objeto.url_doc = " ";
                objeto.path_local = " ";
                objeto.fecha_subida = DateTime.Parse("01/01/1900");
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
