using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipOfPassage.WebService
{
    /// <summary>
    /// An enum that holds the etypes of Intents in the game for ease of access purpouses.
    /// </summary>
    public enum Intents
    {
        /// <summary>
        /// An intent to name or create a new character.
        /// </summary>
        NameCharacter,
        /// <summary>
        /// An intent to progress the story by asking for a new card.
        /// </summary>
        GetNextCard,
        /// <summary>
        /// An intent to attack an enemy.
        /// </summary>
        AttackEnemy,
        /// <summary>
        /// An itnent to scan, or describe an obejct.
        /// </summary>
        ScanObject,
    }
}