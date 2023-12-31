﻿using System;
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
    public class MenuService : IMenuService
    {
        private readonly IGenericRepositorio<Usuario> _usuarioRepositorio;
        private readonly IGenericRepositorio<MenuRol> _menuRolRepositorio;
        private readonly IGenericRepositorio<Menu> _menuRepositorio;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepositorio<Usuario> usuarioRepositorio, IGenericRepositorio<MenuRol> menuRolRepositorio, IGenericRepositorio<Menu> menuRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _menuRolRepositorio = menuRolRepositorio;
            _menuRepositorio = menuRepositorio;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> Lista(int idUsuario)
        {
            IQueryable<Usuario> tbUsuario = await _usuarioRepositorio.Consultar(u => u.IdUsuario == idUsuario);
            IQueryable<MenuRol> tbMenuRol = await _menuRolRepositorio.Consultar();
            IQueryable<Menu> tbMenu = await _menuRepositorio.Consultar();
            try
            {
                IQueryable<Menu> tbResultado = (from u in tbUsuario
                                                join mr in tbMenuRol on u.IdRol equals mr.IdRol
                                                join m in tbMenu on mr.IdMenu equals m.IdMenu
                                                select m).AsQueryable();
                var listaMenus = tbResultado.ToList();

                return _mapper.Map<List<MenuDTO>>(listaMenus);
            }
            catch 
            {

                throw;
            }


        }
    }
}
