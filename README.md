# CentroEventos

CentroEventos es una solución desarrollada en .NET que permite gestionar eventos deportivos, personas y reservas. La solución está estructurada en varios proyectos para mantener una separación clara de responsabilidades y facilitar su mantenimiento y escalabilidad.

## Estructura del Proyecto

La solución está compuesta por los siguientes proyectos:

### 1. CentroEventos.Aplicacion

Este proyecto contiene:

- **Entidades**: Representan los modelos principales del dominio, como `Persona`, `EventoDeportivo` y `Reserva`.
- **Excepciones**: Define excepciones personalizadas como `EntidadNotFoundException`, `CupoExcedidoException`, entre otras, para manejar errores específicos del dominio.
- **Interfaces**: Define contratos para repositorios y fuentes de datos, como `IFuenteDeDatos` y `IRepositorioPersona`.
- **Casos de Uso**: Implementa la lógica de negocio, como el caso de uso `ReservaAltaUseCase` para gestionar reservas.

### 2. CentroEventos.Repositorios

Este proyecto implementa:

- **Fuentes de Datos**: Clases como `FuenteDeDatosCsv` y `FuenteDeDatosAtributoPorLinea` para manejar la persistencia de datos en diferentes formatos.
- **Repositorios**: Implementaciones de repositorios como `RepositorioPersona`, `RepositorioEventoDeportivo` y `RepositorioReserva` que interactúan con las fuentes de datos.

### 3. CentroEventos.Consola

Este proyecto es una aplicación de consola que actúa como punto de entrada para interactuar con la solución. Permite:

- Crear datos iniciales (personas, eventos, reservas).
- Ejecutar casos de uso como la creación de reservas.
- Mostrar información almacenada.

## Requisitos Previos

- **.NET SDK**: Asegúrate de tener instalado el SDK de .NET 8.0 o superior.
- **Sistema Operativo**: Compatible con Linux, Windows y macOS.

## Configuración y Ejecución

Sigue estos pasos para ejecutar la solución:

### 1. Clonar el Repositorio

Clona el repositorio en tu máquina local:

```bash
git clone https://github.com/JandroMejia97/centro-eventos-net
cd CentroEventos
```

### 2. Restaurar Paquetes NuGet

Restaura las dependencias necesarias:

```bash
dotnet restore
```

### 3. Compilar la Solución

Compila la solución para asegurarte de que no hay errores:

```bash
dotnet build
```

### 4. Ejecutar la Aplicación

Ejecuta el proyecto de consola:

```bash
dotnet run --project CentroEventos.Consola
```

### 5. Probar la Aplicación

La aplicación de consola:

- Crea datos iniciales (una persona y un evento deportivo).
- Intenta realizar una reserva para la persona en el evento.
- Muestra las reservas creadas o errores si ocurren.

## Estructura de Archivos

- **CentroEventos.Aplicacion**: Contiene la lógica de negocio y las definiciones del dominio.
- **CentroEventos.Repositorios**: Implementa la persistencia de datos.
- **CentroEventos.Consola**: Proporciona una interfaz de usuario basada en consola.

## Notas Adicionales

- Los datos se almacenan en archivos CSV en el directorio raíz del proyecto.
- Los IDs únicos para las entidades se generan automáticamente y se almacenan en archivos separados (`ids_persona.txt`, `ids_evento.txt`, `ids_reserva.txt`).
- La solución utiliza excepciones personalizadas para manejar errores específicos del dominio.

## Contribuciones

Si deseas contribuir a este proyecto, por favor crea un fork del repositorio, realiza tus cambios y envía un pull request.

## Licencia

Este proyecto está bajo la Licencia MIT. Consulta el archivo `LICENSE` para más detalles.