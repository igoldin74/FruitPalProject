using FruitPal;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitPalTests.tests
{
    [TestFixture]
    class CostCalcFactoryTests
    {
        private readonly Mock<ILogger> _loggerMock = new Mock<ILogger>();

        [Test]
        public void CostCalc_Object_Should_Not_Be_Null()
        {
            var consoleCommand = new ConsoleCommand("fruitpal mango 53 405");
            
            var calculator1 = new CostCalcFactory(_loggerMock.Object).Create(consoleCommand);

            Assert.IsTrue(calculator1 != null);
        }

        [Test]
        public void CostCalc_Objects_Should_Not_Be_Equal()
        {
            var consoleCommand = new ConsoleCommand("fruitpal mango 53 405");

            var calc1 = new CostCalcFactory(_loggerMock.Object).Create(consoleCommand);
            var calc2 = new CostCalcFactory(_loggerMock.Object).Create(consoleCommand);

            Assert.AreNotEqual(calc1, calc2);
        }
    }
}
