using System;

namespace RetrospectiveGame
{
    public interface IDiceRoller
    {
        int RollDice();
        int RollDice(int min, int max);
    }
    public class DiceRoller : IDiceRoller
    {
        private readonly Random randGenerator = new Random();
        public int RollDice()
        {
            return randGenerator.Next(1, 20);
        }

        public int RollDice(int min, int max)
        {
            return randGenerator.Next(8, 20);
        }
    }
}