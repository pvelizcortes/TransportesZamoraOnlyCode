using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_CAMION
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_CAMION { get; set; }

        public string patente { get; set; }
        public string num_chasis { get; set; }
        public string num_motor { get; set; }
        public int id_marca_camion { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public int ano { get; set; }
        public string carga { get; set; }
        public string activo { get; set; }
        public string vin { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_CAMION
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "camion";
        private static string nombre_llave = "ID_CAMION";
        private static string value_insert = "patente, num_chasis, num_motor, id_marca_camion, marca, modelo, ano, carga, activo, vin";
        private static string value_insert2 = "@patente, @num_chasis, @num_motor, @id_marca_camion, @marca, @modelo, @ano, @carga, @activo, @vin";
        private static string value_update = "patente = @patente, " +
        "num_chasis = @num_chasis, " +
        "num_motor = @num_motor, " +
        "id_marca_camion = @id_marca_camion, " +
        "marca = @marca, " +
        "modelo = @modelo, " +
        "ano = @ano, " +
        "carga = @carga, " +
        "activo = @activo, " +
        "vin = @vin " +
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

        public static void INSERT(ref OBJ_CAMION objeto)
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
                        //  patente, num_chasis, num_motor, marca, ano, carga, activo
                        cmd.Parameters.AddWithValue("@patente", objeto.patente);
                        cmd.Parameters.AddWithValue("@num_chasis", objeto.num_chasis);
                        cmd.Parameters.AddWithValue("@num_motor", objeto.num_motor);
                        cmd.Parameters.AddWithValue("@id_marca_camion", objeto.id_marca_camion);
                        cmd.Parameters.AddWithValue("@marca", objeto.marca);
                        cmd.Parameters.AddWithValue("@modelo", objeto.modelo);
                        cmd.Parameters.AddWithValue("@ano", objeto.ano);
                        cmd.Parameters.AddWithValue("@carga", objeto.carga);
                        cmd.Parameters.AddWithValue("@activo", objeto.activo);
                        cmd.Parameters.AddWithValue("@vin", objeto.vin);

                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_CAMION = scope;

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

        public static void UPDATE(ref OBJ_CAMION objeto)
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
                        cmd.Parameters.AddWithValue("@patente", objeto.patente);
                        cmd.Parameters.AddWithValue("@num_chasis", objeto.num_chasis);
                        cmd.Parameters.AddWithValue("@num_motor", objeto.num_motor);
                        cmd.Parameters.AddWithValue("@id_marca_camion", objeto.id_marca_camion);
                        cmd.Parameters.AddWithValue("@marca", objeto.marca);
                        cmd.Parameters.AddWithValue("@modelo", objeto.modelo);
                        cmd.Parameters.AddWithValue("@ano", objeto.ano);
                        cmd.Parameters.AddWithValue("@carga", objeto.carga);
                        cmd.Parameters.AddWithValue("@activo", objeto.activo);
                        cmd.Parameters.AddWithValue("@vin", objeto.vin);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_CAMION);
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

        public static void DELETE(ref OBJ_CAMION objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_CAMION);
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

        public static void LLENAOBJETO(ref OBJ_CAMION objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_CAMION);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // patente, num_chasis, num_motor, marca, ano, carga, activo
                        objeto.patente = reader["patente"].ToString();
                        objeto.num_chasis = reader["num_chasis"].ToString();
                        objeto.num_motor = reader["num_motor"].ToString();
                        objeto.id_marca_camion = int.Parse(reader["id_marca_camion"].ToString());
                        objeto.marca = reader["marca"].ToString();
                        objeto.modelo = reader["modelo"].ToString();
                        objeto.ano = int.Parse(reader["ano"].ToString());
                        objeto.carga = reader["carga"].ToString();
                        objeto.activo = reader["activo"].ToString();
                        objeto.vin = reader["vin"].ToString();
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

        public static void PREPARAOBJETO(ref OBJ_CAMION objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.patente = " ";
                objeto.num_chasis = " ";
                objeto.num_motor = " ";
                objeto.id_marca_camion = 0;
                objeto.marca = " ";
                objeto.modelo = " ";
                objeto.ano = 0;
                objeto.carga = " ";
                objeto.activo = "ACTIVO";
                objeto.vin = " ";
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

        // NUEVAAAAAAAAAAAAAAAAAAAAAS
        public static DataTable LLENADT_VISTA(string sql_where = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connex))
            {
                conn.Open();
                string sql = @"SELECT *  from V_GD_MANT_CAMIONES " + sql_where;
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter ap = new SqlDataAdapter(cmd);
                ap.Fill(dt);
            }
            return dt;
        }
    }
}
