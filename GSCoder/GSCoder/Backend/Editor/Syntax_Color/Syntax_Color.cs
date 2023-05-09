using System.Collections.Generic;
using Eto.Drawing;
using Eto.Forms;

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

        public static void SetSyntaxColorOpenProject(CustomRichTextArea textArea)
        {
            var originalText = textArea.Text;
            var parsedText = parser.GetParsedCode(originalText);

            int selectionStart = 0;

            foreach (var token in parsedText)
            {
                var tokenType = Color_Lexer.GetTokenTypesSyntaxColor(token);

                //if the token is a keyword
                if (tokenType == lexer.TokenTypes.Keyword)
                {
                    var index = originalText.IndexOf(token, selectionStart);

                    if (index >= 0)
                    {
                        // Select token
                        textArea.Selection = new Range<int>(index, index + token.Length -1);
                        textArea.SelectionForeground = GetSyntaxColor(tokenType);

                        // Move start of next search to end of current selection
                        selectionStart = index + token.Length;
                    }
                }
            }

            //set the caret to the start of the text
            textArea.CaretIndex = 0;
        }
    }
}