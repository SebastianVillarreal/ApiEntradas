namespace reportesApi.Models;

public class InsertRecetaModel
{
    public int IdSucursal  { get; set; }
    public string Nombre { get; set; }
    public int IdPlatillo { get; set; }

}

public class RecetaModel: InsertRecetaModel
{
    public int Id {get; set;}
    public string NombrePlatillo { get; set; }
    public string NombreSucursal { get; set; }
    public string FechaActualiza {get; set;}
}