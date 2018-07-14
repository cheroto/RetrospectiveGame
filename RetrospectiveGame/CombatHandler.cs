using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrospectiveGame
{
    public interface ICombatHandler
    {
        void AttackRound(ICharacter attacker, ICharacter defender);
        string CheckIfAttackSuccessful(ICharacter attacker, ICharacter defender);
    }

    public class CombatHandler : ICombatHandler
    {
        private const int maxValue = 20;
        private readonly IDiceRoller diceRoller = new DiceRoller();
        public void AttackRound(ICharacter attacker, ICharacter defender)
        {
            var attackStatus = CheckIfAttackSuccessful(attacker, defender);
            var attackDamage = CalculateAttackDamage(attackStatus);
            defender.TakeDamage(attackDamage);
        }

        public string CheckIfAttackSuccessful(ICharacter attacker, ICharacter defender)
        {
            var attackSuccessfullness = "Miss!";
            var attackBonus = (attacker.Strength - 10) / 2;
            var modifier = attacker.Modifier;
            var diceRoll = diceRoller.RollDice();
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

        private string CheckForCritical(int defense, int attackBonus, int modifier)
        {
            var diceRollForCritical = diceRoller.RollDice();
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
            return diceRoller.RollDice(4, 10);
        }
    }
}
