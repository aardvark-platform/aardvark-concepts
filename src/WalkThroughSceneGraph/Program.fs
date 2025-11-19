open Aardvark.Base

type DemoType = 
    | SceneGraphConcept
    | ExtendingSceneGraphs
    | DynamicPointCloud
    | DynamicSceneData
    | AdaptiveDSL

[<EntryPoint>]
let main _argv = 
 
    let demo = DemoType.ExtendingSceneGraphs

    // first we need to initialize Aardvark's core components
    Aardvark.Init()

    match demo with
    | SceneGraphConcept    -> Concept.Test.run()
    | ExtendingSceneGraphs -> ExtendingSceneGraphs.run()
    | DynamicPointCloud    -> DynamicPointCloud.run()
    | DynamicSceneData     -> DynamicSceneData.run()
    | AdaptiveDSL          -> AdaptiveDSLApproach.run()

    0