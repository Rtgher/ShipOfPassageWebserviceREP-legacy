namespace ShipOfPassage.GameEngine.Models
{
    /// <summary>
    /// The Choice struct.
    /// </summary>
    public struct Choice
    {
        // The ID identifier
        public readonly int ID;
        // The StoryCard it will forward to Next.
        public readonly StoryCard Next;
        // The SsmlContent of this choice.
        public readonly string Content;
    }
}