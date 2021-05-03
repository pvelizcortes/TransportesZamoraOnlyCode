using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_COMB_LOG
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_LOG { get; set; }

        public int id_usuario { get; set; }
        public DateTime fecha { get; set; }
        public string guia { get; set; }
        public float cont_surtidor_inicial { get; set; }
        public int km_camion { get; set; }
        public string nombre_conductor { get; set; }
        public string patente_camion { get; set; }
        public int litros { get; set; }
        public float cont_surtidor_final { get; set; }
        public int stock_estanque { get; set; }
        public int precio { get; set; }
        public int total { get; set; }
        public string factura_asociada { get; set; }
        public string observacion { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_COMB_LOG
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "COMB_LOG";
        private static string nombre_llave = "ID_LOG";
        private static string value_insert = "id_usuario, fecha, guia, cont_surtidor_inicial, km_camion, nombre_conductor, patente_camion, litros, cont_surtidor_final, stock_estanque, precio, total, factura_asociada, observacion";
        private static string value_insert2 = "@id_usuario, @fecha, @guia, @cont_surtidor_inicial, @km_camion, @nombre_conductor, @patente_camion, @litros, @cont_surtidor_final, @stock_estanque, @precio, @total, @factura_asociada, @observacion";
        private static string value_update = "id_usuario = @id_usuario, " +
        "fecha = @fecha, " +
        "guia = @guia, " +
        "cont_surtidor_inicial = @cont_surtidor_inicial, " +
        "km_camion = @km_camion, " +
        "nombre_conductor = @nombre_conductor, " +
        "patente_camion = @patente_camion, " +
        "litros = @litros, " +
        "cont_surtidor_final = @cont_surtidor_final, " +
        "stock_estanque = @stock_estanque, " +
        "precio = @precio, " +
        "total = @total, " +
        "factura_asociada = @factura_asociada, " +
        "observacion = @observacion " +
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

        public static void INSERT(ref OBJ_COMB_LOG objeto)
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
                        cmd.Parameters.AddWithValue("@id_usuario", objeto.id_usuario);
                        cmd.Parameters.AddWithValue("@fecha", objeto.fecha);
                        cmd.Parameters.AddWithValue("@guia", objeto.guia);
                        cmd.Parameters.AddWithValue("@cont_surtidor_inicial", objeto.cont_surtidor_inicial);
                        cmd.Parameters.AddWithValue("@km_camion", objeto.km_camion);
                        cmd.Parameters.AddWithValue("@nombre_conductor", objeto.nombre_conductor);
                        cmd.Parameters.AddWithValue("@patente_camion", objeto.patente_camion);
                        cmd.Parameters.AddWithValue("@litros", objeto.litros);
                        cmd.Parameters.AddWithValue("@cont_surtidor_final", objeto.cont_surtidor_final);
                        cmd.Parameters.AddWithValue("@stock_estanque", objeto.stock_estanque);
                        cmd.Parameters.AddWithValue("@precio", objeto.precio);
                        cmd.Parameters.AddWithValue("@total", objeto.total);
                        cmd.Parameters.AddWithValue("@factura_asociada", objeto.factura_asociada);
                        cmd.Parameters.AddWithValue("@observacion", objeto.observacion);
                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_LOG = scope;

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

        public static void UPDATE(ref OBJ_COMB_LOG objeto)
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
                        cmd.Parameters.AddWithValue("@id_usuario", objeto.id_usuario);
                        cmd.Parameters.AddWithValue("@fecha", objeto.fecha);
                        cmd.Parameters.AddWithValue("@guia", objeto.guia);
                        cmd.Parameters.AddWithValue("@cont_surtidor_inicial", objeto.cont_surtidor_inicial);
                        cmd.Parameters.AddWithValue("@km_camion", objeto.km_camion);
                        cmd.Parameters.AddWithValue("@nombre_conductor", objeto.nombre_conductor);
                        cmd.Parameters.AddWithValue("@patente_camion", objeto.patente_camion);
                        cmd.Parameters.AddWithValue("@litros", objeto.litros);
                        cmd.Parameters.AddWithValue("@cont_surtidor_final", objeto.cont_surtidor_final);
                        cmd.Parameters.AddWithValue("@stock_estanque", objeto.stock_estanque);
                        cmd.Parameters.AddWithValue("@precio", objeto.precio);
                        cmd.Parameters.AddWithValue("@total", objeto.total);
                        cmd.Parameters.AddWithValue("@factura_asociada", objeto.factura_asociada);
                        cmd.Parameters.AddWithValue("@observacion", objeto.observacion);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_LOG);
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

        public static void DELETE(ref OBJ_COMB_LOG objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_LOG);
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

        public static void LLENAOBJETO(ref OBJ_COMB_LOG objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_LOG);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        //  nombre_cliente, razon_social, correo, direccion, telefono, nombre_encargado, activo 
                        objeto.id_usuario = int.Parse(reader["id_usuario"].ToString());
                        objeto.fecha = DateTime.Parse(reader["fecha"].ToString());
                        objeto.guia = reader["guia"].ToString();
                        objeto.cont_surtidor_inicial = float.Parse(reader["cont_surtidor_inicial"].ToString());
                        objeto.km_camion = int.Parse(reader["km_camion"].ToString());
                        objeto.nombre_conductor = reader["nombre_conductor"].ToString();
                        objeto.patente_camion = reader["patente_camion"].ToString();
                        objeto.litros = int.Parse(reader["litros"].ToString());
                        objeto.cont_surtidor_final = float.Parse(reader["cont_surtidor_final"].ToString());
                        objeto.stock_estanque = int.Parse(reader["stock_estanque"].ToString());
                        objeto.precio = int.Parse(reader["precio"].ToString());
                        objeto.total = int.Parse(reader["total"].ToString());
                        objeto.factura_asociada = reader["factura_asociada"].ToString();
                        objeto.observacion = reader["observacion"].ToString();
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

        public static void PREPARAOBJETO(ref OBJ_COMB_LOG objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  

                objeto.id_usuario = 0;
                objeto.fecha = DateTime.Parse("01/01/1900");
                objeto.guia = " ";
                objeto.cont_surtidor_inicial = 0;
                objeto.km_camion = 0;
                objeto.nombre_conductor = " ";
                objeto.patente_camion = " ";
                objeto.litros = 0;
                objeto.cont_surtidor_final = 0;
                objeto.stock_estanque = 0;
                objeto.precio = 0;
                objeto.total = 0;
                objeto.factura_asociada = " ";
                objeto.observacion = " ";
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
