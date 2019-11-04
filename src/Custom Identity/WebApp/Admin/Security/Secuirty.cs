using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApp.Admin.Security
{
    /*<summary>
    A collection of application-wide settings that provide values for secuirity roles which have been set in the web.config's
    <appSettings /> section.
    </summary>   
    */
    public class Secuirty
    {
        public static string AdminRole => ConfigurationManager.AppSettings["adminRole"];
        /*
         The above is the same as typing:
         public static string adminrole
         {
            get{return ConfigurationManager.AppSettings["adminRole"];
         }
         */
        public static string UserRole => ConfigurationManager.AppSettings["userRole"];
    }
}