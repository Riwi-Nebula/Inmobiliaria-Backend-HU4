# Inmobiliaria Backend – Gestión de Clientes (HU4)

Este proyecto corresponde al módulo de **Gestión de Clientes** para una API de Inmobiliaria desarrollada en **ASP.NET Core 8**, utilizando:

- Entity Framework Core 8
- MySQL (Pomelo)
- JWT Authentication (Access Token + Refresh Token)
- Roles (Admin y Customer)

La API permite:

- Registro y login con JWT
- Uso de Refresh Token
- CRUD de clientes con permisos por rol
- Validaciones por ID para usuarios Customer

---

# Estructura del Proyecto
```
Inmobiliaria-Backend-HU4/
│── Inmobiliaria-Backend-HU4.Api
│── Inmobiliaria-Backend-HU4.Application
│── Inmobiliaria-Backend-HU4.Domain
│── Inmobiliaria-Backend-HU4.Infrastructure
│── Inmobiliaria-Backend-HU4.sln
```

---

# Requisitos Previos

- .NET SDK **8.0**
- MySQL Server
- `dotnet-ef` instalado

Instalar `dotnet-ef`:

```bash
  dotnet tool install --global dotnet-ef
```

---

# Instalación de paquetes necesarios

## Paquetes para el proyecto API
```bash
  dotnet add Inmobiliaria-Backend-HU4.Api package Pomelo.EntityFrameworkCore.MySql
  dotnet add Inmobiliaria-Backend-HU4.Api package Microsoft.AspNetCore.Authentication.JwtBearer
  dotnet add Inmobiliaria-Backend-HU4.Api package Microsoft.IdentityModel.Tokens
  dotnet add Inmobiliaria-Backend-HU4.Api package Swashbuckle.AspNetCore
```

## Paquetes para el proyecto Infrastructure (versión .NET 8)
```bash
  dotnet add Inmobiliaria-Backend-HU4.Infrastructure package Microsoft.EntityFrameworkCore --version 8.0.10
  dotnet add Inmobiliaria-Backend-HU4.Infrastructure package Microsoft.EntityFrameworkCore.Relational --version 8.0.10
  dotnet add Inmobiliaria-Backend-HU4.Infrastructure package Microsoft.EntityFrameworkCore.Design --version 8.0.10
  dotnet add Inmobiliaria-Backend-HU4.Infrastructure package Microsoft.EntityFrameworkCore.Abstractions --version 8.0.10
  dotnet add Inmobiliaria-Backend-HU4.Infrastructure package Pomelo.EntityFrameworkCore.MySql --version 8.0.2
```

---

# Configuración de Base de Datos
Agregar cadena de conexión en `appsettings.json` dentro del proyecto Api:
```json
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=inmobiliaria;user=root;password=tu_password;"
}
```

---

# Migraciones

## Crear migración
```bash
  dotnet ef database update \
--project Inmobiliaria-Backend-HU4.Infrastructure \
--startup-project Inmobiliaria-Backend-HU4.Api
```

## Aplicar migración a la base de datos
```bash
 dotnet ef database update \
--project Inmobiliaria-Backend-HU4.Infrastructure \
--startup-project Inmobiliaria-Backend-HU4.Api
```

---

# Ejecutar el proyecto
```bash
  dotnet run --project Inmobiliaria-Backend-HU4.Api
```

La API quedará disponible normalmente en:
```bash
  http://localhost:5055
```

Swagger disponible en:
```bash
  /swagger
```

---

# Autenticación (JWT + Refresh Token)
El proyecto implementa:

✔ Login

✔ Registro

✔ Access Token (15 minutos)

✔ Refresh Token (7 días)

✔ Endpoint /auth/refresh

✔ Roles: Admin y Customer

✔ Validación por ID para Customer (solo puede editar/ver su propio perfil)

---

# Roles y Permisos

| Endpoint                     | Admin | Customer                         |
|------------------------------|--------|----------------------------------|
| GET /api/customer            | ✔      | ❌                                |
| POST /api/customer           | ✔      | ❌                                |
| DELETE /api/customer/{id}    | ✔      | ❌                                |
| GET /api/customer/{id}       | ✔      | ✔ (solo su propio ID)            |
| PUT /api/customer/{id}       | ✔      | ✔ (solo su propio ID)            |


---

# Estado del Proyecto
Este módulo incluye:

- Registro

- Login

- Refresh Token

- Seguridad por roles

- Seguridad estricta por ID

- CRUD de clientes completo

---

