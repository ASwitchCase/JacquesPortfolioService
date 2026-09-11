---
name: add-crud-endpoint
description: Scaffold a new CRUD entity/endpoint for JacquesPortfolioService (Domain + Application + Infrastructure + Api), mirroring the existing Skills/PortfolioProjects vertical-slice pattern. Use when the user wants to add a new endpoint, entity, or resource to this API.
---

# Add CRUD Endpoint

Scaffolds a full vertical slice (Domain → Application → Infrastructure → Api)
for a new entity, following this repo's existing conventions exactly as
implemented for `Skills` and `PortfolioProjects`.

## Step 1 — Ask the user

Use `AskUserQuestion` (or plain questions if that tool isn't available) to
collect:

1. **Entity name** — singular, PascalCase (e.g. `Certification`). The JSON
   shape below doesn't name the entity, so ask for this explicitly.
2. **Data model shape**, as JSON field:type pairs, e.g.
   `{title:String, issuer:String, issuedAt:DateTime}`.
3. **CRUD operations** to generate — multiSelect over: Create, Get by id,
   List (GetAll), Update, Delete. Recommend "all five" as the default.
4. **Authorization** — should the new controller require `[Authorize]`
   (Recommended — matches `SkillsController`/`PortfolioProjectsController`),
   or be anonymous (matches `AuthController.Register`/`Login`)?

Do not guess any of these — if an answer is ambiguous (e.g. an unrecognized
JSON type), ask a follow-up instead of assuming.

## Step 2 — Map JSON types to C#

| JSON type | C# type |
|---|---|
| `String` | `string` |
| `Int` | `int` |
| `Bool`/`Boolean` | `bool` |
| `Number`/`Decimal` | `decimal` |
| `Float`/`Double` | `double` |
| `DateTime`/`Date` | `DateTime` |
| `Guid`/`Uuid` | `Guid` |
| `List<X>`/`X[]` | `List<X>` |

If a type doesn't map cleanly, ask the user rather than guessing.

## Step 3 — Read the reference template first

Before writing any new file, read every file below in full. They are the
living, already-working `PortfolioProjects` slice — the newest and most
complete example in the repo — and are the exact template to mirror. Do not
invent a different structure.

- `src/JacquesPortfolioService.Domain/Entities/PortfolioProject/PortfolioProject.cs`
- `src/JacquesPortfolioService.Domain/Repositories/IPortfolioProjectRepository.cs`
- `src/JacquesPortfolioService.Application/PortfolioProjects/PortfolioProjectDto.cs`
- `src/JacquesPortfolioService.Application/PortfolioProjects/Commands/CreatePortfolioProject/CreatePortfolioProjectCommand.cs`
- `src/JacquesPortfolioService.Application/PortfolioProjects/Commands/CreatePortfolioProject/CreatePortfolioProjectValidator.cs`
- `src/JacquesPortfolioService.Application/PortfolioProjects/Commands/UpdatePortfolioProject/UpdatePortfolioProjectCommand.cs`
- `src/JacquesPortfolioService.Application/PortfolioProjects/Commands/UpdatePortfolioProject/UpdatePortfolioProjectValidator.cs`
- `src/JacquesPortfolioService.Application/PortfolioProjects/Commands/DeletePortfolioProject/DeletePortfolioProjectCommand.cs`
- `src/JacquesPortfolioService.Application/PortfolioProjects/Queries/GetPortfolioProjectById/GetPortfolioProjectById.cs`
- `src/JacquesPortfolioService.Application/PortfolioProjects/Queries/GetPortfolioProjectsList/GetPortfolioProjectsList.cs`
- `src/JacquesPortfolioService.Infrastructure/Persistence/PortfolioProjectConfiguration.cs`
- `src/JacquesPortfolioService.Infrastructure/Persistence/Repositories/PortfolioProjectRepository.cs`
- `src/JacquesPortfolioService.Api/Controllers/PortfolioProjectsController.cs`

## Step 4 — Generate the new entity's files

For entity `{Entity}` (e.g. `Certification`), lowercase `{entity}`
(`certification`), plural route segment `{entities}` (`certifications`),
create the equivalents of the files above, substituting fields per the JSON
shape from Step 1. **Only create Command/Query files for the CRUD operations
selected in Step 1** — e.g. skip `Delete{Entity}Command.cs` and its
validator entirely if Delete wasn't chosen, and skip the corresponding
controller action too.

- `src/JacquesPortfolioService.Domain/Entities/{Entity}/{Entity}.cs`
- `src/JacquesPortfolioService.Domain/Repositories/I{Entity}Repository.cs`
- `src/JacquesPortfolioService.Application/{Entity}s/{Entity}Dto.cs`
- `src/JacquesPortfolioService.Application/{Entity}s/Commands/Create{Entity}/Create{Entity}Command.cs` (+ `Create{Entity}Validator.cs`) — if Create selected
- `src/JacquesPortfolioService.Application/{Entity}s/Commands/Update{Entity}/Update{Entity}Command.cs` (+ `Update{Entity}Validator.cs`) — if Update selected
- `src/JacquesPortfolioService.Application/{Entity}s/Commands/Delete{Entity}/Delete{Entity}Command.cs` — if Delete selected
- `src/JacquesPortfolioService.Application/{Entity}s/Queries/Get{Entity}ById/Get{Entity}ById.cs` — if Get by id selected
- `src/JacquesPortfolioService.Application/{Entity}s/Queries/Get{Entity}sList/Get{Entity}sList.cs` — if List selected
- `src/JacquesPortfolioService.Infrastructure/Persistence/{Entity}Configuration.cs`
- `src/JacquesPortfolioService.Infrastructure/Persistence/Repositories/{Entity}Repository.cs`
- `src/JacquesPortfolioService.Api/Controllers/{Entity}sController.cs`

## Step 5 — Wire up shared files

Edit (don't recreate) these three files:

- `src/JacquesPortfolioService.Infrastructure/Persistence/ApplicationDbContext.cs`
  — add `DbSet<{Entity}> {Entity}s => Set<{Entity}>();` alongside the
  existing `DbSet`s.
- `src/JacquesPortfolioService.Infrastructure/Persistence/IApplicaitonDbContext.cs`
  — mirror the same `DbSet` on the interface. (This filename is genuinely
  misspelled in the repo — don't "fix" it as a drive-by change.)
- `src/JacquesPortfolioService.Infrastructure/DependancyInjection.cs`
  (also genuinely misspelled) — add
  `services.AddScoped<I{Entity}Repository, {Entity}Repository>();` next to
  the existing repository registrations. MediatR, FluentValidation, and
  AutoMapper are assembly-scanned, so the new commands/queries/validators/
  profiles need no explicit registration.

## Step 6 — Conventions to preserve exactly

- **No `namespace` declarations** in Domain/Application/Infrastructure/Api
  production code (tests and EF migrations are the only files that use them).
- **Primary-constructor DI** everywhere: `class Foo(IBar bar) : IFoo`.
- **One file per command/query** holding both the MediatR record and its
  handler class together.
- **One `{Entity}Dto.cs`** holding both the DTO record and its AutoMapper
  `Profile` subclass together.
- **Domain entity**: private setters, private parameterless constructor, a
  static `Create(...)` factory that throws a plain `Exception` (not a custom
  exception type) on invalid input, plus explicit `Update...()` mutator
  method(s) — never public setters.
- **404s via `KeyNotFoundException`**: handlers for Update/Delete/GetById
  throw a bare `KeyNotFoundException` (no message) when the entity isn't
  found. `GlobalExceptionHandler` middleware maps this to 404 automatically
  — never add try/catch in the controller.
- **Validators only for Create/Update**, using FluentValidation
  `RuleFor(x => x.field).NotEmpty()`-style rules per required field. Ask the
  user if any field needs stricter rules (e.g. max length); default to
  `NotEmpty()` otherwise.
- **Controller**: `[ApiController]`, `[Route("api/{entities}")]`, optional
  `[Authorize]` at class level per the Step 1 answer, primary constructor
  `(ISender sender)`, and actions that only call `sender.Send(...)` and map
  the result to an HTTP response — no business logic in the controller.

## Step 7 — Generate the EF Core migration

Once the new files compile, generate (but do not apply) a migration:

```
dotnet ef migrations add Add{Entity}s --project src/JacquesPortfolioService.Infrastructure --startup-project src/JacquesPortfolioService.Api
```

This only writes migration files locally — it does not touch any database.
Do **not** run `dotnet ef database update` as part of this skill; that's a
separate step the user runs themselves when ready.

## Step 8 — Verify

Run `dotnet build` on the solution and fix/report any compile errors before
finishing.

## Step 9 — Optional

`API.md` is already stale relative to the actual endpoints in this repo.
Mention to the user that they may want to document the new endpoint there,
but don't do it unprompted.
