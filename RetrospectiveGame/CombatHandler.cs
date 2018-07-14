using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrospectiveGame
{
    class CombatHandler
    {
        private const int maxValue = 20;
        private readonly IDiceRoller diceRoller = new DiceRoller();
        public void AttackRound(ICharacter attacker, ICharacter defender)
        {
            var attackStatus = CheckIfAttackSuccessful(attacker, defender);
            var attackDamage = CalculateAttackDamage(attackStatus);
            defender.TakeDamage(attackDamage);
        }

        private string CheckIfAttackSuccessful(ICharacter attacker, ICharacter defender)
        {
            var attackSuccessfullness = "Miss!";
            var attackBonus = (attacker.Strength - 10) / 2;
            var modifier = attacker.Modifier;
            var diceRoll = diceRoller.RollDice();
            int totalAttackingPower = GetTotalAttackingPower(attackBonus, modifier, diceRoll);

            if (diceRoll == maxValue)
            {
                attackSuccessfullness = CheckForCritical(defender.Constitution, attackSuccessfullness, attackBonus, modifier);
            }
            else if (diceRoll > defender.Constitution)
            {
                attackSuccessfullness = "Hit!";
            }
            return attackSuccessfullness;
        }

        private string CheckForCritical(int defense, string attackSuccessfullness, int attackBonus, int modifier)
        {
            var diceRollForCritical = diceRoller.RollDice();
            var criticalTestValue = GetTotalAttackingPower(attackBonus, modifier, diceRollForCritical);
            if (criticalTestValue > defense)            
                attackSuccessfullness = "Critical!";            
            else
                attackSuccessfullness = "Hit!";
            return attackSuccessfullness;
        }

        private int GetTotalAttackingPower(int attackBonus, int modifier, int diceRoll)
        {
            throw new NotImplementedException();
        }

        private int CalculateAttackDamage(string attackStatus)
        {
            throw new NotImplementedException();
        }
    }
}
