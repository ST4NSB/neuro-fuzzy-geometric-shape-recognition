using NUnit.Framework;
using DataLayer.Models;
using NeuroFuzzyBusinessLogic;
using System.Collections.Generic;

namespace UnitTests
{
    [TestFixture]
    class NeuroFuzzyClassifierTests
    {
        [Test]
        public void ComputeConvexHullGrahamScan_TestExpectedBehaviour()
        {
            // Init square shape
            var center = new Point(5, 3);

            var pointList = new List<Point>{
                new Point(3, 3), new Point(3, 5), new Point(4, 4),
                new Point(5, 1), new Point(8, 1), new Point(7, 3),
                new Point(8, 6)
            };

            var nfc = new NeuroFuzzyClassifier(pointList, center);

            // Act
            var result = nfc.ComputeConvexHullGrahamScan();

            // Assert
            Assert.AreEqual(new Point(3, 3), result[0]);
            Assert.AreEqual(new Point(3, 5), result[1]);
            Assert.AreEqual(new Point(8, 6), result[2]);
            Assert.AreEqual(new Point(8, 1), result[3]);
            Assert.AreEqual(new Point(5, 1), result[4]);
        }
    }
}
