using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chuyenphatnhanh.Models;

namespace Chuyenphatnhanh.Controllers
{
    public class CommonCodeController : Controller
    {
        private DBConnection db = new DBConnection();

        // GET: CommonCode
        public async Task<ActionResult> Index()
        {
            var cOMMON_CODE = db.COMMON_CODE.Include(c => c.USER_MST).Include(c => c.USER_MST1);
            return View(await cOMMON_CODE.ToListAsync());
        }

        // GET: CommonCode/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMON_CODE cOMMON_CODE = await db.COMMON_CODE.FindAsync(id);
            if (cOMMON_CODE == null)
            {
                return HttpNotFound();
            }
            return View(cOMMON_CODE);
        }

        // GET: CommonCode/Create
        public ActionResult Create()
        {
            ViewBag.MOD_UID = new SelectList(db.USER_MST, "USER_ID", "USER_NAME");
            ViewBag.MOD_UID = new SelectList(db.USER_MST, "USER_ID", "USER_NAME");
            return View();
        }

        // POST: CommonCode/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "COMMON_CODE_ID,COMMON_CODE_VALUE,COMMON_CODE_DESC,PARENT_CODE_VALUE,COMMON_CODE1,DELETE_FLAG,REG_DATE,MOD_DATE,REG_UID,MOD_UID")] COMMON_CODE cOMMON_CODE)
        {
            if (ModelState.IsValid)
            {
                db.COMMON_CODE.Add(cOMMON_CODE);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MOD_UID = new SelectList(db.USER_MST, "USER_ID", "USER_NAME", cOMMON_CODE.MOD_UID);
            ViewBag.MOD_UID = new SelectList(db.USER_MST, "USER_ID", "USER_NAME", cOMMON_CODE.MOD_UID);
            return View(cOMMON_CODE);
        }

        // GET: CommonCode/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMON_CODE cOMMON_CODE = await db.COMMON_CODE.FindAsync(id);
            if (cOMMON_CODE == null)
            {
                return HttpNotFound();
            }
            ViewBag.MOD_UID = new SelectList(db.USER_MST, "USER_ID", "USER_NAME", cOMMON_CODE.MOD_UID);
            ViewBag.MOD_UID = new SelectList(db.USER_MST, "USER_ID", "USER_NAME", cOMMON_CODE.MOD_UID);
            return View(cOMMON_CODE);
        }

        // POST: CommonCode/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "COMMON_CODE_ID,COMMON_CODE_VALUE,COMMON_CODE_DESC,PARENT_CODE_VALUE,COMMON_CODE1,DELETE_FLAG,REG_DATE,MOD_DATE,REG_UID,MOD_UID")] COMMON_CODE cOMMON_CODE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOMMON_CODE).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MOD_UID = new SelectList(db.USER_MST, "USER_ID", "USER_NAME", cOMMON_CODE.MOD_UID);
            ViewBag.MOD_UID = new SelectList(db.USER_MST, "USER_ID", "USER_NAME", cOMMON_CODE.MOD_UID);
            return View(cOMMON_CODE);
        }

        // GET: CommonCode/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMON_CODE cOMMON_CODE = await db.COMMON_CODE.FindAsync(id);
            if (cOMMON_CODE == null)
            {
                return HttpNotFound();
            }
            return View(cOMMON_CODE);
        }

        // POST: CommonCode/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            COMMON_CODE cOMMON_CODE = await db.COMMON_CODE.FindAsync(id);
            db.COMMON_CODE.Remove(cOMMON_CODE);
            await db.SaveChangesAsync();
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
