using System;
using System.Collections.Generic;

namespace AutoVidaSv.Models;

public partial class Usuario
{
    public int Usuarioid { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? Correo { get; set; }

    public string? Contrasena { get; set; }

    public int? Rolid { get; set; }

    public virtual ICollection<Facturacion> Facturacions { get; set; } = new List<Facturacion>();
}
