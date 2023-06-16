using System;
namespace NamcoSystem10Decrypter
{
	public abstract class GameConfig
	{
		public ulong[] eMask = new ulong[16];
		public ulong[] dMask = new ulong[16];
		public ushort xMask = 0;
		public bool usesIVCalculationFunc = false;
		public string[] romFiles;

		public abstract ushort NonlinearCalculation(ulong val1, ulong val2);
		public abstract ulong IVCalculation(int val);
		public abstract ushort Unscramble(ushort data);
	}
}

