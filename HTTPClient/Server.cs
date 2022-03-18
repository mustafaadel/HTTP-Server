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
    class Server
    {
        private Socket serverSocket;
        private int port;
        public Server(int port)
        {
            this.port = port;
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint hostEndPoint = new IPEndPoint(IPAddress.Any, port);
            serverSocket.Bind(hostEndPoint);

        }
        public void StartServer()
        {
            serverSocket.Listen(100);
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                Console.WriteLine("New Client Accepted: {0}", clientSocket.RemoteEndPoint);
                Thread newThread = new Thread(new ParameterizedThreadStart(HandleConnection));
                newThread.Start(clientSocket);
            }

        }
        public void HandleConnection(object obj)
        {
            Socket clientSock = (Socket)obj;
            string welc = "Welcome to test server ";
            byte[] data = Encoding.ASCII.GetBytes(welc);
            clientSock.Send(data);
            int recievedLength;
            while (true)
            {
                data = new byte[1024];
                recievedLength = clientSock.Receive(data);
                if (recievedLength == 0)
                {
                    Console.WriteLine("Client {0} ended the connection", clientSock.RemoteEndPoint);
                    break;
                }
                Console.WriteLine("Recieved: {0} from Client: {1}", Encoding.ASCII.GetString(data, 0, recievedLength), clientSock.RemoteEndPoint);
                clientSock.Send(data, 0, recievedLength, SocketFlags.None);
            }
            clientSock.Close();
        }
    }
}
