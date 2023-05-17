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
            List<lexer.Tokens> TokensList = new List<lexer.Tokens>();

            foreach (string token in ParsedCode)
            {
                //Console.WriteLine(token);
                TokensList.Add(lexer.GetToken(token));
            }

            var ast = parser.CreateAST_Mock(TokensList);
            //print the ast
            foreach (ASTNode node in ast.Children)
            {
                Console.WriteLine(node.Type);
                foreach (ASTNode child in node.Children)
                {
                    Console.WriteLine(child.Type);
                }
            }

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