using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Design;

namespace PokemonPocket
{

    class Program
    {
        public class MyDbContext : DbContext
{
    public DbSet<Pokemon> pokemonSet { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pokemon>()
            .HasKey(u => u.InstanceCount);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Server=localhost;Database=mydatabase;User=root;Password=mysql";
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}



        static List<PokemonMaster> pokemonMasters = new List<PokemonMaster>(){
            new PokemonMaster("Pikachu", 2, "Raichu"),
            new PokemonMaster("Eevee", 3, "Flareon"),
            new PokemonMaster("Charmander", 1, "Charmeleon")
            };

        static Dictionary<string, Type> pokemonTypes = new Dictionary<string, Type> {
            { "Pikachu", typeof(Pikachu) },
            { "Eevee", typeof(Eevee) },
            { "Charmander", typeof(Charmander) }
        };

        static void Main(string[] args)
        {
            //PokemonMaster list for checking pokemon evolution availability.


            //Use "Environment.Exit(0);" if you want to implement an exit of the console program
            //Start your assignment 1 requirements below.

            //initialsing some pokemon
            Db.Pocket.Add(new Eevee("eevee", 12, 13, false));
            Db.Pocket.Add(new Charmander("charmander", 122, 13, false));
            Db.Pocket.Add(new Pikachu("eevee", 54, 13, false));
            Db.Pocket.Add(new Eevee("eevee", 2, 13, false));
            Db.Pocket.Add(new Eevee("eevee", 1, 13, false));
            using (var context = new MyDbContext())
            {
                Eevee newPokemon = new Eevee("eevee", 12, 13, false);
                // Add the new user to the context
                context.pokemonSet.Add(newPokemon);

                // Save changes to persist the new user in the database
                context.SaveChanges();
            }

            /*using (var context = new MyDbContext())
            {
                var newPokemon = new
            {
                Name ="Pikachu",
                Hp = 1,
                Exp = 1,
                Evolved = false,
                Dmg = 1,
                Skill = "pokemon.Skill",
                Mult = 2,
            };
                // Add the new user to the context
                context.pokemonSet.Add(newPokemon);

                // Save changes to persist the new user in the database
                context.SaveChanges();
            }*/
            while (true)
            {

                Start();
            }


        }
        static void Start()
        {
            //test initial pokemon


            Console.WriteLine("*****************************");
            Console.WriteLine("Welcome to Pokemon Pocket App");
            Console.WriteLine("*****************************");
            Console.WriteLine("(1). Add pokemon to my pocket");
            Console.WriteLine("(2). List pokemon in my pocket");
            Console.WriteLine("(3). Check if I can evolve pokemon");
            Console.WriteLine("(4). Evolve pokemon");
            Console.WriteLine("(5). Battle!");
            Console.WriteLine("Please only enter [1,2,3,4] or Q to quit:");

            string option = Console.ReadLine();
            if (option == "Q" || option == "q")
            {
                Environment.Exit(0);
            }
            else
            {
                int int_option = Convert.ToInt32(option);
                if (int_option == 1)
                {
                    Add();
                }
                if (int_option == 2)
                {
                    List();
                }
                if (int_option == 3)
                {
                    Check();
                }
                if (int_option == 4)
                {
                    Evolve();
                }
                if (int_option == 5)
                {
                    Battle();
                }
            }
        }
        static void Add()
        {
            Console.WriteLine("Enter Pokemon's name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Pokemon's HP: ");
            string Hp = Console.ReadLine();
            Console.WriteLine("Enter Pokemon's Exp: ");
            string Exp = Console.ReadLine();
            string nameLower = name.ToLower();

            switch (nameLower)
            {
                case "pikachu":
                    Db.Pocket.Add(new Pikachu(name, int.Parse(Hp), int.Parse(Exp), false));
                    break;

                case "eevee":
                    Db.Pocket.Add(new Eevee(name, int.Parse(Hp), int.Parse(Exp), false));
                    break;

                case "charmander":
                    Db.Pocket.Add(new Charmander(name, int.Parse(Hp), int.Parse(Exp), false));
                    break;
                default:
                    Console.WriteLine("invalid input");
                    break;

            }

            foreach (Pokemon i in Db.Pocket)
            {
                i.display();
            }

        }
        static void List()
        {
            /*List<Pokemon> sorted = new List<Pokemon>(){};*/


            Db.Pocket.Sort((p1, p2) => p2.Hp.CompareTo(p1.Hp));

            foreach (Pokemon i in Db.Pocket)
            {
                Console.WriteLine("---------------------------");
                i.display();
                Console.WriteLine("---------------------------");
            }
        }


        static void Check()
        {
            // foreach (Pokemon i in Db.Pocket)
            // {string name = i.Name.ToLower();switch (name){case "pikachu":// only pikachu will do this
            // if(Pikachu.count >= Program.pokemonMasters[0].NoToEvolve){Console.WriteLine("Pikachu --> Raichu");
            // }break;case "eevee":// pikachu and eevee will do this
            //if(Eevee.count >= Program.pokemonMasters[1].NoToEvolve){
            //                 Console.WriteLine("Eevee --> Vaporeon");
            //             }
            //             break;
            //         case "charmander":
            //         if(Eevee.count >= Program.pokemonMasters[2].NoToEvolve){
            //                 Console.WriteLine("Charmander --> Charmeleon");
            //             }
            //             break;
            //         default:
            //             break;
            //     }
            // }
            foreach (PokemonMaster i in Program.pokemonMasters)
            {
                switch (i.Name)
                {
                    case "Pikachu":
                        if (Pikachu.count >= i.NoToEvolve)
                        {
                            Console.WriteLine("Pikachu --> Raichu");
                        }
                        break;
                    case "Eevee":
                        if (Eevee.count >= i.NoToEvolve)
                        {
                            Console.WriteLine("Eevee --> Vaporeon");
                        }
                        break;
                    case "Charmander":
                        if (Charmander.count >= i.NoToEvolve)
                        {
                            Console.WriteLine("Charmander --> Charmeleon");
                        }
                        break;
                }
            }


        }
        static Pokemon checkType(string toEvolve)
        {

            switch (toEvolve.ToLower())
            {
                case "pikachu":
                    Pikachu evolvedPikachu = new Pikachu(pokemonMasters[0].EvolveTo, 100, 0, true);
                    if (Pikachu.count <= pokemonMasters[0].NoToEvolve)
                    {
                        Console.WriteLine("Pikachu cannot be evolved");
                    }
                    return evolvedPikachu;
                case "eevee":

                    Eevee evolvedEevee = new Eevee(pokemonMasters[1].EvolveTo, 100, 0, true);
                    if (Eevee.count <= pokemonMasters[1].NoToEvolve)
                    {
                        Console.WriteLine("Eevee cannot be evolved");
                    }
                    return evolvedEevee;
                case "charmander":
                    Charmander evolvedCharmander = new Charmander(pokemonMasters[2].EvolveTo, 100, 0, true);
                    if (Charmander.count <= pokemonMasters[2].NoToEvolve)
                    {
                        Console.WriteLine("Charmander cannot be evolved");
                    }
                    return evolvedCharmander;
            }
            Console.WriteLine("Invalid input");
            return null;

        }
        static void Evolve()
        {
            /*evolving takes n number of pokemon of the same type, deletes 
            them, and then creates an evolved version with 100 hp.
            
            issue right now: switch block is executed regardless of what pokemon user wants to evlove.
            It should only check if the pokemon the user wants to evlove is avaiable, not every possible pokemon type.
            */

            Console.WriteLine("Enter Pokemon to Evolve: ");
            string toEvolve = Console.ReadLine();
            Pokemon evolved = checkType(toEvolve.ToLower());
            if (evolved == null) { return; }
            else
            {
                foreach (PokemonMaster i in Program.pokemonMasters)
                {
                    if (toEvolve.ToLower() == i.Name.ToLower())
                    {
                        int delete = 0;
                        for (int j = 0; j < Db.Pocket.Count(); j++)
                        {
                            if (Db.Pocket[j].Name.ToLower() == i.Name.ToLower() & Db.Pocket[j].Evolved == false & delete <= i.NoToEvolve)
                            {
                                Db.Pocket.RemoveAt(j);
                                delete++;
                                j--;
                            }
                        }
                        Db.Pocket.Add(evolved);
                    }
                }

            }
        }
        static void Battle()
        {
            Random r = new Random();
            int randType = r.Next(1, 3);
            int randHp = r.Next(1, 100);
            static Pokemon type(int randType, int randHp)
            {
                if (randType == 1)
                {
                    Pikachu Enemy = new Pikachu("enemy pika", randHp, 0, false);
                    return Enemy;
                }
                else if (randType == 2)
                {
                    Eevee Enemy = new Eevee("enemy Eevee", randHp, 0, false);
                    return Enemy;
                }
                else if (randType == 3)
                {
                    Charmander Enemy = new Charmander("enemy Charmander", randHp, 0, false);
                    return Enemy;
                }
                return null;
            }
            Pokemon Enemy = type(randType, randHp);

            Console.WriteLine(Enemy.Name);

            Console.WriteLine("Choose your pokemon!");
            List();
            Console.WriteLine("type the number of the pokemon you pick: ");
            string pick = Console.ReadLine();
            int intPick = Convert.ToInt32(pick);
            if (intPick > Db.Pocket.Count() || intPick < 0)
            {
                Console.WriteLine("invalid input");
                return;
            }
            Pokemon pokePick = Db.Pocket[intPick - 1];
            Console.WriteLine("You have picked " + pokePick.Name + ".");
            while (pokePick.Hp > 0 & Enemy.Hp > 0)
            {
                Console.WriteLine(pokePick.Name + " used " + pokePick.Skill + "!");
                int ogHp = Enemy.Hp;
                Enemy.calculateDmg(pokePick.Dmg);
                Console.WriteLine(Enemy.Name + "'s Hp has been reduced from " + ogHp + " to " + Enemy.Hp + ".");
                if (Enemy.Hp == 0)
                {
                    Console.WriteLine(Enemy.Name + "has fainted. You won!");
                    return;
                }

                Console.WriteLine(Enemy.Name + " used " + Enemy.Skill + "!");
                ogHp = pokePick.Hp;
                pokePick.calculateDmg(Enemy.Dmg);
                Console.WriteLine(pokePick.Name + "'s Hp has been reduced from " + ogHp + " to " + pokePick.Hp + ".");
                if (pokePick.Hp == 0)
                {
                    Console.WriteLine(pokePick.Name + " has fainted. You lose :(");
                    Console.WriteLine(pokePick.Name + "'s Hp has been restored. Better luck next time!");
                    return;
                }


            }
        }
    }
}