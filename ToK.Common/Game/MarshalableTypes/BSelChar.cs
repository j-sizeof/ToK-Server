using System.Runtime.InteropServices;

namespace ToK.Common.Game.MarshalableTypes
{
    [StructLayout(LayoutKind.Sequential, Pack = HProjectBasics.DEFAULT_PACK)]
    public struct BSelChar
    {
        public unsafe fixed short SPosX[HGameBasics.MAXL_ACC_MOB];
        public unsafe fixed short SPosY[HGameBasics.MAXL_ACC_MOB];

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HGameBasics.MAXL_ACC_MOB)]
        public BMobName[] Name;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HGameBasics.MAXL_ACC_MOB)]
        public BScore[] Score;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HGameBasics.MAXL_ACC_MOB)]
        public BEquip[] Equip;

        public unsafe fixed ushort Guild[HGameBasics.MAXL_ACC_MOB];

        public unsafe fixed int Coin[HGameBasics.MAXL_ACC_MOB];
        public unsafe fixed long Exp[HGameBasics.MAXL_ACC_MOB];
    }
}