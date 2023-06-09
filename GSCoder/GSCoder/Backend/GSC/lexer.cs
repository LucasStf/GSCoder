using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GSCoder.Backend
{
    class lexer
    {
        public enum Tokens
        {
            PLUS,
            MINUS,
            STAR,
            DIV,
            MOD,
            BITOR,
            BITAND,
            BITEXOR,
            SHL,
            SHR,
            ASSIGN,
            PLUSEQ,
            MINUSEQ,
            STAREQ,
            DIVEQ,
            MODEQ,
            BITOREQ,
            BITANDEQ,
            BITEXOREQ,
            SHLEQ,
            SHREQ,
            INC,
            DEC,
            GT,
            LT,
            GE,
            LE,
            NE,
            EQ,
            OR,
            AND,
            TILDE,
            BANG,
            QMARK,
            COLON,
            SHARP,
            COMMA,
            DOT,
            DOUBLEDOT,
            ELLIPSIS,
            SEMICOLON,
            DOUBLECOLON,
            NEWLINE,
            LBRACKET,
            RBRACKET,
            LBRACE,
            RBRACE,
            LPAREN,
            RPAREN,
            NAME,
            PATH,
            STRING,
            ISTRING,
            HASHSTR,
            INT,
            FLT,
            DEVBEGIN,
            DEVEND,
            SINGLELCOMMENT,
            MULTILCOMMENT,
            INLINE,
            INCLUDE,
            USINGTREE,
            ANIMTREE,
            AUTOEXEC,
            CODECALL,
            PRIVATE,
            ENDON,
            NOTIFY,
            WAIT,
            WAITTILL,
            WAITTILLMATCH,
            WAITTILLFRAMEEND,
            IF,
            ELSE,
            DO,
            WHILE,
            FOR,
            FOREACH,
            IN,
            SWITCH,
            CASE,
            DEFAULT,
            BREAK,
            CONTINUE,
            RETURN,
            PROFBEGIN,
            PROFEND,
            THREAD,
            TRUE,
            FALSE,
            UNDEFINED,
            SIZE,
            GAME,
            SELF,
            ANIM,
            LEVEL,
            CONST,
            ISDEFINED,
            VECTORSCALE,
            ANGLESTOUP,
            ANGLESTORIGHT,
            ANGLESTOFORWARD,
            ANGLECLAMP180,
            VECTORTOANGLES,
            ABS,
            GETTIME,
            GETDVAR,
            GETDVARINT,
            GETDVARFLOAT,
            GETDVARVECTOR,
            GETDVARCOLORRED,
            GETDVARCOLORGREEN,
            GETDVARCOLORBLUE,
            GETDVARCOLORALPHA,
            GETFIRSTARRAYKEY,
            GETNEXTARRAYKEY,
            KEYWORD,
            OPERATOR,
        }

       public static readonly Dictionary<Tokens, string> TokenToString = new Dictionary<Tokens, string>
        {
            {Tokens.PLUS, "+"},
            {Tokens.MINUS, "-"},
            {Tokens.STAR, "*"},
            {Tokens.DIV, "/"},
            {Tokens.MOD, "%"},
            {Tokens.BITOR, "|"},
            {Tokens.BITAND, "&"},
            {Tokens.BITEXOR, "^"},
            {Tokens.SHL, "<<"},
            {Tokens.SHR, ">>"},
            {Tokens.ASSIGN, "="},
            {Tokens.PLUSEQ, "+="},
            {Tokens.MINUSEQ, "-="},
            {Tokens.STAREQ, "*="},
            {Tokens.DIVEQ, "/="},
            {Tokens.MODEQ, "%="},
            {Tokens.BITOREQ, "|="},
            {Tokens.BITANDEQ, "&="},
            {Tokens.BITEXOREQ, "^="},
            {Tokens.SHLEQ, "<<="},
            {Tokens.SHREQ, ">>="},
            {Tokens.INC, "++"},
            {Tokens.DEC, "--"},
            {Tokens.GT, ">"},
            {Tokens.LT, "<"},
            {Tokens.GE, ">="},
            {Tokens.LE, "<="},
            {Tokens.NE, "!="},
            {Tokens.EQ, "=="},
            {Tokens.OR, "||"},
            {Tokens.AND, "&&"},
            {Tokens.TILDE, "~"},
            {Tokens.BANG, "!"},
            {Tokens.QMARK, "?"},
            {Tokens.COLON, ":"},
            {Tokens.SHARP, "#"},
            {Tokens.COMMA, ","},
            {Tokens.DOT, "."},
            {Tokens.DOUBLEDOT, ".."},
            {Tokens.ELLIPSIS, "..."},
            {Tokens.SEMICOLON, ";"},
            {Tokens.DOUBLECOLON, "::"},
            {Tokens.NEWLINE, "\n"},
            {Tokens.LBRACKET, "["},
            {Tokens.RBRACKET, "]"},
            {Tokens.LBRACE, "{"},
            {Tokens.RBRACE, "}"},
            {Tokens.LPAREN, "("},
            {Tokens.RPAREN, ")"},
            {Tokens.DEVBEGIN, "/#"},
            {Tokens.DEVEND, "#/"},
            {Tokens.INLINE, "inline"},
            {Tokens.INCLUDE, "include"},
            //{Tokens.INCLUDE, "include"},
            {Tokens.USINGTREE, "#usingtree"},
            {Tokens.ANIMTREE, "#animtree"},
            {Tokens.AUTOEXEC, "autoexec"},
            {Tokens.CODECALL, "codecall"},
            {Tokens.PRIVATE, "private"},
            {Tokens.ENDON, "endon"},
            {Tokens.NOTIFY, "notify"},
            {Tokens.WAIT, "wait"},
            {Tokens.WAITTILL, "waittill"},
            {Tokens.WAITTILLMATCH, "waittillmatch"},
            {Tokens.WAITTILLFRAMEEND, "waittillframeend"},
            {Tokens.IF, "if"},
            {Tokens.ELSE, "else"},
            {Tokens.DO, "do"},
            {Tokens.WHILE, "while"},
            {Tokens.FOR, "for"},
            {Tokens.FOREACH, "foreach"},
            {Tokens.IN, "in"},
            {Tokens.SWITCH, "switch"},
            {Tokens.CASE, "case"},
            {Tokens.DEFAULT, "default"},
            {Tokens.BREAK, "break"},
            {Tokens.CONTINUE, "continue"},
            {Tokens.RETURN, "return"},
            {Tokens.PROFBEGIN, "prof_begin"},
            {Tokens.PROFEND, "prof_end"},
            {Tokens.THREAD, "thread"},
            {Tokens.TRUE, "true"},
            {Tokens.FALSE, "false"},
            {Tokens.UNDEFINED, "undefined"},
            {Tokens.SIZE, "size"},
            {Tokens.GAME, "game"},
            {Tokens.SELF, "self"},
            {Tokens.ANIM, "anim"},
            {Tokens.LEVEL, "level"},
            {Tokens.CONST, "const"},
            {Tokens.ISDEFINED, "isdefined"},
            {Tokens.VECTORSCALE, "vectorscale"},
            {Tokens.ANGLESTOUP, "anglestoup"},
            {Tokens.ANGLESTORIGHT, "anglestoright"},
            {Tokens.ANGLESTOFORWARD, "anglestoforward"},
            {Tokens.ANGLECLAMP180, "angleclamp180"},
            {Tokens.VECTORTOANGLES, "vectortoangles"},
            {Tokens.ABS, "abs"},
            {Tokens.GETTIME, "gettime"},
            {Tokens.GETDVAR, "getdvar"},
            {Tokens.GETDVARINT, "getdvarint"},
            {Tokens.GETDVARFLOAT, "getdvarfloat"},
            {Tokens.GETDVARVECTOR, "getdvarvector"},
            {Tokens.GETDVARCOLORRED, "getdvarcolorred"},
            {Tokens.GETDVARCOLORGREEN, "getdvarcolorgreen"},
            {Tokens.GETDVARCOLORBLUE, "getdvarcolorblue"},
            {Tokens.GETDVARCOLORALPHA, "getdvarcoloralpha"},
            {Tokens.GETFIRSTARRAYKEY, "getfirstarraykey"},
            {Tokens.GETNEXTARRAYKEY, "getnextarraykey"}
        };

        public static Tokens GetToken(string token)
        {
            var current_token = Tokens.NAME;

            //get the token from the string
            foreach (KeyValuePair<Tokens, string> pair in TokenToString)
            {
                if (pair.Value == token)
                {
                    current_token = pair.Key;
                }
            }
            return current_token;
        }

        public static List<string> GetStringAutoCompletion(string text)
        {
            //create a list of string
            var tokens = new List<string>();
            foreach (KeyValuePair<Tokens, string> pair in TokenToString)
            {
                if (pair.Value.Contains(text))
                {
                    tokens.Add(pair.Value);
                }
            }
            return tokens;
        }
    }
}