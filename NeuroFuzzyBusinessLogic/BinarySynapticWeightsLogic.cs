using DataLayer.Enums;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using NeuroFuzzyBusinessLogic.Common;

namespace NeuroFuzzyBusinessLogic
{
    public class BinarySynapticWeightsLogic
    {
        protected internal const int VECTOR_LENGTH = 128; // (32 * 4) = 128
        private bool _canUsePredictor;
        
        // learning data
        private Dictionary<int, List<InputVectorModel>> _distancesDictionary;
        private List<InputVectorModel> _keysHistory;
        private List<InputVectorModel> _inputTrainLayer;
        private List<HiddenNodeModel> _hiddenLayer;

        #region PRIVATE METHODS

        private void Init()
        {
            _canUsePredictor = false;
            _inputTrainLayer = new List<InputVectorModel>();
            _hiddenLayer = new List<HiddenNodeModel>();
        }

        #endregion

        #region PUBLIC METHODS

        public BinarySynapticWeightsLogic()
        {
            Init();
        }

        public void DumpBinarySynapticWeights()
        {
            Init();
        }

        public void AddTrainingSampleToModel(AngleTypeVector inputVector, GeometricalShapeType expectedShape)
        {
            var vector = ConvertAngleTypeVectorToArrayOfBits(inputVector);
            _inputTrainLayer.Add(new InputVectorModel
            {
                InputNodes = vector,
                Label = expectedShape
            });
        }

        public void Train()
        {
            if (IsReadyForTraining())
            {
                FitModel();
                _canUsePredictor = true;
            }
        }

        public bool CanUsePredictor()
        {
            return _canUsePredictor;
        }

        public GeometricalShapeType Predict(AngleTypeVector inputVector)
        {
            if (!CanUsePredictor())
            {
                return GeometricalShapeType.None;
            }

            var isAnyHiddenNodeActivated = false;
            var hiddenNodesActivated = new Dictionary<GeometricalShapeType, int>();
            var inputTestVector = ConvertAngleTypeVectorToArrayOfBits(inputVector);
            
            var shapesToPredict = new GeometricalShapeType[] { 
                GeometricalShapeType.Circle,
                GeometricalShapeType.Square, 
                GeometricalShapeType.Triangle };
            foreach(var shape in shapesToPredict)
            {
                var hiddenLayers = _hiddenLayer.Where(x => x.OutputNodeLabel == shape);
                foreach(var w in hiddenLayers)
                {
                    var sum = 0;
                    for(int bitIndex = 0; bitIndex < VECTOR_LENGTH; bitIndex++)
                    {
                        sum += (inputTestVector[bitIndex] * w.WeightsIndexLayer[bitIndex]);
                    }
                    
                    if (sum > w.ActivationThreshold)
                    {
                        isAnyHiddenNodeActivated = true;
                        if (hiddenNodesActivated.ContainsKey(shape))
                        {
                            hiddenNodesActivated[shape] += 1;
                        }
                        else
                        {
                            hiddenNodesActivated.Add(shape, 1);
                        }
                    }
                }
            }
            
            if (!isAnyHiddenNodeActivated)
            {
                return GeometricalShapeType.None;
            }

            // returns the shape with the highest hidden nodes activated
            return hiddenNodesActivated.OrderByDescending(x => x.Value).FirstOrDefault().Key;
        }

        #endregion

        #region PROTECTED METHODS

        protected internal void FitModel()
        {
            var shapesPlaneCreation = new GeometricalShapeType[] { GeometricalShapeType.Circle, 
                GeometricalShapeType.Square, GeometricalShapeType.Triangle };

            foreach (var currentShape in shapesPlaneCreation)
            {
                _keysHistory = new List<InputVectorModel>();

                // step 1
                var averageVector = CalculateAverageVector(currentShape);
                CalculateDistancesFromKey(averageVector, applyOnAll: false, shape: currentShape);

                var currentKey = _distancesDictionary.OrderBy(x => x.Key).FirstOrDefault().Value?.FirstOrDefault();
                _keysHistory.Add(currentKey);
                CalculateDistancesFromKey(currentKey.InputNodes);

                var yesDist = GetFirstDistanceAtShape(currentShape, orderDistances: "DESC");
                var noDist = GetFirstDistanceAtShape(currentShape, orderDistances: "ASC");

                var distance = 0;

                // step 2
                if (yesDist >= noDist)
                {
                    distance = 1;

                    // step 3
                    while (true)
                    {
                        if (distance > _distancesDictionary.Max(x => x.Key))
                        {
                            break;
                        }

                        if (!_distancesDictionary.ContainsKey(distance))
                        {
                            distance++;
                            continue;
                        }

                        var Opj = _distancesDictionary[distance].Count(x => x.Label == currentShape);
                        var Orj = _distancesDictionary[distance].Count(x => x.Label != currentShape);

                        if (Opj < Orj || distance > yesDist)
                        {
                            break;
                        }

                        distance++;
                    }
                }

                // step 4
                while (true)
                {
                    CreateSeparationPlane(currentKey, distance);
                    if (!ExistsNotProcessedInputNodes(currentShape))
                    {
                        break; // exit
                    }

                    currentKey = GetNextNotProcessedInputNode(currentShape);
                    _keysHistory.Add(currentKey);
                    CalculateDistancesFromKey(currentKey.InputNodes);

                    distance = 0;

                    // step 5
                    while (true)
                    {
                        if (distance > _distancesDictionary.Max(x => x.Key))
                        {
                            break;
                        }

                        if (!_distancesDictionary.ContainsKey(distance))
                        {
                            distance++;
                            continue;
                        }    

                        var Opj = _distancesDictionary[distance].Count(x => x.Label != currentShape);
                        var Orj = _distancesDictionary[distance].Count(x => x.Label == currentShape);

                        if (Opj < Orj)
                        {
                            distance++;
                            continue;
                        }
                        break;
                    }

                    CreateSeparationPlane(currentKey, distance);

                    // step 6
                    if (!ExistsNotProcessedInputNodes(currentShape))
                    {
                        break; // exit
                    }

                    distance++;
                }
            }
        }

        protected internal int[] ConvertAngleTypeVectorToArrayOfBits(AngleTypeVector vector)
        {
            int counter = 0;
            int[] values = new int[VECTOR_LENGTH];

            Helpers.ConcatUintToArray(ref values, (VECTOR_LENGTH / 4), ref counter, vector.Acute);
            Helpers.ConcatUintToArray(ref values, (VECTOR_LENGTH / 2), ref counter, vector.MediumAcute);
            Helpers.ConcatUintToArray(ref values, ((VECTOR_LENGTH / 2) + (VECTOR_LENGTH / 4)), ref counter, vector.Right);
            Helpers.ConcatUintToArray(ref values, VECTOR_LENGTH, ref counter, vector.Obtuse);

            return values;
        }

        protected internal bool IsReadyForTraining()
        {
            var shapesCount = Enum.GetNames(typeof(GeometricalShapeType))
                        .Where(x => x != GeometricalShapeType.None.ToString())
                        .Count();
            var learningDistinctShapeCount = _inputTrainLayer.Select(x => x.Label)
                                                        .Where(x => x != GeometricalShapeType.None)
                                                        .Distinct()
                                                        .Count();

            if (learningDistinctShapeCount != shapesCount)
            {
                return false;
            }

            return true;
        }

        protected internal int GetFirstDistanceAtShape(GeometricalShapeType shape, string orderDistances)
        {
            if (orderDistances == "ASC")
            {
                var orderedDictionary = _distancesDictionary.OrderBy(x => x.Key);

                foreach (var distitem in orderedDictionary)
                {
                    foreach (var item in distitem.Value)
                    {
                        if (item.Label != shape)
                        {
                            return distitem.Key;
                        }
                    }
                }
            }
            else if (orderDistances == "DESC")
            {
                var orderedDictionary = _distancesDictionary.OrderByDescending(x => x.Key);

                foreach (var distitem in orderedDictionary)
                {
                    foreach (var item in distitem.Value)
                    {
                        if (item.Label == shape)
                        {
                            return distitem.Key;
                        }
                    }
                }
            }

            return 0;
        }

        protected internal bool ExistsNotProcessedInputNodes(GeometricalShapeType currentShape)
        {
            var inputNodes = _inputTrainLayer.Where(x => x.Label == currentShape);
            foreach (var input in inputNodes)
            {
                bool found = false;
                foreach(var key in _keysHistory)
                {
                    if (Helpers.GetHammingDistance(key.InputNodes, input.InputNodes, VECTOR_LENGTH) == 0)
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    return true;
                }
            }

            return false;
        }

        protected internal InputVectorModel GetNextNotProcessedInputNode(GeometricalShapeType currentShape)
        {
            var inputNodes = _inputTrainLayer.Where(x => x.Label == currentShape);
            foreach (var input in inputNodes)
            {
                bool found = false;
                foreach (var key in _keysHistory)
                {
                    if (Helpers.GetHammingDistance(key.InputNodes, input.InputNodes, VECTOR_LENGTH) == 0)
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    return input;
                }
            }

            return null;
        }

        protected internal void CreateSeparationPlane(InputVectorModel key, int distance)
        {
            int rightSideValue = 0;
            var weights = new Dictionary<int, int>();
            for (int index = 0; index < VECTOR_LENGTH; index++)
            {
                rightSideValue += key.InputNodes[index];
                
                if (key.InputNodes[index] == 0)
                {
                    weights.Add(index, -1);
                }
                else
                {
                    weights.Add(index, 1); // case when bit value is 1
                }
            }

            var threshold = (((double)(distance - 1) + distance) / 2.0d) - (rightSideValue - 1);

            _hiddenLayer.Add(new HiddenNodeModel
            {
                ActivationThreshold = threshold,
                WeightsIndexLayer = weights,
                OutputNodeLabel = key.Label
            });
        }

        protected internal int[] CalculateAverageVector(GeometricalShapeType shapeType)
        {
            var values = _inputTrainLayer.Where(st => st.Label == shapeType).Select(x => x.InputNodes);

            int[] sums = new int[VECTOR_LENGTH];

            foreach (var value in values)
            {
                for(int i = 0; i < VECTOR_LENGTH; i++)
                {
                    sums[i] += value[i];
                }
            }

            for (int i = 0; i < VECTOR_LENGTH; i++)
            {
                sums[i] = (int)Math.Round((double)sums[i] / values.Count());
            }

            return sums;
        }

        protected internal void CalculateDistancesFromKey(int[] keyNodes, 
                                                          bool applyOnAll = true, 
                                                          GeometricalShapeType shape = GeometricalShapeType.None)
        {
            _distancesDictionary = new Dictionary<int, List<InputVectorModel>>();
            
            foreach(var vItem in _inputTrainLayer)
            {
                if (applyOnAll || (shape == vItem.Label && !applyOnAll))
                {
                    var distance = Helpers.GetHammingDistance(keyNodes, vItem.InputNodes, VECTOR_LENGTH);
                    _distancesDictionary.AddDistance(distance, vItem);
                }
            }
        }

        #endregion
    }
}
