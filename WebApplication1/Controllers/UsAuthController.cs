using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UsAuthController : Controller
    {
        // GET: UsAuth
       
        public ActionResult Index()
        {         
            return View();
        }     

        public ActionResult UsAuth()
        {
            return View();
        }
    }
}