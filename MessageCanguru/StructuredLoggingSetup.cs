using Serilog;
using Sitecore.Pipelines;

namespace MessageCanguru
{
    public class StructuredLoggingSetup
    {
        public void Process(PipelineArgs args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();
        }
    }
}