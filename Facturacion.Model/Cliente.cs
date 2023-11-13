using System;
using System.Collections.Generic;

namespace Facturacion.Model;

public partial class Cliente
{
    public string CedulaCliente { get; set; } = null!;

    public string? NombreCompleto { get; set; }

    public string? Correo { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
