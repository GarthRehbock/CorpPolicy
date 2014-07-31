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
    public class PolicyController : Controller
    {
        private CorpPolicyEntities db = new CorpPolicyEntities();

        public ActionResult AutoComplete(string term)
        {
            var model = db.Policies.Where(p => p.PolicyNo.Contains(term))
                .OrderBy(p => p.PolicyNo).Take(10)
                .Select(p => new
                {
                    label = p.PolicyNo
                });

            return Json(model, JsonRequestBehavior.AllowGet);

        }


        // GET: /Policy/
        public ActionResult Index(int page = 1, int numRec = 10)
        {
            ViewBag.CurrentSort = "";
            ViewBag.IncludeCancelled = "";
            ViewBag.PolicyNoSortParm = "";
            ViewBag.InsuredSortParm = "insured";
            ViewBag.ClientSortParm = "client";
            ViewBag.CurrentFilter = "";

            var policies = db.Policies//.Include(p => p.Client).Include(p => p.Insurer).Include(p => p.PolicyType).Include(p => p.PolicyBank)
                .Where(p => p.Cancelled == false)
                .OrderBy(p => p.PolicyNo)
                .ToPagedList(page, numRec);

            return View(policies);
        }

        // GET: /Policy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Policy policy = db.Policies.Find(id);
            if (policy == null)
            {
                return HttpNotFound();
            }
            return View(policy);
        }

        // GET: /Policy/Create
        public ActionResult Create()
        {
            ViewBag.ClientNo = new SelectList(db.Clients, "ClientNo", "Name");
            ViewBag.InsurerID = new SelectList(db.Insurers, "InsurerID", "InsurerName");
            ViewBag.PolicyTypeID = new SelectList(db.PolicyTypes, "PolicyTypeID", "PolicyTypeName");
            ViewBag.PolicyID = new SelectList(db.PolicyBanks, "PolicyID", "AccountName");
            return View();
        }

        // POST: /Policy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PolicyID,PolicyNo,ClientNo,InsurerID,PolicyTypeID,Cancelled,InceptionDate,AnnivMonth,RenewalDate,CancellationDate,Frequeny,Insured,AdminFee,BrokerFee,Status,Created,CreatedBy,Changed,ChangedBy")] Policy policy)
        {
            if (ModelState.IsValid)
            {
                db.Policies.Add(policy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientNo = new SelectList(db.Clients, "ClientNo", "Name", policy.ClientNo);
            ViewBag.InsurerID = new SelectList(db.Insurers, "InsurerID", "InsurerName", policy.InsurerID);
            ViewBag.PolicyTypeID = new SelectList(db.PolicyTypes, "PolicyTypeID", "PolicyTypeName", policy.PolicyTypeID);
            ViewBag.PolicyID = new SelectList(db.PolicyBanks, "PolicyID", "AccountName", policy.PolicyID);
            return View(policy);
        }

        // GET: /Policy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Policy policy = db.Policies.Find(id);
            if (policy == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientNo = new SelectList(db.Clients, "ClientNo", "Name", policy.ClientNo);
            ViewBag.InsurerID = new SelectList(db.Insurers, "InsurerID", "InsurerName", policy.InsurerID);
            ViewBag.PolicyTypeID = new SelectList(db.PolicyTypes, "PolicyTypeID", "PolicyTypeName", policy.PolicyTypeID);
            ViewBag.PolicyID = new SelectList(db.PolicyBanks, "PolicyID", "AccountName", policy.PolicyID);
            return View(policy);
        }

        // POST: /Policy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PolicyID,PolicyNo,ClientNo,InsurerID,PolicyTypeID,Cancelled,InceptionDate,AnnivMonth,RenewalDate,CancellationDate,Frequeny,Insured,AdminFee,BrokerFee,Status,Created,CreatedBy,Changed,ChangedBy")] Policy policy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(policy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientNo = new SelectList(db.Clients, "ClientNo", "Name", policy.ClientNo);
            ViewBag.InsurerID = new SelectList(db.Insurers, "InsurerID", "InsurerName", policy.InsurerID);
            ViewBag.PolicyTypeID = new SelectList(db.PolicyTypes, "PolicyTypeID", "PolicyTypeName", policy.PolicyTypeID);
            ViewBag.PolicyID = new SelectList(db.PolicyBanks, "PolicyID", "AccountName", policy.PolicyID);
            return View(policy);
        }

        // GET: /Policy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Policy policy = db.Policies.Find(id);
            if (policy == null)
            {
                return HttpNotFound();
            }
            return View(policy);
        }

        // POST: /Policy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Policy policy = db.Policies.Find(id);
            db.Policies.Remove(policy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult Search(string sortOrder, string searchString = "", string currentFilter ="", string includeCancelled = "off", int page = 1, int pageSize = 10)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IncludeCancelled = includeCancelled.Equals("off") ? "" : "checked";
            ViewBag.PolicyNoSortParm = String.IsNullOrEmpty(sortOrder) ? "policyNo_desc" : "";
            ViewBag.InsuredSortParm = sortOrder == "insured" ? "insured_desc" : "insured";
            ViewBag.ClientSortParm = sortOrder == "client" ? "client_desc" : "client";

            if (!searchString.Equals(currentFilter))
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var policies = db.Policies.Include(p => p.Client).Include(p => p.Insurer).Include(p => p.PolicyType).Include(p => p.PolicyBank);

            if (includeCancelled.Equals("off"))
            {
                policies = policies.Where(p => p.Cancelled == false);
            }
    
            if (!String.IsNullOrEmpty(searchString))
            {
                policies = policies.Where(p => p.PolicyNo.Contains(searchString));
            }
       
            switch (sortOrder)
            {
                case "policyNo_desc":
                    policies = policies.OrderByDescending(p => p.PolicyNo);
                    break;
                case "insured":
                    policies = policies.OrderBy(p => p.Insured);
                    break;
                case "insured_desc":
                    policies = policies.OrderByDescending(p => p.Insured);
                    break;
                case "client":
                    policies = policies.OrderBy(p => p.Client.Name);
                    break;
                case "client_desc":
                    policies = policies.OrderByDescending(p => p.Client.Name);
                    break;
                default:
                    policies = policies.OrderBy(p => p.PolicyNo);
                    break;
            }


            return PartialView("_PolicyList", policies.ToPagedList(page, pageSize));
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
