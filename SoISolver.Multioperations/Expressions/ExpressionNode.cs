namespace SoISolver.Multioperations.Expressions;

public abstract class ExpressionNode
{
    public static ExpressionNode operator *(ExpressionNode node1, ExpressionNode node2)
    {
        if(node1 is ZeroNode || node2 is ZeroNode) return ZeroNode.GetZeroNode();
        if(node1 is OneNode) return node2;
        if(node2 is OneNode) return node1;
        
        if (node1 is ParameterNode param1 && node2 is ParameterNode param2)
        {
            if (param1.Equals(param2)) return param1;
            
            return new OperationNode(OperationType.Conjunction,node1, node2);
        }
        
        if (node1 is OperationNode operation1 && node2 is OperationNode operation2)
        {
            if (operation1.Type == operation2.Type)
            {
                switch (operation1.Type)
                {
                    case OperationType.Conjunction:
                        var newArgs = new List<ExpressionNode>(operation1.Arguments);
                        newArgs.AddRange(operation2.Arguments.Where(arg => !newArgs.Contains(arg)));
               
                        return new OperationNode(OperationType.Conjunction, newArgs.ToArray());
                    
                    case OperationType.Disjunction:
                        return new OperationNode(OperationType.Conjunction, operation1, operation2);
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var disjunction = operation1.Type == OperationType.Disjunction ? operation1 : operation2;
            var conjunction = operation1.Type == OperationType.Conjunction ? operation1 : operation2;

            if (conjunction.Arguments.Any(arg => disjunction.Arguments.Contains(arg)))
            {
                return conjunction;
            }

            var newDisjunction = StackMultiplyConjunctionAndDisjunction(conjunction, disjunction);
            return newDisjunction;
        }
        
        var operation = node1 is OperationNode op ? op : (OperationNode)node2;
        var parameter = node1 is ParameterNode param ? param : (ParameterNode)node2;
       
        switch (operation.Type)
        {
            case OperationType.Conjunction:
                if (operation.Arguments.All(arg => !parameter.Equals(arg)))
                {
                    operation.Arguments.Add(parameter);
                }
                return operation;
            
            case OperationType.Disjunction:
                if (operation.Arguments.Any(arg => parameter.Equals(arg)))
                {
                    return parameter;
                }
                return StackMultiplyParameterAndDisjunction(parameter, operation);
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public static ExpressionNode operator +(ExpressionNode node1, ExpressionNode node2)
    {
        if(node1 is OneNode || node2 is OneNode) return OneNode.GetOneNode();
        if(node1 is ZeroNode) return node2;
        if(node2 is ZeroNode) return node1;

        if (node1 is ParameterNode param1 && node2 is ParameterNode param2)
        {
            if (param1.Equals(param2)) return param1;
            
            return new OperationNode(OperationType.Disjunction,node1, node2);
        }
        
        if (node1 is OperationNode operation1 && node2 is OperationNode operation2)
        {
            if (operation1.Type == operation2.Type)
            {
                switch (operation1.Type)
                {
                    case OperationType.Conjunction:
                        return new OperationNode(OperationType.Disjunction, operation1, operation2);
                    
                    case OperationType.Disjunction:
                        var newArgs = new List<ExpressionNode>(operation1.Arguments);
                        newArgs.AddRange(operation2.Arguments.Where(arg => !newArgs.Contains(arg)));
               
                        return new OperationNode(OperationType.Disjunction, newArgs.ToArray());
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var disjunction = operation1.Type == OperationType.Disjunction ? operation1 : operation2;
            var conjunction = operation1.Type == OperationType.Conjunction ? operation1 : operation2;

            if (conjunction.Arguments.Any(arg => disjunction.Arguments.Contains(arg)))
            {
                return disjunction;
            }
            
            disjunction.Arguments.Add(conjunction);
            return disjunction;
        }
        
        var operation = node1 is OperationNode op ? op : (OperationNode)node2;
        var parameter = node1 is ParameterNode param ? param : (ParameterNode)node2;
       
        switch (operation.Type)
        {
            case OperationType.Conjunction:
                if (operation.Arguments.Any(arg => parameter.Equals(arg)))
                {
                    return parameter;
                }
                return new OperationNode(OperationType.Disjunction,node1, node2);
            
            case OperationType.Disjunction:
                if (operation.Arguments.All(arg => !parameter.Equals(arg)))
                {
                    operation.Arguments.Add(parameter);
                }
                return operation;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static ExpressionNode StackMultiplyConjunctionAndDisjunction(OperationNode conjunction, OperationNode disjunction)
    {
        var newDisjunction = new OperationNode(OperationType.Disjunction);
        
        foreach (var conjunctionArg in conjunction.Arguments)
        {
            var stack = new Stack<ExpressionNode>();
            stack.Push(disjunction);

            while (stack.TryPop(out var node))
            {
                if (node is OperationNode { Type: OperationType.Disjunction } operation)
                {
                    foreach (var operationArg in operation.Arguments)
                    {
                        stack.Push(operationArg);
                    }
                    continue;
                }
                
                newDisjunction.Arguments.Add(conjunctionArg * node);
            }
        }

        return newDisjunction;
    }

    // private static ExpressionNode StackMultiplyDisjunctionAndDisjunction(OperationNode disjunction1, OperationNode disjunction2)
    // {
    //     var newDisjunction = new OperationNode(OperationType.Disjunction);
    //     
    // }
    
    private static ExpressionNode StackMultiplyParameterAndDisjunction(ParameterNode parameter, OperationNode disjunction)
    {
        var newDisjunction = new OperationNode(OperationType.Disjunction);
        
        var stack = new Stack<ExpressionNode>();
        stack.Push(disjunction);

        while (stack.TryPop(out var node))
        {
            if (node is OperationNode { Type: OperationType.Disjunction } operation)
            {
                foreach (var operationArg in operation.Arguments)
                {
                    stack.Push(operationArg);
                }
                continue;
            }
            
            newDisjunction.Arguments.Add(parameter * node);
        }

        return newDisjunction;
    }
}