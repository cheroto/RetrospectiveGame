using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using RetrospectiveGame;

namespace RetrospectiveGame.Logic.Tests
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
            combatHandler = new CombatHandler(_mockDice.Object);

            _mockChar1.Setup(c => c.Strength).Returns(16);
            _mockChar2.Setup(c => c.Constitution).Returns(14);

        }

        [TestCase(16, "Hit!")]
        [TestCase(20, "Critical!")]
        [TestCase(4, "Miss!")]
        public void CheckIfAttackSuccessful_Success(int diceRoll, string expectedResult)
        {
            //Arrange
            _mockDice.Setup(dice => dice.RollDice()).Returns(diceRoll);

            //Act
            var actualResult = combatHandler.CheckIfAttackSuccessful(_mockChar1.Object, _mockChar2.Object);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void AttackRoundDamage_Success()
        {
            //Arrange
            _mockDice.Setup(dice => dice.RollDice()).Returns(16);
            _mockDice.Setup(dice => dice.RollDice(4, 11)).Returns(8);
            var expectedResult = 8;

            //Act
            var actualResult = combatHandler.GetAttackRoundDamage(_mockChar1.Object, _mockChar2.Object);
            
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}
