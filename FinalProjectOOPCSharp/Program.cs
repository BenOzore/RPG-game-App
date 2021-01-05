using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectOOPCSharp
{
    /*HERO CLASS*/
    class Hero
    {
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int OriginalHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int GamesWon { get; set; }
        public int GamesPlayed { get; set; }
        public List<Armor> ArmorsBag { get; set; }
        public List<Weapon> WeaponsBag { get; set; }
        public Weapon EquippedWeapon { get; set; }
        public Armor EquippedArmor { get; set; }

        public Hero()
        {
            this.ArmorsBag = new List<Armor>();
            this.WeaponsBag = new List<Weapon>();
            this.Strength = 20;
            this.Defense = 20;
            this.OriginalHealth = int.Parse(ConfigurationManager.AppSettings.Get("OriginalHealth"));
            this.CurrentHealth = 10;
            this.AddingEquippedWeapon("Knife", 5);
            this.AddingEquippedWeapon("Gun", 8);
            this.AddingEquippedArmor("ShieldArmor", 4);
            this.AddingEquippedArmor("IronArmor", 8);
        }
        public void ShowStats()
        {
            Console.WriteLine("--->Stats<---");
            Console.WriteLine("Name: {0}", Name);

            Console.WriteLine("{0} has Won {1} games out of {2} games played in total.", Name, GamesWon, GamesPlayed);

            Console.WriteLine("Strength: {0}", Strength);
            Console.WriteLine("Defense: {0}", Defense);
            Console.WriteLine("Hitpoints: CurrentHealth is {0} / Original Health is {1}", CurrentHealth, OriginalHealth);
        }

        public void ShowInventory()
        {
            Console.WriteLine("---->Inventory<----");
            Console.WriteLine("--->Weapons: ");
            foreach(var weapon in WeaponsBag)
            {
                Console.WriteLine("{0} with {1} Strength is available",weapon.Name, weapon.Power);
            }
            Console.WriteLine("--->Armor: ");
            foreach(var armor in ArmorsBag)
            {
                Console.WriteLine("{0} with {1} Strength is available", armor.Name, armor.Power);
            }
        }

        public void AddingEquippedWeapon(string name, int power)
        {
            var EquippedWeapon = new Weapon(name, power);
            this.WeaponsBag.Add(EquippedWeapon);           
        }

        public void AddingEquippedArmor(string name, int power)
        {
            var EquippedArmor = new Armor(name, power);
            this.ArmorsBag.Add(EquippedArmor);
        }
    }
    /*mONSTER CLASS*/
    class Monster
    {
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int OriginalHealth { get; set; }
        public int CurrentHealth { get; set; }

        public Monster() { }
        public Monster(string Name, int Strength, int Defense, int OriginalHealth, int CurrentHealth)
        {
            this.Name = Name;
            this.Strength = Strength;
            this.Defense = Defense;
            this.OriginalHealth = OriginalHealth;
            this.CurrentHealth = CurrentHealth;
        }
    }
    /*gAME CLASS*/
    class Game
    {
        public Hero Hero { get; set; }
        public Monster Monster { get; set; }

        public Game Games { get; set; }
        public Game()
        {
            this.Hero = new Hero();
        }

        public void Start()
        {
            Console.WriteLine("Hello and Welcome to my first RPG Hero game! :)");
            Console.WriteLine("Please enter your name: ");
            Hero.Name = Console.ReadLine();
            Console.WriteLine("Hello {0}, You are the new hero of today", Hero.Name);
            Main();
        }

        public void Main()
        {
            Console.WriteLine("Please select an option by entering a number from the following:");
            Console.WriteLine("1. Show Stats");
            Console.WriteLine("2. Check Inventory");
            Console.WriteLine("3. Start a Fight with a random Monster!!!");
            var userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    Stats();
                    break;
                case "2":
                    Inventory();
                    break;
                case "3":
                    Fight();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    this.Main();
                    break;
            }
        }
        public void Stats()
        {
            Hero.ShowStats();
            Console.WriteLine("Press any key to return to the main menu");
            Console.ReadKey();
            Console.ReadLine();
            Main();
        }
        public void Inventory()
        {
            Hero.ShowInventory();
            Console.WriteLine("Press any key to return to the main menu");
            Console.ReadKey();
            Console.ReadLine();
            Main();
        }
        public void Fight()
        {
            Fight fight = new Fight(this.Hero, this.Monster, this.Games);
            fight.Start();
        }
    }
    /*fIGHT CLASS*/
    class Fight
    {
        public Monster Monster { get; set; }
        public Game Game { get; set; }
        public Hero Hero { get; set; }
        public List<Monster> Monsters { get; set; }

        public Fight(Hero hero, Monster monster, Game game)
        {
            this.Game = new Game();
            this.Monsters = new List<Monster>();
            this.Monster = monster;
            this.Hero = hero;
            this.AddMonster("Dragon", 10, 5, 15, 14);
            this.AddMonster("Gorilla", 10, 8, 12, 5);
            this.AddMonster("Squid", 10, 15, 20, 7);
            this.AddMonster("Skeleton", 15, 8, 15, 12);
        }

        public void AddMonster(string name, int strength, int defense, int originalHp, int curentHp )
        {
            var monster = new Monster();
            monster.Name = name;
            monster.Strength = strength;
            monster.Defense = defense;
            monster.OriginalHealth = originalHp;
            monster.CurrentHealth = curentHp;
            this.Monsters.Add(monster);
        }
        
        public void Start()
        {
            var random = new Random();
            var enemy = this.Monsters[random.Next(0, this.Monsters.Count)];
            Console.WriteLine("You've been selected to fight the {0} Monster", enemy.Name);
            Console.WriteLine("This monster has \r\n  strength: --> {0}\r\n  " +
                "defense: --> {1}\r\n  originalHealth: --> {2}\r\n CurrentHealth: -->{3}", 
                enemy.Strength, enemy.Defense, enemy.OriginalHealth, enemy.CurrentHealth);
            Console.WriteLine("Press 1 to Fight!");
            Console.WriteLine("Press 2 to return to main menu");

            var userInput = Console.ReadLine();
            if(userInput == "1")
            {
                HeroTurn(enemy);
            }
            else if(userInput == "2")
            {
                Game.Main();
            }
        }

        public void HeroTurn(Monster monster)
        {            
            var enemy = monster;
            int damage;
            Console.WriteLine("Your turn...press enter to attack!");
            Console.ReadLine();

            if((Hero.Strength - enemy.Defense) >= 0)
            {
                damage = 1;
                enemy.CurrentHealth -= damage;      
            }
            else
            {
                damage = Hero.Strength - enemy.Defense;
                enemy.CurrentHealth -= damage;
            }
            Console.WriteLine("You did {0} damage to {1} monster!", damage, enemy.Name);
            Console.WriteLine("Your current health is {0}", Hero.CurrentHealth);
            Console.WriteLine("Enemies current health is {0}", enemy.CurrentHealth);
            if (enemy.CurrentHealth <= 0)
            {
                Win();
            }
            else
            {
                MonsterTurn(enemy);
            }
        }

        public void MonsterTurn(Monster monster)
        {
            Console.WriteLine("Monster's turn...press enter to attack!");
            Console.ReadLine();

            var enemy = monster;
            int damage;
            if ((enemy.Strength - Hero.Defense) <= 0)
            {
                damage = 1;
                Hero.CurrentHealth -= damage;
            }
            else
            {
                damage = enemy.Strength - Hero.Defense;
                Hero.CurrentHealth -= damage;
            }
            Console.WriteLine("the {0} monster did {1} damage to you!", enemy.Name, damage);
            Console.WriteLine("Your current health is {0}", Hero.CurrentHealth);
            Console.WriteLine("Enemies current health is {0}", enemy.CurrentHealth);
            if (Hero.CurrentHealth <= 0)
            {
                Lose();
            }
            else
            {
                HeroTurn(enemy);
            }
            Hero.GamesPlayed++;
        }

        public void Win()
        {
            
            Console.WriteLine("{0} Wins!!! You have successfully defeated the Monster", Hero.Name);
            Hero.GamesWon++;
            Console.WriteLine("Press any key and hit Enter to return to main menu");
            Console.ReadLine();
            Game.Main();
        }
        public void Lose()
        {
            Console.WriteLine("Unfortunately, our Hero {0} has been defeated by the monster. GAME OVER!!", Hero.Name);
            Console.WriteLine("Press any key and hit Enter to return to main menu");
            Console.ReadLine();
            Game.Main();
        }      
    }
    /*wEAPON CLASS*/
    class Weapon
    {
        public string Name { get; set; }
        public int Power { get; set; }

        public Weapon(string Name, int Power)
        {
            this.Name = Name;
            this.Power = Power;
        }
    }
    /*aRMOR CLASS*/
    class Armor
    {
        public string Name { get; set; }
        public int Power { get; set; }
        public Armor(string Name, int Power)
        {
            this.Name = Name;
            this.Power = Power;
        }
    }

    /*pROGRAM CLASS*/
    class Program
    {
        static void Main(string[] args)
        {

            Game game = new Game();
            game.Start();
            Console.ReadKey();
        }
    }
}
