using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindingNemo.Controllers
{
    using System.Data.Entity;

    using Engrams.Models;

    using FindingNemo.Models;

    public class MemoryController : Controller
    {
        private EngramsContext context = new EngramsContext();

        // GET: Memory
        public ActionResult Index()
        {
            ViewBag.Tags = new SelectList(this.context.Tags.OrderBy(t => t.Name), "TagId", "Name");
            return this.View();
        }

        //// GET: Memory/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Memory/Create
        public ActionResult Create()
        {
            var model = this.InitiateMemory();
            return this.View(model);
        }

        // POST: Memory/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                var memory = this.CreateMemory(collection);

                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.View();
            }
        }

        //// GET: Memory/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Memory/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Memory/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Memory/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        #region Partial views

        public PartialViewResult MemoriesByTag(int id)
        {
            return this.PartialView(
                this.context.Memories.Where(m => m.TagId == id).OrderByDescending(r => r.CreateData)
                    .ThenBy(r => r.Title).ToList());
        }

        #endregion

        private MemoryViewModel InitiateMemory()
        {
            var model = new MemoryViewModel
            {
                Tags = this.GetTags()
            };
            return model;
        }

        private IEnumerable<SelectListItem> GetTags()
        {
            var dbTags = this.context.Tags;

            var tags = dbTags.AsEnumerable()
                .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.TagId.ToString(),
                                    Text = x.Name
                                });

            return new SelectList(tags, "Value", "Text");
        }

        //[HttpPost]
        //public JsonResult TagsByName(string prefix)
        //{
        //    var result = from r in this.context.Tags
        //                 where r.Name.ToLower().StartsWith(prefix)
        //                 select r.Name;
        //    return this.Json(result, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult TagsByName()
        {
            string term = this.Request.QueryString["term"].ToLower();
            var res = from r in this.context.Tags
                      where r.Name.ToLower().StartsWith(term)
                      select r.Name;
            return this.Json(res, JsonRequestBehavior.AllowGet);
        }

        private Memory CreateMemory(FormCollection collection)
        {
            var memory = new Memory
            {
                Title = collection["Title"],
                Note = collection["Note"],
                TagId = Convert.ToInt32(collection["SelectedTagId"]),
                CreateData = DateTime.Now
            };

            this.context.Memories.Add(memory);
            this.context.SaveChanges();

            return memory;
        }
    }
}
