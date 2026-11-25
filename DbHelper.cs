using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;

namespace Lab5;

public class DbHelper
{
    private List<Movement> _movements = new List<Movement>();
    private List<Product> _products = new List<Product>();
    private List<Category> _categories = new List<Category>();
    private List<Shop> _shops = new List<Shop>();

    private bool _isLoaded = false;
    private Checked _check = new Checked();

    public bool IsLoaded { get { return _isLoaded; } }

    // Чтение
    public void Load(string path)
    {
        using (XLWorkbook book = new XLWorkbook(path))
        {
            var sM = book.Worksheet("Движение товаров");
            _movements = (from r in sM.RowsUsed() 
                where r.RowNumber() > 1
                select new Movement(
                    r.Cell(1).GetValue<int>(), 
                    r.Cell(2).GetDateTime(),
                    r.Cell(3).GetString().Trim(), 
                    r.Cell(4).GetValue<int>(),
                    r.Cell(5).GetString().Trim(), 
                    r.Cell(6).GetValue<int>(),
                    r.Cell(7).GetString().Trim()
                )).ToList();

            var sP = book.Worksheet("Товар");
            _products = (from r in sP.RowsUsed() 
                where r.RowNumber() > 1
                select new Product(
                    r.Cell(1).GetValue<int>(), 
                    r.Cell(2).GetValue<int>(),
                    r.Cell(3).GetString().Trim(), 
                    r.Cell(4).GetValue<int>(), 
                    r.Cell(5).GetValue<int>(), 
                    r.Cell(6).GetValue<double>() 
                )).ToList();

            var sC = book.Worksheet("Категория");
            _categories = (from r in sC.RowsUsed() 
                where r.RowNumber() > 1
                select new Category(
                    r.Cell(1).GetValue<int>(), 
                    r.Cell(2).GetString().Trim(),
                    r.Cell(3).GetString().Trim()
                )).ToList();

            var sS = book.Worksheet("Магазин");
            _shops = (from r in sS.RowsUsed() 
                where r.RowNumber() > 1
                select new Shop(
                    r.Cell(1).GetString().Trim(), 
                    r.Cell(2).GetString().Trim(),
                    r.Cell(3).GetString().Trim()
                )).ToList();

            _isLoaded = true;
        }
        Console.WriteLine("База загружена!");
    }

    // Просмотр
    public void Show()
    {
        int t = ChooseTable("Какую таблицу показать?");
        if (t == 1)
        {
            foreach (var m in _movements) 
                Console.WriteLine(m);
        }
        else if (t == 2)
        {
            foreach (var p in _products) 
                Console.WriteLine(p);
        }
        else if (t == 3)
        {
            foreach (var c in _categories) 
                Console.WriteLine(c);
        }
        else
        {
            foreach (var s in _shops) 
                Console.WriteLine(s);
        }
    }

    // Удаление
    public void DeleteByKey()
    {
        int t = ChooseTable("Из какой удалить?");
        if (t == 1)
        {
            int id = _check.ReadInt("ID операции:");
            var item = _movements
                .FirstOrDefault(m => m.Id == id);
            if (item != null)
            {
                _movements.Remove(item);
                Console.WriteLine("Удалено!");
            }
            else Console.WriteLine("Не найдено.");
        }
        else if (t == 2)
        {
            int a = _check.ReadInt("Артикул:");
            var item = _products
                .FirstOrDefault(p => p.Article == a);
            if (item != null)
            {
                _products.Remove(item);
                Console.WriteLine("Удалено!");
            }
            else Console.WriteLine("Не найдено.");
        }
        else if (t == 3)
        {
            int id = _check.ReadInt("ID категории:");
            var item = _categories
                .FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                _categories.Remove(item);
                Console.WriteLine("Удалено!");
            }
            else Console.WriteLine("Не найдено.");
        }
        else
        {
            string id = _check.ReadString("ID магазина:");
            var item = _shops
                .FirstOrDefault(s => s.Id == id);
            if (item != null)
            {
                _shops.Remove(item);
                Console.WriteLine("Удалено!");
            }
            else Console.WriteLine("Не найдено.");
        }
    }
    
    // Добавление
    public void AddElement()
    {
        int t = ChooseTable("В какую добавить?");
        try
        {
            if (t == 1)
            {
                int id = _check.ReadInt("ID операции:");
                string sh = _check.ReadString("ID магазина:");
                int a = _check.ReadInt("Артикул:");
                string op = _check.ReadString(
                    "Тип (Поступление/Продажа/Возврат):");
                int q = _check.NaturalReadInt("Количество:");
                string cd = _check.ReadString(
                    "Карта (Да/Нет/-):");
                _movements.Add(new Movement(
                    id, DateTime.Now, sh, a, op, q, cd));
            }
            else if (t == 2)
            {
                int a = _check.ReadInt("Артикул:");
                int c = _check.ReadInt("ID категории:");
                string n = _check.ReadString("Наименование:");
                int pur = _check.NaturalReadInt("Цена закупки:");
                int sale = _check.NaturalReadInt("Цена продажи:");
                double disc = _check.ReadDouble("Скидка (0-1):");
                
                _products.Add(new Product(
                    a, c, n, pur, sale, disc));
            }
            else if (t == 3)
            {
                int id = _check.ReadInt("ID категории:");
                string n = _check.ReadString("Наименование:");
                string a = _check.ReadString(
                    "Возрастное ограничение (12+):");
                _categories.Add(new Category(id, n, a));
            }
            else
            {
                string id = _check.ReadString("ID магазина:");
                string d = _check.ReadString("Район:");
                string a = _check.ReadString("Адрес:");
                _shops.Add(new Shop(id, d, a));
            }
            Console.WriteLine("Элемент добавлен!");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }

    // LINQ-запросы
    public void Query1()
    {
        Console.WriteLine("--- Товары > 500 руб. ---");
        var res = from p in _products 
                  where p.SalePrice > 500 
                  select p;
        foreach (var p in res) 
            Console.WriteLine(p);
    }

    public void Query2()
    {
        Console.WriteLine("--- Общая выручка ---");
        var res = (from m in _movements
                   join p in _products 
                   on m.Article equals p.Article
                   where m.OperationType == "Продажа"
                   select p.SalePrice * m.Quantity)
                   .Sum();
        Console.WriteLine("Итого: " + res + " руб.");
    }

    public void Query3()
    {
        Console.WriteLine("--- Товары в Худонковом ---");
        var res = from m in _movements
                  join s in _shops 
                  on m.ShopId equals s.Id
                  join p in _products 
                  on m.Article equals p.Article
                  where s.District == "Худонковый"
                  && m.OperationType == "Продажа"
                  select new { p.Name, s.District };
        foreach (var item in res) 
            Console.WriteLine(item.Name);
    }

    public void Query4()
    {
        Console.WriteLine("--- Пример из задания " +
                          "(стоимость игрушек) ---");
        var res = (from m in _movements
                   join s in _shops 
                   on m.ShopId equals s.Id
                   join p in _products 
                   on m.Article equals p.Article
                   join c in _categories 
                   on p.CategoryId equals c.Id
                   where s.District == "Худонковый"
                   && c.Name == "Игрушки на радиоупр."
                   && c.AgeLimit == "12+"
                   && m.OperationType == "Продажа"
                   && m.Date >= new DateTime(2024, 8, 1)
                   && m.Date <= new DateTime(2024, 8, 5)
                   select p.SalePrice * m.Quantity)
                   .Sum();
        Console.WriteLine("Ответ: " + res + " руб.");
    }
    
    // Сохранение
    public void Save(string path)
    {
        using (XLWorkbook book = new XLWorkbook())
        {
            var wsS = book.Worksheets.Add("Магазин");
            for (int i = 0; i < _shops.Count; i++)
            {
                var s = _shops[i];
                wsS.Cell(i + 2, 1).Value = s.Id;
                wsS.Cell(i + 2, 2).Value = s.District;
                wsS.Cell(i + 2, 3).Value = s.Address;
            }

            var wsC = book.Worksheets.Add("Категория");
            for (int i = 0; i < _categories.Count; i++)
            {
                var c = _categories[i];
                wsC.Cell(i + 2, 1).Value = c.Id;
                wsC.Cell(i + 2, 2).Value = c.Name;
                wsC.Cell(i + 2, 3).Value = c.AgeLimit;
            }

            var wsP = book.Worksheets.Add("Товар");
            for (int i = 0; i < _products.Count; i++)
            {
                var p = _products[i];
                wsP.Cell(i + 2, 1).Value = p.Article;
                wsP.Cell(i + 2, 2).Value = p.CategoryId;
                wsP.Cell(i + 2, 3).Value = p.Name;
                wsP.Cell(i + 2, 4).Value = p.PurchasePrice;
                wsP.Cell(i + 2, 5).Value = p.SalePrice;
                wsP.Cell(i + 2, 6).Value = p.Discount;
            }

            var wsM = book.Worksheets.Add("Движение");
            for (int i = 0; i < _movements.Count; i++)
            {
                var m = _movements[i];
                wsM.Cell(i + 2, 1).Value = m.Id;
                wsM.Cell(i + 2, 2).Value = m.Date;
                wsM.Cell(i + 2, 3).Value = m.ShopId;
                wsM.Cell(i + 2, 4).Value = m.Article;
                wsM.Cell(i + 2, 5).Value = m.OperationType;
                wsM.Cell(i + 2, 6).Value = m.Quantity;
                wsM.Cell(i + 2, 7).Value = m.HasCard;
            }
            book.SaveAs(path);
        }
        Console.WriteLine("Сохранено!");
    }

    private int ChooseTable(string q)
    {
        Console.WriteLine(q);
        Console.WriteLine("1-Движение, 2-Товар");
        Console.WriteLine("3-Категория, 4-Магазин");
        return _check.ReadRange("Номер:", 1, 4);
    }
}