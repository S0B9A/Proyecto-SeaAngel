# Gestión de Cruceros

> 🔀 El código fuente se encuentra en la rama `sebas`.

---


## Tabla de Contenido

1. [Gestión de Reserva de Cruceros](#gestión-de-reserva-de-cruceros)  
2. [Requerimientos Técnicos](#requerimientos-técnicos)  
3. [Tipo de Negocio](#tipo-de-negocio)  
4. [Buscador de Cruceros](#buscador-de-cruceros)  
5. [Opciones de Usuario](#opciones-de-usuario)  
6. [Gestión de Usuarios](#gestión-de-usuarios)  
7. [Gestión de Destinos y Puertos](#gestión-de-destinos-y-puertos)  
8. [Gestión de Habitaciones](#gestión-de-habitaciones)  
9. [Gestión de Barcos](#gestión-de-barcos)  
10. [Gestión de Cruceros](#gestión-de-cruceros)  
11. [Itinerario del Crucero](#itinerario-del-crucero)  
12. [Fechas y Precios de Habitaciones del Crucero](#fechas-y-precios-de-habitaciones-del-crucero)  
13. [Complementos del Crucero](#complementos-del-crucero)  
14. [Gestión de Reserva de Crucero](#gestión-de-reserva-de-crucero)  
15. [Resumen de la Reserva](#resumen-de-la-reserva)  
16. [Pagar Reserva](#pagar-reserva)  
17. [Historial de Reservas](#historial-de-reservas)  

---

## Gestión de Reserva de Cruceros

Se solicita una aplicación web para gestionar reservas de cruceros. Cada equipo debe analizar, diseñar y proponer su solución.

---

## Requerimientos Técnicos

- ASP.NET Core MVC (.NET 8)
- Base de datos: SQL Server
- Arquitectura basada en buenas prácticas (vista en clase)
- Uso de API externa para:
  - Listar países
  - Verificar tipo de tarjeta de crédito
  - Conversión de tipo de cambio (colones/dólares)
- Uso de AJAX para búsquedas, cálculos y recarga de datos
- Gestión del proyecto con GitLab (con historial de versiones por estudiante)
- Comprensión y justificación completa del desarrollo por parte del equipo
- Proyecto 100% original, sin plagio ni copia (ni uso extensivo de IA)

---

## Tipo de Negocio

- Aplicación enfocada en la gestión de cruceros turísticos.
- Deben escogerse 3 destinos: países o regiones.
- Toda la información debe estar relacionada con los destinos seleccionados.
- Diseño debe ser temático, lógico y amigable.

---

## Buscador de Cruceros

Filtros disponibles:

- Destino
- Puerto de salida
- Fecha (mes y año, no menor al actual)

Ordenar resultados por:

- Precio (ascendente/descendente)
- Fecha más cercana

---

## Opciones de Usuario

- **Inscribirse**: registrarse como nuevo usuario
- **Ingresar**: autenticarse con credenciales

---

## Gestión de Usuarios

Roles:

- Cliente
- Administrador (solo uno)

Información mínima del usuario:

- Nombre
- Teléfono
- Correo electrónico
- Fecha de nacimiento
- País
- Contraseña

---

## Gestión de Destinos y Puertos

- 3 destinos precargados
- Puertos asociados a cada destino
- Ejemplo: Barcelona, España

---

## Gestión de Habitaciones

Cada habitación debe incluir:

- Nombre
- Descripción
- Capacidad mínima y máxima
- Tamaño aproximado

Administrador puede: registrar/modificar habitaciones

---

## Gestión de Barcos

Datos del barco:

- Nombre
- Descripción
- Capacidad de huéspedes
- Habitaciones asociadas (con cantidad disponible)

---

## Gestión de Cruceros

Información obligatoria:

- Nombre
- Imagen representativa
- Cantidad de días
- Barco asignado
- Itinerario
- Fechas y precios de habitaciones

---

## Itinerario del Crucero

Por cada día se debe registrar:

- Puerto
- Descripción (horarios de llegada/salida)

Mínimo 2 puertos por crucero.

---

## Fechas y Precios de Habitaciones del Crucero

- Múltiples fechas de salida por crucero
- Fecha de inicio
- Fecha límite para pagos
- Precios por tipo de habitación (por habitación, no por huésped)

---

## Complementos del Crucero

Información requerida:

- Nombre
- Descripción
- Precio
- Forma de aplicación (por camarote o por huésped)

Administrador puede: registrar/modificar complementos

---

## Gestión de Reserva de Crucero

Registrar:

- Cantidad de habitaciones/camarotes
- Cantidad de huéspedes por habitación
- Tipo de habitación
- Datos de cada huésped (mínimo 5 campos)
- Complementos seleccionados (reflejados en resumen)

---

## Resumen de la Reserva

Debe incluir:

- Nombre del crucero
- Puerto de salida y regreso (según itinerario)
- Fechas (inicio y fin calculada)
- Camarotes y cantidad de huéspedes
- Total por camarotes
- Complementos agregados
- Subtotal
- Impuestos
- Precio total

---

## Pagar Reserva

Condiciones:

- Solo usuarios autenticados
- El usuario queda registrado en la reserva

Opciones de pago:

1. Pago total
2. Pago parcial por depósito ($250 por huésped)
   - Mostrar fecha límite para completar pago

Solicitar datos de tarjeta de crédito:

- Número (16 dígitos)
- Fecha de vencimiento
- CVV
- Titular

Enviar correo con resumen al cliente después del pago

---

## Historial de Reservas

**Cliente puede:**

- Ver todas sus reservas
- Pagar montos pendientes
- Ver detalle completo de cada reserva

**Administrador puede:**

- Ver todas las reservas
- Filtrar por crucero y fecha

---

