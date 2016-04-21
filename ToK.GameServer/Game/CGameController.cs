using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using ToK.Common.Game;
using ToK.Common.Network;
using ToK.Common.Network.PacketStructures;
using ToK.Common.Utility;

namespace ToK.GameServer.Game
{
    public partial class CGameController
    {
        public bool TryInsertPlayer(CPlayer player)
        {
            short validIndex = 1;

            for (; validIndex < GameBasics.MAX_PLAYER; validIndex++)
            {
                if (PlayerState[validIndex] == EPlayerState.CLOSED)
                    break;
            }

            if (validIndex >= GameBasics.MAX_PLAYER)
                return false;

            player.Index = validIndex;

            PlayerState[player.Index] = EPlayerState.UNITIALIZED;

            return true;
        }

        public void DisconnectPlayer(CPlayer player)
        {
            if(GetPlayerState(player) != EPlayerState.CLOSED)
            {
                // TODO: send the dced spawn in the visible area around the player.
                // TODO: proceed removind the player of all the game state: mob grid, spawned mobs, etc.

                PlayerState[player.Index] = EPlayerState.CLOSED;

                MyLog.Write($"The player {player.Index} was disconnected from the server.", ELogType.GAME_EVENT);
            }
        }

        public EPacketError TryProcessPacket(CPlayer player)
        {
            EPacketError err = EPacketError.NO_ERROR;

            try
            {
                // Switch for the packet opcode.
                switch (player.RecvPacket.ReadNextUShort(4))
                {
                    case MAccountLoginPacket.Opcode:
                        err = ProcessAccountLogin(player);
                        break;

                    case MPingPacket.Opcode:

                        break;

                    default:
                        err = EPacketError.PACKET_NOT_HANDLED;
                        break;
                }
            }
            catch (Exception)
            {
                err = EPacketError.UNKNOWN;
            }

            return err;
        }
    }
}