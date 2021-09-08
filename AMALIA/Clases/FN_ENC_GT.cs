using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_ENC_GT
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_GT { get; set; }

        public int num_correlativo { get; set; }
        public int id_conductor { get; set; }
        public int id_conductor2 { get; set; }
        public int id_camion { get; set; }
        public int id_rampla { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_termino { get; set; }
        public int dinero_entregado { get; set; }
        public int sobre_deposito { get; set; }
        public string entregado { get; set; }
        public int km_inicial { get; set; }
        public int km_final { get; set; }
        public int id_estado { get; set; }
        public string observacion { get; set; }
        public int creada_por { get; set; }
        public DateTime fecha_creacion { get; set; }
        // TOTALES
        public int total_flete { get; set; }
        public int total_km { get; set; }
        public float total_litros { get; set; }
        public int total_precio_combustible { get; set; }
        public int total_gastos { get; set; }
        public int saldo_dinero_entregado { get; set; }
        public float rendimiento { get; set; }
        public int saldo_total { get; set; }
        public int revisado_felipe { get; set; }
        public int dinero_devuelto { get; set; }
        public string tipo_camion { get; set; }
        //
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_ENC_GT
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "enc_gt";
        private static string nombre_llave = "id_gt";
        private static string value_insert = " num_correlativo, id_conductor, id_conductor2, id_camion, id_rampla, fecha_inicio, fecha_termino, dinero_entregado, sobre_deposito, entregado, id_estado, km_inicial, km_final, observacion, creada_por, fecha_creacion, revisado_felipe, dinero_devuelto, tipo_camion ";
        private static string value_insert2 = " @num_correlativo, @id_conductor, @id_conductor2, @id_camion, @id_rampla, @fecha_inicio, @fecha_termino, @dinero_entregado, @sobre_deposito, @entregado, @id_estado, @km_inicial, @km_final, @observacion, @creada_por, @fecha_Creacion, @revisado_felipe, @dinero_devuelto, @tipo_camion ";
        private static string value_update =
        " num_correlativo = @num_correlativo, " +
        " id_conductor = @id_conductor, " +
        " id_conductor2 = @id_conductor2, " +
        " id_camion = @id_camion, " +
        " id_rampla = @id_rampla, " +
        " fecha_inicio = @fecha_inicio, " +
        " fecha_termino = @fecha_termino, " +
        " dinero_entregado = @dinero_entregado, " +
        " sobre_deposito = @sobre_deposito, " +
        " dinero_devuelto = @dinero_devuelto, " +
        " entregado = @entregado, " +
        " km_inicial = @km_inicial, " +
        " km_final = @km_final, " +
        " observacion = @observacion, " +
        " revisado_felipe = @revisado_felipe, " +
        " tipo_camion = @tipo_camion, " +
        " id_estado = @id_estado " +    // <----------- EL ULTIMO SIN COMA PLS <3

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

        public static void INSERT(ref OBJ_ENC_GT objeto)
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
                        // num_correlativo, id_conductor, id_camion , fecha_inicio, fecha_termino, dinero_entregado, id_estado
                        cmd.Parameters.AddWithValue("@num_correlativo", objeto.num_correlativo);
                        cmd.Parameters.AddWithValue("@id_conductor", objeto.id_conductor);
                        cmd.Parameters.AddWithValue("@id_conductor2", objeto.id_conductor2);
                        cmd.Parameters.AddWithValue("@id_camion", objeto.id_camion);
                        cmd.Parameters.AddWithValue("@id_rampla", objeto.id_rampla);
                        cmd.Parameters.AddWithValue("@fecha_inicio", objeto.fecha_inicio);
                        cmd.Parameters.AddWithValue("@fecha_termino", objeto.fecha_termino);
                        cmd.Parameters.AddWithValue("@dinero_entregado", objeto.dinero_entregado);
                        cmd.Parameters.AddWithValue("@sobre_deposito", objeto.sobre_deposito);
                        cmd.Parameters.AddWithValue("@dinero_devuelto", objeto.dinero_devuelto);
                        cmd.Parameters.AddWithValue("@entregado", objeto.entregado);
                        cmd.Parameters.AddWithValue("@km_inicial", objeto.km_inicial);
                        cmd.Parameters.AddWithValue("@km_final", objeto.km_final);
                        cmd.Parameters.AddWithValue("@observacion", objeto.observacion);
                        cmd.Parameters.AddWithValue("@id_estado", objeto.id_estado);
                        cmd.Parameters.AddWithValue("@fecha_creacion", objeto.fecha_creacion);
                        cmd.Parameters.AddWithValue("@creada_por", objeto.creada_por);
                        cmd.Parameters.AddWithValue("@revisado_felipe", objeto.revisado_felipe);
                        cmd.Parameters.AddWithValue("@tipo_camion", objeto.tipo_camion);

                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_GT = scope;

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

        public static void UPDATE(ref OBJ_ENC_GT objeto)
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
                        cmd.Parameters.AddWithValue("@num_correlativo", objeto.num_correlativo);
                        cmd.Parameters.AddWithValue("@id_conductor", objeto.id_conductor);
                        cmd.Parameters.AddWithValue("@id_conductor2", objeto.id_conductor2);
                        cmd.Parameters.AddWithValue("@id_camion", objeto.id_camion);
                        cmd.Parameters.AddWithValue("@id_rampla", objeto.id_rampla);
                        cmd.Parameters.AddWithValue("@fecha_inicio", objeto.fecha_inicio);
                        cmd.Parameters.AddWithValue("@fecha_termino", objeto.fecha_termino);
                        cmd.Parameters.AddWithValue("@dinero_entregado", objeto.dinero_entregado);
                        cmd.Parameters.AddWithValue("@sobre_deposito", objeto.sobre_deposito);
                        cmd.Parameters.AddWithValue("@dinero_devuelto", objeto.dinero_devuelto);
                        cmd.Parameters.AddWithValue("@entregado", objeto.entregado);
                        cmd.Parameters.AddWithValue("@km_inicial", objeto.km_inicial);
                        cmd.Parameters.AddWithValue("@km_final", objeto.km_final);
                        cmd.Parameters.AddWithValue("@observacion", objeto.observacion);
                        cmd.Parameters.AddWithValue("@id_estado", objeto.id_estado);
                        cmd.Parameters.AddWithValue("@revisado_felipe", objeto.revisado_felipe);
                        cmd.Parameters.AddWithValue("@tipo_camion", objeto.tipo_camion);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_GT);
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

        public static void DELETE(ref OBJ_ENC_GT objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_GT);
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

        public static void LLENAOBJETO(ref OBJ_ENC_GT objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_GT);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // num_correlativo, id_conductor, id_camion , fecha_inicio, fecha_termino, dinero_entregado, id_estado
                        objeto.num_correlativo = int.Parse(reader["num_correlativo"].ToString());
                        objeto.id_conductor = int.Parse(reader["id_conductor"].ToString());
                        objeto.id_conductor2 = int.Parse(reader["id_conductor2"].ToString());
                        objeto.id_camion = int.Parse(reader["id_camion"].ToString());
                        objeto.id_rampla = int.Parse(reader["id_rampla"].ToString());
                        objeto.fecha_inicio = DateTime.Parse(reader["fecha_inicio"].ToString());
                        objeto.fecha_termino = DateTime.Parse(reader["fecha_termino"].ToString());
                        objeto.dinero_entregado = int.Parse(reader["dinero_entregado"].ToString());
                        objeto.sobre_deposito = int.Parse(reader["sobre_deposito"].ToString());
                        objeto.dinero_devuelto = int.Parse(reader["dinero_devuelto"].ToString());
                        objeto.entregado = reader["entregado"].ToString();
                        objeto.km_inicial = int.Parse(reader["km_inicial"].ToString());
                        objeto.km_final = int.Parse(reader["km_final"].ToString());
                        objeto.id_estado = int.Parse(reader["id_estado"].ToString());
                        objeto.observacion = reader["observacion"].ToString();
                        objeto.saldo_dinero_entregado = int.Parse(reader["saldo_dinero_entregado"].ToString());
                        objeto.total_flete = int.Parse(reader["total_flete"].ToString());
                        objeto.total_gastos = int.Parse(reader["total_gastos"].ToString());
                        objeto.total_precio_combustible = int.Parse(reader["total_precio_combustible"].ToString());
                        objeto.total_km = int.Parse(reader["total_km"].ToString());
                        objeto.total_litros = float.Parse(reader["total_litros"].ToString());
                        objeto.rendimiento = float.Parse(reader["rendimiento"].ToString());
                        objeto.saldo_total = int.Parse(reader["saldo_total"].ToString());
                        objeto.creada_por = int.Parse(reader["creada_por"].ToString());
                        objeto.fecha_creacion = DateTime.Parse(reader["fecha_creacion"].ToString());
                        objeto.revisado_felipe = int.Parse(reader["revisado_felipe"].ToString());
                        objeto.tipo_camion = reader["tipo_camion"].ToString();
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

        public static void PREPARAOBJETO(ref OBJ_ENC_GT objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.num_correlativo = 0;
                objeto.id_conductor = 0;
                objeto.id_conductor2 = 0;
                objeto.id_camion = 0;
                objeto.id_rampla = 0;
                objeto.fecha_inicio = DateTime.Now;
                objeto.fecha_termino = DateTime.Now;
                objeto.dinero_entregado = 0;
                objeto.dinero_devuelto = 0;
                objeto.sobre_deposito = 0;
                objeto.entregado = "NO";
                objeto.km_inicial = 0;
                objeto.km_final = 0;
                objeto.id_estado = 1;
                objeto.observacion = "Sin observación";
                objeto.creada_por = 0;
                objeto.revisado_felipe = 0;
                objeto.fecha_creacion = DateTime.Now;
                objeto.tipo_camion = "Camión";
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
                string sql = @"SELECT * from V_TABLA_GT " + sql_where + " order by num_correlativo DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter ap = new SqlDataAdapter(cmd);
                ap.Fill(dt);
            }
            return dt;
        }

        public static DataTable LLENADTVISTATOP()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connex))
            {
                conn.Open();
                string sql = @"SELECT top(100)*  from V_TABLA_GT order by num_correlativo DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter ap = new SqlDataAdapter(cmd);
                ap.Fill(dt);
            }
            return dt;
        }

        public static void LLENAOBJETOCORRELATIVO(ref OBJ_ENC_GT objeto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connex))
                {
                    conn.Open();
                    string sql = @"SELECT * from " + nombre_tabla + " where num_correlativo = @num_correlativo";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    // CAMBIAR AQUI //
                    // **************************************** **************************************** **************************************** //   
                    cmd.Parameters.AddWithValue("@num_correlativo", objeto.num_correlativo);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // num_correlativo, id_conductor, id_camion , fecha_inicio, fecha_termino, dinero_entregado, id_estado
                        objeto.ID_GT = int.Parse(reader["id_gt"].ToString());
                        objeto.num_correlativo = int.Parse(reader["num_correlativo"].ToString());
                        objeto.id_conductor = int.Parse(reader["id_conductor"].ToString());
                        objeto.id_conductor2 = int.Parse(reader["id_conductor2"].ToString());
                        objeto.id_camion = int.Parse(reader["id_camion"].ToString());
                        objeto.id_rampla = int.Parse(reader["id_rampla"].ToString());
                        objeto.fecha_inicio = DateTime.Parse(reader["fecha_inicio"].ToString());
                        objeto.fecha_termino = DateTime.Parse(reader["fecha_termino"].ToString());
                        objeto.dinero_entregado = int.Parse(reader["dinero_entregado"].ToString());
                        objeto.sobre_deposito = int.Parse(reader["sobre_deposito"].ToString());
                        objeto.dinero_devuelto = int.Parse(reader["dinero_devuelto"].ToString());
                        objeto.entregado = reader["entregado"].ToString();
                        objeto.km_inicial = int.Parse(reader["km_inicial"].ToString());
                        objeto.km_final = int.Parse(reader["km_final"].ToString());
                        objeto.id_estado = int.Parse(reader["id_estado"].ToString());
                        objeto.observacion = reader["observacion"].ToString();
                        objeto.saldo_dinero_entregado = int.Parse(reader["saldo_dinero_entregado"].ToString());
                        objeto.total_flete = int.Parse(reader["total_flete"].ToString());
                        objeto.total_gastos = int.Parse(reader["total_gastos"].ToString());
                        objeto.total_precio_combustible = int.Parse(reader["total_precio_combustible"].ToString());
                        objeto.total_km = int.Parse(reader["total_km"].ToString());
                        objeto.total_litros = float.Parse(reader["total_litros"].ToString());
                        objeto.rendimiento = float.Parse(reader["rendimiento"].ToString());
                        objeto.saldo_total = int.Parse(reader["saldo_total"].ToString());
                        objeto.creada_por = int.Parse(reader["creada_por"].ToString());
                        objeto.fecha_creacion = DateTime.Parse(reader["fecha_creacion"].ToString());
                        objeto.revisado_felipe = int.Parse(reader["revisado_felipe"].ToString());
                        objeto.tipo_camion = reader["tipo_camion"].ToString();
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
    }
}
