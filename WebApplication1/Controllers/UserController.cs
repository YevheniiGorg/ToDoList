using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        Context context;       
       
        public UserController()
        {
            context = new Context();
        }

        void AddUser(string Login, string Password)
        {
            context.Users.Add(new User(Login, Password));
            context.SaveChanges();
        }
        // http://localhost:61212/User?act=AddUser&Login=admin2&Password=12345678

        void DeleteUser(string Login, string Password)
        {
            User user = (from item in context.Users where item.Login == Login where item.Password == Password select item).ToArray<User>()[0];
            context.Users.Remove(user);
            context.SaveChanges();
        }
        // http://localhost:61212/User?act=DeleteUser&Login=admin2&Password=12345678

        void RenameUser(string Login, string Password, string LoginNew, string PasswordNew)
        {
            User user = (from item in context.Users where item.Login == Login where item.Password == Password select item).ToArray<User>()[0];
            user.Login = LoginNew;
            user.Password = PasswordNew;
            context.SaveChanges();
        }
        // http://localhost:61212/User?act=RenameUser&Login=admin2&Password=12345678&LoginNew=admin3&PasswordNew=1111111

        [HttpGet]
        string Validator(string Login, string Password)
        {
            if ((from item in context.Users where item.Login == Login select item).ToArray<User>().Length >0)
            {
                User user = (from item in context.Users where item.Login == Login select item).ToArray<User>()[0];

                if (user.Login == Login & user.Password == Password)
                {
                    return JsonConvert.SerializeObject(user.Id);
                }
                else
                    return JsonConvert.SerializeObject("null");
            }
            else
                return JsonConvert.SerializeObject("null");

        }
        // http://localhost:61212/User?act=Validator&Login=admin&Password=admin


        string ValidatorLogin(string Login)
        {
            User user = (from item in context.Users select item).ToArray()[0];
            if (user.Login == Login)
            {
                return JsonConvert.SerializeObject(true);
            }
            else
                return JsonConvert.SerializeObject(false);
        }      
      
        public string Index()
        {
            switch (Request.QueryString["act"])
            {
                case "AddUser":
                    AddUser(Request.QueryString["Login"], Request.QueryString["Password"]);
                    break;
                case "DeleteUser":
                    DeleteUser(Request.QueryString["Login"], Request.QueryString["Password"]);
                    break;
                case "RenameUser":
                    RenameUser(Request.QueryString["Login"], Request.QueryString["Password"], Request.QueryString["LoginNew"], Request.QueryString["PasswordNew"]);
                    break;
                case "Validator":                   
                    return Validator(Request.QueryString["Login"], Request.QueryString["Password"]);
                case "ValidatorLogin":
                    return ValidatorLogin(Request.QueryString["Login"]);
              
            }
            return null;
        }
    }
}