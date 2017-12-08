using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresLab.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Welcome()
        {
            ViewBag.Message = "Your application welcome page.";

            return View();
        }

        public ActionResult ClientsAll()
        {
            ViewBag.Message = "Muestra todos los clientes";

            return View();
        }

        public ActionResult ClientsNew()
        {
            ViewBag.Message = "Formulario nuevo cliente";

            return View();
        }

        public ActionResult ProductsAll()
        {
            ViewBag.Message = "Muestra todos los Productos";

            return View();
        }

        public ActionResult ProductsNew()
        {
            ViewBag.Message = "Formulario nuevo producto";

            return View();
        }

        public ActionResult Sampling()
        {
            ViewBag.Message = "Your sampling page.";

            return View();
        }


    }
}