using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreDataAPI.Context;
using CoreDataAPI.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace CoreDataAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("dotnet-api/formvertice")]
    public class GetDataForm : ControllerBase
    {
        private readonly AppDBContext _context;

        public GetDataForm(AppDBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Obtiene las unidades para un proyecto específico.
        /// </summary>
        /// <param name="idproyecto">ID del proyecto.</param>
        /// <returns>Lista de unidades relacionadas con el proyecto.</returns>
        [HttpGet("unidades/{idproyecto}")]
        [ProducesResponseType(typeof(IEnumerable<UnidadDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UnidadDTO>>> GetUnidades(int idproyecto)
        {
            try
            {
                var unidades = await (from inv in _context.Inventarios
                                      join un in _context.Unidades
                                      on inv.SkUnidad equals un.SkUnidad
                                      where inv.SkProyecto == idproyecto
                                      select new UnidadDTO
                                      {
                                          SkUnidad = un.SkUnidad,
                                          StrAptUnidad = un.StrAptUnidad,
                                          StrTipologia = !string.IsNullOrEmpty(un.StrTipologia) && un.StrTipologia.Length == 1 && char.IsLetter(un.StrTipologia[0]) ? un.StrTipologia : "F"
                                      }).ToListAsync();
                if (unidades == null || unidades.Count == 0)
                {
                    return NotFound("No se encontraron unidades para el proyecto especificado.");
                }
                return Ok(unidades);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        /// <summary>
        /// Obtiene todos los proyectos.
        /// </summary>
        /// <returns>Lista de proyectos.</returns>
        [HttpGet("proyectos")]
        [ProducesResponseType(typeof(IEnumerable<ProyectoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProyectoDTO>>> GetProyectos()
        {
            try
            {
                var projects = await (from pj in _context.Proyectos
                                      select new ProyectoDTO
                                      {
                                          SkProyecto = pj.SkProyecto,
                                          StrNombreProyecto = pj.StrNombreProyecto,
                                          StrTipoProyecto = pj.StrTipoProyecto
                                      }).ToListAsync();
                if (projects == null || projects.Count == 0)
                {
                    return NotFound("No se encontraron proyectos.");
                }
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        /// <summary>
        /// Obtiene todos los proyectos.
        /// </summary>
        /// <param name="skUbicacion">Identificador de la ubicación.</param>
        /// <param name="typeProject">Tipo de proyecto.</param>
        /// <param name="typeEntrega">Tipo de entrega.</param>
        /// <returns>Lista de elementos.</returns>
        [HttpGet("elementos-ubicacion/{skUbicacion}/{typeProject}/{typeEntrega}")]
        [ProducesResponseType(typeof(IEnumerable<ElementoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ElementoDTO>>> GetProyectos(int skUbicacion, string typeProject, string typeEntrega)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(typeProject) || string.IsNullOrWhiteSpace(typeEntrega))
                {
                    return BadRequest("El tipo de proyecto y el tipo de entrega no pueden estar vacíos.");
                }

                typeProject = Uri.UnescapeDataString(typeProject);
                typeEntrega = Uri.UnescapeDataString(typeEntrega);

                var elements = await (from el in _context.Elementos
                                      join ru_el in _context.RelacionUbicacionElementos
                                      on el.SkElemento equals ru_el.SkElemento
                                      where ru_el.SkUbicacionesUnidad == skUbicacion
                                        && ru_el.StrTipoEntrega == typeEntrega
                                        && ru_el.StrTipoProyecto == typeProject
                                      select new ElementoDTO
                                      {
                                          SkElemento = el.SkElemento,
                                          StrNombreElemento = el.StrNombreElemento,
                                      }).ToListAsync();

                if (elements == null || elements.Count == 0)
                {
                    return NotFound("No se encontraron elementos.");
                }

                return Ok(elements);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        /// <summary>
        /// Obtiene los detalles asociados a una categoría específica.
        /// </summary>
        /// <param name="skCategoria">ID de la categoría para la cual se desean obtener los detalles.</param>
        /// <returns>Una lista de detalles asociados a la categoría especificada.</returns>
        [HttpGet("detalles-categoria/{skCategoria}")]
        [ProducesResponseType(typeof(IEnumerable<DetalleDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DetalleDTO>>> GetDetalle(int skCategoria)
        {
            try
            {
                var detalle = await (from det in _context.Detalles
                                     join cat in _context.Categorias
                                     on det.SkCategoria equals cat.SkCategoria
                                     where cat.SkCategoria == skCategoria
                                     select new DetalleDTO
                                     {
                                         SkDetalle = det.SkDetalle,
                                         StrNombreDetalle = det.StrNombreDetalle
                                     }).ToListAsync();

                if (detalle == null || detalle.Count == 0)
                {
                    return NotFound("No se encontraron detalles para la categoría especificada.");
                }

                return Ok(detalle);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        /// <summary>
        /// Obtiene todos los proyectos.
        /// </summary>
        /// <returns>Lista de elementos.</returns>
        [HttpGet("categorias")]
        [ProducesResponseType(typeof(IEnumerable<CategoriaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoria()
        {
            try
            {
                var categoria = await (from cat in _context.Categorias
                                       select new CategoriaDTO
                                       {
                                           SkCategoria = cat.SkCategoria,
                                           StrNombreCategoria = cat.StrNombreCategoria
                                       }).ToListAsync();

                if (categoria == null || categoria.Count == 0)
                {
                    return NotFound("No se encontraron categorias.");
                }

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        /// <summary>
        /// Obtiene las ubicaciones de unidades para una tipología específica.
        /// </summary>
        /// <param name="tipologia">Tipología a filtrar (A, B, C, D, E, F).</param>
        /// <returns>Lista de ubicaciones de unidades correspondientes.</returns>
        [HttpGet("ubicaciones-unidad/{tipologia}/{typeProject}")]
        [ProducesResponseType(typeof(IEnumerable<UbicacionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UbicacionDTO>>> GetUbicacionesUnidades(string tipologia, string typeProject)
        {
            try
            {
                string ValidateTipologia(string tipologia)
                {

                    return new[] { "A", "B", "C", "D", "E", "F" }.Contains(tipologia) ? tipologia : "F";
                }

                string tipologiaValidada = ValidateTipologia(tipologia);

                var ubicaciones = await (from rt in _context.RelacionTipologiaUbicaciones
                                         join uu in _context.UbicacionesUnidades on rt.SkUbicacion equals uu.SkUbicacion
                                         where rt.StrTipologia == tipologiaValidada
                                         && new[] { "AMBOS", typeProject }.Contains(uu.StrTipoProyecto)
                                         select new UbicacionDTO
                                         {
                                             SkUbicacion = uu.SkUbicacion,
                                             StrNombreUbicacion = uu.StrNombreUbicacion
                                         }).ToListAsync();
                if (ubicaciones == null || ubicaciones.Count == 0)
                {
                    return NotFound("No se encontraron ubicaciones para la tipología especificada.");
                }
                return Ok(ubicaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
