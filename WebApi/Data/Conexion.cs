 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace WebApi.Data
{
    public class Conexion
    {
        public static string rutaConexion
        {
            get
            {
                var environmentConnection = System.Environment.GetEnvironmentVariable("NEXO_DB_CONNECTION");
                if (!string.IsNullOrWhiteSpace(environmentConnection)) return environmentConnection;

                var connection = ConfigurationManager.ConnectionStrings["InventarioDb"];
                if (connection == null || string.IsNullOrWhiteSpace(connection.ConnectionString))
                {
                    throw new ConfigurationErrorsException(
                        "Configure la cadena de conexion 'InventarioDb' en Web.config.");
                }

                return connection.ConnectionString;
            }
        }
    }
}
