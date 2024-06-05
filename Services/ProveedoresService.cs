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
    public class ProveedoresService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        
        public ProveedoresService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;
             _webHostEnvironment = webHostEnvironment;
             
        }

        public string GetRutaReportes()
        {
            return @"C:\Users\User\Documents\GitHub\ApiReportes\templates";
            
        }

        public bool InsertProveedor(InsertProveedorModel proveedor, int user)
        {
            
            List<InsumoModel> lista = new List<InsumoModel>();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            ArrayList parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@pClaveProveedor", SqlDbType = SqlDbType.VarChar, Value = proveedor.Clave });
            parametros.Add(new SqlParameter { ParameterName = "@pNombre", SqlDbType = SqlDbType.VarChar, Value = proveedor.Nombre });
            parametros.Add(new SqlParameter { ParameterName = "@pRFC", SqlDbType = SqlDbType.VarChar, Value = proveedor.RFC });
            parametros.Add(new SqlParameter { ParameterName = "@pDireccion", SqlDbType = SqlDbType.VarChar, Value = proveedor.Direccion });
            parametros.Add(new SqlParameter { ParameterName = "@pUsuario", SqlDbType = SqlDbType.VarChar, Value = user });
            try
            {
                dac.ExecuteNonQuery("InsertProveedor", parametros);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;

            }
           
        }

        public List<ProveedorModel> GetProveedores()
        {
            
            List<ProveedorModel> lista = new List<ProveedorModel>();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            ArrayList parametros = new ArrayList();
            try
            {
                DataSet ds = dac.Fill("GetProveedores", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        lista.Add(new ProveedorModel{
                            Direccion  = row["Direccion"].ToString(),
                            RFC = row["RFC"].ToString(),
                            Nombre = row["Nombre"].ToString(),
                            Id = int.Parse(row["id"].ToString()),
                            Clave = row["ClaveProveedor"].ToString(),
                        });
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lista;
           
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