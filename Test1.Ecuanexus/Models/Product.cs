namespace Test1.Ecuanexus.Client.Models;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Sku { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public DateTime LastUpdated { get; set; }
}