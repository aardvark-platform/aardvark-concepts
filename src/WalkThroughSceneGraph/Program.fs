open System
open Aardvark.Base
open Aardvark.Base.Rendering
open Aardvark.Base.Incremental
open Aardvark.SceneGraph
open Aardvark.Application
open Aardvark.Application.Slim

type DemoType = 
    | SceneGraphConcept
    | ExtendingSceneGraphs
    | DynamicPointCloud
    | DynamicSceneData
    | AdaptiveDSL

[<EntryPoint>]
let main argv = 
 
    let demo = DemoType.ExtendingSceneGraphs

    // first we need to initialize Aardvark's core components
    Ag.initialize()
    Aardvark.Init()

    match demo with
        | SceneGraphConcept -> 
            Concept.Test.run()
            0

        | ExtendingSceneGraphs -> 
            ExtendingSceneGraphs.run()
            0

        | DynamicPointCloud -> 
            DynamicPointCloud.run()
            0

        | DynamicSceneData -> 
            DynamicSceneData.run()
            0

        | AdaptiveDSL -> 
            AdaptiveDSLApproach.run()
            0