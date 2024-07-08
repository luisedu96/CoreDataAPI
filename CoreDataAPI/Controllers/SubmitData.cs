using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Microsoft.AspNetCore.Authorization;
using CoreDataAPI.Context;
using CoreDataAPI.Models;
using CoreDataAPI.DTOs;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace CoreDataAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("dotnet-api/formvertice")]
    public class SubmitData : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;
        private readonly string _containerPath;

        public SubmitData(BlobServiceClient blobServiceClient, IConfiguration configuration, AppDBContext context)
        {
            _context = context;
            _blobServiceClient = blobServiceClient;
            _containerName = Environment.GetEnvironmentVariable("ASPNETCORE_AZBLOBS_CONTAINERNAME") ?? throw new ApplicationException("AzureBlobStorage:ContainerName not found in configuration.");
            _containerPath = Environment.GetEnvironmentVariable("ASPNETCORE_AZBLOBS_PATH") ?? throw new ApplicationException("AzureBlobStorage:ContainerPath not found in configuration.");
        }
        /// <summary>
        /// Sube la información de una unidad y la guarda en la base de datos.
        /// </summary>
        /// <param name="request">Objeto que contiene la información de la unidad.</param>
        /// <returns>Una acción con el resultado de la operación.</returns>
        /// <response code="200">La información de la unidad se subió correctamente y devuelve el ID generado.</response>
        /// <response code="400">Error en el formato de los datos enviados.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPost("submit-info-unidad")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostRequestInfoUnidad([FromBody] RequestUnidadDTO request)
        {
            try
            {
                string datePattern = @"^\d{2}/\d{2}/\d{2} - \d{2}:\d{2}$";
                if (!Regex.IsMatch(request.FechaEntrega, datePattern))
                {
                    return BadRequest("El formato de la fecha de entrega es incorrecto. Debe ser 'dd/MM/yy - HH:mm'.");
                }

                var data = new RequestInfoUnidad
                {
                    StrNombreUser = request.ResponsableName,
                    StrEmail = request.ResponsableEmail,
                    StrTipoEntrega = request.TipoEntrega,
                    StrTipoProyecto = request.TipoProyecto,
                    StrProyecto = request.Proyecto,
                    StrUnidad = request.Apartamento,
                    DateFechaProgramada = request.FechaProgramada,
                    DateFechaEntrega = DateTime.ParseExact(request.FechaEntrega, "dd/MM/yy - HH:mm", CultureInfo.InvariantCulture),
                };

                _context.RequestInfoUnidades.Add(data);
                await _context.SaveChangesAsync();

                return Ok(new { Id = data.SkInfoUnidad, Message = "Información enviada exitosamente." });
            }
            catch (FormatException ex)
            {
                return BadRequest($"Error en al enviar información: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        /// <summary>
        /// Sube la información de una ubicación.
        /// </summary>
        /// <param name="request">Objeto que contiene la información de la ubicación.</param>
        /// <returns>Un mensaje de éxito o error.</returns>
        /// <response code="200">La información se subió correctamente.</response>
        /// <response code="400">Error en el formato de los datos enviados.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPost("submit-info-ubicacion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostRequestfoUbicacion([FromBody] RequestUbicacionDTO request)
        {
            try
            {
                var repet = await _context.RequestInfoUbicaciones
                    .Where(u => u.StrNombreUbicacion == request.Ubicacion && u.SkInfoUnidad == request.SkUnidad)
                    .Select(u => new { u.SkInfoUnidad }).FirstOrDefaultAsync();
                if (repet != null)
                {
                    return Ok(new { Id = repet.SkInfoUnidad, Message = "Información existente." });
                }
                var data = new RequestInfoUbicacion
                {
                    SkInfoUnidad = request.SkUnidad,
                    StrNombreUbicacion = request.Ubicacion,
                };
                _context.RequestInfoUbicaciones.Add(data);
                await _context.SaveChangesAsync();
                return Ok(new { Id = data.SkInfoUbicacion, Message = "Información enviada exitosamente." });
            }
            catch (FormatException ex)
            {
                return BadRequest($"Error en al enviar información: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        /// <summary>
        /// Sube la información de un elemento.
        /// </summary>
        /// <param name="request">Objeto que contiene la información del elemento.</param>
        /// <returns>Un mensaje de éxito o error.</returns>
        /// <response code="200">La información se subió correctamente.</response>
        /// <response code="400">Error en el formato de los datos enviados.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPost("submit-info-elemento")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostRequestInfoElemento([FromBody] RequestElementoDTO request)
        {
            try
            {
                var repet = await _context.RequestInfoElementos
                    .Where(u => u.StrNombreElemento == request.Elemento && u.SkInfoUbicacion == request.SkUbicacion)
                    .Select(u => new { u.SkInfoElemento }).FirstOrDefaultAsync();
                if (repet != null)
                {
                    return Ok(new { Id = repet.SkInfoElemento, Message = "Información existente." });
                }
                var data = new RequestInfoElemento
                {
                    SkInfoUbicacion = request.SkUbicacion,
                    StrNombreElemento = request.Elemento,
                };
                _context.RequestInfoElementos.Add(data);
                await _context.SaveChangesAsync();
                return Ok(new { Id = data.SkInfoElemento, Message = "Información enviada exitosamente." });
            }
            catch (FormatException ex)
            {
                return BadRequest($"Error en al enviar información: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        /// <summary>
        /// Sube la información de una categoría y la imagen asociada a Azure Blob Storage.
        /// </summary>
        /// <param name="request">Objeto que contiene la información de la categoría y la imagen en base64.</param>
        /// <returns>Un mensaje de éxito o error.</returns>
        /// <response code="200">La información se subió correctamente.</response>
        /// <response code="400">Error en el formato de los datos enviados.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPost("submit-info-categoria")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostRequestInfoCategoria([FromBody] RequestCategoriaDTO request)
        {
            try
            {
                string? blobUrl = null;
                string? observation = null;
                if(!string.IsNullOrWhiteSpace(request.Observacion))
                {
                    observation = request.Observacion;
                }
                if (!string.IsNullOrWhiteSpace(request.ImageBase64))
                {
                    byte[] imageStream = Convert.FromBase64String(request.ImageBase64);
                    using (var image = Image.Load(imageStream))
                    {
                        if (Image.DetectFormat(imageStream).Name.ToLower().Equals("jpeg"))
                        {
                            image.Mutate(x => x.AutoOrient());
                        }
                        else
                        {
                            return BadRequest($"Error en el formato de la imagen. Formato aceptado: JPEG");
                        }
                        using (var correctedStream = new MemoryStream())
                        {
                            image.SaveAsJpeg(correctedStream);
                            correctedStream.Position = 0;
                            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
                            var blobClient = containerClient.GetBlobClient($"{_containerPath}/v2_{DateTime.Now:dd_MM_yyyy_HH_mm_ss}.jpg");
                            await blobClient.UploadAsync(correctedStream, true);
                            blobUrl = blobClient.Uri.ToString();
                        }
                    }
                }
                var data = new RequestInfoCategoria
                {
                    StrNombreCategoria = request.Categoria,
                    StrNombreDetalle = request.Detalle,
                    StrObservacion = observation,
                    StrUriImagen = blobUrl,
                    SkInfoElemento = request.SkElemento
                };
                _context.RequestInfoCategorias.Add(data);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Información enviada exitosamente." });
            }
            catch (FormatException ex)
            {
                return BadRequest($"Error en la petición: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}