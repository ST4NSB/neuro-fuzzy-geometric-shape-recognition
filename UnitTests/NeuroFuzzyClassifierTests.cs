using NUnit.Framework;
using FluentAssertions;
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
            // Input square shape
            var pointList = new List<Point>{
                new Point(3, 3), new Point(3, 5), new Point(4, 4),
                new Point(5, 1), new Point(8, 1), new Point(7, 3),
                new Point(8, 6)
            };

            var nfc = new NeuroFuzzyClassifier(pointList, new Point());

            // Act
            var result = nfc.ComputeConvexHullGrahamScan();

            // Assert
            var expected = new List<Point> {
                new Point(5, 1), new Point(8, 1), new Point(8, 6),
                new Point(3, 5), new Point(3,3)
            };
            result.Should().BeEquivalentTo(expected);


        }
    }
}
