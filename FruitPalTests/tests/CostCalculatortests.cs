using FruitPal;
using FruitPalTests.models;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
