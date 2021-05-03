using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_USUARIOS
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_USUARIO { get; set; }
        
        public int id_perfil { get; set; }
        public string nombre_completo { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string usuario { get; set; }
        public string pass { get; set; }
        public string activo { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_USUARIOS
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "usuarios";
        private static string nombre_llave = "id_usuario";
        private static string value_insert = " id_perfil, nombre_completo, correo, telefono, direccion, usuario, pass, activo ";
        private static string value_insert2 = " @id_perfil, @nombre_completo, @correo, @telefono, @direccion, @usuario, @pass, @activo ";
        private static string value_update =
        " id_perfil = @id_perfil, " +
        " nombre_completo = @nombre_completo, " +
        " correo = @correo, " +
        " telefono = @telefono, " +
        " direccion = @direccion, " +
        //" usuario = @usuario, " +
        //" pass = @pass, " +
        " activo = @activo " + // <----------- EL ULTIMO SIN COMA PLS <3

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

        public static void INSERT(ref OBJ_USUARIOS objeto)
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
                        // id_perfil, nombre_completo, correo, telefono, direccion, usuario, pass, activo
                        cmd.Parameters.AddWithValue("@id_perfil", objeto.id_perfil);
                        cmd.Parameters.AddWithValue("@nombre_completo", objeto.nombre_completo);
                        cmd.Parameters.AddWithValue("@correo", objeto.correo);
                        cmd.Parameters.AddWithValue("@telefono", objeto.telefono);
                        cmd.Parameters.AddWithValue("@direccion", objeto.direccion);
                        cmd.Parameters.AddWithValue("@usuario", objeto.usuario);
                        cmd.Parameters.AddWithValue("@pass", objeto.pass);
                        cmd.Parameters.AddWithValue("@activo", objeto.activo);
                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_USUARIO = scope;

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

        public static void UPDATE(ref OBJ_USUARIOS objeto)
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
                        cmd.Parameters.AddWithValue("@id_perfil", objeto.id_perfil);
                        cmd.Parameters.AddWithValue("@nombre_completo", objeto.nombre_completo);
                        cmd.Parameters.AddWithValue("@correo", objeto.correo);
                        cmd.Parameters.AddWithValue("@telefono", objeto.telefono);
                        cmd.Parameters.AddWithValue("@direccion", objeto.direccion);
                        //cmd.Parameters.AddWithValue("@usuario", objeto.usuario);
                        //cmd.Parameters.AddWithValue("@pass", objeto.pass);
                        cmd.Parameters.AddWithValue("@activo", objeto.activo);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_USUARIO);
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

        public static void DELETE(ref OBJ_USUARIOS objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_USUARIO);
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

        public static void LLENAOBJETO(ref OBJ_USUARIOS objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_USUARIO);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // id_perfil, nombre_completo, correo, telefono, direccion, usuario, pass, activo
                        objeto.id_perfil = int.Parse(reader["id_perfil"].ToString());
                        objeto.nombre_completo = reader["nombre_completo"].ToString();
                        objeto.correo = reader["correo"].ToString();
                        objeto.telefono = reader["telefono"].ToString();
                        objeto.direccion = reader["direccion"].ToString();
                        objeto.usuario = reader["usuario"].ToString();
                        objeto.pass = reader["pass"].ToString();
                        objeto.activo =reader["activo"].ToString();
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

        public static void PREPARAOBJETO(ref OBJ_USUARIOS objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.id_perfil = 1;
                objeto.nombre_completo = "no";
                objeto.correo = "no";
                objeto.telefono = "no";
                objeto.direccion = "no";
                objeto.usuario = "no";
                objeto.pass = "1234";
                objeto.activo = "ACTIVO";
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
        public static void LOGIN(ref OBJ_USUARIOS objeto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connex))
                {
                    conn.Open();
                    string sql = @"SELECT * from " + nombre_tabla + " where usuario = @usuario and pass = @pass and activo = 'ACTIVO' ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    // CAMBIAR AQUI //
                    // **************************************** **************************************** **************************************** //   
                    cmd.Parameters.AddWithValue("@usuario", objeto.usuario);
                    cmd.Parameters.AddWithValue("@pass", objeto.pass);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        objeto.ID_USUARIO = int.Parse(reader["ID_USUARIO"].ToString());
                        objeto.id_perfil = int.Parse(reader["id_perfil"].ToString());
                        objeto.nombre_completo = reader["nombre_completo"].ToString();
                        objeto.correo = reader["correo"].ToString();
                        objeto.telefono = reader["telefono"].ToString();
                        objeto.direccion = reader["direccion"].ToString();
                        objeto.usuario = reader["usuario"].ToString();
                        objeto.pass = reader["pass"].ToString();
                        objeto.activo = reader["activo"].ToString();
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

        public static void CambiarContrasena(ref OBJ_USUARIOS objeto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connex))
                {
                    conn.Open();
                    string sql = @"update " + nombre_tabla + " set pass = @pass, usuario = @usuario where id_usuario = @id_usuario ";
                  
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // CAMBIAR AQUI //
                        // **************************************** **************************************** **************************************** //     
                        cmd.Parameters.AddWithValue("@usuario", objeto.usuario);
                        cmd.Parameters.AddWithValue("@pass", objeto.pass);                       
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@id_usuario", objeto.ID_USUARIO);
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

        ///////////////////////////// PROPIOS DEL MODULO / CLASE //////////////////////////////
        public static void BUSCARCONUSUARIO(ref OBJ_USUARIOS objeto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connex))
                {
                    conn.Open();
                    string sql = @"SELECT * from " + nombre_tabla + " where usuario = @usuario ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    // CAMBIAR AQUI //
                    // **************************************** **************************************** **************************************** //   
                    cmd.Parameters.AddWithValue("@usuario", objeto.usuario);             

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        objeto.ID_USUARIO = int.Parse(reader["ID_USUARIO"].ToString());
                        objeto.id_perfil = int.Parse(reader["id_perfil"].ToString());
                        objeto.nombre_completo = reader["nombre_completo"].ToString();
                        objeto.correo = reader["correo"].ToString();
                        objeto.telefono = reader["telefono"].ToString();
                        objeto.direccion = reader["direccion"].ToString();
                        objeto.usuario = reader["usuario"].ToString();
                        objeto.pass = reader["pass"].ToString();
                        objeto.activo = reader["activo"].ToString();
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
