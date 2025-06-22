# CentroEventos

CentroEventos es una solución desarrollada en .NET que permite gestionar eventos deportivos, personas y reservas. La solución está estructurada bajo los principios de Clean Architecture, separando claramente las responsabilidades en capas de Aplicación, Repositorios y UI, lo que facilita el mantenimiento, la escalabilidad y la extensibilidad.

---

## Arquitectura y Estructura del Proyecto

La solución está compuesta por los siguientes proyectos principales:

### 1. CentroEventos.Aplicacion

- **Entidades**: Modelos de dominio (`Persona`, `EventoDeportivo`, `Reserva`, `Usuario`, `PermisoUsuario`).
- **Excepciones**: Manejo de errores específicos del dominio (`EntidadNotFoundException`, `CupoExcedidoException`, etc.).
- **Interfaces**: Contratos para repositorios y fuentes de datos (por ejemplo, `IRepositorioPersona`, `IFuenteDeDatosEventoDeportivo`).
- **Casos de Uso**: Lógica de negocio agrupada en clases de casos de uso (por ejemplo, `EventoDeportivoUseCases`, `ReservaUseCases`).
- **Servicios y Validadores**: Servicios de autorización, hashing de contraseñas y validadores de entidades.

### 2. CentroEventos.Repositorios

- **Contextos**: Configuración de EF Core y DbContext (`CentroEventosDbContext`).
- **Fuentes de Datos**: Implementaciones para persistencia con Entity Framework y SQLite (`FuenteDeDatosEventoDeportivoEF`, etc.).
- **Repositorios**: Implementaciones concretas de los repositorios del dominio.

### 3. CentroEventos.UI

- **Frontend Blazor**: Interfaz de usuario desarrollada con Blazor Server, con migración progresiva a Blazor WASM.
- **Componentes**: Formularios reutilizables, layouts, páginas de entidad, badges de estado, diálogos de confirmación y notificaciones.
- **Autenticación y Autorización**: Implementación personalizada basada en SessionStorage, Claims y control de permisos por roles.
- **Configuración**: Archivos de configuración (`appsettings.json`, `appsettings.Development.json`).

---

## Tecnologías y Herramientas

- **.NET 8.0+**
- **Blazor Server/WASM**
- **Entity Framework Core** (con SQLite)
- **Cookie y SessionStorage** para autenticación
- **DTOs y validaciones**
- **Diseño responsive**

---

## Patrones y Buenas Prácticas

- **Clean Architecture**: Separación estricta de capas y dependencias.
- **Repository y Data Source**: Abstracción de acceso a datos y persistencia.
- **Use Case**: Lógica de negocio encapsulada en casos de uso.
- **DTOs**: Separación entre entidades de dominio y datos expuestos a la UI.
- **Validadores**: Validación centralizada y reutilizable.
- **UI Unificada**: Formularios y layouts reutilizables, badges de estado, manejo de errores y notificaciones.

---

## Seguridad

- **Hashing de contraseñas**: Implementación de hashing seguro en el registro y autenticación de usuarios.
- **Autenticación y Autorización**: Uso de Claims, SessionStorage y control de permisos por roles.
- **Control de sesión**: Expiración y validación de sesión en frontend.
- **Manejo de errores**: Excepciones personalizadas y feedback visual en la UI.

---

## Base de Datos y Persistencia

- **EF Core + SQLite**: Persistencia relacional, migraciones y configuración de claves compuestas.
- **Migraciones**: Soporte para actualización incremental del esquema.
- **Relaciones**: Navegación y restricciones entre entidades (por ejemplo, reservas asociadas a personas y eventos).

---

## Interfaz de Usuario (UI)

- **Blazor Components**: Formularios unificados, layouts reutilizables (`ListLayout`), páginas de alta/edición/listado.
- **Responsive Design**: Adaptación a dispositivos móviles y escritorio.
- **Badges de estado**: Visualización de estados y permisos.
- **Validaciones**: Validación en frontend y backend, mensajes de error claros.
- **Notificaciones y diálogos**: Componentes de Snackbar y ConfirmDialog para feedback y confirmaciones.

---

## Diferencias y Mejoras respecto a la Documentación Anterior

- Se reemplazó la aplicación de consola por una UI web moderna basada en Blazor.
- Se migró la persistencia a EF Core con SQLite, abandonando el uso de archivos CSV.
- Se implementó autenticación y autorización robusta basada en Claims y SessionStorage.
- Se mejoró la experiencia de usuario con formularios unificados, validaciones y diseño responsive.
- Se agregaron servicios de hashing de contraseñas y control de sesión.
- Se centralizó la lógica de negocio en casos de uso y validadores reutilizables.

---

## Ejecución y Desarrollo

1. **Clonar el repositorio**

   ```bash
   git clone https://github.com/JandroMejia97/centro-eventos-net
   cd CentroEventos
   ```

2. **Restaurar dependencias**

   ```bash
   dotnet restore
   ```

3. **Compilar la solución**

   ```bash
   dotnet build
   ```

4. **Ejecutar la UI**

   ```bash
   dotnet run --project CentroEventos.UI
   ```

---

## Contribuciones y Licencia

Las contribuciones son bienvenidas. Por favor, crea un fork, realiza tus cambios y envía un pull request.

Este proyecto está bajo la Licencia MIT.