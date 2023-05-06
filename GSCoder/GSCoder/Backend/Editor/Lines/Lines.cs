using System;
using System.Linq;

namespace GSCoder.Backend
{
    class Lines 
    {
        public static string GetLinesNumber(string fileContent)
        {
            var lines = fileContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var lineNumbers = string.Join("\n", Enumerable.Range(1, Convert.ToInt32(lines.Length)));
            return lineNumbers;
        }
    }
}