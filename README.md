Library Management System — ASP.NET Core MVC

Sistema web de gestión de biblioteca desarrollado con ASP.NET Core MVC, enfocado en la implementación de arquitectura backend profesional, control de acceso seguro y procesamiento transaccional con manejo de inventario.

El objetivo principal del proyecto fue construir una aplicación aplicando prácticas reales utilizadas en entornos empresariales, priorizando la seguridad, consistencia de datos y separación clara de responsabilidades.

## Objetivo del Proyecto

Diseñar un sistema multi-rol capaz de administrar usuarios, catálogo e historial de operaciones mediante reglas de negocio aplicadas completamente desde backend.

El sistema fue desarrollado poniendo énfasis en:

Arquitectura mantenible

Control de acceso por rol

Integridad transaccional

Modelado relacional real

Seguridad a nivel aplicación


# Arquitectura Implementada

La aplicación sigue una arquitectura basada en ASP.NET Core MVC con separación explícita entre capas:

Domain Models

DTO Pattern (ViewModels)

Controller Application Layer

Service Layer Abstraction

Data Access Layer (EF Core)

El uso de DTO Pattern permite aislar entidades de persistencia evitando exposición directa del modelo de base de datos hacia la capa de presentación.

Conceptos aplicados:

Layered Architecture

Separation of Concerns

Dependency Injection

Service-Oriented Design


# Autenticación e Identidad

Se implementó un sistema de autenticación personalizado sin utilizar scaffolding automático.

Características principales:

Cookie-Based Authentication

Claims-Based Identity

Custom Authentication Workflow

Authentication Ticket Management

Session Persistence Control

La identidad del usuario autenticado se mantiene mediante Claims, permitiendo que el backend tome decisiones de autorización sin depender de estado en frontend.


# Autorización y Control de Acceso

El acceso a funcionalidades y datos se controla completamente desde backend mediante:

Policy-Based Authorization

Role-Based Access Control (RBAC)

Application-Level Row Security

Ownership Validation

Privilege Escalation Prevention

Cada consulta es filtrada dinámicamente dependiendo del rol autenticado, garantizando aislamiento de información entre usuarios.


# Gestión de Usuarios

Se desarrolló un módulo completo de administración de usuarios incluyendo:

Registro autónomo de clientes

Administración por roles

Edición controlada de perfiles

Protección de campos sensibles

Desactivación lógica de cuentas

Conceptos implementados:

Secure User Management

DTO-Based Input Control

Overposting Protection

Soft Delete Pattern

Self-Service Profile Management

Las credenciales se almacenan mediante:

BCrypt Password Hashing


# Sistema Transaccional e Inventario

El núcleo del sistema consiste en un motor transaccional capaz de procesar múltiples libros dentro de una misma operación.

Capacidades implementadas:

Multi-Item Transaction Processing

Atomic Database Transactions

ACID Consistency Enforcement

Inventory Consistency Control

Real-Time Stock Validation

Backend Financial Validation

Transaction Number Generation

Las reglas del negocio se aplican exclusivamente en backend mediante Business Rule Enforcement.

# Gestión de Catálogo

El módulo administrativo permite la gestión completa del inventario:

Administración de libros y categorías

Control separado de stock (venta / préstamo)

Filtrado dinámico

Conservación histórica de registros

Conceptos aplicados:

Inventory Management Module

Query Composition Pattern

Relational Data Loading

Soft Delete Strategy


# Procesamiento de Imágenes

Se implementó una capa de servicios dedicada al manejo de imágenes de libros.

Funcionalidades:

Redimensionamiento automático

Compresión WebP

Generación de nombres únicos

Reemplazo seguro de recursos

Conceptos aplicados:

Service Layer Abstraction

File Storage Decoupling

Image Optimization Pipeline

Resource Lifecycle Management

# Modelado de Datos

El sistema fue construido utilizando Entity Framework Core (Code First) aplicando:

Relaciones múltiples entre entidades

Aggregate Transaction Modeling

Enum-Based Domain Modeling

Query-Level Filtering

Permitiendo trazabilidad completa de operaciones dentro del sistema.


# Tecnologías Utilizadas
Backend

ASP.NET Core MVC

Entity Framework Core

LINQ

SQL Server

Arquitectura

MVC Pattern

DTO Pattern

Service Layer

Transaction Management

Seguridad

Cookie Authentication

Claims-Based Identity

Role-Based Authorization

BCrypt Password Hashing

Frontend

Razor Views

Bootstrap


# Enfoque Técnico del Proyecto

Este proyecto demuestra experiencia práctica en:

Secure Backend Architecture

Identity & Access Management

Transactional System Design

Inventory Synchronization

Role-Driven Workflows

Enterprise MVC Application Structure
---

## Objetivo técnico del proyecto

Aplicar prácticas reales de desarrollo backend utilizando el ecosistema .NET, incluyendo modelado relacional, gestión segura de credenciales y arquitectura MVC profesional a partir de un crud base.
