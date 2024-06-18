using System;
using System.Collections.Generic;

namespace AutoVidaSv.Models;

public partial class Renta
{
    public int Rentaid { get; set; }

    public DateOnly? Fechaentrega { get; set; }

    public DateOnly? Fechadevolucion { get; set; }

    public int? Totaldedias { get; set; }

    public int? Agregardiasextra { get; set; }

    public decimal? Totalacancelar { get; set; }

    public virtual ICollection<Detallemovimiento> Detallemovimientos { get; set; } = new List<Detallemovimiento>();
}
