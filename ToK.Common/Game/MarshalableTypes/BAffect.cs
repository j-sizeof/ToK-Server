using System.Runtime.InteropServices;

namespace ToK.Common.Game.MarshalableTypes
{
    [StructLayout(LayoutKind.Sequential, Pack = HProjectBasics.DEFAULT_PACK)]
    public struct BAffect
    {
        public byte Type;
        public byte Value;
        public ushort Level;
        public uint Time;
    }
}