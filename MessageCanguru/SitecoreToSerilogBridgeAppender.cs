using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using log4net.Appender;
using log4net.spi;
using Serilog;
using Serilog.Formatting.Json;
using SerilogWeb.Classic.Enrichers;

namespace MessageCanguru
{
    public class SitecoreToSerilogBridgeAppender : AppenderSkeleton
    {

        private string ReadRequestUrl()
        {
            try
            {
                var path = HttpContext.Current?.Request.Url.AbsoluteUri;
                return path ?? "";
            }
            catch (Exception e)
            {
                return "N/A";
            }
        }

        

        protected override void Append(LoggingEvent loggingEvent)
        {
            var url = ReadRequestUrl();
            
            // Get calling method name
            //string callersFulleName = string.IsNullOrWhiteSpace(loggingEvent.LoggerName) ? "Unable_to_resolve_caller" : loggingEvent.LoggerName;
            //if (callersFulleName.Contains('['))
            //    callersFulleName = callersFulleName.Split('[').FirstOrDefault();
            var logger = loggingEvent.LoggerName;

            var machineName = string.IsNullOrWhiteSpace(System.Environment.MachineName) ? System.Environment.MachineName : "MachineName_not_available";
            var siteName = string.Empty;
            
            try
            {
                siteName = System.Web.Hosting.HostingEnvironment.ApplicationHost.GetSiteName();
            }
            catch (Exception e)
            {
                siteName = "MachineName_not_available";

            }

            var props = new[] {url, logger, siteName};
            switch (loggingEvent.Level.Name)
            {

                case "ERROR":
                    Log.Logger.Error(loggingEvent.RenderedMessage, props);
                    break;
                case "WARN":
                    Log.Logger.Warning(loggingEvent.RenderedMessage, props);
                    break;
                case "INFO":
                    Log.Logger.Information(loggingEvent.RenderedMessage, props);
                    break;
                case "DEBUG":
                    Log.Logger.Debug(loggingEvent.RenderedMessage, props);
                    break;
                case "FATAL":
                    Log.Logger.Fatal(loggingEvent.RenderedMessage, props);
                    break;
                default:
                    Log.Logger.Verbose(loggingEvent.RenderedMessage, props);
                    break;
            }
        }

     
    }
}