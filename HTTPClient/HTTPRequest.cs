using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace HTTPClient
{
    class HTTPRequest
    {
        public static void HandleRequest(object obj)
        {
            string website = (string)obj;
            IPAddress[] addresses;
            try
            {
                addresses = Dns.GetHostAddresses(website);
                if (addresses.Length == 0)
                {
                    Console.WriteLine("Error: {0}", website);
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            IPAddress hostAddress = addresses[0];
            int hostPort = 80;
            IPEndPoint hostep = new IPEndPoint(hostAddress, hostPort);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(hostep);
            string request = "GET / HTTP/1.1\r\n" +
             "Host: " + website + "\r\n" +
             "User-Agent: Chrome/22.0.1229.94\r\n"
            + "\r\n";
            socket.Send(Encoding.ASCII.GetBytes(request));
            byte[] dataReceived = new byte[1024 * 1024];
            int len = socket.Receive(dataReceived);
            string page = Encoding.ASCII.GetString(dataReceived, 0, len);

            socket.Close();


        }

    }
}
