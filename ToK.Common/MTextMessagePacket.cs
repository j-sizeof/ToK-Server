using System;
using System.Runtime.InteropServices;

namespace ToK.Common
{
    /// <summary>
    /// A text message which will be displayed in the client as a game notice.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = ProjectBasics.DEFAULT_PACK)]
    public struct MTextMessagePacket : IGamePacket
    {
        public const ushort Opcode = 0x101;

        public MPacketHeader Header { get; set; }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 96)]
        public String Message;

        public static MTextMessagePacket Create(String msg)
        {
            MTextMessagePacket packet = PacketHelper.GetEmptyValid<MTextMessagePacket>(Opcode);
            packet.Message = msg;
            return packet;
        }
    }
}