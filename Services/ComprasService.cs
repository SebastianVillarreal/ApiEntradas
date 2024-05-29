using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;
using System.Collections.Generic;
using OfficeOpenXml;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
namespace reportesApi.Services
{
    public class ComprasService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        
        public ComprasService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;
             _webHostEnvironment = webHostEnvironment;
             
        }

        public string GetRutaReportes()
        {
            return @"C:\Users\User\Documents\GitHub\ApiReportes\templates";
            
        }






        public decimal Dividir(decimal numerador, decimal denominador)
        {
            if (denominador == 0)
            {
                // Retornamos 0 si el denominador es cero para evitar la división por cero.
                return 0;
            }
            else
            {
                // Si el denominador no es cero, realizamos la división normalmente.
                return numerador / denominador ;
            }
        }


    }
}