using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipOfPassage.Common.Exceptions
{
    /// <summary>
    /// A Game Database Exception. Use this to throw errors relating to the 
    /// Database, either not running properly or returning invalid data baclk.
    /// </summary>
    public class GameDatabaseException : Exception
    {
        public GameDatabaseException() : base(){}
        public GameDatabaseException(string message) : base(message){ }
        public GameDatabaseException(string message, Exception inner ) : base(message, inner) { }
    }
}