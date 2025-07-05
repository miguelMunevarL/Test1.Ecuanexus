using System.Net.Http.Json;
using Test1.Ecuanexus.Client.Models;

namespace Test1.Ecuanexus.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            Console.WriteLine("[ProductService] Constructor - Creando HttpClient");
            try
            {
                _httpClient = httpClientFactory.CreateClient("ApiInventario");
                Console.WriteLine($"[ProductService] BaseAddress: {_httpClient.BaseAddress}");
                Console.WriteLine($"[ProductService] HttpClient creado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProductService] Error al crear HttpClient: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            Console.WriteLine("[ProductService] GetProductsAsync - Iniciando");
            try
            {
                Console.WriteLine("[ProductService] Enviando GET a /api/products");
                var response = await _httpClient.GetAsync("api/products");
                Console.WriteLine($"[ProductService] Respuesta recibida: {response.StatusCode}");
                Console.WriteLine($"[ProductService] Content-Type: {response.Content.Headers.ContentType}");

                response.EnsureSuccessStatusCode();

                var products = await response.Content.ReadFromJsonAsync<List<Product>>() ?? new List<Product>();
                Console.WriteLine($"[ProductService] Productos recibidos: {products.Count}");
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProductService] Error en GetProductsAsync: {ex.Message}");
                Console.WriteLine($"[ProductService] Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<List<Product>> SearchProductsAsync(string term)
        {
            Console.WriteLine($"[ProductService] SearchProductsAsync - Término: '{term}'");
            try
            {
                //var url = $"api/products?searchTerm={Uri.EscapeDataString(term)}";
                //Console.WriteLine($"[ProductService] Enviando GET a {url}");

                 var response = await _httpClient.GetAsync("api/products"+ $"?searchTerm={Uri.EscapeDataString(term)}");
                Console.WriteLine($"[ProductService] Respuesta recibida: {response.StatusCode}");

                response.EnsureSuccessStatusCode();

                var products = await response.Content.ReadFromJsonAsync<List<Product>>() ?? new List<Product>();
                Console.WriteLine($"[ProductService] Productos encontrados: {products.Count}");
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProductService] Error en SearchProductsAsync: {ex.Message}");
                Console.WriteLine($"[ProductService] Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> UpdateStockAsync(int productId, StockUpdateRequest request)
        {
            Console.WriteLine($"[ProductService] UpdateStockAsync - Producto: {productId}, Stock: {request.NewStock}");
            try
            {
                var url = $"api/products/{productId}/stock";
                Console.WriteLine($"[ProductService] Enviando PUT a {url}");

                var response = await _httpClient.PostAsJsonAsync(url, request);
                Console.WriteLine($"[ProductService] Respuesta recibida: {response.StatusCode}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProductService] Error en UpdateStockAsync: {ex.Message}");
                Console.WriteLine($"[ProductService] Stack trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}