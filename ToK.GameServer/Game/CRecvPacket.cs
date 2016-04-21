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
            Buffer = new byte[NetworkBasics.MAXL_PACKET];
            Offset = 0;
        }
    }
}