using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Web.Configuration;
using log4net.Appender;
using log4net.spi;
using Serilog;

namespace MessageCanguru
{
    public class SitecoreStructedUdpAppender : AppenderSkeleton
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            //// Get url path
            //try
            //{
            //    var path = HttpContext.Current.Request.Url.AbsoluteUri;
            //    loggingEvent.Properties["uri"] = path;
            //}
            //catch (Exception e)
            //{
            //    //loggingEvent.Properties["uri"] = "N/A";
            //}





            //StackTrace stackTrace = new StackTrace();

            //// Get calling method name
            //string callersFulleName = string.IsNullOrWhiteSpace(loggingEvent.LoggerName) ? "Unable_to_resolve_caller" : loggingEvent.LoggerName;
            //if (callersFulleName.Contains('['))
            //    callersFulleName = callersFulleName.Split('[').FirstOrDefault();


            //var machineName = System.Environment.MachineName;
            //loggingEvent.Properties["hostname"] = string.IsNullOrWhiteSpace(machineName) ? "MachineName_not_available" : machineName;
            //try
            //{
            //    var siteName = System.Web.Hosting.HostingEnvironment.ApplicationHost.GetSiteName(); // TODO: get hostname
            //    loggingEvent.Properties["sitename"] = string.IsNullOrWhiteSpace(siteName) ? "not_available" : siteName;

            //}
            //catch (Exception e)
            //{
            //    loggingEvent.Properties["sitename"] = "MachineName_not_available";

            //}

            //loggingEvent.Properties["log_owner"] = callersFulleName;

            
            //var configuredSitename = WebConfigurationManager.AppSettings["elkIndexName"];

            //loggingEvent.Properties["indexname"] = string.IsNullOrWhiteSpace(configuredSitename) ? "sitecore_index" : configuredSitename;



            //if (!_isValidated)
            //    ValidateConfiguration();

            //var ipEndPoint = new IPEndPoint(_ipAddress, portNumber);
            //var logMessage = RenderLoggingEvent(loggingEvent);
            //var message = Encoding.ASCII.GetBytes(logMessage);
            //lock (localLock)
            //{
            //    _udpClient.Send(message, message.Length, ipEndPoint);
            //}

            var data = loggingEvent.GetLoggingEventData();
            
            switch (loggingEvent.Level.Name)
            {
                
                case "ERROR":
                    Log.Logger.Error(loggingEvent.RenderedMessage);
                    break;
                case "WARN":
                    Log.Logger.Warning(loggingEvent.RenderedMessage);
                    break;
                case "INFO":
                    Log.Logger.Information(loggingEvent.RenderedMessage);
                    break;
                case "DEBUG":
                    Log.Logger.Debug(loggingEvent.RenderedMessage);
                    break;
                case "FATAL":
                    Log.Logger.Fatal(loggingEvent.RenderedMessage);
                    break;
                default:
                    Log.Logger.Verbose(loggingEvent.RenderedMessage);
                    break;
            }
        }
    }
}