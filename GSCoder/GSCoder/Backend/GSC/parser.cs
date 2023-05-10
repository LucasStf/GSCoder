using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GSCoder.Backend
{
    class parser
    {
        public static List<string> GetParsedCode(string code)
        {
            // Ignorer les commentaires qui commencent par // et /* et qui finissent par */
            //code = Regex.Replace(code, @"(//.*|/\*.*?\*/)", string.Empty, RegexOptions.Singleline);

            List<string> tokens = new List<string>();

            string[] lines = code.Split('\n');
            foreach (string line in lines)
            {
                string pattern = @"\b[\w]+\b|[^\s]";

                Regex regex = new Regex(pattern);
                MatchCollection matches = regex.Matches(line);

                foreach (Match match in matches)
                {
                    tokens.Add(match.Value);
                }

                tokens.Add("\n"); // Ajouter une nouvelle ligne à la fin de chaque ligne
            }

            return tokens;
        }


        public static bool CheckFunctionsSyntax(List<lexer.Tokens> tokens)
        {
            bool syntaxError = false;
            int braceCount = 0;
            int parenCount = 0;
            int line = 0;

            for (int i = 0; i < tokens.Count; i++)
            {
                if(tokens[i] == lexer.Tokens.NewLine)
                {
                    line++;
                }
                else if (tokens[i] == lexer.Tokens.LeftBrace)
                {
                    braceCount++;
                }
                else if (tokens[i] == lexer.Tokens.RightBrace)
                {
                    if (--braceCount < 0)
                    {
                        syntaxError = true;
                        utils.WriteToLogArea($"Syntax error: unmatched right curly brace at line {line}", true);
                        break;
                    }
                }
                else if (tokens[i] == lexer.Tokens.LeftParenthesis)
                {
                    parenCount++;
                }
                else if (tokens[i] == lexer.Tokens.RightParenthesis)
                {
                    if (--parenCount < 0)
                    {
                        syntaxError = true;
                        utils.WriteToLogArea($"Syntax error: unmatched right parenthesis at line {line}", true);
                        break;
                    }
                }
                else if (braceCount == 0 && parenCount == 0 && tokens[i] == lexer.Tokens.Unknown && i < tokens.Count - 1 && tokens[i+1] == lexer.Tokens.LeftParenthesis)
                {
                    int j = i + 2;
                    parenCount = 1;
                    while (j < tokens.Count)
                    {
                        if (tokens[j] == lexer.Tokens.LeftParenthesis)
                        {
                            parenCount++;
                        }
                        else if (tokens[j] == lexer.Tokens.RightParenthesis)
                        {
                            if (--parenCount == 0)
                            {
                                i = j;
                                break;
                            }
                        }
                        j++;
                    }
                    if (parenCount != 0)
                    {
                        syntaxError = true;
                        utils.WriteToLogArea($"Syntax error: unmatched left parenthesis at line {line}", true);
                        break;
                    }
                }
            }

            if (braceCount != 0)
            {
                syntaxError = true;
                utils.WriteToLogArea("Syntax error: unmatched left curly brace", true);
            }
            else if (parenCount != 0)
            {
                syntaxError = true;
                utils.WriteToLogArea("Syntax error: unmatched left parenthesis", true);
            }

            return syntaxError;
        }

        public static bool CheckSyntaxErrors(List<lexer.Tokens> tokens)
        {
            bool syntaxError = false;
            int line = 0;

            if(CheckFunctionsSyntax(tokens) == true)
            {
                syntaxError = true;
            }

            //check the wait syntax
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] == lexer.Tokens.NewLine)
                {
                    line++;
                }

                //wait
                if (tokens[i] == lexer.Tokens.Wait)
                {
                    if (CheckWaitSyntax(tokens.GetRange(i, 3)) == false)
                    {
                        utils.WriteToLogArea("Syntax error at line " + line, true);
                        syntaxError = true;
                    }
                    else
                    {
                        i += 3;
                    }
                }
            }
            return syntaxError;
        }

        //check the wait syntax
        public static bool CheckWaitSyntax(List<lexer.Tokens> tokens)
        {
            /*foreach (lexer.Tokens token in tokens)
            {
                Console.WriteLine(token);
            }*/

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