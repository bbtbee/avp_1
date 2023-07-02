using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PokemonPocket
{
    interface display
    {
        public void display();
    }

    public class PokemonMaster
    {
        public string Name { get; set; }
        public int NoToEvolve { get; set; }
        public string EvolveTo { get; set; }



        public PokemonMaster(string name, int noToEvolve, string evolveTo)
        {
            this.Name = name;
            this.NoToEvolve = noToEvolve;
            this.EvolveTo = evolveTo;
        }
    }
    public class Pokemon : display
    {
        public static int count { get; set; } = 0;
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Exp { get; set; }
        public bool Evolved { get; set; }
        public string Skill { get; set; }
        public int Dmg { get; set; }
        public int Mult { get; set; }
        public int InstanceCount { get; internal set; } = count;

        public Pokemon(string name, int hp, int exp, bool Evolved)
        {
            this.Name = name;
            this.Hp = hp;
            this.Exp = exp;
            this.Evolved = Evolved;
            count++;
        }

        public void calculateDmg(int dmg)
        {
            this.Hp = this.Hp - this.Mult * dmg;
            if (this.Hp < 0) { this.Hp = 0; }
        }

        public void display()
        {
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Hp: " + Hp);
            Console.WriteLine("Exp: " + Exp);
            Console.WriteLine("Skill: " + Skill);
        }
    }

    public class Pikachu : Pokemon
    {
        public static new int count { get; private set; } = 0;
        public Pikachu(string name, int hp, int exp, bool evolved) :
            base(name, hp, exp, evolved)
        {
            Skill = "Thunderbolt";
            Dmg = 25;
            Mult = 1;
            count++;
        }
    }

    public class Eevee : Pokemon
    {
        public static new int count { get; private set; } = 0;
        public Eevee(string name, int hp, int exp, bool evolved) :
            base(name, hp, exp, evolved)
        {
            Skill = "Run Away";
            Dmg = 20;
            Mult = 2;
            count++;
        }
    }

    public class Charmander : Pokemon
    {
        public static new int count { get; private set; } = 0;
        public Charmander(string name, int hp, int exp, bool evolved) :
            base(name, hp, exp, evolved)
        {
            Skill = "Solar Power";
            Dmg = 15;
            Mult = 3;
            count++;
        }
    }
    public class Db
    {
        public static List<Pokemon> Pocket = new List<Pokemon>()
        {

        };

    }
    public class MyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost;Database=mydatabase;User=root;Password=mypassword";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}