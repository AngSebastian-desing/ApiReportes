using ApiReportes.Models;

namespace ApiReportes.Dto
{
    public class UsuarioResponse
    {
        public string? Token { get; set; }
        public int? IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int? Edad { get; set; }
        public string? Puesto { get; set; }

        public static UsuarioResponse BuildFromUsuario(Usuario user, string token)
        {
            return new UsuarioResponse
            {
                Token = token,
                IdUsuario = user.IdUsuario,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Edad = user.Edad ?? 0,
                Puesto = user.Puesto
            };
        }

    }
}
