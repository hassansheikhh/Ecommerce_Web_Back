using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public string SubCategory { get; set; }
        public string SubCategoryName { get; set; }
        public List<ProductViewModel> SubCategories { get; set; }
    }
}