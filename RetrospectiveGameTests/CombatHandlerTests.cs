using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using RetrospectiveGame;

namespace RetrospectiveGameTests
{
    [TestFixture]
    class CombatHandlerTests
    {
        Mock<ICharacter> _mockChar1;
        Mock<ICharacter> _mockChar2;
        ICombatHandler combatHandler;
        Mock<IDiceRoller> _mockDice;

        [SetUp]
        public void SetUp()
        {
            _mockChar1 = new Mock<ICharacter>();
            _mockChar2 = new Mock<ICharacter>();
            _mockDice = new Mock<IDiceRoller>();
            combatHandler = new CombatHandler
            {
                DiceRoller = _mockDice.Object
            };
            _mockChar1.Setup(c => c.Strength).Returns(16);
            _mockChar2.Setup(c => c.Constitution).Returns(14);
        }

        [TestCase(16, "Hit!")]
        [TestCase(20, "Critical!")]
        [TestCase(4, "Miss!")]
        public void CheckIfAttackSUccessful_Success(int diceRoll, string expectedResult)
        {
            //Arrange
            _mockDice.Setup(dice => dice.RollDice()).Returns(diceRoll);

            //Act
            var actualResult = combatHandler.CheckIfAttackSuccessful(_mockChar1.Object, _mockChar2.Object);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }


    }
}
