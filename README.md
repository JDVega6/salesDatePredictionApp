# Arquitectura

## Tecnologías Utilizadas 💻

- **Backend**: .NET Framework 4.8
- **Frontend**: Angular 17
- **Base de datos**: SQL Server
- **Gráficos**: D3.js (Vanilla JS)
- **Procedimiento Almacenado**: SQL Server

## Estructura del proyecto ⚖️

- **Backend**:
  - API RESTful creada con .NET Framework 4.8.
  - Endpoints para la gestión de clientes, órdenes, productos, empleados y transportistas.
  - Conexión a base de datos SQL Server para la persistencia de datos.
  - Procedimiento Almacenado para la creación de nuevas órdenes.
  
- **Frontend**:
  - Aplicación construida con Angular 17.
  - Consumo de la API RESTful para manejar la información de la aplicación.

- **Base de Datos**:
  - Script SQL para levantar la base de datos.
  - Procedimiento almacenado `AddNewOrder` para la creación de nuevas órdenes.
  
- **Mini Challenge**:
  - Creación de un gráfico utilizando D3.js con vanilla JavaScript.

## Cómo Ejecutar el Proyecto ⚙️

1. **Levantar la base de datos**:
   - Ejecuta los scripts SQL proporcionados en el repositorio para crear la base de datos y el procedimiento almacenado.
   - Asegúrate de tener SQL Server corriendo y configurar las cadenas de conexión necesarias.

2. **Backend (API .NET Framework 4.8)**:
   - Abre el proyecto `Backend/` en Visual Studio.
   - Ejecuta la aplicación desde Visual Studio.

3. **Frontend (Angular 17)**:
   - Dirígete al directorio `Frontend/` y ejecuta los siguientes comandos para instalar las dependencias y levantar el servidor:

     ```bash
     npm install
     ng serve
     ```

4. **Acceder a la aplicación**:
   - Accede a la aplicación desde tu navegador en `http://localhost:4200`.

## Video

