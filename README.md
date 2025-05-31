# Proyecto-SeaAngel

🚢 Gestión de Cruceros Proyecto desarrollado para el curso ISW-711 Programación en Ambiente Web II, enfocado en la creación de una aplicación web completa para la reserva y gestión de cruceros turísticos, incluyendo itinerarios, barcos, habitaciones, usuarios y pagos.

🛠 Tecnologías Utilizadas ASP.NET Core MVC (.NET 8) C# SQL Server Entity Framework Core AJAX (para búsquedas dinámicas y recarga de datos) JavaScript, jQuery y Bootstrap API Externa (consumo para países, verificación de tarjetas o tipo de cambio)

🧭 Tipo de Negocio La aplicación simula una agencia de cruceros turísticos que ofrece viajes hacia tres destinos internacionales (países o regiones), cada uno con puertos específicos de salida y llegada. La interfaz está orientada al usuario y diseñada con una estructura clara y atractiva relacionada con la temática de viajes en crucero.

📄 Funcionalidades Principales Página Inicial Accesible sin iniciar sesión. Introducción al negocio de cruceros. Lista de cruceros disponibles (según fecha actual).

🔍 Buscador de Cruceros Permite filtrar por: Destino Puerto de salida Fechas (mes/año; no menores al actual) Ordenar resultados por precio o por fecha más cercana.

👤 Opciones de Usuario Registro como nuevo usuario Inicio de sesión con credenciales

🔐 Gestión de Usuarios Roles: Cliente y Administrador (único) Datos: nombre, teléfono, correo, fecha de nacimiento, país, contraseña. Registro de usuario automático con rol de Cliente.

🌍 Gestión de Destinos y Puertos Precargados 3 destinos turísticos. Puertos asociados a cada destino (ej. Barcelona, España).

🛏️ Gestión de Habitaciones Datos por habitación: nombre, descripción, cantidad de huéspedes, tamaño. Funciones de registro y edición disponibles para el administrador.

🚢 Gestión de Barcos Datos del barco: nombre, descripción, capacidad total. Asociación de habitaciones por barco, incluyendo cantidad disponible.

🧭 Gestión de Cruceros Datos: nombre, imagen, duración, barco, itinerario, fechas y precios. Listado disponible para todos los usuarios.

📅 Itinerario del Crucero Asignación de puertos por día. Mínimo 2 puertos por crucero. Información detallada por día (salida/llegada).

🗓 Fechas y Precios Múltiples fechas por crucero. Fecha de inicio y fecha límite de pago. Precios de habitaciones por tipo (precio por habitación, no por huésped).

➕ Complementos del Crucero Servicios adicionales como propinas, tours, etc. Precio aplicable por camarote o por huésped. Funciones de registro y edición para el administrador.

📝 Gestión de Reserva de Cruceros Selección de crucero, tipo de habitación y cantidad de huéspedes. Ingreso de datos de cada huésped. Selección de complementos opcionales. Visualización de un resumen completo con: Nombre del crucero Puertos de salida y regreso Fechas Camarotes, huéspedes y precios detallados Subtotales, impuestos y precio total

💳 Pago de Reservas Opciones: Pago total Depósito mínimo por huésped (ej. $250) Datos requeridos para tarjeta de crédito: Número, fecha de expiración, CVV, titular Envío de resumen de reserva por correo electrónico tras el pago

📂 Historial de Reservas Clientes: Ver historial completo Completar pagos pendientes Ver detalles de cada reserva

Administrador: Ver todas las reservas Filtro por crucero y fecha
