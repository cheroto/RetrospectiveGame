using System;

namespace RetrospectiveGame.Logic
{
    public interface IDiceRoller
    {
        int RollDice();
        int RollDice(int min, int max);
    }
    public class DiceRoller : IDiceRoller
    {
        private readonly Random randGenerator;

        public DiceRoller()
        {
            randGenerator =  new Random();
        }

        public int RollDice()
        {
            return randGenerator.Next(1, 21);
        }

        public int RollDice(int min, int max)
        {
            return randGenerator.Next(min, max);
        }
    }
}