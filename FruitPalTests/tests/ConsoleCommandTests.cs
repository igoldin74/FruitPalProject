using System;
using NUnit.Framework;
using FruitPal;
using System.Collections.Generic;

namespace FruitPalTests
{
    [TestFixture]
    public class ConsoleCommandTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("", typeof(ArgumentException))]
        [TestCase("    ", typeof(ArgumentException))]
        [TestCase("test", typeof(ArgumentException))]
        public void Validate_Method_Should_Throw_Expected_Exception(string arg, Type expectedException)
        {
            Assert.Throws(expectedException, () => new ConsoleCommand(arg));
        }

        [Test]
        public void TestArgsBreakDown()
        {
            var expectedResult = new List<object>() { "apple", 50, 100 };
            Assert.AreEqual(new ConsoleCommand("fruitpal apple 50 100").parameterValueList, expectedResult);
        }

        [TestCase("test", ExpectedResult = "Unrecognized command 'test'. Please enter a valid command.")]
        public string Validate_Method_Should_Have_Expected_Exception_Message_Case1(string arg)
        {
            return Assert.Catch(() => new ConsoleCommand(arg)).Message;
        }

        [TestCase("fruitpal", ExpectedResult = "Invalid number of arguments. Please use the fruitpal command with the 3 required arguments: <fruit> <price per ton ($)> <trade volume (tons)>")]
        public string Validate_Method_Should_Have_Expected_Exception_Message_Case2(string arg)
        {
            return Assert.Catch(() => new ConsoleCommand(arg)).Message;
        }

        [Test]
        public void Parse_Method_Should_Have_Expected_Exception_Message()
        {
            string[] args = { "fruitpal", "apple", "fifty", "100" };
            try 
            {
               new ConsoleCommand(string.Join(" ", args));
            }
            catch (ArgumentException ex)
            {
                string message = ex.Message;
                Assert.AreEqual(message, string.Format("The value passed for argument '{0}' cannot be parsed to type '{1}'",
                    args[2], Type.GetTypeCode(typeof(decimal))));
            }
            
            
        }

    }
}
