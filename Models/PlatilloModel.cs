namespace reportesApi.Models;

public class InsertPlatilloModel
{
    public string Codigo {get;set ;}
    public string Descripcion {get;set ;}
    public decimal PrecioPublico {get; set;}

}

public class PlatilloModel : InsertPlatilloModel
{
    public int Id {get;set ;}
    
}
