using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WebApi.Models;
using System.Text;

namespace WebApi.Data
{
    public class CuentaData
    {
        public static Cuentas Autenticar(string usuario, string contrasenia)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrEmpty(contrasenia))
            {
                return null;
            }

            var cuenta = ListarCue().FirstOrDefault(item =>
                string.Equals(item.Usuario, usuario.Trim(), StringComparison.OrdinalIgnoreCase));

            return cuenta != null && ComparacionSegura(cuenta.Contrasenia, contrasenia)
                ? cuenta
                : null;
        }

        private static bool ComparacionSegura(string valorGuardado, string valorRecibido)
        {
            var izquierda = Encoding.UTF8.GetBytes(valorGuardado ?? string.Empty);
            var derecha = Encoding.UTF8.GetBytes(valorRecibido ?? string.Empty);
            if (izquierda.Length != derecha.Length) return false;

            var diferencia = 0;
            for (var indice = 0; indice < izquierda.Length; indice++)
            {
                diferencia |= izquierda[indice] ^ derecha[indice];
            }

            return diferencia == 0;
        }

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
