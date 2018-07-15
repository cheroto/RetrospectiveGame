using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrospectiveGame
{
    class Program
    {
        static void Main(string[] args)
        {

            var char1 = new Character("Lightning Bolts");
            var char2 = new Character("Evil Sprint Destroyer");
            var combatHandler = new CombatHandler();

            Console.WriteLine("*****************************");
            Console.WriteLine("***Welcome to RetroBattle!***");
            Console.WriteLine("*****************************");

            Console.WriteLine(string.Format("{0} has a total of {1} life points.", char1.Name, char1.Life));
            Console.WriteLine(string.Format("{0} has a total of {1} life points.", char2.Name, char2.Life));


            Console.WriteLine("Round 1");
            var char2DamageTaken = combatHandler.GetAttackRoundDamage(char1, char2);
            char2.TakeDamage(char2DamageTaken);
            var char1DamageTaken = combatHandler.GetAttackRoundDamage(char2, char1);
            char1.TakeDamage(char1DamageTaken);
            



            Console.ReadLine();
        }
    }
}
