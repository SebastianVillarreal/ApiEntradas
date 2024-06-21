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
    public class RecetasService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        
        public RecetasService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;
             _webHostEnvironment = webHostEnvironment;
             
        }

        public string GetRutaReportes()
        {
            return @"C:\Users\User\Documents\GitHub\ApiReportes\templates";
            
        }

        public int InsertReceta(InsertRecetaModel receta, int user)
        {
            
            List<RecetaModel> lista = new List<RecetaModel>();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            ArrayList parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@pIdUsuario", SqlDbType = SqlDbType.VarChar, Value = user });
            parametros.Add(new SqlParameter { ParameterName = "@pNombre", SqlDbType = SqlDbType.VarChar, Value = receta.Nombre });
            parametros.Add(new SqlParameter { ParameterName = "@pIdPlatillo", SqlDbType = SqlDbType.VarChar, Value = receta.IdPlatillo });
            parametros.Add(new SqlParameter { ParameterName = "@pIdSucursal", SqlDbType = SqlDbType.VarChar, Value = receta.IdSucursal });
            try
            {
                DataSet ds = dac.Fill("InsertReceta", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        lista.Add(new RecetaModel{
                            Id = int.Parse(row["id"].ToString()),
                        });
                    }
                }
                return lista[0].Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;

            }
           
        }

        public List<RecetaModel> GetRecetas()
        {
            
            List<RecetaModel> lista = new List<RecetaModel>();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            ArrayList parametros = new ArrayList();
            try
            {
                DataSet ds = dac.Fill("GetRecetas", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        lista.Add(new RecetaModel{
                            Nombre = row["Nombre"].ToString(),
                            Id = int.Parse(row["id"].ToString()),
                            FechaActualiza = row["FechaActualiza"].ToString(),
                            NombrePlatillo = row["Descripcion"].ToString()
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

        public int InsertDetalleReceta(InsertDetalleRecetaModel receta, int user)
        {
            
            List<RecetaModel> lista = new List<RecetaModel>();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            ArrayList parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@pIdReceta", SqlDbType = SqlDbType.VarChar, Value = receta.IdReceta });
            parametros.Add(new SqlParameter { ParameterName = "@pIdInsumo", SqlDbType = SqlDbType.VarChar, Value = receta.IdInsumo });
            parametros.Add(new SqlParameter { ParameterName = "@pCantidad", SqlDbType = SqlDbType.VarChar, Value = receta.Cantidad });
            parametros.Add(new SqlParameter { ParameterName = "@pReferencia", SqlDbType = SqlDbType.VarChar, Value = receta.Referencia });
            parametros.Add(new SqlParameter { ParameterName = "@pIdUsuario", SqlDbType = SqlDbType.VarChar, Value = user });
            try
            {
                DataSet ds = dac.Fill("InsertDetalleReceta", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        lista.Add(new RecetaModel{
                            Id = int.Parse(row["id"].ToString()),
                        });
                    }
                }
                return lista[0].Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;

            }
           
        }

        public List<DetalleRecetaModel> GetDetalleReceta(int id_receta)
        {
            
            List<DetalleRecetaModel> lista = new List<DetalleRecetaModel>();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            ArrayList parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@pIdReceta", SqlDbType = SqlDbType.VarChar, Value = id_receta });
            try
            {
                DataSet ds = dac.Fill("GetDetalleReceta", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        lista.Add(new DetalleRecetaModel{
                            Id = int.Parse(row["id"].ToString()),
                            IdInsumo =  int.Parse(row["IdInsumo"].ToString()),
                            Cantidad = decimal.Parse( row["Cantidad"].ToString()),
                            Insumo = row["Insumo"].ToString(),
                            Referencia = row["Referencia"].ToString(),
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