using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.UI.Models
{
    public class AjaxResponseModel
    {
        public bool Status { get; set; }

        public string Msg { get; set; }

        public string Type { get; set; }
    }
}