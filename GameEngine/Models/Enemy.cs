using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ShipOfPassage.GameEngine.Models
{
    /// <summary>
    /// The Enemy class.
    /// Contains information about the target enemy.
    /// </summary>
    public class Enemy : GameObject, Combatant
    {
        // Hull points of the ship.
        public int HP { get; set; }
        // Shield Points of the ship.
        public int SP { get; set; }
        // Maximum damage capacity of the enemy.
        public int Damage { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">ID for GameObejct.</param>
        /// <param name="name">The Name of the Game Object.</param>
        /// <param name="descripton">Description of the GameObject.</param>
        /// <param name="parent">Parent EventId for this Enemy.</param>
        /// <param name="hp">Hull points of enemy.</param>
        /// <param name="sp">Shield points of enemy.</param>
        /// <param name="damage">damage of the enemy.</param>
        public Enemy(int id, string name, string descripton, Event parent,
            int hp, int sp, int damage) : base(id, name, descripton,parent, null)
        {
            this.Damage = damage;
            this.HP = hp;
            this.SP = sp;
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
            if (HP <= 25) return true; //aproximate for leisure.
            else return false;
        }

        public string ReportDamage()
        {
            string shieldsStatus = "unknown";
            string hullStatus = "unknown";
            if (HP > 0)
            {
                if (HP > 200) hullStatus = "solid";
                else if (HP > 100) hullStatus = "heavily damaged";
                else if (HP > 50) hullStatus = "compromised";
                else hullStatus = "in critical conditions. Internal systems failure detected";
            }
            else
            {
                return "Target lost. Enemy destroyed.";

            }
            if (SP > 0)
            {
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
            return (int)(damage * mod);
        }

        #endregion

    }
}