using System.Runtime.InteropServices;

namespace ToK.Common.Game.MarshalableTypes
{
    /// <summary>
    /// Represent an item in the game.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = HProjectBasics.DEFAULT_PACK)]
    public struct BItem
    {
        #region Basic Definitions
        [StructLayout(LayoutKind.Sequential, Pack = HProjectBasics.DEFAULT_PACK)]
        public struct BItem_Effects
        {
            public sbyte Code;
            public sbyte Value;
        }
        #endregion

        public short Index;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HGameBasics.MAXL_ITEM_EFFECT)]
        public BItem_Effects[] Effects;
    }

    [StructLayout(LayoutKind.Sequential, Pack = HProjectBasics.DEFAULT_PACK)]
    public struct BEquip
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HGameBasics.MAXL_EQUIP)]
        public BItem[] Items;
    }

    [StructLayout(LayoutKind.Sequential, Pack = HProjectBasics.DEFAULT_PACK)]
    public struct BInventory
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HGameBasics.MAXL_INVENTORY)]
        public BItem[] Items;
    }
}