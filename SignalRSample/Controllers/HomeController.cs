using Microsoft.AspNetCore.Mvc;
using SignalRSample.Models.ORM.Context;
using SignalRSample.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ChatContext _chatcontext;

        public HomeController(ChatContext chatcontext)
        {
            _chatcontext = chatcontext;
        }
        public IActionResult Index()
        {
            //ViewBag.UserName = HttpContext.User.Claims.ToArray()[2].Value;
            List<AdminUser> users = _chatcontext.AdminUsers.ToList();

            return View(users);
        }
    }
}
