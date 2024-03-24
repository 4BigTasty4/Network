using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main(string[] args)
    {
        StartClient();
    }

    static void StartClient()
    {
        TcpClient client = new TcpClient("127.0.0.1", 8888);
        Console.WriteLine("Connected to server.");
        
        NetworkStream stream = client.GetStream();

        string message;

        do
        {
            Console.WriteLine("Client: ");
            message = Console.ReadLine();
            byte[] messageBuffer = Encoding.ASCII.GetBytes(message);
            stream.Write(messageBuffer, 0, messageBuffer.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Server: {response}");
            
        } while (message.ToLower() != "bye");
        
        stream.Close();
        client.Close();
        Console.WriteLine("Connection closed.");
    }
}