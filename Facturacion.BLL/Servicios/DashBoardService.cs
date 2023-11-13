﻿using System;
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

namespace Facturacion.BLL.Servicios
{
    public class DashBoardService : IDashBoardService

    {
        private readonly IVentaRepository _ventaRepositorio;
        private readonly IGenericRepositorio<Producto> _productoRepositorio;
        private readonly IMapper _mapper;

        public DashBoardService(IVentaRepository ventaRepositorio, IGenericRepositorio<Producto> productoRepositorio, IMapper mapper)
        {
            _ventaRepositorio = ventaRepositorio;
            _productoRepositorio = productoRepositorio;
            _mapper = mapper;
        }

        private IQueryable<Venta> retornarVentas(IQueryable<Venta> tablaVenta, int restarCantidadDias)
        {
            DateTime? ultimaFecha = tablaVenta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();
            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);

            return tablaVenta.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);

        }
        private async Task<int> TotalVentasUltimaSemana()
        {
            int total = 0;
            IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();

            if(_ventaQuery.Count() > 0)
            {
                var tablaVenta = retornarVentas(_ventaQuery, -7);
                total = tablaVenta.Count();
            }
            return total;
        }

        private async Task<string> TotalIngresosUltimaSemana()
        {
            decimal resultado = 0;

            IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();

            if( _ventaQuery.Count() > 0)
            {
                var tablaVenta = retornarVentas(_ventaQuery, -7);
                resultado = tablaVenta.Select(v => v.Total).Sum(v => v.Value);

            }

            return Convert.ToString(resultado,new CultureInfo("es-ECU"));

        }

        private async Task<int> TotalProductos()
        {
            IQueryable<Producto> _productoQuery = await _productoRepositorio.Consultar();
            int total = _productoQuery.Count();
            return total;
        }

        private async Task<Dictionary<string,int>> VentasUltimaSemana()
        {
            Dictionary<string,int> resultado = new Dictionary<string,int>();
            IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();

            if(_ventaQuery.Count() > 0)
            {
                var tablaVenta = retornarVentas(_ventaQuery, -7);

                resultado = tablaVenta
                    .GroupBy(v => v.FechaRegistro.Value.Date).OrderBy(g => g.Key)
                    .Select(dv => new {fecha = dv.Key.ToString("dd/MM/yyy"), total = dv.Count()   })
                    .ToDictionary(keySelector : r => r.fecha, elementSelector : r => r.total);
            }

            return resultado;
        }

        public async Task<DashBoardDTO> Resumen()
        {
            DashBoardDTO vmdashBoard = new DashBoardDTO();

            try
            {
                vmdashBoard.TotalVentas = await TotalVentasUltimaSemana();
                vmdashBoard.TotalIngresos = await TotalIngresosUltimaSemana();
                vmdashBoard.TotalProductos = await TotalProductos();

                List<VentaSemanaDTO> listaVentaSemana = new List<VentaSemanaDTO>();

                foreach(KeyValuePair<string, int> item in await VentasUltimaSemana())
                {
                    listaVentaSemana.Add(new VentaSemanaDTO()
                    {
                        Fecha = item.Key,
                        Total = item.Value
                    });
                }

                vmdashBoard.VentasUltimaSemana = listaVentaSemana;
            }
            catch             {

                throw;
            }

            return vmdashBoard;
        }
    }
}
