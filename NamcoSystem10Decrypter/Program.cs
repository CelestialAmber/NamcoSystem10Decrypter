using System;
using System.Collections.Generic;
using System.IO;
using NamcoSystem10Decrypter.Configs;

namespace NamcoSystem10Decrypter {

    class Program {
        static void Main(string[] args) {
            List<byte[]> nandFiles = new List<byte[]>();

            GameConfig config = new Taiko6Config();

			foreach (string file in config.romFiles) {
                nandFiles.Add(File.ReadAllBytes(file));
            }

            Decrypter decrypter = new Decrypter(config, nandFiles);

            decrypter.Decrypt();

        }

	}
}

