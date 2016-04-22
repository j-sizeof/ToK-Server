using ToK.Common;

namespace ToK.GameState
{
    public class CGameStateController
    {
        /*
         * TODO: nesta classe devem ficar todos os objetos que representam o canal da "tmsrv".
         * Para cada canal, o caller deve instanciar 1 objeto deste.
         * 
         * Criar coisas como: MobGridMap, SpawnedMobs, etc.
         */

        public CPlayerState PlayerState { get; set; }

        public CGameStateController()
        {
            PlayerState = new CPlayerState();
        }

        public bool TryInsertPlayer(CPlayer player)
        {
            short validIndex = 1;

            for (; validIndex < NetworkBasics.MAX_PLAYER; validIndex++)
            {
                if (PlayerState[validIndex] == EPlayerState.CLOSED)
                    break;
            }

            if (validIndex >= NetworkBasics.MAX_PLAYER)
                return false;

            player.Index = validIndex;

            PlayerState[player.Index] = EPlayerState.UNITIALIZED;

            return true;
        }

        public void DisconnectPlayer(CPlayer player)
        {
            if (PlayerState[player] != EPlayerState.CLOSED)
            {
                // TODO: send the dced spawn in the visible area around the player.
                // TODO: proceed removind the player of all the game state: mob grid, spawned mobs, etc.

                PlayerState[player] = EPlayerState.CLOSED;

                MyLog.Write($"The player {player.Index} was disconnected from the server.", ELogType.GAME_EVENT);
            }
        }
    }
}