using System;
using System.Collections;
using System.Data;
using reportesApi.DataContext;
using reportesApi.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.Data.SqlClient;
namespace reportesApi.Services
{
    public class InsumosService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        
        public InsumosService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;
             _webHostEnvironment = webHostEnvironment;
             
        }

        public string GetRutaReportes()
        {
            return @"C:\Users\User\Documents\GitHub\ApiReportes\templates";
            
        }

        public List<InsumoModel> GetInsumos()
        {
            
            List<InsumoModel> lista = new List<InsumoModel>();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            ArrayList parametros = new ArrayList();
            try
            {
                DataSet ds = dac.Fill("GetInsumos", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        lista.Add(new InsumoModel{
                            Id = int.Parse(row["Id"].ToString()),
                            Insumo  = row["Insumo"].ToString(),
                            Descripcion  = row["Descripcion"].ToString(),
                            Costo = decimal.Parse(row["Costo"].ToString()),
                            UnidadMedida = row["UM"].ToString()
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

        public bool InsertInsumo(InsumoModel insumo, int user)
        {
            
            List<InsumoModel> lista = new List<InsumoModel>();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            ArrayList parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@pInsumo", SqlDbType = SqlDbType.VarChar, Value = insumo.Insumo });
            parametros.Add(new SqlParameter { ParameterName = "@pDescripcion", SqlDbType = SqlDbType.VarChar, Value = insumo.Descripcion });
            parametros.Add(new SqlParameter { ParameterName = "@pUnidadMedida", SqlDbType = SqlDbType.VarChar, Value = insumo.UnidadMedida });
            parametros.Add(new SqlParameter { ParameterName = "@pCosto", SqlDbType = SqlDbType.VarChar, Value = insumo.Costo });
            parametros.Add(new SqlParameter { ParameterName = "@pUsuario", SqlDbType = SqlDbType.VarChar, Value = user });
            try
            {
                dac.ExecuteNonQuery("InsertarInsumo", parametros);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;

            }
           
        }

        

    }
}