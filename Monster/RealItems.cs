using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster
{
    class RealItems
    {
        // Методы по созданию предметов.

        public static Item CreateBottle()
        {
            Item bottle = new Item();

            bottle._info = "Склянка здоровья(Полностью изцеляет)";
            bottle._typeOfItems = (TypeOfItems)1;  //приведем int к TypeOfItems
            bottle._name = bottle._typeOfItems.ToString();
            bottle.Cost = 500;
            

            return bottle;
        }

        public static Item CreateSword()
        {
            Item sword = new Item();

            sword._info = "Двуручный меч(Нет ничего сильнее!!!)";
            sword._typeOfItems = (TypeOfItems)2;
            sword._name = sword._typeOfItems.ToString();
            sword._damage = 20;
            sword.Cost = 5000;
            

            return sword;

        }

        public static Item CreateOnion()
        {
            Item onion = new Item();

            Personage pers = new Personage();

            onion._typeOfItems = (TypeOfItems)3;
            onion._name = onion._typeOfItems.ToString();

            if(pers._monster == (MonsterType)7) onion._damage = 25;
            else onion._damage = 15;

            onion.Cost = 3500;
            onion._info = "Длинный эльфийский лук(Со 100 метров белке в глаз!!!)";

            return onion;
        }

        public static Item CreateShield()
        {
            Item shield = new Item();

            shield._typeOfItems = (TypeOfItems)4;
            shield._name = shield._typeOfItems.ToString();
            shield._protection = 15;
            shield.Cost = 1500;
            shield._info = "Гномий Щит(сохранит жизнь во всех ударах!!!)";

            return shield;
        }

        public static Item CreateHelmet()
        {
            Item helmet = new Item();

            helmet._typeOfItems = (TypeOfItems)5;
            helmet._name = helmet._typeOfItems.ToString();
            helmet._protection = 10;
            helmet.Cost = 1200;
            helmet._info = "Офицерский шлем(Твоя голова выдердит любой молот!!!)";

            return helmet;
        }

        public static Item CreateArmor()
        {
            Item armor = new Item();

            armor._typeOfItems = (TypeOfItems)6;
            armor._name = armor._typeOfItems.ToString();
            armor._protection = 20;
            armor.Cost = 2000;
            armor._info = "Панцирь(Безопаснее только в танке!!!)";

            return armor;
        }

        public static Item CreateСlub()
        {
            Item club = new Item();

            club._typeOfItems = (TypeOfItems)7;
            club._name = club._typeOfItems.ToString();
            club._damage = 10;
            club.Cost = 2500;
            club._info = "Дубина(Лучше чем кулаки!!!)";

            return club;
        }

        public static Item CreateKnife()
        {
            Item knife = new Item();

            Personage pers = new Personage();

            knife._typeOfItems = (TypeOfItems)8;
            knife._name = knife._typeOfItems.ToString();

            if (pers._monster == (MonsterType)3)
            {
                knife._damage = 5;
                knife.Bonus = 10;
            }
            else knife._damage = 5;

            knife.Cost = 2500;
            knife.Bonus = 80;
            knife._info = "Нож(Да слабовато.Но при ударе со спины убивает на раз(Удача 80%)!!!)";

            return knife;
        }
    }
}
