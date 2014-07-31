using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CorpPolicy.Models;
using PagedList;

namespace CorpPolicy.Controllers
{
    public class ClientController : Controller
    {
        private CorpPolicyEntities _db = new CorpPolicyEntities();

        public ActionResult AutoComplete(string term)
        {
            var model = _db.Clients.Where(cl => cl.Name.Contains(term))
                .OrderBy(cl => cl.Name).Take(10)
                .Select(cl => new
                {
                    label = cl.Name
                });

            return Json(model, JsonRequestBehavior.AllowGet);

        }

        // GET: /Client/
        public ActionResult Index(string clientName = null, int page = 1, int numRec = 10)
        {
            var model = _db.Clients.OrderBy(cl => cl.Name)
                .Where(cl => clientName == null || cl.Name.Contains(clientName))
                .ToPagedList(page, numRec);
                        
            return View(model);
        }

        // GET: /Client/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = _db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: /Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ClientNo,Name,Initials,Title,Salutation,VatNo,Address,Address1,Address2,Address3,PostalCode,BirthDate,Language,IdentityNo,BusinessPhone,HomePhone,Fax,CellPhone,Contact,Referral,MarriedFlag,GroupFlag,VIPFlag,LifeClient,PrintFlag,Cancelled,CancelledDate,Status,Created,CreatedBy,Changed,ChangedBy")] Client client)
        {
            if (ModelState.IsValid)
            {
                _db.Clients.Add(client);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: /Client/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = _db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: /Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ClientNo,Name,Initials,Title,Salutation,VatNo,Address,Address1,Address2,Address3,PostalCode,BirthDate,Language,IdentityNo,BusinessPhone,HomePhone,Fax,CellPhone,Contact,Referral,MarriedFlag,GroupFlag,VIPFlag,LifeClient,PrintFlag,Cancelled,CancelledDate,Status,Created,CreatedBy,Changed,ChangedBy")] Client client)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(client).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: /Client/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = _db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: /Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = _db.Clients.Find(id);
            _db.Clients.Remove(client);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult Search(string currentFilter = "", string searchString = "", int page = 1, int pageSize = 10)
        {
            if (!searchString.Equals(currentFilter))
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var model = _db.Clients.Select(cl => cl);

            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(cl => cl.Name.Contains(searchString));
            }

            model = model.OrderBy(cl => cl.Name);

            return PartialView("_ClientList", model.ToPagedList(page, pageSize));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
