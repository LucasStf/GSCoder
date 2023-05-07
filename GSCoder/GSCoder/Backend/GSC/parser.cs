using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Eto.Forms;

namespace GSCoder.Backend
{
    class parser
    {
        public static List<string> GetParsedCode(string code)
        {
            // Ignorer les commentaires qui commencent par // et /* et qui finissent par */
            code = Regex.Replace(code, @"(//.*|/\*.*?\*/)", string.Empty, RegexOptions.Singleline);

            List<string> tokens = new List<string>();

            string pattern = @"\b[\w]+\b|[^\s]";

            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(code);

            foreach (Match match in matches)
            {
                tokens.Add(match.Value);
            }

            return tokens;
        }

        public static void CheckSyntaxErrors(List<lexer.Tokens> tokens)
        {
            //check the wait syntax
            for (int i = 0; i < tokens.Count; i++)
            {
                //wait
                if (tokens[i] == lexer.Tokens.Wait)
                {
                    if (CheckWaitSyntax(tokens.GetRange(i, 3)) == false)
                    {
                        Console.WriteLine("Syntax error: wait syntax is wrong");
                    }
                    else
                    {
                        i += 3;
                        Console.WriteLine("wait syntax is correct");
                    }
                }
            }
        }


        //check the wait syntax
        public static bool CheckWaitSyntax(List<lexer.Tokens> tokens)
        {
            foreach (lexer.Tokens token in tokens)
            {
                Console.WriteLine(token);
            }

            if (tokens[0] != lexer.Tokens.Wait)
                return false;

            if (tokens[1] != lexer.Tokens.IntegerLiteral)
                return false;

            if(tokens[2] != lexer.Tokens.Semicolon)
                return false;

            return true;
        }

    }
}