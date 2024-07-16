using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;

namespace ApiReportes.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public int? Edad { get; set; }
    public string? Puesto { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    [JsonIgnore]
    public virtual ICollection<Reporte> Reportes { get; set; } = new List<Reporte>();
}
