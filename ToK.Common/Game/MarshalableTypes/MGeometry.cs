﻿using System.Runtime.InteropServices;

namespace ToK.Common.Game.MarshalableTypes
{
    /// <summary>
    /// Represents a 4-byte bidimensional position in the game.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = ProjectBasics.DEFAULT_PACK)]
    public struct MPosition
    {
        public short X;
        public short Y;
    }
}