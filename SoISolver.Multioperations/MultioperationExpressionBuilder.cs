using SoISolver.Multioperations.Expressions;

namespace SoISolver.Multioperations;

public struct ExpressionMatrixArgument
{
    public readonly ParameterNodeType Type;
    public readonly int Index;
    
    public ExpressionMatrixArgument(ParameterNodeType type, int index)
    {
        Type = type;
        Index = index;
    }
}

public class MultioperationExpressionBuilder
{
    public List<ExpressionNode> BuildExpression(Multioperation multioperation, params object[] args)
    {
        var revertArgs = args.Reverse().ToArray();
        var rank = multioperation.Rank;
        var listExpressions = FromBoolToNodes(multioperation, revertArgs);

        for (var i = 1; i < revertArgs.Length; i++)
        {
            var newListExpression = new List<ExpressionNode>();
            foreach (var chunk in listExpressions.Chunk(rank))
            {
                ExpressionNode node = ZeroNode.GetZeroNode();
                for (var j = 0; j < chunk.Length; j++)
                {
                    if(chunk[j] is ZeroNode) continue;

                    var subnode = chunk[j] * CreateNode(revertArgs[i], j, multioperation.Rank);

                    node += subnode;
                }
                newListExpression.Add(node);
            }

            listExpressions = newListExpression;
        }

        return listExpressions;
    }

    private List<ExpressionNode> FromBoolToNodes(Multioperation multioperation, object arg)
    {
        var listExpressions = new List<ExpressionNode>();
        var chunks = multioperation.GetSpaceMatrixChunks();
        
        
        foreach (var chunk in chunks)
        {
            ExpressionNode node = ZeroNode.GetZeroNode();
            for (var i = 0; i < chunk.Length; i++)
            {
                if (chunk[i])
                {
                    node += CreateNode(arg, i, multioperation.Rank);
                }
            }
            
            listExpressions.Add(node);
        }

        return listExpressions;
    }

    private ExpressionNode CreateNode(object obj, int subindex, int rank)
    {
        if (obj is List<ExpressionNode> nodes && nodes.Count == rank)
        {
            return nodes[subindex];
        }
        if (obj is ExpressionMatrixArgument matrixArg)
        {
           return new ParameterNode(matrixArg.Type, matrixArg.Index, subindex);
        }
        
        throw new ArgumentException();
        
    }
}