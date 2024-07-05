using System.Collections.Generic;

namespace reportesApi.Models;

public class InsertPresupuestoModel
{
    public string Referencia {get; set;}
    public int IdSucursal {get; set;}
    public string Fecha {get; set;}
}

public class PresupuestoModel: InsertPresupuestoModel
{
    public int Id {get; set;}
}

public class InsertDetallePresupeustoModel
{
    public int IdPresupeusto {get; set;}
    public int IdProducto {get; set;}
    public decimal Cantidad {get; set;}


}

public class DetallePresupuestoModel: InsertDetallePresupeustoModel 
{
    public int Id { get; set;}
    public string Producto {get; set;}
}


public class GetExplosionInsumosModel
{
    public string Insumo {get; set;}
    public string Descripcion {get; set;}
    public decimal CantidadInsumo {get; set;}
    public decimal Existencia {get; set;}
    public decimal Faltante {get; set;}

}


public class GetInsumosOrdenProduccion
{
    public string Insumo {get; set;}
    public string Descripcion {get; set;}
    public decimal Total {get; set;}
    public decimal Existencia {get; set;}
    public decimal Faltante {get; set;}
    public string UM {get; set;}
    public List<GetInsumosOrdenProduccion> Insumos {get;set ;}
}