using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using ToK.Common.Game;
using ToK.Common.Network;
using ToK.Common.Network.PacketStructures;

namespace ToK.GameServer.Game
{
    public partial class CGameController
    {
        public bool TryInsertPlayer(CPlayer player)
        {
            short validIndex = 0;

            for (; validIndex < HGameBasics.MAX_PLAYER; validIndex++)
            {
                if (PlayerState[validIndex] == EPlayerState.CLOSED)
                    break;
            }

            if (validIndex >= HGameBasics.MAX_PLAYER)
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
            }
        }

        public EPacketError TryProcessPacket(CPlayer player)
        {
            EPacketError err = EPacketError.NO_ERROR;

            try
            {
                if (!HPacketHelper.Decrypt(player.RecvPacket.Buffer, player.RecvPacket.Offset))
                {
                    err = EPacketError.CHECKSUM_FAIL;
                }
                else
                {
                    // Switch for the packet opcode.
                    switch (player.RecvPacket.Header.Opcode)
                    {
                        case BAccountLoginPacket.Opcode:
                            err = ProcessAccountLogin(player);
                            break;

                        case BPingPacket.Opcode:

                            break;

                        default:
                            err = EPacketError.PACKET_NOT_HANDLED;
                            break;
                    }
                }
            }
            catch(Exception)
            {
                err = EPacketError.UNKNOWN;
            }

            return err;
        }
    }
}