using ToK.Common.Game;
using ToK.GameServer.Game;

namespace ToK.GameServer.Game
{
    public partial class CGameController
    {
        /*
         * TODO: nesta classe devem ficar todos os objetos que representam o canal da "tmsrv".
         * Para cada canal, o caller deve instanciar 1 objeto deste.
         * 
         * Criar coisas como: MobGridMap, SpawnedMobs, etc.
         */

        private EPlayerState[] PlayerState { get; set; }

        public EPlayerState GetPlayerState(CPlayer player)
        {
            if (player.HaveValidIndex)
                return PlayerState[player.Index];
            else
                return EPlayerState.UNITIALIZED;
        }

        public CGameController()
        {
            PlayerState = new EPlayerState[GameBasics.MAX_PLAYER];
        }
    }
}