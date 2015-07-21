using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.UI.Utility
{
    public static class DataHelper
    {
        public static string MD5(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
        }
    }
}