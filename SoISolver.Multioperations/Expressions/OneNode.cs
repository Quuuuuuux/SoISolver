namespace SoISolver.Multioperations.Expressions;

public class OneNode: ExpressionNode
{
    private static OneNode? _reference;
    
    private OneNode(){}

    public static OneNode GetOneNode()
    {
        if (_reference is null)
        {
            _reference = new OneNode();
        }

        return _reference;
    }
    
    public override string ToString()
    {
        return "1";
    }
}