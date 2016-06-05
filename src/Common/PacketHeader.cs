// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class PacketHeader : IPacket
    {
        public UInt32 Size;
        public UInt16 ID;
        public Byte Checksum;

        public void CreateChecksum()
        {
            byte[] data = new byte[7];
            Buffer.BlockCopy(BitConverter.GetBytes(Size), 0, data, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(ID), 0, data, 4, 2);

            byte checksum = 0;
            for(int i = 0; i < 7; i++) { checksum += data[i]; }

            Checksum = checksum;
        }
    }
}
