using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_CLIENTE
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_CLIENTE { get; set; }

        public string nombre_cliente { get; set; }
        public string razon_social { get; set; }
        public string correo { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string telefono2 { get; set; }
        public string nombre_encargado { get; set; }
        public string nombre_encargado2 { get; set; }
        public string activo { get; set; }
        public string rut_cliente { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_CLIENTE
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "cliente";
        private static string nombre_llave = "ID_CLIENTE";
        private static string value_insert = " nombre_cliente, razon_social, correo, direccion, telefono, nombre_encargado, nombre_encargado2, telefono2, activo, rut_cliente ";
        private static string value_insert2 = " @nombre_cliente, @razon_social, @correo, @direccion, @telefono, @nombre_encargado, @nombre_encargado2, @telefono2, @activo, @rut_cliente ";
        private static string value_update =
        " nombre_cliente = @nombre_cliente, " +
        " razon_social = @razon_social, " +
        " correo = @correo, " +
        " telefono = @telefono, " +
        " telefono2 = @telefono2, " +
        " direccion = @direccion, " +
        " nombre_encargado = @nombre_encargado, " +
        " nombre_encargado2 = @nombre_encargado2, " +
        " rut_cliente = @rut_cliente, " +
        " activo = @activo " + // <----------- EL ULTIMO SIN COMA PLS <3

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

        public static void INSERT(ref OBJ_CLIENTE objeto)
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
                        cmd.Parameters.AddWithValue("@nombre_cliente", objeto.nombre_cliente);
                        cmd.Parameters.AddWithValue("@razon_social", objeto.razon_social);
                        cmd.Parameters.AddWithValue("@correo", objeto.correo);
                        cmd.Parameters.AddWithValue("@telefono", objeto.telefono);
                        cmd.Parameters.AddWithValue("@telefono2", objeto.telefono2);
                        cmd.Parameters.AddWithValue("@direccion", objeto.direccion);
                        cmd.Parameters.AddWithValue("@nombre_encargado", objeto.nombre_encargado);
                        cmd.Parameters.AddWithValue("@nombre_encargado2", objeto.nombre_encargado2);
                        cmd.Parameters.AddWithValue("@activo", objeto.activo);
                        cmd.Parameters.AddWithValue("@rut_cliente", objeto.rut_cliente);
                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_CLIENTE = scope;

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

        public static void UPDATE(ref OBJ_CLIENTE objeto)
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
                        cmd.Parameters.AddWithValue("@nombre_cliente", objeto.nombre_cliente);
                        cmd.Parameters.AddWithValue("@razon_social", objeto.razon_social);
                        cmd.Parameters.AddWithValue("@correo", objeto.correo);
                        cmd.Parameters.AddWithValue("@telefono", objeto.telefono);
                        cmd.Parameters.AddWithValue("@telefono2", objeto.telefono2);
                        cmd.Parameters.AddWithValue("@direccion", objeto.direccion);
                        cmd.Parameters.AddWithValue("@nombre_encargado", objeto.nombre_encargado);
                        cmd.Parameters.AddWithValue("@nombre_encargado2", objeto.nombre_encargado2);
                        cmd.Parameters.AddWithValue("@activo", objeto.activo);
                        cmd.Parameters.AddWithValue("@rut_cliente", objeto.rut_cliente);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_CLIENTE);
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

        public static void DELETE(ref OBJ_CLIENTE objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_CLIENTE);
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

        public static void LLENAOBJETO(ref OBJ_CLIENTE objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_CLIENTE);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        //  nombre_cliente, razon_social, correo, direccion, telefono, nombre_encargado, activo 
                        objeto.nombre_cliente = reader["nombre_cliente"].ToString();
                        objeto.razon_social = reader["razon_social"].ToString();
                        objeto.correo = reader["correo"].ToString();
                        objeto.telefono = reader["telefono"].ToString();
                        objeto.telefono2 = reader["telefono2"].ToString();
                        objeto.direccion = reader["direccion"].ToString();
                        objeto.nombre_encargado = reader["nombre_encargado"].ToString();
                        objeto.nombre_encargado2 = reader["nombre_encargado2"].ToString();
                        objeto.activo = reader["activo"].ToString();
                        objeto.rut_cliente = reader["rut_cliente"].ToString();
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

        public static void PREPARAOBJETO(ref OBJ_CLIENTE objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  

                objeto.nombre_cliente = "no";
                objeto.razon_social = "no";
                objeto.correo = "no";
                objeto.telefono = "no";
                objeto.telefono2 = "no";
                objeto.direccion = "no";
                objeto.nombre_encargado = "no";
                objeto.nombre_encargado2 = "no";
                objeto.activo = "ACTIVO";
                objeto.rut_cliente = "no";
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
