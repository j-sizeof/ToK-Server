using System;
using System.Runtime.InteropServices;
using ToK.Common.Persistency;

namespace ToK.Common.Network.PacketStructures
{
    /// <summary>
    /// Packet sent by game client requesting login.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = HProjectBasics.DEFAULT_PACK)]
    public struct BAccountLoginPacket : IGamePacket
    {
        public const ushort Opcode = 0x20D;

        public BPacketHeader Header { get; set; }
        
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = BLoginInfo.MAXL_PSW)]
        public String Password;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = BLoginInfo.MAXL_ACCNAME)]
        public String AccName;

        public unsafe fixed byte Zero[52];
        
        public int ClientVersion;

        public int DBNeedSave;

        public unsafe fixed int AdapterName[4];
    }
}