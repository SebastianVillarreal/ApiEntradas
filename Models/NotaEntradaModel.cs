namespace reportesApi.Models;

public class InsertNotaEntradaModel
{
    public int IdProveedor { get; set; }
    public int IdSucursal { get; set; }
    public string Factura {get; set;}
}

public class NotaEntradaModel: InsertNotaEntradaModel
{
    public int Id {get; set;}
    public string Nota { get; set; }
    public string Proveedor {get; set;}
    public string Fecha {get; set;}
    public decimal Total { get; set; }
    public string Estatus {get; set;}
}