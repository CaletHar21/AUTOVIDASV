using System;
using System.Collections.Generic;

namespace AutoVidaSv.Models;

public partial class Venta
{
    public int Ventaid { get; set; }

    public int? Autoid { get; set; }

    public DateOnly? Fechaventa { get; set; }

    public decimal? Valorventa { get; set; }

    public virtual ICollection<Detallemovimiento> Detallemovimientos { get; set; } = new List<Detallemovimiento>();
}
