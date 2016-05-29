using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Engrams.Controllers
{
    using System.Data.Entity.Migrations;

    using Engrams.Models;

    public class TagController : Controller
    {
        private EngramsContext context = new EngramsContext();
        // GET: Tag
        public ActionResult Index()
        {
            return this.View(this.context.Tags);
        }

        // GET: Tag/Details/5
        public ActionResult Details(int id)
        {
            return this.View((Tag)this.context.Tags.Select(t => t.TagId == id));
        }

        // GET: Tag/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Tag/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var tag = new Tag
                              {
                                  Name = collection["Name"],
                                  Description = collection["Description"]
                              };

                this.context.Tags.Add(tag);
                this.context.SaveChanges();

                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.View();
            }
        }

        // GET: Tag/Edit/5
        public ActionResult Edit(int id)
        {
            return this.View();
        }

        // POST: Tag/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var dbTag = this.context.Tags.SingleOrDefault(t => t.TagId == id);

                if (dbTag == null)
                {
                    throw new ArgumentNullException(nameof(dbTag));
                }
                else
                {
                    dbTag.Name = collection["Name"];
                    dbTag.Description = collection["Description"];

                    this.context.Tags.AddOrUpdate(dbTag);
                    this.context.SaveChanges();
                }

                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.View();
            }
        }

        // GET: Tag/Delete/5
        public ActionResult Delete(int id)
        {
            return this.View();
        }

        // POST: Tag/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.View();
            }
        }
    }
}
