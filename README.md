# Sistema de Gestión de Inventario

Aplicación de consola desarrollada en C# para administrar el stock de productos: altas, ingresos, egresos, bajas y alertas de reposición o sobrestock.

Es un proyecto personal de aprendizaje, construido por etapas, con foco en la programación orientada a objetos, la arquitectura limpia y las buenas prácticas de desarrollo.

## Funcionalidades

- **Alta de productos** con ID, nombre, categoría y límites de stock mínimo y máximo.
- **Consulta** de un producto por su ID y **listado** de todo el inventario.
- **Registro de ingresos y egresos** de stock.
- **Baja de productos**, solo cuando no tienen stock disponible.
- **Alertas de stock**: productos en o bajo el mínimo (reposición) y productos que superan el máximo (sobrestock).
- **Normalización de datos**: IDs, nombres y categorías se guardan en mayúsculas y sin espacios en los extremos, por lo que `per-001` y `PER-001` se consideran el mismo producto.

## Reglas de negocio

El sistema garantiza que ningún producto quede en un estado inválido:

- El ID y el nombre son obligatorios, y no puede haber dos productos con el mismo ID.
- El stock mínimo no puede ser negativo, y el máximo debe ser mayor que el mínimo.
- Todo producto se crea con cantidad 0. El stock solo cambia mediante ingresos y egresos, nunca editando la cantidad directamente.
- Las cantidades de ingreso y egreso deben ser positivas, y no se puede egresar más de lo disponible.
- No se puede eliminar un producto con stock, para no perder el registro de mercadería que existe físicamente.

## Tecnologías

- C# / .NET 10
- Git y GitHub (flujo con ramas por funcionalidad y Pull Requests)

## Arquitectura

El proyecto sigue los principios de la **arquitectura limpia**, separando el código en capas con responsabilidades distintas:

```
GestionInventario/
├── Dominio/
│   ├── Producto.cs        # Entidad con sus reglas de negocio
│   └── Inventario.cs      # Administra la colección de productos
├── Presentacion/
│   └── MenuConsola.cs     # Interacción con el usuario
└── Program.cs             # Punto de entrada
```

- **Dominio**: contiene las entidades y todas las reglas de negocio. No depende de ninguna otra capa: no sabe si lo usa una consola, una API o una web.
- **Presentación**: muestra la información y recibe los datos del usuario. No valida reglas de negocio; las delega en el dominio.

### Decisiones de diseño

- **Encapsulamiento**: la cantidad de un producto solo puede modificarse desde la propia clase, y el inventario expone sus productos como una colección de solo lectura. Así, ninguna regla puede saltearse desde afuera.
- **Excepciones para las reglas de negocio**: el dominio lanza excepciones cuando una operación no es válida, y la capa de presentación las atrapa en un único punto para mostrarlas al usuario. El dominio detecta los errores; la presentación decide cómo comunicarlos.
- **Inyección de dependencias**: el menú recibe el inventario en su constructor en lugar de crearlo, lo que permitirá cambiar más adelante la forma de almacenar los datos sin modificar la presentación.

## Cómo ejecutarlo

Requisitos: [SDK de .NET 10](https://dotnet.microsoft.com/download).

```bash
git clone https://github.com/franzenn/GestionInventario.git
cd GestionInventario
dotnet run
```

> Por ahora, los datos se guardan en memoria y se pierden al cerrar el programa. La persistencia es la próxima etapa del proyecto.

## Roadmap

- [x] **Etapa 1**: dominio y menú de consola
- [ ] Pruebas unitarias automatizadas
- [ ] **Etapa 2**: persistencia de datos (archivo y SQLite), separando el proyecto en capas independientes
- [ ] **Etapa 3**: API web con ASP.NET Core
- [ ] **Etapa 4**: frontend con HTML, CSS y JavaScript
- [ ] Soporte para productos serializados (seguimiento individual por número de serie)