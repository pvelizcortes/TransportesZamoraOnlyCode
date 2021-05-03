using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_CHECKLISTS_RESPUESTAS
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_RESPUESTA { get; set; }

        public int id_checklist_completado { get; set; }
        public int id_pregunta { get; set; }
        public string respuesta { get; set; }
        public string foto { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_CHECKLISTS_RESPUESTAS
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "CHECKLISTS_RESPUESTAS";
        private static string nombre_llave = "ID_RESPUESTA";
        private static string value_insert = "id_checklist_completado, id_pregunta, respuesta, foto";
        private static string value_insert2 = "@id_checklist_completado, @id_pregunta, @respuesta, @foto";
        private static string value_update = "id_checklist_completado = @id_checklist_completado, " +
        "id_pregunta = @id_pregunta, " +
        "respuesta = @respuesta, " +
        "foto = @foto " +
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

        public static void INSERT(ref OBJ_CHECKLISTS_RESPUESTAS objeto)
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
                        cmd.Parameters.AddWithValue("@id_checklist_completado", objeto.id_checklist_completado);
                        cmd.Parameters.AddWithValue("@id_pregunta", objeto.id_pregunta);
                        cmd.Parameters.AddWithValue("@respuesta", objeto.respuesta);
                        cmd.Parameters.AddWithValue("@foto", objeto.foto);


                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_RESPUESTA = scope;

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

        public static Boolean INSERTLIST(List<OBJ_CHECKLISTS_RESPUESTAS> objetos)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connex))
                {
                    conn.Open();
                    string sql = @" insert into " + nombre_tabla +
                            " ( " + value_insert + ") values" +
                            " (" + value_insert2 + "); ";
                    foreach (OBJ_CHECKLISTS_RESPUESTAS objeto in objetos)
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            // CAMBIAR AQUI //
                            // **************************************** **************************************** **************************************** // 
                            cmd.Parameters.AddWithValue("@id_checklist_completado", objeto.id_checklist_completado);
                            cmd.Parameters.AddWithValue("@id_pregunta", objeto.id_pregunta);
                            cmd.Parameters.AddWithValue("@respuesta", objeto.respuesta);
                            cmd.Parameters.AddWithValue("@foto", objeto.foto);
                            // **************************************** **************************************** **************************************** //  
                            cmd.ExecuteScalar();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void UPDATE(ref OBJ_CHECKLISTS_RESPUESTAS objeto)
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
                        cmd.Parameters.AddWithValue("@id_checklist_completado", objeto.id_checklist_completado);
                        cmd.Parameters.AddWithValue("@id_pregunta", objeto.id_pregunta);
                        cmd.Parameters.AddWithValue("@respuesta", objeto.respuesta);
                        cmd.Parameters.AddWithValue("@foto", objeto.foto);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_RESPUESTA);
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

        public static void DELETE(ref OBJ_CHECKLISTS_RESPUESTAS objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_RESPUESTA);
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

        public static void LLENAOBJETO(ref OBJ_CHECKLISTS_RESPUESTAS objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_RESPUESTA);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        objeto.id_checklist_completado = int.Parse(reader["id_checklist_completado"].ToString());
                        objeto.id_pregunta = int.Parse(reader["id_pregunta"].ToString());
                        objeto.respuesta = reader["respuesta"].ToString();
                        objeto.foto = reader["foto"].ToString();
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

        public static void PREPARAOBJETO(ref OBJ_CHECKLISTS_RESPUESTAS objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.id_checklist_completado = 0;
                objeto.id_pregunta = 0;
                objeto.respuesta = " ";
                objeto.foto = " ";
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
