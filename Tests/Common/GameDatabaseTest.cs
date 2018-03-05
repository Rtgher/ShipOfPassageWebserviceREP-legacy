using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ShipOfPassage.Common;
using ShipOfPassage.GameEngine.Models;

namespace ShipOfPassage.Tests
{
    /// <summary>
    /// Test Class for the Database.
    /// All database-related tests should be in here.
    /// </summary>
    [TestFixture]
    public class GameDatabaseTest
    {
        /// <summary>
        /// An instance of the database.
        /// </summary>
        private GameDatabase database;
        // a mock id.
        private string _testPlayerID = "testPlayer";

        /// <summary>
        /// Setup the test suite.
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            if(database!=null)database.CloseConnection();
            database = new GameDatabase();
        }

        /// <summary>
        /// Test that database can return a valid card.
        /// </summary>
        [Test]
        [TestCase(1, TestName = "DefaultTestWith1")]
        public void GetCardGetsValidCard(int id, CardType cardType = CardType.System)
        {
            StoryCard testStoryCard = database.GetStoryCardById(id);

            Assert.NotNull(testStoryCard, "StoryCard returned was null.");
            Assert.AreEqual(testStoryCard.ID, id, "Bad ID returned... somehow...?");
            Assert.AreEqual(testStoryCard.Type, cardType, "Bad/Wrong StoryCard type :(");
        }

        /// <summary>
        /// Adds a new player to the database.
        /// </summary>
        [Test]
        [Ignore("Test only needs to work once.")]
        public void AddNewPlayerReturnsTrue()
        {
            Player newPlayer = new Player(_testPlayerID, "", DateTime.Now);
            Assert.IsTrue(database.AddNewPlayer(newPlayer));
        }

        /// <summary>
        /// Try to query for the player.
        /// </summary>
        [Test]
        public void GetPlayerByID()
        {
            Player newPlayer = database.GetPlayerById(_testPlayerID);
            Assert.NotNull(newPlayer, "Failed to get valid player.");
            Assert.AreEqual(newPlayer.ID, _testPlayerID, "Wrong player returned. invalid ID.");
        }

        /// <summary>
        /// Tear down the test suite.
        /// </summary>
        [OneTimeTearDown]
        public void TearDown()
        {
            database.CloseConnection();
        }
    }
}