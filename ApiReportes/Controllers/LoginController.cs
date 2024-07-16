using ApiReportes.Dto;
using ApiReportes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiReportes.Service;
using ApiReportes.Data;
using Azure;

namespace ApiReportes.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ReportesContext _DBcontext;
        private readonly JwtService _jwtService;

        public LoginController(ReportesContext DBcontext, JwtService Jwtconfiguration)
        {
            _DBcontext = DBcontext;
            _jwtService = Jwtconfiguration;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLogin usuarioLogin)
        {
            var user = await _DBcontext.Usuarios.FirstOrDefaultAsync(x => x.Email == usuarioLogin.Email);

            try
            {

                if (user == null || !BCrypt.Net.BCrypt.Verify(usuarioLogin.Password, user.Password))
                    return Unauthorized("Algo salió mal, email o contraseña incorrectos");

                var token = _jwtService.GenerateToken(user);
                var response = UsuarioResponse.BuildFromUsuario(user, token);

                return StatusCode(StatusCodes.Status200OK, new { response });

            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status401Unauthorized, new { mensaje = ex.Message }); }

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(Usuario usuario)
        {
            try
            {
                usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
                _DBcontext.Usuarios.Add(usuario);
                await _DBcontext.SaveChangesAsync();

                return Ok("Usuario registrado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex });
            }


        }


    }
}
