﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjektBazyDanych;

namespace ProjektBazyDanych.Controllers
{
    public class RunwaysController : Controller
    {
        private connectionString db = new connectionString();

        // GET: Runways
        public async Task<ActionResult> Index()
        {
            foreach (var item in db.Runways)
            {
                item.animalCount = item.Animals.Count;
            }
            return View(await db.Runways.ToListAsync());
        }

        // GET: Runways/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Runway runway = await db.Runways.FindAsync(id);
            runway.animalCount = runway.Animals.Count;

            if (runway == null)
            {
                return HttpNotFound();
            }
            return View(runway);
        }

        // GET: Runways/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Runways/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,area,animalCount")] Runway runway)
        {
            if (ModelState.IsValid)
            {
                db.Runways.Add(runway);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(runway);
        }

        // GET: Runways/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Runway runway = await db.Runways.FindAsync(id);
            runway.animalCount = runway.Animals.Count;

            if (runway == null)
            {
                return HttpNotFound();
            }
            return View(runway);
        }

        // POST: Runways/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,area,animalCount")] Runway runway)
        {
            if (ModelState.IsValid)
            {
                db.Entry(runway).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(runway);
        }

        // GET: Runways/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Runway runway = await db.Runways.FindAsync(id);
            runway.animalCount = runway.Animals.Count;

            if (runway == null)
            {
                return HttpNotFound();
            }
            return View(runway);
        }

        // POST: Runways/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Runway runway = await db.Runways.FindAsync(id);
            db.Runways.Remove(runway);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //GET: Runways/AddSupervisor/5
        public async Task<ActionResult> AddSupervisor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Runway runway = await db.Runways.FindAsync(id);
            if (runway == null)
            {
                return HttpNotFound();
            }
            var runwaySupervisors = runway.Supervisors.Select(x => x.id).ToList();
            IEnumerable<Supervisor> availableSupervisors = db.Supervisors.
                Where(x => !runwaySupervisors.
                Contains(x.id));
            string firstSupervisor;
            if (!runwaySupervisors.Any())
                firstSupervisor = db.Supervisors.Where(x => !runwaySupervisors.Contains(x.id)).FirstOrDefault().lastName;
            else firstSupervisor = "brak dostępnego nadzorcy";

            ViewBag.lastName = new SelectList(availableSupervisors, "lastName", "lastName", firstSupervisor);
            return View(runway);

        }

        //POST: Runways/AddSupervisor/5
        [HttpPost, ActionName("AddSupervisor")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddSupervisor(string lastName, int? runwayId)
        {
            if (runwayId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Supervisor supervisor = db.Supervisors.Where(x=>x.lastName==lastName).FirstOrDefault();
                Runway runway = await db.Runways.FindAsync(runwayId);
                runway.Supervisors.Add(supervisor);
                supervisor.Runways.Add(runway);
                db.Entry(runway).State = EntityState.Modified;
                db.Entry(supervisor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = runway.id });
            }
            catch
            {
                Runway runway = await db.Runways.FindAsync(runwayId);
                var runwaySupervisors = runway.Supervisors.Select(x => x.id).ToList();
                IEnumerable<Supervisor> availableSupervisors = db.Supervisors.
                    Where(x => !runwaySupervisors.
                    Contains(x.id));
                string firstSupervisor;
                if (!runwaySupervisors.Any())
                    firstSupervisor = db.Supervisors.Where(x => !runwaySupervisors.Contains(x.id)).FirstOrDefault().lastName;
                else firstSupervisor = "brak dostępnego nadzorcy";

                ViewBag.lastName = new SelectList(availableSupervisors, "lastName", "lastName", firstSupervisor);
                return View(runway);
            }
        }
        //GET: Runways/DeleteSupervisor/5
        public async Task<ActionResult> DeleteSupervisor(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Runway runway = await db.Runways.FindAsync(id);
            if (runway == null)
            {
                return HttpNotFound();
            }
            
            var runwaySupervisors = runway.Supervisors.Select(x => x.id).ToList();
            IEnumerable<Supervisor> availableSupervisors = db.Supervisors.
                Where(x => runwaySupervisors.
                Contains(x.id));
            string firstSupervisor;
            if (!availableSupervisors.Any())
                firstSupervisor = availableSupervisors.FirstOrDefault().lastName;
            else firstSupervisor = "brak dostępnego nadzorcy";

            ViewBag.lastName = new SelectList(availableSupervisors, "lastName", "lastName", firstSupervisor);
            return View(runway);

        }

        //POST: Runways/DeleteSupervisor/5
        [HttpPost, ActionName("DeleteSupervisor")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteSupervisor(string lastName, int? runwayId)
        {
            if (runwayId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Supervisor supervisor = db.Supervisors.Where(x => x.lastName == lastName).FirstOrDefault();
                Runway runway = await db.Runways.FindAsync(runwayId);
                runway.Supervisors.Remove(supervisor);
                supervisor.Runways.Remove(runway);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = runway.id });
            }
            catch
            {
                Runway runway = await db.Runways.FindAsync(runwayId);
                var runwaySupervisors = runway.Supervisors.Select(x => x.id).ToList();
                IEnumerable<Supervisor> availableSupervisors = db.Supervisors.
                    Where(x => !runwaySupervisors.
                    Contains(x.id));
                string firstSupervisor;
                if (!runwaySupervisors.Any())
                    firstSupervisor = db.Supervisors.Where(x => !runwaySupervisors.Contains(x.id)).FirstOrDefault().lastName;
                else firstSupervisor = "brak dostępnego nadzorcy";

                ViewBag.lastName = new SelectList(availableSupervisors, "lastName", "lastName", firstSupervisor);
                return View(runway);
            }
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
