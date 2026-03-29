using System;
using log4net;

namespace eCommerce.SharedLibrary
{
    public static class LogException
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(LogException));

        public static void LogExceptions(Exception ex)
        {
            if (ex == null) return;
            Logger.Error("Unhandled exception", ex);
#if DEBUG
            Console.Error.WriteLine(ex);
#endif
        }
    }
}
