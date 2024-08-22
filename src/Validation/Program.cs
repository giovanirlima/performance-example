using System.Diagnostics;
using Validation.Examples.v1;

var sw = new Stopwatch();
var before2 = GC.CollectionCount(2);
var before1 = GC.CollectionCount(1);
var before0 = GC.CollectionCount(0);

//var sut = Original.ValidateCPF;
//var sut = Version2.ValidateCPF;
//var sut = Version3.ValidateCPF;
//var sut = Version4.ValidateCPF;
//var sut = Version5.ValidateCPF;
var sut = Version6.ValidateCPF;

sw.Start();

for (int i = 0; i < 1000000; i++)
{
    if (!sut("771.189.500-33"))
        throw new Exception("Error!");

    if (sut("771.189.500-34"))
        throw new Exception("Error!");
}

sw.Stop();

Console.WriteLine($"Tempo total: {sw.ElapsedMilliseconds}ms");
Console.WriteLine($"GC Gen #2  : {GC.CollectionCount(2) - before2}");
Console.WriteLine($"GC Gen #1  : {GC.CollectionCount(1) - before1}");
Console.WriteLine($"GC Gen #0  : {GC.CollectionCount(0) - before0}");
Console.WriteLine("Done!");