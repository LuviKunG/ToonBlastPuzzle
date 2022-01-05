using System;

namespace ToonBlastPuzzle
{
    /// <summary>
    /// Exception for game development with message.
    /// </summary>
    public class GameDevelopmentException : Exception
    {
        /// <summary>
        /// (Private this because we don't want to throw exception without message.)
        /// </summary>
        private GameDevelopmentException() : base() { }
        public GameDevelopmentException(string message) : base(message) { }
        public GameDevelopmentException(string message, Exception innerException) : base(message, innerException) { }
    }
}