using DataLayer.Enums;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestFixture]
    class BinarySynapticWeightsTests
    {
        [TestCase]
        public void SimpleTestCase()
        {
            var trueVal = true;
            Assert.IsTrue(trueVal);
        }

        [TestCase(AngleType.Sharp)]
        public void InputTestCase(AngleType expected)
        {
            var result = new Dictionary<AngleType, int>() 
            {
                { AngleType.Sharp, 1 }
            };

            Assert.AreEqual(expected, result.FirstOrDefault().Key);
        }
    }
}
