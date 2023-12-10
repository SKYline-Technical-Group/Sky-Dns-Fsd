using System;
using System.Net;
using System.Net.Sockets;

class DnsServer
{
    static void Main()
    {
        // 配置DNS服务器的IP地址和端口
        string dnsIp = "127.0.0.1";
        int dnsPort = 53;

        // 创建UDP监听器
        UdpClient udpListener = new UdpClient(dnsPort);
        IPEndPoint clientEndpoint = new IPEndPoint(IPAddress.Any, 0);

        Console.WriteLine($"DNS Server listening on {dnsIp}:{dnsPort}");

        while (true)
        {
            // 接收DNS请求
            byte[] dnsRequestData = udpListener.Receive(ref clientEndpoint);

            // 处理DNS请求并生成响应
            byte[] dnsResponseData = GenerateDnsResponse(dnsRequestData);

            // 发送DNS响应到客户端
            udpListener.Send(dnsResponseData, dnsResponseData.Length, clientEndpoint);
        }
    }

    static byte[] GenerateDnsResponse(byte[] request)
    {
        // 在这里添加你的DNS记录，这是一个例子
        var dnsRequest = DnsClient.Request.Read(request);
        var dnsResponse = dnsRequest.WithQueryResponse(new IPAddress(1, 2, 3, 4));
        return dnsResponse.AsByteArray();
    }
}