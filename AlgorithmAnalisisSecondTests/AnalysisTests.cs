using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmAnalisisSecond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmAnalisisSecond.Tests
{
    [TestClass()]
    public class AnalysisTests
    {

        private Analysis analysis = new Analysis();

        private List<string> testPalindromes = new List<string>
        {
            "anna",
            "civic",
            "kayak",
            "level",
            "madam",
            "mom",
            "noon",
            "racecar",
            "radar",
            "redder",
            "refer",
            "repaper",
            "rotator",
            "rotor",
            "sagas",
            "solos",
            "stats",
            "tenet",
            "wow",
        };

        [TestMethod()]
        public void SecondPartTest()
        {
            foreach (string palindome in testPalindromes)
            {
                int amountBefore = analysis.SecondPart(palindome);
                Assert.AreEqual(0, amountBefore);
            }
        }

        [TestMethod()]
        public void WordIsPalindromeTest()
        {
            foreach (string palindrome in testPalindromes)
            {
                Assert.IsTrue(analysis.WordIsPalindrome(palindrome));
            }
            List<string> testWords = new List<string>() {
                "Mantas",
                "Lukas",
                "Arvydas",
                "Audrius",
                "Raimundas"
            };
            foreach (string notPalindrome in testWords)
            {
                Assert.IsFalse(analysis.WordIsPalindrome(notPalindrome));
            }
        }
    }
}