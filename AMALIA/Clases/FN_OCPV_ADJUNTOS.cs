using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_OCPV_ADJUNTOS
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_DOC { get; set; }

        public int id_oc { get; set; }
        public string nom_archivo { get; set; }
        public string nom_real { get; set; }
        public DateTime fecha { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_OCPV_ADJUNTOS
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        private static string nombre_tabla = "OCPV_ADJUNTOS";
        private static string nombre_llave = "ID_DOC";
        private static string value_insert = "id_oc, nom_archivo, nom_real, fecha";
        private static string value_insert2 = "@id_oc, @nom_archivo, @nom_real, @fecha";
        private static string value_update = "id_oc = @id_oc, " +
        "nom_archivo = @nom_archivo, " +
        "nom_real = @nom_real, " +
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

        public static void INSERT(ref OBJ_OCPV_ADJUNTOS objeto)
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
                        //  nombre_cliente, razon_social, correo, direccion, telefono, nombre_encargado, activo 
                        cmd.Parameters.AddWithValue("@id_oc", objeto.id_oc);
                        cmd.Parameters.AddWithValue("@nom_archivo", objeto.nom_archivo);
                        cmd.Parameters.AddWithValue("@nom_real", objeto.nom_real);
                        cmd.Parameters.AddWithValue("@fecha", objeto.fecha);
                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_DOC = scope;

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

        public static void UPDATE(ref OBJ_OCPV_ADJUNTOS objeto)
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
                        cmd.Parameters.AddWithValue("@id_oc", objeto.id_oc);
                        cmd.Parameters.AddWithValue("@nom_archivo", objeto.nom_archivo);
                        cmd.Parameters.AddWithValue("@nom_real", objeto.nom_real);
                        cmd.Parameters.AddWithValue("@fecha", objeto.fecha);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_DOC);
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

        public static void DELETE(ref OBJ_OCPV_ADJUNTOS objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_DOC);
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

        public static void LLENAOBJETO(ref OBJ_OCPV_ADJUNTOS objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_DOC);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        //  nombre_cliente, razon_social, correo, direccion, telefono, nombre_encargado, activo 
                        objeto.id_oc = int.Parse(reader["id_oc"].ToString());
                        objeto.nom_archivo = reader["nom_archivo"].ToString();
                        objeto.nom_real = reader["nom_real"].ToString();
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

        public static void PREPARAOBJETO(ref OBJ_OCPV_ADJUNTOS objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.id_oc = 0;
                objeto.nom_archivo = " ";
                objeto.nom_real = " ";
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
