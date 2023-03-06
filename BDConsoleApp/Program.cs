using BDApp;
using DBApp;
using DBConsoleApp;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BDConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var manager = new Manager();

            manager.Connect();

            manager.ShowData();

            


            Console.WriteLine("Введите логин для удаления:");

            var rowCount =  manager.DeleteUserByLogin(Console.ReadLine());


            Console.WriteLine("Количество удаленных строк = " + rowCount);

            manager.ShowData();
            manager.Disconnect();
        }
    }
}