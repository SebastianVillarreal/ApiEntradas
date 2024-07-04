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
using System.Data;


namespace reportesApi.Controllers
{
   
    [Route("api")]
    public class PresupuestosController: ControllerBase
    {
   
        private readonly PresupuestoService _presupuestoService;
        private readonly ILogger<ComprasController> _logger;
  
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        

        Encrypt enc = new Encrypt();

        public PresupuestosController(PresupuestoService presupuestoservice, ILogger<ComprasController> logger, IJwtAuthenticationService authService) {
            _presupuestoService = presupuestoservice;
            _logger = logger;
       
            _authService = authService;
            // Configura la ruta base donde se almacenan los archivos.
            // Asegúrate de ajustar la ruta según tu estructura de directorios.

            
            
        }

        [HttpPost("InsertPresupuesto")]
        public JsonResult InsertPresupuesto([FromBody] InsertPresupuestoModel presupuesto)
        {

            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _presupuestoService.InsertPresupuesto(presupuesto, 1);
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

        [HttpPost("InsertDetallePresupuesto")]
        public JsonResult InsertDetallePresupuesto([FromBody] InsertDetallePresupeustoModel presupuesto)
        {

            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _presupuestoService.InsertDetallePresupuesto(presupuesto);
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

        [HttpGet("GetPresupuestos")]
        public JsonResult GetPresupuestos([FromQuery] string fecha_inicial, string fecha_final)
        {

            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _presupuestoService.GetPresupuestos(fecha_inicial, fecha_final);
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

        [HttpGet("GetDetallePresupuesto")]
        public JsonResult GetDetallePresupuesto([FromQuery] int id_presupuesto )
        {

            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _presupuestoService.GetDetallePresupuesto(id_presupuesto);
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

        [HttpGet("GetExplosionInsumosPresupuesto")]
        public JsonResult GetExplosionInsumosPresupuesto([FromQuery] int id_presupuesto )
        {

            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _presupuestoService.GetExplosionInsumosPresupuesto(id_presupuesto);
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