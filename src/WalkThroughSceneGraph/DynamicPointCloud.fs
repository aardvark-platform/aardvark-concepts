module DynamicPointCloud

open Aardvark.Base
open Aardvark.Base.Rendering
open Aardvark.Base.Incremental
open Aardvark.SceneGraph
open Aardvark.Application


let run () = 

    // window { ... } is similar to show { ... } but instead
    // of directly showing the window we get the window-instance
    // and may show it later.
    let win =
        window {
            backend Backend.GL
            display Display.Mono
            debug false
            samples 8
        }

    let cnt = 10000
    let generateVertices () =
        let rand = System.Random()
        let points = Array.init cnt (fun _ -> 
            V3f(rand.NextDouble(),rand.NextDouble(),rand.NextDouble())
        )
        ArrayBuffer(points) :> IBuffer

    let currentBuffer = generateVertices () |> Mod.init
    let verticesBufferView = BufferView(currentBuffer, typeof<V3f>)

    win.Keyboard.DownWithRepeats.Values.Add(fun k -> 
        match k with
            | Keys.G -> 
                transact (fun _ -> 
                    currentBuffer.Value <- generateVertices()
                )
            | _ ->
                ()
    )

    let sg = 
        Sg.draw IndexedGeometryMode.PointList
        |> Sg.vertexBuffer DefaultSemantic.Positions verticesBufferView
        |> Sg.shader {
                do! DefaultSurfaces.trafo
                do! DefaultSurfaces.constantColor C4f.Red
            }
    
    // show the window
    win.Scene <- sg
    win.Run()

