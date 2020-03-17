using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Lab3
{
    class Program
    {

        private static void Task2()
        {
            try
            {
                IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
                Console.WriteLine("Имя компьютера: {0}", computerProperties.HostName);
                Console.WriteLine("Имя домена: {0}", computerProperties.DomainName);
                Console.WriteLine("Имя хоста: {0}", Environment.MachineName);

                //Получаем все сетевые интерфейсы локального компьютера
                NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();

                if (adapters == null || adapters.Length < 1)
                {
                    Console.WriteLine("Сетевые адаптеры не найдены");
                    //return;
                }
                Console.WriteLine("Количество сетевых интерфейсов: {0}\n", adapters.Length);

                foreach (NetworkInterface adapter in adapters)
                {
                    Console.WriteLine(String.Empty.PadLeft(50, '='));
                    Console.WriteLine("Имя сетевого адаптера: {0}", adapter.Name);
                    Console.WriteLine("Тип сетевого интерфейса: {0}", adapter.NetworkInterfaceType);
                    Console.WriteLine("Описание интерфейса: {0}", adapter.Description);
                    Console.WriteLine("Состояние сетевого подключения: {0}", adapter.OperationalStatus);

                    //Получение и вывод физического адреса
                    PhysicalAddress physicalAddress = adapter.GetPhysicalAddress();
                    byte[] bytes = physicalAddress.GetAddressBytes();
                    Console.Write("Физический адрес: ");
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        Console.Write("{0}", bytes[i].ToString("X2"));
                        if (i != bytes.Length - 1)
                        {
                            Console.Write("-");
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("Размер физического адреса: {0} байт", bytes.Length);

                    //Получение и вывод IP-адресов
                    //Получаем объект, описывающий конфигурацию сетевого интерфейса
                    IPInterfaceProperties adapterProperties = adapter.GetIPProperties();

                    //Получаем unicast-адреса, назначенные текущему интерфейсу
                    UnicastIPAddressInformationCollection unicastCollection = adapterProperties.UnicastAddresses;
                    if (unicastCollection.Count > 0)
                    {
                        foreach (UnicastIPAddressInformation unicastAddr in unicastCollection)
                        {
                            //Выводим IPv4
                            if (unicastAddr.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                
                                Console.WriteLine("IPv4 адрес: {0}", unicastAddr.Address.ToString());
                                Console.WriteLine("Маска: {0}", unicastAddr.IPv4Mask);
                                Console.WriteLine("Размер IPv4: {0}", unicastAddr.IPv4Mask.GetAddressBytes().Length);
                                Console.WriteLine("Размер префикса: {0}", unicastAddr.PrefixLength);

                            }

                            //Выводим IPv6
                            if (unicastAddr.Address.AddressFamily == AddressFamily.InterNetworkV6)
                            {
                                Console.WriteLine("IPv6 адрес: {0}", unicastAddr.Address);
                                Console.WriteLine("Размер IPv6: {0}", 128);
                            }
                        }
                    }
                    Console.WriteLine();
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(("Ошибка: " + ex.ToString()));
            }

            finally
            {
                Console.ReadLine();
            }
        }

        private static void Task3(string name = "hostname")
        {
            if (name == "hostname")
                name = Dns.GetHostName();
            //Console.WriteLine("");
            IPHostEntry info = Dns.GetHostEntry(name);
            // IP
            string IPv4 = "", IPv6 = "";
            foreach (IPAddress address in info.AddressList)
            {
                if (address.AddressFamily.ToString() != ProtocolFamily.InterNetworkV6.ToString())
                    IPv4 += "\t\t" + address + "\n";
                else
                    IPv6 += "\t\t" + address + "\n";
            }
            Console.WriteLine("\tIPv4 : \n" + IPv4);
            Console.WriteLine("");
            Console.WriteLine("\tIPv6 : \n" + IPv6);

            // Alias
            Console.WriteLine("\tAlias-имена: ");
            foreach (var alias in info.Aliases)
                Console.WriteLine("\t" + alias);
            Console.WriteLine("");
            IPHostEntry inf = new IPHostEntry();
            

            Console.WriteLine("\n");
        }

        private static void Task4()
        {
            Console.WriteLine("IPv4 : ");
            Console.WriteLine("\tАдрес петли обратной связи\t: " + IPAddress.Loopback);
            Console.WriteLine("\tШироковещательный IP-адрес\t: " + IPAddress.Broadcast);
            Console.WriteLine("\tАдрес, обозначающий все сетевые интерфейсы данного узла\t: " + IPAddress.Any);

            Console.WriteLine("IPv6 : ");
            Console.WriteLine("\tАдрес петли обратной связи\t: " + IPAddress.IPv6Loopback);
            Console.WriteLine("\tШироковещательный IP-адрес\t: " + IPAddress.Broadcast);
            Console.WriteLine("\tАдрес, обозначающий все сетевые интерфейсы данного узла\t: " + IPAddress.IPv6Any);
        }

        static void Main(string[] args)
        {
            char awnswear = '9';
            while (awnswear != '0')
            {
                Console.Write("\nВведите номер задания: ");
                awnswear = Console.ReadKey().KeyChar;
                Console.WriteLine("\n");
                if (awnswear == '2')
                {
                    Task2();
                }
                else if (awnswear == '3')
                {
                    Console.Write("Введите DNS-NAME : ");
                    Task3(Console.ReadLine());
                }
                else if (awnswear == '4')
                    Task4();
            }
        }
    }
}

