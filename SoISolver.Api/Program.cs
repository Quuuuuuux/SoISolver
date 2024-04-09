using SoISolver.Multioperations;
using SoISolver.Multioperations.Expressions;

// var builder = WebApplication.CreateBuilder(args);
// var app = builder.Build();
//
// app.MapGet("/", () => "Hello World!");

short r = 2;
short d = 3;

var len = Math.Pow(r, d);

var a = new Multioperation(r, d, new byte[] { 3,3,2,0,0,1,1,2 });

// short r = 2;
// short d = 2;
//
// var len = Math.Pow(r, d);
//
// var a = new Multioperation(r, d, new byte[] { 1,3,3,2 });

for (var i = 0; i < r; ++i)
{
    Console.Write("[ ");
    for (short j = 0; j < len; ++j)
    {
        var value = a[i, j];
        
        Console.Write(value ? 1 : 0);

        if ((j+1)%r == 0) Console.Write(" ");
    }
    Console.WriteLine(" ]");
}

var b = new MultioperationExpressionBuilder();

try
{
    var expression = b.BuildExpression(a, new []{
        new ExpressionMatrixArgument(ParameterNodeType.Coefficient, 1), 
        new ExpressionMatrixArgument(ParameterNodeType.Variable, 1),
       new ExpressionMatrixArgument(ParameterNodeType.Variable, 2)}
    );
    
    Console.WriteLine("--------------------------------------------------------\n");
    Console.WriteLine(expression);
    Console.WriteLine("end");
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}

// app.Run();