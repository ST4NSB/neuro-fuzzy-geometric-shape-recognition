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
        private bool _canUsePredictor;
        private List<Tuple<AngleTypeVector, GeometricalShapeType>> _vectorModel;
        private Dictionary<double, List<Tuple<AngleTypeVector, GeometricalShapeType>>> _distancesModel;

        #region PRIVATE METHODS

        private void Init()
        {
            _canUsePredictor = false;
            _vectorModel = new List<Tuple<AngleTypeVector, GeometricalShapeType>>();
            _distancesModel = new Dictionary<double, List<Tuple<AngleTypeVector, GeometricalShapeType>>>();
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

        public void Train(AngleTypeVector inputVector, GeometricalShapeType expectedShape)
        {
            AddTrainingSampleToModel(inputVector, expectedShape);

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
            // demo function
            // logic etc.
            return GeometricalShapeType.Triangle;
        }

        #endregion

        #region PROTECTED METHODS

        protected internal void AddTrainingSampleToModel(AngleTypeVector inputVector, GeometricalShapeType expectedShape)
        {
            _vectorModel.Add(new Tuple<AngleTypeVector, GeometricalShapeType>(inputVector, expectedShape));
        }

        protected internal void FitModel()
        {

        }

        protected internal bool IsReadyForTraining()
        {
            var shapesCount = Enum.GetNames(typeof(GeometricalShapeType)).Length;
            var learningDistinctShapeCount = _vectorModel.Select(x => x.Item2)
                                                         .Distinct()
                                                         .Count();

            if (learningDistinctShapeCount != shapesCount)
            {
                return false;
            }

            return true;
        }

        protected internal AngleTypeVector CalculateAverageVector(GeometricalShapeType shapeType)
        {
            var values = _vectorModel.Where(st => st.Item2 == shapeType)
                                         .Select(x => x.Item1);

            return new AngleTypeVector
            {
                Acute = values.Average(v => (int)v.Acute),
                AcuteRight = values.Average(v => (int)v.AcuteRight),
                ObtuseRight = values.Average(v => (int)v.ObtuseRight),
                Obtuse = values.Average(v => (int)v.Obtuse)
            };
        }

        protected internal void ClassifyDistancesToAverageValue(Tuple<AngleTypeVector, GeometricalShapeType> avg)
        {
            _vectorModel.ForEach(vItem => {
                var distance = Helpers.GetEuclideanDistance(avg.Item1, vItem.Item1);
                _distancesModel.AddDistance(distance, vItem);
            });
        }

        #endregion
    }
}
