using System.Collections.Generic;

namespace GSCoder.Backend
{
    public class ASTNode
    {
        public NodeType Type { get; set; }
        public string Value { get; set; }
        public List<ASTNode> Children { get; set; }

        public ASTNode(NodeType type)
        {
            Type = type;
            Children = new List<ASTNode>();
        }

        public void AddArgument(ASTNode argument)
        {
            Children.Add(argument);
        }
    }

    public enum NodeType
    {
        Program,
        IncludeDirective,
        FunctionDeclaration,
        Block,
        Thread,
        FunctionCall,
        ForLoop,
        WaitTill,
        EndOn,
        String,
        Integer,
        Identifier,
    }
}