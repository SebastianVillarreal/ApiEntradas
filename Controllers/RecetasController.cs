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
    public class RecetasController: ControllerBase
    {
   
        private readonly RecetasService _articulosService;
        private readonly ILogger<RecetasController> _logger;
  
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        

        Encrypt enc = new Encrypt();

        public RecetasController(RecetasService articulosservice, ILogger<RecetasController> logger, IJwtAuthenticationService authService) {
            _articulosService = articulosservice;
            _logger = logger;
       
            _authService = authService;
            // Configura la ruta base donde se almacenan los archivos.
            // Asegúrate de ajustar la ruta según tu estructura de directorios.

            
            
        }

        [HttpPost("InsertReceta")]
        public JsonResult InsertNotaEntrada([FromBody] InsertRecetaModel receta)
        {

            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _articulosService.InsertReceta(receta, 1);
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

        [HttpGet("GetRecetas")]
        public JsonResult GetRecetas()
        {

            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _articulosService.GetRecetas();
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

        [HttpPost("InsertDetalleReceta")]
        public JsonResult InsertDetalleReceta([FromBody] InsertDetalleRecetaModel receta)
        {

            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _articulosService.InsertDetalleReceta(receta, 1);
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

        [HttpGet("GetDetalleReceta")]
        public JsonResult GetDetalleReceta([FromQuery] int receta)
        {

            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _articulosService.GetDetalleReceta(receta);
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