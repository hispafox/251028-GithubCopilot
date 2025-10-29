# Migración a EF Core

Requisitos previos: .NET 8 SDK

Instalación de paquetes ejemplo (SQLite):
- dotnet add package Microsoft.EntityFrameworkCore.Sqlite
- dotnet add package Microsoft.EntityFrameworkCore.Design

Ejemplo de DbContext y repository EF en el archivo.

Comandos de migraciones:
- dotnet ef migrations add Initial
- dotnet ef database update

Checklist y recursos adicionales.
