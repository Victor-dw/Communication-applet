using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        Socket g_client;


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            g_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType
                .Tcp);
            IPAddress ip = IPAddress.Parse(textBox1.Text);
            IPEndPoint iPEndPoint = new IPEndPoint(ip, Convert.ToInt32(textBox2.Text));
            g_client.Connect(iPEndPoint);
            new Thread(() =>
            {
                while (true)
                {
              byte[] receivedData = new byte[1024 * 1024];
                g_client.Receive(receivedData);
                textBox3.Text += DateTime.Now + "收到" + ASCIIEncoding.UTF8.GetString(receivedData) + "\r\n";
                }
            }).Start();
        }

        void SendMessage(String msg)
        {
            g_client.Send(ASCIIEncoding.UTF8.GetBytes(msg));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendMessage(textBox4.Text);
            textBox3.Text += DateTime.Now + "发送" + textBox4.Text + "\r\n";
            textBox4.Text = null; 
        }
    }
}
