open System
open System.Diagnostics
open Aardvark.Base
open Aardvark.Rendering
open FSharp.Data.Adaptive
open Aardvark.SceneGraph
open Aardvark.SceneGraph.IO
open Aardvark.Application
open Aardvark.Application.Slim
open System.Threading

[<EntryPoint>]
let main argv = 
 
    // first we need to initialize Aardvark's core components
    Aardvark.Init()

    // create an OpenGL/Vulkan application. Use the use keyword (using in C#) in order to
    // properly dipose resources on shutdown...
    use app = new OpenGlApplication()

    // GameWindow is a GLFW window underneath.
    let win = app.CreateGameWindow(samples = 8)
    win.Focus()


    // load the aardvark model
    let modelPath = Path.combine [__SOURCE_DIRECTORY__; ".."; ".."; "data"; "aardvark"; "aardvark.obj" ]

    let aardvark = 
        Loader.Assimp.load modelPath
         |> Sg.adapter
         |> Sg.normalizeTo (Box3d(-V3d.III, V3d.III))
         |> Sg.transform (Trafo3d.FromOrthoNormalBasis(V3d.IOO,V3d.OIO,-V3d.OOI))
         |> Sg.translate 0.0 0.0 0.5
         |> Sg.shader {
               do! DefaultSurfaces.trafo
               do! DefaultSurfaces.constantColor C4f.White
               do! DefaultSurfaces.diffuseTexture
               do! DefaultSurfaces.normalMap
               do! DefaultSurfaces.simpleLighting
           }

    let floor =
        Sg.quad
        |> Sg.scale 100.0
        |> Sg.diffuseTexture DefaultTextures.checkerboard
        |> Sg.shader {
            do! DefaultSurfaces.trafo
            do! DefaultSurfaces.diffuseTexture
        }

    // initialize a changeable transformation for our aardvark model
    let aardvarkTrafo = cval Trafo3d.Identity

    // get the key states for the relevant keys
    let forward = win.Keyboard.IsDown Keys.Up
    let backward = win.Keyboard.IsDown Keys.Down
    let left = win.Keyboard.IsDown Keys.Left
    let right = win.Keyboard.IsDown Keys.Right

    // calculate a "move vector" encoding the movement
    // direction of the model (depending on the key-states)
    let moveVec =
        let moveVecLR = 
            (left, right) ||> AVal.map2 (fun l r ->
                (if l then V3d.IOO else V3d.OOO) +
                (if r then -V3d.IOO else V3d.OOO)
            )

        let moveVecFB = 
            (forward, backward) ||> AVal.map2 (fun fw bw ->
                (if fw then -V3d.OIO else V3d.OOO) +
                (if bw then V3d.OIO else V3d.OOO)
            )
        (moveVecLR, moveVecFB) 
        ||> AVal.map2 (+)
         
    // the aardvark's speed
    let speed = 1.5

    

    // whenever an image is rendered and the "move vector" is not
    // zero we step the aardvarkTrafo accordingly.
    // Note that there is no "game-loop" but instead the rendering
    // of a frame causes the rendering of the next (via changing the transformation)
    // Also note that this approach has one major pitfall:
    // when no frame is ever rendered the animation will not start ==> see below
    let mutable frameCounter = 0
    
    let sw = Stopwatch()
    win.BeforeRender.Add(fun () ->
        sw.Restart()
    )


    win.AfterRender.Add(fun () ->
        sw.Stop()
        // get the current move-vector
        let dir = AVal.force moveVec

        if not (Fun.IsTiny dir) then
            let dt = sw.Elapsed.TotalSeconds
            transact (fun () ->
                aardvarkTrafo.Value <- 
                    aardvarkTrafo.Value * 
                    Trafo3d.Translation(dir * speed * dt)
            )
        frameCounter <- frameCounter + 1
    )
    // whenever the "move vector" changes we (wrongly) tell the 
    // rendering system that the aardvarkTrafo has changed in order
    // to get it to render a frame.
    moveVec.AddCallback(fun _ ->
        transact (fun () ->
            aardvarkTrafo.MarkOutdated()
        )
    ) |> ignore


    // get the aardvark's center position (0,0,0 in local coords)
    let center =
        aardvarkTrafo |> AVal.map (fun t -> t.Forward.TransformPos V3d.Zero)
        
    // let the camera look at the model
    let cameraPosition = V3d(3.0,3.0,3.0)
    let cameraView =
        center |> AVal.map (fun c ->
            CameraView.lookAt cameraPosition c V3d.OOI
        )

    // the class Frustum describes camera frusta, which can be used to compute a projection matrix.
    let frustum = 
        // the frustum needs to depend on the window size (in oder to get proper aspect ratio)
        win.Sizes 
            // construct a standard perspective frustum (60 degrees horizontal field of view,
            // near plane 0.1, far plane 50.0 and aspect ratio x/y.
            |> AVal.map (fun s -> Frustum.perspective 60.0 0.1 50.0 (float s.X / float s.Y))


    let sg =
        Sg.ofList [ 
            aardvark
            |> Sg.trafo aardvarkTrafo
            floor
        ]
        // extract our viewTrafo from the dynamic cameraView and attach it to the scene graphs viewTrafo 
        |> Sg.viewTrafo (cameraView |> AVal.map CameraView.viewTrafo )
        // compute a projection trafo, given the frustum contained in frustum
        |> Sg.projTrafo (frustum |> AVal.map Frustum.projTrafo    )


    let renderTask = 
        // compile the scene graph into a render task
        app.Runtime.CompileRender(win.FramebufferSignature, sg)

    // assign the render task to our window...
    win.RenderTask <- renderTask
    win.Run()
    0