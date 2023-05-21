using Proyecto_DSWI.DAO;
using Proyecto_DSWI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_DSWI.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductoDao pDao = new ProductoDao();
        public ActionResult Index()
        {
            var lista = pDao.ListarProducto();
            return View(lista);
        }
        [HttpGet]
        public ActionResult ObtenerProducto(int productoId)
        {
            var obj = pDao.ObtenerProducto(productoId);
            return View(obj);
        }
        [HttpGet]
        public ActionResult ListadoCrud()
        {
            ViewBag.mensaje = TempData["mensaje"];
            var lista = pDao.ListarProducto();
            return View(lista);
        }
        [HttpGet]
        public ActionResult Details(int productoId)
        {
            var producto = pDao.ObtenerProducto(productoId);
            return View(producto);
        }
        [HttpGet]
        public ActionResult Create()
        {
            Producto p = new Producto();
            ViewBag.categorias = new SelectList(pDao.ListarCategorias(), "CategoriaId", "Nombre");
            return View(p);
        }
        [HttpPost]
        public ActionResult Create(Producto p)
        {
            var regex = new Regex(@"^[A-Z0-9\s]*$");
            ViewBag.categorias = new SelectList(pDao.ListarCategorias(), "CategoriaId", "Nombre");
            HttpPostedFileBase file = Request.Files["Foto"];
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    p.Foto = Path.GetFileName(file.FileName);
                    string ruta = Path.Combine(Server.MapPath("~/fotos"), p.Foto);
                    file.SaveAs(ruta);
                }
            }
            if (!regex.IsMatch(p.Nombre)) 
            {
                ViewBag.mensaje = "El nombre del producto tiene que estar en mayúsculas.";
                return View(p);
            }
            //if (!p.Nombre.All(c => char.IsUpper(c)))
            //{
            //    ViewBag.mensaje = "El nombre del producto tiene que estar en mayúsculas.";
            //    return View(p);
            //}
            if (p.Nombre != null && !string.IsNullOrEmpty(p.Nombre))
            {
                int res = pDao.RegistrarProducto(p);
                if (res > 0)
                {
                    TempData["mensaje"] = "Producto registrado correctamente";
                    return RedirectToAction("ListadoCrud", "Producto");
                }
            }
            ViewBag.mensaje = "Error al registrar producto";
            return View(p);
        }
        [HttpGet]
        public ActionResult Edit(int productoId)
        {
            var producto = pDao.ObtenerProducto(productoId);
            ViewBag.categorias = new SelectList(pDao.ListarCategorias(), "CategoriaId", "Nombre");
            return View(producto);
        }
        [HttpPost]
        public ActionResult Edit(Producto p)
        {
            var regex = new Regex(@"^[A-Z0-9\s]*$");
            ViewBag.categorias = new SelectList(pDao.ListarCategorias(), "CategoriaId", "Nombre");
            HttpPostedFileBase file = Request.Files["Foto"];
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    p.Foto = Path.GetFileName(file.FileName);
                    string ruta = Path.Combine(Server.MapPath("~/fotos"), p.Foto);
                    file.SaveAs(ruta);
                }
            }
            if (!regex.IsMatch(p.Nombre))
            {
                ViewBag.mensaje = "El nombre del producto tiene que estar en mayúsculas.";
                return View(p);
            }
            if (p.Nombre != null && !string.IsNullOrEmpty(p.Nombre))
            {
                int res = pDao.ActualizarProducto(p);
                if (res > 0)
                {
                    TempData["mensaje"] = "Producto actualizado correctamente";
                    return RedirectToAction("ListadoCrud", "Producto");
                }
            }           
            ViewBag.mensaje = "Error al actualizar producto";
            return View(p);
        }

        [HttpGet]
        public ActionResult Delete(int productoId)
        {
            var producto = pDao.ObtenerProducto(productoId);
            return View(producto);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int productoId)
        {
            int res = pDao.EliminarProducto(productoId);
            if (res > 0)
            {
                TempData["mensaje"] = "Producto eliminado correctamente";
                return RedirectToAction("ListadoCrud", "Producto");
            }
            ViewBag.mensaje = "Error al eliminar producto";
            return View();
        }
    }
}