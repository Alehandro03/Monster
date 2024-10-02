using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster
{
    enum TypeOfItems
    {
        BottleOfHealth = 1,
        Sword,   //меч
        Onion,     //лук
        Shield,  //щит
        Helmet,  // шлем
        Armor,   // доспехи
        Club,    // дубинка
        Knife    // нож

    }

    class Item //Шаблон предмета
    {
        public TypeOfItems _typeOfItems; //Название

        public string _name { get; set; }

        public string _info { get; set; }

        public int _damage { get; set; }

        public int _protection { get; set; }

        public int Bonus = 0;

        public bool Use;

        private int y  = 0;
        public int Count  // колличество данного предмета
        {
            get
            {
                return y;
            }
            set
            {
                y += 1;
            }
        }

        public double Cost { get; set; } // цена

        public Item(TypeOfItems typeOfItems, double cost, int count, int protection, string name, string info)
        {
            _typeOfItems = typeOfItems;
            Cost = cost;
            Count = count;
            _protection = protection;
            _name = name;
            _info = info;
        }
        public Item(TypeOfItems typeOfItems, int damage, double cost, int count, int protection, string name, string info, int bonus, bool use)
            : this(typeOfItems, cost, count, protection, name, info)
        {
            _damage = damage;
            Bonus = bonus;
            Use = use;
        }

        public Item()
        {

        }

        public string ItemText
        {
            get
            {
                return (_typeOfItems == TypeOfItems.BottleOfHealth ? "Склянка здоровья" : (_typeOfItems == TypeOfItems.Helmet ? "Шлем"
                    : (_typeOfItems == TypeOfItems.Knife ? "Нож" : (_typeOfItems == TypeOfItems.Armor ? "Доспехи"
                   : (_typeOfItems == TypeOfItems.Onion ? "Лук" : (_typeOfItems == TypeOfItems.Club ? "Дубина"
                   : (_typeOfItems == TypeOfItems.Shield ? "Щит" : "Меч")))))));
            }

        }
    }
    class Inventar // Инвентарь персонажа
    {

        public static List<Item> mas = new List<Item>(); // список предметов персонажа       

        public string Owner { get; set; } //Владелец

        public Inventar(string owner) 
        {
            Owner = owner;
        }   
        public static List<Item> AddItem(Item item)
        {
                     
            foreach (var i in mas) 
            {
                if(i != item) mas.Add(item);

                else if(i==item) item.Count++;
            }   
            return mas;
            
        }

        public static string DeleteItem( Personage pers, Item item)
        {
            string a  = " ";
            foreach (var i in pers.item)
            {
                if (item._name == i._name)
                {
                    pers.Hp = 100;
                    pers.item.Remove(i);

                    return a = "Предмет использован!";
                }
                else a =  "Данный предмет отсутствует!";

                
            }
            return a;
            
        }

        public static string UseItem( Personage pers, Item item)

        {
            string a = " ";
            foreach (var i in pers.item)
            {
                if (item._name == i._name)
                {
                    if (item._protection != 0 && i.Use == false)
                    {
                        pers.Hp += item._protection;
                        i.Use = true;
                        return "Броня использована!";
                        
                    }
                    else if (item._damage != 0 && i.Use == false)
                    {
                        pers.MinAttack += item._damage;
                        i.Use = true;
                        return "Оружие использовано";
                    }
                }
                else a = "Данный предмет отсутствует!";
            }
            return a;

        }  

        public static string RemoveItem( Personage pers, Item item)
        {
            string tmp = " ";

            foreach (var i in pers.item)
            {
                if (item._name == i._name)
                {
                    if (item._protection != 0 && i.Use == true)
                    {
                        pers.Hp -= item._protection;
                        i.Use = false;
                        tmp = "Броня снята!";
                    }
                    else if (item._damage != 0 && i.Use == true)
                    {
                        pers.MinAttack -= item._damage;
                        i.Use = false;
                        tmp = "Оружие снято";
                    }
                }
                else tmp = "Данный предмет отсутствует!";
            }
            return tmp;


        }

    }
    
}
