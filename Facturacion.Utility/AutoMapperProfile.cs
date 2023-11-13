using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Facturacion.DTO;
using Facturacion.Model;
namespace Facturacion.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CultureInfo cultureInfo = CultureInfo.InvariantCulture;
            // var numberInfo = new NumberFormatInfo();
            // numberInfo.NumberDecimalSeparator = ".";
            // cultureInfo.NumberFormat = numberInfo;

            #region Rol
            CreateMap<Rol,RolDTO>().ReverseMap();
            #endregion Rol

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion Menu

            #region Cliente
            CreateMap<Cliente,ClienteDTO>().ReverseMap();
            #endregion Cliente

            #region Usuario
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino =>
                        destino.RolDescription,
                        opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                        )
                .ForMember(destino =>
                        destino.EsActivo,
                        opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                        );


            CreateMap<Usuario, SesionDTO>()
                .ForMember(destino =>
                        destino.RolDescription,
                        opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                        );

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino =>
                        destino.IdRolNavigation,
                        opt => opt.Ignore()
                        )
                .ForMember(destino =>
                        destino.EsActivo,
                        opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                        );

            #endregion Usuario

            #region Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            #endregion Categoria

            #region Producto
            CreateMap<Producto, ProductoDTO>()
                .ForMember(destino =>
                        destino.CategoriaDescription,
                        opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Nombre)
                        )
                .ForMember(destino =>
                        destino.Precio,
                        opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, cultureInfo))
                        )

                .ForMember(destino =>
                        destino.EsActivo,
                        opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)   
                        );

            CreateMap<ProductoDTO, Producto>()
                .ForMember(destino =>
                        destino.IdCategoriaNavigation,
                        opt => opt.Ignore()
                        )
                .ForMember(destino =>
                        destino.Precio,
                        opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, cultureInfo))
                        )

                .ForMember(destino =>
                        destino.EsActivo,
                        opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                        );
                        #endregion Producto

            #region Venta

            CreateMap<Venta, VentaDTO>()
                .ForMember(destino =>
                        destino.TotalTexto,
                        opt => opt.MapFrom(origen => Convert.ToString(origen.Total.GetValueOrDefault()))
                        )
                .ForMember(destino =>
                        destino.Cliente,
                        opt => opt.MapFrom(origen => origen.CedulaClienteNavigation)
                        )
                .ForMember(destino =>
                        destino.FechaRegistro,
                        opt => opt.MapFrom(orgien => orgien.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                        );

            CreateMap<VentaDTO, Venta>()
                .ForMember(destino =>
                        destino.CedulaCliente,
                        opt => opt.MapFrom(origen => origen.Cliente.CedulaCliente)
                        )
                .ForMember(destino =>
                        destino.Total,
                        opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, cultureInfo))
                        );

            #endregion Venta

            #region DetalleVenta
            CreateMap<DetalleVenta, DetalleVentaDTO>()
                .ForMember(destino =>
                        destino.ProductoDescription,
                        opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
                        )
                .ForMember(destino =>
                        destino.PrecioTexto,
                        opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, cultureInfo))
                        )

                .ForMember(destino =>
                        destino.TotalTexto,
                        opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, cultureInfo))
                        );

            CreateMap<DetalleVentaDTO, DetalleVenta>()

                .ForMember(destino =>
                        destino.Precio,
                        opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PrecioTexto, cultureInfo))
                        )

                .ForMember(destino =>
                        destino.Total,
                        opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, cultureInfo))
                        );




            #endregion DetalleVenta

            #region Reporte
            CreateMap<DetalleVenta, ReporteDTO>()
                .ForMember(destino =>
                        destino.FechaRegistro,
                        opt => opt.MapFrom(orgien => orgien.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                        )
                .ForMember(destino =>
                        destino.NumeroDocumento,
                        opt => opt.MapFrom(orgien => orgien.IdVentaNavigation.NumeroDocumento)
                        )
                .ForMember(destino =>
                        destino.TipoPago,
                        opt => opt.MapFrom(orgien => orgien.IdVentaNavigation.TipoPago)
                        )
                .ForMember(destino =>
                        destino.TotalVenta,
                        opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, cultureInfo))
                        )

                .ForMember(destino =>
                        destino.Producto,
                        opt => opt.MapFrom(orgien => orgien.IdProductoNavigation.Nombre)
                        )

                .ForMember(destino =>
                        destino.Precio,
                        opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, cultureInfo))
                        )

                .ForMember(destino =>
                        destino.Total,
                        opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, cultureInfo))
                        );



            #endregion Reporte
        }
    }
}
