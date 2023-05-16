using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GSCoder.Backend
{
    class file_read
    {
        public static List<byte> Read(string filePath)
        {
            var data = new List<byte>();

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                if (stream.Length > 0)
                {
                    data = new List<byte>((int)stream.Length);
                    byte[] buffer = new byte[stream.Length];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    data.AddRange(buffer.Take(bytesRead));
                }
            }

            return data;
        }
    }
}