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
            timer1.Start(); // Запуск таймера 

            // Перевірка наявності лише одного значення змінної класу  thread
            if (thread != null)
            {
                return;
            }

            // Створення ПАСИВНОГО сокета
            // Параметри
            // - AddressFamily.InterNetwork - для IPv4
            // - SocketType.Dgram - тип пакету - протокол UDP (за замовчуванням TCP !)
            // - ProtocolType.IP - протокол передачі даних
            // Для отримання сокетом точки підключення потрібно передати IP-адресу та порт
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);

            // Cтворення кінцевої точки "в один рядок"
            // Параметри 
            // - IP адреса сервера
            // - порт - 11000
            //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);
            IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[2], 11000);

            // привязка сокета до кінцевої точки
            socket.Bind(endPoint);

            // При використанні протоколу UDP при підключенні на боці сервера відсутній метод Listen,
            // що прослуховував та встановлював з'єднання з конкретним  клієнтом !!!

            // Запуск у окремому потоці - у параметри передається ф-ція, що отримуватиме дані від клієнта (самописна)
            thread = new Thread(SendTimeToClient);

            // Переведення потоку у фоновий режим
            thread.IsBackground = true;

            // Запуск потоку - у параметри передається сокет сервера для запуску
            thread.Start(socket);

            // Повідомлення про старт сервера
            Text = "Server was started !";
            tbServerInfo.Text = "Server was started !\r\n";
        }

        private void SendTimeToClient(object? obj)
        {
            // Створення сокета, який буде з'єднуватись з клієнтом -
            // отримання сокета, що надходить у асинхронному потоці
            Socket socket_ReceiveAndSend = obj as Socket;

            // Отримання даних клієнта
            // Створення буфера
            byte[] buffer = new byte[1024];

            // Встановлення кінцевої точки EndPoint для того,
            // щоб через неї підключати будь-якого клієнта - вказуються параметри
            // - IP адреса - IPAddress.Any 
            // - порт - 11000 - аналогічний порту сервера
            EndPoint endPoint = new IPEndPoint(IPAddress.Any, 11000);

            try
            {
                // Цикл очікування підключення клієнта
                do
                {
                    // -------------------------------------------------------------- ОТРИМАННЯ даних - ЗАПИТ -------------------------------------------
                    // Для відправки/отримання даних використовуються методи
                    // відповідно SendTo() та ReceiveFrom()
                    // у параметри приймає - буфер, посилання на кінцеву точку підключеного клієнта
                    // результат (кількість отриманих байтів інформації) зберігатиметься у змінній типу int
                    int len = socket_ReceiveAndSend.ReceiveFrom(buffer, ref endPoint);

                    // Виведення отриманої інф 
                    StringBuilder sb = new StringBuilder(tbServerInfo.Text);
                    sb.AppendLine($"{len} byte received from {endPoint}, time {lbNetworkClock.Text}"); // додавання технічної інф з перенесенням на новий рядок
                    sb.AppendLine(Encoding.Default.GetString(buffer, 0, len)); // додавання отриманої інф з перенесенням на новий рядок (зчитується з буферу від 0 до len)

                    // Виведення інф після отримання
                    // Оскільки обробка відбуається з окремого потоці і з цього потоку потрібно дані вигрузити в основний потік, 
                    // застосовується метод BeginInvoke()
                    // через делегат Action<> передати метод, який виконається по завершенні (самописний)
                    tbServerInfo.BeginInvoke(new Action<string>(AddText), sb.ToString());

                    
                    // -------------------------------------------------------------- ВІДПРАВКА даних -------------------------------------------
                    // передача даних - без асинхронності
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
            // Відображення поточного часу у відповідному візуальному компоненті
            lbNetworkClock.Text = DateTime.Now.ToLongTimeString();
        }
    }
}