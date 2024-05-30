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
    public class DetalleComprasService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        
        public DetalleComprasService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;
             _webHostEnvironment = webHostEnvironment;
             
        }

        public string GetRutaReportes()
        {
            return @"C:\Users\User\Documents\GitHub\ApiReportes\templates";
            
        }

        public bool InsertarDetalleNotaEntrada(InsertarDetalleNotaEntradaModel nota, int user)
        {
            
            List<InsumoModel> lista = new List<InsumoModel>();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            ArrayList parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@pIdNota", SqlDbType = SqlDbType.VarChar, Value = nota.IdNota });
            parametros.Add(new SqlParameter { ParameterName = "@pInusmo", SqlDbType = SqlDbType.VarChar, Value = nota.Insumo });
            parametros.Add(new SqlParameter { ParameterName = "@pCosto", SqlDbType = SqlDbType.VarChar, Value = nota.Costo });
            parametros.Add(new SqlParameter { ParameterName = "@pCantidad", SqlDbType = SqlDbType.VarChar, Value = nota.Cantidad });
            parametros.Add(new SqlParameter { ParameterName = "@pUsuario", SqlDbType = SqlDbType.VarChar, Value = user });
            try
            {
                dac.ExecuteNonQuery("InsertarDetalleNotaEntrada", parametros);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;

            }
           
        }

        public List<DetalleNotaEntradaModel> GetDetalleNotaEntrada(int id_nota)
        {
            
            List<DetalleNotaEntradaModel> lista = new List<DetalleNotaEntradaModel>();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            ArrayList parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@IdNota", SqlDbType = SqlDbType.VarChar, Value = id_nota });
            try
            {
                DataSet ds = dac.Fill("GetDetalleNotaEntrada", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        lista.Add(new DetalleNotaEntradaModel{
                            Costo  = decimal.Parse( row["Costo"].ToString()),
                            Cantidad = decimal.Parse(row["Cantidad"].ToString()),
                            DescripcionInsumo = row["Descripcion"].ToString(),
                            Insumo = row["insumo"].ToString(),
                            Id = int.Parse(row["id"].ToString()),
                            Fecha = row["FechaRegistro"].ToString(),
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