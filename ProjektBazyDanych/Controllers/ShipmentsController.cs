﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjektBazyDanych.Controllers
{
    public class ShipmentsController : Controller
    {
        private connectionString db = new connectionString();

        // GET: Shipments
        public async Task<ActionResult> Index()
        {
            var shipments = db.Shipments.Include(s => s.Settlements).Include(s => s.Supplier);
            return View(await shipments.ToListAsync());
        }

        // GET: Shipments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = await db.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // GET: Shipments/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.Settlements, "shipmentId", "shipmentId");
            ViewBag.supplierName = new SelectList(db.Suppliers, "name", "name");
            ViewBag.id = new SelectList(db.Settlements, "shipmentId", "shipmentId");
            return View();
        }

        // POST: Shipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,shipmentDate,supplierName,amount")] Shipment shipment)
        {
            if (shipment.shipmentDate > DateTime.Today)
            {
                ModelState.AddModelError("shipmentDate", "Data nie może być większa od obecnej");
            }
            //int id = 0;
            //do
            //{
            //    id = new Random().Next();
            //} while (db.Shipments.Select(x => x.id).ToList().Contains(id));
            //shipment.id = id;
            if (ModelState.IsValid)
            {
                db.Shipments.Add(shipment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.Settlements, "shipmentId", "shipmentId", shipment.id);
            ViewBag.supplierName = new SelectList(db.Suppliers, "name", "name", shipment.supplierId);
            ViewBag.id = new SelectList(db.Settlements, "shipmentId", "shipmentId", shipment.id);
            return View(shipment);
        }

        // GET: Shipments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = await db.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.Settlements, "shipmentId", "shipmentId", shipment.id);
            ViewBag.supplierName = new SelectList(db.Suppliers, "name", "name", shipment.supplierId);
            ViewBag.id = new SelectList(db.Settlements, "shipmentId", "shipmentId", shipment.id);
            return View(shipment);
        }

        // POST: Shipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,shipmentDate,supplierName,amount")] Shipment shipment)
        {
            if (shipment.shipmentDate > DateTime.Today)
            {
                ModelState.AddModelError("shipmentDate", "Data nie może być większa od obecnej");
            }
            if (ModelState.IsValid)
            {
                db.Entry(shipment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id = new SelectList(db.Settlements, "shipmentId", "shipmentId", shipment.id);
            ViewBag.supplierName = new SelectList(db.Suppliers, "name", "name", shipment.supplierId);
            ViewBag.id = new SelectList(db.Settlements, "shipmentId", "shipmentId", shipment.id);
            return View(shipment);
        }

        // GET: Shipments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = await db.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // POST: Shipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Shipment shipment = await db.Shipments.FindAsync(id);
            db.Shipments.Remove(shipment);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AddFood(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = await db.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            var shipmentFood = shipment.Foods.Select(x => x.name).ToList();
            IEnumerable<Food> availableFood = db.Foods.
                Where(x => !shipmentFood.
                Contains(x.name));

            ViewBag.name = new SelectList(availableFood, "name", "name");
            return View(shipment);
        }

        [HttpPost, ActionName("AddFood")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddFood(string name, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (name == null)
            {
                ModelState.AddModelError("name", "Nie wybrałeś jedzenia");
            }
            if (ModelState.IsValid)
            {
                Food food = await db.Foods.Where(x => x.name == name).SingleOrDefaultAsync();
                Shipment shipment = await db.Shipments.FindAsync(id);
                shipment.Foods.Add(food);
                food.Shipments.Add(shipment);
                db.Entry(shipment).State = EntityState.Modified;
                db.Entry(food).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = shipment.id });
            }
            else
            {
                Shipment shipment = await db.Shipments.FindAsync(id);
                var shipmentFood = shipment.Foods.Select(x => x.name).ToList();
                IEnumerable<Food> availableFood = db.Foods.
                    Where(x => !shipmentFood.
                    Contains(x.name));

                ViewBag.name = new SelectList(availableFood, "name", "name");
                return View(shipment);
            }
        }

        public async Task<ActionResult> DeleteFood(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = await db.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            var shipmentFood = shipment.Foods.Select(x => x.name).ToList();
            IEnumerable<Food> availableFood = db.Foods.
                Where(x => shipmentFood.
                Contains(x.name));

            ViewBag.name = new SelectList(availableFood, "name", "name");
            return View(shipment);
        }

        [HttpPost, ActionName("DeleteFood")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteFood(string name, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (name == null)
            {
                ModelState.AddModelError("name", "Nie wybrałeś jedzenia");
            }
            if (ModelState.IsValid)
            {
                Food food = await db.Foods.Where(x => x.name == name).SingleOrDefaultAsync();
                Shipment shipment = await db.Shipments.FindAsync(id);
                shipment.Foods.Remove(food);
                food.Shipments.Remove(shipment);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = shipment.id });
            }
            else
            {
                Shipment shipment = await db.Shipments.FindAsync(id);
                ViewBag.name = new SelectList(db.Foods.Where(x => !shipment.Foods.Contains(x)), "name", "name");
                return View(shipment);
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