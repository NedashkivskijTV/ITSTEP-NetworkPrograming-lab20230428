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
            // -------------------------------------------------------------- ВІДПРАВКА даних - ЗАПИТ до сервера

            // Створення АКТИВНОГО сокета - буде відсилати дані
            // Параметри
            // - AddressFamily.InterNetwork - для IPv4
            // - SocketType.Dgram - тип пакету - протокол UDP (за замовчуванням TCP !)
            // - ProtocolType.IP - протокол передачі даних
            // Для отримання сокетом точки підключення потрібно передати IP-адресу та порт
            Socket socket_send = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);

            // Cтворення кінцевої точки "в один рядок"
            // Параметри 
            // - IP адреса сервера
            // - порт - 11000
            IPEndPoint endPoint_client = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);
            //IPEndPoint endPoint_client = new IPEndPoint(IPAddress.Loopback, 11000); // не працює

            // При використанні протоколу UDP при відправці даних (на боці клієнта) відсутній метод CONNECT

            // передача даних - без асинхронності
            socket_send.SendTo(Encoding.Default.GetBytes(tbClientQuery.Text), endPoint_client);

            // Очищення поля для вводу запитів
            tbClientQuery.Clear();


            // ---------------------------------------------------------------- ОТРИМАННЯ даних 
            byte[] buffer = new byte[1024];
            EndPoint endPoint_Server = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);

            try
            { 
                // Для відправки/отримання даних використовуються методи
                // відповідно SendTo() та ReceiveFrom()
                // ReceiveFrom() - поміщається в окремий потік
                // у параметри приймає - буфер, посилання на кінцеву точку підключеного сокета
                // результат (кількість отриманих байтів інформації) зберігатиметься 
                int len = socket_send.ReceiveFrom(buffer, ref endPoint_Server);

                // Виведення отриманої інф 
                StringBuilder sb = new StringBuilder(tbClientQuery.Text);
                sb.AppendLine($"{len} byte received from {endPoint_Server}"); // додавання технічної інф з перенесенням на новий рядок
                string timeCurent = Encoding.Default.GetString(buffer, 0, len); // отримання даних, відправлених сервером - поточний час
                sb.AppendLine(timeCurent); // додавання отриманої інф з перенесенням на новий рядок (зчитується з буферу від 0 до len)

                // Виведення статистики 
                tbClientQuery.BeginInvoke(new Action<string>(AddTextToTb), sb.ToString());

                // Виведення отриманого з сервера повідомлення - поточний час
                lbNetworkClock.BeginInvoke(new Action<string>(AddText), timeCurent);
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Закриття з'єднань
                //socket_send.Shutdown(SocketShutdown.Both); // розірвання усіх підключень
                socket_send.Shutdown(SocketShutdown.Send); // UDP-протокол - застосовується одностороннє розірвання після передачі даних
                socket_send.Close(); // Закриття сокета
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