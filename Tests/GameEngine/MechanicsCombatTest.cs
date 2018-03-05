using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using ShipOfPassage.GameEngine.Mechanics;
using ShipOfPassage.GameEngine.Models;

namespace ShipOfPassage.Tests.GameEngine
{
    [TestFixture]
    public class MechanicsCombatTest
    {
        private Combatant smallEnemy;
        private Combatant mediumEnemy;
        private Combatant largeEnemy;
        private Combatant mockPlayer;

        [SetUp]
        public void SetUp()
        {
            string testDesc = "Test description.";
            smallEnemy =  new Enemy(1, "Small Enemy", testDesc, null, 100, 100, 50);
            mediumEnemy = new Enemy(2,"Medium Enemy", testDesc, null, 300, 200, 100);
            largeEnemy = new Enemy(3, testDesc, testDesc, null, 500, 300, 130);
            mockPlayer = new Player("Test Player", "Test Mc'Testor", DateTime.Now, 1);
        }

        [Test]
        public void AttackDoesDamageToEnemy()
        {
            Combat.Attack(mockPlayer, ref smallEnemy);
            Assert.AreNotEqual(100, ((Enemy)smallEnemy).SP, $"{((Enemy)smallEnemy).Name} did not take damage.");
            Combat.Attack(mockPlayer, ref mediumEnemy);
            Assert.AreNotEqual(200, ((Enemy)mediumEnemy).SP, $"{((Enemy)mediumEnemy).Name} did not take damage.");
        }

        [Test]
        public void PlayerNeverHasZeroDamage()
        {
            for(int i=0; i<300; i++)
            { 
                int testDamage = mockPlayer.GetDamage();
                Assert.Greater(testDamage,0, "Player got a zero for damage.");
                
            }
        }
        [Test]
        public void PlayerWinsEasyBattle()
        {
            while (!mockPlayer.WasDestroyed() && !smallEnemy.WasDestroyed())
            {
                Combat.Attack(mockPlayer, ref smallEnemy);
                if (!smallEnemy.WasDestroyed())
                    Combat.Attack(smallEnemy, ref mockPlayer);
            }
            Assert.True(smallEnemy.WasDestroyed());
            Assert.False(mockPlayer.WasDestroyed(), "Player lost an easy battle. This shouldn't happen.");
        }
    }
}