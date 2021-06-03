using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SanphamsController : Controller
    {
        private CT25Team11Entities db = new CT25Team11Entities();

        // GET: Sanphams
        public ActionResult Index()
        {
            return View(db.Sanphams.ToList());
        }

        public ActionResult Search(string keyword)
        {
            var model = db.Sanphams.ToList();
            model = model.Where(s => s.TenSanPham.ToLower().Contains(keyword.ToLower())).ToList();
            ViewBag.keyword = keyword;
            return View("Index2", model);
        }
        public ActionResult Index2()
        {
            return View(db.Sanphams.ToList());
        }
        public ActionResult InforProduct(string id, Sanpham SANPHAM)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sanpham sanpham = db.Sanphams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // GET: Sanphams/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sanpham sanpham = db.Sanphams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // GET: Sanphams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sanphams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sanpham sANPHAM)
        {
            if (ModelState.IsValid)
            {
                if (sANPHAM.ImageUpload != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(sANPHAM.ImageUpload.FileName).ToString();
                    string extension = Path.GetExtension(sANPHAM.ImageUpload.FileName);
                    filename = filename + extension;
                    sANPHAM.HinhAnh = "~/images/" + filename;
                    sANPHAM.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/images/"), filename));
                    db.Sanphams.Add(sANPHAM);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View(sANPHAM);
        }

        // GET: Sanphams/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sanpham sanpham = db.Sanphams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // POST: Sanphams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (Sanpham sANPHAM)
        {
            if (ModelState.IsValid)
            {
                if (sANPHAM.ImageUpload != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(sANPHAM.ImageUpload.FileName).ToString();
                    string extension = Path.GetExtension(sANPHAM.ImageUpload.FileName);
                    filename = filename + extension;
                    sANPHAM.HinhAnh = "~/images/" + filename;
                    sANPHAM.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/images/"), filename));

                    db.Entry(sANPHAM).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(sANPHAM);
        }

        // GET: Sanphams/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sanpham sanpham = db.Sanphams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // POST: Sanphams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Sanpham sanpham = db.Sanphams.Find(id);
            db.Sanphams.Remove(sanpham);
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
