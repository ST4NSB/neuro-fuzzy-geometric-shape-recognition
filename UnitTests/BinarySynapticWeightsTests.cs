using DataLayer.Enums;
using DataLayer.Models;
using NeuroFuzzyBusinessLogic;
using NeuroFuzzyBusinessLogic.Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestFixture]
    class BinarySynapticWeightsTests
    {
        BinarySynapticWeightsLogic _binarySynapticWeightsLogic;

        [SetUp]
        public void Setup()
        {
            _binarySynapticWeightsLogic = new BinarySynapticWeightsLogic();
        }

        [TestCase]  
        public void IsReadyToLearn_Empty()
        {
            var result = _binarySynapticWeightsLogic.IsReadyForTraining();
            Assert.IsFalse(result);
        }

        [TestCase(1,2,3,4)]
        public void IsReadyToLearn_NonEmpty_False(int acute, int acuteRight, int obtuseRight, int obtuse)
        {
            var input = new AngleTypeVector
            {
                Acute = acute,
                MediumAcute = acuteRight,
                Right = obtuseRight,
                Obtuse = obtuse
            };

            _binarySynapticWeightsLogic.AddTrainingSampleToModel(input, GeometricalShapeType.Circle);

            var result = _binarySynapticWeightsLogic.IsReadyForTraining();
            Assert.IsFalse(result);
        }

        [TestCase(1, 2, 3, 4)]
        public void IsReadyToLearn_NonEmpty_True(int acute, int acuteRight, int obtuseRight, int obtuse)
        {
            var input = new AngleTypeVector
            {
                Acute = acute,
                MediumAcute = acuteRight,
                Right = obtuseRight,
                Obtuse = obtuse
            };
            
            foreach(var tag in Enum.GetValues(typeof(GeometricalShapeType)))
            {
                _binarySynapticWeightsLogic.AddTrainingSampleToModel(input, (GeometricalShapeType)tag);
            }

            var result = _binarySynapticWeightsLogic.IsReadyForTraining();
            Assert.IsTrue(result);
        }

        [TestCase]
        public void CalculateAverageVector()
        {
            var input = new AngleTypeVector[]
            {
                new AngleTypeVector 
                {
                    Acute = 10,
                    MediumAcute = 1,
                    Obtuse = 3,
                    Right = 6
                },
                new AngleTypeVector
                {
                    Acute = 1,
                    MediumAcute = 0,
                    Obtuse = 4,
                    Right = 6
                },
                new AngleTypeVector
                {
                    Acute = 0,
                    MediumAcute = 10,
                    Obtuse = 34,
                    Right = 1
                },
            };

            var expected = new AngleTypeVector
            {
                Acute = 3.6666666666666665d,
                MediumAcute = 3.6666666666666665d,
                Right = 4.333333333333333d,
                Obtuse = 13.666666666666666d
            };

            foreach(var item in input)
            {
                _binarySynapticWeightsLogic.AddTrainingSampleToModel(item, GeometricalShapeType.Circle);
            }
            _binarySynapticWeightsLogic.AddTrainingSampleToModel(new AngleTypeVector(1, 2, 3, 4), GeometricalShapeType.Square);

            var result = _binarySynapticWeightsLogic.CalculateAverageVector(GeometricalShapeType.Circle);
            Assert.AreEqual(expected.Acute, result.Acute);
            Assert.AreEqual(expected.MediumAcute, result.MediumAcute);
            Assert.AreEqual(expected.Obtuse, result.Obtuse);
            Assert.AreEqual(expected.Right, result.Right);
        }

        [TestCase]
        public void GetEuclideanDistance()
        {
            var avg = new AngleTypeVector
            {
                Acute = 100.0d,
                MediumAcute = 2.1d,
                Obtuse = 13.66d,
                Right = 4.0d,
            };

            var input = new AngleTypeVector
            {
                Acute = 50,
                MediumAcute = 10,
                Obtuse = 35,
                Right = 72
            };

            var expected = 87.41742160462067d;
            var result = Helpers.GetEuclideanDistance(avg, input);
            Assert.AreEqual(expected, result);
        }
    }
}
