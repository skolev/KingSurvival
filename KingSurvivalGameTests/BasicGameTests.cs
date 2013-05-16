using System;
using KingSurvivalGame.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace KingSurvivalGame.Tests
{
    [TestClass]
    public class BasicGameTests
    {
        [TestMethod]
        public void TestValidateCommandValidKingInputUpLeft()
        {
            Game game = new Game();
            game.MovesCount = 2;
            bool result = game.ValidateCommand("KUL");
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestValidateCommandValidKingInputUpRight()
        {
            Game game = new Game();
            game.MovesCount = 2;
            bool result = game.ValidateCommand("KUR");
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestValidateCommandValidKingInputDownRight()
        {
            Game game = new Game();
            game.MovesCount = 2;
            bool result = game.ValidateCommand("KDR");
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestValidateCommandValidKingInputDownLeft()
        {
            Game game = new Game();
            game.MovesCount = 2;
            bool result = game.ValidateCommand("KDL");
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestValidateCommandEmptyString()
        {
            Game game = new Game();
            game.MovesCount = 2;
            bool result = game.ValidateCommand(string.Empty);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestValidateCommandKingInputEmptyString()
        {
            Game game = new Game();
            game.MovesCount = 2;
            bool result = game.ValidateCommand(string.Empty);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestValidateCommandKingInputInvalidCommand()
        {
            Game game = new Game();
            game.MovesCount = 2;
            bool result = game.ValidateCommand("KFC");
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestValidateCommandValidAPawnCommand()
        {
            Game game = new Game();
            game.MovesCount = 1;
            bool result = game.ValidateCommand("AR");
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestValidateCommandValidBPawnCommand()
        {
            Game game = new Game();
            game.MovesCount = 1;
            bool result = game.ValidateCommand("BR");
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestValidateCommandValidCPawnCommand()
        {
            Game game = new Game();
            game.MovesCount = 1;
            bool result = game.ValidateCommand("CL");
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestValidateCommandValidDPawnCommand()
        {
            Game game = new Game();
            game.MovesCount = 1;
            bool result = game.ValidateCommand("DL");
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestValidateCommandInvalidPawnCommand()
        {
            Game game = new Game();
            game.MovesCount = 1;
            bool result = game.ValidateCommand("SL");
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestValidateCommandPawnCommandEmptyString()
        {
            Game game = new Game();
            game.MovesCount = 1;
            bool result = game.ValidateCommand(string.Empty);
        }

        [TestMethod]
        public void TestCoordinatesInBoardMinimalValues()
        {
            BindingFlags eFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            Game game = new Game();
            int[] values = new int[] { 2, 3 };
            object[] arguments = new object[] { values };
            MethodInfo testedMethod = typeof(Game).GetMethod("CheckIfInBoard", eFlags);
            bool result = (bool)testedMethod.Invoke(game, arguments);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestCoordinatesInBoardMaximalValues()
        {
            BindingFlags eFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            Game game = new Game();
            int[] values = new int[] { 9, 11 };
            object[] arguments = new object[] { values };
            MethodInfo testedMethod = typeof(Game).GetMethod("CheckIfInBoard", eFlags);
            bool result = (bool)testedMethod.Invoke(game, arguments);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestCoordinatesInBoardRandomValues()
        {
            BindingFlags eFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            Game game = new Game();
            int[] values = new int[] { 8, 7 };
            object[] arguments = new object[] { values };
            MethodInfo testedMethod = typeof(Game).GetMethod("CheckIfInBoard", eFlags);
            bool result = (bool)testedMethod.Invoke(game, arguments);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestCoordinatesInBoardInvalidRowValue()
        {
            BindingFlags eFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            Game game = new Game();
            int[] values = new int[] { 1, 10 };
            object[] arguments = new object[] { values };
            MethodInfo testedMethod = typeof(Game).GetMethod("CheckIfInBoard", eFlags);
            bool result = (bool)testedMethod.Invoke(game, arguments);
            Assert.AreEqual(false, result);
        }

        public void TestCoordinatesInBoardInvalidColValue()
        {
            BindingFlags eFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            Game game = new Game();
            int[] values = new int[] { 5, 10 };
            object[] arguments = new object[] { values };
            MethodInfo testedMethod = typeof(Game).GetMethod("CheckIfInBoard", eFlags);
            bool result = (bool)testedMethod.Invoke(game, arguments);
            Assert.AreEqual(false, result);
        }
    }
}
