using System;
using System.Collections.Generic;

namespace AutoVidaSv.Models;

public partial class Facturacion
{
    public int Facturacionid { get; set; }

    public DateOnly? Fecha { get; set; }

    public int? Usuairoid { get; set; }

    public int? Detallemovimientoid { get; set; }

    public virtual Detallemovimiento? Detallemovimiento { get; set; }

    public virtual Usuario? Usuairo { get; set; }
}
