using System.Runtime.InteropServices;

namespace ToK.Common.Game.MarshalableTypes
{
    /// <summary>
    /// Represents the buffs in the game.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = ProjectBasics.DEFAULT_PACK)]
    public struct MAffect
    {
        public byte Type;
        public byte Value;
        public ushort Level;
        public uint Time;
    }
}