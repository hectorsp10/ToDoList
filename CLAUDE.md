# ToDoList

API REST de práctica en .NET. El objetivo es **aprender**, no terminar rápido.

## Cómo trabajar conmigo

- **Haz solo lo que te pida en cada momento.** No implementes partes del proyecto por adelantado ni completes cosas que no he mencionado.
- Si una petición es ambigua o choca con lo definido en este fichero, pregúntame antes de escribir código.
- Cuando tomes una decisión de diseño no evidente, explícamela en una o dos frases.
- No instales paquetes NuGet ni herramientas que no estén en la lista de abajo sin preguntarme antes.
- No borres, muevas ni renombres ficheros o carpetas existentes sin preguntarme.
- No ejecutes `dotnet new` sobre proyectos que ya existen.
- Después de cambiar código, ejecuta `dotnet build` (y `dotnet test` si afecta a algo con tests) y dime el resultado.

## Stack

- Última versión LTS de .NET, ASP.NET Core con **Controllers** (no Minimal APIs).
- EF Core con SQLite.
- Tests: xUnit, FluentAssertions 7.2.0 (no la 8), Bogus y Moq.
- OpenAPI + Scalar para la documentación interactiva.
- Todo el código (clases, métodos, variables) en inglés.

## Arquitectura

Hexagonal, organizada **por feature** en todas las capas (la feature actual es `ToDos`).

Reglas de dependencia:
- **Domain**: no referencia a ningún proyecto ni paquete externo.
- **Application** → Domain.
- **Infrastructure** → Application, Domain.
- **API** → Application, Infrastructure (Infrastructure solo para registrar la DI en `Program.cs`).
- Los controllers dependen solo de las interfaces de los servicios de Application, nunca del repositorio.
- EF Core trabaja **solo con `TodoRepositoryEntity`**, nunca con la entidad de dominio. `TodoMapper` traduce entre ambas.

## Estructura

```
ToDoList/
├── src/
│   ├── ToDoList.Domain/
│   │   └── ToDos/
│   │       ├── Entities/        TodoEntity, TodoStatusEnum
│   │       ├── ValueObjects/    TodoId, TodoTitle, TodoDescription, TodoStatus, TodoCreatedAt
│   │       ├── Repositories/    ITodoRepository
│   │       └── Exceptions/      TodoNotFoundException, InvalidTodoException
│   ├── ToDoList.Application/
│   │   ├── ToDos/
│   │   │   ├── DTOs/            TodoDto
│   │   │   └── Services/
│   │   │       ├── Create/      CreateTodoCommand, ICreateTodoService, CreateTodoService
│   │   │       ├── Get/         GetTodoQuery, IGetTodoService, GetTodoService
│   │   │       ├── Search/      ISearchTodosService, SearchTodosService
│   │   │       ├── Update/      UpdateTodoStatusCommand, IUpdateTodoStatusService, UpdateTodoStatusService
│   │   │       └── Delete/      DeleteTodoCommand, IDeleteTodoService, DeleteTodoService
│   │   └── DependencyInjection.cs
│   ├── ToDoList.Infrastructure/
│   │   ├── ToDos/
│   │   │   ├── RepositoryEntities/   TodoRepositoryEntity
│   │   │   ├── Mappers/              TodoMapper
│   │   │   ├── Repositories/         TodoRepository
│   │   │   └── Configurations/       TodoRepositoryEntityConfiguration
│   │   ├── Shared/Persistence/       AppDbContext, Migrations/
│   │   └── DependencyInjection.cs
│   └── ToDoList.API/
│       ├── ToDos/
│       │   ├── Controllers/     TodosController
│       │   └── Contracts/       CreateTodoRequest, UpdateTodoStatusRequest, TodoResponse
│       ├── Shared/Middleware/   ExceptionHandlingMiddleware
│       └── Program.cs
└── tests/
    ├── ToDoList.Domain.Tests/ToDos/
    │   ├── Entities/            TodoEntityMother, TodoEntityTest
    │   └── ValueObjects/        Un *Test por value object
    ├── ToDoList.Application.Tests/ToDos/Services/<Operación>/
    │                            <Command>Mother, <Command>Test, <Servicio>Test
    ├── ToDoList.Infrastructure.Tests/ToDos/
    │   ├── Mappers/             TodoMapperTest
    │   └── Repositories/        TodoRepositoryTest
    └── ToDoList.API.IntegrationTests/ToDos/
                                 TodosControllerTest
```

Los namespaces coinciden con las carpetas. Las carpetas de los tests replican las del proyecto que prueban.

## Domain

- **Value objects**: `sealed record` con constructor privado, propiedad `Value` y métodos de fábrica que validan y lanzan `InvalidTodoException`.
  - `TodoId` (`Guid`): `New()`; `From(Guid)` rechaza `Guid.Empty`.
  - `TodoTitle` (`string`): obligatorio, `Trim()`, máximo 200 caracteres (constante `MaxLength`).
  - `TodoDescription` (`string?`): opcional (vacío → `null`), máximo 1000 caracteres (constante `MaxLength`).
  - `TodoStatus`: envuelve `TodoStatusEnum`; `Initial()` devuelve `Planned`; `Create(...)` valida con `Enum.IsDefined`.
  - `TodoCreatedAt` (`DateTime` UTC): `Now()`; `From(DateTime)` rechaza fechas cuyo `Kind` no sea `Utc`.
- **`TodoStatusEnum`**: `Planned`, `InProgress`, `Done`.
- **`TodoEntity`**: `sealed class`, propiedades con su value object y `private set`, sin constructor vacío. Métodos de fábrica `Create(title, description)` (TODO nuevo) y `Restore(...)` (reconstruye uno existente). Método `ChangeStatus(TodoStatus)`.
- **`ITodoRepository`**: `GetByIdAsync(TodoId)`, `GetAllAsync()`, `AddAsync(TodoEntity)`, `UpdateAsync(TodoEntity)`, `DeleteAsync(TodoId)`. Todos asíncronos y con `CancellationToken`.

## Application

- Commands y queries son `record` con tipos primitivos y van en la carpeta de su operación.
- Cada servicio tiene su interfaz (puerto de entrada) en la misma carpeta y un único método `ExecuteAsync(..., CancellationToken)`.
- Los servicios dependen solo de `ITodoRepository` y devuelven `TodoDto` (con `FromEntity`).
- `Get`, `Update` y `Delete` lanzan `TodoNotFoundException` si el TODO no existe.
- `AddApplication()` registra cada servicio a mano como scoped (sin escaneo de ensamblados).

## Infrastructure

- `TodoRepositoryEntity`: propiedades públicas con tipos primitivos (`Status` guardado como string).
- `TodoMapper`: `ToRepositoryEntity` y `ToDomain` (usa `TodoEntity.Restore` y `DateTime.SpecifyKind(..., Utc)`, porque SQLite no conserva el `Kind`).
- `UpdateAsync` carga la `TodoRepositoryEntity` existente, copia los cambios y guarda.
- `AddInfrastructure(IConfiguration)` registra el `DbContext` (connection string `DefaultConnection`) y el repositorio.
- Migraciones en `Shared/Persistence/Migrations`, con `ToDoList.API` como proyecto de inicio. `dotnet-ef` como herramienta local, nunca global.

## API

| Método | Ruta | Éxito | Errores |
|---|---|---|---|
| POST | `/api/todos` | 201 + `Location` | 400 |
| GET | `/api/todos/{id:guid}` | 200 | 404 |
| GET | `/api/todos` | 200 (`[]` si está vacío) | — |
| PATCH | `/api/todos/{id:guid}/status` | 200 | 400, 404 |
| DELETE | `/api/todos/{id:guid}` | 204 | 404 |

- Los contracts HTTP son distintos de los commands y DTOs de Application; se mapean en el controller.
- `ExceptionHandlingMiddleware`: `InvalidTodoException` → 400, `TodoNotFoundException` → 404 y cualquier otra → 500. Siempre con `ProblemDetails`.
- Enums serializados como string (`JsonStringEnumConverter`).

## Paquetes permitidos

| Proyecto | Paquetes |
|---|---|
| Application | `Microsoft.Extensions.DependencyInjection.Abstractions` |
| Infrastructure | `Microsoft.EntityFrameworkCore.Sqlite` |
| API | `Microsoft.EntityFrameworkCore.Design`, `Scalar.AspNetCore` |
| Domain.Tests | `FluentAssertions` 7.2.0, `Bogus` |
| Application.Tests | `FluentAssertions` 7.2.0, `Bogus`, `Moq` |
| Infrastructure.Tests | `FluentAssertions` 7.2.0, `Bogus` |
| API.IntegrationTests | `FluentAssertions` 7.2.0, `Microsoft.AspNetCore.Mvc.Testing` |

Además de estos, solo se permiten los que traen las plantillas de `dotnet new`. Usa la misma versión de EF Core en todos los proyectos.

## Convenciones de tests

- xUnit: `[Fact]`, y `[Theory]` + `[InlineData]` para casos parametrizados. La preparación común va en el constructor y las dependencias en campos `private readonly`.
- FluentAssertions, con `using (new AssertionScope())` cuando haya varias comprobaciones.
- Nombres de test con el formato `WhenX_ShouldY` o `MethodName_ShouldY`.
- Arrange / Act / Assert separados por líneas en blanco, sin comentarios.
- **Object Mothers**: clase `<Tipo>Mother` con un método estático `GenerateCorrectly(...)` con parámetros opcionales nullable; lo que no se pasa se genera con Bogus.
- `TodoEntityMother` vive en Domain.Tests y se reutiliza desde Application.Tests e Infrastructure.Tests.
- Repositorio y tests de integración con **SQLite en memoria** (`Data Source=:memory:` con la conexión abierta). Nunca el proveedor `InMemory` de EF Core.