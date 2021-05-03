using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_ENC_OTZ
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_OTZ { get; set; }

        public int id_gt { get; set; }
        public int correlativo_otz { get; set; }
        public int correlativo_gt { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public int id_obra { get; set; }
        public int id_cliente { get; set; }
        public string guia { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_final { get; set; }
        public int valor_viaje { get; set; }
        public string activo { get; set; }
        public int estadia { get; set; }
        public int entradas { get; set; }
        public int doble_conductor { get; set; }
        public int carga_descarga { get; set; }
        public int flete_de_tercero { get; set; }
        public int otros { get; set; }
        public string detalle_otros { get; set; }
        public string d_sol_oc { get; set; }
        public string d_ot { get; set; }
        public string d_eepp { get; set; }
        public string d_gasto { get; set; }
        public string d_oc { get; set; }
        public string d_hes { get; set; }
        public string d_factura { get; set; }
        public string d_nombre_tercero { get; set; }
        public string d_eepp_tercero { get; set; }
        public string d_factura_tercero { get; set; }
        public int diferencia_factura { get; set; }
        public string observacion_factura { get; set; }

        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_ENC_OTZ
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "enc_otz";
        private static string nombre_llave = "id_otz";
        private static string value_insert = " id_gt, correlativo_otz, correlativo_gt, origen, destino, id_obra, id_cliente, guia, fecha_inicio, fecha_final, valor_viaje, activo, estadia, entradas, doble_conductor, carga_descarga, flete_de_tercero, otros, detalle_otros, d_sol_oc, d_ot, d_eepp, d_gasto, d_oc, d_hes, d_factura, d_nombre_tercero, d_eepp_tercero, d_factura_tercero, diferencia_factura, observacion_factura ";
        private static string value_insert2 = " @id_gt, @correlativo_otz, @correlativo_gt, @origen, @destino, @id_obra, @id_cliente, @guia, @fecha_inicio, @fecha_final, @valor_viaje, @activo, @estadia, @entradas, @doble_conductor,  @carga_descarga, @flete_de_tercero, @otros, @detalle_otros, @d_sol_oc , @d_ot, @d_eepp, @d_gasto, @d_oc, @d_hes, @d_factura, @d_nombre_tercero, @d_eepp_tercero, @d_factura_tercero, @diferencia_factura, @observacion_factura ";
        private static string value_update =
        " id_gt = @id_gt, " +
        " correlativo_otz = @correlativo_otz, " +
        " correlativo_gt = @correlativo_gt, " +
        " origen = @origen, " +
        " destino = @destino, " +
        " id_obra = @id_obra, " +
        " id_cliente = @id_cliente, " +
        " guia = @guia, " +
        " fecha_inicio = @fecha_inicio, " +
        " fecha_final = @fecha_final, " +
        " valor_viaje = @valor_viaje, " +
        " estadia = @estadia, " +
        " entradas = @entradas, " +
        " doble_conductor = @doble_conductor, " +
        " carga_descarga = @carga_descarga, " +
        " flete_de_tercero = @flete_de_tercero, " +
        " otros = @otros, " +
        " detalle_otros = @detalle_otros, " +
        //@d_sol_oc , @d_ot, @d_eepp, @d_gasto, @d_oc, @d_hes, @d_factura
        " d_sol_oc = @d_sol_oc, " +
        " d_ot = @d_ot, " +
        " d_eepp = @d_eepp, " +
        " d_gasto = @d_gasto, " +
        " d_oc = @d_oc, " +
        " d_hes = @d_hes, " +
        " d_factura = @d_factura, " +
        " d_nombre_tercero = @d_nombre_tercero, " +
        " d_eepp_tercero = @d_eepp_tercero, " +
        " d_factura_tercero = @d_factura_tercero, " +
            " diferencia_factura = @diferencia_factura, " +
            " observacion_factura = @observacion_factura, " +
        " activo = @activo " +    // <----------- EL ULTIMO SIN COMA PLS <3

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

        public static DataTable LLENADTVISTA(string sql_where = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connex))
            {
                conn.Open();
                string sql = @"SELECT *  from V_OTZ " + sql_where;
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter ap = new SqlDataAdapter(cmd);
                ap.Fill(dt);
            }
            return dt;
        }

        public static void INSERT(ref OBJ_ENC_OTZ objeto)
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
                        // id_gt, correlativo_otz, correlativo_gt, origen, destino, id_obra, id_cliente, guia, fecha_inicio, fecha_final, valor_viaje, activo, estadia, entradas, doble_conductor, otros, detalle_otros 
                        // //@d_sol_oc , @d_ot, @d_eepp, @d_gasto, @d_oc, @d_hes, @d_factura
                        cmd.Parameters.AddWithValue("@id_gt", objeto.id_gt);
                        cmd.Parameters.AddWithValue("@correlativo_otz", objeto.correlativo_otz);
                        cmd.Parameters.AddWithValue("@correlativo_gt", objeto.correlativo_gt);
                        cmd.Parameters.AddWithValue("@origen", objeto.origen);
                        cmd.Parameters.AddWithValue("@destino", objeto.destino);
                        cmd.Parameters.AddWithValue("@id_obra", objeto.id_obra);
                        cmd.Parameters.AddWithValue("@id_cliente", objeto.id_cliente);
                        cmd.Parameters.AddWithValue("@guia", objeto.guia);
                        cmd.Parameters.AddWithValue("@fecha_inicio", objeto.fecha_inicio);
                        cmd.Parameters.AddWithValue("@fecha_final", objeto.fecha_final);
                        cmd.Parameters.AddWithValue("@valor_viaje", objeto.valor_viaje);
                        cmd.Parameters.AddWithValue("@activo", objeto.activo);
                        cmd.Parameters.AddWithValue("@estadia", objeto.estadia);
                        cmd.Parameters.AddWithValue("@entradas", objeto.entradas);
                        cmd.Parameters.AddWithValue("@doble_conductor", objeto.doble_conductor);
                        cmd.Parameters.AddWithValue("@carga_descarga", objeto.carga_descarga);
                        cmd.Parameters.AddWithValue("@flete_de_tercero", objeto.flete_de_tercero);
                        cmd.Parameters.AddWithValue("@otros", objeto.otros);
                        cmd.Parameters.AddWithValue("@detalle_otros", objeto.detalle_otros);
                        cmd.Parameters.AddWithValue("@d_sol_oc", objeto.d_sol_oc);
                        cmd.Parameters.AddWithValue("@d_ot", objeto.d_ot);
                        cmd.Parameters.AddWithValue("@d_eepp", objeto.d_eepp);
                        cmd.Parameters.AddWithValue("@d_gasto", objeto.d_gasto);
                        cmd.Parameters.AddWithValue("@d_oc", objeto.d_oc);
                        cmd.Parameters.AddWithValue("@d_hes", objeto.d_hes);
                        cmd.Parameters.AddWithValue("@d_factura", objeto.d_factura);
                        cmd.Parameters.AddWithValue("@d_nombre_tercero", objeto.d_nombre_tercero);
                        cmd.Parameters.AddWithValue("@d_eepp_tercero", objeto.d_eepp_tercero);
                        cmd.Parameters.AddWithValue("@d_factura_tercero", objeto.d_factura_tercero);
                        cmd.Parameters.AddWithValue("@diferencia_factura", objeto.diferencia_factura);
                        cmd.Parameters.AddWithValue("@observacion_factura", objeto.observacion_factura);
                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_OTZ = scope;

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

        public static void UPDATE(ref OBJ_ENC_OTZ objeto)
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
                        cmd.Parameters.AddWithValue("@correlativo_otz", objeto.correlativo_otz);
                        cmd.Parameters.AddWithValue("@correlativo_gt", objeto.correlativo_gt);
                        cmd.Parameters.AddWithValue("@origen", objeto.origen);
                        cmd.Parameters.AddWithValue("@destino", objeto.destino);
                        cmd.Parameters.AddWithValue("@id_obra", objeto.id_obra);
                        cmd.Parameters.AddWithValue("@id_cliente", objeto.id_cliente);
                        cmd.Parameters.AddWithValue("@guia", objeto.guia);
                        cmd.Parameters.AddWithValue("@fecha_inicio", objeto.fecha_inicio);
                        cmd.Parameters.AddWithValue("@fecha_final", objeto.fecha_final);
                        cmd.Parameters.AddWithValue("@valor_viaje", objeto.valor_viaje);
                        cmd.Parameters.AddWithValue("@activo", objeto.activo);
                        cmd.Parameters.AddWithValue("@estadia", objeto.estadia);
                        cmd.Parameters.AddWithValue("@entradas", objeto.entradas);
                        cmd.Parameters.AddWithValue("@doble_conductor", objeto.doble_conductor);
                        cmd.Parameters.AddWithValue("@carga_descarga", objeto.carga_descarga);
                        cmd.Parameters.AddWithValue("@flete_de_tercero", objeto.flete_de_tercero);
                        cmd.Parameters.AddWithValue("@otros", objeto.otros);
                        cmd.Parameters.AddWithValue("@detalle_otros", objeto.detalle_otros);
                        cmd.Parameters.AddWithValue("@d_sol_oc", objeto.d_sol_oc);
                        cmd.Parameters.AddWithValue("@d_ot", objeto.d_ot);
                        cmd.Parameters.AddWithValue("@d_eepp", objeto.d_eepp);
                        cmd.Parameters.AddWithValue("@d_gasto", objeto.d_gasto);
                        cmd.Parameters.AddWithValue("@d_oc", objeto.d_oc);
                        cmd.Parameters.AddWithValue("@d_hes", objeto.d_hes);
                        cmd.Parameters.AddWithValue("@d_factura", objeto.d_factura);
                        cmd.Parameters.AddWithValue("@d_nombre_tercero", objeto.d_nombre_tercero);
                        cmd.Parameters.AddWithValue("@d_eepp_tercero", objeto.d_eepp_tercero);
                        cmd.Parameters.AddWithValue("@d_factura_tercero", objeto.d_factura_tercero);
                        cmd.Parameters.AddWithValue("@diferencia_factura", objeto.diferencia_factura);
                        cmd.Parameters.AddWithValue("@observacion_factura", objeto.observacion_factura);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_OTZ);
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

        public static void DELETE(ref OBJ_ENC_OTZ objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_OTZ);
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

        public static void LLENAOBJETO(ref OBJ_ENC_OTZ objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_OTZ);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // id_gt, correlativo_otz, correlativo_gt, origen, destino, id_obra, id_cliente, guia, fecha_inicio, fecha_final, valor_viaje, activo, estadia, entradas, doble_conductor, otros, detalle_otros 
                        objeto.id_gt = int.Parse(reader["id_gt"].ToString());
                        objeto.correlativo_otz = int.Parse(reader["correlativo_otz"].ToString());
                        objeto.correlativo_gt = int.Parse(reader["correlativo_gt"].ToString());
                        objeto.origen = reader["origen"].ToString();
                        objeto.destino = reader["destino"].ToString();
                        objeto.id_obra = int.Parse(reader["id_obra"].ToString());
                        objeto.id_cliente = int.Parse(reader["id_cliente"].ToString());
                        objeto.guia = reader["guia"].ToString();
                        objeto.fecha_inicio = DateTime.Parse(reader["fecha_inicio"].ToString());
                        objeto.fecha_final = DateTime.Parse(reader["fecha_final"].ToString());
                        objeto.valor_viaje = int.Parse(reader["valor_viaje"].ToString());
                        objeto.activo = reader["activo"].ToString();
                        objeto.estadia = int.Parse(reader["estadia"].ToString());
                        objeto.entradas = int.Parse(reader["entradas"].ToString());
                        objeto.doble_conductor = int.Parse(reader["doble_conductor"].ToString());
                        objeto.carga_descarga = int.Parse(reader["carga_descarga"].ToString());
                        objeto.flete_de_tercero = int.Parse(reader["flete_de_tercero"].ToString());
                        objeto.otros = int.Parse(reader["otros"].ToString());
                        objeto.detalle_otros = reader["detalle_otros"].ToString();
                        // //@d_sol_oc , @d_ot, @d_eepp, @d_gasto, @d_oc, @d_hes, @d_factura
                        objeto.d_sol_oc = reader["d_sol_oc"].ToString();
                        objeto.d_ot = reader["d_ot"].ToString();
                        objeto.d_eepp = reader["d_eepp"].ToString();
                        objeto.d_gasto = reader["d_gasto"].ToString();
                        objeto.d_oc = reader["d_oc"].ToString();
                        objeto.d_hes = reader["d_hes"].ToString();
                        objeto.d_factura = reader["d_factura"].ToString();
                        objeto.d_nombre_tercero = reader["d_nombre_tercero"].ToString();
                        objeto.d_eepp_tercero = reader["d_eepp_tercero"].ToString();
                        objeto.d_factura_tercero = reader["d_factura_tercero"].ToString();
                        objeto.diferencia_factura = int.Parse(reader["diferencia_factura"].ToString());
                        objeto.observacion_factura = reader["observacion_factura"].ToString();
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

        public static void PREPARAOBJETO(ref OBJ_ENC_OTZ objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.id_gt = 0;
                objeto.correlativo_otz = 0;
                objeto.correlativo_gt = 0;
                objeto.origen = "no";
                objeto.destino = "no";
                objeto.id_cliente = 1;
                objeto.guia = "no";
                objeto.fecha_inicio = DateTime.Now;
                objeto.fecha_final = DateTime.Now;
                objeto.valor_viaje = 0;
                objeto.activo = "ACTIVO";
                objeto.estadia = 0;
                objeto.entradas = 0;
                objeto.doble_conductor = 0;
                objeto.carga_descarga = 0;
                objeto.flete_de_tercero = 0;
                objeto.otros = 0;
                objeto.detalle_otros = "no";
                objeto.d_sol_oc = " ";
                objeto.d_ot = " ";
                objeto.d_eepp = " ";
                objeto.d_gasto = " ";
                objeto.d_oc = " ";
                objeto.d_hes = " ";
                objeto.d_factura = " ";
                objeto.d_nombre_tercero = " ";
                objeto.d_eepp_tercero = " ";
                objeto.d_factura_tercero = " ";
                objeto.diferencia_factura = 0;
                objeto.observacion_factura = " ";
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
    }
}
