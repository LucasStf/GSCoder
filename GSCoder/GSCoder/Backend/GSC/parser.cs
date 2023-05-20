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
                if (tokens[i] == lexer.Tokens.NEWLINE)
                {
                    line++;
                }

                //string
                if (tokens[i] == lexer.Tokens.STRING)
                {
                    if (tokens[i] != lexer.Tokens.STRING || tokens[i + 1] != lexer.Tokens.EQ || tokens[i + 2] != lexer.Tokens.SEMICOLON)
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
                if (tokens[i] == lexer.Tokens.NEWLINE)
                {
                    line++;
                }
                else if (tokens[i] == lexer.Tokens.LBRACE)
                {
                    braceCount++;
                }
                else if (tokens[i] == lexer.Tokens.RBRACE)
                {
                    if (--braceCount < 0)
                    {
                        syntaxError = true;
                        utils.WriteToLogArea($"Syntax error: unmatched right curly brace at line {line + 1}", true);
                        break;
                    }
                }
                else if (tokens[i] == lexer.Tokens.LPAREN)
                {
                    parenCount++;
                }
                else if (tokens[i] == lexer.Tokens.RPAREN)
                {
                    if (--parenCount < 0)
                    {
                        syntaxError = true;
                        utils.WriteToLogArea($"Syntax error: unmatched right parenthesis at line {line + 1}", true);
                        break;
                    }
                }
                else if (braceCount == 0 && parenCount == 0 && tokens[i] == lexer.Tokens.NAME && i < tokens.Count - 1 && tokens[i+1] == lexer.Tokens.LPAREN)
                {
                    int j = i + 2;
                    parenCount = 1;
                    while (j < tokens.Count)
                    {
                        if (tokens[j] == lexer.Tokens.LPAREN)
                        {
                            parenCount++;
                        }
                        else if (tokens[j] == lexer.Tokens.RPAREN)
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
                if (tokens[i] == lexer.Tokens.NEWLINE)
                {
                    line++;
                }

                //wait
                if (tokens[i] == lexer.Tokens.WAIT)
                {
                    if (tokens[i] != lexer.Tokens.WAIT || tokens[i + 1] != lexer.Tokens.INT || tokens[i + 2] != lexer.Tokens.SEMICOLON)
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

            //Init function
            ASTNode initFunction = new ASTNode(NodeType.FunctionDeclaration);
            initFunction.Value = "init";
            root.Children.Add(initFunction);

            ASTNode initBlock = new ASTNode(NodeType.Block);
            initFunction.Children.Add(initBlock);
            //end init function

            ASTNode thread = new ASTNode(NodeType.FunctionCall);
            thread.Value = "onplayerconnect";
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

        public static void PrintAST(ASTNode node, string indent = "")
        {
            Console.WriteLine(indent + "Node type: " + node.Type + " - " + node.Value);

            foreach (ASTNode child in node.Children)
            {
                Console.WriteLine(indent + "  Children of " + node.Type + ":");

                PrintAST(child, indent + "    ");
            }
        }

        //function to creates AST from the tokens
        public static ASTNode CreateAST(List<string> code)
        {
            ASTNode root = new ASTNode(NodeType.Program);
            int codeLength = code.Count;

            for (int i = 0; i < codeLength; i++)
            {
                switch (lexer.GetToken(code[i]))
                {
                    case lexer.Tokens.SHARP when code[i + 1] == "include":
                        //include
                        string includePath = "";
                        ASTNode include = new ASTNode(NodeType.IncludeDirective);

                        //get the include path
                        for (int j = i + 1; j < code.Count; j++)
                        {
                            if (lexer.GetToken(code[j]) == lexer.Tokens.SEMICOLON)
                            {
                                i = j;
                                break;
                            }
                            else
                            {
                                includePath += code[j];
                            }
                        }
                        include.Value = includePath;
                        root.Children.Add(include);
                    break;
                    case lexer.Tokens.NAME when code[i + 1] == "(" && code[i + 2] == ")" && code[i + 3] != ";":
                        ASTNode function = new ASTNode(NodeType.FunctionDeclaration);
                    function.Value = code[i];
                    root.Children.Add(function);

                    ASTNode block = GenerateFunctionBlock(code, i + 1);
                    function.Children.Add(block);
                    break;
                }
            }

            return root;
        }

        //function to generate the block of the function
        public static ASTNode GenerateFunctionBlock(List<string> code, int index)
        {
            ASTNode block = new ASTNode(NodeType.Block);

            // get the function block
            for (int j = index + 1; j < code.Count; j++)
            {
                if (lexer.GetToken(code[j]) == lexer.Tokens.LBRACE)
                {
                    int braceCount = 1;
                    for (int k = j + 1; k < code.Count; k++)
                    {
                        if (lexer.GetToken(code[k]) == lexer.Tokens.LBRACE)
                        {
                            braceCount++;
                        }
                        else if (lexer.GetToken(code[k]) == lexer.Tokens.RBRACE)
                        {
                            braceCount--;
                            if (braceCount == 0)
                            {
                                index = k;
                                break;
                            }
                        }

                        var tokenType = lexer.GetToken(code[k]);
                        var nodeType = NodeType.Unknown;

                        // Check if the token can have a block
                        if (tokenType == lexer.Tokens.FOR)
                        {
                            nodeType = NodeType.For;
                            var forBlock = GenerateFunctionBlock(code, k + 1);
                            var forNode = new ASTNode(nodeType) { Value = nodeType.ToString() };
                            forNode.Children.Add(forBlock);
                            block.Children.Add(forNode);
                        }
                        else
                        {
                            block.Children.Add(new ASTNode(nodeType) { Value = lexer.GetToken(code[k]).ToString() });
                        }
                    }
                    break;
                }
            }

            return block;
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