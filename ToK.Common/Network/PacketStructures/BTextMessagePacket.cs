using System;
using System.Runtime.InteropServices;

namespace ToK.Common.Network.PacketStructures
{
    [StructLayout(LayoutKind.Sequential, Pack = HProjectBasics.DEFAULT_PACK)]
    public struct BTextMessagePacket : IGamePacket
    {
        public const ushort Opcode = 0x101;

        public BPacketHeader Header { get; set; }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 96)]
        public String Message;

        public static BTextMessagePacket Create(String msg)
        {
            BTextMessagePacket packet = HPacketHelper.GetEmptyValid<BTextMessagePacket>(Opcode);
            packet.Message = msg;
            return packet;
        }
    }
}