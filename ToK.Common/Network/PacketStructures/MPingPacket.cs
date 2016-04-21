using System;
using System.Runtime.InteropServices;
using ToK.Common.Game;
using ToK.Common.Game.MarshalableTypes;
using ToK.Common.Persistency;

using static ToK.Common.Game.GameBasics;

namespace ToK.Common.Network.PacketStructures
{
    /// <summary>
    /// A simple ping packet sent periodically from the client to maintain the connection with the server active.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = ProjectBasics.DEFAULT_PACK)]
    public struct MPingPacket : IGamePacket
    {
        public const ushort Opcode = 0x3A0;

        public MPacketHeader Header { get; set; }
    }
}