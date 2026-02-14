# 🎓 Sistema de Gestión de Cursos - Clean Architecture

![.NET 10](https://img.shields.io/badge/.NET-10.0-purple) ![Architecture](https://img.shields.io/badge/Architecture-Clean-green) ![Tests](https://img.shields.io/badge/Tests-Passing-success)

Este proyecto es una implementación de referencia de un sistema de gestión académica construido con **.NET 10 (Preview)** y **C# 14**, siguiendo estrictamente los principios de **Clean Architecture** y **Domain-Driven Design (DDD)**.

El objetivo es demostrar cómo desacoplar la lógica de negocio de la infraestructura y la UI, garantizando mantenibilidad y testabilidad.

## 🏗️ Arquitectura del Proyecto

La solución sigue la Regla de Dependencia, donde las capas internas no conocen a las externas:

- **Core (Dominio):** Contiene las Entidades (`records`) y los Contratos de Repositorio. No tiene dependencias externas.
- **Application (Casos de Uso):** Orquesta la lógica de negocio. Usa Inyección de Dependencias para interactuar con la infraestructura.
- **Infrastructure:** Implementación de persistencia con **Entity Framework Core**.
- **API:** Endpoints ligeros usando **Minimal APIs**.

## 🚀 Stack Tecnológico

- **Framework:** .NET 10
- **Lenguaje:** C# 14 (Uso extensivo de *Primary Constructors* y *Records*)
- **Base de Datos:** SQLite (Entorno de desarrollo) / SQL Server Ready
- **ORM:** Entity Framework Core
- **Testing:** xUnit + FluentAssertions + NSubstitute

## 💡 Decisiones de Diseño Clave

1.  **Inmutabilidad:** Se utilizaron `records` para todas las entidades del dominio y DTOs para garantizar la inmutabilidad y seguridad en hilos.
2.  **Rich Domain Model:** La lógica de validación (ej. `CapacidadMaxima`) reside en los Casos de Uso y Entidades, no en los Controladores.
3.  **Minimal APIs:** Se optó por Minimal APIs sobre Controllers tradicionales para reducir el *boilerplate* y mejorar el rendimiento.
4.  **Testing:** Cobertura de tests unitarios para los Casos de Uso, mockeando las dependencias de base de datos.

## 🛠️ Cómo ejecutarlo

1.  Clonar el repositorio.
2.  Restaurar paquetes: `dotnet restore`
3.  Ejecutar la API: `dotnet run --project GestionCursos.API`
4.  Correr los tests: `dotnet test`
