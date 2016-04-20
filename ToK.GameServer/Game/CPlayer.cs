using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using ToK.Common;
using ToK.Common.Game;
using ToK.Common.Network;
using ToK.Common.Network.PacketStructures;
using ToK.Common.Utility;

namespace ToK.GameServer.Game
{
    public class CPlayer
    {
        public short Index { get; set; }

        public NetworkStream Stream { get; private set; }

        public CRecvPacket RecvPacket { get; set; }

        public CPlayer(NetworkStream _stream)
        {
            Index = -1;
            Stream = _stream;
            RecvPacket = new CRecvPacket();
        }
        
        public bool HaveValidIndex
        {
            get { return (Index >= 1 && Index <= HGameBasics.MAX_PLAYER); }
        }

        public void SendPacket(byte[] packet)
        {
            CLog.Write(String.Format("Packet 0x{0:X} were sent to the player (Index: {1}).",
                BitConverter.ToUInt16(packet, 4), Index), ELogType.NETWORK);

            HPacketHelper.Encrypt(packet);

            Stream.Write(packet, 0, BitConverter.ToUInt16(packet, 0));
        }

        public void SendPacket<T>(T _t) where T:struct
        {
            byte[] rawPacket = HMyMarshal.GetBytes<T>(_t);

            SendPacket(rawPacket);
        }
    }
}