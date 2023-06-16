using System;
namespace NamcoSystem10Decrypter.Configs
{
	public class Taiko6Config : GameConfig
	{
		public Taiko6Config() {
			eMask = new ulong[]{
				0x0000000000000022, 0x0000000000008082, 0x0000808400d10000, 0x0000000000000088,
				0x0000000000001040, 0x0000000000001600, 0x0000000714400404, 0x0000000021c40800,
				0x0000000000004018, 0x000000002c450200, 0x0000000000000c01, 0x0000000000000180,
				0x000000000c000414, 0x0000000000000110, 0x0000000023006000, 0x0000000000000068
			};

			dMask = new ulong[]{
				0x0000000000000026, 0x0000000000008802, 0x0000081100f22000, 0x000000000000008a,
				0x0000000000001400, 0x0000000000000600, 0x00000012d8400404, 0x0000000021804800,
				0x0000000000004090, 0x0000000068012000, 0x0000000000004801, 0x0000000000000180,
				0x00000000c8000504, 0x0000000000000111, 0x0000000032086008, 0x0000000000000248
			};

			xMask = 0x9f6b;

			romFiles = new string[]{
				"tk61vera_0.8e",
				"tk61vera_1.8d",
				"tk61vera_2.7e"
			};
		}

		public override ushort NonlinearCalculation(ulong prevCipherwords, ulong prevPlainwords) {
			ulong previousMasks = prevCipherwords ^ prevPlainwords;
			return (ushort)((1 & (previousMasks >> 12) & (ulong)(UtilityFunctions.GF2Reduce(prevCipherwords & 0x808400410000) ^ UtilityFunctions.GF2Reduce(prevPlainwords & 0x81100630000))) << 4);
		}

		//Unused
		public override ulong IVCalculation(int val) {
			return 0;
		}

		public override ushort Unscramble(ushort data)
		{
			return UtilityFunctions.BitswapUInt16(data, 0xe, 0xc, 0xf, 0xd, 0x9, 0xb, 0x8, 0xa, 0x5, 0x4, 0x7, 0x6, 0x2, 0x1, 0x0, 0x3);
		}
	}
}

