using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.DTO
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }

        public string? Nombre { get; set; }

        public int? IdCategoria { get; set; }

        public string? CategoriaDescription { get; set; }

        public int? Stock { get; set; }

        public string? Precio { get; set; }

        public int? EsActivo { get; set; }

        public string? UrlImage { get; set; }
    }
}
