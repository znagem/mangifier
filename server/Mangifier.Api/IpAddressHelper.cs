using System.Net;
using System.Net.Sockets;

namespace Mangifier.Api;

public static class IpAddressHelper
{
    public static string GetIpAddress()
    {
        var ipAddress = "127.0.0.1";

        using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0);
        socket.Connect("8.8.8.8", 65530);

        if (socket.LocalEndPoint is IPEndPoint endPoint)
            ipAddress = endPoint.Address.ToString();

        return ipAddress;
    }
}