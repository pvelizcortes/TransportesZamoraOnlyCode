using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_CARGA_COMBUSTIBLE
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_CARGA { get; set; }

        public int id_gt { get; set; }
        public string estacion { get; set; }
        public DateTime fecha { get; set; }
        public string guia { get; set; }
        public string rollo { get; set; }
        public int kilometraje { get; set; }
        public double litros_cargados { get; set; }
        public int precio { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_CARGA_COMBUSTIBLE
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "carga_combustible";
        private static string nombre_llave = "id_carga";
        private static string value_insert = " id_gt, estacion, fecha, guia, rollo, kilometraje, litros_cargados, precio ";
        private static string value_insert2 = " @id_gt, @estacion, @fecha, @guia, @rollo, @kilometraje, @litros_cargados, @precio ";
        private static string value_update =
        " id_gt = @id_gt, " +
        " estacion = @estacion, " +
        " fecha = @fecha, " +
        " guia = @guia, " +
        " rollo = @rollo, " +
        " kilometraje = @kilometraje, " +
        " litros_cargados = @litros_cargados, " +
        " precio = @precio " + // <----------- EL ULTIMO SIN COMA PLS <3

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


        public static void INSERT(ref OBJ_CARGA_COMBUSTIBLE objeto)
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
                        // id_gt, estacion, fecha, guia, rollo, kilometraje, litros_cargados, precio ";
                        cmd.Parameters.AddWithValue("@id_gt", objeto.id_gt);
                        cmd.Parameters.AddWithValue("@estacion", objeto.estacion);
                        cmd.Parameters.AddWithValue("@fecha", objeto.fecha);
                        cmd.Parameters.AddWithValue("@guia", objeto.guia);
                        cmd.Parameters.AddWithValue("@rollo", objeto.rollo);
                        cmd.Parameters.AddWithValue("@kilometraje", objeto.kilometraje);
                        cmd.Parameters.AddWithValue("@litros_cargados", objeto.litros_cargados);
                        cmd.Parameters.AddWithValue("@precio", objeto.precio);
                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_CARGA = scope;

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

        public static void UPDATE(ref OBJ_CARGA_COMBUSTIBLE objeto)
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
                        cmd.Parameters.AddWithValue("@id_gt", objeto.id_gt);
                        cmd.Parameters.AddWithValue("@estacion", objeto.estacion);
                        cmd.Parameters.AddWithValue("@fecha", objeto.fecha);
                        cmd.Parameters.AddWithValue("@guia", objeto.guia);
                        cmd.Parameters.AddWithValue("@rollo", objeto.rollo);
                        cmd.Parameters.AddWithValue("@kilometraje", objeto.kilometraje);
                        cmd.Parameters.AddWithValue("@litros_cargados", objeto.litros_cargados);
                        cmd.Parameters.AddWithValue("@precio", objeto.precio);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_CARGA);
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

        public static void DELETE(ref OBJ_CARGA_COMBUSTIBLE objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_CARGA);
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

        public static void LLENAOBJETO(ref OBJ_CARGA_COMBUSTIBLE objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_CARGA);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // id_perfil, nombre_completo, correo, telefono, direccion, usuario, pass, activo
                        objeto.id_gt = int.Parse(reader["id_gt"].ToString());
                        objeto.estacion = reader["estacion"].ToString();
                        objeto.fecha = DateTime.Parse(reader["fecha"].ToString());
                        objeto.guia = reader["guia"].ToString();
                        objeto.rollo = reader["rollo"].ToString();
                        objeto.kilometraje = int.Parse(reader["kilometraje"].ToString());
                        objeto.litros_cargados = double.Parse(reader["litros_cargados"].ToString());
                        objeto.precio = int.Parse(reader["precio"].ToString());
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

        public static void PREPARAOBJETO(ref OBJ_CARGA_COMBUSTIBLE objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.id_gt = 0;
                objeto.estacion = "no";
                objeto.fecha = DateTime.Now;
                objeto.guia = "no";
                objeto.rollo = "no";
                objeto.kilometraje = 0;
                objeto.litros_cargados = 0;
                objeto.precio = 0;
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
        ///////////////////////////// PROPIOS DEL MODULO / CLASE //////////////////////////////     
        public static DataTable LLENADTVISTA(string sql_where = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connex))
            {
                conn.Open();
                string sql = @"SELECT *  from v_Combustibles " + sql_where;
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter ap = new SqlDataAdapter(cmd);
                ap.Fill(dt);
            }
            return dt;
        }

        public static void DELETEWITHGT(ref OBJ_CARGA_COMBUSTIBLE objeto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connex))
                {
                    conn.Open();
                    string sql = @"DELETE FROM " + nombre_tabla + " WHERE ID_GT = @ID_GT";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // CAMBIAR AQUI //
                        // **************************************** **************************************** **************************************** //   
                        cmd.Parameters.AddWithValue("@ID_GT", objeto.id_gt);
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

    }
}
