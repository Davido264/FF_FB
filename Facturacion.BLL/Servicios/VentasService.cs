using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Facturacion.BLL.Servicios.Contratro;
using Facturacion.DAL.Repositorios.Contrato;
using Facturacion.DTO;
using Facturacion.Model;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.BLL.Servicios
{
    public class VentasService : IVentaService
    {
        private readonly IVentaRepository _ventaRepositorio;
        private readonly IGenericRepositorio<DetalleVenta> _detalleVentaRepositorio;
        private readonly IMapper _mapper;

        public VentasService(IVentaRepository ventaRepositorio, IGenericRepositorio<DetalleVenta> detalleVentaRepositorio, IMapper mapper)
        {
            _ventaRepositorio = ventaRepositorio;
            _detalleVentaRepositorio = detalleVentaRepositorio;
            _mapper = mapper;
        }

        public async Task<VentaDTO> Resgistrar(VentaDTO modelo)
        {
            try
            {
                var ventaGenerada = await _ventaRepositorio.Registrar(_mapper.Map<Venta>(modelo));

                if (ventaGenerada.IdVenta == 0) 
                    throw new TaskCanceledException("No se pudo crear");

                var DTOGenerado = _mapper.Map<VentaDTO>(ventaGenerada);
                DTOGenerado.Cliente = modelo.Cliente;
                return DTOGenerado;
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Venta> query = await _ventaRepositorio.Consultar();
            try
            {
                if(buscarPor == "fecha")
                {
                    DateTime fech_Incio = DateTime.ParseExact(fechaInicio,"dd/MM/yyyy",new CultureInfo("es-ECU"));
                    DateTime fech_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-ECU"));

                    return await query.Where(v =>
                            v.FechaRegistro.Value.Date >= fech_Incio.Date &&
                            v.FechaRegistro.Value.Date <= fech_Fin.Date
                        )
                        .Include(dv => dv.CedulaClienteNavigation)
                        .Include(dv => dv.DetalleVenta)
                        .ThenInclude(p => p.IdProductoNavigation)
                        .Select(e => _mapper.Map<VentaDTO>(e))
                        .ToListAsync();
                }

                else
                {
                    return await query.Where(v => v.NumeroDocumento == numeroVenta)
                        .Include(dv => dv.CedulaClienteNavigation)
                        .Include(dv => dv.DetalleVenta)
                        .ThenInclude(p => p.IdProductoNavigation)
                        .Select(e => _mapper.Map<VentaDTO>(e))
                        .ToListAsync();
                }
            }
            catch
            {

                throw;
            }
        }

        public async Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin)
        {
            IQueryable<DetalleVenta> query = await _detalleVentaRepositorio.Consultar();

            try
            {
                DateTime fech_Incio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-ECU"));
                DateTime fech_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-ECU"));

                return await query
                    .Include(p => p.IdProductoNavigation)
                    .Include(v => v.IdVentaNavigation)
                    .Where(dv => 
                     dv.IdVentaNavigation.FechaRegistro.Value.Date >= fech_Incio.Date &&
                     dv.IdVentaNavigation.FechaRegistro.Value.Date <= fech_Fin.Date
                    )
                    .Select(e => _mapper.Map<ReporteDTO>(e))
                    .ToListAsync();
            }
            catch
            {

                throw;
            }
        }
    }
}
