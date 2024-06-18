using System;
using System.Collections.Generic;

namespace AutoVidaSv.Models;

public partial class Detallemovimiento
{
    public int Detallemovimientoid { get; set; }

    public int? Autoid { get; set; }

    public int? Rentaid { get; set; }

    public int? Ventaid { get; set; }

    public DateOnly? Fecha { get; set; }

    public int? Usuarioid { get; set; }

    public int? Facturacionid { get; set; }

    public virtual Carro? Auto { get; set; }

    public virtual ICollection<Facturacion> Facturacions { get; set; } = new List<Facturacion>();

    public virtual Renta? Renta { get; set; }

    public virtual Venta? Venta { get; set; }
}
