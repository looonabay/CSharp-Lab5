using System;

namespace Lab5;

// Движение товаров
public class Movement
{
    private int _id;
    private DateTime _date;
    private string _shopId = "";
    private int _article;
    private string _operationType = "";
    private int _quantity;
    private string _hasCard = "";
    
    
    public int Id
    {
        get
        {
            return _id;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException
                    ("ID операции должен быть больше нуля.");
            }

            _id = value;
        }
    }
    
    public DateTime Date
    {
        get
        {
            return _date;
        }
        set
        {
            _date = value;
        }
    }
    
    public string ShopId
    {
        get
        {
            return _shopId;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException
                    ("ID магазина не может быть пустым.");
            }

            _shopId = value;
        }
    }
    
    public int Article
    {
        get
        {
            return _article;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException
                    ("Артикул должен быть больше нуля.");
            }

            _article = value;
        }
    }
    
    public string OperationType
    {
        get
        {
            return _operationType;
        }
        set
        {
            if (value != 
                "Поступление" && value != "Продажа" && 
                value != "Возврат")
            {
                throw new ArgumentException
                    ("Тип операции: Поступление, " +
                     "Продажа или Возврат.");
            }

            _operationType = value;
        }
    }

    // Количество упаковок
    public int Quantity
    {
        get
        {
            return _quantity;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException
                    ("Количество упаковок должно быть " +
                     "больше нуля.");
            }

            _quantity = value;
        }
    }
    
    public string HasCard
    {
        get
        {
            return _hasCard;
        }
        set
        {
            if (value != "Да" && value != "Нет" && value != "-")
            {
                throw new ArgumentException
                    ("Наличие карты: Да, Нет или -.");
            }

            _hasCard = value;
        }
    }

    public Movement()
    {
    }

    public Movement(int id, DateTime date, string shopId, 
        int article, string operationType, int quantity, 
        string hasCard)
    {
        Id = id;
        Date = date;
        ShopId = shopId;
        Article = article;
        OperationType = operationType;
        Quantity = quantity;
        HasCard = hasCard;
    }

    public override string ToString()
    {
        return "Операция " + _id + " | " + _date.ToString
                   ("dd.MM.yyyy") + " | магазин " + _shopId
               + " | артикул " + _article + " | " + 
               _operationType + " | " + _quantity
               + " уп. | карта: " + _hasCard;
    }
}