using System;
using ToK.Common;

namespace ToK.GameState
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

    /// <summary>
    /// The actual state of the players in the game cycle.
    /// </summary>
    public class CPlayerState
    {
        private EPlayerState[] m_PlayerState;

        public CPlayerState()
        {
            m_PlayerState = new EPlayerState[NetworkBasics.MAX_PLAYER];
        }

        /// <summary>
        /// Gets or sets the player state.
        /// Exceptions:
        ///     Exception when accessing this with a invalid player index.
        /// </summary>
        public EPlayerState this[CPlayer player]
        {
            get { return this[player.Index]; }
            set { this[player.Index] = value; }
        }

        public EPlayerState this[int index]
        {
            get
            {
                if (CPlayer.IsValidIndex(index))
                    return m_PlayerState[index];
                else
                    throw new Exception("Some player tried to access CPlayerState without a valid index.");
            }

            set
            {
                if (CPlayer.IsValidIndex(index))
                    m_PlayerState[index] = value;
                else
                    throw new Exception("Some player tried to access CPlayerState without a valid index.");
            }
        }
    }
}