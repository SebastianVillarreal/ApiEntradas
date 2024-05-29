using System;
using Microsoft.AspNetCore.Mvc;
using reportesApi.Services;
using reportesApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using reportesApi.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using reportesApi.Helpers;


namespace reportesApi.Controllers
{
   
    [Route("api")]
    public class InsumoController: ControllerBase
    {
        private readonly InsumosService _articulosService;
        private readonly ILogger<InsumoController> _logger;
  
        private readonly IJwtAuthenticationService _authService;
        


        Encrypt enc = new Encrypt();

        public InsumoController(InsumosService articulosservice, ILogger<InsumoController> logger, IJwtAuthenticationService authService) {
            _articulosService = articulosservice;
            _logger = logger;
       
            _authService = authService;
            
        }

       // [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetInsumos")]
        public JsonResult GetInsumos()
        {

            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _articulosService.GetInsumos();
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

        [HttpPost("InsertInsumo")]
        public JsonResult InsertInsumo([FromBody] InsumoModel insumo)
        {

            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _articulosService.InsertInsumo(insumo, 1);
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



