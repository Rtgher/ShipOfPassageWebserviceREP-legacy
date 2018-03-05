using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShipOfPassage.GameEngine.Models;

namespace ShipOfPassage.GameEngine.Mechanics
{
    /// <summary>
    /// A static Combat Controller that processes combat between two combatants.
    /// </summary>
    public static class Combat
    {
        /// <summary>
        /// Makes the two combatants fight. 
        /// </summary>
        /// <param name="aggresor">The agressor passed by val</param>
        /// <param name="defendant">The defendant passed by ref</param>
        /// <returns>Report of the defendant's status.</returns>
        public static string Attack(Combatant aggresor, ref Combatant defendant)
        {
            defendant.TakeDamage(aggresor.GetDamage());
            return defendant.ReportDamage();
        }
    }
}