using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.SharedLibrary.Logs
{
    public static class LogException
    {
        public static void LogExceptions(Exception ex)
        {
            LogToFile(ex.Message);
            LogToCnsole(ex.Message);
            LogToDebugger(ex.Message);
        }

        private static void LogToFile(string message)
        {
            Log.Information(message);
        }
        private static void LogToDebugger(string message)
        {
            Log.Debug(message);
        }

        private static void LogToCnsole(string message)
        {
            Log.Warning(message);
        }
    }
}
