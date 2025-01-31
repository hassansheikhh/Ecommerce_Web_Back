using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce_Web_App.Models
{
    public class ProductImages
    {
        public byte[] ImageData { get; set; }  // Image ko byte[] format mai store karenge
        public string ContentType { get; set; }  // Image ka content type (jpg, png, etc.)

    }
}