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
                AcuteRight = acuteRight,
                ObtuseRight = obtuseRight,
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
                AcuteRight = acuteRight,
                ObtuseRight = obtuseRight,
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
                    AcuteRight = 1,
                    Obtuse = 3,
                    ObtuseRight = 6
                },
                new AngleTypeVector
                {
                    Acute = 1,
                    AcuteRight = 0,
                    Obtuse = 4,
                    ObtuseRight = 6
                },
                new AngleTypeVector
                {
                    Acute = 0,
                    AcuteRight = 10,
                    Obtuse = 34,
                    ObtuseRight = 1
                },
            };

            var expected = new AngleTypeVector
            {
                Acute = 3.6666666666666665d,
                AcuteRight = 3.6666666666666665d,
                ObtuseRight = 4.333333333333333d,
                Obtuse = 13.666666666666666d
            };

            foreach(var item in input)
            {
                _binarySynapticWeightsLogic.AddTrainingSampleToModel(item, GeometricalShapeType.Circle);
            }
            _binarySynapticWeightsLogic.AddTrainingSampleToModel(new AngleTypeVector(1, 2, 3, 4), GeometricalShapeType.Square);

            var result = _binarySynapticWeightsLogic.CalculateAverageVector(GeometricalShapeType.Circle);
            Assert.AreEqual(expected.Acute, result.Acute);
            Assert.AreEqual(expected.AcuteRight, result.AcuteRight);
            Assert.AreEqual(expected.Obtuse, result.Obtuse);
            Assert.AreEqual(expected.ObtuseRight, result.ObtuseRight);
        }

        [TestCase]
        public void GetEuclideanDistance()
        {
            var avg = new AngleTypeVector
            {
                Acute = 100.0d,
                AcuteRight = 2.1d,
                Obtuse = 13.66d,
                ObtuseRight = 4.0d,
            };

            var input = new AngleTypeVector
            {
                Acute = 50,
                AcuteRight = 10,
                Obtuse = 35,
                ObtuseRight = 72
            };

            var expected = 87.41742160462067d;
            var result = Helpers.GetEuclideanDistance(avg, input);
            Assert.AreEqual(expected, result);
        }
    }
}
