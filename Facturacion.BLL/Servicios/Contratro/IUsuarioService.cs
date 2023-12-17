using Facturacion.DTO;

namespace Facturacion.BLL.Servicios.Contratro
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> Lista();
        Task<UsuarioDTO?> ObtenerPorId(int id);
        Task<SesionDTO> ValidarCredenciales( string correo , string clave);
        Task<UsuarioDTO> Crear(UsuarioDTO modelo);
        Task<bool> Editar(UsuarioDTO modelo);
        Task<bool> Eliminar(int id);
    }
}
