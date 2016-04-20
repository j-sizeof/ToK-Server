using System.Runtime.InteropServices;
using ToK.Common.Game;
using ToK.Common.Game.MarshalableTypes;

namespace ToK.Common.Persistency
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = HProjectBasics.DEFAULT_PACK)]
    public struct BAccountFile
    {
        public BAccountInfo Info;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HGameBasics.MAXL_ACC_MOB)]
        public BMobCore[] MobCore;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HGameBasics.MAXL_CARGO_ITEM)]
        public BItem[] Cargo;

        public int CargoCoin;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HGameBasics.MAXL_ACC_MOB)]
        public BAccountFile_ShortSkill[] ShortSkill;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HGameBasics.MAXL_ACC_MOB)]
        public BAccountFile_Affects[] Affects;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HGameBasics.MAXL_ACC_MOB)]
        public BMobExtra[] MobExtra;

        public int Donate;

        public unsafe fixed sbyte TempKey[52];

        #region Marshal Array Hack
        [StructLayout(LayoutKind.Sequential, Pack = HProjectBasics.DEFAULT_PACK)]
        public struct BAccountFile_ShortSkill
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] Get;
        }

        [StructLayout(LayoutKind.Sequential, Pack = HProjectBasics.DEFAULT_PACK)]
        public struct BAccountFile_Affects
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HGameBasics.MAXL_AFFECT)]
            public BAffect[] Get;
        }
        #endregion
    }
}