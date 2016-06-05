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
	public interface IPacket { }
    public class PacketManager
    {
        public static int GetSize(IPacket packet)
        {
            return Marshal.SizeOf(packet);
        }

        public static byte GetChecksum(uint size, ushort packetId)
        {
            byte[] data = new byte[7];
            Buffer.BlockCopy(BitConverter.GetBytes(size), 0, data, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(packetId), 0, data, 4, 2);

            byte checksum = 0;
            for(int i = 0; i < 7; i++) { checksum += data[i]; }

            return checksum;
        }

        public static byte[] ToArray(IPacket packet)
        {
            int size = Marshal.SizeOf(packet);
            byte[] buffer = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.StructureToPtr(packet, ptr, true);
            Marshal.Copy(ptr, buffer, 0, size);
            Marshal.FreeHGlobal(ptr);

            return buffer;
        }

        public static IPacket ToStructure(byte[] buffer, int size, Type type)
        {
            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.Copy(buffer, 0, ptr, size);

            IPacket packet = (IPacket)Marshal.PtrToStructure(ptr, type);
            Marshal.FreeHGlobal(ptr);

            return packet;
        }

        public static IPacket ToStructure(byte[] buffer, int offset, int size, Type type)
        {
            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.Copy(buffer, offset, ptr, size);

            IPacket packet = (IPacket)Marshal.PtrToStructure(ptr, type);
            Marshal.FreeHGlobal(ptr);

            return packet;
        }
    }
}
