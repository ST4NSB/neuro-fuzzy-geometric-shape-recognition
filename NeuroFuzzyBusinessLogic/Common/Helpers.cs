using DataLayer.Models;
using System;

namespace NeuroFuzzyBusinessLogic.Common
{
    public static class Helpers
    {
        public static double GetEuclideanDistance(AngleTypeVector avg, AngleTypeVector vector)
        {
            return Math.Sqrt(Math.Pow((double)avg.Acute - (int)vector.Acute, 2) +
                   Math.Pow((double)avg.MediumAcute - (int)vector.MediumAcute, 2) +
                   Math.Pow((double)avg.Obtuse - (int)vector.Obtuse, 2) +
                   Math.Pow((double)avg.Right - (int)vector.Right, 2));
        }

        public static int GetHammingDistance(int[] key, int[] vector, int vectorLength)
        {
            var dist = default(int);
            
            for(var i = 0; i < vectorLength; i++)
            {
                if (key[i] != vector[i])
                {
                    dist++;
                }
            }

            return dist;
        }

        public static void ConcatUintToArray(ref int[] values, int limit, ref int counter, int vectorCount)
        {
            int shifts = 31;
            while (counter < limit)
            {
                uint acuteAngle = ConvertNumberToSerialCoding(vectorCount);
                var bit = (acuteAngle >> shifts) & 0x00000001;
                shifts--;

                values[counter] = (int)bit;
                counter++;
            }
        }

        internal static uint ConvertNumberToSerialCoding(int number)
        {
            if (number > 32 || number < 0)
            {
                throw new Exception("Number can't be higher than 32 or lower than 0");
            }
            
            if (number == 0)
            {
                return 0;
            }

            uint value = 0x00000000;
            for(var i = 0; i < number; i++)
            {
                value = value << 1;
                value += 1;
            }

            return value;
        }
    }
}
