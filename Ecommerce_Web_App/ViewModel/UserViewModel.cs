using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce_Web_API.ViewModel
{
    public class UserViewModel
    {
        public bool IsSuccess { get; set; }
        public string ResponseMessage { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Postalcode { get; set; }
        public int CreatedBy { get; set; }
        public string PhoneNo { get; set; }
        public string FullName { get; set; }
    }
}