namespace Concept

// dependency graph, mod stuff
open Aardvark.Base.Incremental
// attribute grammar implementation
open Aardvark.Base.Ag

// placeholder for geometry description (e.g. vertices, normales etc)
type Geometry = string
// placeholder for transformations (typically M44f matrices)
type Trafo3d = string

[<AutoOpen>]
module Operators = 
    // Trafos can be multiplied (string concatenation in our case)
    let (*) (a : IMod<Trafo3d>) (b : IMod<Trafo3d>) =
        Mod.map2 (fun a b -> sprintf "%s%s" a b) a b

    
// object to be rendered
[<StructuredFormatDisplay("{AsString}")>] // this object contains an IMod. 
// for nice prints in this demo we evaluate its content in custom print method.
type RenderObject = { geometry : Geometry; trafo : IMod<Trafo3d>} with
    member x.AsString = sprintf "{ geometry = %A; trafo = %A }" x.geometry (x.trafo.GetValue())

// provide functions for transformations in this module
module Trafo3d = 
    // identity transformation is empty string 
    let identity = ""

// empty marker interface for scene graph nodes
type ISg = interface end

type RenderNode(geometry : string) =
    interface ISg
    member x.Geometry = geometry

type IApplicator(child : IMod<ISg>) =
    interface ISg
    member x.Child = child

type Trafo(child : IMod<ISg>, trafo : IMod<Trafo3d>) =
    inherit IApplicator(child)

    member x.Child = child
    member x.Trafo = trafo

type Group(children : aset<ISg>) =
    interface ISg
    member x.Children = children

[<AutoOpen>]
module Extensions = 
    type ISg with
        member sg.RenderObjects() : aset<RenderObject> = 
            sg?RenderObjects()
           

[<Semantic>]
type RenderObjectSemantics() = 
    
    member x.RenderObjects(node : RenderNode) = 
        aset {
            let t : IMod<Trafo3d> = node?Trafo
            yield { geometry = node.Geometry; trafo = t }
        }

    member x.RenderObjects(applicator : IApplicator) =
        aset {
            let! child = applicator.Child
            yield! child.RenderObjects()
        }

    member x.RenderObjects(group : Group) =
        aset {
            for c in group.Children do
                yield! c.RenderObjects()
        }

[<Semantic>]
type TrafoSemantics() = 
    member x.Trafo(t : Trafo) =
        t.Child?Trafo <- t.Trafo * t?Trafo
    member x.Trafo(r : Root<ISg>) = 
        r.Child?Trafo <- Mod.constant Trafo3d.identity


module Sg = 
    let geometry (s : string) = RenderNode(s) :> ISg
    let ofASet (s : aset<ISg>) = Group(s) :> ISg
    let ofSeq (xs : seq<ISg>) = Group(ASet.ofSeq xs) :> ISg
    let transformed (t : IMod<Trafo3d>) (child : IMod<ISg>) = 
        Trafo(child, t)
    let transformed' (t : IMod<Trafo3d>) (child : ISg) =
        Trafo(Mod.constant child, t)
    let empty = Group(ASet.empty) :> ISg

module Test = 

    let run () =
        Aardvark.Base.Ag.initialize()

        // remember trafo values in order to modify later
        // in real world scenarios interaction code such as camera controllers
        // would modify modifiables accordingly.
        let trafo1 = Mod.init "5"
        let trafo2 = Mod.init "1"
        
        // another, for now empty list of scene graphs
        let additionalNodes = CSet.ofList []
        let addtionalSg = Sg.ofASet additionalNodes

        let sg1 = 
            Sg.transformed' (Mod.init "v") (
                Sg.ofSeq [
                    Sg.transformed' trafo1 (Sg.geometry "A")
                    Sg.transformed' trafo2 (Sg.geometry "B")
                    addtionalSg
                ]
            )

        let adaptiveSet = sg1.RenderObjects()
        let currentState = adaptiveSet |> ASet.toList
        printfn "to render: %A" currentState

        transact (fun _ -> 
            trafo1.Value <- "100"
        )

        let currentState = adaptiveSet |> ASet.toList
        printfn "to render: %A" currentState

        // let us now structurally modify the scene

        transact (fun _ -> 
            CSet.add (Sg.geometry "C") additionalNodes |> ignore
        )

        let currentState = adaptiveSet |> ASet.toList
        printfn "to render: %A" currentState

