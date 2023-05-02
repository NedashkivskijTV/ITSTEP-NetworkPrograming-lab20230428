using System;
using System.Threading;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;

namespace ClientNetworkClock
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            Process.Start("ServerNetworkClock.exe");
        }

        private void btnGetTime_Click(object sender, EventArgs e)
        {
            //Text = "The client got time";
            // -------------------------------------------------------------- ²������� ����� - ����� �� �������

            // ��������� ��������� ������ - ���� �������� ���
            // ���������
            // - AddressFamily.InterNetwork - ��� IPv4
            // - SocketType.Dgram - ��� ������ - �������� UDP (�� ������������� TCP !)
            // - ProtocolType.IP - �������� �������� �����
            // ��� ��������� ������� ����� ���������� ������� �������� IP-������ �� ����
            Socket socket_send = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);

            // C�������� ������ ����� "� ���� �����"
            // ��������� 
            // - IP ������ �������
            // - ���� - 11000
            IPEndPoint endPoint_client = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);
            //IPEndPoint endPoint_client = new IPEndPoint(IPAddress.Loopback, 11000); // �� ������

            // ��� ����������� ��������� UDP ��� �������� ����� (�� ���� �볺���) ������� ����� CONNECT

            // �������� ����� - ��� ������������
            socket_send.SendTo(Encoding.Default.GetBytes(tbClientQuery.Text), endPoint_client);

            // �������� ���� ��� ����� ������
            tbClientQuery.Clear();


            // ---------------------------------------------------------------- ��������� ����� 
            byte[] buffer = new byte[1024];
            EndPoint endPoint_Server = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);

            try
            { 
                // ��� ��������/��������� ����� ���������������� ������
                // �������� SendTo() �� ReceiveFrom()
                // ReceiveFrom() - ��������� � ������� ����
                // � ��������� ������ - �����, ��������� �� ������ ����� ����������� ������
                // ��������� (������� ��������� ����� ����������) �������������� 
                int len = socket_send.ReceiveFrom(buffer, ref endPoint_Server);

                // ��������� �������� ��� 
                StringBuilder sb = new StringBuilder(tbClientQuery.Text);
                sb.AppendLine($"{len} byte received from {endPoint_Server}"); // ��������� ������� ��� � ������������ �� ����� �����
                string timeCurent = Encoding.Default.GetString(buffer, 0, len); // ��������� �����, ����������� �������� - �������� ���
                sb.AppendLine(timeCurent); // ��������� �������� ��� � ������������ �� ����� ����� (��������� � ������ �� 0 �� len)

                // ��������� ���������� 
                tbClientQuery.BeginInvoke(new Action<string>(AddTextToTb), sb.ToString());

                // ��������� ���������� � ������� ����������� - �������� ���
                lbNetworkClock.BeginInvoke(new Action<string>(AddText), timeCurent);
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // �������� �'������
                //socket_send.Shutdown(SocketShutdown.Both); // ��������� ��� ���������
                socket_send.Shutdown(SocketShutdown.Send); // UDP-�������� - ������������� ����������� ��������� ���� �������� �����
                socket_send.Close(); // �������� ������
            }

        }

        private void AddTextToTb(string str)
        {
            tbClientQuery.Text = str;
        }

        private void AddText(string str)
        {
            lbNetworkClock.Text = str;
        }
    }
}