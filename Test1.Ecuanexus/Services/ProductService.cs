using System;
using System.Net.Http.Json;
using Test1.Ecuanexus.Client.Models;
using Microsoft.Extensions.Logging;

namespace Test1.Ecuanexus.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IHttpClientFactory httpClientFactory, ILogger<ProductService> logger)
        {
            
            try
            {
                _httpClient = httpClientFactory.CreateClient("ApiInventario");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProductService] Error al crear HttpClient: {ex.Message}");
                throw;
            }

            _logger = logger;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            Console.WriteLine("[ProductService] GetProductsAsync - Iniciando");
            try
            {
                
                var response = await _httpClient.GetAsync("api/products");
                response.EnsureSuccessStatusCode();

                var products = await response.Content.ReadFromJsonAsync<List<Product>>() ?? new List<Product>();
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProductService] Error en GetProductsAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Product>> SearchProductsAsync(string term)
        {
           
            try
            {
                var url = $"api/products?searchTerm={Uri.EscapeDataString(term)}";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var products = await response.Content.ReadFromJsonAsync<List<Product>>() ?? new List<Product>();
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProductService] Error en SearchProductsAsync: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateProductStockAsync(int productId, StockUpdateRequest request)
        {
            try
            {
                var url = $"api/products/{productId}/stock";               
                var response = await _httpClient.PostAsJsonAsync(url, request);                
                response.EnsureSuccessStatusCode();
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProductService] Error en UpdateProductStockAsync: {ex.Message}");
                throw;
            }
        }

       
    }
}