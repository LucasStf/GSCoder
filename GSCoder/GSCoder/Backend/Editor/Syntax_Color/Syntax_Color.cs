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

                case lexer.TokenTypes.Keyword:
                    return Colors.RoyalBlue;

                case lexer.TokenTypes.Operator:
                    return Colors.DarkGreen;

                case lexer.TokenTypes.String:
                    return Colors.DarkOrange;

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

        public static void SetColorCurrentText(string text, RichTextArea textArea, int startIndex)
        {
            var token = Color_Lexer.GetTokenTypesSyntaxColor(text);
            if(token != lexer.TokenTypes.Unknown)
            {
                //set the selection on the current text
                textArea.Selection = new Range<int>(startIndex, startIndex + text.Length -1);

                textArea.SelectionForeground = GetSyntaxColor(token);

                // set the cursor position to the end of the currenttext
                textArea.CaretIndex = text.Length + startIndex;
            }
        }

        public static void SetSyntaxColorOpenProject(RichTextArea textArea)
        {
            var originalText = textArea.Text;
            var parsedText = parser.GetParsedCode(originalText);
            bool isValid = false;

            int selectionStart = 0;

            foreach (var token in parsedText)
            {
                var tokenType = Color_Lexer.GetTokenTypesSyntaxColor(token);

                //if token is a string
                if (tokenType == lexer.TokenTypes.String)
                {
                    isValid = true;
                }
                //if token is an operator
                if(tokenType == lexer.TokenTypes.Operator)
                {
                    isValid = true;
                }
                //if the token is a keyword
                if (tokenType == lexer.TokenTypes.Keyword)
                {
                    isValid = true;
                }

                if(isValid)
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

                isValid = false;
            }

            //set the caret to the start of the text
            textArea.CaretIndex = 0;
        }
    }
}