using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFighter
{
    class Human
    {
        int attack = 10;
        int defense = 10;
        int maxHP;
        int curHP;


        public int Attack { get => attack; set => attack = value; }
        public int Defense { get => defense; set => defense = value; }
        public int MaxHP { get => maxHP; set => maxHP = value; }
        public int CurHP { get => curHP; set => curHP = value; }


        public void SetHp()
        {
            MaxHP = 2 * Attack + 5 * Defense;
            CurHP = MaxHP;
        }
        public void UpdateHP()
        {
            decimal CurHP_d;
            decimal MaxHP_d;
            MaxHP_d = MaxHP;
            CurHP_d = CurHP;
            decimal proz = CurHP_d / MaxHP_d;
            MaxHP= 2 * Attack + 5 * Defense;
            CurHP = (int)(MaxHP * proz);

        }
        public int HPBar()
        {
            double CurHP_d;
            double MaxHP_d;
            MaxHP_d = MaxHP;
            CurHP_d = CurHP;
            double proz = (CurHP_d / MaxHP_d);
            int pos= (int)Math.Floor(proz*100);
            return pos;
        }
        public int Hit(int curHP_d, int Att_a, int Def_d)
        {
            curHP_d = (int)(curHP_d - (2 * Att_a - 0.3 * Def_d));
            return curHP_d;
        }
        public int HeavyHit(int curHP_d, int Att_a, int Def_d)
        {
            curHP_d = (int)(curHP_d - (3 * Att_a - 0.1 * Def_d));
            
            return curHP_d;
        }

    }

    class Player : Human
    {
        int money;
        public int Money { get => money; set => money = value; }

        public Player(int attack, int defense, int money)
        {
            this.Attack = attack;
            this.Defense = defense;
            this.Money = money;
        }
        //public void PlayerStart()
        //{
            
        //}
        public void Healing()
        {
            int tempCurHP, tempMoney;
            tempCurHP = CurHP + (int)(MaxHP * 0.3);
            tempMoney = Money - (int)(MaxHP * 0.2);
            if (tempMoney > 0)
            {
                if (tempCurHP > MaxHP)
                {
                    CurHP = MaxHP;
                    Money = Money - (int)(MaxHP * 0.2);
                }
                else
                {
                    CurHP = CurHP + (int)(MaxHP * 0.3);
                    Money = Money - (int)(MaxHP * 0.2);
                }
            }
            else if (tempMoney == 0)
            {
                CurHP = CurHP + (int)(MaxHP * 0.2);
                Money = 0;
            }
            else
            {
                CurHP = CurHP + Money;
                Money = 0;
            }
        }
    }

    class Enemy : Human
    {
        string name;
        int level;
        int drop;
        public int Level { get => level; set => level = value; }
        public int Drop { get => drop; set => drop = value; }
        public string Name { get => name; set => name = value; }

        public Enemy(int attack, int defense, int level, int drop)
        {
            this.Attack = attack;
            this.Defense = defense;
            this.Level = level;
            this.Drop = drop;
        }


        public void SetName()
        {
            List<string> vornamen = new()
            {
                "Dietrich",
                "Frank",
                "Ilyass",
                "Alexander",
                "Georg",
                "Stephanus"
            };
            List<string> zunamen = new()
            {
                ", der Alte",
                ", der Gänse-Würger",
                ", der Welpen-Schmeißer",
                ", der SI'ler",
                ", der Warmduscher",
                ", der Bleiche",
                ", der Lord",
                ", der Pessimist"
            };
            Random rand = new Random();
            int index = rand.Next(0, (vornamen.Count));
            Name = vornamen[index];

            index = rand.Next(0, zunamen.Count );
            Name = Name + zunamen[index];
            switch (index)
            {
                case 0:
                    Attack = Attack - 5;
                    Defense = Defense - 2;
                    Drop = Drop + 50;
                    break;
                case 1:
                    Attack = Attack + 10;
                    Defense = Defense;
                    Drop = Drop;
                    break;
                case 2:
                    Attack = Attack + 7;
                    Defense = Defense + 2;
                    Drop = Drop + 30;
                    break;
                case 3:
                    Attack = Attack + 20;
                    Defense = Defense - 5;
                    Drop = Drop + 150;
                    break;
                case 4:
                    Attack = Attack;
                    Defense = Defense + 8;
                    Drop = Drop;
                    break;
                case 5:
                    Attack = Attack + 3;
                    Defense = Defense + 3;
                    Drop = Drop + 20;
                    break;
                case 6:
                    Attack = Attack;
                    Defense = Defense + 15;
                    Drop = Drop + 250;
                    break;
                case 7:
                    Attack = Attack - 2;
                    Defense = Defense - 2;
                    Drop = Drop - 20;
                    break;
                case 8:
                    Attack = Attack;
                    Defense = Defense;
                    Drop = Drop + 20;
                    break;
                default:
                    break;
            }
            //Console.WriteLine($"Der Gegner erscheint. Es ist {Name}.\n");
        }
        public void lvlUP() //Muss immer mit den KonstruktorDaten aktualisiert werden
        {
            Attack = 10 + Level;
            Defense = 10 + Level;
            Drop = 10 + 2 * Level;
            
        }
        public void EnemyTod()
        {
            //Console.WriteLine($"{Name} ist gefallen. Du findest {Drop} Goldmünzen.");
            Level++;
            lvlUP();
            SetName();
            SetHp();
            
        }

    }


}
