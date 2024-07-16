using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using ApiReportes.Models;
using Microsoft.AspNetCore.Cors;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.IdentityModel.Tokens;
using ApiReportes.Data;
namespace ApiReportes.Controllers
{
    [EnableCors("CorsRules")]
    [Route("api/reports")]
    [ApiController]
    public class ReporteController(ReportesContext reporte) : ControllerBase
    {
        public readonly ReportesContext reportesContext = reporte;

        [HttpGet]
        [Route("list")]
        public IActionResult GetList()
        {
            List<Reporte> objLista = [];

            try
            {
                objLista = [.. reportesContext.Reportes.Include(u => u.Usuario)];
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = objLista });
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message, response = objLista }); }

        }

        [HttpGet]
        [Route("report/{folio:int}")]
        public IActionResult GetReport(int folio)
        {
            try
            {
                Reporte objReporte = reportesContext.Reportes.Include(r => r.Usuario).FirstOrDefault(r => r.Folio == folio);
                if (objReporte == null) { return BadRequest("Reporte no encontrado"); }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = objReporte });
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message }); }

        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveReport([FromBody] Reporte reporte)
        {
            try
            {

                reportesContext.Reportes.Add(reporte);
                await reportesContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });

            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status405MethodNotAllowed, new { mensaje = ex.Message }); }
        }

        [HttpPut]
        [Route("report/update")]
        public IActionResult UpdateReport([FromBody] Reporte reporte)
        {

            Reporte objReporte = reportesContext.Reportes.Include(r => r.Usuario).FirstOrDefault(r => r.Folio == reporte.Folio);
            if (objReporte == null) { return BadRequest("El reporte que se desea actualizar no fue no encontrado"); }
            try
            {
                objReporte.Titulo = reporte.Titulo;
                objReporte.Descripcion = reporte.Descripcion;
                objReporte.Fecha = reporte.Fecha;
                objReporte.IdUsuario = reporte.IdUsuario;
                objReporte.Estatus = reporte.Estatus;
                objReporte.Imagen = reporte.Imagen;

                reportesContext.Reportes.Update(objReporte);
                reportesContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });

            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status405MethodNotAllowed, new { mensaje = ex.Message }); }
        }

        [HttpDelete]
        [Route("report/delete/{folio:int}")]
        public IActionResult DeleteReport(int folio)
        {
            try
            {
                Reporte objReporte = reportesContext.Reportes.Find(folio);
                if (objReporte == null) { return BadRequest("El reporte que se desea eliminar no fue encontrado"); }

                reportesContext.Reportes.Remove(objReporte);
                reportesContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message }); }

        }

    }
}
