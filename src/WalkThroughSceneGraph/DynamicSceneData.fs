module DynamicSceneData 

open System
open Aardvark.Base
open Aardvark.Base.Rendering
open Aardvark.Base.Incremental
open Aardvark.SceneGraph
open Aardvark.Application
open Aardvark.Application.Slim
open Aardvark.SceneGraph.IO
open Aardvark.Base.Incremental.Operators

let run () = 

    // create an OpenGL/Vulkan application. Use the use keyword (using in C#) in order to
    // properly dipose resources on shutdown...
    use app = new OpenGlApplication()
    // SimpleRenderWindow is a System.Windows.Forms.Form which contains a render control
    // of course you can a custum form and add a control to it.
    // Note that there is also a WPF binding for OpenGL. For more complex GUIs however,
    // we recommend using aardvark-media anyways..
    let win = app.CreateGameWindow(samples = 8)

    // Given eye, target and sky vector we compute our initial camera pose
    let initialView = CameraView.LookAt(V3d(3.0,3.0,3.0), V3d.Zero, V3d.OOI)
    // the class Frustum describes camera frusta, which can be used to compute a projection matrix.
    let frustum = 
        // the frustum needs to depend on the window size (in oder to get proper aspect ratio)
        win.Sizes 
            // construct a standard perspective frustum (60 degrees horizontal field of view,
            // near plane 0.1, far plane 50.0 and aspect ratio x/y.
            |> Mod.map (fun s -> Frustum.perspective 60.0 0.1 50.0 (float s.X / float s.Y))

    // create a controlled camera using the window mouse and keyboard input devices
    // the window also provides a so called time mod, which serves as tick signal to create
    // animations - seealso: https://github.com/aardvark-platform/aardvark.docs/wiki/animation
    let cameraView = DefaultCameraController.control win.Mouse win.Keyboard win.Time initialView
    
    
    let rnd = new System.Random()
    let objectPositions : cset<V3d> = 
        CSet.ofList [ 
            for i in 0 .. 3 do
                yield V3d(rnd.NextDouble(),rnd.NextDouble(),rnd.NextDouble())
       ]
       
    win.Keyboard.DownWithRepeats.Values.Add(fun k -> 
        match k with
            | Keys.G -> 
                transact (fun _ -> 
                    let newPos = V3d(rnd.NextDouble(),rnd.NextDouble(),rnd.NextDouble())
                    objectPositions.Add newPos |> ignore
                )
            | _ ->
                ()
    )

    let objects = 
        objectPositions |> ASet.map (fun p -> 
            let b = Box3d.FromCenterAndSize(V3d.Zero,V3d.III * 0.2)
            let obj = Sg.box' C4b.Green b
            Sg.translate p.X p.Y p.Z obj
        )

    let scene = Sg.set objects

    let timeDependentRotation = 
        let sw = System.Diagnostics.Stopwatch.StartNew()
        win.Time |> Mod.map (fun _ -> 
            Trafo3d.RotationZ(sw.Elapsed.TotalSeconds * 0.2)
        )

    let objects = 
        objectPositions |> ASet.map (fun p -> 
            let b = Box3d.FromCenterAndSize(V3d.Zero,V3d.III * 0.2)
            Sg.box' C4b.Green b 
            |> Sg.trafo timeDependentRotation
            |> Sg.translate p.X p.Y p.Z 
        )

    //let scene = Sg.set objects

    let sg =
        scene
            // here we use fshade to construct a shader: https://github.com/aardvark-platform/aardvark.docs/wiki/FShadeOverview
            |> Sg.effect [
                    DefaultSurfaces.trafo                 |> toEffect
                    DefaultSurfaces.constantColor C4f.Red |> toEffect
                    DefaultSurfaces.simpleLighting        |> toEffect
                ]
            // extract our viewTrafo from the dynamic cameraView and attach it to the scene graphs viewTrafo 
            |> Sg.viewTrafo (cameraView  |> Mod.map CameraView.viewTrafo )
            // compute a projection trafo, given the frustum contained in frustum
            |> Sg.projTrafo (frustum |> Mod.map Frustum.projTrafo    )


    let renderTask = 
        // compile the scene graph into a render task
        app.Runtime.CompileRender(win.FramebufferSignature, sg)

    // assign the render task to our window...
    win.RenderTask <- renderTask
    win.Run()