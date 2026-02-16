# Library Management System — ASP.NET Core MVC

Sistema web desarrollado con **ASP.NET Core MVC** y **Entity Framework Core (Code First)**, enfocado en implementar arquitectura backend profesional, manejo seguro de usuarios y modelado relacional para sistemas transaccionales.

---

## Objetivo del proyecto

Construir un sistema de gestión de biblioteca aplicando buenas prácticas reales de desarrollo backend, incluyendo:

- Modelado relacional correcto
- Gestión segura de usuarios
- Arquitectura MVC estructurada
- Base para autenticación profesional

---

## Arquitectura implementada

El sistema utiliza una arquitectura basada en:

- **Domain Models** para entidades de base de datos  
- **ViewModels (DTO Pattern)** para proteger la capa de presentación  
- **Entity Framework Core (Code First)** para control del esquema  
- **Fluent API Configuration** para relaciones, restricciones e integridad  

Esto permite una clara separación entre Presentation, Domain y Data Access.

---

## Sistema de gestión de usuarios

Se implementó un sistema completo de usuarios utilizando:

- Entidad `Usuario` como Identity principal  
- Relación con entidad `Rol`  
- CRUD completo de usuarios  
- Password hashing using **BCrypt**  
- Persistencia exclusiva de `PasswordHash`  
- Uso de **ViewModels** para evitar exposición directa del modelo  
- Restricciones de unicidad en Correo y Cedula  

---

## Modelado relacional avanzado

La entidad `TransaccionBiblioteca` implementa:

- **Foreign Keys**
- **Self-referencing relationships**
- Referencias múltiples hacia `Usuario` (cliente y empleado)
- Control de integridad mediante Fluent API

Permitiendo trazabilidad completa de transacciones.

---

## Tecnologías utilizadas

**Backend**
- ASP.NET Core MVC
- Entity Framework Core
- LINQ
- Fluent API
- Code First Migrations

**Database**
- SQL Server
- Relational Database Modeling
- Foreign Key Constraints

**Security**
- BCrypt Password Hashing
- DTO Pattern (ViewModels)
- Secure Identity Modeling

**Frontend**
- Razor Views
- Bootstrap

---

## Estado actual

Implementado:

- CRUD completo de Usuarios
- Password hashing seguro con BCrypt
- Uso correcto de ViewModels (DTO Pattern)
- Configuración avanzada mediante Fluent API
- Migraciones funcionales con EF Core

En desarrollo:

- Cookie Authentication
- Claims-based Identity
- Policy-based Authorization

---

## Objetivo técnico del proyecto

Aplicar prácticas reales de desarrollo backend utilizando el ecosistema .NET, incluyendo modelado relacional, gestión segura de credenciales y arquitectura MVC profesional.
