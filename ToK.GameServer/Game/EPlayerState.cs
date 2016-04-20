namespace ToK.GameServer.Game
{
    /// <summary>
    /// Represents the actual state of the player in the game.
    /// </summary>
    public enum EPlayerState
    {
        /// <summary>
        /// Setted when the system asked to shutdown the player.
        /// </summary>
        CLOSED = 0,
        /// <summary>
        /// Waiting to be inserted in the GameController. The player have just been created and don't sent the INIT_CODE packet yet.
        /// </summary>
        UNITIALIZED,
        /// <summary>
        /// Setted when processing the login request.
        /// </summary>
        PROCESSING_LOGIN,
        /// <summary>
        /// Setted when the login process have success. The player is in the character selecion step.
        /// </summary>
        SEL_CHAR,
    }
}