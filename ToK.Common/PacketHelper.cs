using System;
using System.Runtime.InteropServices;

namespace ToK.Common
{
    /// <summary>
    /// Implements the packet enc/dec algorithms.
    /// </summary>
    public static class PacketHelper
    {
        private static Random rd;

        static PacketHelper()
        {
            rd = new Random();
        }

        /// <summary>
        /// Creates a empty ready-to-use copy of a given implementation of IGamePacket.
        /// </summary>
        public static T GetEmptyValid<T>(ushort opcode) where T : struct, IGamePacket
        {
            MPacketHeader validHeader = new MPacketHeader();
            T packet = MyMarshal.CreateEmpty<T>();

            // Set the default values to the packet header.
            validHeader.Size = (ushort)Marshal.SizeOf(packet);
            validHeader.Opcode = opcode;
            validHeader.Key = (byte)rd.Next(127);
            validHeader.TimeStamp = (uint)Environment.TickCount;

            packet.Header = validHeader;

            return packet;
        }
    }
}