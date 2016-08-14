// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Runtime.InteropServices;

namespace Game.Network.Packets
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ModelInfo
    {
        public int Sex;
        public int Race;
        // hairId, faceId, bodyId, handsId, feetId
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I4, SizeConst = 5)]
        public int[] ModelId;
        public int TextureId;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I4, SizeConst = 24)]
        public int[] WearInfo;

        public ModelInfo()
        {
            this.ModelId = new int[5];
            this.WearInfo = new int[24];
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class LobbyCharacterInfo
    {
        public ModelInfo ModelInfo;
        public int Level;
        public int Job;
        public int JobLevel;
        public int ExpPercentage;
        public int Hp;
        public int Mp;
        public int Permission;
        [MarshalAs(UnmanagedType.I1)]
        public bool IsBanned;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 19)]
        public string Name;
        public uint SkinColor;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
        public string CreateTime;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
        public string DeleteTime;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I4, SizeConst = 24)]
        public int[] WearItemEnhanceInfo;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I4, SizeConst = 24)]
        public int[] WearItemLevelInfo;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 24)]
        public byte[] WearItemElementalType;

        public LobbyCharacterInfo()
        {
            this.ModelInfo = new ModelInfo();
            this.WearItemEnhanceInfo = new int[24];
            this.WearItemLevelInfo = new int[24];
            this.WearItemElementalType = new byte[24];
        }
    }
}