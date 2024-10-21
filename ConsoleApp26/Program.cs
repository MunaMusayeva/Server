using System.Net.Sockets;
using System.Net;
using System.Text;

var client = new Socket(AddressFamily.InterNetwork,
                        SocketType.Dgram,
                        ProtocolType.Udp);

var ip = IPAddress.Parse("127.0.0.1");
var port = 45678;
var remoteEP = new IPEndPoint(ip, port);

var buffer = new byte[ushort.MaxValue - 28];
var msg = string.Empty;

while (true)
{
    msg = Console.ReadLine() ?? string.Empty;
    var sendBuffer = Encoding.Default.GetBytes(msg);
    client.SendTo(sendBuffer, remoteEP);
    EndPoint serverEP = new IPEndPoint(IPAddress.Any, 0);
    var count = client.ReceiveFrom(buffer, ref serverEP);
    var serverMsg = Encoding.Default.GetString(buffer, 0, count);
    Console.WriteLine($"Server: {serverMsg}");
}
