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

        public List<NotaEntradaModel> GetNotasEntrada(string fecha_inicial, string fecha_final, int sucursal)
        {
            
            List<NotaEntradaModel> lista = new List<NotaEntradaModel>();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            ArrayList parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@pFechaInicial", SqlDbType = SqlDbType.VarChar, Value = fecha_inicial });
            parametros.Add(new SqlParameter { ParameterName = "@pFechaFinal", SqlDbType = SqlDbType.VarChar, Value = fecha_final });
            parametros.Add(new SqlParameter { ParameterName = "@pIdSucursal", SqlDbType = SqlDbType.VarChar, Value = sucursal });
            try
            {
                DataSet ds = dac.Fill("GetNotasEntrada", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        lista.Add(new NotaEntradaModel{
                            Factura  = row["Factura"].ToString(),
                            //Nota  = row["Descripcion"].ToString(),
                            Total = decimal.Parse(row["Total"].ToString()),
                            Estatus = row["Estatus"].ToString(),
                            Proveedor = row["Proveedor"].ToString(),
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