using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class CodeTaskController : Controller
    {

        DefaultConnection context = new DefaultConnection();
        // GET: CodeTask
        public ActionResult Index()
        {
            return View(context.CodeTasks);
        }

        // GET: CodeTask/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CodeTask/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CodeTask/Create
        [HttpPost]
        public ActionResult Create(CodeTask collection)
        {
            context.CodeTasks.Add(collection);
            collection.Datastamp = DateTime.Now;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: CodeTask/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CodeTask/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CodeTask/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CodeTask/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
