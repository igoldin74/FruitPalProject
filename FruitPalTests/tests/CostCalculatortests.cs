using FruitPal;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace FruitPalTests.tests
{
    [TestFixture]
    class CostCalculatorTests
    {
        private TradingEngine _sut;
        private readonly Mock<ILogger> _loggerMock = new Mock<ILogger>();
        private JsonCommoditySerializer _commoditySerializer = new JsonCommoditySerializer();
        private FileCommoditySource _commoditySource = new FileCommoditySource();


        [Test]
        public void CostCalc_Should_Produce_Expected_Output()
        {

            string[] lines = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "testinput.csv"));

            foreach (var l in lines)
            {
                string[] line = l.Split(',');
                //string input = l.Replace(',',' ');
                string input = string.Join(" ", line, 0, 4);
                _sut = new TradingEngine(_loggerMock.Object, new ConsoleCommand(input), _commoditySource, _commoditySerializer,
                new CostCalcFactory(_loggerMock.Object));
                _sut.GetCostOutput();
                var output = _sut.CommodityCosts;

                Assert.AreEqual(string.Join(" ", output), line[4]);
            }
        }
    }
}
