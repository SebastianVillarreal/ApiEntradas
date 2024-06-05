namespace reportesApi.Models;


public class InsertProveedorModel
{
    
    public string Clave { get; set;}
    public string Nombre { get; set;}
    public string RFC {get; set;}
    public string Direccion {get; set;}
}

public class ProveedorModel: InsertProveedorModel
{
    public int Id { get; set;}
}