using System;
using NUnit.Framework;
using RetrospectiveGame;
using Moq;

namespace RetrospectiveGame.Logic.Tests
{
    [TestFixture]
    public class CharacterTests
    {
        public ICharacter _char1;
        public ICharacter _char2;

        Mock<IDiceRoller> mockDice;

        [SetUp]
        public void SetUp()
        {
            mockDice = new Mock<IDiceRoller>();

            mockDice.Setup(dice => dice.RollDice(8, 21)).Returns(10);
            mockDice.Setup(dice => dice.RollDice()).Returns(12);

            _char1 = new Character("Bill", mockDice.Object);
            _char2 = new Character("Joe", mockDice.Object);

        }
        [Test]
        public void SetNewStats_Success()
        {
            //Arrange
            var expectedResult = 10;

            //Act
            _char1.SetNewStats();
            var actualResult = _char1.Strength;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TakeDamage_Success()
        {
            //Arrange
            var expectedResult = 15;
            _char1.SetNewStats();

            //Act
            _char1.TakeDamage(5);
            var actualResult = _char1.Life;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);      
        }
    }
}
