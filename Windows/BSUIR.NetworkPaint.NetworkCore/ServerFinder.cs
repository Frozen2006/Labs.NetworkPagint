using BSUIR.NetworkPaint.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BSUIR.NetworkPaint.NetworkCore
{
    public class ServerFinder
    {
        private const int MaxWaitTime = 60;
        private Socket _socket;
        private IPEndPoint _endpoint;
        private IPEndPoint _sendEndpoint;
        private volatile bool _isDataAccepted;
        private volatile ServerData _data;
        Thread reciverThread;

        public ServerFinder(int port)
        {
            _endpoint = new IPEndPoint(IPAddress.Any, port);
            _sendEndpoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), port + 1);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.EnableBroadcast = true;
            _socket.Bind(_endpoint);

            reciverThread = new Thread(() => ReciveData());
        }

        public ServerData Find()
        {
            var startTime = DateTime.Now;

            reciverThread.Start();

            while ((!_isDataAccepted) && ((DateTime.Now - startTime).Seconds < MaxWaitTime))
            {
                _socket.SendTo(new byte[0], _sendEndpoint);
                Thread.Sleep(500);
            }

            if (_isDataAccepted)
            {
                return _data;
            }
            else
            {
                return null;
            }
        }

        public void ReciveData()
        {
            byte[] buffer = new byte[8192];

            int dataLength = 0;

            while (!_isDataAccepted)
            {
                dataLength = _socket.Receive(buffer); 
                if (dataLength > 0)
                {
                    ReciveCallback(buffer);
                    buffer = new byte[8192];
                }
            }
        }


        public void ReciveCallback(byte[] data)
        {
            if (data.Length > 0)
            {
                MemoryStream stream = new MemoryStream(data);

                BinaryFormatter formatter = new BinaryFormatter();

                stream.Seek(0, SeekOrigin.Begin);

                var recivedData = (string)formatter.Deserialize(stream);

                _data = CustomXmlSerializer.Deserialize<ServerData>(recivedData);

                _isDataAccepted = true;
            }


        }
    }
}
