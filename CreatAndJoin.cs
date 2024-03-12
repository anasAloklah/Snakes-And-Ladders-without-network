using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace SnakesAndLadders
{
    public partial class CreatAndJoin : Form
    {
        IPAddress host;
        IPEndPoint hostEndpoint;
        Socket clientSocket;
        public CreatAndJoin()
        {
            InitializeComponent();
             host = IPAddress.Parse("192.168.1.255");
            IPAddress sd = IPAddress.Any;
            IPEndPoint ip = new IPEndPoint(IPAddress.Broadcast, 8000);
             hostEndpoint = new IPEndPoint(host, 8000);

             clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string msgToServer = "Creating";
            byte[] msgToServerArr = Encoding.ASCII.GetBytes(msgToServer);
            int msgLength = clientSocket.SendTo(msgToServerArr, hostEndpoint);





           // clientSocket.Shutdown(SocketShutdown.Both);
            //clientSocket.Close();
            //clientSocket.Shutdown(SocketShutdown g);
        }

        public static EndPoint endPoint;
        private void CreatAndJoin_Load(object sender, EventArgs e)
        {
            EndPoint endPoint = new IPEndPoint(IPAddress.Any, 8000);
            // Or use 127.0.0.1 address that refers to local host
            //8000 is the number of any free port
            //use command netstat to find all used ports.
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            serverSocket.Bind(endPoint);
            ThreadPool.QueueUserWorkItem(HandleClient, serverSocket);
        }
        static public void HandleClient(Object serverSocket1)
        {
            Socket serverSocket = (Socket)serverSocket1;
            
            byte[] msgFromClient = new byte[1024];
            int length = serverSocket.ReceiveFrom(msgFromClient, ref endPoint);
            string msg = Encoding.ASCII.GetString(msgFromClient, 0, length);
            Console.WriteLine(msg);
        }

    }
}
