using System;

namespace GSCoder.Backend
{
    class lexer
    {
        enum TokenTypes
        {
            Bool,
            String,
            Int,
            Float,
            Char,
            Null,
        }

        public static bool IsTypeValid(string typeString)
        {
            if(typeString.Length != 0)
            {
                string capitalizedType = char.ToUpper(typeString[0]) + typeString.Substring(1);
                return Enum.IsDefined(typeof(TokenTypes), capitalizedType);
            }
            return false;
        }
    }
}