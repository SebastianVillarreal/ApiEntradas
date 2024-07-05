namespace reportesApi.Models;

public class InsertRecetaModel
{
    public int IdSucursal  { get; set; }
    public string Nombre { get; set; }
    public int IdPlatillo { get; set; }
    public int Tipo {get; set;}

}

public class RecetaModel: InsertRecetaModel
{
    public int Id {get; set;}
    public string NombrePlatillo { get; set; }
    public string NombreSucursal { get; set; }
    public string FechaActualiza {get; set;}
}

public class InsertDetalleRecetaModel
{
    public int IdReceta { get; set;}
    public int IdInsumo {get; set;}
    public decimal Cantidad { get; set;}
    public string Referencia { get; set;}

}

public class DetalleRecetaModel: InsertDetalleRecetaModel
{
    public int Id { get; set;}
    public string Insumo { get; set; }
    public string Descripcion {get; set;}
}