using SoISolver.Multioperations;

// var builder = WebApplication.CreateBuilder(args);
// var app = builder.Build();
//
// app.MapGet("/", () => "Hello World!");

short r = 3;
short d = 2;

var len = Math.Pow(r, d);

var a = new Multioperation(r, d, new byte[] { 2,4,5,6,3,1,7,2,2 });

for (var i = 0; i < r; ++i)
{
    Console.Write("[ ");
    for (short j = 0; j < len; ++j)
    {
        Console.Write(a[i, j] ? 1 : 0);
        if ((j+1)%r == 0) Console.Write(" ");
    }
    Console.WriteLine(" ]");
}


// app.Run();