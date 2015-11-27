using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class CodeTasksController : Controller
    {
        private DefaultConnection db = new DefaultConnection();

        // GET: CodeTasks
        public ActionResult Index()
        {
            return View(db.CodeTasks.ToList());
        }

        // GET: CodeTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeTask codeTask = db.CodeTasks.Find(id);
            if (codeTask == null)
            {
                return HttpNotFound();
            }
            return View(codeTask);
        }

        // GET: CodeTasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CodeTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Text,Image,TestFunction,UserInputTemplate,Coast,Datastamp")] CodeTask codeTask)
        {
            if (ModelState.IsValid)
            {
                db.CodeTasks.Add(codeTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(codeTask);
        }

        // GET: CodeTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeTask codeTask = db.CodeTasks.Find(id);
            if (codeTask == null)
            {
                return HttpNotFound();
            }
            return View(codeTask);
        }

        // POST: CodeTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Text,Image,TestFunction,UserInputTemplate,Coast,Datastamp")] CodeTask codeTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(codeTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(codeTask);
        }

        // GET: CodeTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeTask codeTask = db.CodeTasks.Find(id);
            if (codeTask == null)
            {
                return HttpNotFound();
            }
            return View(codeTask);
        }

        // POST: CodeTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CodeTask codeTask = db.CodeTasks.Find(id);
            db.CodeTasks.Remove(codeTask);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
