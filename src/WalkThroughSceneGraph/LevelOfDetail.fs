namespace Aardvark.SceneGraph

open Aardvark.Base
open Aardvark.SceneGraph
open Aardvark.SceneGraph.Semantics
open FSharp.Data.Adaptive
open Aardvark.Rendering

type LodScope = { cameraPosition : V3d; bb : Box3d }
type LodNode(viewDecider : (LodScope -> bool), low : ISg, high : ISg) =
    inherit Sg.AbstractApplicator(low)

    member x.Low = low
    member x.High = high
    member x.ViewDecider = viewDecider

[<Rule>]
type LodSem() =

    member x.RenderObjects(node : LodNode, scope : Ag.Scope) : aset<IRenderObject> =
        aset {
            let bb       = node.Low.GlobalBoundingBox(scope)
            let lowQuality  = node.Low.RenderObjects(scope)
            let highQuality = node.High.RenderObjects(scope)

            let! camera = scope.CameraLocation

            if node.ViewDecider { cameraPosition = camera; bb = AVal.force bb } then 
                yield! highQuality
            else    
                yield! lowQuality
        }

module Sg =

    let lod (decider : LodScope -> bool) (low : ISg) (high : ISg)= 
        LodNode(decider,low,high) :> ISg