using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyString
{
    // 11/08/2017
    // 1. split ApproximatelyEquals class into two methods
    // broadening the choice of results type to include list of metrics
    // 2. changed name to better reflect usage

    public static partial class StringComparison
    {
        public static bool IsMatch(this string source, string target, ComparisonTolerance tolerance, params ComparisonOptions[] options)
        {

            var comparisonResults = GetQualityMetrics(source, target, options);


            if (comparisonResults.Count == 0)
            {
                return false;
            }

            if (tolerance == ComparisonTolerance.Strong)
            {
                if (comparisonResults.Average() < 0.25)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (tolerance == ComparisonTolerance.Normal)
            {
                if (comparisonResults.Average() < 0.5)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (tolerance == ComparisonTolerance.Weak)
            {
                if (comparisonResults.Average() < 0.75)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (tolerance == ComparisonTolerance.Manual)
            {
                if (comparisonResults.Average() > 0.6)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }


        public static List<double> GetQualityMetrics(string source, string target,params ComparisonOptions[] options)
        {

            List<double> comparisonResults = new List<double>();

            if (!options.Contains(ComparisonOptions.CaseSensitive))
            {
                source = source.Capitalize();
                target = target.Capitalize();
            }

            // Min: 0    Max: source.Length = target.Length
            if (options.Contains(ComparisonOptions.UseHammingDistance))
            {
                if (source.Length == target.Length)
                {
                    comparisonResults.Add(source.HammingDistance(target) / target.Length);
                }
            }

            // Min: 0    Max: 1
            if (options.Contains(ComparisonOptions.UseJaccardDistance))
            {
                comparisonResults.Add(source.JaccardDistance(target));
            }

            // Min: 0    Max: 1
            if (options.Contains(ComparisonOptions.UseJaroDistance))
            {
                comparisonResults.Add(source.JaroDistance(target));
            }

            // Min: 0    Max: 1
            if (options.Contains(ComparisonOptions.UseJaroWinklerDistance))
            {
                comparisonResults.Add(source.JaroWinklerDistance(target));
            }

            // Min: 0    Max: LevenshteinDistanceUpperBounds - LevenshteinDistanceLowerBounds
            // Min: LevenshteinDistanceLowerBounds    Max: LevenshteinDistanceUpperBounds
            if (options.Contains(ComparisonOptions.UseNormalizedLevenshteinDistance))
            {
                comparisonResults.Add(Convert.ToDouble(source.NormalizedLevenshteinDistance(target)) / Convert.ToDouble((Math.Max(source.Length, target.Length) - source.LevenshteinDistanceLowerBounds(target))));
            }
            else if (options.Contains(ComparisonOptions.UseLevenshteinDistance))
            {
                comparisonResults.Add(Convert.ToDouble(source.LevenshteinDistance(target)) / Convert.ToDouble(source.LevenshteinDistanceUpperBounds(target)));
            }

            if (options.Contains(ComparisonOptions.UseLongestCommonSubsequence))
            {
                comparisonResults.Add(1 - Convert.ToDouble((source.LongestCommonSubsequence(target).Length) / Convert.ToDouble(Math.Min(source.Length, target.Length))));
            }

            if (options.Contains(ComparisonOptions.UseLongestCommonSubstring))
            {
                comparisonResults.Add(1 - Convert.ToDouble((source.LongestCommonSubstring(target).Length) / Convert.ToDouble(Math.Min(source.Length, target.Length))));
            }

            // Min: 0    Max: 1
            if (options.Contains(ComparisonOptions.UseSorensenDiceDistance))
            {
                comparisonResults.Add(source.SorensenDiceDistance(target));
            }

            // Min: 0    Max: 1
            if (options.Contains(ComparisonOptions.UseOverlapCoefficient))
            {
                comparisonResults.Add(1 - source.OverlapCoefficient(target));
            }

            // Min: 0    Max: 1
            if (options.Contains(ComparisonOptions.UseRatcliffObershelpSimilarity))
            {
                comparisonResults.Add(1 - source.RatcliffObershelpSimilarity(target));
            }

            return comparisonResults;
        }
    }
}
