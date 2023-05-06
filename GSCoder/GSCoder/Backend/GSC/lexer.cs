using System;

namespace GSCoder.Backend
{
    class lexer
    {
        enum TokenTypes
        {
            // Types
            Void,
            Int,
            Float,
            Vector,
            Entity,
            String,
            Code,
        }
        
        enum TokenComments
        {
            // Comments
            SingleLineComment,
            MultiLineComment,
        }

        enum TokenPonctuation
        {
           // Punctuation
            Semicolon,
            Comma,
            Dot,
            Arrow,
            Colon,
            DoubleColon,
            QuestionMark,
            LeftParenthesis,
            RightParenthesis,
            LeftBracket,
            RightBracket,
            LeftBrace,
            RightBrace,
        }

        enum TokenOperators
        {
            // Operators
            Assign,
            AddAssign,
            SubAssign,
            MulAssign,
            DivAssign,
            ModAssign,
            Inc,
            Dec,
            Equal,
            NotEqual,
            GreaterThan,
            LessThan,
            GreaterThanOrEqual,
            LessThanOrEqual,
            LogicalAnd,
            LogicalOr,
            BitwiseAnd,
            BitwiseOr,
            BitwiseXor,
            BitwiseNot,
            ShiftLeft,
            ShiftRight,
        }

        enum TokenKeywords
        {
            //Keywords
            Break,
            Case,
            Continue,
            Default,
            Do,
            Else,
            For,
            If,
            Switch,
            While,
        }

        enum TokenModifiers
        {
            // Modifiers
            Const,
            Extern,
            Static,
            Public,
            Private,
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

        public static bool IsCommentValid(string commentString)
        {
            if (commentString.Length != 0)
            {
                string capitalizedComment = char.ToUpper(commentString[0]) + commentString.Substring(1);
                return Enum.IsDefined(typeof(TokenComments), capitalizedComment);
            }
            return false;
        }

        public static bool IsPonctuationValid(string ponctuationString)
        {
            if (ponctuationString.Length != 0)
            {
                string capitalizedPonctuation = char.ToUpper(ponctuationString[0]) + ponctuationString.Substring(1);
                return Enum.IsDefined(typeof(TokenPonctuation), capitalizedPonctuation);
            }
            return false;
        }

        public static bool IsOperatorValid(string operatorString)
        {
            if (operatorString.Length != 0)
            {
                string capitalizedOperator = char.ToUpper(operatorString[0]) + operatorString.Substring(1);
                return Enum.IsDefined(typeof(TokenOperators), capitalizedOperator);
            }
            return false;
        }

        public static bool IsKeywordValid(string keywordString)
        {
            if (keywordString.Length != 0)
            {
                string capitalizedKeyword = char.ToUpper(keywordString[0]) + keywordString.Substring(1);
                return Enum.IsDefined(typeof(TokenKeywords), capitalizedKeyword);
            }
            return false;
        }

        public static bool IsModifierValid(string modifierString)
        {
            if (modifierString.Length != 0)
            {
                string capitalizedModifier = char.ToUpper(modifierString[0]) + modifierString.Substring(1);
                return Enum.IsDefined(typeof(TokenModifiers), capitalizedModifier);
            }
            return false;
        }
    }
}