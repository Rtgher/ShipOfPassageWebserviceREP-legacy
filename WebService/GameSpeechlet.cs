using AlexaSkillsKit.Speechlet;
using System;
using AlexaSkillsKit.UI;
using ShipOfPassage.Common;
using ShipOfPassage.Common.Exceptions;
using ShipOfPassage.GameEngine.Models;

namespace ShipOfPassage.WebService
{
    /// <summary>
    /// The Game Speechlet class used to parse and process Alexxa's Speechlet requests.
    /// Extends the basic Speechlet class provided in the Alexa SkillKit.
    /// </summary>
    public class GameSpeechlet : Speechlet
    {
        /// <summary>
        /// An instance of the Game Database.
        /// </summary>
        private GameDatabase _database;
        // A reference to the current session user.
        private User _user;

        /// <summary>
        /// Sends a response upon receiving an Intent request.
        /// </summary>
        /// <param name="intentRequest">The request</param>
        /// <param name="session">The current session.</param>
        /// <returns> A new Speechlet Response.</returns>
        public override SpeechletResponse OnIntent(IntentRequest intentRequest, Session session)
        {
            User user = session.User;
            
            throw new NotImplementedException();
        }
        /// <summary>
        /// Sends a response upon receiving a Launch request.
        /// </summary>
        /// <param name="intentRequest">The request</param>
        /// <param name="session">The current session.</param>
        /// <returns>A SpeechletResponse.</returns>
        public override SpeechletResponse OnLaunch(LaunchRequest launchRequest, Session session)
        {
            _user = session.User;
            _database = new GameDatabase();

            Player player = null;
            //get user from db.
            try
            {
                player = _database.GetPlayerById(_user.Id);
            }
            catch (GameDatabaseException)
            {
                //create a new one if that fails.
                Console.WriteLine("No player found. Creating a new player.");
                player = new Player(_user.Id, "", DateTime.Now);
                createPlayer(player);
                return getCharacterCreationResponse(player);
            }

            return getNextStoryCard(player);
        }
        /// <summary>
        /// Sends a response upon receiving a Session Started request.
        /// </summary>
        /// <param name="intentRequest">The request</param>
        /// <param name="session">The current session.</param>
        /// <returns></returns>
        public override void OnSessionStarted(SessionStartedRequest sessionStartedRequest, Session session)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sends a response upon receiving a Session End request.
        /// </summary>
        /// <param name="intentRequest">The request</param>
        /// <param name="session">The current session.</param>
        /// <returns></returns>
        public override void OnSessionEnded(SessionEndedRequest sessionEndedRequest, Session session)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the first Character creation card and sets up the player.
        /// </summary>
        /// <param name="player">The player object.</param>
        /// <returns></returns>
        private SpeechletResponse getCharacterCreationResponse(Player player)
        {
            StoryCard createCharacterCard = _database.GetStoryCardById(StoryCard.PLAYER_CREATE_NAME);
            SimpleCard cardContent = new SimpleCard();
            cardContent.Content = createCharacterCard.CardContent;

            SsmlOutputSpeech speech = new SsmlOutputSpeech();
            speech.Ssml = createCharacterCard.SsmlContent;

            SpeechletResponse response =  new SpeechletResponse();
            response.Card = cardContent;
            response.OutputSpeech = speech;
            response.ShouldEndSession = false;

            return response;
        }

        /// <summary>
        /// A method to create a new player in the Database.
        /// </summary>
        /// <param name="player">The Player object to create.</param>
        /// <returns>The player object back.</returns>
        private Player createPlayer(Player player)
        {
            player.NextCardID = StoryCard.FIRST_STORY_CARD;
            bool result = _database.AddNewPlayer(player);
            if (!result)
                throw new GameDatabaseException("Error: Could not add the new player.");

            return player;
        }

        /// <summary>
        /// A method used to construct a speechlet response.
        /// Uses the Player object to get the next card in que.
        /// Will also change the current card positon of the 
        /// player once it sends the response.
        /// </summary>
        /// <param name="player">The player object.</param>
        /// <returns>SpeechletResponse</returns>
        private SpeechletResponse getNextStoryCard(Player player)
        {
            // get card and set the next card to the player.
            StoryCard currentStoryCard = _database.GetStoryCardById(player.NextCardID);
            player.NextCardID = currentStoryCard.NextCardId;
            // Create the Simple storyCard content.
            SimpleCard card = new SimpleCard();
            card.Content = currentStoryCard.CardContent;

            SsmlOutputSpeech speech = new SsmlOutputSpeech();
            speech.Ssml = currentStoryCard.SsmlContent;

            SpeechletResponse response = new SpeechletResponse();
            response.OutputSpeech = speech;
            response.Card = card;
            response.ShouldEndSession = false;

            return response;
        }
}
}