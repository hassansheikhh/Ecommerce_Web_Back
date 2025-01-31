using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Ecommerce_Web_App.EntityFramework;
using Ecommerce_Web_App.Models;
using Ecommerce_Web_App.ViewModel;
using Newtonsoft.Json;

namespace Ecommerce_Web_App.APIController
{
    [RoutePrefix("api/product")]
    public class ProductAPIController : ApiController
    {
        [HttpGet]
        [Route("GetCategory")]

        public async Task<JsonResult<object>> GetAllCategories()
        {
            object resp;
            List<ProductViewModel> model = new List<ProductViewModel>();

            try
            {
                var task = await Task.Run(() => model = new Models.Product().GetAllCategories());
                if (model!=null)
                {
                    resp = new
                    {
                        Status = HttpStatusCode.OK,
                        Message = "Success",
                        Data = model ,
                    };
                }
                else
                {
                    resp = new
                    {
                        Status = HttpStatusCode.NoContent,
                        Message = "No categories found",
                    };
                }
            }
            catch (Exception ex)
            {

                resp = new
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            return Json(resp);
        }


        //[HttpPost]
        //[Route("AddProduct")]
        //public async Task<JsonResult<object>> AddProduct(ProductViewModel model)
        //{
        //    object resp;
        //    try
        //    {
        //        var task = await Task.Run(() => model = new Models.Product().AddProduct(model));
        //        if (model.IsSuccess)
        //        {
        //            resp = new
        //            {
        //                Status = HttpStatusCode.OK,
        //                Message = "Success",
        //            };
        //        }
        //        else
        //        {
        //            resp = new
        //            {
        //                Status = HttpStatusCode.NoContent,
        //                Message = model.ResponseMessage,
        //            };
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        resp = new
        //        {
        //            Status = HttpStatusCode.BadRequest,
        //            Message = ex.Message,
        //        };
        //    }
        //    return Json(resp);
        //}



        [HttpPost]
        [Route("AddProduct")]
        public async Task<JsonResult<object>> AddProduct()
        {
            object resp;
            try
            {
                var httpRequest = HttpContext.Current.Request;

                // JSON Data ko parse karne ke liye
                ProductViewModel model = new ProductViewModel();
                if (!string.IsNullOrEmpty(httpRequest.Form["ProductData"]))
                {
                    model = JsonConvert.DeserializeObject<ProductViewModel>(httpRequest.Form["ProductData"]);
                }

                List<ProductImages> imageList = new List<ProductImages>();

                // Multiple images ko process karne ke liye
                if (httpRequest.Files.Count > 0)
                {
                    foreach (string fileName in httpRequest.Files)
                    {
                        HttpPostedFile file = httpRequest.Files[fileName];
                        if (file != null && file.ContentLength > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.InputStream.CopyTo(ms);
                                imageList.Add(new ProductImages
                                {
                                    ImageData = ms.ToArray(),
                                    ContentType = file.ContentType
                                });
                            }
                        }
                    }
                }

                // Call stored procedure to save product and images
                var task = await Task.Run(() => model = new Models.Product().AddProduct(model, imageList));

                if (model.IsSuccess)
                {
                    resp = new
                    {
                        Status = HttpStatusCode.OK,
                        Message = "Success",
                    };
                }
                else
                {
                    resp = new
                    {
                        Status = HttpStatusCode.NoContent,
                        Message = model.ResponseMessage,
                    };
                };
            }
            catch (Exception ex)
            {
                resp = new
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            return Json(resp);
        }

    }
}
