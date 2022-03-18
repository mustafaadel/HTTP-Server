using System;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace HTTPClient
{
    class Program
    {

        static void Main(string[] args)
        {
            string[] websites = new string[]
            {
               "www.google.com","www.facebook.com","www.yahoo.com"
            };
            foreach (string website in websites)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(HTTPRequest.HandleRequest));
                thread.Start(website);
            }
            Console.ReadLine();
        }
    }
}
