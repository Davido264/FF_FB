using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Facturacion.BLL.Servicios.Contratro;
using Facturacion.DTO;
using Facturacion.API.Utilidad;
using Facturacion.API.Authorization;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using Amazon;

namespace Facturacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoServicio;
        private readonly RegionEndpoint _region;
        private readonly IConfiguration _config;

        public ProductoController(IProductoService productoServicio, IConfiguration config)
        {
            _productoServicio = productoServicio;
            _region = RegionEndpoint.USEast2;
            _config = config;
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
            var rsp = new Response<string>();
            var s3 = new AmazonS3Client(_config.GetValue<string>("AwsAccessKey"),_config.GetValue<string>("AwsSecretKey"),_region);
            var imageId = Guid.NewGuid().ToString();
            var request = new GetPreSignedUrlRequest
            {
                BucketName = "proyecto-mobiles",
                Key = imageId,
                Expires = DateTime.UtcNow.AddMinutes(2),
                Verb = HttpVerb.PUT
            };
            var url = s3.GetPreSignedURL(request);

            rsp.status = true;
            rsp.value = url;
            return await Task.FromResult(Ok(rsp));
        }
    }
}
