using System;
using System.Reflection;
using KingSurvivalGame.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KingSurvivalGame.Tests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void TestGameGrid()
        {
            Game testGame = new Game();
            string resultField = testGame.GetGridAsString();
            string expected = string.Format(
                "{0}" +
                "UL 01234567 UR{0}" +
                "   ________   {0}" +
                "0 |A+B+C+D+| 0{0}" +
                "1 |+-+-+-+-| 1{0}" +
                "2 |-+-+-+-+| 2{0}" +
                "3 |+-+-+-+-| 3{0}" +
                "4 |-+-+-+-+| 4{0}" +
                "5 |+-+-+-+-| 5{0}" +
                "6 |-+-+-+-+| 6{0}" +
                "7 |+-+K+-+-| 7{0}" +
                "  |________|  {0}" +
                "DL 01234567 DR{0}{0}",
                Environment.NewLine);
            Assert.AreEqual<string>(expected, resultField);
        }

        [TestMethod]
        public void TestGetPawnDestinationDownRight()
        {
            BindingFlags bindingnFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            Game game = new Game();
            int[] values = new int[] { 2, 3 };
            object[] arguments = new object[] { values, 'R' };
            MethodInfo testedMethod = typeof(Game).GetMethod("GetPawnDestination", bindingnFlags);
            int[] result = (int[])testedMethod.Invoke(game, arguments);
            int[] expected = new int[] { 3, 4 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestGetPawnDestinationDownLeft()
        {
            BindingFlags bindingnFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            Game game = new Game();
            int[] values = new int[] { 2, 5 };
            object[] arguments = new object[] { values, 'L' };
            MethodInfo testedMethod = typeof(Game).GetMethod("GetPawnDestination", bindingnFlags);
            int[] result = (int[])testedMethod.Invoke(game, arguments);
            int[] expected = new int[] { 3, 4 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestPlayPawnAMoveLeft()
        {
            Game game = new Game();
            bool result = game.PlayPawnMove('A', 'L');
            bool expected = false;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestPlayPawnAMoveRight()
        {
            Game game = new Game();
            bool result = game.PlayPawnMove('A', 'R');
            bool expected = true;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestPlayPawnDMoveLeft()
        {
            Game game = new Game();
            bool result = game.PlayPawnMove('D', 'L');
            bool expected = true;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestPlayPawnDMoveRight()
        {
            Game game = new Game();
            bool result = game.PlayPawnMove('D', 'R');
            bool expected = true;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestPlayPawnInvalidPawn()
        {
            Game game = new Game();
            game.PlayPawnMove('E', 'R');
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestPlayPawnInvalidMove()
        {
            Game game = new Game();
            game.PlayPawnMove('E', 'D');
        }

        [TestMethod]
        public void TestPlayPawnIsStuck()
        {
            Game game = new Game();
            game.PlayPawnMove('A', 'L');
            bool result = game.CheckIfAllPawnsAreStuck();
            bool expected = false;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestGetKingDestinationUpLeft()
        {
            BindingFlags bindingnFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            Game game = new Game();
            int[] coordinates = new int[] { 9, 6 };
            object[] arguments = new object[] { coordinates, 'U', 'L' };
            MethodInfo testedMethod = typeof(Game).GetMethod("GetKingDestination", bindingnFlags);
            int[] result = (int[])testedMethod.Invoke(game, arguments);
            int[] expected = new int[] { 8, 5 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestGetKingDestinationUpRight()
        {
            BindingFlags bindingnFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            Game game = new Game();
            int[] coordinates = new int[] { 9, 6 };
            object[] arguments = new object[] { coordinates, 'U', 'R' };
            MethodInfo testedMethod = typeof(Game).GetMethod("GetKingDestination", bindingnFlags);
            int[] result = (int[])testedMethod.Invoke(game, arguments);
            int[] expected = new int[] { 8, 7 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestGetKingDestinationDownLeft()
        {
            BindingFlags bindingnFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            Game game = new Game();
            int[] coordinates = new int[] { 9, 6 };
            object[] arguments = new object[] { coordinates, 'D', 'L' };
            MethodInfo testedMethod = typeof(Game).GetMethod("GetKingDestination", bindingnFlags);
            int[] result = (int[])testedMethod.Invoke(game, arguments);
            int[] expected = new int[] { 10, 5 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestGetKingDestinationDownRight()
        {
            BindingFlags bindingnFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            Game game = new Game();
            int[] coordinates = new int[] { 9, 6 };
            object[] arguments = new object[] { coordinates, 'D', 'R' };
            MethodInfo testedMethod = typeof(Game).GetMethod("GetKingDestination", bindingnFlags);
            int[] result = (int[])testedMethod.Invoke(game, arguments);
            int[] expected = new int[] { 10, 7 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestPlayKingMoveUpLeft()
        {
            Game game = new Game();
            bool result = game.PlayKingMove('U', 'L');
            bool expected = true;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestPlayKingMoveDownLeft()
        {
            Game game = new Game();
            bool result = game.PlayKingMove('D', 'L');
            bool expected = false;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestPlayKingMoveUpRight()
        {
            Game game = new Game();
            bool result = game.PlayKingMove('U', 'R');
            bool expected = true;
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void TestPlayKingMoveDownRight()
        {
            Game game = new Game();
            bool result = game.PlayKingMove('D', 'R');
            bool expected = false;
            Assert.AreEqual(expected, result);
        }
    }
}
