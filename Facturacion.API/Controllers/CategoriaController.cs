using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Facturacion.BLL.Servicios.Contratro;
using Facturacion.DTO;
using Facturacion.API.Utilidad;
using Facturacion.BLL.Servicios;
using Facturacion.API.Authorization;

namespace Facturacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaServicio;

        public CategoriaController(ICategoriaService categoriaServicio)
        {
            _categoriaServicio = categoriaServicio;
        }

        [HttpGet]
        [Route("Lista")]
        [AllowAnonymous]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<CategoriaDTO>>();


            try
            {
                rsp.status = true;
                rsp.value = await _categoriaServicio.Lista();


            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }

            return Ok(rsp);
        }

    }
}
