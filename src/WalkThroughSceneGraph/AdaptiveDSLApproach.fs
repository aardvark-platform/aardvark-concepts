module AdaptiveDSLApproach

open Aardvark.Base
open FSharp.Data.Adaptive

let run () =

    let input1 = cval 5

    let output1 = 
        AVal.map (fun a -> a * 2) input1

    printfn "output was: %d" (AVal.force output1) // 10

    transact (fun _ -> 
        input1.Value <- 10
    )

    let output2 =
        input1 |> AVal.map (fun a -> a * 2) // 20
    
    printfn "output was: %d" (AVal.force output2)

    let input2 = AVal.init 10

    let summedResult1 = AVal.map2 (fun l r -> l + r) input1 input2

    printfn "summedResult1 was: %d" (AVal.force summedResult1) // 10 + 10 = 20

    // batch change
    transact (fun _ -> 
        input1.Value <- 1
        input2.Value <- 1000
    )

    printfn "summedResult1 was: %d" (AVal.force summedResult1)
    
    // how does this extend to mulitple inputs? Mod.map3... 
    // is there a more flexible approach => use DSL

    let summedResult2 = 
        adaptive {
            // control flow allowed here.
            let! currentInput1 = input1
            printfn "reexecute from currentInput1"
            let! currentInput2 = input2
            printfn "reexecute from currentInput2"
            return currentInput1 + currentInput2
        }

    printfn "summedResult2 was: %d" (AVal.force summedResult2) // 1001

    transact (fun _ -> 
        input2.Value <- 1
    )

    printfn "summedResult2 was: %d" (AVal.force summedResult2) //2

    // same works for sets
    let inputSet = cset [1;2;3]

    let outputSet1 = inputSet |> ASet.map (fun a -> a + 1)

    printfn "outputSet was: %A" (ASet.force outputSet1) //[2;3;4]

    transact (fun _ -> 
        inputSet.Add 4 |> ignore
    )

    printfn "outputSet was: %A" (ASet.force outputSet1) //[2;3;4;5]

    // DSL approach
    let inputSet2 = inputSet :> aset<int>
    let outputSet2 =
        aset {
            let! currentInput1 = input1
            printfn "evaluate outputSet2"
            for e in inputSet2 do
                yield e + currentInput1
        }

    printfn "outputSet2 was: %A" (ASet.force outputSet2) // [2;3;4;5]

    transact (fun _ -> 
        input1.Value <- 999
        inputSet.Add(5) |> ignore
    )

    printfn "outputSet2 was: %A" (ASet.force outputSet2) // [1000; 1001; 1002; 1003; 1004]