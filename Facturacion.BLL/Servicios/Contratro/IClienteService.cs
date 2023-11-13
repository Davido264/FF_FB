using Facturacion.DTO;

namespace Facturacion.BLL.Servicios.Contratro
{
    public interface IClienteService
    {
        Task<List<ClienteDTO>> Lista();
        Task<ClienteDTO> Crear(ClienteDTO modelo);
        Task<bool> Editar(ClienteDTO modelo);
        Task<bool> Eliminar(string cedula);
    }
}
