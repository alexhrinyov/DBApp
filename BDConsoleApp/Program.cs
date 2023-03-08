using BDApp;
using DBApp;
using DBConsoleApp;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BDConsoleApp
{
    internal class Program
    {
        static Manager manager;
        static void Main(string[] args)
        {
            manager = new Manager();

            manager.Connect();

            Console.WriteLine("Список команд для работы консоли:");
            Console.WriteLine(Commands.stop + ": прекращение работы");
            Console.WriteLine(Commands.add + ": добавление данных");
            Console.WriteLine(Commands.delete + ": удаление данных");
            Console.WriteLine(Commands.update + ": обновление данных");
            Console.WriteLine(Commands.show + ": просмотр данных");

            string command;
            do
            {
                Console.WriteLine("Введите команду:");
                command = Console.ReadLine();
                Console.WriteLine();
                switch (command)
                {
                    case (nameof(Commands.add)):
                        Add();
                        break;
                    case (nameof(Commands.delete)):
                        Delete();
                        break;
                    case (nameof(Commands.update)):
                        Update();
                        break;
                    case (nameof(Commands.show)):
                        Show();
                        break;

                };
            }
            while (command != nameof(Commands.stop));

            


            manager.Disconnect();
        }

        public static void Delete()
        {
            Console.WriteLine("Введите логин для удаления:");

            var rowCount = manager.DeleteUserByLogin(Console.ReadLine());


            Console.WriteLine("Количество удаленных строк = " + rowCount);
        }

        public static void Update()
        {
            Console.WriteLine("Введите логин для обновления имени:");
            var login = Console.ReadLine();
            Console.WriteLine("Введите новое имя:");
            var name = Console.ReadLine();
            manager.UpdateUserName(name,"NetworkUser",login);
        }

        public static void Show()
        {
            manager.ShowData();
        }

        public static void Add()
        {
            manager.InsertUser();
        }


        public enum Commands
        {
            stop,
            add,
            delete,
            update,
            show
        }


    }
}