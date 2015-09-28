using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyString
{
    public static partial class Operations
    {
        public static string Capitalize(this string source)
        {
            return source.ToUpper();
        }

        public static string[] SplitIntoIndividualElements(string source)
        {
            string[] stringCollection = new string[source.Length];

            for (int i = 0; i < stringCollection.Length; i++)
            {
                stringCollection[i] = source[i].ToString();
            }

            return stringCollection;
        }

        public static string MergeIndividualElementsIntoString(IEnumerable<string> source)
        {
            string returnString = "";

            for (int i = 0; i < source.Count(); i++)
            {
                returnString += source.ElementAt<string>(i);
            }
            return returnString;
        }

        public static List<string> ListPrefixes(this string source)
        {
            List<string> prefixes = new List<string>();

            for (int i = 0; i < source.Length; i++)
            {
                prefixes.Add(source.Substring(0, i));
            }

            return prefixes;
        }

        public static List<string> ListBiGrams(this string source)
        {
            return ListNGrams(source, 2);
        }

        public static List<string> ListTriGrams(this string source)
        {
            return ListNGrams(source, 3);
        }

        public static List<string> ListNGrams(this string source, int n)
        {
            List<string> nGrams = new List<string>();

            if (n > source.Length)
            {
                return null;
            }
            else if (n == source.Length)
            {
                nGrams.Add(source);
                return nGrams;
            }
            else
            {
                for (int i = 0; i < source.Length - n; i++)
                {
                    nGrams.Add(source.Substring(i, n));
                }

                return nGrams;
            }
        }

        public static IEnumerable<char> Intersect(this string a, string b)
        {

            var dictionary = new Dictionary<char, bool>();
            foreach (var c1 in a)
            {
                dictionary[c1] = false;
            }
            foreach (var c2 in b)
            {
                if (dictionary.ContainsKey(c2))
                {
                    dictionary[c2] = true;
                }
            }
            return dictionary.Where(x => x.Value).Select(x => x.Key);
        }

        public static IEnumerable<char> Union(this string a, string b)
        {
            var list = new List<char>();
            foreach (var c1 in a)
            {

                if (!list.Contains(c1))
                {
                    list.Add(c1);
                }
            }
            foreach (var c2 in b)
            {
                if (!list.Contains(c2))
                {
                    list.Add(c2);
                }
            }
            return list;
        }
    }
}
