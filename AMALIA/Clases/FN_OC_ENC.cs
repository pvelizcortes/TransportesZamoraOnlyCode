using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_OC_ENC
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_OC { get; set; }

        public int correlativo_oc { get; set; }
        public DateTime fecha_oc { get; set; }
        public int id_proveedor { get; set; }
        public string solicitante { get; set; }
        public string destino { get; set; }
        public string contacto { get; set; }
        public string email { get; set; }
        public DateTime plazo_entrega { get; set; }
        public string clase { get; set; }
        public float neto { get; set; }
        public float iva { get; set; }
        public float total { get; set; }
        public string observacion { get; set; }
        public int usuario { get; set; }
        public DateTime fecha_creacion { get; set; }
        public string aprobado_mz { get; set; }
        public string aprobado_fz { get; set; }
        public string obs_aprobacion { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_OC_ENC
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "OC_ENC";
        private static string nombre_llave = "ID_OC";
        private static string value_insert = "correlativo_oc, fecha_oc, id_proveedor, solicitante, destino, contacto, email, plazo_entrega, clase, neto, iva, total, observacion, usuario, fecha_creacion, aprobado_mz, aprobado_fz, obs_aprobacion";
        private static string value_insert2 = "@correlativo_oc, @fecha_oc, @id_proveedor, @solicitante, @destino, @contacto, @email, @plazo_entrega, @clase, @neto, @iva, @total, @observacion, @usuario, @fecha_creacion, @aprobado_mz, @aprobado_fz, @obs_aprobacion";
        private static string value_update = "correlativo_oc = @correlativo_oc, " +
        "fecha_oc = @fecha_oc, " +
        "id_proveedor = @id_proveedor, " +
        "solicitante = @solicitante, " +
        "destino = @destino, " +
        "contacto = @contacto, " +
        "email = @email, " +
        "plazo_entrega = @plazo_entrega, " +
        "clase = @clase, " +
        "neto = @neto, " +
        "iva = @iva, " +
        "total = @total, " +
        "observacion = @observacion, " +
        "usuario = @usuario, " +
        "fecha_creacion = @fecha_creacion, " +
        "aprobado_mz = @aprobado_mz, " +
        "aprobado_fz = @aprobado_fz, " +
        "obs_aprobacion = @obs_aprobacion " +
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

        public static void INSERT(ref OBJ_OC_ENC objeto)
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
                        cmd.Parameters.AddWithValue("@correlativo_oc", objeto.correlativo_oc);
                        cmd.Parameters.AddWithValue("@fecha_oc", objeto.fecha_oc);
                        cmd.Parameters.AddWithValue("@id_proveedor", objeto.id_proveedor);
                        cmd.Parameters.AddWithValue("@solicitante", objeto.solicitante);
                        cmd.Parameters.AddWithValue("@destino", objeto.destino);
                        cmd.Parameters.AddWithValue("@contacto", objeto.contacto);
                        cmd.Parameters.AddWithValue("@email", objeto.email);
                        cmd.Parameters.AddWithValue("@plazo_entrega", objeto.plazo_entrega);
                        cmd.Parameters.AddWithValue("@clase", objeto.clase);
                        cmd.Parameters.AddWithValue("@neto", objeto.neto);
                        cmd.Parameters.AddWithValue("@iva", objeto.iva);
                        cmd.Parameters.AddWithValue("@total", objeto.total);
                        cmd.Parameters.AddWithValue("@observacion", objeto.observacion);
                        cmd.Parameters.AddWithValue("@usuario", objeto.usuario);
                        cmd.Parameters.AddWithValue("@fecha_creacion", objeto.fecha_creacion);
                        cmd.Parameters.AddWithValue("@aprobado_mz", objeto.aprobado_mz);
                        cmd.Parameters.AddWithValue("@aprobado_fz", objeto.aprobado_fz);
                        cmd.Parameters.AddWithValue("@obs_aprobacion", objeto.obs_aprobacion);

                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_OC = scope;

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

        public static void UPDATE(ref OBJ_OC_ENC objeto)
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
                        cmd.Parameters.AddWithValue("@correlativo_oc", objeto.correlativo_oc);
                        cmd.Parameters.AddWithValue("@fecha_oc", objeto.fecha_oc);
                        cmd.Parameters.AddWithValue("@id_proveedor", objeto.id_proveedor);
                        cmd.Parameters.AddWithValue("@solicitante", objeto.solicitante);
                        cmd.Parameters.AddWithValue("@destino", objeto.destino);
                        cmd.Parameters.AddWithValue("@contacto", objeto.contacto);
                        cmd.Parameters.AddWithValue("@email", objeto.email);
                        cmd.Parameters.AddWithValue("@plazo_entrega", objeto.plazo_entrega);
                        cmd.Parameters.AddWithValue("@clase", objeto.clase);
                        cmd.Parameters.AddWithValue("@neto", objeto.neto);
                        cmd.Parameters.AddWithValue("@iva", objeto.iva);
                        cmd.Parameters.AddWithValue("@total", objeto.total);
                        cmd.Parameters.AddWithValue("@observacion", objeto.observacion);
                        cmd.Parameters.AddWithValue("@usuario", objeto.usuario);
                        cmd.Parameters.AddWithValue("@fecha_creacion", objeto.fecha_creacion);
                        cmd.Parameters.AddWithValue("@aprobado_mz", objeto.aprobado_mz);
                        cmd.Parameters.AddWithValue("@aprobado_fz", objeto.aprobado_fz);
                        cmd.Parameters.AddWithValue("@obs_aprobacion", objeto.obs_aprobacion);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_OC);
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

        public static void DELETE(ref OBJ_OC_ENC objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_OC);
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

        public static void LLENAOBJETO(ref OBJ_OC_ENC objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_OC);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // patente, num_chasis, num_motor, marca, ano, carga, activo
                        objeto.correlativo_oc = int.Parse(reader["correlativo_oc"].ToString());
                        objeto.fecha_oc = DateTime.Parse(reader["fecha_oc"].ToString());
                        objeto.id_proveedor = int.Parse(reader["id_proveedor"].ToString());
                        objeto.solicitante = reader["solicitante"].ToString();
                        objeto.destino = reader["destino"].ToString();
                        objeto.contacto = reader["contacto"].ToString();
                        objeto.email = reader["email"].ToString();
                        objeto.plazo_entrega = DateTime.Parse(reader["plazo_entrega"].ToString());
                        objeto.clase = reader["clase"].ToString();
                        objeto.neto = float.Parse(reader["neto"].ToString());
                        objeto.iva = float.Parse(reader["iva"].ToString());
                        objeto.total = float.Parse(reader["total"].ToString());
                        objeto.observacion = reader["observacion"].ToString();
                        objeto.usuario = int.Parse(reader["usuario"].ToString());
                        objeto.fecha_creacion = DateTime.Parse(reader["fecha_creacion"].ToString());
                        objeto.aprobado_mz = reader["aprobado_mz"].ToString();
                        objeto.aprobado_fz = reader["aprobado_fz"].ToString();
                        objeto.obs_aprobacion = reader["obs_aprobacion"].ToString();
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

        public static void PREPARAOBJETO(ref OBJ_OC_ENC objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.correlativo_oc = 0;
                objeto.fecha_oc = DateTime.Now;
                objeto.id_proveedor = 0;
                objeto.solicitante = " ";
                objeto.destino = " ";
                objeto.contacto = " ";
                objeto.email = " ";
                objeto.plazo_entrega = DateTime.Now;
                objeto.clase = " ";
                objeto.neto = 0;
                objeto.iva = 0;
                objeto.total = 0;
                objeto.observacion = " ";
                objeto.usuario = 0;
                objeto.fecha_creacion = DateTime.Now;
                objeto.aprobado_mz = "NO";
                objeto.aprobado_fz = "NO";
                objeto.obs_aprobacion = " ";
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

        public static int GetCorrelativo()
        {
            DBUtil db = new DBUtil();
            int scalar = int.Parse(db.Scalar("select (ISNULL(MAX(oc),0) + 1) from correlativos").ToString());
            return scalar;
        }

        public static void GetTotales(ref OBJ_OC_ENC objeto)
        {
            try
            {
                DBUtil db = new DBUtil();
                DataTable dt = new DataTable();
                dt = db.consultar("select isnull(sum(neto),0) as 'neto', isnull(sum(iva),0) as 'iva', isnull(sum(total),0) as 'total' from oc_detalle where id_oc = " + objeto.ID_OC);
                FN_OC_ENC.LLENAOBJETO(ref objeto);
                if (objeto._respok)
                {
                    objeto.neto = (int)Math.Round(float.Parse(dt.Rows[0][0].ToString())); 
                    objeto.iva = (int)Math.Round(float.Parse(dt.Rows[0][1].ToString()));
                    objeto.total = (int)Math.Round(float.Parse(dt.Rows[0][2].ToString()));
                    FN_OC_ENC.UPDATE(ref objeto);
                }
            }
            catch (Exception ex)
            {
                objeto._respok = false;
                objeto._respdet = ex.Message;
            }

            
        }

        public static DataTable getDetalleOC(int IDOC)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connex))
            {
                conn.Open();
                string sql = @"SELECT *  from v_ordenes_compra_detalle where id_oc = " + IDOC;
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter ap = new SqlDataAdapter(cmd);
                ap.Fill(dt);
            }
            return dt;
        }
    }
}
