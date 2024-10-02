using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster
{
    enum MonsterType
    {
        Dracon = 1,
        Orc,
        Undead,
        Thief,
        Mutant,
        Animal,
        Elf,
        People
    }
    class Personage
    {
        //Методы:
        //int GetAttack(int bonus); (получить точное значение силы атаки в диапазоне[MinAttack; MaxAttack] + bonus)
        //* void Wounds(int damage); (вычитает damage из его HP; при этом HP не может уйти в минус!!!)
       
        public MonsterType _monster;

        public int Hp = 100; //(текущее здоровье. Проверка: 0 - 100, но не более MaxHP)

        private int MaxHp = 100; //(полное здоровье. Проверка: 0 - 500)

        public int MinHp = 0; // (поверка здоровья, если меньше 0 )

        public int MinAttack = 1; //(минимальная сила атаки.Проверка:  1-10)

        public int MaxAttack = 10;  //(максимальная атаки. Проверка:  20-100)

        public int Armor = 0;

        public int Cash = 5000;

        public string WarCry;//боевой клич

        public string DeadCry; // крик смерти

        public string Name; // Ник нейм персонажа

        public bool ActiveStart;

        public bool Active = true;

        public int Number;

        public Inventar inventar;

        public List<Item> item = new List<Item>();

        public Personage() { }
        public Personage(string name, MonsterType monster)
            : this()
        {
            Name = name;
            _monster = monster;
            
        }
        public Personage(string name, MonsterType monster, int cash, int hp, int max_hp, int min_attack, int max_attack, string war_cry, string die_cry, int armor)
            : this(name, monster)
        {
            Cash = cash;
            Hp = hp;
            MaxHp = max_hp;
            MinAttack = min_attack;
            MaxAttack = max_attack;
            WarCry = war_cry;
            DeadCry = die_cry;
            Armor = armor;

        }

        public string MonsterText
        {
            get
            {
                return (_monster == MonsterType.Dracon ? "Дракон" : (_monster == MonsterType.Orc ? "Орк"
                    : (_monster == MonsterType.Thief ? "Вор" : (_monster == MonsterType.Mutant ? "Мутант"
                   : (_monster == MonsterType.Animal ? "Животное" : (_monster == MonsterType.Undead ? "Нежить"
                   : (_monster == MonsterType.Elf ? "Эльф" : "Человек")))))));
            }

        }

        public Item GetUseItemAtPers(Personage pers)
        {
            Item tmp = new Item();
            if(pers.item.Capacity != 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (pers.item[i].Use == true && pers.item[i]._damage != 0)
                    {
                        return pers.item[i];

                    }
                }
            
                             
            }
            else
            {
                return null;
            }
            return tmp;
           
        }
        public string IsDie     //(монстр не мертв, если его HP > 0)
        {
            get { return DeadCry = "Aaaa, нет, я..... У М Е Р А Ю!!!"; }
        }

        public string GetInfo   //* (развернутая строка с описанием монстра(все основные поля, кроме строк его криков, в понятном пользователю текстовом виде)).
        {
            get
            {
                return ($"Имя персонажа:{Name},\nРасса:{_monster},\nКооличество здоровья:{Hp},\nДеньги:{Cash}.");
            }
        }

        public  int GetAttack(int MinAttack, int MaxAttack, Item item)
        {                                    //(получить точное значение силы атаки в диапазоне[MinAttack; MaxAttack] + bonus)
            int attack = 0;

            Random rnd = new Random();

            int shec = rnd.Next(1,3);

            if (shec == 2) attack = MinAttack + MaxAttack;

            else if (shec == 1) attack = MinAttack;

            else if (shec == 3) attack = 0; // промах

            if(item != null)
            {
                if (item.Bonus != 0)
                {
                    if (shec == 2) attack = MaxAttack;

                    else if (shec == 1) attack = MinAttack;

                    else if (shec == 3) attack = MaxAttack + item.Bonus; // критический удар
                }
            }
            

            return attack ;
        }

        public string Wounds(ref Personage pers, int MinAttack, int MaxAttack, Item item) // Последствия удара
        {
            if(pers.Armor != 0 ) pers.Hp = (pers.Hp + pers.Armor) - GetAttack(MinAttack, MaxAttack, item);

            if (pers.Hp <= MinHp) return IsDie;

            else if (pers.Hp > MaxHp / 2) return "Фуф легко отделался!";

            else return "Не хило потрепало!!!";
        }
    }
}
