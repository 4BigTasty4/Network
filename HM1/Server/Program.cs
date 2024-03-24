using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main(string[] args)
    {
        StartServer();
    }

    static void StartServer()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 8888);
        listener.Start();
        Console.WriteLine("Server started. Waiting for connections...");

        TcpClient client = listener.AcceptTcpClient();
        Console.WriteLine("Client connected.");

        NetworkStream stream = client.GetStream();

        byte[] buffer = new byte[1024];
        int bytesRead;

        bool shouldExit = false;

        while (!shouldExit)
        {
            bytesRead = stream.Read(buffer, 0, buffer.Length);
            string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Client: {dataReceived}");

            if (dataReceived.ToLower() == "bye")
            {
                Console.WriteLine("Client disconnected.");
                shouldExit = true;
            }
            else
            {
                Console.Write("Server: ");
                string response = Console.ReadLine();
                byte[] responseBuffer = Encoding.ASCII.GetBytes(response);
                
                stream.Write(responseBuffer, 0, responseBuffer.Length);
            }
        }
        stream.Close();
        client.Close();
        listener.Stop();
    }
}