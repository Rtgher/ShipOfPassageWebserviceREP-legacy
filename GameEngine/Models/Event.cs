using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ShipOfPassage.GameEngine.Models
{
    /// <summary>
    /// An Enum for the StoryCard EventId Type.
    /// </summary>
    public enum CardEventType
    {
        //EventId consists of a set of choices.
        Choice,
        //EventId consists of a set of combatants.
        Combat
    }
    /// <summary>
    /// The EventId class of the Game. Contains information
    /// relating to events happening in the game.
    /// </summary>
    public class Event
    {
        // ID identifier.
        public readonly int ID;
        // The Type of StoryCard.
        public CardEventType Type;
        // The Choices for this EventId;
        public Choice[] Choices;
        // The enemies contained in the event.
        public Enemy[] Enemies;

    }
}