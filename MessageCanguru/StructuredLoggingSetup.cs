using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Json;
using Sitecore.Pipelines;

namespace MessageCanguru
{
    public class StructuredLoggingSetup
    {
        public void Process(PipelineArgs args)
        {
            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.AppSettings()
            //    .CreateLogger();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.UdpLogstash(new JsonFormatter())
                .WriteTo.File("c:\\temp\\testlog.txt")
                .Enrich.WithHttpRequestRawUrl()
                .Enrich.WithHttpRequestId()
                .Enrich.WithHttpRequestUserAgent()
                .CreateLogger();
        }
    }
}