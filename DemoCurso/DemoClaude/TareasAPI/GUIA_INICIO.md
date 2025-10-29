# Gu�a de Inicio R�pido - API de Gesti�n de Tareas

## ?? Ejecutar la API

### Opci�n 1: Usando dotnet CLI
```bash
cd TareasAPI
dotnet run
```

### Opci�n 2: Con Hot Reload (desarrollo)
```bash
cd TareasAPI
dotnet watch run
```

### Opci�n 3: Desde Visual Studio
1. Abre el proyecto en Visual Studio
2. Presiona F5 o haz clic en el bot�n "Play"

## ?? Acceder a la API

Una vez ejecutada, la API estar� disponible en:
- **Swagger UI**: https://localhost:{puerto}/swagger
- **API Base**: https://localhost:{puerto}/api/tareas

El puerto se mostrar� en la consola al ejecutar la aplicaci�n.

## ?? Datos de Ejemplo

La API viene precargada con 3 tareas de ejemplo:
1. "Completar documentaci�n del proyecto" - Pendiente
2. "Revisar c�digo del equipo" - Pendiente
3. "Preparar presentaci�n para cliente" - Completada

## ?? Probar la API

### Usando Swagger UI (Recomendado para principiantes)
1. Ejecuta la aplicaci�n
2. Navega a https://localhost:{puerto}/swagger
3. Expande cualquier endpoint
4. Haz clic en "Try it out"
5. Ingresa los par�metros necesarios
6. Haz clic en "Execute"

### Usando el archivo .http (VS Code con REST Client)
1. Instala la extensi�n "REST Client" en VS Code
2. Abre el archivo `TareasAPI-Tests.http`
3. Actualiza el puerto si es necesario (l�nea 2)
4. Haz clic en "Send Request" sobre cualquier petici�n

### Usando cURL
```bash
# Obtener todas las tareas
curl -k https://localhost:7116/api/tareas

# Crear una nueva tarea
curl -k -X POST https://localhost:7116/api/tareas \
  -H "Content-Type: application/json" \
  -d "{\"descripcion\":\"Mi nueva tarea\",\"fechaLimite\":\"2024-12-31T23:59:59Z\"}"

# Obtener tarea por ID
curl -k https://localhost:7116/api/tareas/1

# Actualizar tarea
curl -k -X PUT https://localhost:7116/api/tareas/1 \
  -H "Content-Type: application/json" \
  -d "{\"completada\":true}"

# Eliminar tarea
curl -k -X DELETE https://localhost:7116/api/tareas/1
```

### Usando Postman
1. Importa la colecci�n desde Swagger UI o crea manualmente las peticiones
2. Base URL: `https://localhost:{puerto}/api/tareas`
3. Configura los headers: `Content-Type: application/json`

## ?? Endpoints Disponibles

| M�todo | Endpoint | Descripci�n |
|--------|----------|-------------|
| GET | `/api/tareas` | Obtener todas las tareas |
| GET | `/api/tareas/{id}` | Obtener una tarea espec�fica |
| GET | `/api/tareas/estado/{completada}` | Filtrar por estado |
| POST | `/api/tareas` | Crear nueva tarea |
| PUT | `/api/tareas/{id}` | Actualizar tarea |
| PATCH | `/api/tareas/{id}/completar` | Marcar como completada |
| DELETE | `/api/tareas/{id}` | Eliminar tarea |

## ?? Ejemplos de Respuestas

### GET /api/tareas
```json
[
  {
    "id": 1,
    "descripcion": "Completar documentaci�n del proyecto",
    "fechaLimite": "2024-12-15T23:59:59Z",
    "completada": false,
    "fechaCreacion": "2024-12-10T10:00:00Z"
  }
]
```

### POST /api/tareas
**Request:**
```json
{
  "descripcion": "Nueva tarea importante",
  "fechaLimite": "2024-12-31T23:59:59Z"
}
```

**Response (201 Created):**
```json
{
  "id": 4,
  "descripcion": "Nueva tarea importante",
  "fechaLimite": "2024-12-31T23:59:59Z",
  "completada": false,
  "fechaCreacion": "2024-12-10T11:30:00Z"
}
```

## ?? Errores Comunes

### Error de certificado SSL
Si usas cURL y obtienes un error SSL, agrega la opci�n `-k`:
```bash
curl -k https://localhost:7116/api/tareas
```

### Error 404 - Not Found
- Verifica que la URL sea correcta
- Aseg�rate de incluir `/api/` en la ruta

### Error 400 - Bad Request
- Revisa que el JSON est� bien formado
- Verifica que todos los campos requeridos est�n presentes
- Aseg�rate de que la descripci�n tenga entre 5 y 500 caracteres
- Verifica que la fecha l�mite no sea en el pasado

## ??? Configuraci�n

### Cambiar el puerto
Edita `Properties/launchSettings.json`:
```json
"https": {
  "commandName": "Project",
  "applicationUrl": "https://localhost:7116;http://localhost:5116",
  ...
}
```

### Deshabilitar HTTPS (no recomendado para producci�n)
En `Program.cs`, comenta la l�nea:
```csharp
// app.UseHttpsRedirection();
```

## ?? Recursos Adicionales

- [Documentaci�n ASP.NET Core](https://docs.microsoft.com/aspnet/core)
- [Swagger/OpenAPI](https://swagger.io/)
- [REST API Best Practices](https://restfulapi.net/)

## ?? Soluci�n de Problemas

### La aplicaci�n no inicia
```bash
# Limpiar y reconstruir
dotnet clean
dotnet build
dotnet run
```

### Error de puerto en uso
```bash
# Cambiar el puerto en launchSettings.json
# O matar el proceso que usa el puerto
netstat -ano | findstr :7116
taskkill /PID {PID} /F
```

### Ver logs detallados
En `appsettings.Development.json`, cambia el nivel de logging:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Debug"
    }
  }
}
```
