using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PresLab.DAL;
using PresLab.Models;
using System.Data.Entity.Infrastructure;
using PresLab.ViewModels;
using Postal;

namespace PresLab.Controllers
{
    public class OrderController : Controller
    {
        private PresLabContext db = new PresLabContext();

        // GET: Order
        public ActionResult Index(int? SelectedClient)
        {
            var clients = db.Clients.OrderBy(q => q.Name).ToList();
            ViewBag.SelectedClient = new SelectList(clients, "ClientID", "Name", SelectedClient);
            int clientID = SelectedClient.GetValueOrDefault();
            var orders = db.Orders.Include(o => o.Client);
            return View(orders.ToList());
        }

        // GET: Order/Details/5
        public ActionResult Details(int? id, int? SelectedClient)
        {
            var clients = db.Clients.OrderBy(q => q.Name).ToList();
            ViewBag.SelectedClient = new SelectList(clients, "ClientID", "Name", SelectedClient);
            int clientID = SelectedClient.GetValueOrDefault();
            var orders = db.Orders.Include(o => o.Client);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            var order = new Order();
            order.Samplings = new List<Product>();
            PopulateAssignedProductData(order);
            PopulateClientsDropDownList();
            return View();
        }

        // POST: Order/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,ClientID,RequestDate")] Order order, string[] selectedProducts)
        {
            if (selectedProducts != null)
            {
                order.Samplings = new List<Product>();
                foreach (var product in selectedProducts)
                {
                    var productToAdd = db.Products.Find(int.Parse(product));
                    order.Samplings.Add(productToAdd);
                }
            }
            try
            {
                    if (ModelState.IsValid)
                {
                    db.Orders.Add(order);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateClientsDropDownList(order.ClientID);
            PopulateAssignedProductData(order);
            return View(order);
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders
                .Include(i => i.Samplings)
                .Where(i => i.OrderID == id)
                .Single();
            PopulateAssignedProductData(order);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedProducts)

        //([Bind(Include = "OrderID,ClientID,RequestDate")] Order order)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderToUpdate = db.Orders
               .Include(i => i.Samplings)
               .Where(i => i.OrderID == id)
               .Single();

            if (TryUpdateModel(orderToUpdate, "",
               new string[] { "ClientID", "RequestDate" }))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        UpdateSamplings(selectedProducts, orderToUpdate);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateClientsDropDownList(orderToUpdate.ClientID);
            PopulateAssignedProductData(orderToUpdate);
            return View(orderToUpdate);
        }

        private void UpdateSamplings(string[] selectedProducts, Order orderToUpdate)
        {
            if (selectedProducts == null)
            {
                orderToUpdate.Samplings = new List<Product>();
                return;
            }

            var selectedProductsHS = new HashSet<string>(selectedProducts);
            var samplings = new HashSet<int>
                (orderToUpdate.Samplings.Select(c => c.ID));
            foreach (var product in db.Products)
            {
                if (selectedProductsHS.Contains(product.ID.ToString()))
                {
                    if (!samplings.Contains(product.ID))
                    {
                        orderToUpdate.Samplings.Add(product);
                    }
                }
                else
                {
                    if (samplings.Contains(product.ID))
                    {
                        orderToUpdate.Samplings.Remove(product);
                    }
                }
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private void PopulateClientsDropDownList(object selectedClient = null)
        {
            var clientsQuery = from c in db.Clients
                                   orderby c.Name
                                   select c;
            ViewBag.ClientID = new SelectList(clientsQuery, "ClientID", "Name", selectedClient);
        }

        // GET: Order/Budget/6
        public ActionResult Budget(int? id , int? SelectedClient)
        {
            var clients = db.Clients.OrderBy(q => q.Name).ToList();
            ViewBag.SelectedClient = new SelectList(clients, "ClientID", "Name", SelectedClient);
            int clientID = SelectedClient.GetValueOrDefault();
            var orders = db.Orders.Include(o => o.Client);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        [HttpPost, ActionName("Print")]
        [ValidateAntiForgeryToken]
        public ActionResult PrintProyect(int? id, int? SelectedClient)
        {

            Order order = db.Orders.Find(id);
            var clients = db.Clients.OrderBy(q => q.Name).ToList();
            ViewBag.SelectedClient = new SelectList(clients, "ClientID", "Name", "Email" , SelectedClient);
            int clientID = SelectedClient.GetValueOrDefault();
            var clientEmail = ViewBag.SelectedClient.Email;

            dynamic email = new Email("Budget");
            email.To = clientEmail;
            email.Send();
            return View();
        }




        private void PopulateAssignedProductData(Order order)
        {
            var allProducts = db.Products;
            var Samplings = new HashSet<int>(order.Samplings.Select(c => c.ID));
            var viewModel = new List<AssignedProductData>();
            foreach (var product in allProducts)
            {
                viewModel.Add(new AssignedProductData
                {
                    ProductID = product.ID,
                    Type = product.Type,
                    Brand = product.Brand,
                    Assigned = Samplings.Contains(product.ID)
                });
            }
            ViewBag.Products = viewModel;
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
