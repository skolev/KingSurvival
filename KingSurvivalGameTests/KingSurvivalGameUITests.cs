using System;
using KingSurvivalGame.Common;
using KingSurvivalGame.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KingSurvivalGame.Tests
{
    [TestClass]
    public class KingSurvivalGameUITests
    {
        [TestMethod]
        public void TestGameGrid()
        {
            Game testGame = new Game();
            string resultField = testGame.GetGridAsString();
            string expected = string.Format(
                "{0}" +
                "UL  0 1 2 3 4 5 6 7  UR{0}" +
                "   _________________   {0}" +
                "0 | A   B   C   D   | 0{0}" +
                "1 |                 | 1{0}" +
                "2 |                 | 2{0}" +
                "3 |                 | 3{0}" +
                "4 |                 | 4{0}" +
                "5 |                 | 5{0}" +
                "6 |                 | 6{0}" +
                "7 |       K         | 7{0}" +
                "  |_________________|  {0}" +
                "DL  0 1 2 3 4 5 6 7  DR{0}{0}",
                Environment.NewLine
                );
            Assert.AreEqual<string>(expected, resultField);
        }

        // TODO example game session test
    }
}
