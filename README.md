# sys-net-audit

[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Platform](https://img.shields.io/badge/Platform-Linux%20%7C%20Windows-lightgrey?logo=linux)](https://ubuntu.com)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

Herramienta de consola en **C# (.NET 8)** desarrollada para el diagnóstico rápido de conectividad de red, inspección de interfaces locales y verificación de servicios esenciales (ICMP y DNS) en entornos de soporte técnico IT, Helpdesk Nivel 1/2 y administración de sistemas.

---

## Propósito del proyecto

En entornos corporativos de soporte técnico, el triaje rápido de problemas de red antes de escalar una incidencia es crítico. **`sys-net-audit`** automatiza la recolección de parámetros de red locales y ejecuta pruebas de disponibilidad sobre servicios clave en un único comando ejecutable desde la terminal, permitiendo guardar un registro estructurado en JSON para adjuntar a tickets de soporte.

---

## Funcionalidades principales

* **Inspección de adaptadores locales**: Detección automática de interfaces de red físicas y virtuales activas (excluyendo la interfaz de bucle local *loopback*).
* **Extracción de parámetros de red**: Lectura directa de direcciones IPv4 asociadas, direcciones MAC físicas y estado operativo de las interfaces (`Up` / `Down`).
* **Pruebas de conectividad ICMP**: Comprobación automática de conectividad hacia puerta de enlace o DNS público mediante solicitudes Ping con control de tiempo de espera.
* **Verificación de resolución DNS**: Comprobación del estado del resolutor DNS local del sistema operativo.
* **Exportación de informes en JSON**: Opción mediante la bandera `--json` para generar un archivo con marca de tiempo (*timestamp*) listo para integrarse con herramientas de monitoreo o sistemas de ticketing.
* **Soporte multiplataforma**: Compatible de forma nativa con distribuciones Linux (Ubuntu/Debian) y Microsoft Windows.

---

## Estructura del proyecto

El proyecto implementa la separación de responsabilidades (SRP) dividiendo la capa de modelo de datos, la capa de servicios de red y la interfaz de usuario por consola:

```text
sys-net-audit/
├── .gitignore
├── README.md
└── src/
    ├── SysNetAudit.csproj
    ├── Program.cs
    ├── Models/
    │   └── NetworkReport.cs
    └── Services/
        └── NetworkAuditService.cs
