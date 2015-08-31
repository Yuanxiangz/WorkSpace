using System;
using System.Diagnostics;
using Microsoft.Owin.Hosting;

namespace WebApiSample
{
    internal class Program
    {
        private const int Port = 900;
        private static readonly string Address = "http://*:" + Port;
        private static Process BrowserProcess { get; set; }

        private static void Main( string[] args )
        {
            Console.WriteLine( "Initializing..." );

            try
            {
                using( IDisposable webApp = WebApp.Start( Address ), browser = LaunchDocumentation() )
                {
                    Console.WriteLine( "Service started at {0}", Address );
                    Console.WriteLine( "Press any key to stop" );
                    Console.ReadLine();
                }
            }
            catch( Exception ex )
            {
                Console.WriteLine( "Critical Error:" );
                Console.WriteLine( ex.InnerException.Message );
            }
        }

        /// <summary>
        ///     Open a browser and navigate to the swagger API documentation URL
        /// </summary>
        private static Process LaunchDocumentation()
        {
            var docUrl = Address.Replace( "*", "localhost" ) + "/" + Startup.ApiPath;
            return Process.Start( "chrome.exe", string.Format( "--incognito {0}", docUrl ) );
        }
    }
}
