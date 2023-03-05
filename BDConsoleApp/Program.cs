using BDApp;
using DBApp;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BDConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var data = new DataTable();
            var connector = new MainConnector();

            var result = connector.ConnectAsync();

            if (result.Result)
            {
                Console.WriteLine("Подключено успешно!");

                var db = new DbExecutor(connector);

                var tablename = "NetworkUser";

                Console.WriteLine("Получаем данные таблицы " + tablename);

                data = db.SelectAll(tablename);
                Console.WriteLine("Отключаем БД!");
                

                Console.WriteLine("Количество строк в " + tablename + ": " + data.Rows.Count);
                Console.WriteLine("Отсоединённая модель:");
                foreach (DataColumn column in data.Columns)
                {
                    Console.Write($"{column.ColumnName}\t");
                }
                Console.WriteLine();
                
                foreach (DataRow row in data.Rows)
                {
                    var cells = row.ItemArray;
                    foreach (var cell in cells)
                    {
                        Console.Write($"{cell}\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();

                // с помощью присоединённой модели

                SqlDataReader reader = db.SelectAllCommandReader(tablename);
                var columnList = new List<string>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var name = reader.GetName(i);
                    columnList.Add(name);
                }
                // выводим номера столбцов
                Console.WriteLine("Присоединённая модель:");
                for (int i = 0; i < columnList.Count; i++)
                {
                    Console.Write($"{columnList[i]}\t");
                }
                Console.WriteLine();
                // чтение

                //Метод reader.Read() возвращает значение true,
                //пока reader не достиг своего конца
                while (reader.Read())
                {
                    for (int i = 0; i < columnList.Count; i++)
                    {
                        var value = reader[columnList[i]];
                        Console.Write($"{value}\t");
                    }

                    Console.WriteLine();
                }
                connector.DisconnectAsync();


            }
            else
            {
                Console.WriteLine("Ошибка подключения!");
            }

            Console.ReadKey();




        }
    }
}