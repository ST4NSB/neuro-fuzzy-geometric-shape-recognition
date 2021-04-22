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

        [TestCase(0,0,0,0)]
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
            _binarySynapticWeightsLogic.AddTrainingSampleToModel(input, GeometricalShapeType.Square);

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

            _binarySynapticWeightsLogic.AddTrainingSampleToModel(input, GeometricalShapeType.Square);
            _binarySynapticWeightsLogic.AddTrainingSampleToModel(input, GeometricalShapeType.Circle);
            _binarySynapticWeightsLogic.AddTrainingSampleToModel(input, GeometricalShapeType.Triangle);
            
            var result = _binarySynapticWeightsLogic.IsReadyForTraining();
            Assert.IsTrue(result);
        }

        [TestCase(0, (uint)0x00000000)]
        [TestCase(1, (uint)0x00000001)]
        [TestCase(5, (uint)0x0000001f)]
        [TestCase(10, (uint)0x000003ff)]
        [TestCase(31, (uint)0x7fffffff)]
        [TestCase(32, (uint)0xffffffff)]
        public void ConvertNumberToSerialCoding(int inputValue, uint expected)
        {
            var result = Helpers.ConvertNumberToSerialCoding(inputValue);
            Assert.AreEqual(expected, result);
        }

        [TestCase(new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, 3, 0)]
        [TestCase(new int[] { 0, 0, 1 }, new int[] { 0, 0, 1 }, 3, 0)]
        [TestCase(new int[] { 1, 1, 1 }, new int[] { 1, 1, 1 }, 3, 0)]
        [TestCase(new int[] { 0, 1, 1 }, new int[] { 0, 0, 0 }, 3, 2)]
        [TestCase(new int[] { 0, 0, 1 }, new int[] { 0, 1, 1 }, 3, 1)]
        [TestCase(new int[] { 0, 0, 1 }, new int[] { 1, 1, 1 }, 3, 2)]
        public void GetHammingDistance(int[] key, int[] vector, int length, int expected)
        {
            var result = Helpers.GetHammingDistance(key, vector, length);
            Assert.AreEqual(expected, result);
        }

        [TestCase]
        public void ConvertAngleTypeVectorToArrayOfBits_32()
        {
            var input = new AngleTypeVector
            {
                Acute = 32, 
                MediumAcute = 32,
                Right = 32,
                Obtuse = 32
            };

            var result = _binarySynapticWeightsLogic.ConvertAngleTypeVectorToArrayOfBits(input);
            for (int i = 0; i < BinarySynapticWeightsLogic.VECTOR_LENGTH; i++)
            {
                Assert.AreEqual(1, result[i]);
            }
        }

        [TestCase]
        public void ConvertAngleTypeVectorToArrayOfBits_2()
        {
            var input = new AngleTypeVector
            {
                Acute = 2,
                MediumAcute = 2,
                Right = 2,
                Obtuse = 2
            };

            var result = _binarySynapticWeightsLogic.ConvertAngleTypeVectorToArrayOfBits(input);
            Assert.AreEqual(0, result[0]);
            Assert.AreEqual(0, result[29]);
            Assert.AreEqual(1, result[30]);
            Assert.AreEqual(1, result[31]);
            
            Assert.AreEqual(0, result[32]);
            Assert.AreEqual(0, result[61]);
            Assert.AreEqual(1, result[62]);
            Assert.AreEqual(1, result[63]);
          
            Assert.AreEqual(0, result[64]);
            Assert.AreEqual(0, result[93]);
            Assert.AreEqual(1, result[94]);
            Assert.AreEqual(1, result[95]);
      
            Assert.AreEqual(0, result[96]);
            Assert.AreEqual(0, result[124]);
            Assert.AreEqual(0, result[125]);
            Assert.AreEqual(1, result[126]);
            Assert.AreEqual(1, result[127]);
        }

        [TestCase]
        public void PredictorTest()
        {
            // circles
            _binarySynapticWeightsLogic.AddTrainingSampleToModel(new AngleTypeVector
            {
                Acute = 10,
                MediumAcute = 0,
                Right = 0,
                Obtuse = 0,
            }, GeometricalShapeType.Circle);
            _binarySynapticWeightsLogic.AddTrainingSampleToModel(new AngleTypeVector
            {
                Acute = 9,
                MediumAcute = 1,
                Right = 0,
                Obtuse = 0,
            }, GeometricalShapeType.Circle);
            _binarySynapticWeightsLogic.AddTrainingSampleToModel(new AngleTypeVector
            {
                Acute = 8,
                MediumAcute = 2,
                Right = 0,
                Obtuse = 0,
            }, GeometricalShapeType.Circle);


            // squares
            _binarySynapticWeightsLogic.AddTrainingSampleToModel(new AngleTypeVector
            {
                Acute = 4,
                MediumAcute = 1,
                Right = 1,
                Obtuse = 4,
            }, GeometricalShapeType.Square);_binarySynapticWeightsLogic.AddTrainingSampleToModel(new AngleTypeVector
            {
                Acute = 4,
                MediumAcute = 2,
                Right = 0,
                Obtuse = 4,
            }, GeometricalShapeType.Square);_binarySynapticWeightsLogic.AddTrainingSampleToModel(new AngleTypeVector
            {
                Acute = 1,
                MediumAcute = 1,
                Right = 0,
                Obtuse = 8,
            }, GeometricalShapeType.Square);
            _binarySynapticWeightsLogic.AddTrainingSampleToModel(new AngleTypeVector
            {
                Acute = 2,
                MediumAcute = 0,
                Right = 1,
                Obtuse = 7,
            }, GeometricalShapeType.Square);

            // triangles
            _binarySynapticWeightsLogic.AddTrainingSampleToModel(new AngleTypeVector
            {
                Acute = 3,
                MediumAcute = 7,
                Right = 0,
                Obtuse = 0,
            }, GeometricalShapeType.Triangle);
            _binarySynapticWeightsLogic.AddTrainingSampleToModel(new AngleTypeVector
            {
                Acute = 1,
                MediumAcute = 3,
                Right = 6,
                Obtuse = 0,
            }, GeometricalShapeType.Triangle);_binarySynapticWeightsLogic.AddTrainingSampleToModel(new AngleTypeVector
            {
                Acute = 2,
                MediumAcute = 2,
                Right = 5,
                Obtuse = 1,
            }, GeometricalShapeType.Triangle);_binarySynapticWeightsLogic.AddTrainingSampleToModel(new AngleTypeVector
            {
                Acute = 1,
                MediumAcute = 5,
                Right = 4,
                Obtuse = 0,
            }, GeometricalShapeType.Triangle);

            // train
            _binarySynapticWeightsLogic.Train();

            var result_circle = _binarySynapticWeightsLogic.Predict(new AngleTypeVector
            {
                Acute = 8,
                MediumAcute = 1,
                Right = 1,
                Obtuse = 0,
            });

            var result_square = _binarySynapticWeightsLogic.Predict(new AngleTypeVector
            {
                Acute = 2,
                MediumAcute = 1,
                Right = 1,
                Obtuse = 6,
            });

            var result_triangle = _binarySynapticWeightsLogic.Predict(new AngleTypeVector
            {
                Acute = 2,
                MediumAcute = 4,
                Right = 4,
                Obtuse = 0,
            });

            Assert.AreEqual(GeometricalShapeType.Circle, result_circle); // 4 - circle, 1 - square, 1 - triangle
            Assert.AreEqual(GeometricalShapeType.Square, result_square); // 1 - circle, 6 - square, 1 - triangle
            Assert.AreEqual(GeometricalShapeType.Triangle, result_triangle); // 1 - circle, 1 - square, 6 - triangle
        }
    }
}
