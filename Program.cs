using System;
using System.IO;

namespace Lab5;

internal class Program
{
    // Excel-файл лежит рядом с программой
    private const string FilePath = "LR5-var1.xlsx";

    private static void Main(string[] args)
    {
        Checked check = new Checked();
        DbHelper db = new DbHelper();
        bool work = true;

        while (work)
        {
            ShowMenu();
            int choice = check.ReadRange("Пункт меню:", 0, 9);

            // Работа с базой только после чтения
            if (choice > 1 && choice < 9 && !db.IsLoaded)
            {
                Console.WriteLine("Сначала прочитайте (пункт 1).");
                continue;
            }

            try
            {
                switch (choice)
                {
                    case 1:
                        db.Load(FilePath);
                        break;
                    case 2:
                        db.Show();
                        break;
                    case 3:
                        db.DeleteByKey();
                        break;
                    case 4:
                        db.AddElement();
                        break;
                    case 5:
                        db.Query1();
                        break;
                    case 6:
                        db.Query2();
                        break;
                    case 7:
                        db.Query3();
                        break;
                    case 8:
                        db.Query4();
                        break;
                    case 9:
                        db.Save(FilePath);
                        break;
                    case 0:
                        Console.WriteLine("Выход.");
                        work = false;
                        break;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Ошибка: файл не найден!");
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка: закройте файл в Excel!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Ошибка данных: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
    }

    private static void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("===== БД «Детские товары» =====");
        Console.WriteLine("1 - Прочитать базу из Excel");
        Console.WriteLine("2 - Просмотреть таблицу");
        Console.WriteLine("3 - Удалить запись по ключу");
        Console.WriteLine("4 - Добавить новую запись");
        Console.WriteLine("5 - Запрос 1 (1 таблица)");
        Console.WriteLine("6 - Запрос 2 (2 таблицы)");
        Console.WriteLine("7 - Запрос 3 (3 таблицы)");
        Console.WriteLine("8 - Запрос 4 (3+ таблицы)");
        Console.WriteLine("9 - Сохранить изменения");
        Console.WriteLine("0 - Выход");
    }
}