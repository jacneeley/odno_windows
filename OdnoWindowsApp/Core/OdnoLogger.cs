using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdnoWindowsApp.Core
{
    internal class OdnoLogger
    {
        private class OdnoLoggerSrv {
            private static readonly ILoggerFactory? _instance;

            static OdnoLoggerSrv() {
                _instance = LoggerFactory.Create(config => {
                    config.AddConsole();
                    config.SetMinimumLevel(LogLevel.Warning);
                });
            }

            private OdnoLoggerSrv() { }

            internal static OdnoLoggerSrv Instance { get; } = new OdnoLoggerSrv();

            internal ILoggerFactory OdnoLoggerInstance => _instance;
        }

        public static ILogger BuildOdnoLogger(string category) {
            return OdnoLoggerSrv.Instance.OdnoLoggerInstance.CreateLogger(category);
        }
    }
}
