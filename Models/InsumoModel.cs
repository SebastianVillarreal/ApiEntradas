namespace reportesApi.Models;


public class InsumoModel
{
    public int Id { get; set; }
    public string Insumo {get;set ;}
    public string Descripcion {get;set ;}
    public decimal Costo {get; set;}
    public string UnidadMedida {get; set;}
}