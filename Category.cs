using System;

namespace Lab5;

// Категория
public class Category
{
    private int _id;
    private string _name = "";
    private string _ageLimit = "";
    
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
                    ("ID категории должен быть больше нуля.");
            }

            _id = value;
        }
    }

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
                throw new ArgumentException
                    ("Наименование категории не может быть " +
                     "пустым.");
            }

            _name = value;
        }
    }
    
    public string AgeLimit
    {
        get
        {
            return _ageLimit;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value) ||
                !value.EndsWith("+"))
            {
                throw new ArgumentException("Возрастное " +
                    "ограничение должно быть вида 12+.");
            }

            _ageLimit = value;
        }
    }

    public Category()
    {
    }

    public Category(int id, string name, string ageLimit)
    {
        Id = id;
        Name = name;
        AgeLimit = ageLimit;
    }

    public override string ToString()
    {
        return "Категория " + _id + " | " + _name + " | " + 
               _ageLimit;
    }
}