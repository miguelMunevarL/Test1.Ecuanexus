using Test1.Ecuanexus.Components;
using Test1.Ecuanexus.Services;

var builder = WebApplication.CreateBuilder(args);

// --- CONFIGURACIÓN REQUERIDA ---
const string apiKey = "CANDIDATO_1_A6A3E4D8-B3A4-4E8A-B499-2A8A2F2B2E1A";
const string apiBaseUrl = "https://test1.ecuafact.com/"; // URL de API actualizada

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddScoped<ProductService>();

builder.Services.AddHttpClient("ApiInventario", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
    
});


// --- FIN DE CONFIGURACIÓN ---

var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();