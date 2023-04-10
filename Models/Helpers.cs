using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VotoElectronico.Models
{
    public class Helpers
    {
        public static string GetConnectionString()
        {
            string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["VotoElectronicoEntities3"].ConnectionString;
            return conexion;
        }
        
    }
}