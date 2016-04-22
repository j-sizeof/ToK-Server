using System;
using ToK.GameState;

namespace ToK.HandlePlayerRequest
{
    public static class PlayerRequestController
    {
        public static ERequestResult TryProcessPacket(CGameStateController gs, CPlayer player)
        {
            ERequestResult result = ERequestResult.NO_ERROR;

            try
            {
                // Switch for the packet opcode.
                switch (player.RecvPacket.ReadNextUShort(4))
                {
                    case MAccountLoginPacket.Opcode:
                        result = MAccountLoginPacket.HandleRequest(gs, player);
                        break;

                    case MPingPacket.Opcode:
                        result = MPingPacket.HandleRequest(gs, player);
                        break;

                    default:
                        result = ERequestResult.PACKET_NOT_HANDLED;
                        break;
                }
            }
            catch (Exception)
            {
                result = ERequestResult.UNKNOWN;
            }

            return result;
        }
    }
}