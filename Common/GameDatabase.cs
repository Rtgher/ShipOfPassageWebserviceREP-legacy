using System;
using ShipOfPassage.GameEngine.Models;
using MySql.Data.MySqlClient;
using ShipOfPassage.Common.Exceptions;

namespace ShipOfPassage.Common
{
    /// <summary>
    /// A class specifically to conenct to the Game Database on the 
    /// AWS cloud.
    /// </summary>
    public class GameDatabase
    {
        // Database user login info.
        private string _dbUid = "username";  //TODO: Encrypt these.
        private string _pass = "password"; //TODO: Encrypt these.
        // The SQL connection data.
        private string _serverAddress = "shipofpassagemysql.c7x8cl3psxgr.eu-west-2.rds.amazonaws.com";
        private string _databaseName = "ShipOfPassage";
        private string _connString = "Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;";
        // The SQL Connection Object.
        private MySqlConnection conn = null;
        // The MySQl Reader Object.
        private MySqlDataReader reader;

        /// <summary>
        /// Basic constructor. Sets up the connection and 
        /// starts it.
        /// </summary>
        public GameDatabase()
        {
            //TODO: get encrypted strings.

            // build the connection string.
            _connString = _connString.Replace("myServerAddress", _serverAddress);
            _connString = _connString.Replace("myDataBase", _databaseName);
            _connString = _connString.Replace("myUsername", _dbUid);
            _connString = _connString.Replace("myPassword", _pass);

            //start the connection.
            conn = new MySqlConnection();
            conn.ConnectionString = _connString;
            try
            {
                conn.Open();
            }
            catch (MySqlException sqle)
            {
                // Print stack trace and error info.
                Console.WriteLine("Caught SQL Exception:" + sqle);
                Console.WriteLine("Exception Message: " + sqle.Message);
                Console.WriteLine(sqle.StackTrace);
                //set conn back to null to avoid any weird behaviour down the line.
                conn = null;
            }
        }

        /// <summary>
        /// Queries the database for a specific StoryCard.
        /// Returns a StoryCard made from the data in the Database.
        /// </summary>
        /// <param name="id">The ID unique identifier of the StoryCard.</param>
        /// <returns></returns>
        public StoryCard GetStoryCardById(int id)
        {
            MySqlCommand command =  new MySqlCommand("SELECT * FROM StoryCard WHERE idCard = @idVal", conn);
            command.Parameters.AddWithValue("idVal", id);
            StoryCard retStoryCard = null;
            // Create new SqlDataReader object and read data from the command.
            using (reader = command.ExecuteReader())
            {
                reader.Read();
                if (reader.HasRows)
                { 
                    int cardID = (int) reader[0];
                    string ssmlContent = (string) reader[1];
                    CardType cardType = (CardType) Enum.Parse(typeof(CardType), (string) reader[2]);
                    int nextCardId = (int) reader[3];
                    string cardConten = (string) reader[5];
                    int eventId;
                    CardEvent cardEvent = null;
                    try
                    {
                        eventId = (int) reader[4];
                        cardEvent= GetCardEventById(eventId);
                    }
                    catch (Exception ex)
                    {
                        if (ex is InvalidCastException || ex is GameDatabaseException)
                        {
                            eventId = 0;
                            cardEvent = null;
                        }
                        else throw ex;
                    }
                    //Close the reader. We don't need it anymore. Allows for another operation to be run.
                    reader.Close();

                    // create the new StoryCard
                    retStoryCard = new StoryCard(cardID, ssmlContent, cardType, nextCardId, cardEvent); 
                }
                else
                {
                    reader.Close();
                    throw new GameDatabaseException("Statement did not return any rowns.");
                }

                if (retStoryCard == null)
                    Console.WriteLine("WARNING: Null Value for Return StoryCard. Searched by ID= "+ id);
                return retStoryCard;
            }
        }

        /// <summary>
        /// Queries the database for a specific StoryCard Event.
        /// Returns a CardEvent object made from the values in the Database
        /// </summary>
        /// <param name="id">The ID identifier of the event.</param>
        /// <returns></returns>
        public CardEvent GetCardEventById(int id = 0)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM Event WHERE idEvent = @idVal", conn);
            command.Parameters.AddWithValue("idVal", id);
            CardEvent cardEvent = null;
            using (reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    // Create Choices

                    // create the new event.
                    cardEvent = new CardEvent((int) reader[0], (string) reader[1], (Choice) reader[2],
                        (Choice) reader[3],
                        (Choice) reader[4]);

                }
                else
                {
                    throw new GameDatabaseException("Statement did not return any rowns.");
                }
            }
            reader.Close();

            return cardEvent;
            
        }

        /// <summary>
        /// Queries the database for a specific Player.
        /// Returns the player object with the data from the database.
        /// Throws a GameDatabaseException if it doesn't return anything.
        /// </summary>
        /// <param name="id">The ID identifier of the player.</param>
        /// <returns>The Player object.</returns>
        public Player GetPlayerById(string id)
        {
            Player returnPlayer = null;

            MySqlCommand command = new MySqlCommand("SELECT CharacterName, CurrentCard, Scene, lastSession FROM Player WHERE idPlayer LIKE @idVal", conn);
            command.Parameters.AddWithValue("idVal", id);
            using (reader = command.ExecuteReader())
            {
                reader.Read();
                if (reader.HasRows)
                {
                    string characterName;
                    try
                    {
                        characterName = (string) reader[0];
                    }
                    catch (InvalidCastException)
                    {
                        characterName = ""; 
                    }
                    int currentCard = (int) reader[1];
                    int currentScene;
                    try
                    {
                        currentScene = (int) reader[2];
                    }
                    catch (InvalidCastException)
                    {
                        currentScene = 0;
                    }
                    DateTime lastSession = DateTime.Now;
                    try
                    {
                        lastSession = (DateTime) reader[3];
                    }
                    catch (InvalidCastException)
                    {
                        Console.WriteLine("WARNING: Could not complete cast for DateTime of the player's last session.");
                    }
                    //create the Player Object.
                    returnPlayer =  new Player(id, characterName,lastSession);
                    if (currentScene > 0)
                        returnPlayer.SceneNumber = currentScene;

                }
                else
                {
                    reader.Close();
                    throw new GameDatabaseException("No rows returned for query for player for id =" + id);
                }
            }

            return returnPlayer;
        }

        /// <summary>
        /// Create a new Player object in the database.
        /// </summary>
        /// <param name="player">The Player Object</param>
        /// <returns>True is the player was added successfully.</returns>
        public bool AddNewPlayer(Player player)
        {
            MySqlCommand command =  new MySqlCommand("INSERT INTO Player(idPlayer, CurrentCard, lastSession) VALUES (@id, @idCard, @lastSession);",conn);
            command.Parameters.AddWithValue("id", player.ID);
            command.Parameters.AddWithValue("idCard", player.NextCardID);
            command.Parameters.AddWithValue("lastSession", player.LastSession);

            int result = command.ExecuteNonQuery();
            if (result <= 0) return false;
            return true;
        }

        /// <summary>
        /// Close the current connection.
        /// Then Disposes it.
        /// </summary>
        public void CloseConnection()
        {
            conn.Close();
            conn.Dispose();
        }

    }
}
