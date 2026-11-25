using System;

namespace Lab5;

// Запись таблицы «Товар»
public class Product
{
    private int _article;
    private int _categoryId;
    private string _name = "";
    private int _purchasePrice;
    private int _salePrice;
    private double _discount;

    // Артикул (ключ)
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
                throw new ArgumentException("Артикул должен быть больше нуля.");
            }

            _article = value;
        }
    }

    // ID категории
    public int CategoryId
    {
        get
        {
            return _categoryId;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("ID категории должен быть больше нуля.");
            }

            _categoryId = value;
        }
    }

    // Наименование товара
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Наименование товара не может быть пустым.");
            }

            _name = value;
        }
    }

    // Цена закупки, руб
    public int PurchasePrice
    {
        get
        {
            return _purchasePrice;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Цена закупки должна быть больше нуля.");
            }

            _purchasePrice = value;
        }
    }

    // Цена продажи без скидки, руб
    public int SalePrice
    {
        get
        {
            return _salePrice;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Цена продажи должна быть больше нуля.");
            }

            _salePrice = value;
        }
    }

    // Скидка по карте клиента в долях: 0.05 - это 5%
    public double Discount
    {
        get
        {
            return _discount;
        }
        set
        {
            if (value < 0 || value > 1)
            {
                throw new ArgumentException("Скидка должна быть от 0 до 1 (от 0% до 100%).");
            }

            _discount = value;
        }
    }

    public Product()
    {
    }

    public Product(int article, int categoryId, string name,
        int purchasePrice, int salePrice, double discount)
    {
        Article = article;
        CategoryId = categoryId;
        Name = name;
        PurchasePrice = purchasePrice;
        SalePrice = salePrice;
        Discount = discount;
    }

    public override string ToString()
    {
        return "Артикул " + _article + " | " + _name + " | категория " + _categoryId
               + " | закупка " + _purchasePrice + " руб. | продажа " + _salePrice
               + " руб. | скидка по карте " + Math.Round(_discount * 100) + "%";
    }
}