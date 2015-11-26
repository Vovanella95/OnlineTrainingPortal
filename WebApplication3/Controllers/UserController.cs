using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class UserController : Controller
    {

        DefaultConnection context = new DefaultConnection();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyData()
        {
            return View(context.UserDatas.FirstOrDefault(w => w.Email == User.Identity.Name));
        }

        [HttpPost]
        public ActionResult MyData([Bind(Exclude = "avatar")]UserData userData, HttpPostedFileBase avatar)
        {
            var entry = context.UserDatas.First(w => w.Email == userData.Email);
            if (avatar != null)
            {
                byte[] ava = new byte[avatar.ContentLength];
                avatar.InputStream.Read(ava, 0, avatar.ContentLength);
                userData.Avatar = ava;
            }
            else
            {
                userData.Avatar = entry.Avatar;
            }

            context.Entry(entry).CurrentValues.SetValues(userData);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult AllUsers()
        {
            return View(context.UserDatas);
        }



        public static UserData GetCurrentUserData(string userName, string type = "main")
        {
            return new DefaultConnection().UserDatas.First(w => w.Email == userName);
        }

        public ActionResult GetUserAvatar(string userName = "none")
        {

            byte[] imageData;
            if (userName == "none")
            {
                imageData = context.UserDatas.First(w => w.Email == User.Identity.Name).Avatar;
            }else
            {
                imageData = context.UserDatas.First(w => w.Email == userName).Avatar;
            }

            if (imageData != null)
            {
                return File(imageData, "image/png");
            }
            return File("~/Icons/User-icon.png", "image/png");
        }
    }
}