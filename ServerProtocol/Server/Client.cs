using System.Net.Sockets;
using ServerProtocol.Server.Protocols;

namespace ServerProtocol.Server
{
    public class Client
    {
        public static int dataBufferSize = 4096;
        public int id;
        public TCP tcp;

        public Client(int _clientId, TcpClient socket)
        {
            id = _clientId;
            tcp = new TCP(id, socket);
        }

        public class TCP
        {
            public TcpClient socket;
            private readonly int id;
            private NetworkStream stream;
            private byte[] receiveBuffer = new byte[dataBufferSize];

            public TCP(int _id, TcpClient _socket)
            {
                id = _id;
                socket = _socket;
                stream = socket.GetStream();
            }

            public void Connect()
            {
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }

            // public void SendData(Packet _packet)
            // {
            //     try
            //     {
            //         if (socket != null)
            //         {
            //             stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
            //         }
            //     }
            //     catch (Exception _ex)
            //     {
            //         Console.WriteLine($"Error sending data to player {id} via TCP: {_ex}");
            //     }
            // }

            private void ReceiveCallback(IAsyncResult ar)
            {
                try
                {
                    int bytesRead = stream.EndRead(ar);

                    if (bytesRead <= 0)
                    {
                        //TODO DISCONECT
                        return;
                    }

                    byte[] receivedData = new byte[bytesRead];
                    Array.Copy(receiveBuffer, receivedData, bytesRead);


                    ProtocolManager.HandleNetworkInformation(id, receivedData);

                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
                }
                catch (Exception _ex)
                {
                    Console.WriteLine(_ex);
                }
            }
        }
    }
}