using System;
namespace NamcoSystem10Decrypter
{
	public class UtilityFunctions
	{
		public static int CountUInt64OneBits(ulong val) {
			int count = 0;

			for (int i = 0; i < 64; i++) {
				if ((val & 1) == 1) count++;
				val >>= 1;
			}

			return count;
		}

		public static int GF2Reduce(ulong num) {
			return CountUInt64OneBits(num) & 1;
		}

		public static ushort BitswapUInt16(ushort val, params int[] outputBits) {
			if(outputBits.Length != 16) {
				throw new Exception("Error: number of output bits has to be 16");
			}

			ushort result = 0;

			for(int i = 0; i < 16; i++) {
				result |= (ushort)(((val >> outputBits[i]) & 1) << (15 - i));
			}

			return result;
		}
	}
}

