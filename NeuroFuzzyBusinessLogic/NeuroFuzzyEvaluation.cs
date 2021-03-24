using DataLayer;
using DataLayer.Enums;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuroFuzzyBusinessLogic
{
    public class NeuroFuzzyEvaluation
    {
        public static double GetAccuracy(PredictionHistoryModel predictionHistory, int roundingDigits = 3)
        {
            if (!predictionHistory.ActualValues.Any() && !predictionHistory.PredictedValues.Any())
            {
                return 0.0d;
            }

            if (predictionHistory.ActualValues.Count() != predictionHistory.PredictedValues.Count())
            {
                throw new Exception("The size of the actual testing samples is not the same as the size of the predicted values!");
            }

            int correctVals = 0, allVals = 0;
            var length = predictionHistory.ActualValues.Count();
            for(var i = 0; i < length; i++)
            {
                if (predictionHistory.ActualValues[i] == predictionHistory.PredictedValues[i])
                {
                    correctVals++;
                }

                allVals++;
            }

            return Math.Round(((double)correctVals / allVals) * 100.0d, roundingDigits);
        }

        public static Dictionary<GeometricalShapeType, ConfusionMatrixModel> GetConfusionMatrixEvaluationDetails(
            PredictionHistoryModel predictionHistory, 
            int beta = 1,
            int roundingDigits = 3)
        {
            if (!predictionHistory.ActualValues.Any() && !predictionHistory.PredictedValues.Any())
            {
                return null;
            }

            if (predictionHistory.ActualValues.Count() != predictionHistory.PredictedValues.Count())
            {
                throw new Exception("The size of the actual testing samples is not the same as the size of the predicted values!");
            }

            var tagsSet = new HashSet<GeometricalShapeType>(predictionHistory.ActualValues);
            tagsSet.UnionWith(predictionHistory.PredictedValues);

            var results = new Dictionary<GeometricalShapeType, ConfusionMatrixModel>();
            var length = predictionHistory.ActualValues.Count();
            foreach(var tag in tagsSet)
            {
                int tp = 0, tn = 0, fp = 0, fn = 0;
                for (var i = 0; i < length; i++)
                {
                    if (predictionHistory.ActualValues[i] != tag && predictionHistory.PredictedValues[i] != tag)
                    {
                        tn++;
                    }
                    else if (predictionHistory.ActualValues[i] == tag && predictionHistory.PredictedValues[i] == tag)
                    {
                        tp++;
                    }
                    else if (predictionHistory.ActualValues[i] == tag && predictionHistory.PredictedValues[i] != tag)
                    {
                        fn++;
                    }
                    else if (predictionHistory.ActualValues[i] != tag && predictionHistory.PredictedValues[i] == tag)
                    {
                        fp++;
                    }
                }

                double accuracy = (double)(tp + tn) / (tp + tn + fn + fp);
                if (double.IsNaN(accuracy) || double.IsInfinity(accuracy))
                {
                    accuracy = 0.0f;
                }
                double precision = (double)tp / (tp + fp);
                if (double.IsNaN(precision) || double.IsInfinity(precision))
                {
                    precision = 0.0f;
                }
                double recall = (double)tp / (tp + fn); // true positive rate
                if (double.IsNaN(recall) || double.IsInfinity(recall))
                {
                    recall = 0.0f;
                }
                double fmeasure = (double)((beta * beta + 1) * precision * recall) / ((beta * beta) * precision + recall);
                if (double.IsNaN(fmeasure) || double.IsInfinity(fmeasure))
                {
                    fmeasure = 0.0f;
                }
                double specificity = (double)tn / (tn + fp); // true negative rate
                if (double.IsNaN(specificity) || double.IsInfinity(specificity))
                {
                    specificity = 0.0f;
                }

                var confMatrix = new ConfusionMatrixModel
                {
                    TruePositive = tp,
                    TrueNegative = tn,
                    FalsePositive = fp,
                    FalseNegative = fn,
                    Beta = beta,
                    Accuracy = Math.Round(accuracy * 100, roundingDigits),
                    Precision = Math.Round(precision * 100, roundingDigits),
                    Recall = Math.Round(recall * 100, roundingDigits),
                    Specificity = Math.Round(specificity * 100, roundingDigits),
                    Fmeasure = Math.Round(fmeasure * 100, roundingDigits)
                };
                results.Add(tag, confMatrix);
            }
            return results;
        }
    }
}
