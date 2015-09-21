// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System.IO;
using System.Text;
using Common.Utilities;
using System;

namespace Common
{
	/// <summary>
	/// A Packet
	/// </summary>
	public class PacketStream : MemoryStream
	{
		/// <summary>
		/// Packet ID
		/// </summary>
		public short Id { get; private set; }

		private MemoryStream inner;

		#region Constructors
		/// <summary>
		/// Creates an empty packet
		/// </summary>
		public PacketStream()
		{
			this.inner = new MemoryStream();
		}

		/// <summary>
		/// Creates a packet and set its ID
		/// </summary>
		/// <param name="PacketId"></param>
		public PacketStream(short PacketId)
		{
			this.inner = new MemoryStream();
			this.inner.Write(new byte[4], 0, 4);
			this.inner.Write(BitConverter.GetBytes(PacketId), 0, 2);
			this.inner.Write(new byte[1], 0, 1);
		}

		/// <summary>
		/// Creates a packet using given bytes
		/// </summary>
		/// <param name="pInner"></param>
		public PacketStream(byte[] pInner)
		{
			this.inner = new MemoryStream(pInner);
		}

		/// <summary>
		/// Creates a packet with given data
		/// </summary>
		/// <param name="pInner"></param>
		public PacketStream(MemoryStream pInner)
		{
			this.inner = pInner;
		}

		#endregion
		
		/// <summary>
		/// Converts packet to array
		/// </summary>
		/// <returns></returns>
		public override byte[] ToArray()
		{
			return this.inner.ToArray();
		}

		#region Write Methods
		/// <summary>
		/// Writes count bytes of given buffer to the packet at given offset
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.inner.Write(buffer, offset, count);
		}

		/// <summary>
		/// Writes a 16-bits integer to the packet
		/// </summary>
		/// <param name="val"></param>
		public void WriteInt16(Int16 val)
		{
			this.inner.Write(BitConverter.GetBytes(val), 0, 2);
		}

		/// <summary>
		/// Writes a 16-bits unsigned integer to the packet
		/// </summary>
		/// <param name="val"></param>
		public void WriteUInt16(UInt16 val)
		{
			this.inner.Write(BitConverter.GetBytes(val), 0, 2);
		}

		/// <summary>
		/// Writes a 32-bits integer to the packet
		/// </summary>
		/// <param name="val"></param>
		public void WriteInt32(Int32 val)
		{
			this.inner.Write(BitConverter.GetBytes(val), 0, 4);
		}

		/// <summary>
		/// Writes a 32-bits unsigned integer to the packet
		/// </summary>
		/// <param name="val"></param>
		public void WriteUInt32(UInt32 val)
		{
			this.inner.Write(BitConverter.GetBytes(val), 0, 4);
		}

		/// <summary>
		/// Writes a 64-bits integer to the packet
		/// </summary>
		/// <param name="val"></param>
		public void WriteInt64(Int64 val)
		{
			this.inner.Write(BitConverter.GetBytes(val), 0, 8);
		}

		/// <summary>
		/// Writes a 64-bits unsigned integer to the packet
		/// </summary>
		/// <param name="val"></param>
		public void WriteUInt64(UInt64 val)
		{
			this.inner.Write(BitConverter.GetBytes(val), 0, 8);
		}

		/// <summary>
		/// Writes a floating-point number to the packet
		/// </summary>
		/// <param name="val"></param>
		public void WriteSingle(Single val)
		{
			this.inner.Write(BitConverter.GetBytes(val), 0, 4);
		}

		/// <summary>
		/// Writes a byte to the packet
		/// </summary>
		/// <param name="value"></param>
		public override void WriteByte(byte value)
		{
			this.inner.WriteByte(value);
		}

		/// <summary>
		/// Writes a string to the packet, filling with zeros
		/// </summary>
		/// <param name="val"></param>
		/// <param name="size"></param>
		public void WriteString(String val, int size)
		{
			byte[] tmp = Encoding.ASCII.GetBytes(val);
			this.inner.Write(tmp, 0, tmp.Length);
			this.inner.Write(new byte[size - tmp.Length], 0, size - tmp.Length);
		}

		/// <summary>
		/// Writes a string to the packet, without filling with zeros
		/// </summary>
		/// <param name="value"></param>
		internal void WriteString(String value)
		{
			this.WriteString(value, value.Length);
		}

		/// <summary>
		/// Writes a boolean to the packet
		/// </summary>
		/// <param name="val"></param>
		public void WriteBool(bool val)
		{
			this.inner.WriteByte((byte)(val ? 1 : 0));
		}
		#endregion

		#region Read Methods
		/// <summary>
		/// Reads count bytes from the packet, starting at the offset to the buffer
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public override int Read(byte[] buffer, int offset, int count)
		{
			var result = this.inner.Read(buffer, offset, count);

			return result;
		}

		/// <summary>
		/// Read count bytes
		/// </summary>
		/// <param name="count"></param>
		/// <param name="offset"></param>
		/// <param name="countHeader"></param>
		/// <returns></returns>
		public byte[] ReadBytes(int count, int offset = -1, bool countHeader = false)
		{
			if (countHeader)
				inner.Seek(offset, SeekOrigin.Begin);
			else if (offset == -1 && inner.Position < Globals.HeaderLength)
				inner.Seek(Globals.HeaderLength, SeekOrigin.Begin);
			else if (offset >= 0)
				inner.Seek(offset + Globals.HeaderLength, SeekOrigin.Begin);

			byte[] buffer = new byte[count];
			inner.Read(buffer, 0, count);
			return buffer;
		}

		/// <summary>
		/// Reads a signed 16-bits Integer from the packet
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="countHeader"></param>
		/// <returns></returns>
		public Int16 ReadInt16(int offset = -1, bool countHeader = false)
		{
			if (countHeader)
				inner.Seek(offset, SeekOrigin.Begin);
			else if (offset == -1 && inner.Position < Globals.HeaderLength)
				inner.Seek(Globals.HeaderLength, SeekOrigin.Begin);
			else if (offset >= 0)
				inner.Seek(offset + Globals.HeaderLength, SeekOrigin.Begin);

			byte[] buffer = new byte[2];
			inner.Read(buffer, 0, 2);
			return BitConverter.ToInt16(buffer, 0);
		}

		/// <summary>
		/// Reads an unsigned 16-bits integer from the packet
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="countHeader"></param>
		/// <returns></returns>
		public UInt16 ReadUInt16(int offset = -1, bool countHeader = false)
		{
			if (countHeader)
				inner.Seek(offset, SeekOrigin.Begin);
			else if (offset == -1 && inner.Position < Globals.HeaderLength)
				inner.Seek(Globals.HeaderLength, SeekOrigin.Begin);
			else if (offset >= 0)
				inner.Seek(offset + Globals.HeaderLength, SeekOrigin.Begin);

			byte[] buffer = new byte[2];
			inner.Read(buffer, 0, 2);
			return BitConverter.ToUInt16(buffer, 0);
		}

		/// <summary>
		/// Reads a signed 32-bits integer from the packet
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="countHeader"></param>
		/// <returns></returns>
		public Int32 ReadInt32(int offset = -1, bool countHeader = false)
		{
			if (countHeader)
				inner.Seek(offset, SeekOrigin.Begin);
			else if (offset == -1 && inner.Position < Globals.HeaderLength)
				inner.Seek(Globals.HeaderLength, SeekOrigin.Begin);
			else if (offset >= 0)
				inner.Seek(offset + Globals.HeaderLength, SeekOrigin.Begin);

			byte[] buffer = new byte[4];
			inner.Read(buffer, 0, 4);
			return BitConverter.ToInt32(buffer, 0);
		}

		/// <summary>
		/// Reads an unsigned 32-bits integer from the packet
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="countHeader"></param>
		/// <returns></returns>
		public UInt32 ReadUInt32(int offset = -1, bool countHeader = false)
		{
			if (countHeader)
				inner.Seek(offset, SeekOrigin.Begin);
			else if (offset == -1 && inner.Position < Globals.HeaderLength)
				inner.Seek(Globals.HeaderLength, SeekOrigin.Begin);
			else if (offset >= 0)
				inner.Seek(offset + Globals.HeaderLength, SeekOrigin.Begin);

			byte[] buffer = new byte[4];
			inner.Read(buffer, 0, 4);
			return BitConverter.ToUInt32(buffer, 0);
		}

		/// <summary>
		/// Reads a signed 64-bits integer from the packet
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="countHeader"></param>
		/// <returns></returns>
		public Int64 ReadInt64(int offset = -1, bool countHeader = false)
		{
			if (countHeader)
				inner.Seek(offset, SeekOrigin.Begin);
			else if (offset == -1 && inner.Position < Globals.HeaderLength)
				inner.Seek(Globals.HeaderLength, SeekOrigin.Begin);
			else if (offset >= 0)
				inner.Seek(offset + Globals.HeaderLength, SeekOrigin.Begin);

			byte[] buffer = new byte[8];
			inner.Read(buffer, 0, 8);
			return BitConverter.ToInt64(buffer, 0);
		}

		/// <summary>
		/// Reads an unsigned 64-bits integer from the packet
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="countHeader"></param>
		/// <returns></returns>
		public UInt64 ReadUInt64(int offset = -1, bool countHeader = false)
		{
			if (countHeader)
				inner.Seek(offset, SeekOrigin.Begin);
			else if (offset == -1 && inner.Position < Globals.HeaderLength)
				inner.Seek(Globals.HeaderLength, SeekOrigin.Begin);
			else if (offset >= 0)
				inner.Seek(offset + Globals.HeaderLength, SeekOrigin.Begin);

			byte[] buffer = new byte[8];
			inner.Read(buffer, 0, 8);
			return BitConverter.ToUInt64(buffer, 0);
		}

		/// <summary>
		/// Reads a floating-point number from the packet
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="countHeader"></param>
		/// <returns></returns>
		public Single ReadSingle(int offset = -1, bool countHeader = false)
		{
			if (countHeader)
				inner.Seek(offset, SeekOrigin.Begin);
			else if (offset == -1 && inner.Position < Globals.HeaderLength)
				inner.Seek(Globals.HeaderLength, SeekOrigin.Begin);
			else if (offset >= 0)
				inner.Seek(offset + Globals.HeaderLength, SeekOrigin.Begin);

			byte[] buffer = new byte[4];
			inner.Read(buffer, 0, 4);
			return BitConverter.ToSingle(buffer, 0);
		}

		/// <summary>
		/// Reads a string of given size from the packet
		/// </summary>
		/// <param name="size"></param>
		/// <param name="offset"></param>
		/// <param name="countHeader"></param>
		/// <returns></returns>
		public String ReadString(int size, int offset = -1, bool countHeader = false)
		{
			if (countHeader)
				inner.Seek(offset, SeekOrigin.Begin);
			else if (offset == -1 && inner.Position < Globals.HeaderLength)
				inner.Seek(Globals.HeaderLength, SeekOrigin.Begin);
			else if (offset >= 0)
				inner.Seek(offset + Globals.HeaderLength, SeekOrigin.Begin);

			byte[] buffer = new byte[size];
			inner.Read(buffer, 0, size);

			return ByteUtils.ToString(buffer);
		}

		/// <summary>
		/// Reads a boolean value from the packet
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="countHeader"></param>
		/// <returns></returns>
		public bool ReadBool(int offset = -1, bool countHeader = false)
		{
			if (countHeader)
				inner.Seek(offset, SeekOrigin.Begin);
			else if (offset == -1 && inner.Position < Globals.HeaderLength)
				inner.Seek(Globals.HeaderLength, SeekOrigin.Begin);
			else if (offset >= 0)
				inner.Seek(offset + Globals.HeaderLength, SeekOrigin.Begin);

			return (inner.ReadByte() == 0 ? false : true);
		}

		/// <summary>
		/// Reads a byte from the packet
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="countHeader"></param>
		/// <returns></returns>
		internal byte ReadByte(int offset = -1, bool countHeader = false)
		{
			if (countHeader)
				inner.Seek(offset, SeekOrigin.Begin);
			else if (offset == -1 && inner.Position < Globals.HeaderLength)
				inner.Seek(Globals.HeaderLength, SeekOrigin.Begin);
			else if (offset >= 0)
				inner.Seek(offset + Globals.HeaderLength, SeekOrigin.Begin);

			return (byte)inner.ReadByte();
		}
		#endregion

		/// <summary>
		/// Gets the packet size
		/// </summary>
		/// <returns></returns>
		public Int32 GetSize()
		{
			return inner.ToArray().Length;
		}

		/// <summary>
		/// Gets the packet ID
		/// </summary>
		/// <returns></returns>
		public Int16 GetId()
		{
			if (this.Id > 0) return this.Id;

			inner.Seek(4, SeekOrigin.Begin);
			byte[] res = new byte[2];
			inner.Read(res, 0, 2);
			this.Id = BitConverter.ToInt16(res, 0);
			return this.Id;
		}

		/// <summary>
		/// Get packet data (without the header)
		/// </summary>
		/// <returns></returns>
		public byte[] GetData()
		{
			byte[] res = new byte[inner.Length - 7];
			inner.Seek(7, SeekOrigin.Begin);
			inner.Read(res, 0, res.Length);

			return res;
		}

		/// <summary>
		/// Sets Packet ID
		/// </summary>
		/// <param name="pPacketId"></param>
		public void SetId(Int16 pPacketId)
		{
			inner.Seek(4, SeekOrigin.Begin);
			inner.Write(BitConverter.GetBytes(pPacketId), 0, 2);
		}

		/// <summary>
		/// Gets the entire packet data
		/// </summary>
		/// <returns></returns>
		public MemoryStream GetPacket()
		{
			this.inner.Seek(0, SeekOrigin.Begin);
			this.inner.Write(BitConverter.GetBytes(inner.ToArray().Length), 0, 4);
			return this.inner;
		}
	}
}
