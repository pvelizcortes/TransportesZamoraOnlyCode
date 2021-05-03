using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_OC_DETALLE
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_OC_DET { get; set; }

        public int id_oc { get; set; }
        public int li { get; set; }
        public int cant { get; set; }
        public string um { get; set; }
        public string ProgPago { get; set; }
        public string glosa { get; set; }
        public string autorizada { get; set; }
        public string cancelada { get; set; }
        public string facturada { get; set; }
        public string num_factura { get; set; }
        public string sistFact { get; set; }
        public string estado { get; set; }
        public float unitario { get; set; }
        public float neto { get; set; }
        public float iva { get; set; }
        public float total { get; set; }
        public string observacion { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_OC_DETALLE
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "OC_DETALLE";
        private static string nombre_llave = "ID_OC_DET";
        private static string value_insert = "id_oc, li, cant, um, ProgPago, glosa, autorizada, cancelada, facturada, num_factura, sistFact, estado, unitario, neto, iva, total, observacion";
        private static string value_insert2 = "@id_oc, @li, @cant, @um, @ProgPago, @glosa, @autorizada, @cancelada, @facturada, @num_factura, @sistFact, @estado, @unitario, @neto, @iva, @total, @observacion";
        private static string value_update = "id_oc = @id_oc, " +
        "li = @li, " +
        "cant = @cant, " +
        "um = @um, " +
        "ProgPago = @ProgPago, " +
        "glosa = @glosa, " +
        "autorizada = @autorizada, " +
        "cancelada = @cancelada, " +
        "facturada = @facturada, " +
        "num_factura = @num_factura, " +
        "sistFact = @sistFact, " +
        "estado = @estado, " +
        "unitario = @unitario, " +
        "neto = @neto, " +
        "iva = @iva, " +
        "total = @total, " +
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

        public static void INSERT(ref OBJ_OC_DETALLE objeto)
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
                        cmd.Parameters.AddWithValue("@id_oc", objeto.id_oc);
                        cmd.Parameters.AddWithValue("@li", objeto.li);
                        cmd.Parameters.AddWithValue("@cant", objeto.cant);
                        cmd.Parameters.AddWithValue("@um", objeto.um);
                        cmd.Parameters.AddWithValue("@ProgPago", objeto.ProgPago);
                        cmd.Parameters.AddWithValue("@glosa", objeto.glosa);
                        cmd.Parameters.AddWithValue("@autorizada", objeto.autorizada);
                        cmd.Parameters.AddWithValue("@cancelada", objeto.cancelada);
                        cmd.Parameters.AddWithValue("@facturada", objeto.facturada);
                        cmd.Parameters.AddWithValue("@num_factura", objeto.num_factura);
                        cmd.Parameters.AddWithValue("@sistFact", objeto.sistFact);
                        cmd.Parameters.AddWithValue("@estado", objeto.estado);
                        cmd.Parameters.AddWithValue("@unitario", objeto.unitario);
                        cmd.Parameters.AddWithValue("@neto", objeto.neto);
                        cmd.Parameters.AddWithValue("@iva", objeto.iva);
                        cmd.Parameters.AddWithValue("@total", objeto.total);
                        cmd.Parameters.AddWithValue("@observacion", objeto.observacion);

                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_OC_DET = scope;

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

        public static void UPDATE(ref OBJ_OC_DETALLE objeto)
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
                        cmd.Parameters.AddWithValue("@li", objeto.li);
                        cmd.Parameters.AddWithValue("@cant", objeto.cant);
                        cmd.Parameters.AddWithValue("@um", objeto.um);
                        cmd.Parameters.AddWithValue("@ProgPago", objeto.ProgPago);
                        cmd.Parameters.AddWithValue("@glosa", objeto.glosa);
                        cmd.Parameters.AddWithValue("@autorizada", objeto.autorizada);
                        cmd.Parameters.AddWithValue("@cancelada", objeto.cancelada);
                        cmd.Parameters.AddWithValue("@facturada", objeto.facturada);
                        cmd.Parameters.AddWithValue("@num_factura", objeto.num_factura);
                        cmd.Parameters.AddWithValue("@sistFact", objeto.sistFact);
                        cmd.Parameters.AddWithValue("@estado", objeto.estado);
                        cmd.Parameters.AddWithValue("@unitario", objeto.unitario);
                        cmd.Parameters.AddWithValue("@neto", objeto.neto);
                        cmd.Parameters.AddWithValue("@iva", objeto.iva);
                        cmd.Parameters.AddWithValue("@total", objeto.total);
                        cmd.Parameters.AddWithValue("@observacion", objeto.observacion);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_OC_DET);
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

        public static void DELETE(ref OBJ_OC_DETALLE objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_OC_DET);
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

        public static void LLENAOBJETO(ref OBJ_OC_DETALLE objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_OC_DET);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        objeto.id_oc = int.Parse(reader["id_oc"].ToString());
                        objeto.li = int.Parse(reader["li"].ToString());
                        objeto.cant = int.Parse(reader["cant"].ToString());
                        objeto.um = reader["um"].ToString();
                        objeto.ProgPago = reader["ProgPago"].ToString();
                        objeto.glosa = reader["glosa"].ToString();
                        objeto.autorizada = reader["autorizada"].ToString();
                        objeto.cancelada = reader["cancelada"].ToString();
                        objeto.facturada = reader["facturada"].ToString();
                        objeto.num_factura = reader["num_factura"].ToString();
                        objeto.sistFact = reader["sistFact"].ToString();
                        objeto.estado = reader["estado"].ToString();
                        objeto.unitario = float.Parse(reader["unitario"].ToString());
                        objeto.neto = float.Parse(reader["neto"].ToString());
                        objeto.iva = float.Parse(reader["iva"].ToString());
                        objeto.total = float.Parse(reader["total"].ToString());
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

        public static void PREPARAOBJETO(ref OBJ_OC_DETALLE objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.id_oc = 0;
                objeto.li = 0;
                objeto.cant = 0;
                objeto.um = " ";
                objeto.ProgPago = " ";
                objeto.glosa = " ";
                objeto.autorizada = "NO";
                objeto.cancelada = "NO";
                objeto.facturada = "NO";
                objeto.num_factura = " ";
                objeto.sistFact = "NO";
                objeto.estado = "ABIERTA";
                objeto.unitario = 0;
                objeto.neto = 0;
                objeto.iva = 0;
                objeto.total = 0;
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
