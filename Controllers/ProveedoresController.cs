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
    public class ProveedoresController: ControllerBase
    {
   
        private readonly ProveedoresService _articulosService;
        private readonly ILogger<ProveedoresController> _logger;
  
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        

        Encrypt enc = new Encrypt();

        public ProveedoresController(ProveedoresService articulosservice, ILogger<ProveedoresController> logger, IJwtAuthenticationService authService) {
            _articulosService = articulosservice;
            _logger = logger;
       
            _authService = authService;
            // Configura la ruta base donde se almacenan los archivos.
            // Asegúrate de ajustar la ruta según tu estructura de directorios.

            
            
        }

        [HttpPost("InsertProveedor")]
        public JsonResult InsertNotaEntrada([FromBody] InsertProveedorModel proveedor)
        {

            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _articulosService.InsertProveedor(proveedor, 1);
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

        [HttpGet("GetProveedores")]
        public JsonResult GetProveedores()
        {

            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _articulosService.GetProveedores();
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