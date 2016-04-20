using System.Runtime.InteropServices;

namespace ToK.Common.Game.MarshalableTypes
{
    [StructLayout(LayoutKind.Sequential, Pack = HProjectBasics.DEFAULT_PACK)]
    public struct SPosition
    {
        public short X;
        public short Y;
    }
}