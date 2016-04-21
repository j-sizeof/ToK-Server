using System.Runtime.InteropServices;

namespace ToK.Common.Game.MarshalableTypes
{
    /// <summary>
    /// Contains data representing the character selection scene. Used in some game packets.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = ProjectBasics.DEFAULT_PACK)]
    public struct MSelChar
    {
        public unsafe fixed short SPosX[GameBasics.MAXL_ACC_MOB];
        public unsafe fixed short SPosY[GameBasics.MAXL_ACC_MOB];

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GameBasics.MAXL_ACC_MOB)]
        public BMobName[] Name;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GameBasics.MAXL_ACC_MOB)]
        public MScore[] Score;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GameBasics.MAXL_ACC_MOB)]
        public BEquip[] Equip;

        public unsafe fixed ushort Guild[GameBasics.MAXL_ACC_MOB];

        public unsafe fixed int Coin[GameBasics.MAXL_ACC_MOB];
        public unsafe fixed long Exp[GameBasics.MAXL_ACC_MOB];
    }
}