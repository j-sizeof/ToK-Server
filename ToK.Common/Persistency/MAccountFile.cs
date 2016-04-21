using System.Runtime.InteropServices;
using ToK.Common.Game;
using ToK.Common.Game.MarshalableTypes;

namespace ToK.Common.Persistency
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = ProjectBasics.DEFAULT_PACK)]
    public struct MAccountFile
    {
        public MAccountInfo Info;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GameBasics.MAXL_ACC_MOB)]
        public BMobCore[] MobCore;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GameBasics.MAXL_CARGO_ITEM)]
        public MItem[] Cargo;

        public int CargoCoin;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GameBasics.MAXL_ACC_MOB)]
        public BAccountFile_ShortSkill[] ShortSkill;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GameBasics.MAXL_ACC_MOB)]
        public BAccountFile_Affects[] Affects;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GameBasics.MAXL_ACC_MOB)]
        public MMobExtra[] MobExtra;

        public int Donate;

        public unsafe fixed sbyte TempKey[52];

        #region Marshal Array Hack
        [StructLayout(LayoutKind.Sequential, Pack = ProjectBasics.DEFAULT_PACK)]
        public struct BAccountFile_ShortSkill
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] Get;
        }

        [StructLayout(LayoutKind.Sequential, Pack = ProjectBasics.DEFAULT_PACK)]
        public struct BAccountFile_Affects
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = GameBasics.MAXL_AFFECT)]
            public MAffect[] Get;
        }
        #endregion
    }
}