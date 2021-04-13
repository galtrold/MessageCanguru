using System;
using Serilog;
using Serilog.Configuration;
using Serilog.Formatting;

namespace MessageCanguru
{
    public static class UdpLogstashSinkExtensions
    {
        public static LoggerConfiguration UdpLogstash(this LoggerSinkConfiguration loggerConfiguration, ITextFormatter formatter = null, IFormatProvider formatProvider = null)
        {
            return loggerConfiguration.Sink(new UdpLogstashSink(formatter, formatProvider));
        }
    }
}