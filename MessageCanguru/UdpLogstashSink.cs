using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace MessageCanguru
{
    public class UdpLogstashSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;
        private readonly ITextFormatter _formatter;
        private readonly UdpClient _udpClient;
        private Object _lock = new object();
        private string _destination = "localhost";


        public UdpLogstashSink(ITextFormatter formatter, IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
            _formatter = formatter;
            _udpClient = new UdpClient();

        }

        public void Emit(LogEvent logEvent)
        {
            var renderMessage = logEvent.RenderMessage();
            logEvent.AddPropertyIfAbsent(new LogEventProperty("RenderMessage", new ScalarValue(renderMessage)));

            var strWriter = new StringWriter();
            _formatter.Format(logEvent, strWriter);
            var jsonMessage = strWriter.ToString();

            var destinationAddress = Dns.GetHostAddresses(_destination).FirstOrDefault(p => p.AddressFamily == AddressFamily.InterNetwork);
            var destinationEndpoint = new IPEndPoint(destinationAddress, 8086);
            var logPackage = Encoding.ASCII.GetBytes(jsonMessage);
            lock (_lock)
            {
                _udpClient.Send(logPackage, logPackage.Length, destinationEndpoint);
            }
        }
    }
}