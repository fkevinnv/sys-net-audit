using System;
using System.IO;
using SysNetAudit.Services;

namespace SysNetAudit
{
    /// <summary>
    /// Punto de entrada principal de la aplicación de consola SysNetAudit.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Método de entrada que gestiona el flujo de auditoría y la visualización/exportación de resultados.
        /// </summary>
        /// <param name="args">Argumentos de la línea de comandos (ej: '--json' para generar informe en disco).</param>
        private static void Main(string[] args)
        {
            Console.WriteLine("[INFO] Iniciando auditoria de red local...");

            var auditService = new NetworkAuditService();
            var report = auditService.GenerateReport();

            // Mostrar información general del equipo
            Console.WriteLine($"Equipo: {report.HostName}");
            Console.WriteLine($"Adaptadores detectados: {report.Adapters.Count}");
            Console.WriteLine("------------------------------------------------");

            // Listar cada interfaz de red activa y sus direcciones asignadas
            foreach (var adapter in report.Adapters)
            {
                Console.WriteLine($"Interfaz: {adapter.Name} ({adapter.Status})");
                Console.WriteLine($"  MAC: {adapter.MacAddress}");
                Console.WriteLine($"  IPs: {string.Join(", ", adapter.IpAddresses)}");
            }

            // Listar los resultados de las comprobaciones ICMP y DNS
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Pruebas de conectividad:");
            foreach (var check in report.ConnectivityChecks)
            {
                string result = check.Value ? "OK" : "FAIL";
                Console.WriteLine($"  {check.Key}: {result}");
            }

            // Exportar a archivo JSON si el usuario pasa la bandera '--json'
            if (args.Length > 0 && args[0] == "--json")
            {
                string jsonOutput = auditService.ExportToJson(report);
                string fileName = $"audit_report_{DateTime.Now:yyyyMMdd_HHmmss}.json";
                File.WriteAllText(fileName, jsonOutput);
                Console.WriteLine($"\n[INFO] Informe exportado a: {fileName}");
            }
        }
    }
}