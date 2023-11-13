using Facturacion.DAL.BDContext;
using Facturacion.DAL.Repositorios.Contrato;
using Facturacion.Model;

namespace Facturacion.DAL.Repositorios
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
         private readonly BdfacturacionContext _bdcontext;

        public VentaRepository(BdfacturacionContext bdcontext): base(bdcontext)
        {
            _bdcontext = bdcontext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
           Venta ventaGenerada = new Venta();
           using(var transaction = _bdcontext.Database.BeginTransaction())
           {
               try
               {
                   foreach (DetalleVenta dv in modelo.DetalleVenta)
                   {
                       Producto producto_encontrado = _bdcontext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();
                       producto_encontrado.Stock = producto_encontrado.Stock - dv.Cantidad;
                       _bdcontext.Productos.Update(producto_encontrado);
                   }
                   await _bdcontext.SaveChangesAsync();

                   NumeroDocumento correlativo = _bdcontext.NumeroDocumentos.First();
                   correlativo.UltimoNumero = correlativo.UltimoNumero + 1; 
                   correlativo.FechaRegistro = DateTime.Now;

                   _bdcontext.NumeroDocumentos.Update(correlativo);
                   await _bdcontext.SaveChangesAsync();

                   int CantidadDigitos = 4;
                   string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                   string numeroVenta = ceros + correlativo.UltimoNumero.ToString();

                   numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos,CantidadDigitos);

                   modelo.NumeroDocumento = numeroVenta;

                   await _bdcontext.Venta.AddAsync(modelo);
                   await _bdcontext.SaveChangesAsync();

                   ventaGenerada = modelo;

                   transaction.Commit();


               }
               catch 
               {
                   transaction.Rollback();
                   throw;
               }
               return ventaGenerada;
           }
        }
    }
}
