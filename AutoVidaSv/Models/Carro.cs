using System;
using System.Collections.Generic;

namespace AutoVidaSv.Models;

public partial class Carro
{
    public int Autoid { get; set; }

    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public DateOnly? Anio { get; set; }

    public string? Transmicion { get; set; }

    public string? Combustible { get; set; }

    public int? Albumcarid { get; set; }

    public virtual Albumcar? Albumcar { get; set; }

    public virtual ICollection<Detallemovimiento> Detallemovimientos { get; set; } = new List<Detallemovimiento>();
}
