using System;
namespace NamcoSystem10Decrypter
{
	public class Sys10Type2Decrypter
	{
		GameConfig config;
		ushort mask;
		ulong prevCipherwords;
		ulong prevPlainwords;
		int[] initSbox = new int[16];


		public Sys10Type2Decrypter(GameConfig config)
		{
			this.config = config;
		}

		public void Init(int iv) {
			if (config.usesIVCalculationFunc) {
				prevCipherwords = config.IVCalculation(iv);
			} else {
				prevCipherwords = UtilityFunctions.BitswapUInt16((ushort)initSbox[iv], 3, 16, 16, 2, 1, 16, 16, 0, 16, 16, 16, 16, 16, 16, 16, 16);
			}
			prevPlainwords = 0;
			mask = 0;
		}

		public void Reset() {
			mask = 0;
			prevCipherwords = 0;
			prevPlainwords = 0;
		}

		public ushort Decrypt(ushort cipherword) {
			ushort plainword = (ushort)(cipherword ^ mask);

			prevCipherwords <<= 16;
			prevCipherwords ^= cipherword;
			prevPlainwords <<= 16;
			prevPlainwords ^= plainword;

			mask = 0;
			for (int j = 15; j >= 0; j--) {
				mask <<= 1;
				mask ^= (ushort)UtilityFunctions.GF2Reduce(config.eMask[j] & prevCipherwords);
				mask ^= (ushort)UtilityFunctions.GF2Reduce(config.dMask[j] & prevPlainwords);
			}
			mask ^= config.xMask;
			mask ^= config.NonlinearCalculation(prevCipherwords, prevPlainwords);

			return plainword;
		}
	}
}

