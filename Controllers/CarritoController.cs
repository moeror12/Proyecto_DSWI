using Proyecto_DSWI.DAO;
using Proyecto_DSWI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_DSWI.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ProductoDao pDao = new ProductoDao();
        private readonly VentaDao vDao = new VentaDao();
        // GET: Carrito        
        public int getIndex(int id)
        {
            List<CarritoItems> compras = (List<CarritoItems>)Session["carrito"];
            for (int i = 0; i < compras.Count; i++)
            {
                if (compras[i].Producto.ProductoId == id)
                {
                    return i;
                }
            }
            return -1;
        }
        public ActionResult ListarCarrito()
        {
            var lista = (List<CarritoItems>)Session["carrito"] == null ? new List<CarritoItems>() : (List<CarritoItems>)Session["carrito"];
            ViewBag.Subtotal = lista.Sum(x => x.Producto.Precio * x.Cantidad);
            ViewBag.Iva = lista.Sum(x => x.Producto.Precio * x.Cantidad) * 0.18M;
            ViewBag.Total = lista.Sum(x => x.Producto.Precio * x.Cantidad) * 1.18M;
            ViewBag.mensaje = TempData["mensaje"];
            return View(lista);
        }
        public ActionResult Agregar(int id, int cantidad)
        {
            if (Session["carrito"] == null)
            {
                List<CarritoItems> compras = new List<CarritoItems>();
                compras.Add(new CarritoItems(pDao.ObtenerProducto(id), cantidad));
                Session["carrito"] = compras;
            }
            else
            {
                List<CarritoItems> compras = (List<CarritoItems>)Session["carrito"];
                int index = getIndex(id);
                //if (pDao.ObtenerProducto(id).Stock > cantidad)
                //{
                    if (index == -1)
                        compras.Add(new CarritoItems(pDao.ObtenerProducto(id), cantidad));
                    else
                        compras[index].Cantidad += cantidad;
                    Session["carrito"] = compras;
            //    }
            //    else 
            //    TempData["mensaje"] = pDao.ObtenerProducto(id).Nombre + " no tiene suficiente stock";
            }
            return Content(Url.Action("ListarCarrito").ToString());
        }
        public ActionResult Actualizar(int id, int cantidad)
        {
            List<CarritoItems> compras = (List<CarritoItems>)Session["carrito"];
            int index = getIndex(id);
            compras[index].Cantidad = cantidad;
            Session["carrito"] = compras;
            return Content(Url.Action("ListarCarrito").ToString());
        }
        public ActionResult Delete(int id)
        {
            List<CarritoItems> compras = (List<CarritoItems>)Session["carrito"];
            compras.RemoveAt(getIndex(id));
            Session["carrito"] = compras;
            return Content(Url.Action("ListarCarrito").ToString());
        }

        public ActionResult CompraFinalizada()
        {
            var lista = (List<CarritoItems>)Session["carrito"];
            if (lista != null)
            {
                ViewBag.Total = lista.Sum(x => x.Producto.Precio * x.Cantidad) * 1.18M;
                Session["carrito"] = new List<CarritoItems>();
                return View();
            }
            return View();
        }

        [HttpPost]
        public ActionResult FinalizarCompra()
        {
            List<CarritoItems> compras = (List<CarritoItems>)Session["carrito"];
            var subtotal = compras.Sum(x => x.Producto.Precio * x.Cantidad);
            var stockRestante = compras.Sum(x => x.Producto.Stock - x.Cantidad);
            var idProducto = compras.Sum(x => x.Producto.ProductoId);
            if (stockRestante >= 0)
            {
                Venta v = new Venta()
                {
                    Subtotal = compras.Sum(x => x.Producto.Precio * x.Cantidad),
                    Iva = subtotal * 0.18M,
                    Total = subtotal * 1.18M
                };
                int res = vDao.RegistrarVenta(v, stockRestante, idProducto);
                if (res > 0)
                {
                    return Content(Url.Action("CompraFinalizada", "Carrito"));
                }
                TempData["mensaje"] = "Error al registrar producto";
            }
            else {
                TempData["mensaje"] = "Producto no tiene suficiente stock";
            }
            return Content(Url.Action("ListarCarrito", "Carrito"));

        }
    }
}
