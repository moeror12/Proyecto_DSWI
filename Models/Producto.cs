using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DSWI.Models
{
    public class Producto
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Foto { get; set; }
        public int CategoriaId { get; set; }
        public string Categoria { get; set; }
        public int Stock { get; set; }
    }
}
