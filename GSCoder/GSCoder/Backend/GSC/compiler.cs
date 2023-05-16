using System;
using System.Collections.Generic;
using GSCoder.Backend;

namespace GSCoder.Backend
{
    class compiler
    {
        public static void Compile()
        {
            var data = file_read.Read("/home/strafe/Téléchargements/Linux release x64 binaries/compiled/t6/main.gsc");

            string magic = "";
            foreach (byte b in data)
            {
                magic += b.ToString("X2") + " ";
            }
            Console.WriteLine(magic, false);
        }
    }
}