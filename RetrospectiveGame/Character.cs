using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrospectiveGame
{
    public interface ICharacter
    {
        int Constitution { get; set; }
        IDiceRoller DiceRoller { get; set; }
        int Life { get; set; }
        int Modifier { get; set; }
        string Name { get; set; }
        int Strength { get; set; }

        void SetNewStats();
        void TakeDamage(int attackDamage);
    }

    public class Character : ICharacter
    {
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Constitution { get; set; }
        public int Life { get; set; }
        public int Modifier { get; set; }
        public IDiceRoller DiceRoller { get; set; }

        private readonly int minAttributes = 8;
        private readonly int maxAtrributes = 20;

        public Character(string name)
        {
            Name = name;
            DiceRoller = new DiceRoller();
            SetNewStats();
        }

        public void SetNewStats()
        {
            Strength = DiceRoller.RollDice(minAttributes, maxAtrributes);
            Constitution = DiceRoller.RollDice(minAttributes, maxAtrributes);
            Life = Constitution * 2;
            Modifier = 0;
        }

        public void TakeDamage(int hitPoints)
        {
            Life = Life - hitPoints;
        }
    }
}
