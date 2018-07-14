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

        [SetUp]
        public void SetUp()
        {
            _mockChar1 = new Mock<ICharacter>();
            _mockChar2 = new Mock<ICharacter>();
            combatHandler = new CombatHandler();
        }
    }
}
