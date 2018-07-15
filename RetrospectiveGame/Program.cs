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

            PlotIntroText();

            SetModifier(char1);
            SetModifier(char2);

            ShowStats(char1);
            ShowStats(char2);
            Console.ReadLine();

            DoBattle(char1, char2, combatHandler, roundNumber);

        }

        private static void SetModifier(Character character)
        {
            Console.WriteLine(string.Format("Please write modifier for Hero:", character.Name));
            character.Modifier = int.Parse(Console.ReadLine());
        }

        private static void PlotIntroText()
        {
            Console.WriteLine("***********************************");
            Console.WriteLine("******Welcome to RetroBattle!******");
            Console.WriteLine("***********************************");
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
        }

        private static void DoBattle(ICharacter char1, ICharacter char2, ICombatHandler combatHandler, int roundNumber)
        {
            while (combatHandler.AreCharactersAlive(char1, char2))
            {
                roundNumber += 1;
                BattleRound(char1, char2, combatHandler, roundNumber);
                Console.ReadLine();
            }
            if (char1.Life < 1) PlayHeroDeathRattle(char1);
            else PlayVillainDeathRattle(char2);
        }

        private static void PlayVillainDeathRattle(ICharacter character)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            System.Threading.Thread.Sleep(500);
            Console.WriteLine(string.Format("{0} constantly parries sword attacks but exhaustion is clearly taking it's toll.", character.Name));
            Console.WriteLine("With sluggish responses, he is finally unable to parry a strike aiming for the neck.");
            Console.WriteLine();
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine(string.Format("{0}'s body stumbles to the ground, head rolling on the opposite direction. " +
                "After a few last twiches, he is gone.", character.Name));
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine(YouWinString);
        }

        private static void PlayHeroDeathRattle(ICharacter character)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            System.Threading.Thread.Sleep(500);
            Console.WriteLine(string.Format("{0} valiantly fights despite numerous broken bones and cuts throughout his body.", character.Name));
            Console.WriteLine("A moment of distraction, however, leads to a sword through the heart.");
            Console.WriteLine();
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine(string.Format("{0} stumbles to the ground, coughs blood one last time and dies.", character.Name));
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine(GameOverString);
        }

        private static void BattleRound(ICharacter char1, ICharacter char2, ICombatHandler combatHandler, int roundNumber)
        {
            Console.WriteLine();
            Console.WriteLine(string.Format("Round {0}", roundNumber));
            Console.WriteLine();

            ShowLife(char1);
            ShowLife(char2);
            Console.WriteLine();

            AttackSequence(char1, char2, combatHandler);
            AttackSequence(char2, char1, combatHandler);
        }

        private static void ShowLife(ICharacter character)
        {
            Console.WriteLine(string.Format("{0} has a total of {1} life points.", character.Name, character.Life));
        }

        private static void AttackSequence(ICharacter attacker, ICharacter defender, ICombatHandler combatHandler)
        {
            var damageTaken = combatHandler.GetAttackRoundDamage(attacker, defender);
            defender.TakeDamage(damageTaken);

            Console.WriteLine(string.Format("{0} attacks!", attacker.Name));
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine(combatHandler.LastAttackStatus);
            if (damageTaken > 0) Console.WriteLine(string.Format("{0} has taken of {1} life points.", defender.Name, damageTaken));
            Console.WriteLine();
        }

        private const string GameOverString = @"  _______      ___      .___  ___.  _______      ______   ____    ____  _______ .______      
 /  _____|    /   \     |   \/   | |   ____|    /  __  \  \   \  /   / |   ____||   _  \     
|  |  __     /  ^  \    |  \  /  | |  |__      |  |  |  |  \   \/   /  |  |__   |  |_)  |    
|  | |_ |   /  /_\  \   |  |\/|  | |   __|     |  |  |  |   \      /   |   __|  |      /     
|  |__| |  /  _____  \  |  |  |  | |  |____    |  `--'  |    \    /    |  |____ |  |\  \----.
 \______| /__/     \__\ |__|  |__| |_______|    \______/      \__/     |_______|| _| `._____|
                                                                                             ";

        private const string YouWinString = @"____    ____  ______    __    __     ____    __    ____  __  .__   __.  __  
\   \  /   / /  __  \  |  |  |  |    \   \  /  \  /   / |  | |  \ |  | |  | 
 \   \/   / |  |  |  | |  |  |  |     \   \/    \/   /  |  | |   \|  | |  | 
  \_    _/  |  |  |  | |  |  |  |      \            /   |  | |  . `  | |  | 
    |  |    |  `--'  | |  `--'  |       \    /\    /    |  | |  |\   | |__| 
    |__|     \______/   \______/         \__/  \__/     |__| |__| \__| (__) 
                                                                            ";
    }
}
