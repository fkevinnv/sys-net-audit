using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.Json;
using SysNetAudit.Models;

namespace SysNetAudit.Services
{
    /// <summary>
    /// Servicio encargado de inspeccionar las interfaces de red del sistema y realizar pruebas de conectividad.
    /// </summary>
    public class NetworkAuditService
    {
        /// <summary>
        /// Recopila la información de los adaptadores de red activos y ejecuta pruebas básicas de conectividad.
        /// </summary>
        /// <returns>Objeto <see cref="NetworkReport"/> con el informe estructurado.</returns>
        public NetworkReport GenerateReport()
        {
            var report = new NetworkReport
            {
                HostName = Dns.GetHostName()
            };

            foreach (var adapter in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Omitir la interfaz de bucle local (loopback)
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                    continue;

                var adapterInfo = new AdapterInfo
                {
                    Name = adapter.Name,
                    Description = adapter.Description,
                    Status = adapter.OperationalStatus.ToString(),
                    MacAddress = adapter.GetPhysicalAddress().ToString()
                };

                var ipProperties = adapter.GetIPProperties();
                foreach (var unicast in ipProperties.UnicastAddresses)
                {
                    // Filtrar únicamente direcciones IPv4
                    if (unicast.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        adapterInfo.IpAddresses.Add(unicast.Address.ToString());
                    }
                }

                report.Adapters.Add(adapterInfo);
            }

            // Realizar pruebas de conectividad externa
            report.ConnectivityChecks["Gateway_Ping"] = CheckPing("1.1.1.1");
            report.ConnectivityChecks["DNS_Resolution"] = CheckDnsResolution("google.com");

            return report;
        }

        /// <summary>
        /// Comprueba la conectividad mediante una solicitud ICMP Ping con tiempo de espera.
        /// </summary>
        private bool CheckPing(string host)
        {
            try
            {
                using var pinger = new Ping();
                var reply = pinger.Send(host, 2000);
                return reply.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica la capacidad del sistema para resolver nombres de dominio mediante DNS.
        /// </summary>
        private bool CheckDnsResolution(string domain)
        {
            try
            {
                var entries = Dns.GetHostAddresses(domain);
                return entries.Length > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Serializa el informe de auditoría a formato JSON con sangría formateada.
        /// </summary>
        public string ExportToJson(NetworkReport report)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(report, options);
        }
    }
}