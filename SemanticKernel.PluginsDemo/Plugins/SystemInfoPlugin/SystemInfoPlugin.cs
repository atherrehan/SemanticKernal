using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Diagnostics;
using System.Management;
using System.Text;

namespace SemanticKernel.PluginsDemo.Plugins.SystemInfoPlugin
{
    public class SystemInfoPlugin
    {
        [KernelFunction("get_memory_ram")]// Preferred to use snake case
        [Description("Gets the system memory / RAM usage information.")]
        public string GetSystemMemoryUsage()
        {
            string result = string.Empty;
            //Query the win32_OperatingSystem class to get system memory usage
            using (var searcher = new ManagementObjectSearcher("SELECT TotalVisibleMemorySize, FreePhysicalMemory FROM Win32_OperatingSystem"))
            {
                foreach (var obj in searcher.Get())
                {
                    var totalMemory = Convert.ToInt64(obj["TotalVisibleMemorySize"]);
                    var freeMemory = Convert.ToInt64(obj["FreePhysicalMemory"]);
                    var usedMemory = totalMemory - freeMemory;
                    result = $"Total Memory: {totalMemory / 1024} MB, Used Memory: {usedMemory / 1024} MB, Free Memory: {freeMemory / 1024} MB";
                }
            }
            return result;
        }
        [KernelFunction("get_top_memory_processes")]// Preferred to use snake case
        [Description("Gets the top memory / RAM consuming processes.")]
        public string GetTopMemoryConsumingPorcesses([Description("Number of processes to evaluate")]int processes)
        {

            // 1. Obtain the total physical memory (in bytes) via WMI (Win32_OperatingSystem).
            double totalMemoryBytes = GetTotalPhysicalMemory();

            // 2. Get the list of all system processes.
            var allProcesses = Process.GetProcesses();

            // 3. Sort the processes by descending memory usage and take the top 10.
            var topProcesses = allProcesses
                .OrderByDescending(p => p.WorkingSet64)
                .Take(processes);

            // 4. Build the result string.
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== Top processes by memory usage ===");

            int rank = 1;
            foreach (var proc in topProcesses)
            {
                long memBytes = 0;
                double percentage = 0.0;

                try
                {
                    // Could throw an exception if we don't have access to certain processes (AccessDenied).
                    memBytes = proc.WorkingSet64;
                    percentage = memBytes / totalMemoryBytes * 100.0;
                }
                catch (Exception ex)
                {
                    // We ignore or handle the exception as appropriate.
                    // For example, if we can't access a process,
                    // we move on to the next one.
                    sb.AppendLine($"{rank}. [Access Denied] - {ex.Message}");
                    rank++;
                    continue;
                }

                double memMB = memBytes / (1024.0 * 1024.0);

                // Process name (can be null in some cases).
                string processName = string.IsNullOrEmpty(proc.ProcessName)
                    ? "Unknown"
                    : proc.ProcessName;

                sb.AppendLine($"{rank}. {processName} - {memMB:0.00} MB ({percentage:0.00}%)");
                rank++;
            }

            return sb.ToString();

        }
        /// <summary>
        /// Gets the system's total physical memory in bytes using WMI.
        /// </summary>
        private double GetTotalPhysicalMemory()
        {
            double totalMemoryBytes = 0;

            var searcher = new ManagementObjectSearcher(
                "SELECT TotalVisibleMemorySize FROM Win32_OperatingSystem"
            );

            // Retrieve the first (and only) record.
            var obj = searcher.Get().Cast<ManagementObject>().FirstOrDefault();
            if (obj != null)
            {
                // TotalVisibleMemorySize is in KB, convert to bytes.
                ulong totalMemKB = (ulong)obj["TotalVisibleMemorySize"];
                totalMemoryBytes = totalMemKB * 1024.0;
            }

            return totalMemoryBytes;
        }
    }
}
