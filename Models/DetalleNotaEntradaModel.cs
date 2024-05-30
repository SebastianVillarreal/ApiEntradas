namespace reportesApi.Models;


public class InsertarDetalleNotaEntradaModel
{

    public int IdNota { get; set; }
    public string Insumo { get; set; }
    public decimal Costo { get; set; }
    public decimal Cantidad { get; set; }
    

}


public class DetalleNotaEntradaModel: InsertarDetalleNotaEntradaModel
{
    public int Id {get; set;}
    public string Fecha { get; set;}
    public string DescripcionInsumo { get; set; }
    
}

