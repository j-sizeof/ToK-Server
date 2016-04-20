using System;
using System.Runtime.InteropServices;
using ToK.Common;
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
        /// Get the next ushort from the buffer starting from the buffer's offset.
        /// </summary>
        /// <param name="adtOffset">Aditional offset.</param>
        /// <param name="optOffset">Optional offset.</param>
        public ushort GetUShort(int adtOffset = 0, int optOffset = -1)
        {
            return BitConverter.ToUInt16(this.Buffer, (optOffset + adtOffset >= 0 && optOffset + adtOffset < this.Buffer.Length) ? optOffset + adtOffset : this.Offset + adtOffset);
        }
    }
}