using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using System.Data.Entity;

namespace WebApplication3.Controllers
{
    public class UserSolveController : Controller
    {
        DefaultConnection context = new DefaultConnection();
        // GET: UserSolve
        public ActionResult Index()
        {
            return View(context.TaskGroups);
        }

        public ActionResult GroupDetails(int? id)
        {
            return View(context.TaskGroups.Include(w => w.CodeTasks).First(w=>w.Id == id));
        }

        public static UserData GetCurrentUser(string userName)
        {
            return new DefaultConnection().UserDatas.Include(w => w.CompletedTasks).First(w => w.Email == userName);
        }
    }
}