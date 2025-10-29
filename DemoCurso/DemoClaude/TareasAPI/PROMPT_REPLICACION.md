# ?? PROMPT PARA REPLICAR EL PROYECTO "API DE GESTI�N DE TAREAS"

## ?? PROMPT COMPLETO PARA IA (GitHub Copilot, ChatGPT, Claude, etc.)

Copia y pega este prompt completo a tu asistente de IA:

---

```
Crea una API REST completa de gesti�n de tareas usando ASP.NET Core 8.0 con las siguientes caracter�sticas espec�ficas:

## ??? ESTRUCTURA DEL PROYECTO

Nombre del proyecto: **TareasAPI**
Framework: **.NET 8.0**
Tipo: **ASP.NET Core Web API**

### Estructura de carpetas y archivos:
```
TareasAPI/
??? Controllers/
?   ??? TareasController.cs
??? Models/
?   ??? Tarea.cs
??? DTOs/
?   ??? CrearTareaDto.cs
?   ??? ActualizarTareaDto.cs
??? Repositories/
?   ??? ITareaRepository.cs
?   ??? TareaRepository.cs
??? Properties/
?   ??? launchSettings.json
??? Program.cs
??? appsettings.json
??? appsettings.Development.json
??? TareasAPI.csproj
??? TareasAPI-Tests.http
??? README.md
??? GUIA_INICIO.md
??? RESUMEN.md
??? INICIO_RAPIDO.md
??? MIGRACION_EF_CORE.md
```

## ?? MODELO DE DATOS

### Clase Tarea (Models/Tarea.cs):
- **Id**: int (auto-incrementado)
- **Descripcion**: string (requerido, no vac�o)
- **FechaLimite**: DateTime (requerido)
- **Completada**: bool (default: false)
- **FechaCreacion**: DateTime (default: DateTime.UtcNow)

Namespace: `TareasAPI.Models`

## ?? DTOs (Data Transfer Objects)

### CrearTareaDto (DTOs/CrearTareaDto.cs):
- **Descripcion**: string, [Required], [StringLength(500, MinimumLength = 5)]
  - ErrorMessage: "La descripci�n es obligatoria"
  - ErrorMessage: "La descripci�n debe tener entre 5 y 500 caracteres"
- **FechaLimite**: DateTime, [Required]
  - ErrorMessage: "La fecha l�mite es obligatoria"

Namespace: `TareasAPI.DTOs`

### ActualizarTareaDto (DTOs/ActualizarTareaDto.cs):
- **Descripcion**: string? (nullable), [StringLength(500, MinimumLength = 5)]
- **FechaLimite**: DateTime? (nullable)
- **Completada**: bool? (nullable)

Namespace: `TareasAPI.DTOs`

## ??? REPOSITORIO (Repository Pattern)

### Interfaz ITareaRepository (Repositories/ITareaRepository.cs):
M�todos as�ncronos:
- `Task<IEnumerable<Tarea>> ObtenerTodasAsync()`
- `Task<Tarea?> ObtenerPorIdAsync(int id)`
- `Task<Tarea> CrearAsync(Tarea tarea)`
- `Task<Tarea?> ActualizarAsync(int id, Tarea tarea)`
- `Task<bool> EliminarAsync(int id)`
- `Task<IEnumerable<Tarea>> ObtenerPorEstadoAsync(bool completada)`

Namespace: `TareasAPI.Repositories`

### Implementaci�n TareaRepository (Repositories/TareaRepository.cs):
- Almacenamiento en memoria usando `List<Tarea>`
- Variable privada `_nextId` para auto-incremento (inicia en 1)
- Constructor con 3 tareas de ejemplo:
  1. "Completar documentaci�n del proyecto" - Pendiente, +5 d�as
  2. "Revisar c�digo del equipo" - Pendiente, +2 d�as
  3. "Preparar presentaci�n para cliente" - Completada, +7 d�as

Namespace: `TareasAPI.Repositories`

## ?? CONTROLADOR (Controllers/TareasController.cs)

**Ruta base**: `[Route("api/[controller]")]`
**Atributo**: `[ApiController]`

### Dependencias inyectadas:
- `ITareaRepository _repository`
- `ILogger<TareasController> _logger`

### Endpoints (7 en total):

1. **GET /api/tareas**
   - Nombre m�todo: `ObtenerTodas()`
   - Retorna: `ActionResult<IEnumerable<Tarea>>`
   - Status: 200 OK
   - Documentaci�n XML: "Obtiene todas las tareas"

2. **GET /api/tareas/{id}**
   - Nombre m�todo: `ObtenerPorId(int id)`
   - Retorna: `ActionResult<Tarea>`
   - Status: 200 OK / 404 Not Found
   - Logging: Warning cuando no se encuentra
   - Mensaje de error: `{ mensaje: "No se encontr� la tarea con ID {id}" }`
   - Documentaci�n XML: "Obtiene una tarea por su ID"

3. **GET /api/tareas/estado/{completada}**
   - Nombre m�todo: `ObtenerPorEstado(bool completada)`
   - Retorna: `ActionResult<IEnumerable<Tarea>>`
   - Status: 200 OK
   - Documentaci�n XML: "Obtiene tareas filtradas por estado"

4. **POST /api/tareas**
   - Nombre m�todo: `Crear([FromBody] CrearTareaDto tareaDto)`
   - Retorna: `ActionResult<Tarea>`
   - Status: 201 Created / 400 Bad Request
   - Validaciones:
     - ModelState.IsValid
     - FechaLimite no puede ser en el pasado
   - Mensaje de error: `{ mensaje: "La fecha l�mite no puede ser en el pasado" }`
   - Logging: Informaci�n al crear
   - Retorna: CreatedAtAction con location header
   - Documentaci�n XML: "Crea una nueva tarea"

5. **PUT /api/tareas/{id}**
   - Nombre m�todo: `Actualizar(int id, [FromBody] ActualizarTareaDto tareaDto)`
   - Retorna: `ActionResult<Tarea>`
   - Status: 200 OK / 400 Bad Request / 404 Not Found
   - Validaciones:
     - ModelState.IsValid
     - Tarea existe
     - FechaLimite no puede ser en el pasado (si se proporciona)
   - Actualiza solo campos proporcionados (no nulos)
   - Logging: Informaci�n al actualizar
   - Documentaci�n XML: "Actualiza una tarea existente"

6. **PATCH /api/tareas/{id}/completar**
   - Nombre m�todo: `MarcarComoCompletada(int id)`
   - Retorna: `ActionResult<Tarea>`
   - Status: 200 OK / 404 Not Found
   - Acci�n: Marca Completada = true
   - Logging: Informaci�n al marcar
   - Documentaci�n XML: "Marca una tarea como completada"

7. **DELETE /api/tareas/{id}**
   - Nombre m�todo: `Eliminar(int id)`
   - Retorna: `IActionResult`
   - Status: 204 No Content / 404 Not Found
   - Mensaje de error: `{ mensaje: "No se encontr� la tarea con ID {id}" }`
   - Logging: Informaci�n al eliminar
   - Documentaci�n XML: "Elimina una tarea"

Namespace: `TareasAPI.Controllers`

## ?? CONFIGURACI�N (Program.cs)

### Servicios registrados:
```csharp
builder.Services.AddControllers();
builder.Services.AddSingleton<ITareaRepository, TareaRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo {
        Version = "v1",
        Title = "API de Gesti�n de Tareas",
        Description = "Una API REST para gestionar tareas con ASP.NET Core 8",
        Contact = new OpenApiContact {
            Name = "Equipo de Desarrollo"
        }
    });
});

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

### Pipeline de middleware:
```csharp
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
```

## ?? ARCHIVOS DE CONFIGURACI�N

### appsettings.json:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "TareasAPI": "Information"
    }
  },
  "AllowedHosts": "*",
  "AppSettings": {
    "ApplicationName": "API de Gesti�n de Tareas",
    "Version": "1.0.0",
    "MaxTareaDescriptionLength": 500,
    "MinTareaDescriptionLength": 5
  }
}
```

### appsettings.Development.json:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### launchSettings.json:
```json
{
  "$schema": "http://json.schemastore.org/launchsettings.json",
  "iisSettings": {
    "windowsAuthentication": false,
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "http://localhost:9384",
      "sslPort": 44370
    }
  },
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "swagger",
      "applicationUrl": "http://localhost:5196",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "swagger",
      "applicationUrl": "https://localhost:7091;http://localhost:5196",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "launchUrl": "swagger",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

### TareasAPI.csproj:
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="8.0.21" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.6.2" />
  </ItemGroup>
</Project>
```

## ?? ARCHIVO DE PRUEBAS (TareasAPI-Tests.http)

Crea un archivo con variables y todas las peticiones HTTP para probar:
- Variables: hostname, port, host
- GET todas las tareas
- GET tarea por ID
- GET tareas completadas
- GET tareas pendientes
- POST crear nueva tarea
- PUT actualizar tarea completa
- PUT actualizar solo descripci�n
- PATCH marcar como completada
- DELETE eliminar tarea
- POST crear con fecha inv�lida (caso de error)
- POST crear con descripci�n corta (caso de error)

## ?? DOCUMENTACI�N

### Crear 5 archivos de documentaci�n en Markdown:

1. **README.md**: Documentaci�n completa con:
   - Descripci�n del proyecto
   - Caracter�sticas (emoji checkmarks)
   - Modelo de datos
   - Requisitos
   - Instalaci�n y ejecuci�n
   - Todos los endpoints con ejemplos
   - Ejemplos de uso con cURL
   - Validaciones
   - Arquitectura
   - Pr�ximas mejoras sugeridas
   - Licencia

2. **GUIA_INICIO.md**: Gu�a detallada con:
   - 3 opciones para ejecutar (dotnet CLI, watch, Visual Studio)
   - C�mo acceder a la API
   - Datos de ejemplo precargados
   - 4 formas de probar (Swagger, .http, cURL, Postman)
   - Tabla de endpoints
   - Ejemplos de respuestas JSON
   - Errores comunes y soluciones
   - Configuraci�n (puertos, HTTPS)
   - Recursos adicionales
   - Soluci�n de problemas

3. **RESUMEN.md**: Resumen ejecutivo con:
   - Estado del proyecto (COMPLETADO)
   - Estructura completa del proyecto
   - Funcionalidades implementadas
   - Tabla de endpoints
   - Modelo de datos
   - Validaciones
   - C�mo ejecutar (3 pasos)
   - C�mo probar (3 opciones)
   - Datos de ejemplo
   - Tecnolog�as utilizadas
   - Archivos de documentaci�n
   - Conceptos aplicados
   - Pr�ximas mejoras
   - Notas importantes
   - Estado final con emojis

4. **INICIO_RAPIDO.md**: Gu�a ultra-r�pida con:
   - 3 pasos para ejecutar
   - Prueba r�pida en Swagger
   - Lista de archivos de ayuda
   - Comandos �tiles
   - Tabla de endpoints principales
   - Lista de caracter�sticas
   - Tecnolog�as usadas
   - Pr�ximos pasos
   - Enlaces de ayuda

5. **MIGRACION_EF_CORE.md**: Gu�a completa de migraci�n a EF Core con:
   - Requisitos previos
   - Instalaci�n de paquetes (SQL Server, SQLite, PostgreSQL)
   - C�digo del DbContext completo
   - C�digo del TareaRepositoryEF completo
   - Actualizaci�n de Program.cs
   - Cadenas de conexi�n para cada BD
   - Comandos de migraciones
   - Comandos �tiles de EF Core
   - Caracter�sticas adicionales (paginaci�n, b�squeda, �ndices)
   - Soluci�n de problemas
   - Recursos adicionales
   - Checklist de migraci�n

## ?? ESTILO Y CONVENCIONES

- **Idioma**: Espa�ol para todo (nombres, comentarios, mensajes)
- **Naming**: PascalCase para clases y m�todos, camelCase para variables
- **Async/Await**: Todos los m�todos del repositorio y controlador
- **Nullable**: Habilitado en el proyecto
- **Status Codes**: Usar StatusCodes.Status* constants
- **ProducesResponseType**: Documentar todos los status codes posibles
- **Logging**: Usar ILogger para operaciones importantes
- **Error messages**: Retornar objetos con propiedad "mensaje"
- **Documentation**: Comentarios XML summary en todos los endpoints

## ? CARACTER�STICAS ESPECIALES

1. **Validaci�n inteligente en actualizaci�n**: Solo actualizar campos no nulos
2. **Endpoint especial PATCH**: Marcar como completada sin enviar todo el objeto
3. **Logging contextual**: Incluir el ID en los mensajes de log
4. **Datos de ejemplo**: 3 tareas precargadas con fechas relativas
5. **Swagger configurado**: Con t�tulo, descripci�n y contacto
6. **CORS abierto**: Para desarrollo frontend
7. **Auto-incremento manual**: Controlar IDs en memoria
8. **FechaCreacion autom�tica**: Asignada en el repositorio
9. **Validaci�n de fechas pasadas**: En creaci�n y actualizaci�n
10. **CreatedAtAction**: Retornar location header al crear

## ?? CHECKLIST DE COMPLETITUD

Aseg�rate de crear TODO lo siguiente:
- [ ] Modelo Tarea con 5 propiedades
- [ ] Dos DTOs con validaciones Data Annotations
- [ ] Interfaz ITareaRepository con 6 m�todos
- [ ] TareaRepository con List en memoria y 3 datos de ejemplo
- [ ] TareasController con 7 endpoints completamente funcionales
- [ ] Program.cs con Swagger, CORS, y DI configurado
- [ ] 3 archivos de configuraci�n JSON completos
- [ ] Archivo .csproj con 2 paquetes NuGet
- [ ] launchSettings.json con 3 perfiles
- [ ] Archivo .http con todas las pruebas
- [ ] 5 archivos de documentaci�n Markdown completos
- [ ] Comentarios XML en todos los endpoints
- [ ] Manejo de errores en todos los endpoints
- [ ] Logging en operaciones CRUD
- [ ] Validaciones de ModelState
- [ ] Validaciones de fechas pasadas
- [ ] Respuestas con mensajes descriptivos
- [ ] Status codes apropiados

## ?? RESULTADO ESPERADO

El proyecto debe:
? Compilar sin errores
? Ejecutarse con `dotnet run`
? Abrir Swagger autom�ticamente
? Mostrar 3 tareas de ejemplo al hacer GET
? Validar correctamente todos los campos
? Rechazar fechas en el pasado
? Registrar logs en consola
? Responder con status codes apropiados
? Funcionar completamente sin base de datos
? Estar listo para migrar a EF Core

## ?? EXTRAS IMPORTANTES

- Usar `DateTime.UtcNow` para fechas (no DateTime.Now)
- CORS debe estar registrado Y usado en el pipeline
- Swagger solo debe activarse en Development
- UseHttpsRedirection debe estar habilitado
- UseAuthorization debe estar en el pipeline (aunque no se use a�n)
- Todos los m�todos deben ser as�ncronos (async/await)
- Los namespaces deben coincidir con la estructura de carpetas
```

---

## ?? INSTRUCCIONES DE USO DEL PROMPT

### Para GitHub Copilot Chat:
1. Abre una nueva ventana de VS Code
2. Presiona `Ctrl+Shift+P` ? "New Folder..."
3. Presiona `Ctrl+I` (Copilot Chat inline)
4. Pega el prompt completo
5. Espera a que Copilot cree todo el proyecto

### Para ChatGPT/Claude:
1. Abre una nueva conversaci�n
2. Pega el prompt completo
3. Pide que genere archivo por archivo
4. Copia cada archivo a tu proyecto

### Para Copilot Edits:
1. Crea la carpeta del proyecto
2. Abre Copilot Edits (`Ctrl+Shift+I`)
3. Pega el prompt
4. Selecciona "Create new files"
5. Revisa y acepta los cambios

## ?? VALIDACI�N DEL PROYECTO CREADO

Despu�s de crear el proyecto, valida que:

```bash
# 1. Compilar
dotnet build

# 2. Verificar que no hay errores
# Salida esperada: Build succeeded. 0 Warning(s). 0 Error(s)

# 3. Ejecutar
dotnet run

# 4. Probar en navegador
# https://localhost:7091/swagger

# 5. Probar GET
# Debe devolver 3 tareas de ejemplo

# 6. Probar POST
# Crear una tarea nueva y verificar que devuelve status 201
```

## ?? M�TRICAS DEL PROYECTO

- **Archivos totales**: 16 archivos principales
- **L�neas de c�digo**: ~800 l�neas (sin contar documentaci�n)
- **Endpoints**: 7 endpoints REST
- **DTOs**: 2 clases
- **Modelos**: 1 clase
- **Interfaces**: 1 interfaz
- **Implementaciones**: 1 repositorio
- **Controladores**: 1 controlador
- **Archivos de configuraci�n**: 4 archivos
- **Documentaci�n**: 5 archivos Markdown
- **Pruebas**: 12 casos de prueba en .http
- **Paquetes NuGet**: 2 paquetes

## ?? CONCEPTOS CLAVE IMPLEMENTADOS

1. **Repository Pattern** - Separaci�n de l�gica de datos
2. **Dependency Injection** - Inyecci�n de dependencias nativa
3. **DTOs** - Objetos de transferencia de datos
4. **Data Annotations** - Validaci�n declarativa
5. **Async/Await** - Programaci�n as�ncrona
6. **REST API** - Arquitectura RESTful completa
7. **Swagger/OpenAPI** - Documentaci�n autom�tica
8. **CORS** - Cross-Origin Resource Sharing
9. **Logging** - Registro de eventos
10. **Status Codes** - C�digos HTTP apropiados
11. **Model Validation** - Validaci�n de entrada
12. **Error Handling** - Manejo de errores descriptivo

## ? CHECKLIST FINAL

Antes de considerar el proyecto completo, verifica:

- [ ] Todos los archivos est�n creados
- [ ] El proyecto compila sin errores
- [ ] Swagger se abre autom�ticamente
- [ ] Los 7 endpoints funcionan correctamente
- [ ] Las validaciones rechazan datos inv�lidos
- [ ] Los logs aparecen en la consola
- [ ] Las 3 tareas de ejemplo est�n disponibles
- [ ] Los archivos .http funcionan (si usas VS Code)
- [ ] Toda la documentaci�n est� completa
- [ ] Los status codes son correctos
- [ ] Los mensajes de error son descriptivos

## ?? LEARNING PATH SUGERIDO

Despu�s de crear este proyecto:

1. **D�a 1-2**: Familiar�zate con la estructura y prueba todos los endpoints
2. **D�a 3-4**: Modifica funcionalidades existentes
3. **D�a 5-6**: Agrega nuevas funcionalidades (categor�as, prioridades)
4. **D�a 7-10**: Migra a Entity Framework Core (usa MIGRACION_EF_CORE.md)
5. **D�a 11-15**: Agrega autenticaci�n JWT
6. **D�a 16-20**: Agrega pruebas unitarias
7. **D�a 21-25**: Agrega pruebas de integraci�n
8. **D�a 26-30**: Despliega en Azure o AWS

---

**Versi�n del Prompt**: 1.0.0  
**Compatible con**: .NET 8.0  
**�ltima actualizaci�n**: Diciembre 2024  
**Creado por**: Proyecto TareasAPI Original

�Buena suerte replicando el proyecto! ??
