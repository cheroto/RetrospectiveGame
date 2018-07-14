using System;
using NUnit.Framework;
using RetrospectiveGame;
using Moq;

namespace RetrospectiveGameTests
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
            _char1 = new Character("Bill");
            _char2 = new Character("Joe");
            mockDice = new Mock<IDiceRoller>();

            mockDice.Setup(dice => dice.RollDice(8, 20)).Returns(10);
            mockDice.Setup(dice => dice.RollDice()).Returns(12);
            _char1.DiceRoller = mockDice.Object;
        }
        [Test]
        public void TestSetNewStats()
        {
            //Arrange
            var expectedResult = 10;

            //Act
            _char1.SetNewStats();
            var actualResult = _char1.Strength;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
