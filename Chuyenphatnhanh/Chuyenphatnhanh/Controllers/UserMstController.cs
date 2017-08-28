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
    public class UserMstController : Controller
    {
        private DBConnection db = new DBConnection();

        // GET: UserMst
        public async Task<ActionResult> Index()
        {
            return View(await db.USER_MST.ToListAsync());
        }

        // GET: UserMst/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_MST uSER_MST = await db.USER_MST.FindAsync(id);
            if (uSER_MST == null)
            {
                return HttpNotFound();
            }
            return View(uSER_MST);
        }

        // GET: UserMst/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserMst/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "USER_ID,USER_NAME,PASSWORD,NUMBER_LOGIN_FAIL,LAST_CHANGE_PASS,OLD_PASSWORD,ADDRESS,PHONE,BRANCH,DELETE_FLAG,REG_DATE,MOD_DATE,REG_UID,MOD_UID")] USER_MST uSER_MST)
        {
            if (ModelState.IsValid)
            {
                db.USER_MST.Add(uSER_MST);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(uSER_MST);
        }

        // GET: UserMst/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_MST uSER_MST = await db.USER_MST.FindAsync(id);
            if (uSER_MST == null)
            {
                return HttpNotFound();
            }
            return View(uSER_MST);
        }

        // POST: UserMst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "USER_ID,USER_NAME,PASSWORD,NUMBER_LOGIN_FAIL,LAST_CHANGE_PASS,OLD_PASSWORD,ADDRESS,PHONE,BRANCH,DELETE_FLAG,REG_DATE,MOD_DATE,REG_UID,MOD_UID")] USER_MST uSER_MST)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSER_MST).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(uSER_MST);
        }

        // GET: UserMst/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_MST uSER_MST = await db.USER_MST.FindAsync(id);
            if (uSER_MST == null)
            {
                return HttpNotFound();
            }
            return View(uSER_MST);
        }

        // POST: UserMst/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            USER_MST uSER_MST = await db.USER_MST.FindAsync(id);
            db.USER_MST.Remove(uSER_MST);
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
