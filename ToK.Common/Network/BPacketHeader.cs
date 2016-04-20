using System.Runtime.InteropServices;

namespace ToK.Common.Network.PacketStructures
{
    /// <summary>
    /// All the packet structures must implement this interface.
    /// </summary>
    public interface IGamePacket
    {
        BPacketHeader Header { get; set; }
    }

    /// <summary>
    /// Header present in all the valid game packets.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = HProjectBasics.DEFAULT_PACK)]
    public struct BPacketHeader
    {
        public ushort Size;

        public byte Key;
        public byte CheckSum;

        public ushort Opcode;
        public ushort ClientId;

        public uint TimeStamp;
    }
}