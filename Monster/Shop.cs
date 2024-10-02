using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Monster
{
    
    class Shop
    {
        //Методы в магазине проверяют на прямую наличие денег у персонажа(пока костыльно) и если есть то в 
        // методе создает предмет и через ссылку ref отдает персу.И вычетает cash.
        
        public static string GetBottle(Personage pers)
        {

            if (pers.Cash >= 500)
            {
                Item bottle = RealItems.CreateBottle();//создаем нудный предмет
                pers.Cash = pers.Cash - 500;

                pers.item.Add(bottle); //добовляем прелмет в инвентарь

                return "Склянка здоровья преобретена";

            }
            else return "Недостаточно золота";
        }

        public static string GetSword(Personage pers)
        {

            if (pers.Cash >= 5000)
            {
                Item sword = RealItems.CreateSword();//создаем нудный предмет
                pers.Cash = pers.Cash - 5000;

                pers.item.Add(sword); //добовляем прелмет в инвентарь

                return "Меч преобретен";

            }
            else return "Недостаточно золота";
        }

        public static string GetOnion(Personage pers)
        {          
            if (pers.Cash >= 3500)
            {
  
                Item onion = RealItems.CreateOnion();//создаем нудный предмет
                pers.Cash = pers.Cash - 3500;

                pers.item.Add(onion); //добовляем предмет в инвентарь

                return "Лук преобретен";
            }
            else return "Недостаточно золота";
        }

        public static string GetShield(Personage pers)
        {

            if (pers.Cash >= 1500)
            {
                Item shield = RealItems.CreateShield();
                pers.Cash = pers.Cash - 1500;

                pers.item.Add(shield); ; //добовляем прелмет в инвентарь

                return "Щит преобретен";
            }
            else return "Недостаточно золота";
        }

        public static string GetHelmet(Personage pers)
        {

            if (pers.Cash >= 1200)
            {
                Item helmet = RealItems.CreateHelmet();
                pers.Cash = pers.Cash - 1200;

                pers.item.Add(helmet); //добовляем прелмет в инвентарь

                return "Шлем преобретен";
            }
            else return "Недостаточно золота";
        }

        public static string GetArmor(Personage pers)
        {


            if (pers.Cash >= 2000)
            {
                Item armor = RealItems.CreateArmor();
                pers.Cash = pers.Cash - 2000;

                pers.item.Add(armor); //добовляем прелмет в инвентарь

                return "Доспехи преобретены";
            }
            else return "Недостаточно золота";
        }

        public static string GetClub(Personage pers)
        {

            if (pers.Cash >= 2500)
            {
                Item club = RealItems.CreateСlub();
                pers.Cash = pers.Cash - 2500;

                pers.item.Add(club); //добовляем прелмет в инвентарь

                return "Дубина переобретена";
            }
            else return "Недостаточно золота";
        }

        public static string GetKnife(Personage pers)
        {

            if (pers.Cash >= 2000)
            {
                Item knife = RealItems.CreateKnife();
                pers.Cash = pers.Cash - 2000;

                pers.item.Add(knife); //добовляем прелмет в инвентарь

                return "Нож преобретен";
            }
            else return "Недостаточно золота";
        }

      

    }
}
