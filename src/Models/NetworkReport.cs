using System;
using System.Collections.Generic;

namespace SysNetAudit.Models
{
    /// <summary>
    /// Almacena los detalles técnicos y el estado de un adaptador de red específico.
    /// </summary>
    public class AdapterInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string MacAddress { get; set; } = string.Empty;

        /// <summary>
        /// Lista de direcciones IP (IPv4) asociadas al adaptador.
        /// </summary>
        public List<string> IpAddresses { get; set; } = new();
    }

    /// <summary>
    /// Representa el informe global de auditoría de red, incluyendo interfaces y pruebas de conectividad.
    /// </summary>
    public class NetworkReport
    {
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string HostName { get; set; } = string.Empty;
        public List<AdapterInfo> Adapters { get; set; } = new();

        /// <summary>
        /// Estado de los chequeos de conectividad externos (ej: Gateway Ping, Resolución DNS).
        /// </summary>
        public Dictionary<string, bool> ConnectivityChecks { get; set; } = new();
    }
}