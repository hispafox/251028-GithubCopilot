# API de Gesti�n de Tareas (TareasAPI)

Descripci�n: API REST para gestionar tareas creada con ASP.NET Core 8.0.

Caracter�sticas:
- ? CRUD de tareas
- ? Validaciones con Data Annotations
- ? Inyecci�n de dependencias
- ? Swagger en Development
- ? CORS abierto para desarrollo
- ? Almacenamiento en memoria (List)

Modelo de datos: ver `Models/Tarea.cs`

Requisitos:
- .NET 8 SDK

Instalaci�n y ejecuci�n:
1. dotnet build
2. dotnet run
3. Abrir https://localhost:7091/swagger

Endpoints: Ver `Controllers/TareasController.cs`

Ejemplos de uso con cURL:
- GET todas: curl https://localhost:7091/api/tareas

Validaciones: FechaLimite no puede estar en el pasado; Descripcion requerida y entre 5 y 500 caracteres.

Arquitectura: Repositorio en memoria, Controller, DTOs y Models.

Pr�ximas mejoras sugeridas: Migrar a EF Core, autenticaci�n JWT, tests.

Licencia: MIT
