using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Facturacion.BLL.Servicios.Contratro;
using Facturacion.DTO;
using Facturacion.API.Utilidad;
using Facturacion.API.Authorization;

namespace Facturacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoServicio;

        public ProductoController(IProductoService productoServicio)
        {
            _productoServicio = productoServicio;
        }

        [HttpGet]
        [Route("Lista")]
        [AllowAnonymous]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<ProductoDTO>>();


            try
            {
                rsp.status = true;
                rsp.value = await _productoServicio.Lista();


            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }

            return Ok(rsp);
        }


        [HttpPost]
        [Route("Guardar")]

        public async Task<ActionResult> Guardar([FromBody] ProductoDTO producto)
        {
            var rsp = new Response<ProductoDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _productoServicio.Crear(producto);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(rsp);
        }

        [HttpPut]
        [Route("Editar")]

        public async Task<ActionResult> Editar([FromBody]ProductoDTO producto)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _productoServicio.Editar(producto);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(rsp);
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]

        public async Task<ActionResult> Eliminar(int id)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _productoServicio.Eliminar(id);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(rsp);
        }

        [HttpGet]
        [Route("PermisoS3")]
        [AllowAnonymous]
        public async Task<IActionResult> PermisoS3()
        {
            return Ok();
        }
    }
}
