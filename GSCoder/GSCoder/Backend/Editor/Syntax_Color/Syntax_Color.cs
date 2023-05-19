using Eto.Drawing;
using Eto.Forms;

namespace GSCoder.Backend
{
    class Syntax_Color
    {
        public static Color GetSyntaxColor(lexer.Tokens token)
        {
            switch (token)
            {
                case lexer.Tokens.KEYWORD:
                    return Colors.RoyalBlue;

                case lexer.Tokens.OPERATOR:
                    return Colors.DarkOrange;

                default:
                    return Colors.White;
            }
        }

        public static void SetColorCurrentText(string text, RichTextArea textArea, int startIndex)
        {
            var token = Color_Lexer.GetTokenTypesSyntaxColor(text);
            if(token != lexer.Tokens.NAME)
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
                if (tokenType == lexer.Tokens.STRING)
                {
                    isValid = true;
                }
                //if token is an operator
                if(tokenType == lexer.Tokens.OPERATOR)
                {
                    isValid = true;
                }
                //if the token is a keyword
                if (tokenType == lexer.Tokens.KEYWORD)
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