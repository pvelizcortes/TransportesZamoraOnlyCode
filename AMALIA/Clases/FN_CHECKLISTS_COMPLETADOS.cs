using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_CHECKLISTS_COMPLETADOS
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_CHECKLIST_COMPLETADO { get; set; }

        public int id_checklist { get; set; }
        public DateTime fecha { get; set; }
        public string usuario { get; set; }
        public string observacion { get; set; }
        public string estado { get; set; }
        public string nombre_conductor { get; set; }
        public string rut { get; set; }
        public string patente_camion { get; set; }
        public string patente_rampla { get; set; }
        public DateTime fecha_aprobacion { get; set; }
        public string usuario_aprobacion { get; set; }
        public string observacion_aprobacion { get; set; }
        public string nombre_inspeccion { get; set; }
        public string nombre_Proveedor { get; set; }
        public string aux1 { get; set; }
        public string aux2 { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_CHECKLISTS_COMPLETADOS
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        private static string nombre_tabla = "CHECKLISTS_COMPLETADOS";
        private static string nombre_llave = "ID_CHECKLIST_COMPLETADO";
        private static string value_insert = "id_checklist, fecha, usuario, observacion, estado, nombre_conductor, rut, patente_camion, patente_rampla, fecha_aprobacion, usuario_aprobacion, observacion_aprobacion, nombre_inspeccion, nombre_Proveedor, aux1, aux2";
        private static string value_insert2 = "@id_checklist, @fecha, @usuario, @observacion, @estado, @nombre_conductor, @rut, @patente_camion, @patente_rampla, @fecha_aprobacion, @usuario_aprobacion, @observacion_aprobacion, @nombre_inspeccion, @nombre_Proveedor, @aux1, @aux2";
        private static string value_update = "id_checklist = @id_checklist, " +
        "fecha = @fecha, " +
        "usuario = @usuario, " +
        "observacion = @observacion, " +
        "estado = @estado, " +
        "nombre_conductor = @nombre_conductor, " +
        "rut = @rut, " +
        "patente_camion = @patente_camion, " +
        "patente_rampla = @patente_rampla, " +
        "fecha_aprobacion = @fecha_aprobacion, " +
        "usuario_aprobacion = @usuario_aprobacion, " +
        "observacion_aprobacion = @observacion_aprobacion, " +
        "nombre_inspeccion = @nombre_inspeccion, " +
        "nombre_Proveedor = @nombre_Proveedor, " +
        "aux1 = @aux1, " +
        "aux2 = @aux2 " +
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

        public static void INSERT(ref OBJ_CHECKLISTS_COMPLETADOS objeto)
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
                        cmd.Parameters.AddWithValue("@id_checklist", objeto.id_checklist);
                        cmd.Parameters.AddWithValue("@fecha", objeto.fecha);
                        cmd.Parameters.AddWithValue("@usuario", objeto.usuario);
                        cmd.Parameters.AddWithValue("@observacion", objeto.observacion);
                        cmd.Parameters.AddWithValue("@estado", objeto.estado);
                        cmd.Parameters.AddWithValue("@nombre_conductor", objeto.nombre_conductor);
                        cmd.Parameters.AddWithValue("@rut", objeto.rut);
                        cmd.Parameters.AddWithValue("@patente_camion", objeto.patente_camion);
                        cmd.Parameters.AddWithValue("@patente_rampla", objeto.patente_rampla);
                        cmd.Parameters.AddWithValue("@fecha_aprobacion", objeto.fecha_aprobacion);
                        cmd.Parameters.AddWithValue("@usuario_aprobacion", objeto.usuario_aprobacion);
                        cmd.Parameters.AddWithValue("@observacion_aprobacion", objeto.observacion_aprobacion);
                        cmd.Parameters.AddWithValue("@nombre_inspeccion", objeto.nombre_inspeccion);
                        cmd.Parameters.AddWithValue("@nombre_Proveedor", objeto.nombre_Proveedor);
                        cmd.Parameters.AddWithValue("@aux1", objeto.aux1);
                        cmd.Parameters.AddWithValue("@aux2", objeto.aux2);

                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_CHECKLIST_COMPLETADO = scope;

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

        public static void UPDATE(ref OBJ_CHECKLISTS_COMPLETADOS objeto)
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
                        cmd.Parameters.AddWithValue("@id_checklist", objeto.id_checklist);
                        cmd.Parameters.AddWithValue("@fecha", objeto.fecha);
                        cmd.Parameters.AddWithValue("@usuario", objeto.usuario);
                        cmd.Parameters.AddWithValue("@observacion", objeto.observacion);
                        cmd.Parameters.AddWithValue("@estado", objeto.estado);
                        cmd.Parameters.AddWithValue("@nombre_conductor", objeto.nombre_conductor);
                        cmd.Parameters.AddWithValue("@rut", objeto.rut);
                        cmd.Parameters.AddWithValue("@patente_camion", objeto.patente_camion);
                        cmd.Parameters.AddWithValue("@patente_rampla", objeto.patente_rampla);
                        cmd.Parameters.AddWithValue("@fecha_aprobacion", objeto.fecha_aprobacion);
                        cmd.Parameters.AddWithValue("@usuario_aprobacion", objeto.usuario_aprobacion);
                        cmd.Parameters.AddWithValue("@observacion_aprobacion", objeto.observacion_aprobacion);
                        cmd.Parameters.AddWithValue("@nombre_inspeccion", objeto.nombre_inspeccion);
                        cmd.Parameters.AddWithValue("@nombre_Proveedor", objeto.nombre_Proveedor);
                        cmd.Parameters.AddWithValue("@aux1", objeto.aux1);
                        cmd.Parameters.AddWithValue("@aux2", objeto.aux2);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_CHECKLIST_COMPLETADO);
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

        public static void DELETE(ref OBJ_CHECKLISTS_COMPLETADOS objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_CHECKLIST_COMPLETADO);
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

        public static void LLENAOBJETO(ref OBJ_CHECKLISTS_COMPLETADOS objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_CHECKLIST_COMPLETADO);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        objeto.id_checklist = int.Parse(reader["id_checklist"].ToString());
                        objeto.fecha = DateTime.Parse(reader["fecha"].ToString());
                        objeto.usuario = reader["usuario"].ToString();
                        objeto.observacion = reader["observacion"].ToString();
                        objeto.estado = reader["estado"].ToString();
                        objeto.nombre_conductor = reader["nombre_conductor"].ToString();
                        objeto.rut = reader["rut"].ToString();
                        objeto.patente_camion = reader["patente_camion"].ToString();
                        objeto.patente_rampla = reader["patente_rampla"].ToString();
                        objeto.fecha_aprobacion = DateTime.Parse(reader["fecha_aprobacion"].ToString());
                        objeto.usuario_aprobacion = reader["usuario_aprobacion"].ToString();
                        objeto.observacion_aprobacion = reader["observacion_aprobacion"].ToString();
                        objeto.nombre_inspeccion = reader["nombre_inspeccion"].ToString();
                        objeto.nombre_Proveedor = reader["nombre_Proveedor"].ToString();
                        objeto.aux1 = reader["aux1"].ToString();
                        objeto.aux2 = reader["aux2"].ToString();
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

        public static void PREPARAOBJETO(ref OBJ_CHECKLISTS_COMPLETADOS objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.id_checklist = 0;
                objeto.fecha = DateTime.Parse("01/01/1900");
                objeto.usuario = " ";
                objeto.observacion = " ";
                objeto.estado = " ";
                objeto.nombre_conductor = " ";
                objeto.rut = " ";
                objeto.patente_camion = " ";
                objeto.patente_rampla = " ";
                objeto.fecha_aprobacion = DateTime.Parse("01/01/1900");
                objeto.usuario_aprobacion = " ";
                objeto.observacion_aprobacion = " ";
                objeto.nombre_inspeccion = " ";
                objeto.nombre_Proveedor = " ";
                objeto.aux1 = " ";
                objeto.aux2 = " ";
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
