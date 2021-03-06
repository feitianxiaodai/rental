﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.UI.Models
{
    public class UploadResponeModel
    {

        public UploadResponeModel()
        {
            fils = new List<UploadFileInfo>();
        }
        public List<UploadFileInfo> fils { get; set; }
    }

    public class UploadFileInfo
    {
        public string url { get; set; }
        public string name { get; set; }

        public string type { get; set; }

        public int size { get; set; }
    }
}