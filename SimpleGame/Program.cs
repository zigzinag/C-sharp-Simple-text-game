using System;
using System.Linq;
using System.Threading;

namespace SimpleGame
{
    class ChoosenName
    {
        public string Name;
        public ChoosenName(string nm)
        {
            if (nm == "")
            {
                Name = "Hero of Kvatch";
                Console.WriteLine(Name);
            }
            else
            {
                Name = char.ToUpper(nm[0]) + nm.Substring(1);
            }
            Console.WriteLine("You are {0}, ok. \nYou in the strange house and you need to go out!", Name);
            Console.WriteLine();
        }
    }
    class Person
    {
        public virtual int Health { get; set; }
        public virtual int Damage { get; set; }
        public string Name { get; set; }
    }

    class Warrior : Person
    {
        public string nm;
        public Warrior(string nm)
        {
            Name = nm;
            Health = 100;
            Damage = 25;
        }

    }
    class Mage : Person
    {
        public string nm;
        public Mage(string nm)
        {
            Name = nm;
            Health = 80;
            Damage = 30;
        }

    }

    class Skeleton
    {
        public string Name = "Skeleton";
        public double Health = 50.0;
        public double Damage = 10.0;
        ~Skeleton() { }
    }
    class Zombie
    {
        public string Name = "Zombie";
        public double Health = 55.0;
        public double Damage = 10.0;
        ~Zombie() { }
    }
    class Witch
    {
        public string Name = "Witch";
        public double Health = 60.0;
        public double Damage = 10.0;
        ~Witch() { }
    }

    class Battle : Person
    {
        public double return_health;

        public void Combat(double pl_dm, double en_dm, string name)
        {
            Console.WriteLine(
                "Your attack was {0}, {1} attack was {2}",
                pl_dm,
                name,
                en_dm);
        }
        public Battle(int rand_dmg, double enemy_hp, double player_hp, double player_damage, double enemy_damage, string name)
        {
            double battle_enemy_hp = enemy_hp;
            double battle_player_hp = player_hp;

            while (battle_enemy_hp > 0)
            {
                /* Here I create random damage for battle */
                Random randy = new Random();
                double randy_1 = 0;
                double randy_2 = 0;
                while (randy_1 == randy_2)
                {
                    randy_1 = Convert.ToDouble(randy.Next(rand_dmg));
                    randy_2 = Convert.ToDouble(randy.Next(rand_dmg));

                }
                randy = null;

                double pl_dm = player_damage + randy_1;

                double en_dm = enemy_damage + randy_2;

                battle_enemy_hp = battle_enemy_hp - pl_dm;
                battle_player_hp = battle_player_hp - en_dm;
                Combat(pl_dm, en_dm, name);

                return_health = battle_player_hp;
            }

        }
    }
    class ChoosenClass
    {
        public string class_of_person;
        public string name;
        public double player_health;
        public double player_damage;
        public ChoosenClass()
        {
            Console.WriteLine("Choose your name:");
            ChoosenName name_of_player = new ChoosenName(Console.ReadLine());
            Console.WriteLine("Choose your class. Type \"Mage\" or \"Warrior\"");
            string class_of_person = Console.ReadLine().ToUpper();
            Console.WriteLine();
            if (class_of_person == "MAGE")
            {
                Mage gamer = new Mage(name_of_player.Name);
                name = name_of_player.Name;
                player_health = gamer.Health;
                player_damage = gamer.Damage;
            }
            else if (class_of_person == "WARRIOR")
            {
                Warrior gamer = new Warrior(name_of_player.Name);
                name = name_of_player.Name;
                player_health = gamer.Health;
                player_damage = gamer.Damage;
            }
            else
            {
                Console.WriteLine("I don't understand you, I think you are warrior!");
                Warrior gamer = new Warrior(name_of_player.Name);
                name = name_of_player.Name;
                player_health = gamer.Health;
                player_damage = gamer.Damage;
            }
            Console.WriteLine();
        }
    }
    class HealthBar
    {
        private double max_hp_sign = 20.0;
        //Here, I made variable, that means maximum quantitiy of signs in health bar
        private double RelativeHp(double hp)
        {
            double hpi = hp / 100.0 * max_hp_sign;
            return hpi;
        }
        //Here, I find relative value of current health
        private double AntiHp(double hp)
        {
            double anti_hpi = max_hp_sign - RelativeHp(hp);
            return anti_hpi;
        }
        //I print health bar
        public HealthBar(double hp)
        {
            if (hp > 100)
            {
                hp = 100;
                PrintHealthBar(hp);
            }
            else
            {
                PrintHealthBar(hp);
            }
        }
        public void PrintHealthBar(double hp)
        {
            Console.WriteLine();
            Console.WriteLine("Level of your health:");
            Console.WriteLine("|" +
                string.Concat(Enumerable.Repeat("=", Convert.ToInt32(RelativeHp(hp)))) +
                string.Concat(Enumerable.Repeat(" ", Convert.ToInt32(AntiHp(hp)))) +
                "|");
        }
        // And now, I desctruct class
        ~HealthBar() { }
    }
    class Room
    {

        public double health;
        public double damage;

        public Room(double input_health, double input_damage)
        {
            health = input_health;
            damage = input_damage;
        }
        static int index = 0;

        public double FindHealth()
        {
            Random randy = new Random();
            int stuff = randy.Next(101);
            if (stuff < 10)
            {
                Console.WriteLine("You find small health poison. Your health now increased on 10 point.");
                Console.WriteLine();
                return health += 10;
            }
            else if (stuff < 20)
            {
                Console.WriteLine("You find medium health poison. Your health now increased on 20 point.");
                Console.WriteLine();
                return health += 20;
            }
            else if (stuff < 20)
            {
                Console.WriteLine("You find big health poison. Your health now increased on 40 point.");
                Console.WriteLine();
                return health += 40;
            }
            else
            {
                Console.WriteLine("Room is empty. But you have a rest and now you feel yourself more healthy.");
                Console.WriteLine();
                index += 1;
                return health += 5;
            }
        }
        public double FindDamage()
        {
            Random randy = new Random();
            int stuff = randy.Next(101);
            if ((stuff > 10 && stuff < 20) && index != 1)
            {
                Console.WriteLine("You find damage poison. Your damage now increased on 10 point.");
                Console.WriteLine();
                return damage += 10;
            }
            else { return damage; }
        }
        ~Room() { }
    }
    class WinningSituation
    {
        public string answer;
        public double health;
        public double damage;
        public WinningSituation(double hlth, double dmg)
        {
            health = hlth;
            damage = dmg;
        }
        public void AfterBattle()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("You win a battle. Are you want to find something in the room? \"Yes\" or \"Not\": ", Console.ForegroundColor);
            Console.ForegroundColor = ConsoleColor.Gray;
            string answer = Console.ReadLine().ToUpper();
            if (answer == "YES")
            {
                Room room = new Room(health, damage);
                health = room.FindHealth();
                damage = room.FindDamage();
                Console.WriteLine("You searched the whole room and you open the door to the next room.");
            }
            else { Console.WriteLine("You go to the another room!"); }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            ChoosenClass klass = new ChoosenClass();
            double changed_player_hp = klass.player_health;
            double changed_damage_hp = klass.player_damage;

            Console.WriteLine("You are starting fight now!");
            int rand_dmg = 9;

            Console.WriteLine("You entered first room. You see the very dangerous Skeleton");
            Thread.Sleep((int)(1000));

            Skeleton s = new Skeleton();
            Battle first_battle = new Battle(rand_dmg, s.Health, changed_player_hp, changed_damage_hp, s.Damage, s.Name);
            if (first_battle.return_health <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You died!", Console.ForegroundColor);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.WriteLine("You killed a {0}", s.Name);
                changed_player_hp = first_battle.return_health;
                HealthBar first = new HealthBar(changed_player_hp);
                WinningSituation first_room = new WinningSituation(changed_player_hp, changed_damage_hp);
                first_room.AfterBattle();
                changed_player_hp = first_room.health;
                changed_damage_hp = first_room.damage;
                Console.WriteLine();
                Console.Clear();

                Console.WriteLine("You entered second room. You see the very dangerous Zombie");
                Thread.Sleep((int)(1000));

                /* Here I start the second battle */
                Zombie z = new Zombie();
                Battle second_battle = new Battle(rand_dmg, z.Health, changed_player_hp, changed_damage_hp, z.Damage, z.Name);
                if (second_battle.return_health <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You died!", Console.ForegroundColor);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.WriteLine("You killed a {0}", z.Name);
                    changed_player_hp = second_battle.return_health;
                    HealthBar second = new HealthBar(changed_player_hp);
                    WinningSituation second_room = new WinningSituation(changed_player_hp, changed_damage_hp);
                    second_room.AfterBattle();
                    changed_player_hp = second_room.health;
                    changed_damage_hp = second_room.damage;
                    Console.WriteLine();
                    Console.Clear();

                    Console.WriteLine("You entered third room. You see the very dangerous Witch");
                    Thread.Sleep((int)(1000));

                    /* Here I start the third battle */
                    Witch w = new Witch();
                    Battle third_battle = new Battle(rand_dmg, w.Health, changed_player_hp, changed_damage_hp, w.Damage, w.Name);
                    if (third_battle.return_health <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You died!", Console.ForegroundColor);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.WriteLine("You killed a {0}", w.Name);
                        HealthBar third = new HealthBar(changed_player_hp);
                        WinningSituation third_room = new WinningSituation(changed_player_hp, changed_damage_hp);
                        third_room.AfterBattle();
                        changed_player_hp = third_room.health;
                        changed_damage_hp = third_room.damage;
                        HealthBar fourth = new HealthBar(changed_player_hp);
                        Console.Clear();

                        Console.WriteLine("You open last door.");
                        Thread.Sleep((int)(1000));
                        Console.WriteLine("You stand under the sky, you are tired, but your journey only beginning");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Press any key to quit...", Console.ForegroundColor);
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}
