using System.Collections.Generic;

namespace GSCoder.Backend
{
    class Color_Lexer
    {
        //create a dictionary of all keywoards tokens to colorize
        public static readonly Dictionary<string, lexer.Tokens> Keyword = new Dictionary<string, lexer.Tokens>
        {
            {"break", lexer.Tokens.BREAK},
            {"return", lexer.Tokens.RETURN},
            {"case", lexer.Tokens.CASE},
            {"continue", lexer.Tokens.CONTINUE},
            {"default", lexer.Tokens.DEFAULT},
            {"do", lexer.Tokens.DO},
            {"else", lexer.Tokens.ELSE},
            {"for", lexer.Tokens.FOR},
            {"if", lexer.Tokens.IF},
            {"switch", lexer.Tokens.SWITCH},
            {"while", lexer.Tokens.WHILE},
            {"wait", lexer.Tokens.WAIT},
            {"self", lexer.Tokens.SELF},
            {"thread", lexer.Tokens.THREAD},
            {"level", lexer.Tokens.LEVEL},
        };

        public static readonly Dictionary<string, lexer.Tokens> Operators = new Dictionary<string, lexer.Tokens>
        {
            {"//", lexer.Tokens.SINGLELCOMMENT},
            {"/*", lexer.Tokens.MULTILCOMMENT},
            {"=", lexer.Tokens.ASSIGN},
            {"+=", lexer.Tokens.PLUSEQ},
            {"-=", lexer.Tokens.MINUSEQ},
            {"*=", lexer.Tokens.STAREQ},
            {"/=", lexer.Tokens.DIVEQ},
            {"%=", lexer.Tokens.MODEQ},
            {"++", lexer.Tokens.INC},
            {"--", lexer.Tokens.DEC},
            {"==", lexer.Tokens.EQ},
            {"!=", lexer.Tokens.NE},
            {">", lexer.Tokens.GT},
            {"<", lexer.Tokens.LT},
            {">=", lexer.Tokens.GE},
            {"<=", lexer.Tokens.LE},
            {"&&", lexer.Tokens.AND},
            {"||", lexer.Tokens.OR},
            {"&", lexer.Tokens.BITAND},
            {"|", lexer.Tokens.BITOR},
            {"^", lexer.Tokens.BITEXOR},
            {"~", lexer.Tokens.TILDE},
            {"<<", lexer.Tokens.SHL},
            {">>", lexer.Tokens.SHR},
            {"true", lexer.Tokens.TRUE},
            {"false", lexer.Tokens.FALSE},
        };

        //function to see the token type
        public static lexer.Tokens GetTokenTypesSyntaxColor(string token)
        {
            var tokenType = lexer.Tokens.NAME;

            //check if the token is a keyword
            if (Keyword.ContainsKey(token))
            {
                tokenType = lexer.Tokens.KEYWORD;
            }
            //check if the token is an operator
            else if (Operators.ContainsKey(token))
            {
                tokenType = lexer.Tokens.OPERATOR;
            }

            return tokenType;
        }
    }
}