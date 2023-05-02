using System;
using System.Threading;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerNetworkClock
{
    public partial class Form1 : Form
    {
        Thread thread;

        public Form1()
        {
            InitializeComponent();
            
            //timer1.Start();
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            timer1.Start(); // ������ ������� 

            // �������� �������� ���� ������ �������� ����� �����  thread
            if (thread != null)
            {
                return;
            }

            // ��������� ��������� ������
            // ���������
            // - AddressFamily.InterNetwork - ��� IPv4
            // - SocketType.Dgram - ��� ������ - �������� UDP (�� ������������� TCP !)
            // - ProtocolType.IP - �������� �������� �����
            // ��� ��������� ������� ����� ���������� ������� �������� IP-������ �� ����
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);

            // C�������� ������ ����� "� ���� �����"
            // ��������� 
            // - IP ������ �������
            // - ���� - 11000
            //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);
            IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[2], 11000);

            // �������� ������ �� ������ �����
            socket.Bind(endPoint);

            // ��� ����������� ��������� UDP ��� ��������� �� ���� ������� ������� ����� Listen,
            // �� ������������� �� ������������ �'������� � ����������  �볺���� !!!

            // ������ � �������� ������ - � ��������� ���������� �-���, �� ������������ ��� �� �볺��� (���������)
            thread = new Thread(SendTimeToClient);

            // ����������� ������ � ������� �����
            thread.IsBackground = true;

            // ������ ������ - � ��������� ���������� ����� ������� ��� �������
            thread.Start(socket);

            // ����������� ��� ����� �������
            Text = "Server was started !";
            tbServerInfo.Text = "Server was started !\r\n";
        }

        private void SendTimeToClient(object? obj)
        {
            // ��������� ������, ���� ���� �'���������� � �볺���� -
            // ��������� ������, �� ��������� � ������������ ������
            Socket socket_ReceiveAndSend = obj as Socket;

            // ��������� ����� �볺���
            // ��������� ������
            byte[] buffer = new byte[1024];

            // ������������ ������ ����� EndPoint ��� ����,
            // ��� ����� �� ��������� ����-����� �볺��� - ���������� ���������
            // - IP ������ - IPAddress.Any 
            // - ���� - 11000 - ���������� ����� �������
            EndPoint endPoint = new IPEndPoint(IPAddress.Any, 11000);

            try
            {
                // ���� ���������� ���������� �볺���
                do
                {
                    // -------------------------------------------------------------- ��������� ����� - ����� -------------------------------------------
                    // ��� ��������/��������� ����� ���������������� ������
                    // �������� SendTo() �� ReceiveFrom()
                    // � ��������� ������ - �����, ��������� �� ������ ����� ����������� �볺���
                    // ��������� (������� ��������� ����� ����������) �������������� � ����� ���� int
                    int len = socket_ReceiveAndSend.ReceiveFrom(buffer, ref endPoint);

                    // ��������� �������� ��� 
                    StringBuilder sb = new StringBuilder(tbServerInfo.Text);
                    sb.AppendLine($"{len} byte received from {endPoint}, time {lbNetworkClock.Text}"); // ��������� ������� ��� � ������������ �� ����� �����
                    sb.AppendLine(Encoding.Default.GetString(buffer, 0, len)); // ��������� �������� ��� � ������������ �� ����� ����� (��������� � ������ �� 0 �� len)

                    // ��������� ��� ���� ���������
                    // ������� ������� ��������� � �������� ������ � � ����� ������ ������� ��� ��������� � �������� ����, 
                    // ������������� ����� BeginInvoke()
                    // ����� ������� Action<> �������� �����, ���� ���������� �� ��������� (����������)
                    tbServerInfo.BeginInvoke(new Action<string>(AddText), sb.ToString());

                    
                    // -------------------------------------------------------------- ²������� ����� -------------------------------------------
                    // �������� ����� - ��� ������������
                    socket_ReceiveAndSend.SendTo(Encoding.Default.GetBytes(lbNetworkClock.Text), endPoint);

                } while (true);

            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void AddText(string str)
        {
            tbServerInfo.Text = str;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // ³���������� ��������� ���� � ���������� ���������� ���������
            lbNetworkClock.Text = DateTime.Now.ToLongTimeString();
        }
    }
}