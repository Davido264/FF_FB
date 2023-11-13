using Facturacion.DAL.BDContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Facturacion.DAL.Repositorios.Contrato;
using Facturacion.DAL.Repositorios;
using Facturacion.Utility;
using Facturacion.BLL.Servicios.Contratro;
using Facturacion.BLL.Servicios;

namespace Facturacion.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencias (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BdfacturacionContext>(options =>{
                options.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
            });


            services.AddTransient(typeof(IGenericRepositorio<>), typeof(GenericRepository<>));
            services.AddScoped<IVentaRepository, VentaRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IVentaService, VentasService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IClienteService, ClienteService>();

        }


      

    }
}
