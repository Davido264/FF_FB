﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturacion.Model;
namespace Facturacion.DAL.Repositorios.Contrato
{
    public interface IVentaRepository : IGenericRepositorio<Venta>
    {
        Task<Venta> Registrar(Venta modelo);

    }
}
