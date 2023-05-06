using Eto.Drawing;

namespace GSCoder.Backend
{
    class Syntax_Color
    {
        public static Color GetSyntaxColor(lexer.TokenTypes tokenType)
        {
            switch (tokenType)
            {
                case lexer.TokenTypes.Comment:
                    return Colors.Green;

                case lexer.TokenTypes.Punctuation:
                    return Colors.Blue;

                case lexer.TokenTypes.Keyword:
                    return Colors.Red;

                case lexer.TokenTypes.Modifier:
                    return Colors.Purple;

                case lexer.TokenTypes.Operator:
                    return Colors.Yellow;

                case lexer.TokenTypes.Type:
                    return Colors.Orange;

                case lexer.TokenTypes.String:
                    return Colors.Brown;

                case lexer.TokenTypes.Integer:
                    return Colors.Gray;

                case lexer.TokenTypes.Float:
                    return Colors.Gray;

                case lexer.TokenTypes.Unknown:
                    return Colors.White;

                default:
                    return Colors.White;
            }
        }
    }
}