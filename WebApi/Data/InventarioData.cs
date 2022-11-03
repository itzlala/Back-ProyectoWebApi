using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Data
{
    public class InventarioData
    {
        public static bool Registrar(Inventario oInventario)
        {
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("usp_registrarinv", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@folio", oInventario.Folio);
                cmd.Parameters.AddWithValue("@tipo", oInventario.Tipo);
                cmd.Parameters.AddWithValue("@estatus", oInventario.Estatus);
                cmd.Parameters.AddWithValue("@descfis", oInventario.DescFis);
                cmd.Parameters.AddWithValue("@desctec", oInventario.DescTec);
                cmd.Parameters.AddWithValue("@marca", oInventario.Marca);
                cmd.Parameters.AddWithValue("@modelo", oInventario.Modelo);
                cmd.Parameters.AddWithValue("@nomprod", oInventario.NomProd);
                cmd.Parameters.AddWithValue("@nserie", oInventario.Nserie);
                cmd.Parameters.AddWithValue("@costo", oInventario.Costo);
                cmd.Parameters.AddWithValue("@lugar", oInventario.Lugar);
                cmd.Parameters.AddWithValue("@asignacion", oInventario.Asignacion);
                cmd.Parameters.AddWithValue("@observaciones", oInventario.Observaciones);

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

        public static bool Modificar(Inventario oInventario)
        {
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("usp_modificarinv", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idinventario", oInventario.IdInventario);
                cmd.Parameters.AddWithValue("@folio", oInventario.Folio);
                cmd.Parameters.AddWithValue("@tipo", oInventario.Tipo);
                cmd.Parameters.AddWithValue("@estatus", oInventario.Estatus);
                cmd.Parameters.AddWithValue("@descfis", oInventario.DescFis);
                cmd.Parameters.AddWithValue("@desctec", oInventario.DescTec);
                cmd.Parameters.AddWithValue("@marca", oInventario.Marca);
                cmd.Parameters.AddWithValue("@modelo", oInventario.Modelo);
                cmd.Parameters.AddWithValue("@nomprod", oInventario.NomProd);
                cmd.Parameters.AddWithValue("@nserie", oInventario.Nserie);
                cmd.Parameters.AddWithValue("@costo", oInventario.Costo);
                cmd.Parameters.AddWithValue("@lugar", oInventario.Lugar);
                cmd.Parameters.AddWithValue("@asignacion", oInventario.Asignacion);
                cmd.Parameters.AddWithValue("@observaciones", oInventario.Observaciones);

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

        public static List<Inventario> Listar()
        {
            List<Inventario> oListaInventario = new List<Inventario>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("usp_listarinv", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            oListaInventario.Add(new Inventario()
                            {
                                IdInventario = Convert.ToInt32(dr["IdInventario"]),
                                Folio = dr["Folio"].ToString(),
                                Tipo = dr["Tipo"].ToString(),
                                Estatus = dr["Estatus"].ToString(),
                                DescFis = dr["DescFis"].ToString(),
                                DescTec = dr["DescTec"].ToString(),
                                Marca = dr["Marca"].ToString(),
                                Modelo = dr["Modelo"].ToString(),
                                NomProd = dr["NomProd"].ToString(),
                                Nserie = dr["Nserie"].ToString(),
                                Costo = dr["Costo"].ToString(),
                                Lugar = dr["Lugar"].ToString(),
                                Asignacion = dr["Asignacion"].ToString(),
                                Observaciones = dr["Observaciones"].ToString(),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString())
                            });
                        }

                    }



                    return oListaInventario;
                }
                catch (Exception)
                {
                    return oListaInventario;
                }
            }
        }

        public static Inventario Obtener(int idinventario)
        {
            Inventario oInventario = new Inventario();
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("usp_obtenerinv", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idinventario", idinventario);

                try
                {
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            oInventario = new Inventario()
                            {
                                IdInventario = Convert.ToInt32(dr["IdInventario"]),
                                Folio = dr["Folio"].ToString(),
                                Tipo = dr["Tipo"].ToString(),
                                Estatus = dr["Estatus"].ToString(),
                                DescFis = dr["DescFis"].ToString(),
                                DescTec = dr["DescTec"].ToString(),
                                Marca = dr["Marca"].ToString(),
                                Modelo = dr["Modelo"].ToString(),
                                NomProd = dr["NomProd"].ToString(),
                                Nserie = dr["Nserie"].ToString(),
                                Costo = dr["Costo"].ToString(),
                                Lugar = dr["Lugar"].ToString(),
                                Asignacion = dr["Asignacion"].ToString(),
                                Observaciones = dr["Observaciones"].ToString(),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString())
                            };
                        }

                    }



                    return oInventario;
                }
                catch (Exception)
                {
                    return oInventario;
                }
            }
        }

        public static bool Eliminar(int id)
        {
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("usp_eliminarinv", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idinventario", id);

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