# sys-net-audit

Herramienta de consola desarrollada en C# (.NET 8) orientada al diagnóstico rápido de interfaces de red local, comprobación de conectividad e inspección de servicios esenciales (ICMP y DNS) en entornos de soporte técnico e infraestructura IT.

## Características principales

* **Auditoría de adaptadores de red**: Identificación de interfaces físicas y virtuales activas, omitiendo de forma automática la interfaz de bucle local (loopback).
* **Extracción de parámetros técnicos**: Lectura de direcciones IPv4 asignadas, direcciones MAC físicas y estado operativo (`Up`/`Down`).
* **Pruebas de conectividad**: Diagnóstico automático del estado de red mediante comprobación de latencia ICMP (Ping) y verificación de resolución de nombres mediante servidores DNS.
* **Exportación de informes**: Capacidad de generar informes estructurados en formato JSON con marcas de tiempo (timestamp) para auditorías offline o adjuntos a tickets de soporte.
* **Diseño multiplataforma**: Compatible con entornos Linux (Ubuntu), Windows y macOS gracias a la utilización de la biblioteca estándar de .NET 8.

## Arquitectura del proyecto

El código está estructurado siguiendo el principio de responsabilidad única (SRP) y separación de capas:

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