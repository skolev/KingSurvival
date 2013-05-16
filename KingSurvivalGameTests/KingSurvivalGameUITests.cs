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
        public void TestUserInputValidKingCommand()
        {
            bool result = KingSurvivalGameUI.ExecuteCommand("KUL", new Game());
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestUserInputValidPawnCommand()
        {
            bool result = KingSurvivalGameUI.ExecuteCommand("AR", new Game());
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestUserInputInvalidKingCommand()
        {
            bool result = KingSurvivalGameUI.ExecuteCommand("KOL", new Game());
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUserInputInvalidommand()
        {
            KingSurvivalGameUI.ExecuteCommand("GIT", new Game());
        }
    }
}
