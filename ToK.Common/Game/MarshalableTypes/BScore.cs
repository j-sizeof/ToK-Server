using System.Runtime.InteropServices;

namespace ToK.Common.Game.MarshalableTypes
{
    /// <summary>
    /// Represents a set of information regard to a mob's status.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = HProjectBasics.DEFAULT_PACK)]
    public struct BScore
    {
        public int Level;

        public int Defense;
        public int Damage;

        public byte Merchant; // TODO: unknown type!

        private byte m_MovementSpeed;
        public byte MovementSpeed
        {
            get { return m_MovementSpeed; }
            set { m_MovementSpeed = (value > HGameBasics.MAX_MOB_SPEED) ? HGameBasics.MAX_MOB_SPEED : value; }
        }

        public byte Direction; // The direction the mob is facing.
        public byte ChaosRate; // TODO: unknown type!

        public int MaxHp;
        public int MaxMp;
        public int CurrHp;
        public int CurrMp;

        public short Str;
        public short Int;
        public short Dex;
        public short Con;
        
        public unsafe fixed short Special[4]; // The 4 special points ("pontos de aprendizagem").
    }
}