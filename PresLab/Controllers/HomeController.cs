using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using PresLab.DAL;
using PresLab.Models;

namespace PresLab.Controllers
{
    public class HomeController : Controller
    {
        private PresLabContext db = new PresLabContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoLogin(string mail, string password)
        {
            
            User user = db.Users.FirstOrDefault(u => u.Mail.Equals(mail));
            if (user != null
                && user.Password.Equals(password)) 
            {
                Session["LoggedUser"] = user; 
                return RedirectToAction("Welcome", "Home");
            }
            
            return RedirectToAction("Index", "Home");
        }



        public ActionResult Welcome()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }

        [HttpPost]
        public ActionResult GetContact(string name, string email, string phone, string message)
        {
            
            ViewBag.Name = name;
            ViewBag.Email = email;
            ViewBag.Phone = phone;
            ViewBag.Message = message;

           
            SmtpClient clienteSmtp = new SmtpClient("smtp-mail.outlook.com", 587);
            clienteSmtp.Credentials = new NetworkCredential("preslabtest@outlook.com", "123Testing");
            clienteSmtp.EnableSsl = true;

            
            MailMessage mailToAdmin = new MailMessage();
            mailToAdmin.From = new MailAddress("preslabtest@outlook.com", "Prueba PresLab");
            mailToAdmin.To.Add("preslabtest@outlook.com");
            mailToAdmin.Subject = "Nuevo Contacto";
            mailToAdmin.Body = name + "(" + email + "), Te.:" + phone + "desea recibir más información sobre PresLab.\nSu mensaje fue: " + message;

            
            clienteSmtp.Send(mailToAdmin);

            
            MailMessage mailToUser = new MailMessage();
            mailToUser.From = new MailAddress("preslabtest@outlook.com", "Prueba PresLab");
            mailToUser.To.Add(email);
            mailToUser.Subject = "¡Gracias por contactarse con nosotros!";
            mailToUser.IsBodyHtml = true;
            mailToUser.Body = "Hola <b>" + name + "</b>!<br>Gracias por contactarse con nosotros!<br>Estará recibiendo la información solicitada a la brevedad.<br>Saludos Cordiales<br>Equipo de <b>PresLab</b><img src=\"~/ Content / img / mob_lab_banner1.jpg\" />";

            
            clienteSmtp.Send(mailToUser);
            
            return RedirectToAction("Index", "Home");
        }

    }
}