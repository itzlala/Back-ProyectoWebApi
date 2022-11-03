using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Data
{
    public class CuentaData
    {
        public static bool RegistrarCue(Cuentas oCuenta)
        {
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("usp_registrarCue", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usuario", oCuenta.Usuario);
                cmd.Parameters.AddWithValue("@contrasenia", oCuenta.Contrasenia);

                try
                {
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static bool ModificarCue(Cuentas oCuenta)
        {
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("usp_modificarCue", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcuenta", oCuenta.IdCuenta);
                cmd.Parameters.AddWithValue("@usuario", oCuenta.Usuario);
                cmd.Parameters.AddWithValue("@contrasenia", oCuenta.Contrasenia);

                try
                {
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static List<Cuentas> ListarCue()
        {
            List<Cuentas> oListaCuenta = new List<Cuentas>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("usp_listarCue", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            oListaCuenta.Add(new Cuentas()
                            {
                                IdCuenta = Convert.ToInt32(dr["IdCuenta"]),
                                Usuario = dr["Usuario"].ToString(),
                                Contrasenia = dr["Contrasenia"].ToString(),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString())
                            });
                        }

                    }



                    return oListaCuenta;
                }
                catch (Exception)
                {
                    return oListaCuenta;
                }
            }
        }

        public static Cuentas ObtenerCue(int idcuenta)
        {
            Cuentas oCuenta = new Cuentas();
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("usp_obtenerCue", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcuenta", idcuenta);

                try
                {
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            oCuenta = new Cuentas()
                            {
                                IdCuenta = Convert.ToInt32(dr["IdCuenta"]),
                                Usuario = dr["Usuario"].ToString(),
                                Contrasenia = dr["Contrasenia"].ToString(),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString())
                            };
                        }

                    }



                    return oCuenta;
                }
                catch (Exception)
                {
                    return oCuenta;
                }
            }
        }

        public static bool EliminarCue(int id)
        {
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("usp_eliminarCue", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcuenta", id);

                try
                {
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}