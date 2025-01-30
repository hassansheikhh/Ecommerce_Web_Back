using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ecommerce_Web_App.EntityFramework;
using Ecommerce_Web_App.ViewModel;

namespace Ecommerce_Web_App.Models
{
    public class Product
    {
        public Ecommerce_Web_DBEntities db = new Ecommerce_Web_DBEntities();

        public List<ProductViewModel> GetAllCategories()
        {
            List<ProductViewModel> model = new List<ProductViewModel>();

            try
            {
                model = db.Categories
                    .Where(x => x.MasterId == null)
                    .AsEnumerable()
                    .Select(x => new ProductViewModel
                    {
                        PkCategoryId = x.PK_CategoryId,
                        CategoryDetail = x.CategoryDetail,
                        CategoryName = x.CategoryName,
                        SubCategories = db.Categories
                            .Where(sub => sub.MasterId == x.PK_CategoryId)
                            .AsEnumerable()
                            .Select(sub => new ProductViewModel
                            {
                                PkCategoryId = sub.PK_CategoryId,
                                CategoryName = sub.CategoryName,
                                CategoryDetail = sub.CategoryDetail,
                            }).ToList() ?? new List<ProductViewModel>() 
                    }).ToList();
            }
            catch (Exception ex)
            {
                model = new List<ProductViewModel>
        {
            new ProductViewModel { ResponseMessage = ex.Message }
        };
            }

            return model;
        }

        //public ProductViewModel AddProduct(ProductViewModel model)
        //{
        //    try
        //    {
        //        model = db.
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = ex.Message;
        //        model.ResponseMessage = message;

        //    }
        //    return model;
        //}
    }
}

