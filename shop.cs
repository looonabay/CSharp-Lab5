using System;

namespace Lab5;

// Магазин
public class Shop
{
    private string _id = "";
    private string _district = "";
    private string _address = "";
    
    public string Id
    {
        get
        {
            return _id;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("ID магазина не " +
                    "может быть пустым.");
            }

            _id = value;
        }
    }

    public string District
    {
        get
        {
            return _district;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException
                    ("Район не может быть пустым.");
            }

            _district = value;
        }
    }

    public string Address
    {
        get
        {
            return _address;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException
                    ("Адрес не может быть пустым.");
            }

            _address = value;
        }
    }

    public Shop()
    {
    }

    public Shop(string id, string district, string address)
    {
        Id = id;
        District = district;
        Address = address;
    }

    public override string ToString()
    {
        return "Магазин " + _id + " | район " + _district +
               " | " + _address;
    }
}