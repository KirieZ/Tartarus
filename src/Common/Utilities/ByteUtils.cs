// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
	/// <summary>
	/// Swap values between big endian and small endian
	/// and other byte manipulations
	/// </summary>
	public static class ByteUtils
	{
		// Code base on https://jamesmccaffrey.wordpress.com/2013/12/29/converting-a-big-endian-integer-to-low-endian-using-c/

		//
		public static string ToString(byte[] buffer)
		{
			int num = 0;

			for (int i = 0; i < buffer.Length; i++)
			{
				if (buffer[i] == 0x00) break;
				num++;
			}
			
			byte[] str = new byte[num];

			for (int i = 0; i < num; i++)
			{
				str[i] = buffer[i];
			}

			return Encoding.ASCII.GetString(str);
		}

		/// <summary>
		/// Swaps a Int16 (2 byte) byte array from Big Endian
		/// to Small Endian or vice-versa.
		/// </summary>
		/// <param name="value">The array to be converted</param>
		/// <returns>Int16 value swapped</returns>
		public static Int16 ToInt16(byte[] value)
		{
			Array.Reverse(value);
			return BitConverter.ToInt16(value, 0);
		}

		/// <summary>
		/// Swaps a Int16 from Big Endian to Small
		/// Endian and vice-versa
		/// </summary>
		/// <param name="value">The value to be swapped</param>
		/// <returns>Int16 value swapped</returns>
		public static Int16 ToInt16(Int16 value)
		{
			return ToInt16(BitConverter.GetBytes(value));
		}

		/// <summary>
		/// Swaps a Int32 (4 byte) byte array from Big Endian
		/// to Small Endian or vice-versa.
		/// </summary>
		/// <param name="value">The array to be converted</param>
		/// <returns>Int32 value swapped</returns>
		public static Int32 ToInt32(byte[] value)
		{
			Array.Reverse(value);
			return BitConverter.ToInt32(value, 0);
		}

		/// <summary>
		/// Swaps a Int32 from Big Endian to Small
		/// Endian and vice-versa
		/// </summary>
		/// <param name="value">The value to be swapped</param>
		/// <returns>Int32 value swapped</returns>
		public static Int32 ToInt32(Int32 value)
		{
			return ToInt32(BitConverter.GetBytes(value));
		}

		/// <summary>
		/// Swaps a Int64 (8 byte) byte array from Big Endian
		/// to Small Endian or vice-versa.
		/// </summary>
		/// <param name="value">The array to be converted</param>
		/// <returns>Int64 value swapped</returns>
		public static Int64 ToInt64(byte[] value)
		{
			Array.Reverse(value);
			return BitConverter.ToInt64(value, 0);
		}

		/// <summary>
		/// Swaps a Int64 from Big Endian to Small
		/// Endian and vice-versa
		/// </summary>
		/// <param name="value">The value to be swapped</param>
		/// <returns>Int64 value swapped</returns>
		public static Int64 ToInt64(Int64 value)
		{
			return ToInt64(BitConverter.GetBytes(value));
		}


		// [Pyrok]

		/// <summary>
		/// The return value is the high-order word of the specified value.
		/// </summary>
		/// <param name="pDWord"></param>
		/// <returns></returns>
		public static short HiWord(int pDWord)
		{
			return ((short)(((pDWord) >> 16) & 0xFFFF));
		}


		/// <summary>
		/// The return value is the low-order word of the specified value.
		/// </summary>
		/// <param name="pDWord">The value</param>
		/// <returns></returns>
		public static short LoWord(int pDWord)
		{
			return ((short)(pDWord & 0xffff));
		}
	}
}