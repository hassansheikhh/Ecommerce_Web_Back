using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ecommerce_Web_App.Models;

namespace Ecommerce_Web_App.ViewModel
{
    public class ProductViewModel
    {
        public bool IsSuccess { get; set; }
        public string ResponseMessage { get; set; }
        public int PkCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDetail { get; set; }
        public bool IsActive { get; set; }
        public List<ProductViewModel> SubCategories { get; set; }
        public string ProductName { get; set; }
        public string ArticleNo { get; set; }
        public string ProductColor { get; set; }
        public int FkCategoryId { get; set; }
        public string ProductDetail { get; set; }
        public string ProductSize { get; set; }
        public int BuyingPrice { get; set; }
        public int SellingPrice { get; set; }
        public int CreatedBy { get; set; }

        //public List<HttpPostedFileBase> Images { get; set; }
        public List<ProductImages> Images { get; set; }
    }
}