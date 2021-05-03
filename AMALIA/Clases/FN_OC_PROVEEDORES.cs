using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AMALIAFW
{
    public class OBJ_OC_PROVEEDORES
    {
        // PK AUTONUMERADA & PROPIOS DE LA TABLA
        public int ID_OC_PROVEEDOR { get; set; }

        public string rut { get; set; }
        public string razon_social { get; set; }
        public string nombre_corto { get; set; }
        public string direccion { get; set; }
        public string comuna { get; set; }
        public string ciudad { get; set; }
        public string contacto { get; set; }
        public string fono { get; set; }
        public string banco { get; set; }
        public string tipo_cuenta { get; set; }
        public string num_cuenta { get; set; }
        public string rut_cuenta { get; set; }
        public string email { get; set; }
        public string activo { get; set; }
        public bool _respok { get; set; }
        public string _respdet { get; set; }
    }


    public static class FN_OC_PROVEEDORES
    {
        public static string connex = ConfigurationManager.ConnectionStrings["default"].ToString();

        // CAMBIAR AQUI //
        // **************************************** **************************************** **************************************** //       
        private static string nombre_tabla = "OC_PROVEEDORES";
        private static string nombre_llave = "ID_OC_PROVEEDOR";
        private static string value_insert = "rut, razon_social, nombre_corto, direccion, comuna, ciudad, contacto, fono, banco, tipo_cuenta, num_cuenta, rut_cuenta, email, activo";
        private static string value_insert2 = "@rut, @razon_social, @nombre_corto, @direccion, @comuna, @ciudad, @contacto, @fono, @banco, @tipo_cuenta, @num_cuenta, @rut_cuenta, @email, @activo";
        private static string value_update = "rut = @rut, " +
        "razon_social = @razon_social, " +
        "nombre_corto = @nombre_corto, " +
        "direccion = @direccion, " +
        "comuna = @comuna, " +
        "ciudad = @ciudad, " +
        "contacto = @contacto, " +
        "fono = @fono, " +
        "banco = @banco, " +
        "tipo_cuenta = @tipo_cuenta, " +
        "num_cuenta = @num_cuenta, " +
        "rut_cuenta = @rut_cuenta, " +
        "email = @email, " +
        "activo = @activo " +
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

        public static void INSERT(ref OBJ_OC_PROVEEDORES objeto)
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
                        cmd.Parameters.AddWithValue("@rut", objeto.rut);
                        cmd.Parameters.AddWithValue("@razon_social", objeto.razon_social);
                        cmd.Parameters.AddWithValue("@nombre_corto", objeto.nombre_corto);
                        cmd.Parameters.AddWithValue("@direccion", objeto.direccion);
                        cmd.Parameters.AddWithValue("@comuna", objeto.comuna);
                        cmd.Parameters.AddWithValue("@ciudad", objeto.ciudad);
                        cmd.Parameters.AddWithValue("@contacto", objeto.contacto);
                        cmd.Parameters.AddWithValue("@fono", objeto.fono);
                        cmd.Parameters.AddWithValue("@banco", objeto.banco);
                        cmd.Parameters.AddWithValue("@tipo_cuenta", objeto.tipo_cuenta);
                        cmd.Parameters.AddWithValue("@num_cuenta", objeto.num_cuenta);
                        cmd.Parameters.AddWithValue("@rut_cuenta", objeto.rut_cuenta);
                        cmd.Parameters.AddWithValue("@email", objeto.email);
                        cmd.Parameters.AddWithValue("@activo", objeto.activo);

                        // **************************************** **************************************** **************************************** //  
                        int scope = Convert.ToInt32(cmd.ExecuteScalar());
                        // AQUI TAMBIEN
                        objeto.ID_OC_PROVEEDOR = scope;

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

        public static void UPDATE(ref OBJ_OC_PROVEEDORES objeto)
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
                        cmd.Parameters.AddWithValue("@rut", objeto.rut);
                        cmd.Parameters.AddWithValue("@razon_social", objeto.razon_social);
                        cmd.Parameters.AddWithValue("@nombre_corto", objeto.nombre_corto);
                        cmd.Parameters.AddWithValue("@direccion", objeto.direccion);
                        cmd.Parameters.AddWithValue("@comuna", objeto.comuna);
                        cmd.Parameters.AddWithValue("@ciudad", objeto.ciudad);
                        cmd.Parameters.AddWithValue("@contacto", objeto.contacto);
                        cmd.Parameters.AddWithValue("@fono", objeto.fono);
                        cmd.Parameters.AddWithValue("@banco", objeto.banco);
                        cmd.Parameters.AddWithValue("@tipo_cuenta", objeto.tipo_cuenta);
                        cmd.Parameters.AddWithValue("@num_cuenta", objeto.num_cuenta);
                        cmd.Parameters.AddWithValue("@rut_cuenta", objeto.rut_cuenta);
                        cmd.Parameters.AddWithValue("@email", objeto.email);
                        cmd.Parameters.AddWithValue("@activo", objeto.activo);
                        // LLAVE PARA EL UPDATE
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_OC_PROVEEDOR);
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

        public static void DELETE(ref OBJ_OC_PROVEEDORES objeto)
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
                        cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_OC_PROVEEDOR);
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

        public static void LLENAOBJETO(ref OBJ_OC_PROVEEDORES objeto)
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
                    cmd.Parameters.AddWithValue("@" + nombre_llave, objeto.ID_OC_PROVEEDOR);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // patente, num_chasis, num_motor, marca, ano, carga, activo
                        objeto.rut = reader["rut"].ToString();
                        objeto.razon_social = reader["razon_social"].ToString();
                        objeto.nombre_corto = reader["nombre_corto"].ToString();
                        objeto.direccion = reader["direccion"].ToString();
                        objeto.comuna = reader["comuna"].ToString();
                        objeto.ciudad = reader["ciudad"].ToString();
                        objeto.contacto = reader["contacto"].ToString();
                        objeto.fono = reader["fono"].ToString();
                        objeto.banco = reader["banco"].ToString();
                        objeto.tipo_cuenta = reader["tipo_cuenta"].ToString();
                        objeto.num_cuenta = reader["num_cuenta"].ToString();
                        objeto.rut_cuenta = reader["rut_cuenta"].ToString();
                        objeto.email = reader["email"].ToString();
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

        public static void PREPARAOBJETO(ref OBJ_OC_PROVEEDORES objeto)
        {
            try
            {
                // **************************************** **************************************** **************************************** //  
                objeto.rut = " ";
                objeto.razon_social = " ";
                objeto.nombre_corto = " ";
                objeto.direccion = " ";
                objeto.comuna = " ";
                objeto.ciudad = " ";
                objeto.contacto = " ";
                objeto.fono = " ";
                objeto.banco = " ";
                objeto.tipo_cuenta = " ";
                objeto.num_cuenta = " ";
                objeto.rut_cuenta = " ";
                objeto.email = " ";
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
