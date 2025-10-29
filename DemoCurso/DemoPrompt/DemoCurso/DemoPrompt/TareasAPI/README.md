# API de Gestión de Tareas (TareasAPI)

Descripción: API REST para gestionar tareas creada con ASP.NET Core 8.0.

Características:
- ? CRUD de tareas
- ? Validaciones con Data Annotations
- ? Inyección de dependencias
- ? Swagger en Development
- ? CORS abierto para desarrollo
- ? Almacenamiento en memoria (List)

Modelo de datos: ver `Models/Tarea.cs`

Requisitos:
- .NET 8 SDK

Instalación y ejecución:
1. dotnet build
2. dotnet run
3. Abrir https://localhost:7091/swagger

Endpoints: Ver `Controllers/TareasController.cs`

Ejemplos de uso con cURL:
- GET todas: curl https://localhost:7091/api/tareas

Validaciones: FechaLimite no puede estar en el pasado; Descripcion requerida y entre 5 y 500 caracteres.

Arquitectura: Repositorio en memoria, Controller, DTOs y Models.

Próximas mejoras sugeridas: Migrar a EF Core, autenticación JWT, tests.

Licencia: MIT
