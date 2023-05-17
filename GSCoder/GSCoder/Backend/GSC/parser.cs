using System;
using System.Collections.Generic;
using System.Linq;
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

        public static bool CheckSyntaxErrors(string code)
        {
            List<string> ParsedCode = GetParsedCode(code);
            List<lexer.Tokens> TokensList = new List<lexer.Tokens>();

            foreach (string token in ParsedCode)
            {
                //Console.WriteLine(token);
                TokensList.Add(lexer.GetToken(token));
            }

            bool syntaxError = false;

            if(CheckFunctionsSyntax(TokensList))
            {
                syntaxError = true;
            }
            else if(CheckWaitSyntax(TokensList))
            {
                syntaxError = true;
            }
            else if(CheckVariablesSyntax(TokensList))
            {
                syntaxError = true;
            }
            
            return syntaxError;
        }

        //check the variables syntax
        public static bool CheckVariablesSyntax(List<lexer.Tokens> tokens)
        {
            int line = 0;
            bool syntaxError = false;

            //check the variables syntax
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] == lexer.Tokens.NewLine)
                {
                    line++;
                }

                //int
                if (tokens[i] == lexer.Tokens.Int)
                {
                    if (tokens[i] != lexer.Tokens.Int || tokens[i + 1] != lexer.Tokens.Identifier || tokens[i + 2] != lexer.Tokens.Semicolon)
                    {
                        utils.WriteToLogArea("int syntax error at line " + (line + 1), true);
                        syntaxError = true;
                        break;
                    }
                    else
                    {
                        i += 3;
                    }
                }

                //string
                if (tokens[i] == lexer.Tokens.String)
                {
                    if (tokens[i] != lexer.Tokens.String || tokens[i + 1] != lexer.Tokens.Equal || tokens[i + 2] != lexer.Tokens.Semicolon)
                    {
                        utils.WriteToLogArea("string syntax error at line " + (line + 1), true);
                        syntaxError = true;
                        break;
                    }
                    else
                    {
                        i += 3;
                    }
                }
            }

            return syntaxError;
        }

        public static bool CheckFunctionsSyntax(List<lexer.Tokens> tokens)
        {
            bool syntaxError = false;
            int braceCount = 0;
            int parenCount = 0;
            int line = 0;

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] == lexer.Tokens.NewLine)
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
                        utils.WriteToLogArea($"Syntax error: unmatched right curly brace at line {line + 1}", true);
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
                        utils.WriteToLogArea($"Syntax error: unmatched right parenthesis at line {line + 1}", true);
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
                        utils.WriteToLogArea($"Syntax error: unmatched left parenthesis at line {line + 1}", true);
                        break;
                    }
                }
            }

            if (braceCount != 0)
            {
                syntaxError = true;
                utils.WriteToLogArea($"Syntax error: unmatched left curly brace at line {line + 1}", true);
            }

            return syntaxError;
        }

        //check the wait syntax
        public static bool CheckWaitSyntax(List<lexer.Tokens> tokens)
        {
            int line = 0;
            bool syntaxError = false;

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
                    if (tokens[i] != lexer.Tokens.Wait || tokens[i + 1] != lexer.Tokens.IntegerLiteral || tokens[i + 2] != lexer.Tokens.Semicolon)
                    {
                        utils.WriteToLogArea("Syntax error at line " + (line + 1), true);
                        syntaxError = true;
                        break;
                    }
                    else
                    {
                        i += 3;
                    }
                }
            }

            return syntaxError;
        }

        //function to creates AST from the tokens
        public static ASTNode CreateAST_Mock(List<lexer.Tokens> tokens)
        {
            ASTNode root = new ASTNode(NodeType.Program);

            ASTNode include1 = new ASTNode(NodeType.IncludeDirective);
            include1.Value = "maps\\mp\\gametypes\\_hud_util";
            root.Children.Add(include1);

            ASTNode include2 = new ASTNode(NodeType.IncludeDirective);
            include2.Value = "maps\\mp\\gametypes\\_rank";
            root.Children.Add(include2);

            ASTNode initFunction = new ASTNode(NodeType.FunctionDeclaration);
            initFunction.Value = "init";
            root.Children.Add(initFunction);

            ASTNode initBlock = new ASTNode(NodeType.Block);
            initFunction.Children.Add(initBlock);

            ASTNode thread = new ASTNode(NodeType.Thread);
            initBlock.Children.Add(thread);

            ASTNode threadFunction = new ASTNode(NodeType.FunctionCall);
            threadFunction.Value = "onplayerconnect";
            thread.Children.Add(threadFunction);

            ASTNode onplayerconnectFunction = new ASTNode(NodeType.FunctionDeclaration);
            onplayerconnectFunction.Value = "onplayerconnect";
            root.Children.Add(onplayerconnectFunction);

            ASTNode onplayerconnectBlock = new ASTNode(NodeType.Block);
            onplayerconnectFunction.Children.Add(onplayerconnectBlock);

            ASTNode forLoop = new ASTNode(NodeType.ForLoop);
            onplayerconnectBlock.Children.Add(forLoop);

            ASTNode waittill = new ASTNode(NodeType.WaitTill);
            waittill.Value = "connecting";
            ASTNode waittillArgument = new ASTNode(NodeType.Identifier);
            waittillArgument.Value = "player";
            waittill.AddArgument(waittillArgument);
            forLoop.Children.Add(waittill);

            ASTNode playerThread = new ASTNode(NodeType.Thread);
            forLoop.Children.Add(playerThread);

            ASTNode threadFunction2 = new ASTNode(NodeType.FunctionCall);
            threadFunction2.Value = "onplayerspawned";
            playerThread.Children.Add(threadFunction2);

            ASTNode onplayerspawnedFunction = new ASTNode(NodeType.FunctionDeclaration);
            onplayerspawnedFunction.Value = "onplayerspawned";
            root.Children.Add(onplayerspawnedFunction);

            ASTNode onplayerspawnedBlock = new ASTNode(NodeType.Block);
            onplayerspawnedFunction.Children.Add(onplayerspawnedBlock);

            ASTNode endon1 = new ASTNode(NodeType.EndOn);
            endon1.Value = "disconnect";
            onplayerspawnedBlock.Children.Add(endon1);

            ASTNode endon2 = new ASTNode(NodeType.EndOn);
            endon2.Value = "game_ended";
            onplayerspawnedBlock.Children.Add(endon2);

            ASTNode forLoop2 = new ASTNode(NodeType.ForLoop);
            onplayerspawnedBlock.Children.Add(forLoop2);

            ASTNode waittill2 = new ASTNode(NodeType.WaitTill);
            waittill2.Value = "spawned_player";
            forLoop2.Children.Add(waittill2);

            ASTNode threadFunction3 = new ASTNode(NodeType.FunctionCall);
            threadFunction3.Value = "myFunction";
            forLoop2.Children.Add(threadFunction3);

            ASTNode myFunction = new ASTNode(NodeType.FunctionDeclaration);
            myFunction.Value = "myFunction";
            root.Children.Add(myFunction);

            ASTNode myFunctionBlock = new ASTNode(NodeType.Block);
            myFunction.Children.Add(myFunctionBlock);

            ASTNode iprintlnbold = new ASTNode(NodeType.FunctionCall);
            iprintlnbold.Value = "self iprintlnbold";
            ASTNode iprintlnboldArgument = new ASTNode(NodeType.String);
            iprintlnboldArgument.Value = "^2My First Function!";
            iprintlnbold.AddArgument(iprintlnboldArgument);
            myFunctionBlock.Children.Add(iprintlnbold);

            return root;
        }

        public static uint GetOpcodeValue(opcodes.opcode opcode)
        {
            uint opcodeValue = op_t6.codes.FirstOrDefault(x => x.Value == opcode).Key;
            return opcodeValue;
        }

        public static List<byte> GenerateBytecodeFromAST(ASTNode ast)
        {
            List<byte> bytecode = new List<byte>();

            foreach (ASTNode childNode in ast.Children)
            {
                switch (childNode.Type)
                {
                    case NodeType.FunctionCall:
                        //get the uint value of the opcode from the dictionary
                        var op_byte = GetOpcodeValue(opcodes.opcode.OP_ScriptFunctionCall);
                        bytecode.Add((byte)op_byte);
                        break;
                    //default:
                        // Handle unsupported node types or parsing errors
                        //Console.WriteLine($"Unsupported node type or parsing error: {childNode.Type}");
                        //throw new Exception($"Unsupported node type or parsing error: {childNode.Type}");
                }
            }

            return bytecode;
        }
    }
}