using Eto.Drawing;

namespace GSCoder.Backend
{
    class Syntax_Color
    {
        //define all colors
        public static Color CommentColor = Color.FromArgb(255, 87, 166, 74);
        public static Color PonctuationColor = Color.FromArgb(255, 86, 156, 214);
        public static Color KeywordColor = Color.FromArgb(255, 155, 81, 224);
        public static Color ModifierColor = Color.FromArgb(255, 43, 145, 175);
        public static Color OperatorColor = Color.FromArgb(255, 224, 224, 224);
        public static Color TypeColor = Color.FromArgb(255, 86, 156, 214);


        public static string GetToken(string currentText)
        {
            //create a tokentype variable
            string tokenType = "";

            //get the type of the token
            if(lexer.IsCommentValid(currentText))
                tokenType = "Comment";

            if(lexer.IsPonctuationValid(currentText))
                tokenType = "Ponctuation";

            if(lexer.IsKeywordValid(currentText))
                tokenType = "Keyword";

            if(lexer.IsModifierValid(currentText))
                tokenType = "Modifier";

            if(lexer.IsOperatorValid(currentText))
                tokenType = "Operator";

            if(lexer.IsTypeValid(currentText))
                tokenType = "Type";        

            return tokenType;                    
        }

        public static Color GetColorByToken(string token)
        {
            var color = Colors.White;

            //get the color of the token
            if(token == "Comment")
                color = CommentColor;

            if(token == "Ponctuation")
                color = PonctuationColor;

            if(token == "Keyword")
                color = KeywordColor;

            if(token == "Modifier")
                color = ModifierColor;

            if(token == "Operator")
                color = OperatorColor;

            if(token == "Type")
                color = TypeColor;
                                    
            return color;
        }
    }
}