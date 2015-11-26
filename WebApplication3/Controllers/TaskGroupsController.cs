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
    public class TaskGroupsController : Controller
    {
        private DefaultConnection db = new DefaultConnection();

        // GET: TaskGroups
        public ActionResult Index()
        {
            return View(db.TaskGroups.Include(w => w.CodeTasks).ToList());
        }

        // GET: TaskGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskGroup taskGroup = db.TaskGroups.Include(w=>w.CodeTasks).First(w=>w.Id == id);
            if (taskGroup == null)
            {
                return HttpNotFound();
            }
            return View(taskGroup);
        }

        // GET: TaskGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] TaskGroup taskGroup)
        {
            if (ModelState.IsValid)
            {
                db.TaskGroups.Add(taskGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taskGroup);
        }

        // GET: TaskGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskGroup taskGroup = db.TaskGroups.Find(id);
            if (taskGroup == null)
            {
                return HttpNotFound();
            }
            return View(taskGroup);
        }

        [HttpGet]
        public ActionResult AddTaskToGroup(int id)
        {
            var codeTask = new CodeTask()
            {
                Title = "New Code Task",
                Datastamp = DateTime.UtcNow
            };

            db.CodeTasks.Add(codeTask);

            var entry = db.TaskGroups.Include(w => w.CodeTasks).First(w => w.Id == id);
            if (entry.CodeTasks == null)
            {
                entry.CodeTasks = new List<CodeTask>();
            }
            entry.CodeTasks.Add(codeTask);
            db.Entry(entry).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }





        // POST: TaskGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] TaskGroup taskGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskGroup);
        }

        // GET: TaskGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskGroup taskGroup = db.TaskGroups.Find(id);
            if (taskGroup == null)
            {
                return HttpNotFound();
            }
            return View(taskGroup);
        }

        // POST: TaskGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskGroup taskGroup = db.TaskGroups.Find(id);
            db.TaskGroups.Remove(taskGroup);
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
