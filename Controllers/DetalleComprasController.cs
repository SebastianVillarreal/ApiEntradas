using System;
using Microsoft.AspNetCore.Mvc;
using reportesApi.Services;
using reportesApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using reportesApi.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using reportesApi.Helpers;
using Newtonsoft.Json;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Microsoft.AspNetCore.Hosting;


namespace reportesApi.Controllers
{
   
    [Route("api")]
    public class DetalleComprasController: ControllerBase
    {
   
        private readonly DetalleComprasService _articulosService;
        private readonly ILogger<DetalleComprasController> _logger;
  
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        

        Encrypt enc = new Encrypt();

        public DetalleComprasController(DetalleComprasService articulosservice, ILogger<DetalleComprasController> logger, IJwtAuthenticationService authService) {
            _articulosService = articulosservice;
            _logger = logger;
       
            _authService = authService;
            // Configura la ruta base donde se almacenan los archivos.
            // Asegúrate de ajustar la ruta según tu estructura de directorios.

            
            
        }

        [HttpPost("InsertarDetalleNotaEntrada")]
        public JsonResult InsertarDetalleNotaEntrada([FromBody] InsertarDetalleNotaEntradaModel nota)
        {

            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _articulosService.InsertarDetalleNotaEntrada(nota, 1);
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";

                objectResponse.response = new
                {
                    data =  articulo
                };
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }


            return new JsonResult(objectResponse);

        }

        [HttpGet("GetDetalleNotaEntrada")]
        public JsonResult GetDetalleNotaEntrada([FromQuery] int id_nota)
        {

            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _articulosService.GetDetalleNotaEntrada(id_nota);
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";

                objectResponse.response = new
                {
                    data =  articulo
                };
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }


            return new JsonResult(objectResponse);

        }
    }



}