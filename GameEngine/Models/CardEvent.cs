using System.Collections.Generic;

namespace ShipOfPassage.GameEngine.Models
{
    /// <summary>
    /// The StoryCard Event class.
    /// </summary>
    public class CardEvent
    {
        // The public identifier.
        public readonly int ID;
        // The content string of the Event.
        public readonly string Content;
        // The choices 
        public Choice Choice1;
        public Choice Choice2;
        public Choice Choise3;

        /// <summary>
        /// Public constructor. Uses 3 Choice Values.
        /// </summary>
        /// <param name="id">Public identifier</param>
        /// <param name="content">String content</param>
        /// <param name="c1">Choice 1</param>
        /// <param name="c2">Choice 2</param>
        /// <param name="c3">Choice 3</param>
        public CardEvent(int id, string content, Choice c1, Choice c2, Choice c3)
        {
            this.ID = id;
            this.Content = content;
            this.Choice1 = c1;
            this.Choice2 = c2;
            this.Choise3 = c3;
        }

    }
}