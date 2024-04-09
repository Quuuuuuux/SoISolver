namespace SoISolver.Multioperations.Expressions;

public enum OperationType
{
    Conjunction,
    Disjunction,
}

public class OperationNode: ExpressionNode, IEquatable<OperationNode>
{
    public OperationType Type { get; init; }
    public List<ExpressionNode> Arguments { get; init; }

    public OperationNode(OperationType type, params ExpressionNode[] args)
    {
        Type = type;
        Arguments = new List<ExpressionNode>();
        Arguments.AddRange(args);
    }

    public bool Equals(OperationNode? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Type == other.Type && Arguments.TrueForAll(x => other.Arguments.Contains(x));
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((OperationNode)obj);
    }

    public override string ToString()
    {
        var separator = Type is OperationType.Conjunction ? "*" : "+";
        return string.Join(separator, Arguments);
    }
}