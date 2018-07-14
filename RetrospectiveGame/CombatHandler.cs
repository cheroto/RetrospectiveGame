using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrospectiveGame
{
    class CombatHandler
    {
        private readonly IDiceRoller diceRoller = new DiceRoller();
        public void AttackRound(ICharacter attacker, ICharacter defender)
        {
            var attackStatus = CheckIfAttackSuccessful(attacker, defender);
            var attackDamage = CalculateAttackDamage(attackStatus);
            defender.TakeDamage(attackDamage);
        }

        private string CheckIfAttackSuccessful(ICharacter attacker, ICharacter defender)
        {
            var attackBonus = (attacker.Strength - 10) / 2;
            var modifier = attacker.Modifier;
            var diceRoll = diceRoller.RollDice();
            int totalAttackingPower = GetTotalAttackingPower(attackBonus, modifier, diceRoll);

            return "";
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
