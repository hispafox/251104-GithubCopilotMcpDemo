# Task Management API

API RESTful para gestión de tareas implementada con .NET 8, Entity Framework Core y SQL Server LocalDB.

## ??? Arquitectura

El proyecto sigue una arquitectura en capas:

```
ApiDemo/
??? Core/       # Capa de dominio
?   ??? Entities/ # Entidades del dominio
?   ??? Enums/            # Enumeraciones
?   ??? Interfaces/       # Contratos de repositorio y UoW
??? Application/              # Capa de aplicación
?   ??? DTOs/           # Data Transfer Objects
?   ??? Validators/      # Validadores FluentValidation
?   ??? Mappings/      # Perfiles de AutoMapper
?   ??? Services/ # Servicios de negocio
??? Infrastructure/        # Capa de infraestructura
?   ??? Data/           # DbContext de EF Core
?   ??? Repositories/  # Implementación del repositorio genérico
?   ??? UnitOfWork/    # Implementación del Unit of Work
??? Controllers/          # Controladores Web API
??? Middlewares/   # Middleware personalizado
??? Program.cs  # Configuración de la aplicación
```

## ?? Tecnologías Utilizadas

- **.NET 8** - Framework de aplicación
- **Entity Framework Core 8** - ORM
- **SQL Server LocalDB** - Base de datos
- **AutoMapper** - Mapeo objeto-objeto
- **FluentValidation** - Validación de datos
- **Swagger/OpenAPI** - Documentación de API

## ?? Patrones Implementados

- **Repository Pattern** - Abstracción de acceso a datos
- **Unit of Work Pattern** - Gestión de transacciones
- **DTO Pattern** - Separación de modelos de dominio y transporte
- **Dependency Injection** - Inversión de control

## ?? Configuración Inicial

### 1. Restaurar paquetes NuGet

```bash
dotnet restore
```

### 2. Aplicar migraciones (si no está creada la BD)

```bash
dotnet ef database update
```

### 3. Ejecutar la aplicación

```bash
dotnet run
```

La API estará disponible en:
- HTTPS: `https://localhost:7xxx`
- HTTP: `http://localhost:5xxx`
- Swagger UI: `https://localhost:7xxx/swagger`

## ?? Endpoints Disponibles

### Tasks

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/tasks` | Obtener todas las tareas |
| `GET` | `/api/tasks/{id}` | Obtener una tarea por ID |
| `POST` | `/api/tasks` | Crear una nueva tarea |
| `PUT` | `/api/tasks/{id}` | Actualizar una tarea existente |
| `DELETE` | `/api/tasks/{id}` | Eliminar una tarea |
| `GET` | `/api/tasks/status/{status}` | Filtrar tareas por estado |

### Estados válidos (TaskStatus)
- `Pending`
- `InProgress`
- `Completed`

### Prioridades válidas (TaskPriority)
- `Low`
- `Medium`
- `High`

## ?? Ejemplos de Uso

### Crear una tarea

```bash
POST /api/tasks
Content-Type: application/json

{
  "title": "Implementar API de tareas",
  "description": "Crear una API RESTful completa",
  "status": "InProgress",
  "priority": "High",
  "dueDate": "2024-12-31T23:59:59Z"
}
```

### Actualizar una tarea

```bash
PUT /api/tasks/{id}
Content-Type: application/json

{
  "title": "Implementar API de tareas",
  "description": "Crear una API RESTful completa con tests",
  "status": "Completed",
  "priority": "High",
  "dueDate": "2024-12-31T23:59:59Z"
}
```

### Obtener tareas por estado

```bash
GET /api/tasks/status/InProgress
```

## ? Validaciones

### CreateTaskDto / UpdateTaskDto

- **Title**: Requerido, entre 3-200 caracteres
- **Description**: Opcional, máximo 1000 caracteres
- **Status**: Requerido, debe ser: Pending, InProgress o Completed
- **Priority**: Requerido, debe ser: Low, Medium o High
- **DueDate**: Opcional, debe ser fecha futura si se proporciona

## ??? Base de Datos

### Connection String

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TaskManagementDb;Trusted_Connection=true;TrustServerCertificate=true"
  }
}
```

### Tabla: Tasks

| Campo | Tipo | Descripción |
|-------|------|-------------|
| Id | uniqueidentifier | Primary Key |
| Title | nvarchar(200) | Título de la tarea |
| Description | nvarchar(1000) | Descripción detallada |
| Status | nvarchar(max) | Estado (Pending, InProgress, Completed) |
| Priority | nvarchar(max) | Prioridad (Low, Medium, High) |
| DueDate | datetime2 | Fecha de vencimiento (nullable) |
| CreatedAt | datetime2 | Fecha de creación |
| UpdatedAt | datetime2 | Fecha de última actualización |

## ??? Comandos útiles de Entity Framework

### Crear una nueva migración

```bash
dotnet ef migrations add NombreDeLaMigracion
```

### Aplicar migraciones pendientes

```bash
dotnet ef database update
```

### Revertir a una migración específica

```bash
dotnet ef database update NombreDeLaMigracion
```

### Eliminar la última migración

```bash
dotnet ef migrations remove
```

### Ver el SQL que generará una migración

```bash
dotnet ef migrations script
```

## ?? Manejo de Errores

La API implementa un middleware centralizado de manejo de errores que devuelve respuestas estructuradas:

### Respuesta de error estándar

```json
{
  "statusCode": 400,
  "message": "Validation failed",
  "errors": [
    "Title is required",
    "DueDate must be in the future"
  ]
}
```

### Códigos de estado HTTP

- `200 OK` - Operación exitosa
- `201 Created` - Recurso creado
- `204 No Content` - Eliminación exitosa
- `400 Bad Request` - Validación fallida
- `404 Not Found` - Recurso no encontrado
- `500 Internal Server Error` - Error del servidor

## ?? Documentación Swagger

La documentación interactiva de la API está disponible en:

```
https://localhost:7xxx/swagger
```

Desde Swagger UI puedes:
- Ver todos los endpoints disponibles
- Probar los endpoints directamente
- Ver los modelos de datos
- Ver las respuestas esperadas

## ?? Testing

Para probar la API puedes usar:

1. **Swagger UI** - Interfaz web interactiva
2. **Postman** - Cliente API popular
3. **cURL** - Línea de comandos
4. **HTTPie** - Herramienta CLI amigable

### Ejemplo con cURL

```bash
# Crear una tarea
curl -X POST https://localhost:7xxx/api/tasks \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Mi primera tarea",
    "status": "Pending",
    "priority": "Medium"
  }'

# Obtener todas las tareas
curl https://localhost:7xxx/api/tasks
```

## ?? Recursos Adicionales

- [Documentación de .NET 8](https://docs.microsoft.com/dotnet/)
- [Entity Framework Core](https://docs.microsoft.com/ef/core/)
- [FluentValidation](https://docs.fluentvalidation.net/)
- [AutoMapper](https://automapper.org/)

## ?? Contribución

Para contribuir al proyecto:

1. Fork el repositorio
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## ?? Licencia

Este proyecto es de código abierto y está disponible bajo la [MIT License](LICENSE).

## ? Características Implementadas

- ? CRUD completo de tareas
- ? Validación robusta con FluentValidation
- ? Repository Pattern y Unit of Work
- ? Mapeo automático con AutoMapper
- ? Manejo centralizado de errores
- ? Documentación Swagger/OpenAPI
- ? Base de datos SQL Server LocalDB
- ? Arquitectura en capas
- ? Inyección de dependencias
- ? Migraciones de Entity Framework Core

## ?? Próximos Pasos

- [ ] Implementar autenticación JWT
- [ ] Agregar paginación y filtros avanzados
- [ ] Implementar caché con Redis
- [ ] Agregar tests unitarios y de integración
- [ ] Implementar logging con Serilog
- [ ] Agregar soporte para múltiples usuarios
- [ ] Implementar notificaciones
- [ ] Agregar exportación de tareas (PDF, Excel)
