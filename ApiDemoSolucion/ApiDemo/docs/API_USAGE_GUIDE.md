# Guía de Uso de la API - Ejemplos Prácticos

## Requisitos Previos

- .NET 8 SDK instalado
- SQL Server LocalDB (incluido con Visual Studio)
- Una herramienta para hacer peticiones HTTP (Postman, cURL, o Swagger UI)

## Ejecutar la Aplicación

```bash
cd ApiDemo
dotnet run
```

La API estará disponible en `https://localhost:7xxx` (el puerto exacto se mostrará en la consola).

## Acceder a Swagger UI

Abrir en el navegador: `https://localhost:7xxx/swagger`

---

## Ejemplos de Peticiones

### 1. Obtener todas las tareas

**Request:**
```http
GET https://localhost:7xxx/api/tasks
```

**Response (200 OK):**
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "title": "Completar documentación",
    "description": "Escribir README completo",
    "status": "InProgress",
    "priority": "High",
    "dueDate": "2024-12-31T23:59:59Z",
    "createdAt": "2024-11-01T10:00:00Z",
  "updatedAt": "2024-11-05T15:30:00Z"
  }
]
```

---

### 2. Obtener una tarea específica por ID

**Request:**
```http
GET https://localhost:7xxx/api/tasks/3fa85f64-5717-4562-b3fc-2c963f66afa6
```

**Response (200 OK):**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "title": "Completar documentación",
  "description": "Escribir README completo",
  "status": "InProgress",
  "priority": "High",
  "dueDate": "2024-12-31T23:59:59Z",
  "createdAt": "2024-11-01T10:00:00Z",
  "updatedAt": "2024-11-05T15:30:00Z"
}
```

**Response (404 Not Found):**
```json
{
  "message": "Task not found"
}
```

---

### 3. Crear una nueva tarea

**Request:**
```http
POST https://localhost:7xxx/api/tasks
Content-Type: application/json

{
  "title": "Implementar autenticación JWT",
  "description": "Agregar seguridad con tokens JWT",
  "status": "Pending",
  "priority": "High",
  "dueDate": "2024-12-15T23:59:59Z"
}
```

**Response (201 Created):**
```json
{
  "id": "9ea15f87-2341-4abc-a123-4d56789def01",
  "title": "Implementar autenticación JWT",
  "description": "Agregar seguridad con tokens JWT",
  "status": "Pending",
  "priority": "High",
  "dueDate": "2024-12-15T23:59:59Z",
  "createdAt": "2024-11-06T08:11:25Z",
  "updatedAt": "2024-11-06T08:11:25Z"
}
```

**Headers:**
```
Location: https://localhost:7xxx/api/tasks/9ea15f87-2341-4abc-a123-4d56789def01
```

---

### 4. Crear tarea con validación fallida

**Request:**
```http
POST https://localhost:7xxx/api/tasks
Content-Type: application/json

{
  "title": "AB",
  "status": "InvalidStatus",
  "priority": "High"
}
```

**Response (400 Bad Request):**
```json
{
  "statusCode": 400,
  "message": "Validation failed",
  "errors": [
    "Title must be between 3 and 200 characters",
    "Status must be Pending, InProgress, or Completed"
  ]
}
```

---

### 5. Actualizar una tarea existente

**Request:**
```http
PUT https://localhost:7xxx/api/tasks/9ea15f87-2341-4abc-a123-4d56789def01
Content-Type: application/json

{
  "title": "Implementar autenticación JWT",
  "description": "Agregar seguridad con tokens JWT y refresh tokens",
  "status": "InProgress",
  "priority": "High",
  "dueDate": "2024-12-15T23:59:59Z"
}
```

**Response (200 OK):**
```json
{
  "id": "9ea15f87-2341-4abc-a123-4d56789def01",
  "title": "Implementar autenticación JWT",
  "description": "Agregar seguridad con tokens JWT y refresh tokens",
  "status": "InProgress",
  "priority": "High",
  "dueDate": "2024-12-15T23:59:59Z",
  "createdAt": "2024-11-06T08:11:25Z",
  "updatedAt": "2024-11-06T10:30:00Z"
}
```

---

### 6. Eliminar una tarea

**Request:**
```http
DELETE https://localhost:7xxx/api/tasks/9ea15f87-2341-4abc-a123-4d56789def01
```

**Response (204 No Content):**
```
(Sin contenido en el body)
```

**Response (404 Not Found):**
```json
{
  "message": "Task not found"
}
```

---

### 7. Filtrar tareas por estado

**Request:**
```http
GET https://localhost:7xxx/api/tasks/status/InProgress
```

**Response (200 OK):**
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "title": "Completar documentación",
    "description": "Escribir README completo",
    "status": "InProgress",
    "priority": "High",
    "dueDate": "2024-12-31T23:59:59Z",
    "createdAt": "2024-11-01T10:00:00Z",
    "updatedAt": "2024-11-05T15:30:00Z"
  },
  {
    "id": "2bd74a91-8765-4321-b890-1234567890ab",
    "title": "Implementar tests unitarios",
    "description": "Crear suite de tests con xUnit",
    "status": "InProgress",
    "priority": "High",
    "dueDate": "2024-11-10T23:59:59Z",
    "createdAt": "2024-11-03T09:00:00Z",
    "updatedAt": "2024-11-06T08:00:00Z"
  }
]
```

**Estados válidos:**
- `Pending`
- `InProgress`
- `Completed`

**Request con estado inválido:**
```http
GET https://localhost:7xxx/api/tasks/status/InvalidStatus
```

**Response (400 Bad Request):**
```json
{
  "statusCode": 400,
  "message": "Invalid status",
  "errors": [
    "Status must be Pending, InProgress, or Completed"
  ]
}
```

---

## Ejemplos con cURL

### Crear tarea
```bash
curl -X POST https://localhost:7xxx/api/tasks \
  -H "Content-Type: application/json" \
  -d '{
  "title": "Nueva tarea desde cURL",
    "description": "Descripción de prueba",
    "status": "Pending",
    "priority": "Medium",
    "dueDate": "2024-12-20T23:59:59Z"
}'
```

### Obtener todas las tareas
```bash
curl https://localhost:7xxx/api/tasks
```

### Obtener tarea por ID
```bash
curl https://localhost:7xxx/api/tasks/YOUR-TASK-ID-HERE
```

### Actualizar tarea
```bash
curl -X PUT https://localhost:7xxx/api/tasks/YOUR-TASK-ID-HERE \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Tarea actualizada",
    "description": "Nueva descripción",
    "status": "Completed",
    "priority": "High",
    "dueDate": "2024-12-25T23:59:59Z"
  }'
```

### Eliminar tarea
```bash
curl -X DELETE https://localhost:7xxx/api/tasks/YOUR-TASK-ID-HERE
```

### Filtrar por estado
```bash
curl https://localhost:7xxx/api/tasks/status/InProgress
```

---

## Ejemplos con PowerShell

### Crear tarea
```powershell
$body = @{
    title = "Nueva tarea desde PowerShell"
    description = "Descripción de prueba"
    status = "Pending"
    priority = "High"
    dueDate = "2024-12-20T23:59:59Z"
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:7xxx/api/tasks" `
    -Method Post `
    -Body $body `
    -ContentType "application/json"
```

### Obtener todas las tareas
```powershell
Invoke-RestMethod -Uri "https://localhost:7xxx/api/tasks" -Method Get
```

### Actualizar tarea
```powershell
$taskId = "YOUR-TASK-ID-HERE"
$body = @{
    title = "Tarea actualizada"
    description = "Nueva descripción"
    status = "Completed"
    priority = "High"
    dueDate = "2024-12-25T23:59:59Z"
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:7xxx/api/tasks/$taskId" `
    -Method Put `
    -Body $body `
    -ContentType "application/json"
```

### Eliminar tarea
```powershell
$taskId = "YOUR-TASK-ID-HERE"
Invoke-RestMethod -Uri "https://localhost:7xxx/api/tasks/$taskId" -Method Delete
```

---

## Reglas de Validación

### Para CreateTaskDto y UpdateTaskDto:

| Campo | Requerido | Validación |
|-------|-----------|------------|
| **title** | Sí | Entre 3 y 200 caracteres |
| **description** | No | Máximo 1000 caracteres |
| **status** | Sí | Debe ser: Pending, InProgress o Completed |
| **priority** | Sí | Debe ser: Low, Medium o High |
| **dueDate** | No | Debe ser fecha futura (si se proporciona) |

---

## Inicializar la Base de Datos con Datos de Ejemplo

Para cargar datos de ejemplo, descomentar en `Program.cs`:

```csharp
// Seed Database (Uncomment to initialize with sample data)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
  DbInitializer.Initialize(context);
}
```

Esto creará 6 tareas de ejemplo en la base de datos.

---

## Códigos de Estado HTTP

| Código | Significado | Cuándo se usa |
|--------|-------------|---------------|
| **200 OK** | Éxito | GET exitoso, PUT exitoso |
| **201 Created** | Recurso creado | POST exitoso |
| **204 No Content** | Éxito sin contenido | DELETE exitoso |
| **400 Bad Request** | Petición inválida | Validación fallida, formato incorrecto |
| **404 Not Found** | Recurso no encontrado | GET/PUT/DELETE de ID inexistente |
| **500 Internal Server Error** | Error del servidor | Error no manejado |

---

## Consejos y Buenas Prácticas

1. **IDs únicos**: Cada tarea tiene un GUID único generado automáticamente
2. **Timestamps automáticos**: `CreatedAt` y `UpdatedAt` se gestionan automáticamente
3. **Validación estricta**: Todos los campos se validan antes de persistir
4. **Respuestas consistentes**: Todas las respuestas siguen el mismo formato
5. **Documentación Swagger**: Usar Swagger UI para explorar y probar la API interactivamente

---

## Troubleshooting

### Error: "No se puede conectar a la base de datos"
- Verificar que SQL Server LocalDB esté instalado
- Ejecutar: `dotnet ef database update`

### Error: "Validation failed"
- Verificar que todos los campos requeridos estén presentes
- Verificar que los valores de enum sean correctos (case-sensitive)
- Verificar que las fechas sean futuras si se proporcionan

### Error 404 en todas las peticiones
- Verificar que la API esté corriendo
- Verificar la URL y el puerto correcto
- Asegurarse de usar HTTPS si está configurado

---

## Recursos Adicionales

- **Swagger UI**: `https://localhost:7xxx/swagger`
- **Documentación completa**: Ver `README.md`
- **PRD del proyecto**: Ver `docs/prd/PRD_Demo.md`
