using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipOfPassage.GameEngine.Models
{
    /// <summary>
    /// The Player Class. Contains information about the player.
    /// </summary>
    public class Player : Combatant
    {
        // The ID of the current card.
        public int NextCardID { get; set; }
        // The current scene, if available.
        public int SceneNumber { get; set; }
        // ID identifier of the player
        public readonly string ID;
        // The player's name.
        public string CharacterName;
        // The DateTime value of when the player last had a session open.
        public DateTime LastSession { get; set; }
        /// <summary>
        /// The Hull points of the player ship. Currently defaulted to 300. 
        /// Should be set via database. 
        /// </summary>
        public int HP { get; private set; }
        /// <summary>
        /// The Shield Points of the player ship.
        /// </summary>
        public int SP { get; private set; }
        /// <summary>
        /// The damage that the player can dish out.
        /// </summary>
        public int Damage { get; private set; }

        /// <summary>
        /// Initialiser for player. Only create once.
        /// </summary>
        /// <param name="id">The ID idenifier of the player.</param>
        /// <param name="characterName">The character name.</param>
        public Player(string id, string characterName, DateTime lastSession, int nextCardId = 0)
        {
            this.ID = id;
            this.CharacterName = characterName;
            this.LastSession = lastSession;
            this.NextCardID = nextCardId;
            HP = 300;
            SP = 200;
            Damage = 100;
        }

        #region Combatant

        public void TakeDamage(int damage)
        {
            if (SP > 0)
            {
                SP -= damage;
                if (SP < 0)
                {
                    HP += SP;//take excess shields damage out of the HP.
                }
            }
        }

        public bool WasDestroyed()
        {
            if (HP <= 0) return true;
            else return false;
        }

        public string ReportDamage()
        {
            string shieldsStatus = "unknown";
            string hullStatus = "unknown";
            if (HP > 0)
            {
                if (HP == 300) hullStatus = "at full capacity";
                else
                if (HP > 200) hullStatus = "solid";
                else if (HP > 100) hullStatus = "under 50%. Heavy damage sustained";
                else if (HP > 50) hullStatus = "compromised. Reccomend immediate withdrawal";
                else hullStatus = "in critical conditions. Internal systems failure detected";
            }
            else
            {
                return "Target lost. Enemy destroyed.";

            }
            if (SP > 0)
            {
                if (SP == 200) shieldsStatus = "at full capacity";
                else
                if (SP > 150) shieldsStatus = "lightly damaged";
                if (SP > 100) shieldsStatus = "holding strong";
                else if (SP > 50) shieldsStatus = "failing";
                else if (SP > 0) shieldsStatus = "in critical condition";
            }
            else shieldsStatus = "disabled";

            string response = $"Enemy shields are {shieldsStatus}. Hull integrity {hullStatus}.";

            return response;
        }

        public int GetDamage()
        {
            Random random = new Random();
            int damage = Damage;
            float mod = (float)random.NextDouble();
            return (int)(damage * mod * 1.5);//player damage is slightly increased by 50% to quicken combat.
        }

        #endregion

    }
}