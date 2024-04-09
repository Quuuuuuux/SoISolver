namespace SoISolver.Multioperations.Expressions;

public enum ParameterNodeType
{
    Variable,
    Coefficient,
}

public class ParameterNode: ExpressionNode, IEquatable<ParameterNode>
{
    public ParameterNodeType Type { get; init; }
    public int Index { get; init; }
    public int SubIndex { get; init; }

    public ParameterNode(ParameterNodeType type, int index, int subIndex)
    {
        Type = type;
        Index = index;
        SubIndex = subIndex;
    }

    public bool Equals(ParameterNode? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Type == other.Type && Index == other.Index && SubIndex == other.SubIndex;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ParameterNode)obj);
    }

    public override string ToString()
    {
        var type = Type is ParameterNodeType.Coefficient ? "C" : "X";
        return $"{type}{Index}{SubIndex}";
    }
}