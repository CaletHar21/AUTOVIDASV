using System;
using System.Collections.Generic;

namespace AutoVidaSv.Models;

public partial class Albumcar
{
    public int Albumcarid { get; set; }

    public string? Imagen { get; set; }

    public virtual ICollection<Carro> Carros { get; set; } = new List<Carro>();
}
