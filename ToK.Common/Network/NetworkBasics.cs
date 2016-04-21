using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToK.Common.Network
{
    public static class NetworkBasics
    {
        /// <summary>
        /// Max packet length in bytes.
        /// </summary>
        public const int MAXL_PACKET = 8000;

        /// <summary>
        /// Code used in the game network protocol to initiate the connection. Every player must send this 4-byte value as the first packet.
        /// </summary>
        public const int INIT_CODE = 0x1F11F311;
    }
}