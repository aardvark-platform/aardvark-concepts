using System;
using System.Collections.Generic;
using Aardvark.Base;
using FSharp.Data.Adaptive;
using CSharp.Data.Adaptive;

namespace IncrementalSystem_CSharp
{
    class Program
    {
        static void Main()
        {
            // Create a modref cell. can be changed via side effects
            var input = new ChangeableValue<int>(10);

            var output = input.Map(x => x * 2);

            Console.WriteLine($"output was: {output}");
            // Prints: output was Aardvark.Base.Incremental.ModModule+MapMod`2[System.Int32,System.Int32]
            // not what we expected. Since mods are lazy and tostring does not force them we need
            // to pull the value out of it.

            Console.WriteLine($"output was: {output.GetValue()}"); // F# equivalent: Mod.force : IMod<'a> -> 'a
            // output was: 20

            using (Adaptive.Transact)
            {
                input.Value = 20;
            }

            Console.WriteLine($"output was: {output.GetValue()}");
            // output was: 40

            // semantically, output now dependens on input declaratively. 
            // the dependency graph looks like:
            //   
            //       (x) => x * 2
            // input ----------------> output
            // mods are nodes, and the edges are annotated with transition functions.


            // the same works for collection types, e.g. an unordered set
            // can be created as such:
            var inputSet = new ChangeableHashSet<int>(new List<int> { 1, 2, 100 });

            // similarly to LINQ, there are extensions for incrementally 
            // reacting to changes on the input set.
            var less10 = inputSet.Where(x => x < 10);

            Console.Write("less10: ");
            // similarly to GetValue(), ToArray() evaluates the current state
            // of the adaptive set instance.
            less10.ToArray().ForEach(x => Console.Write($"{x},"));
            Console.WriteLine();

            // again, atomic modifications can be submitted using a transaction.
            using (Adaptive.Transact)
            {
                inputSet.AddRange(new List<int>() { 3, 10000 });
            }

            // and re-evaluate to investigate the changes.
            Console.Write("less10: ");
            less10.ToArray().ForEach(x => Console.Write($"{x},"));
            Console.WriteLine();
        }
    }
}
