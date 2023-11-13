using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturacion.DAL.Repositorios.Contrato;
using Facturacion.DAL.BDContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Facturacion.DAL.Repositorios
{
    public class GenericRepository<TModelo> : IGenericRepositorio<TModelo> where TModelo : class
    {
        private readonly BdfacturacionContext _bdcontext;

        public GenericRepository(BdfacturacionContext bdcontext)
        {
            _bdcontext = bdcontext;
        }

        public async Task<TModelo> Obtener(Expression<Func<TModelo, bool>> filtro)
        {
            try
            {
                TModelo modelo = await _bdcontext.Set<TModelo>().FirstOrDefaultAsync(filtro);
                return modelo;
            }
            catch 
            {
                throw;
            }
        }

        public async Task<TModelo> Crear(TModelo modelo)
        {
            try
            {
                _bdcontext.Set<TModelo>().Add(modelo);
                await _bdcontext.SaveChangesAsync();
                return modelo;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(TModelo modelo)
        {
            try
            {
                _bdcontext.Set<TModelo>().Update(modelo);
                await _bdcontext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(TModelo modelo)
        {
            try
            {
                _bdcontext.Set<TModelo>().Remove(modelo);
                await _bdcontext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }
        public async Task<IQueryable<TModelo>> Consultar(Expression<Func<TModelo, bool>> filtro = null)
        {
            try
            {
                IQueryable<TModelo> queryModelo = filtro == null? _bdcontext.Set<TModelo>(): _bdcontext.Set<TModelo>().Where(filtro);
                return queryModelo;
            }
            catch
            {
                throw;
            }
        }

    }
}
