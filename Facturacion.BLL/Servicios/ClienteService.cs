using AutoMapper;
using Facturacion.DAL.Repositorios.Contrato;
using Facturacion.DTO;
using Facturacion.Model;

namespace Facturacion.BLL.Servicios.Contratro
{
    public class ClienteService : IClienteService
    {
        private readonly IGenericRepositorio<Cliente> _clienteRepositorio;
        private readonly IMapper _mapper;

        public ClienteService(IGenericRepositorio<Cliente> clienteRepositorio, IMapper mapper)
        {
            _clienteRepositorio = clienteRepositorio;
            _mapper = mapper;
        }


        public async Task<List<ClienteDTO>> Lista() {
            return (await _clienteRepositorio.Consultar())
                .Select(c => _mapper.Map<ClienteDTO>(c))
                .ToList();
        }

        public async Task<ClienteDTO> Crear(ClienteDTO modelo) {
            var clienteCreado = await _clienteRepositorio.Crear(_mapper.Map<Cliente>(modelo));
            return modelo;
        }

        public async Task<bool> Editar(ClienteDTO modelo) {
            var clienteModelo = _mapper.Map<Cliente>(modelo);

            var usuarioEncontrado = await _clienteRepositorio.Obtener(u => u.CedulaCliente == clienteModelo.CedulaCliente);

            if (usuarioEncontrado == null)
                throw new TaskCanceledException("El cliente no existe");

            usuarioEncontrado.NombreCompleto = clienteModelo.NombreCompleto;
            usuarioEncontrado.Correo = clienteModelo.Correo;
            usuarioEncontrado.Direccion = clienteModelo.Direccion;

            bool respuesta = await _clienteRepositorio.Editar(usuarioEncontrado);

            if (!respuesta)
                throw new TaskCanceledException("No se pudo editar");
            return respuesta;
        }

        public async Task<bool> Eliminar(string cedula) {
            var clienteEncontrado = await _clienteRepositorio.Obtener(u => u.CedulaCliente == cedula);

            if (clienteEncontrado == null) 
                throw new TaskCanceledException("El cliente no existe");

            bool respuesta = await _clienteRepositorio.Eliminar(clienteEncontrado);

            if (!respuesta)
                throw new TaskCanceledException("No se pudo eliminar");
            return respuesta;
        }
    }
}
