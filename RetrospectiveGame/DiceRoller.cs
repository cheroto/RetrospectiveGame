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

        private static readonly Lazy<DiceRoller> lazy =
        new Lazy<DiceRoller>(() => new DiceRoller());

        public static DiceRoller Instance { get { return lazy.Value; } }

        private DiceRoller()
        {
        }

        private readonly Random randGenerator = new Random();
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