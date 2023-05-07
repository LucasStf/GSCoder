using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
            Return,
            Case,
            Continue,
            Default,
            Do,
            Else,
            For,
            If,
            Switch,
            While,
            Wait,
            Self,
            Thread,
            Level,
            Maps,
            Game,
            Anim,
            AnimTree,
            Struct,
            StructDef,
            Enum,
            EnumDef,
            Precache,
            PrecacheModel,
            PrecacheShader,
            PrecacheFont,
            PrecacheFx,
            PrecacheMenu,
            PrecacheString,
            PrecacheMaterial,


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

            Unknown
        }

        private static readonly Dictionary<string, Tokens> Keywords = new Dictionary<string, Tokens>
        {
            {"break", Tokens.Break},
            {"return", Tokens.Return},
            {"case", Tokens.Case},
            {"continue", Tokens.Continue},
            {"default", Tokens.Default},
            {"do", Tokens.Do},
            {"else", Tokens.Else},
            {"for", Tokens.For},
            {"if", Tokens.If},
            {"switch", Tokens.Switch},
            {"while", Tokens.While},
            {"wait", Tokens.Wait},
            {"self", Tokens.Self},
            {"thread", Tokens.Thread},
            {"level", Tokens.Level},
            {"maps", Tokens.Maps},
            {"game", Tokens.Game},
            {"anim", Tokens.Anim},
            {"animtree", Tokens.AnimTree},
            {"struct", Tokens.Struct},
            {"structdef", Tokens.StructDef},
            {"enum", Tokens.Enum},
            {"enumdef", Tokens.EnumDef},
            {"precache", Tokens.Precache},
            {"precache_model", Tokens.PrecacheModel},
            {"precache_shader", Tokens.PrecacheShader},
            {"precache_font", Tokens.PrecacheFont},
            {"precache_fx", Tokens.PrecacheFx},
            {"precache_menu", Tokens.PrecacheMenu},
            {"precache_string", Tokens.PrecacheString},
            {"precache_material", Tokens.PrecacheMaterial}
        };

        private static readonly Dictionary<string, Tokens> Operators = new Dictionary<string, Tokens>
        {
            {"=", Tokens.Assign},
            {"+=", Tokens.AddAssign},
            {"-=", Tokens.SubAssign},
            {"*=", Tokens.MulAssign},
            {"/=", Tokens.DivAssign},
            {"%=", Tokens.ModAssign},
            {"++", Tokens.Inc},
            {"--", Tokens.Dec},
            {"==", Tokens.Equal},
            {"!=", Tokens.NotEqual},
            {">", Tokens.GreaterThan},
            {"<", Tokens.LessThan},
            {">=", Tokens.GreaterThanOrEqual},
            {"<=", Tokens.LessThanOrEqual},
            {"&&", Tokens.LogicalAnd},
            {"||", Tokens.LogicalOr},
            {"&", Tokens.BitwiseAnd},
            {"|", Tokens.BitwiseOr},
            {"^", Tokens.BitwiseXor},
            {"~", Tokens.BitwiseNot},
            {"<<", Tokens.ShiftLeft},
            {">>", Tokens.ShiftRight}
        };

        private static readonly Dictionary<string, Tokens> Punctuation = new Dictionary<string, Tokens>
        {
            {";", Tokens.Semicolon},
            {",", Tokens.Comma},
            {".", Tokens.Dot},
            {"->", Tokens.Arrow},
            {":", Tokens.Colon},
            {"::", Tokens.DoubleColon},
            {"?", Tokens.QuestionMark},
            {"(", Tokens.LeftParenthesis},
            {")", Tokens.RightParenthesis},
            {"[", Tokens.LeftBracket},
            {"]", Tokens.RightBracket},
            {"{", Tokens.LeftBrace},
            {"}", Tokens.RightBrace}
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
            {
                return TokenTypes.Comment;
            }

            switch (currentText)
            {
                case var x when Keywords.ContainsKey(x):
                case var y when Types.ContainsKey(y):
                    return TokenTypes.Keyword;
                case var z when Modifiers.ContainsKey(z):
                    return TokenTypes.Modifier;

                //if this is a number
                case var a when Regex.IsMatch(a, @"^\d+$"):
                    return TokenTypes.Integer;
                default:
                    return TokenTypes.Unknown;
            }
        }

        //function  that returns the tokens assiocated in the dictionary
        public static Tokens GetToken(string currentText)
        {
            switch (currentText)
            {
                case var x when Keywords.ContainsKey(x):
                    return Keywords[x];
                case var y when Types.ContainsKey(y):
                    return Types[y];
                case var z when Modifiers.ContainsKey(z):
                    return Modifiers[z];
                case var a when Operators.ContainsKey(a):
                    return Operators[a];
                case var b when Punctuation.ContainsKey(b):
                    return Punctuation[b];
                //if this is a number
                case var c when Regex.IsMatch(c, @"^\d+$"):
                    return Tokens.IntegerLiteral;    
                default:
                    return Tokens.Unknown;
            }
        }
    }
}