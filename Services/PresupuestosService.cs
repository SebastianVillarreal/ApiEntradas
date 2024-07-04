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
    public class PresupuestoService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        
        public PresupuestoService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;
             _webHostEnvironment = webHostEnvironment;
             
        }

        public string GetRutaReportes()
        {
            return @"C:\Users\User\Documents\GitHub\ApiReportes\templates";
            
        }

        public int InsertPresupuesto(InsertPresupuestoModel presupuesto, int user)
        {
            
            List<PresupuestoModel> lista = new List<PresupuestoModel>();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            ArrayList parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@pFecha", SqlDbType = SqlDbType.VarChar, Value = presupuesto.Fecha });
            parametros.Add(new SqlParameter { ParameterName = "@pIdSucursal", SqlDbType = SqlDbType.VarChar, Value = presupuesto.IdSucursal });
            parametros.Add(new SqlParameter { ParameterName = "@pUsuario", SqlDbType = SqlDbType.VarChar, Value = user });
            parametros.Add(new SqlParameter { ParameterName = "@pReferencia", SqlDbType = SqlDbType.VarChar, Value = presupuesto.Referencia });
            try
            {

                DataSet ds = dac.Fill("InsertPresupuestoProduccion", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        lista.Add(new PresupuestoModel{
                            Id = int.Parse(row["Id"].ToString()),
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

        public List<PresupuestoModel> GetPresupuestos(string fecha_inicial, string fecha_final)
        {
            
            List<PresupuestoModel> lista = new List<PresupuestoModel>();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            ArrayList parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@pFechaInicial", SqlDbType = SqlDbType.VarChar, Value = fecha_inicial });
            parametros.Add(new SqlParameter { ParameterName = "@pFechaFinal", SqlDbType = SqlDbType.VarChar, Value = fecha_final });
            try
            {
                DataSet ds = dac.Fill("GetPresupuestos", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        lista.Add(new PresupuestoModel{
                            Id = int.Parse(row["Id"].ToString()),
                            Fecha = row["Fecha"].ToString(),
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

        public bool InsertDetallePresupuesto(InsertDetallePresupeustoModel renglon)
        {
            
            List<InsumoModel> lista = new List<InsumoModel>();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            ArrayList parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@pIdProduccion", SqlDbType = SqlDbType.VarChar, Value =renglon.IdPresupeusto });
            parametros.Add(new SqlParameter { ParameterName = "@pIdProducto", SqlDbType = SqlDbType.VarChar, Value = renglon.IdProducto });
            parametros.Add(new SqlParameter { ParameterName = "@pCantidad", SqlDbType = SqlDbType.VarChar, Value = renglon.Cantidad });
            try
            {
                dac.ExecuteNonQuery("InsertDetallePresupuestoProduccion", parametros);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;

            }
           
        }

        public List<DetallePresupuestoModel> GetDetallePresupuesto(int id_presupuesto)
        {
            
            List<DetallePresupuestoModel> lista = new List<DetallePresupuestoModel>();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            ArrayList parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@pIdPresupuesto", SqlDbType = SqlDbType.VarChar, Value = id_presupuesto });
            try
            {
                DataSet ds = dac.Fill("GetDetallePresupuesto", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        lista.Add(new DetallePresupuestoModel{
                            Id = int.Parse(row["id"].ToString()),
                            Producto = row["Producto"].ToString(),
                            Cantidad = decimal.Parse(row["Cantidad"].ToString())
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


        public List<GetExplosionInsumosModel> GetExplosionInsumosPresupuesto(int id)
        {
            
            List<GetExplosionInsumosModel> lista = new List<GetExplosionInsumosModel>();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            ArrayList parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@pIdPresupuesto", SqlDbType = SqlDbType.VarChar, Value = id });
            try
            {
                DataSet ds = dac.Fill("GetExplosionInsumos", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        lista.Add(new GetExplosionInsumosModel{
                            Insumo =row["insumo"].ToString(),
                            Descripcion = row["Descripcion"].ToString(),
                            CantidadInsumo =decimal.Parse(row["cantidad"].ToString()),
                            Existencia = decimal.Parse(row["Existencia"].ToString()),
                            Faltante = decimal.Parse(row["Faltante"].ToString())
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