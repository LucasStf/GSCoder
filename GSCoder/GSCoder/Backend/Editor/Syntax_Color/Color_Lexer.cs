using System.Collections.Generic;

namespace GSCoder.Backend
{
    class Color_Lexer
    {
        //create a dictionary of all keywoards tokens to colorize
        public static readonly Dictionary<string, lexer.Tokens> Keywords = new Dictionary<string, lexer.Tokens>
        {
            {"break", lexer.Tokens.Break},
            {"return", lexer.Tokens.Return},
            {"case", lexer.Tokens.Case},
            {"continue", lexer.Tokens.Continue},
            {"default", lexer.Tokens.Default},
            {"do", lexer.Tokens.Do},
            {"else", lexer.Tokens.Else},
            {"for", lexer.Tokens.For},
            {"if", lexer.Tokens.If},
            {"switch", lexer.Tokens.Switch},
            {"while", lexer.Tokens.While},
            {"wait", lexer.Tokens.Wait},
            {"self", lexer.Tokens.Self},
            {"thread", lexer.Tokens.Thread},
            {"level", lexer.Tokens.Level},
        };

        public static readonly Dictionary<string, lexer.Tokens> Comments = new Dictionary<string, lexer.Tokens>
        {
            {"//", lexer.Tokens.SingleLineComment},
            {"/*", lexer.Tokens.MultiLineComment},
        };

        public static readonly Dictionary<string, lexer.Tokens> Operators = new Dictionary<string, lexer.Tokens>
        {
            {"=", lexer.Tokens.Assign},
            {"+=", lexer.Tokens.AddAssign},
            {"-=", lexer.Tokens.SubAssign},
            {"*=", lexer.Tokens.MulAssign},
            {"/=", lexer.Tokens.DivAssign},
            {"%=", lexer.Tokens.ModAssign},
            {"++", lexer.Tokens.Inc},
            {"--", lexer.Tokens.Dec},
            {"==", lexer.Tokens.Equal},
            {"!=", lexer.Tokens.NotEqual},
            {">", lexer.Tokens.GreaterThan},
            {"<", lexer.Tokens.LessThan},
            {">=", lexer.Tokens.GreaterThanOrEqual},
            {"<=", lexer.Tokens.LessThanOrEqual},
            {"&&", lexer.Tokens.LogicalAnd},
            {"||", lexer.Tokens.LogicalOr},
            {"&", lexer.Tokens.BitwiseAnd},
            {"|", lexer.Tokens.BitwiseOr},
            {"^", lexer.Tokens.BitwiseXor},
            {"~", lexer.Tokens.BitwiseNot},
            {"<<", lexer.Tokens.ShiftLeft},
            {">>", lexer.Tokens.ShiftRight},
            {"true", lexer.Tokens.True},
            {"false", lexer.Tokens.False},
        };

        //function to see the token type
        public static lexer.TokenTypes GetTokenTypesSyntaxColor(string token)
        {
            switch(token)
            {
                //case if the dictionary contains the token
                case var x when Keywords.ContainsKey(x):
                    return lexer.TokenTypes.Keyword;
                case var x when Comments.ContainsKey(x):
                    return lexer.TokenTypes.Comment; 
                //if the token contains an ; at the end
                case var x when (Operators.ContainsKey(x)) || (Operators.ContainsKey(x) && x.EndsWith(";")):
                    return lexer.TokenTypes.Operator;  
                //if the token contains an " at the start and end
                case var x when x.StartsWith("\"") && x.EndsWith("\""):
                    return lexer.TokenTypes.String;     
                default:
                    return lexer.TokenTypes.Unknown;    
            }
        }
    }
}