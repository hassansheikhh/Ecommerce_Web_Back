
using System.Data.Entity;
using Ecommerce_Web_API.Models;
using Ecommerce_Web_API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Remoting;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Ecommerce_Web_API.APIController
{
    [RoutePrefix("api/user")]
    public class UserApiController : ApiController
    {

        [HttpPost]
        [Route("SignUp")]
        public async Task<JsonResult<object>> SignUp(UserViewModel model)
        {
            object resp;
            try
            {
                var task = await Task.Run(() => model = new Models.User().SignUp(model));
                if (model.IsSuccess)
                {
                    resp = new
                    {
                        Status = HttpStatusCode.OK,
                        Message = "SignUp Successfully Go To The Login Page",
                      
                    };
                }
                else
                {
                    resp = new
                    {
                        Status = HttpStatusCode.NoContent,
                        Message = model.ResponseMessage,
                    };
                }

            }
            catch (Exception ex)
            {
                resp = new
                {
                    Status = HttpStatusCode.InternalServerError,
                    Message = ex.Message.ToString(),
                    ExceptionMessage = ex.InnerException,

                };
            }

            return Json(resp);
        }


        [HttpPost]
        [Route("Login")]
        public async Task<JsonResult<object>> Login(UserViewModel model)
        {
            object resp;
            try
            {
                var task = await Task.Run(() => model = new Models.User().Login(model));
                if (model.IsSuccess)
                {
                    resp = new
                    {
                        Status = HttpStatusCode.OK,
                        Message = model.ResponseMessage,
                        Data = model,
                    };
                }
                else
                {
                    resp = new
                    {
                        Status = HttpStatusCode.NoContent,
                        Message = model.ResponseMessage,
                    };
                }
            }
            catch (Exception ex)
            {
                resp = new
                {
                    Status = HttpStatusCode.InternalServerError,
                    Message = "Internal Server Error!",
                    Exception = ex.Message,
                };
            }
            return Json(resp);
        }
    }
}
