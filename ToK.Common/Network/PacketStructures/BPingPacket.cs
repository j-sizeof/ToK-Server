using System;
using System.Runtime.InteropServices;
using ToK.Common.Game;
using ToK.Common.Game.MarshalableTypes;
using ToK.Common.Persistency;

using static ToK.Common.Game.HGameBasics;

namespace ToK.Common.Network.PacketStructures
{
    /// <summary>
    /// TODO: packet summary.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = HProjectBasics.DEFAULT_PACK)]
    public struct BPingPacket : IGamePacket
    {
        public const ushort Opcode = 0x3A0;

        public BPacketHeader Header { get; set; }
    }
}