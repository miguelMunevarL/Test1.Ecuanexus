using Test1.Ecuanexus.Components;
using Test1.Ecuanexus.Services;

var builder = WebApplication.CreateBuilder(args);

// --- CONFIGURACIÓN REQUERIDA ---
const string apiKey = "CANDIDATO_1_A6A3E4D8-B3A4-4E8A-B499-2A8A2F2B2E1A";
const string apiBaseUrl = "https://test1.ecuafact.com/"; // URL de API actualizada

Console.WriteLine($"[Program] Configurando servicios...");
Console.WriteLine($"[Program] API Base URL: {apiBaseUrl}");
Console.WriteLine($"[Program] API Key: {apiKey}");

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddScoped<ProductService>();

Console.WriteLine($"[Program] Configurando HttpClient...");
builder.Services.AddHttpClient("ApiInventario", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
    Console.WriteLine($"[Program] HttpClient configurado con BaseAddress: {client.BaseAddress}");
    Console.WriteLine($"[Program] Headers configurados: {string.Join(", ", client.DefaultRequestHeaders.Select(h => $"{h.Key}={string.Join(",", h.Value)}"))}");
});

Console.WriteLine($"[Program] Servicios configurados exitosamente");
// --- FIN DE CONFIGURACIÓN ---

var app = builder.Build();

Console.WriteLine($"[Program] Aplicación construida");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

Console.WriteLine($"[Program] Aplicación configurada, iniciando...");
app.Run();