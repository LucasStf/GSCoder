using System;
using System.Collections.Generic;

namespace GSCoder.Backend
{
    class lexer
    {

        public enum TokenTypes
        {
            Comment,
            Punctuation,
            Keyword,
            Modifier,
            Operator,
            Type,
            String,
            Integer,
            Float,
            Identifier,
            Unknown
        }

        public enum Tokens
        {
            // Types
            Void,
            Int,
            Float,
            Vector,
            Entity,
            String,
            Code,

            // Keywords
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
            Self,

            // Modifiers
            Const,
            Extern,
            Static,
            Public,
            Private,

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

            // Literals
            IntegerLiteral,
            FloatLiteral,
            StringLiteral,

            // Identifiers
            Identifier,

            // Comments
            SingleLineComment,
            MultiLineComment,
        }

        private static readonly Dictionary<string, Tokens> Keywords = new Dictionary<string, Tokens>
        {
            {"break", Tokens.Break},
            {"case", Tokens.Case},
            {"continue", Tokens.Continue},
            {"default", Tokens.Default},
            {"do", Tokens.Do},
            {"else", Tokens.Else},
            {"for", Tokens.For},
            {"if", Tokens.If},
            {"switch", Tokens.Switch},
            {"while", Tokens.While},
            {"self", Tokens.Self}
        };

        private static readonly Dictionary<string, Tokens> Types = new Dictionary<string, Tokens>
        {
            {"void", Tokens.Void},
            {"int", Tokens.Int},
            {"float", Tokens.Float},
            {"vector", Tokens.Vector},
            {"entity", Tokens.Entity},
            {"string", Tokens.String},
            {"code", Tokens.Code}
        };

        private static readonly Dictionary<string, Tokens> Modifiers = new Dictionary<string, Tokens>
        {
            {"const", Tokens.Const},
            {"extern", Tokens.Extern},
            {"static", Tokens.Static},
            {"public", Tokens.Public},
            {"private", Tokens.Private}
        };

        public static TokenTypes GetTokenType(string currentText)
        {

            if (currentText.StartsWith("//")/*currentText.StartsWith("/*")*/)
                return TokenTypes.Comment;

            //if the current text is a keyword
            if (Keywords.ContainsKey(currentText))
                return TokenTypes.Keyword;

            if (Types.ContainsKey(currentText))
                return TokenTypes.Type;

            if (Modifiers.ContainsKey(currentText))
                return TokenTypes.Modifier;
            

            return TokenTypes.Unknown;
        }
    }
}