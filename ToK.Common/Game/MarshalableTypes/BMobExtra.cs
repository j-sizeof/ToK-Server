using System.Runtime.InteropServices;

namespace ToK.Common.Game.MarshalableTypes
{
    public enum EMysticLandQuest : sbyte
    {
        DontHave = 0,
        DontConcluded = 1,
        Completed = 2
    }

    [StructLayout(LayoutKind.Sequential, Pack = HProjectBasics.DEFAULT_PACK)]
    public struct BMobExtra
    {
        public short ClassMaster;
        public sbyte Citizen;
        public int Fame;
        public sbyte Soul;
        public short MortalFace;

        public BQuestInfo QuestInfo;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public BSavedCelestial[] SavedCelestial; // Represents the celestial and the sub-celestial.

        public long LastNT;
        public int NT;

        public int KefraTicket;
        public long DivineEnd;
        public uint Hold;

        public BDayLog DayLog;

        public BDonateInfo DonateInfo;

        public unsafe fixed int Empty[9];

        [StructLayout(LayoutKind.Sequential, Pack = HProjectBasics.DEFAULT_PACK)]
        public struct BQuestInfo
        {
            [StructLayout(LayoutKind.Sequential, Pack = HProjectBasics.DEFAULT_PACK)]
            public struct BQuestInfo_Mortal
            {
                public sbyte Newbie; // 00_01_02_03_04  quest com quatro etapas
                public EMysticLandQuest MysticLand;
                public sbyte MolarGargoyle;
                public sbyte OrcPill;
                public unsafe fixed byte Empty[30];
            }

            [StructLayout(LayoutKind.Sequential, Pack = HProjectBasics.DEFAULT_PACK)]
            public struct BQuestInfo_Arch
            {
                public sbyte MortalSlot;
                public sbyte MortalLevel;
                public sbyte Level355;
                public sbyte Level370;
                public sbyte Cristal; //00_01_02_03_04 quest com quatro etapas
                public unsafe fixed byte Empty[30];
            }

            [StructLayout(LayoutKind.Sequential, Pack = HProjectBasics.DEFAULT_PACK)]
            public struct BQuestInfo_Celestial
            {
                public short ArchLevel;
                public short CelestialLevel;
                public short SubCelestialLevel;

                public sbyte Lv40;
                public sbyte Lv90;

                public sbyte Add120;
                public sbyte Add150;
                public sbyte Add180;
                public sbyte Add200;

                public sbyte Arcana;
                public sbyte Reset;

                public unsafe fixed byte Empty[30];
            }

            public BQuestInfo_Mortal Mortal;
            public BQuestInfo_Arch Arch;
            public BQuestInfo_Celestial Celestial;

            public sbyte Circle;
            public unsafe fixed byte Empty[30];
        }

        [StructLayout(LayoutKind.Sequential, Pack = HProjectBasics.DEFAULT_PACK)]
        public struct BSavedCelestial
        {
            public int Class;
            public long Exp;

            public SPosition StellarGemPosition;

            public BScore BaseScore;

            public int LearnedSkill;

            public BMobPointsLeft PointsLeft;

            public unsafe fixed byte SkillBar1[4];
            public unsafe fixed byte SkillBar2[16];

            public sbyte Soul;

            public unsafe fixed byte Empty[30];
        }

        [StructLayout(LayoutKind.Sequential, Pack = HProjectBasics.DEFAULT_PACK)]
        public struct BDayLog
        {
            public long Exp;
            public int YearDay;
        }

        [StructLayout(LayoutKind.Sequential, Pack = HProjectBasics.DEFAULT_PACK)]
        public struct BDonateInfo
        {
            public long LastTime;
            public int Count;
        }
    }
}