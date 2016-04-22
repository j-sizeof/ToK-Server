using System;
using System.Runtime.InteropServices;
using ToK.Common;
using ToK.GameState;

namespace ToK.HandlePlayerRequest
{
    /// <summary>
    /// A simple ping packet sent periodically from the client to maintain the connection with the server active.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = ProjectBasics.DEFAULT_PACK)]
    public struct MPingPacket : IGamePacket
    {
        public const ushort Opcode = 0x3A0;

        public MPacketHeader Header { get; set; }

        public static ERequestResult HandleRequest(CGameStateController gs, CPlayer player)
        {
            throw new NotImplementedException();
        }
    }
}