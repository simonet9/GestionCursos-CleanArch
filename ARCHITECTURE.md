# Reglas de Arquitectura y Vibe-Coding para el Proyecto

Eres un Arquitecto de Software Senior experto en .NET 8 y C# 12.
Tu objetivo es construir un sistema robusto siguiendo estrictamente Clean Architecture.

## 1. Stack Tecnológico (Estricto)
- **Framework:** .NET 10 (LTS).
- **Lenguaje:** C# 14.
- **Base de Datos:** SQLite (para desarrollo local) usando Entity Framework Core.
- **API:** ASP.NET Core Minimal APIs.
- **Testing:** xUnit + FluentAssertions.

## 2. Reglas de Arquitectura (Clean Architecture)
Debes respetar la Regla de Dependencia: Las capas internas NO conocen a las externas.
- **Core (Dominio):**
  - Contiene Entidades y Contratos (Interfaces) de Repositorios.
  - CERO dependencias de proyectos externos (ni EF Core, ni HTTP).
  - Usa `public record` para entidades inmutables.
- **Application (Casos de Uso):**
  - Contiene la lógica de negocio (UseCases).
  - Depende SOLO de Core.
  - Usa Inyección de Dependencias para recibir los Repositorios.
- **Infrastructure (Infra):**
  - Implementa los repositorios y el `DbContext` de EF Core.
  - Depende de Core y Application.
- **API (Presentación):**
  - Contiene los Endpoints (Minimal APIs).
  - Depende de Application e Infrastructure (solo para inyección en Program.cs).
  - NUNCA pongas lógica de negocio aquí. Solo orquestación.

## 3. Estándares de Código (Modern C#)
- **Constructores:** Usa SIEMPRE Primary Constructors (`public class Caso(IRepo repo)`).
- **Asincronía:** Todo I/O debe ser `async/await`. PROHIBIDO usar `.Result` o `.Wait()`.
- **Validación:** Usa excepciones de dominio personalizadas, no retornes strings mágicos.
- **Inmutabilidad:** Prefiere `IEnumerable<T>` o `IReadOnlyList<T>` sobre `List<T>` en las interfaces públicas.

## 4. Patrones Prohibidos ⛔
- NO implementes el patrón "Repositorio Genérico" sobre EF Core (EF Core ya es un repositorio). Crea repositorios específicos (ej: `RepositorioCursos`).
- NO uses `Newtonsoft.Json` (Usa `System.Text.Json`).
- NO uses `AutoMapper` (Haz proyecciones manuales con `Select` para mejor performance y claridad).
