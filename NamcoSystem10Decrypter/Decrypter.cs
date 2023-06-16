using System;
using System.Collections.Generic;
using System.IO;

namespace NamcoSystem10Decrypter
{
	public class Decrypter
	{
		GameConfig config;
		Sys10Type2Decrypter decrypter;
		List<byte[]> nandFiles;

		public Decrypter(GameConfig config, List<byte[]> nandFiles)
		{
			this.config = config;
			this.decrypter = new Sys10Type2Decrypter(config);
			this.nandFiles = nandFiles;
		}

		public void Decrypt() {
			//Decrypt the bios
			byte[] bios = new byte[0x400000];

			NandCopy(nandFiles[0], bios, 0, 0x40, 0xE0);
			NandCopy(nandFiles[0], bios, 0x20000, 0x120, 0x1F00);

			File.WriteAllBytes("bios_decrypted.bin", bios);
		}

		public void NandCopy(byte[] nandData, byte[] destArray, int destStartOffset, int startPage, int pages) {
			int destOffset = destStartOffset;

			for(int i = startPage; i < startPage + pages; i++) {
				int address = i * 0x210;

				for (int j = 0; j < 0x200; j += 2) {
					//Console.WriteLine(nandData[address + j].ToString("X2") + "," + nandData[address + j + 1].ToString("X2"));
					ushort data = (ushort)(nandData[address + j + 1] | (nandData[address + j] << 8));
					ushort decryptedVal = config.Unscramble((ushort)(data ^ 0xaaaa));
					destArray[destOffset + 1] = (byte)(decryptedVal >> 8);
					destArray[destOffset] = (byte)(decryptedVal & 0xFF);
					destOffset += 2;
				}
			}
		}
	}
}

