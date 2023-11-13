using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturacion.DTO;



namespace Facturacion.BLL.Servicios.Contratro
{
    public interface IRolService
    {
        Task<List<RolDTO>> Lista();
    }
}
