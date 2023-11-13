using System;
using System.Collections.Generic;
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
    public class RolService : IRolService
    {
        private readonly IGenericRepositorio<Rol> _rolRepositorio;
        private readonly IMapper _mapper;

        public RolService(IGenericRepositorio<Rol> rolRepositorio, IMapper mapper)
        {
            _rolRepositorio = rolRepositorio;
            _mapper = mapper;
        }

        public async Task<List<RolDTO>> Lista()
        {
            try
            {
                var listaRoles = await _rolRepositorio.Consultar();
                return _mapper.Map<List<RolDTO>>(listaRoles.ToList());
            }
            catch 
            {

                throw;
            }
        }
    }
}
