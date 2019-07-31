#if INTERACTIVE
#load @"..\..\.paket\load\net471\main.group.fsx"
#r "System.IO.Compression.dll"
#else
#endif

open Aardvark.Base

// prerequisites: use 64bit interactive (e.g. in visual studio: tools/options/f# tools/64bit interactive must be true)

module InteractiveHelper = 
    open System.Reflection
    open Aardvark.Base
    open Aardvark.Base.Incremental
    open System.IO.Compression
    let loadPluginsManually() = 
        let assemblies =
            [
                Assembly.Load("Aardvark.SceneGraph")
            ]
        for a in assemblies do
            Introspection.RegisterAssembly a

    let inTemp (f : string -> 'a) =
        let last = System.Environment.CurrentDirectory
        let current = System.IO.Path.GetTempPath()
        System.Environment.CurrentDirectory <- current
        let r = f current
        System.Environment.CurrentDirectory <- last
        r

    let init() =
        if not System.Environment.Is64BitProcess then
            failwith "F# interactive shell must be 64bit (e.g. in visual studio: tools/options/f# tools/64bit interactive must be true)."
        inTemp (fun currentDir ->
            printfn "temp for native dependencies: %s" currentDir
            loadPluginsManually()
            Mod.initialize()
            Ag.initialize()
            let gl = Assembly.Load("Aardvark.Rendering.GL")
            let vk = Assembly.Load("Aardvark.Rendering.Vulkan")
            Aardvark.UnpackNativeDependenciesToBaseDir(gl,currentDir)
            Aardvark.UnpackNativeDependenciesToBaseDir(vk,currentDir)
            Aardvark.Init()
            printfn "glvm ~> %A" (Aardvark.Base.DynamicLinker.tryLoadLibrary "glvm")
            printfn "vkvm ~> %A" (Aardvark.Base.DynamicLinker.tryLoadLibrary "vkvm")
        )


InteractiveHelper.init()


open System
open Aardvark.Base
open Aardvark.Base.Rendering
open Aardvark.Base.Incremental
open Aardvark.SceneGraph
open Aardvark.Application
open Aardvark.Application.WinForms



// create an OpenGL/Vulkan application. Use the use keyword (using in C#) in order to
let app = new OpenGlApplication()
// SimpleRenderWindow is a System.Windows.Forms.Form which contains a render control
// of course you can a custum form and add a control to it.
// Note that there is also a WPF binding for OpenGL. For more complex GUIs however,
// we recommend using aardvark-media anyways..
let win = app.CreateSimpleRenderWindow(samples = 8)
//win.Title <- "Hello Aardvark"

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

// create a quad using low level primitives (IndexedGeometry is our base type for specifying
// geometries using vertices etc)
let quadSg =
    let quad =
        IndexedGeometry(
            Mode = IndexedGeometryMode.TriangleList,
            IndexArray = ([|0;1;2; 0;2;3|] :> System.Array),
            IndexedAttributes =
                SymDict.ofList [
                    DefaultSemantic.Positions,                  [| V3f(-1,-1,0); V3f(1,-1,0); V3f(1,1,0); V3f(-1,1,0) |] :> Array
                    DefaultSemantic.Normals,                    [| V3f.OOI; V3f.OOI; V3f.OOI; V3f.OOI |] :> Array
                    DefaultSemantic.DiffuseColorCoordinates,    [| V2f.OO; V2f.IO; V2f.II; V2f.OI |] :> Array
                ]
        )
                
    // create a scenegraph, given a IndexedGeometry instance...
    quad |> Sg.ofIndexedGeometry

let firstScene = 
    Sg.box' C4b.White (Box3d.FromCenterAndSize(V3d.OOO,V3d.III)) 
        |> Sg.trafo (Trafo3d.RotationZInDegrees 45.0 |> Mod.constant)
        // here we use fshade to construct a shader: https://github.com/aardvark-platform/aardvark.docs/wiki/FShadeOverview

let currentScene = Mod.init firstScene

let renderTask =
    currentScene
        |> Sg.dynamic
        |> Sg.effect [
                DefaultSurfaces.trafo                 |> toEffect
                DefaultSurfaces.constantColor C4f.Red |> toEffect
                DefaultSurfaces.simpleLighting        |> toEffect
            ]
        // extract our viewTrafo from the dynamic cameraView and attach it to the scene graphs viewTrafo 
        |> Sg.viewTrafo (cameraView  |> Mod.map CameraView.viewTrafo )
        // compute a projection trafo, given the frustum contained in frustum
        |> Sg.projTrafo (frustum |> Mod.map Frustum.projTrafo    )
        |> Sg.compile win.Runtime win.FramebufferSignature


// assign the render task to our window...
win.RenderTask <- renderTask
win.Visible <- true


let change () =
    let newSg = Sg.sphere' 4 C4b.Green 1.0
    transact (fun _ -> currentScene.Value <- newSg)