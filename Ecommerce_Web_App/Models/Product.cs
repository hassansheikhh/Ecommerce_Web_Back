using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Ecommerce_Web_App.EntityFramework;
using Ecommerce_Web_App.ViewModel;
using System.Configuration;

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

        public ProductViewModel AddProduct(ProductViewModel model, List<ProductImages> images)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Ecommerce_Web_DBEntities"].ConnectionString;
                using (SqlConnection conn = new SqlConnection("connectionString"))
                {
                    SqlCommand cmd = new SqlCommand("SP_AddProduct", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ✅ Add Product Parameters
                    cmd.Parameters.AddWithValue("@PRODUCTNAME", model.ProductName);
                    cmd.Parameters.AddWithValue("@ARTICALNO", model.ArticleNo);
                    cmd.Parameters.AddWithValue("@PRODUCTCOLOR", model.ProductColor);
                    cmd.Parameters.AddWithValue("@FK_CATEGORY_ID", model.FkCategoryId);
                    cmd.Parameters.AddWithValue("@PRODUCTDETAIL", model.ProductDetail);
                    cmd.Parameters.AddWithValue("@PRODUCTSIZE", model.ProductSize);
                    cmd.Parameters.AddWithValue("@BUYINGPRICE", model.BuyingPrice);
                    cmd.Parameters.AddWithValue("@SELLINGPRICE", model.SellingPrice);
                    cmd.Parameters.AddWithValue("@CREATEDBY", model.CreatedBy);

                    // ✅ Image Table (TVP)
                    DataTable dtImages = new DataTable();
                    dtImages.Columns.Add("ImageData", typeof(byte[]));
                    dtImages.Columns.Add("ContentType", typeof(string));

                    foreach (var image in images)
                    {
                        dtImages.Rows.Add(image.ImageData, image.ContentType);
                    }

                    // ✅ Pass Image Table as Parameter
                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@Images", dtImages);
                    tvpParam.SqlDbType = SqlDbType.Structured;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                model.ResponseMessage = "Product Added Successfully";
            }
            catch (Exception ex)
            {
                model.ResponseMessage = ex.Message;
            }

            return model;
        }
    }
}

