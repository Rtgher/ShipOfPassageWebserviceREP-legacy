using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipOfPassage.GameEngine.Models
{
    /// <summary>
    /// An interface that contains some basic methods required for 
    /// objects to combat each other.
    /// </summary>
    public interface Combatant
    {

        /// <summary>
        /// Tells the object to take damage and calculate its own hp.
        /// </summary>
        void TakeDamage(int damage);
        /// <summary>
        /// A getter that checks if the ship is destroyed or is still functioning.
        /// </summary>
        /// <returns>True if the ship still has HP or SP.</returns>
        bool WasDestroyed();
        /// <summary>
        /// Ask the combatant to report its condition in a verbal-friendly manner.
        /// </summary>
        /// <returns>A string to add to the report card.</returns>
        string ReportDamage();
        /// <summary>
        /// A handle for the damage power of the combatant.
        /// Not just a getter, it also modifies the damage 
        /// by applying random factors.
        /// </summary>
        /// <returns>The calculated damage.</returns>
        int GetDamage();
    }
}
