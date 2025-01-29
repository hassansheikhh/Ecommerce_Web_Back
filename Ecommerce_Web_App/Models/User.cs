using Ecommerce_Web_API.Common;
using Ecommerce_Web_API.ViewModel;
using Ecommerce_Web_App.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce_Web_API.Models
{
    public class User

    {
        public Ecommerce_Web_DBEntities db = new Ecommerce_Web_DBEntities();
        public UserViewModel SignUp(UserViewModel model)
        {
            try
            {
                model = db.SP_SignUp(model.UserName,model.FullName, model.Email, CommonUser.GetMd5Hash(model.Password, model.UserName), model.Address, model.Postalcode, model.State, model.City, model.PhoneNo)
                    .Select(x => new UserViewModel
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        Password = model.Password,
                        Address = model.Address,
                        Postalcode = model.Postalcode,
                        State = model.State,
                        City = model.City,
                        CreatedBy = model.CreatedBy
                    }).ToList().FirstOrDefault();
                if (model != null)
                {
                    model.IsSuccess = true;
                }

            }
            catch (Exception ex)
            {

                var message = ex.Message;
                model.ResponseMessage = message;
            }
            return model;
        }


        public UserViewModel Login(UserViewModel model)
        {
            try
            {
                //model = db.SP_Login(model.UserName, CommonUser.GetMd5Hash(model.Password, model.UserName))
                model = db.SP_Login(model.UserName, CommonUser.GetMd5Hash(model.Password, model.UserName))
                .Select(x => new UserViewModel
                {
                    UserName = x.UserName,
                    FullName = x.FullName,
                    Email = x.Email,
                    Address = x.Address,
                    Postalcode = (int)x.Postalcode,
                    State = x.State,
                    City = x.City,
                    PhoneNo = x.Phone_No,
                    ResponseMessage = x.Message,

                }).ToList().FirstOrDefault();
                if (model != null)
                {
                    model.IsSuccess = true;
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                model.ResponseMessage = message;
            }
            return model;
        }
    }
}