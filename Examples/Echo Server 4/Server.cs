using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace EchoServerGraphical
{
    public partial class Server : Form
    {
        TcpListener ServerListener = null;
        TcpClient ServerCln = null;
        Thread echoThread = null;

        bool serverOnline = false;

        public Server()
        {
            InitializeComponent();
        }
//-------------------------------------------------------------------------------
        private void Server_Load(object sender, EventArgs e)
        {
            if (ServerInite())
            {
                serverOnline = true;
                ServerStatuslbl.Text = "Online";
                ServerRUNbtn.Text = "OFF";

                ThreadInit();
            }                
        }
        private void Server_FormClosed(object sender, FormClosedEventArgs e)
        {
            serverOnline = false;
        }
//-------------------------------------------------------------------------------
        void ServerRUNbtn_Click(object sender, EventArgs e)
        {
            if (!serverOnline)
            {
                if (ServerInite())
                {
                    serverOnline = true;
                    ServerStatuslbl.Text = "Online";                
                    ServerRUNbtn.Text = "OFF";

                    ThreadInit();
                }                
            }
            else
            {
                serverOnline = false;                
            }
        }
//-------------------------------------------------------------------------------
        bool ServerInite()
        {
            ServerListener = null;
            try
            {
                ServerListener = new TcpListener(IPAddress.Parse(ipTXT.Text), Convert.ToInt32(portTXT.Text));
                ServerListener.Start();
                richTextBox1.Text = "The server is started." + "\r\n" + "\r\n";
            }
            catch (SocketException exc)
            {
                richTextBox1.Text += "Error: " + exc.Message + "\r\n";
                return false;
            }
            return true;
        }
        void ThreadInit()
        {
            echoThread = new Thread(new ThreadStart(WorkingWithClientThread));
            echoThread.IsBackground = false;
            CheckForIllegalCrossThreadCalls = false; //прекращает отлов ошибок неправомерного использования
            echoThread.Start();
        }                
//-------------------------------------------------------------------------------        
        void WorkingWithClientThread()
        {           
            WorkingWithNewClient();
            ServerShutdown();
        }
        void WorkingWithNewClient()
        {   
            richTextBox1.Text += "Waiting for a client..."; 
            while (serverOnline)
            {                                        
                if (ServerListener.Pending())
                {
                    ServerCln = ServerListener.AcceptTcpClient();
                    richTextBox1.Text += " connected." + "\r\n" + "\r\n";
                                        
                    MessageHandling();
                }
                Thread.Sleep(100);
            }      
        }        
        void MessageHandling()
        {
            NetworkStream NWS = ServerCln.GetStream();
            BinaryReader R = new BinaryReader(NWS);
            BinaryWriter W = new BinaryWriter(NWS);

            while (serverOnline)
            {
                try
                {
                    W.Write(EchoHandling(R.ReadString()));
                }
                catch (IOException)
                {
                    richTextBox1.Text += "Сlient was disconnected!" + "\r\n" + "\r\n";

                    W.Close();
                    R.Close();
                    NWS.Close();
                    ServerCln.Close();

                    WorkingWithNewClient();
                }
                catch (Win32Exception)
                {

                }
            }
            W.Close();
            R.Close();
            NWS.Close();
            ServerCln.Close();
        }
        void ServerShutdown()
        {
            ServerListener.Stop();

            try
            {
                ServerStatuslbl.Text = "Offline";
                ServerRUNbtn.Text = "ON";

                richTextBox1.Text += "\r\n" + "Shutdown complite." + "\r\n" + "\r\n";
            }
            catch (Win32Exception)
            {

            }
        }
//------------------------------------------------------------------------------- 
        string EchoHandling(string message)
        {
            richTextBox1.Text += "Recived: " + message + "\r\n";

            string answer = message;
            richTextBox1.Text += "Echo was sent." + "\r\n" + "\r\n";

            return answer;
        }
    }
}
