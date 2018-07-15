using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrospectiveGame
{
    public interface ICombatHandler
    {
        IDiceRoller DiceRoller { get; set; }
        string LastAttackStatus { get; set; }

        int GetAttackRoundDamage(ICharacter attacker, ICharacter defender);
        string CheckIfAttackSuccessful(ICharacter attacker, ICharacter defender);
        bool AreCharactersAlive(ICharacter char1, ICharacter char2);
    }

    public class CombatHandler : ICombatHandler
    {
        private const int maxValue = 20;
        public IDiceRoller DiceRoller { get; set; }
        public string LastAttackStatus { get; set; }

        public CombatHandler()
        {
            DiceRoller = RetrospectiveGame.DiceRoller.Instance;
        }

        public int GetAttackRoundDamage(ICharacter attacker, ICharacter defender)
        {
            LastAttackStatus = CheckIfAttackSuccessful(attacker, defender);
            var attackDamage = CalculateAttackDamage(LastAttackStatus);
            return attackDamage;
        }

        public string CheckIfAttackSuccessful(ICharacter attacker, ICharacter defender)
        {
            var attackSuccessfullness = "Miss!";
            var attackBonus = (attacker.Strength - 10) / 2;
            var modifier = attacker.Modifier;
            var diceRoll = DiceRoller.RollDice();
            int totalAttackingPower = GetTotalAttackingPower(attackBonus, modifier, diceRoll);

            if (diceRoll == maxValue)
            {
                attackSuccessfullness = CheckForCritical(defender.Constitution, attackBonus, modifier);
            }
            else if (diceRoll > defender.Constitution)
            {
                attackSuccessfullness = "Hit!";
            }
            return attackSuccessfullness;
        }

        public bool AreCharactersAlive(ICharacter char1, ICharacter char2)
        {
            return (char1.Life > 0 && char2.Life > 0);            
        }

        private string CheckForCritical(int defense, int attackBonus, int modifier)
        {
            var diceRollForCritical = DiceRoller.RollDice();
            var criticalTestValue = GetTotalAttackingPower(attackBonus, modifier, diceRollForCritical);
            if (criticalTestValue > defense)            
                return "Critical!";            
            return "Hit!";
        }

        private int GetTotalAttackingPower(int attackBonus, int modifier, int diceRoll)
        {
            var totalAttackingPower = attackBonus + modifier + diceRoll;
            return totalAttackingPower;
        }

        private int CalculateAttackDamage(string attackStatus)
        {
            switch (attackStatus)
            {
                case "Miss!":
                    return 0;
                case "Hit!":
                    return DiceRoller.RollDice(4, 10);
                case "Critical!":
                    return 15;
                default:
                    return 0;
            }
        }
    }
}
