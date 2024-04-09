namespace SoISolver.Multioperations.Expressions;

public class ZeroNode: ExpressionNode
{
    private static ZeroNode? _reference = null!;
    
    private ZeroNode(){}

    public static ZeroNode GetZeroNode()
    {
        if (_reference is null)
        {
            _reference = new ZeroNode();
        }

        return _reference;
    }
    
    public override string ToString()
    {
        return "0";
    }
}