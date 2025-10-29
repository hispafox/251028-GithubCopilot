# ?? INICIO R�PIDO - 3 Pasos para Ejecutar tu API

## ? �Tu API est� lista para usar!

### ?? Paso 1: Navegar al proyecto
```bash
cd TareasAPI
```

### ?? Paso 2: Ejecutar la aplicaci�n
```bash
dotnet run
```

Ver�s algo como:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7116
      Now listening on: http://localhost:5116
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

### ?? Paso 3: Abrir Swagger
Abre tu navegador en:
```
https://localhost:7116/swagger
```
(Usa el puerto que aparezca en tu consola)

---

## ?? Prueba R�pida

### En Swagger UI:
1. Haz clic en **GET /api/tareas**
2. Clic en "Try it out"
3. Clic en "Execute"
4. �Ver�s las 3 tareas de ejemplo!

### Crear una tarea:
1. Haz clic en **POST /api/tareas**
2. Clic en "Try it out"
3. Modifica el JSON:
```json
{
  "descripcion": "Mi primera tarea desde Swagger",
  "fechaLimite": "2024-12-31T23:59:59Z"
}
```
4. Clic en "Execute"
5. �Tarea creada! Ver�s el ID generado

---

## ?? Archivos de Ayuda

- **README.md** ? Documentaci�n completa
- **GUIA_INICIO.md** ? Gu�a detallada paso a paso
- **RESUMEN.md** ? Resumen del proyecto completo
- **TareasAPI-Tests.http** ? Pruebas de endpoints (VS Code)
- **MIGRACION_EF_CORE.md** ? C�mo migrar a base de datos real

---

## ??? Comandos �tiles

```bash
# Ejecutar con auto-reload (desarrollo)
dotnet watch run

# Compilar sin ejecutar
dotnet build

# Limpiar y reconstruir
dotnet clean
dotnet build

# Ver informaci�n del proyecto
dotnet --info
```

---

## ?? Endpoints Principales

| Acci�n | M�todo | URL |
|--------|--------|-----|
| Ver todas | GET | `/api/tareas` |
| Ver una | GET | `/api/tareas/1` |
| Crear | POST | `/api/tareas` |
| Actualizar | PUT | `/api/tareas/1` |
| Eliminar | DELETE | `/api/tareas/1` |
| Marcar completa | PATCH | `/api/tareas/1/completar` |

---

## ?? Caracter�sticas Implementadas

? 7 endpoints REST completos
? Validaci�n de datos autom�tica
? Documentaci�n con Swagger
? 3 tareas de ejemplo precargadas
? Logging integrado
? CORS habilitado
? Manejo de errores descriptivos
? Respuestas HTTP est�ndar

---

## ?? Tecnolog�as Usadas

- **ASP.NET Core 8.0**
- **Minimal API con Controladores**
- **Repository Pattern**
- **Swagger/OpenAPI**
- **Dependency Injection**
- **Data Annotations para validaci�n**

---

## ?? Pr�ximos Pasos

1. **Familiar�zate** con los endpoints en Swagger
2. **Prueba** crear, editar y eliminar tareas
3. **Revisa** el c�digo en los archivos .cs
4. **Experimenta** modificando funcionalidades
5. **Migra** a Entity Framework Core (ver MIGRACION_EF_CORE.md)

---

## ? Necesitas Ayuda?

- **Problema t�cnico?** ? Revisa GUIA_INICIO.md
- **Quieres entender el c�digo?** ? Revisa RESUMEN.md
- **Quieres base de datos real?** ? Revisa MIGRACION_EF_CORE.md

---

## ?? �Disfruta tu API!

Todo est� funcionando perfectamente. �Es hora de probarla!

**Recuerda**: Los datos se almacenan en memoria, as� que se pierden al reiniciar la aplicaci�n. Si quieres persistencia real, sigue la gu�a de migraci�n a Entity Framework Core.

---

**Estado del Proyecto**: ? Completado y Funcional
**Compilaci�n**: ? Sin errores
**Listo para**: Desarrollo, Aprendizaje y Extensi�n
