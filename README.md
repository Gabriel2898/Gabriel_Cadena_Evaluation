# Sistema de Gestión y Transaccion de Producto - FullStack

Aplicación completa para gestión de inventarios con:
- **Frontend**: Angular 15+ con Angular Material
- **Backend**: .NET 6 con arquitectura de microservicios
- **Base de datos**: SQL Server

## 🛠 Requisitos Técnicos

### 🔧 Backend (.NET Core 6)

**Requisitos:**
- [SDK .NET 6.0+](https://dotnet.microsoft.com/download)
- [SQL Server 2019+](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

**Para cambiar la conexion realizarla en los archivos ProductService/appsettings.json y TransactionService/appsettings.json**

**Paquetes necesarios (ejecutar en ambos microservicios):**
```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Swashbuckle.AspNetCore
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
```
## 🖥 Frontend Angular

### 📋 Requisitos Técnicos

**Dependencias principales:**
- Angular 15+
- Angular Material 15+
- TypeScript 4.8+
- RxJS 7.5+
- Node.js 16.x+

### 🛠 Instalación

1. **Instalar Angular CLI globalmente:**
   ```bash
   npm install -g @angular/cli@latest
    npm install
    ng add @angular/material@latest
    npm install @angular/flex-layout@latest ngx-pagination ngx-toastr```

