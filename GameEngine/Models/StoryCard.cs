using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShipOfPassage.Common;

namespace ShipOfPassage.GameEngine.Models
{
    /// <summary>
    /// Enum detaiing the card type.
    /// </summary>
    public enum CardType
    {
        /// <summary>
        /// StoryCard that just Narrates the Story or actions to the player.
        /// </summary>
        Narrative,
        /// <summary>
        /// StoryCard that contains Combat information, requiring a combat action from the player.
        /// </summary>
        Combat,
        /// <summary>
        /// StoryCard that prompts a choice, and leads to a branch in the Story.
        /// </summary>
        Choice,
        /// <summary>
        /// A StoryCard that is a combination of other card types.
        /// </summary>
        Combo,
        /// <summary>
        /// A StoryCard that relates to system issues, and not the story. Default start, end and Error cards
        /// would be the most appropiate examples.
        /// </summary>
        System
    }

    /// <summary>
    /// StoryCard containing the Scene data and monologue.
    /// </summary>
    public class StoryCard
    {
        // A card pointing out a system error in the game.
        public static readonly int SYSTEM_EROOR_CARD = 1;

        // A card asking the player to state his name.
        public static readonly int PLAYER_CREATE_NAME = 3;

        /// <summary>
        /// The Id reference to the first Story Card.
        /// </summary>
        public static readonly int FIRST_STORY_CARD = 4;

        // Id from DB.
        public readonly int ID;
        // SsmlContent BLOB.
        public readonly String SsmlContent;
        // Text to be displayed on accompanying card.
        public readonly string CardContent;
        // StoryCard Type.
        public readonly CardType Type;
        // Next StoryCard if single.
        public readonly int NextCardId;
        // Array of cards if not single.
        public readonly int[] NextCardIds;
        // Event if applicable.
        public readonly CardEvent Event;

        /// <summary>
        /// Basic constructor for the StoryCard object.
        /// Uses the Database params. Single StoryCard follow.
        /// </summary>
        /// <param name="iD">The ID of the StoryCard</param>
        /// <param name="ssmlContent">The ssmlContent BLOB</param>
        /// <param name="type">The type of the StoryCard</param>
        /// <param name="nextCardId">The next card in queue</param>
        /// <param name="cardEvent">The evet if applicable.</param>
        public StoryCard(int iD, String ssmlContent, CardType type, int nextCardId, CardEvent cardEvent = null)
        {
            this.ID = iD;
            this.SsmlContent = ssmlContent;
            this.Type = type;
            this.NextCardId = nextCardId;
            this.Event = cardEvent;
        }
        /// <summary>
        /// Basic StoryCard Constructorfor the StoryCard Object.
        /// Uses a StoryCard Array for the nextCardIds instead of a single object.
        /// </summary>
        /// <param name="iD">The ID of the StoryCard</param>
        /// <param name="ssmlContent">The ssmlContent BLOB</param>
        /// <param name="type">The type of the StoryCard</param>
        /// <param name="nextCardIds">An Array containing the nextCardIds.</param>
        /// <param name="cardEvent">A card Event if applicable.</param>
        public StoryCard(int iD, String ssmlContent, CardType type, int[] nextCardIds, CardEvent cardEvent = null)
        {
            this.ID = iD;
            this.SsmlContent = ssmlContent;
            this.Type = type;
            this.NextCardIds = nextCardIds;
            this.Event = cardEvent;
        }
    }
}