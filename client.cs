using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public class client
    {
        public static async Task<Socket> StartClient()
        {
            Console.Write("Server Ip:");
            string ipAdress = Console.ReadLine();
            Console.Write("Port:");
            int port = int.Parse( Console.ReadLine());
            byte[] bytes = new byte[1024];
            try
            {  
                IPAddress address = Dns.GetHostEntry(ipAdress).AddressList[0];
                Console.WriteLine("IpAddress --->"+address.ToString()+" Ip address family--->"+address.AddressFamily);
                IPEndPoint endPoint = new IPEndPoint(address, port);

                Socket sender = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    sender.Connect(endPoint);
                    Console.WriteLine($"Socket Connected to {sender.RemoteEndPoint}");
                    return sender;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    throw;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string RecieveMessage(Socket handle)
        {
            string data = null;
            byte[] bytes = new byte[1024];
            int bytesRecive = handle.Receive(bytes);
            data += Encoding.ASCII.GetString(bytes, 0, bytesRecive);
            return data;
        }
        public static void SendMessage(Socket handle, string data)
        {
            byte[] msg = Encoding.ASCII.GetBytes(data);
            handle.Send(msg);
        }
        public static void CloseConnection(Socket handle)
        {
            handle.Shutdown(SocketShutdown.Both);
            handle.Disconnect(true);
            handle.Close();
        }
    }
}
