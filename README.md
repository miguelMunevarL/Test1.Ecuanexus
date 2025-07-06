# Prueba Técnica: Blazor Dashboard

## Misión
Complete la funcionalidad del dashboard de inventario en "Components/Pages/Dashboard.razor".

El "HttpClient" ya está configurado en "Program.cs". Reemplace los placeholders con la URL del API y su API Key asignada.

Los objetivos se detallan en el "TestChecklist" visible en la página principal.

### Requisito Adicional: Estilo Global

Para asegurar la consistencia de la marca, se requiere que toda la aplicación utilice la fuente 'Roboto' de Google Fonts. Por favor, modifica la estructura de la aplicación para que esta fuente se cargue globalmente en todas las páginas.

## Entregable
Enlace a un repositorio Git con la solución y las respuestas a las siguientes preguntas en este "README".

---
### 1. Enfoque y Decisiones
Use InteractiveServer ya que este me permite un renderizado interactivo del lado servidor mediante Blazor Server;  usa las propiedades de signalR para manejar tanto eventos como actualizaciones
y era el que mejor se acoplaba al compoenente; opciones como InteractiveWebAssembly necesitaban acondicionarse o en mi caso me pedia hacerlo ya que mostraba el siguiente error
InvalidOperationException: A component of type 'Test1.Ecuanexus.Components.ProductFilter' has render mode 'InteractiveWebAssemblyRenderMode', but the required endpoints are not mapped on the server. 
When calling 'MapRazorComponents', add a call to 'AddInteractiveWebAssemblyRenderMode'. For example, 'builder.MapRazorComponents<...>.AddInteractiveWebAssemblyRenderMode()'
El cual segun la documentacion; refiere que el compoennte llama el modo de renderizado WebAssemby pero la app no esta configurada de esa manera; problemas similares ocurren con InteractiveAuto; por tanto InteractiveServer representa la opcion mas viable.


### 2. Desafíos
Entender y escoger el modo de renderizado adecuado para Blazor, ya que las opciones disponibles no se adaptaban a los requisitos del proyecto.

