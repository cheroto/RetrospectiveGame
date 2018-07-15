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
            var roundNumber = 0;


            Console.WriteLine("***********************************");
            Console.WriteLine("******Welcome to RetroBattle!******");
            Console.WriteLine("***********************************");

            ShowStats(char1);
            ShowStats(char2);

            DoBattle(char1, char2, combatHandler, roundNumber);

        }

        private static void ShowStats(Character character)
        {
            Console.WriteLine();
            Console.WriteLine(string.Format("Stats for {0}:", character.Name));
            Console.WriteLine();

            Console.WriteLine(string.Format("Strength: {0}:", character.Strength));
            Console.WriteLine(string.Format("Constitution: {0}:", character.Constitution));
            Console.WriteLine(string.Format("Modifier: {0}:", character.Modifier));
            Console.WriteLine(string.Format("Life: {0}:", character.Life));

            Console.WriteLine();
            Console.WriteLine("***********************************");
            Console.WriteLine();
            Console.ReadLine();
        }

        private static void DoBattle(ICharacter char1, ICharacter char2, ICombatHandler combatHandler, int roundNumber)
        {
            while (combatHandler.AreCharactersAlive(char1, char2))
            {
                roundNumber += 1;
                BattleRound(char1, char2, combatHandler, roundNumber);
                Console.ReadLine();
            }
            if (char1.Life < 1) PlayChar1DeathRattle(char1);
            else PlayChar2DeathRattle(char2);
        }

        private static void PlayChar2DeathRattle(ICharacter character)
        {
            Console.Clear();
            Console.WriteLine(string.Format("{0} valiantly fights despite numerous broken bones and cuts throughout his body.", character.Name));
            Console.WriteLine("A moment of distraction, however, leads to a sword through the heart.");
            Console.WriteLine();
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine(string.Format("{0} stumbles to the ground, coughs blood one last time and dies.", character.Name));
            Console.WriteLine(GameOverString);
        }

        private static void PlayChar1DeathRattle(ICharacter character)
        {
            Console.Clear();
            Console.WriteLine(string.Format("{0} valiantly fights despite numerous broken bones and cuts throughout his body.", character.Name));
            Console.WriteLine("A moment of distraction, however, leads to a sword through the heart.");
            Console.WriteLine();
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine(string.Format("{0} stumbles to the ground, coughs blood one last time and dies.", character.Name));
            Console.WriteLine(GameOverString);
        }

        private static void BattleRound(ICharacter char1, ICharacter char2, ICombatHandler combatHandler, int roundNumber)
        {
            Console.WriteLine();
            Console.WriteLine(string.Format("Round {0}", roundNumber));
            Console.WriteLine();

            Console.WriteLine(string.Format("{0} has a total of {1} life points.", char1.Name, char1.Life));
            Console.WriteLine(string.Format("{0} has a total of {1} life points.", char2.Name, char2.Life));
            Console.WriteLine();

            var char2DamageTaken = combatHandler.GetAttackRoundDamage(char1, char2);
            char2.TakeDamage(char2DamageTaken);

            Console.WriteLine(string.Format("{0} attacks!", char1.Name));
            System.Threading.Thread.Sleep(500);
            Console.WriteLine(combatHandler.LastAttackStatus);
            if (char2DamageTaken > 0) Console.WriteLine(string.Format("{0} has taken of {1} life points.", char2.Name, char2DamageTaken));
            Console.WriteLine();


            var char1DamageTaken = combatHandler.GetAttackRoundDamage(char2, char1);
            char1.TakeDamage(char1DamageTaken);

            Console.WriteLine(string.Format("{0} attacks!", char2.Name));
            System.Threading.Thread.Sleep(500);
            Console.WriteLine(combatHandler.LastAttackStatus);
            if (char1DamageTaken > 0 ) Console.WriteLine(string.Format("{0} has taken of {1} life points.", char1.Name, char1DamageTaken));
            Console.WriteLine();

        }

        private const string GameOverString = @"  _______      ___      .___  ___.  _______      ______   ____    ____  _______ .______      
 /  _____|    /   \     |   \/   | |   ____|    /  __  \  \   \  /   / |   ____||   _  \     
|  |  __     /  ^  \    |  \  /  | |  |__      |  |  |  |  \   \/   /  |  |__   |  |_)  |    
|  | |_ |   /  /_\  \   |  |\/|  | |   __|     |  |  |  |   \      /   |   __|  |      /     
|  |__| |  /  _____  \  |  |  |  | |  |____    |  `--'  |    \    /    |  |____ |  |\  \----.
 \______| /__/     \__\ |__|  |__| |_______|    \______/      \__/     |_______|| _| `._____|
                                                                                             ";
    }
}
