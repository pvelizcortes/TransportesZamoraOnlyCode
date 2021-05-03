using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_GD_NEUMATICOS
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_NEUMATICO { get; set; }

        public int num_interno { get; set; }
        public string marca { get; set; }
        public int posicion { get; set; }
        public string patente { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public int km_ingreso { get; set; }
        public string presion { get; set; }
        public string prof_izq { get; set; }
        public string prof_der { get; set; }
        public string lugar_neumatico { get; set; }
        public string motivo_cambio { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_GD_NEUMATICOS
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "GD_NEUMATICOS";
        private static string nombre_llave = "ID_NEUMATICO";
        private static string value_insert = "num_interno, marca, posicion, patente, fecha_ingreso, km_ingreso, presion, prof_izq, prof_der, lugar_neumatico, motivo_cambio";
        private static string value_insert2 = "@num_interno, @marca, @posicion, @patente, @fecha_ingreso, @km_ingreso, @presion, @prof_izq, @prof_der, @lugar_neumatico, @motivo_cambio";
        private static string value_update = "num_interno = @num_interno, " +
        "marca = @marca, " +
        "posicion = @posicion, " +
        "patente = @patente, " +
        "fecha_ingreso = @fecha_ingreso, " +
        "km_ingreso = @km_ingreso, " +
        "presion = @presion, " +
        "prof_izq = @prof_izq, " +
        "prof_der = @prof_der, " +
        "lugar_neumatico = @lugar_neumatico, " +
        "motivo_cambio = @motivo_cambio " +
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

        public static void INSERT(ref OBJ_GD_NEUMATICOS objeto)
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
                        cmd.Parameters.AddWithValue("@num_interno", objeto.num_interno);
                        cmd.Parameters.AddWithValue("@marca", objeto.marca);
                        cmd.Parameters.AddWithValue("@posicion", objeto.posicion);
                        cmd.Parameters.AddWithValue("@patente", objeto.patente);
                        cmd.Parameters.AddWithValue("@fecha_ingreso", objeto.fecha_ingreso);
                        cmd.Parameters.AddWithValue("@km_ingreso", objeto.km_ingreso);
                        cmd.Parameters.AddWithValue("@presion", objeto.presion);
                        cmd.Parameters.AddWithValue("@prof_izq", objeto.prof_izq);
                        cmd.Parameters.AddWithValue("@prof_der", objeto.prof_der);
                        cmd.Parameters.AddWithValue("@lugar_neumatico", objeto.lugar_neumatico);
                        cmd.Parameters.AddWithValue("@motivo_cambio", objeto.motivo_cambio);
                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_NEUMATICO = scope;

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

        public static void UPDATE(ref OBJ_GD_NEUMATICOS objeto)
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
                        cmd.Parameters.AddWithValue("@num_interno", objeto.num_interno);
                        cmd.Parameters.AddWithValue("@marca", objeto.marca);
                        cmd.Parameters.AddWithValue("@posicion", objeto.posicion);
                        cmd.Parameters.AddWithValue("@patente", objeto.patente);
                        cmd.Parameters.AddWithValue("@fecha_ingreso", objeto.fecha_ingreso);
                        cmd.Parameters.AddWithValue("@km_ingreso", objeto.km_ingreso);
                        cmd.Parameters.AddWithValue("@presion", objeto.presion);
                        cmd.Parameters.AddWithValue("@prof_izq", objeto.prof_izq);
                        cmd.Parameters.AddWithValue("@prof_der", objeto.prof_der);
                        cmd.Parameters.AddWithValue("@lugar_neumatico", objeto.lugar_neumatico);
                        cmd.Parameters.AddWithValue("@motivo_cambio", objeto.motivo_cambio);

                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_NEUMATICO);
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

        public static void DELETE(ref OBJ_GD_NEUMATICOS objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_NEUMATICO);
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

        public static void LLENAOBJETO(ref OBJ_GD_NEUMATICOS objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_NEUMATICO);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        objeto.num_interno = int.Parse(reader["num_interno"].ToString());
                        objeto.marca = reader["marca"].ToString();
                        objeto.posicion = int.Parse(reader["posicion"].ToString());
                        objeto.patente = reader["patente"].ToString();
                        objeto.fecha_ingreso = DateTime.Parse(reader["fecha_ingreso"].ToString());
                        objeto.km_ingreso = int.Parse(reader["km_ingreso"].ToString());
                        objeto.presion = reader["presion"].ToString();
                        objeto.prof_izq = reader["prof_izq"].ToString();
                        objeto.prof_der = reader["prof_der"].ToString();
                        objeto.lugar_neumatico = reader["lugar_neumatico"].ToString();
                        objeto.motivo_cambio = reader["motivo_cambio"].ToString();
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

        public static void PREPARAOBJETO(ref OBJ_GD_NEUMATICOS objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.num_interno = 0;
                objeto.marca = " ";
                objeto.posicion = 0;
                objeto.patente = " ";
                objeto.fecha_ingreso = DateTime.Parse("01/01/1900");
                objeto.km_ingreso = 0;
                objeto.presion = " ";
                objeto.prof_izq = " ";
                objeto.prof_der = " ";
                objeto.lugar_neumatico = " ";
                objeto.motivo_cambio = " ";
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
