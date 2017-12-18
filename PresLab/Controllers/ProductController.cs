using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PresLab.DAL;
using PresLab.Models;
using PresLab.ViewModels;

namespace PresLab.Controllers
{
    public class ProductController : Controller
    {
        private PresLabContext db = new PresLabContext();

        // GET: Product
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            PopulateCategoriesDropDownList();

            var product = new Product();
            product.Tests = new List<Test>();
            PopulateAssignedTestData(product);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Type,Brand,Description,Supplier, CategoryID")]Product product, string[] selectedTests)
        {
            try
            {
                if (selectedTests != null)
                {
                    product.Tests = new List<Test>();
                    foreach (var test in selectedTests)
                    {
                        var testToAdd = db.Tests.Find(int.Parse(test));
                        product.Tests.Add(testToAdd);
                    }
                }
                if (ModelState.IsValid)
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateCategoriesDropDownList(product.CategoryID);
            PopulateAssignedTestData(product);
            return View(product);


       }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products
                .Include(i => i.Tests)
                .Where(i => i.ID == id)
                .Single();
            PopulateAssignedTestData(product);
            if (product == null)
            {
                return HttpNotFound();
            }
            PopulateCategoriesDropDownList(product.CategoryID);
            return View(product);
        }

        private void PopulateAssignedTestData(Product product)
        {
            var allTests = db.Tests;
            var productTests = new HashSet<int>(product.Tests.Select(t => t.TestID));
            var viewModel = new List<AssignedTestData>();
            foreach (var test in allTests)
            {
                viewModel.Add(new AssignedTestData
                {
                    TestID = test.TestID,
                    Name = test.Name,
                    Assigned = productTests.Contains(test.TestID)
                });
            }
            ViewBag.Tests = viewModel;
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedTests)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var productToUpdate = db.Products
               .Include(i => i.Tests)
               .Where(i => i.ID == id)
               .Single();
               

            if (TryUpdateModel(productToUpdate, "",
               new string[] { "Type", "Brand", "Description", "Supplier", "CategoryID" }))
            {
                try
                {
                   
                    UpdateProductTest(selectedTests, productToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateCategoriesDropDownList(productToUpdate.CategoryID);
            PopulateAssignedTestData(productToUpdate);
            return View(productToUpdate);
        }

        private void PopulateCategoriesDropDownList(object selectedCategory = null)
        {
            var categoriesQuery = from c in db.Categories
                                   orderby c.Name
                                   select c;
            ViewBag.CategoryID = new SelectList(categoriesQuery, "CategoryID", "Name", selectedCategory);
        }

        private void UpdateProductTest(string[] selectedTests, Product productToUpdate)
        {
            if (selectedTests == null)
            {
                productToUpdate.Tests = new List<Test>();
                return;
            }

            var selectedTestsHS = new HashSet<string>(selectedTests);
            var productTests = new HashSet<int>
                (productToUpdate.Tests.Select(t => t.TestID));
            foreach (var test in db.Tests)
            {
                if (selectedTestsHS.Contains(test.TestID.ToString()))
                {
                    if (!productTests.Contains(test.TestID))
                    {
                        productToUpdate.Tests.Add(test);
                    }
                }
                else
                {
                    if (productTests.Contains(test.TestID))
                    {
                        productToUpdate.Tests.Remove(test);
                    }
                }
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
