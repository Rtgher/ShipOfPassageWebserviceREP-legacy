using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ShipOfPassage.GameEngine.Models
{
    /// <summary>
    /// The primitive class for any Objects in the game.
    /// </summary>
    public abstract class GameObject
    {
        // The Identifier.
        public readonly int ID;
        // The Name of the object.
        public readonly string Name;
        // The common object description for any objects.
        public readonly string Description;
        // The Parent Event this Enemy belongs to.
        public readonly Event Parent;
        // The Sprite for this object.
        public Image Sprite { get; set; }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">The ID identifier</param>
        /// <param name="description">The description for this Object</param>
        /// <param name="parent">The parent Event of this file.</param>
        /// <param name="sprite">The sprite to be rendered in the game card if needed.</param>
        protected GameObject(int id, string name, string description, Event parent, Image sprite = null)
        {
            this.ID = id;
            this.Name = name;
            this.Description = description;
            this.Parent = parent;
            this.Sprite = null;
        }
    }
}