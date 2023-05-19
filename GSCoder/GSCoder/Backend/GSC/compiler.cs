using System;
using System.Collections.Generic;
using GSCoder.Backend;
using System.IO;
using Directories.Net;
using Eto.Forms;

namespace GSCoder.Backend
{
    class compiler
    {
        public static void Compile()
        {
            //var data = file_read.Read("/home/strafe/Téléchargements/Linux release x64 binaries/compiled/t6/main.gsc");
            var data = File.ReadAllText(project_infos.path + "/main.gsc");
            List<byte> outpout = new List<byte>();

            //for debug
            /*string magic = "";
            foreach (byte b in data)
            {
                magic += b.ToString("X2") + " ";
            }
            Console.WriteLine(magic, false);*/

            var dir = new UserDirectories();
            var path = dir.DocumentDir + "/GSCoder/Projects/Compiled/";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //create a list of tokens from the code
            List<string> ParsedCode = parser.GetParsedCode(data);

            /*foreach (lexer.Tokens token in TokensList)
            {
                Console.WriteLine(token);
            }*/

            //var ast = parser.CreateAST_Mock(TokensList);
            var ast = parser.CreateAST(ParsedCode);

            //print the ast
            foreach (ASTNode node in ast.Children)
            {
                Console.WriteLine("Node type : " + node.Type + " - " + node.Value);
                foreach (ASTNode child in node.Children)
                {
                    Console.WriteLine("Children of " + node.Type + " : " + child.Type);
                }
            }

            //print the ast_mock
            /*foreach (ASTNode node in ast.Children)
            {
                Console.WriteLine("Node type : " + node.Type + " - " + node.Value);
                foreach (ASTNode child in node.Children)
                {
                    Console.WriteLine("Children of " + node.Type + " : " + child.Type);
                    //print the children of the children
                    foreach (ASTNode child2 in child.Children)
                    {
                        Console.WriteLine("Children of " + child.Type + " : " + child2.Type + " - " + child2.Value);
                    }
                }
            }*/

            /*outpout = parser.GenerateBytecodeFromAST(ast);

            // Créer le fichier compilé
            using (BinaryWriter writer = new BinaryWriter(File.Open(path + "main.gsc", FileMode.Create)))
            {
                writer.Write(outpout.ToArray());
            }*/

            MessageBox.Show("Compilation done", "GSCoder");
        }
    }
}