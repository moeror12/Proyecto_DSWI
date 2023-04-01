using Proyecto_DSWI.DAO;
using Proyecto_DSWI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Proyecto_DSWI.Controllers
{
    public class SeguridadController : Controller
    {
        private readonly UsuarioDao dao = new UsuarioDao();
        public ActionResult Login()
        {
            Usuario u = new Usuario();
            return View(u);
        }
        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            Usuario uSession = dao.Login(usuario);
            if (uSession == null)
            {
                ViewBag.ErrorLogin = "El usuario o contraseña son incorrectos";
                return View(usuario);
            }
            Session["usuario"] = uSession;
            return RedirectToAction("Index", "Producto");
        }
        [HttpGet]
        public ActionResult CerrarSession()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login", "Seguridad");
        }
    }
}
