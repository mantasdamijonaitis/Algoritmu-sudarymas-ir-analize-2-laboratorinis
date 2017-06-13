using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmAnalisisSecond
{
    public class Analysis
    {
        private const int n = 2;
        private const int maxValue = 10;
        private int[] w = null;
        private int[] v = null;
        private StringBuilder logger = new StringBuilder();

        int notOptimizedMethodCalls = 0;
        int notOptimizedLoopCalls = 0;

        int optimizedMethodCalls = 0;
        int optimizedLoopCalls = 0;

        public Analysis()
        {
            FirstPart();
        }

        public int SecondPart(string initialString)
        {
            string longestPalindrome = FindLongestPalindrome(initialString);
            string beforePalidrome = initialString.Replace(longestPalindrome, string.Empty);

            /*Console.WriteLine("longestPalindrome " + longestPalindrome);
            Console.WriteLine("beforePalindrome " + beforePalidrome);
            Console.WriteLine("amount to add " + (beforePalidrome != string.Empty ? beforePalidrome.Length : longestPalindrome.Length - 1));*/
            return beforePalidrome.Length;
        }

        private void FirstPart()
        {
            Random random = new Random();
            w = new int[n] { 1, 2 };
            v = new int[n] { 3, 4 };
            int arg = 4;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("Neoptimizuota kvieciame su:");
            int finalNotOptimised = F(arg, w, v);
            Console.WriteLine(new string('-', 20));
            stopwatch.Stop();
            Console.WriteLine("Galutinis rez: " + finalNotOptimised +
                " metodas kviestas kartu: " + notOptimizedMethodCalls +
                " ciklas sukosi kartu: " + notOptimizedLoopCalls +
                " uztruko laiko: " + stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();
            stopwatch.Start();
            Console.WriteLine("Optimizuota kvieciame su:");
            int finalOptimised = FOpt(arg, w, v);
            stopwatch.Stop();
            Console.WriteLine("Galutinis rez opt: " + finalOptimised +
                " metodas kviestas kartu: " + optimizedMethodCalls +
                " ciklas sukosi kartu: " + optimizedLoopCalls +
                " uztruko laiko: " + stopwatch.ElapsedMilliseconds);
        }

        public string FindLongestPalindrome(string word)
        {
            string longestPalindrome = string.Empty;
            for (int searchLength = 2; searchLength < word.Length; searchLength++)
            {
                for (int index = 0; index < word.Length; index++)
                {
                    if (index + searchLength <= word.Length)
                    {
                        string section = word.Substring(index, /*index + */searchLength);
                        if (WordIsPalindrome(section) && section.Length > longestPalindrome.Length)
                        {
                            longestPalindrome = section;
                        }
                    }
                }
            }
            if (longestPalindrome.Length != 0)
            {
                return longestPalindrome;
            }
            return word;
        }

        public bool WordIsPalindrome(string word)
        {
            float wordMiddle = word.Length / 2;
            if (word.Length == 2)
            {
                return word[0] == word[1];
            }
            int leftIndex = (int)wordMiddle - 1;
            int rightIndex = wordMiddle == 1 ? 2 : word.Length % wordMiddle == 0 ? (int)wordMiddle : (int)wordMiddle + 1;
            while (leftIndex >= 0 && rightIndex < word.Length)
            {
                if (word[leftIndex] != word[rightIndex])
                {
                    return false;
                }
                leftIndex--;
                rightIndex++;
            }
            return true;
        }

        private int F(int W, int[] w, int[] v, int max = 0)
        {
            //notOptimizedMethodCalls++;
            if (W <= 0)
            {
                return 0;
            }
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("w[i]={0} W={1} v[i]={2} max={3}",
                            w[i], W, v[i], max);
                int tarpinis = F(W - w[i], w, v, max);
                //notOptimizedLoopCalls++;
                Console.WriteLine("tarpinis issiskaiciavo su: W={0} w[i]={1} v[i]={2}", W, w[i], v[i]);
                if (w[i] <= W && tarpinis + v[i] > max)
                {
                    max = tarpinis + v[i];
                    Console.WriteLine("max issiskaiciavo su: tarpinis={0} v[i]={1}", tarpinis, v[i]);
                }
            }
            return max;
        }

        private int FOpt(int W, int[] w, int[] v, int max = 0, List<int> cachedW = null, List<int> cachedTarpinis = null)
        {
            Console.WriteLine(W);
            optimizedMethodCalls++;
            if (cachedW == null)
            {
                cachedW = new List<int>();
            }
            if (cachedTarpinis == null)
            {
                cachedTarpinis = new List<int>();
            }
            if (W <= 0)
            {
                return 0;
            }
            if (!cachedW.Contains(W))
            {
                for (int i = 0; i < n; i++)
                {
                    int tarpinis = FOpt(W - w[i], w, v, max, cachedW, cachedTarpinis);
                    if (!cachedTarpinis.Contains(tarpinis))
                    {
                        if (w[i] <= W && tarpinis + v[i] > max)
                        {
                            max = tarpinis + v[i];
                            Console.WriteLine("w[i]={0} W={1} tarpinis={2} v[i]={3} max={4}",
                                w[i], W, tarpinis, v[i], max);
                        }
                        optimizedLoopCalls++;
                        cachedTarpinis.Add(tarpinis);
                    }
                }
                cachedW.Add(W);
            }
            return max;
        }

    }
}
