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

        //function to see the token type
        public static lexer.TokenTypes GetTokenTypesSyntaxColor(string token)
        {
            switch(token)
            {
                //case if the dictionary contains the token
                case var x when Keywords.ContainsKey(x):
                    return lexer.TokenTypes.Keyword;
                default:
                    return lexer.TokenTypes.Unknown;    
            }
        }
    }
}