using ToK.Common.Network;
using ToK.Common.Network.PacketStructures;

namespace ToK.GameServer.Game
{
    /// <summary>
    /// Represents a place holder to trait a received packet.
    /// </summary>
    public class CRecvPacket
    {
        /// <summary>
        /// The raw packet buffer.
        /// </summary>
        public byte[] Buffer { get; set; }

        /// <summary>
        /// The actual offset to the valid data in the buffer.
        /// </summary>
        public int Offset { get; set; }

        public CRecvPacket()
        {
            Buffer = new byte[HNetworkBasics.MAXL_PACKET];
            Offset = 0;
        }

        /// <summary>
        /// Gets a copy of the actual packet header.
        /// </summary>
        public unsafe BPacketHeader Header
        {
            get
            {
                fixed(byte* pinnedBuffer = Buffer)
                {
                    return *(BPacketHeader*)&pinnedBuffer[Offset];
                }
            }
        }
    }
}