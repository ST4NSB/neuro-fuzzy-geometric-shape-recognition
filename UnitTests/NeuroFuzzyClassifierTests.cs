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
        public void ComputeConvexHullGrahamScan_SquareShape_ExpectedBehaviour()
        {
            // Input shape points
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

        [Test]
        public void ComputeConvexHullGrahamScan_EllipseShape_ExpectedBehaviour()
        {
            // Input shape points
            var pointList = new List<Point>{
                new Point(350, 250), new Point(400, 270), new Point(450, 200),
                new Point(520, 180), new Point(600, 195), new Point(700, 280),
                new Point(800, 300), new Point(860, 310), new Point(900, 400),
                new Point(900, 500), new Point(890, 600), new Point(800, 700),
                new Point(705, 750), new Point(600, 805), new Point(500, 750),
                new Point(450, 700), new Point(400, 600), new Point(420, 500),
                new Point(330, 490), new Point(300, 350), new Point(600, 500),
            };

            var nfc = new NeuroFuzzyClassifier(pointList, new Point());

            // Act
            var result = nfc.ComputeConvexHullGrahamScan();

            // Assert
            var expected = new List<Point> {
                new Point(520, 180), new Point(600, 195), new Point(860, 310),
                new Point(900, 400), new Point(900, 500), new Point(890, 600),
                new Point(800, 700), new Point(705, 750), new Point(600, 805),
                new Point(500, 750), new Point(450, 700), new Point(330, 490),
                new Point(300, 350), new Point(350, 250), new Point(450, 200),
            };
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ComputeConvexHullGrahamScan_TriangleShape_ExpectedBehaviour()
        {
            // Input shape points
            var pointList = new List<Point>{
                new Point(-20,-10), new Point(-10, 15), new Point(-15, 10),
                new Point(-16, -4), new Point(-15, -7), new Point( 10, 10),
                new Point(-10, -4), new Point(-14,  2), new Point(-7,  20),
                new Point( 15,  5), new Point( 12,  1), new Point( 5,  15),
                new Point( -5, 25), new Point(  0, 20), new Point(-5,  -5),
                new Point(  0, -3), new Point( -5,  5), new Point( 5,   2),
            };

            var nfc = new NeuroFuzzyClassifier(pointList, new Point());

            // Act
            var result = nfc.ComputeConvexHullGrahamScan();

            // Assert
            var expected = new List<Point> {
                new Point(-20,-10), new Point(-5, -5), new Point( 12,  1),
                new Point( 15,  5), new Point(-5, 25), new Point(-15, 10),
            };
            result.Should().BeEquivalentTo(expected);
        }
        
    }
}
