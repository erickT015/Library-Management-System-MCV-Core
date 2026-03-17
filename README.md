# Library Management System — ASP.NET Core MVC

Sistema de gestión de biblioteca con control de usuarios, inventario y préstamos. Permite manejar operaciones de venta y préstamo con validaciones de negocio y control de acceso por roles.

---

## ¿Qué hace este sistema?

- Gestión de usuarios con roles (admin / cliente)
- Control de inventario para préstamos y ventas
- Procesamiento de transacciones sobre libros
- Validaciones críticas ejecutadas en backend
- Prevención de inconsistencias en operaciones simultáneas

---

## Stack tecnológico

ASP.NET Core MVC
Entity Framework Core
SQL Server
Razor Views
Bootstrap

---

⚙️​ Enfoque técnico
<details> <summary><strong>Ver detalles técnicos</strong></summary>
Arquitectura y patrones

Arquitectura MVC en capas

DTO Pattern para aislamiento de datos

Service Layer para lógica de negocio

Clean Layered Architecture

Seguridad y acceso

Cookie Authentication

Claims-Based Authentication

Role-Based Access Control (RBAC)

Policy-Based Authorization

BCrypt hashing

Manejo de datos y consistencia

ACID Transaction Handling

Soft Delete Strategy

Query-level filtering

Aggregate transaction modeling

</details>

---

## ​ Módulos principales

User Management

Catalog / Inventory Management

Transaction Engine

Image Processing Service

---

🖼 Procesamiento de Imágenes
<details> <summary><strong>Ver detalles del procesamiento de imágenes</strong></summary>

Servicio dedicado para manejo de imágenes:

Redimensionamiento automático

Compresión WebP

Generación de nombres únicos

Reemplazo seguro de recursos

Conceptos aplicados:

File Storage Decoupling

Resource Lifecycle Management

Image Optimization Pipeline

</details>

---

​✳️​ Modelado de Datos
<details> <summary><strong>Ver detalles de modelado de datos</strong></summary>

Implementado con Entity Framework Core (Code First):

Modelado relacional de entidades

Uso de enums para lógica de dominio

Trazabilidad de transacciones

Migraciones para versionado de base de datos

</details>

---

## Cómo ejecutar el proyecto

Clonar el repositorio

Configurar la cadena de conexión a SQL Server

Ejecutar migraciones

Ejecutar la aplicación desde Visual Studio

---

## 📌 Notas

Proyecto enfocado en fortalecer bases de desarrollo backend con .NET, para la gestión diaria de una biblioteca asumiendo empleados físicos en tienda, permisos de solo administrador, autogestión de clientes, partiendo de un crud básico se escaló usando enfoque empresarial especialmente en autenticación, autorización y manejo interno de operaciones.