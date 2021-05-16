namespace Concept

// dependency graph, mod stuff
// attribute grammar implementation
open Aardvark.Base.Ag
open Aardvark.Base
open FSharp.Data.Adaptive

// placeholder for geometry description (e.g. vertices, normales etc)
type Geometry = string
// placeholder for transformations (typically M44f matrices)
type Trafo3d = string

[<AutoOpen>]
module Operators = 
    // Trafos can be multiplied (string concatenation in our case)
    let (*) (a : aval<Trafo3d>) (b : aval<Trafo3d>) =
        AVal.map2 (fun a b -> sprintf "%s%s" a b) a b

    
// object to be rendered
[<StructuredFormatDisplay("{AsString}")>] // this object contains an IMod. 
// for nice prints in this demo we evaluate its content in custom print method.
type RenderObject = { geometry : Geometry; trafo : aval<Trafo3d>} with
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

type IApplicator(child : aval<ISg>) =
    interface ISg
    member x.Child = child

type Trafo(child : aval<ISg>, trafo : aval<Trafo3d>) =
    inherit IApplicator(child)

    member x.Child = child
    member x.Trafo = trafo

type Group(children : aset<ISg>) =
    interface ISg
    member x.Children = children

[<AutoOpen>]
module Extensions = 
    type ISg with
        member sg.RenderObjects(scope : Ag.Scope) : aset<RenderObject> = 
            sg?RenderObjects(scope)
           

[<Rule>]
type RenderObjectSemantics() = 
    
    member x.RenderObjects(node : RenderNode, scope : Ag.Scope) = 
        aset {
            let t : aval<Trafo3d> = scope?Trafo
            yield { geometry = node.Geometry; trafo = t }
        }

    member x.RenderObjects(applicator : IApplicator, scope : Ag.Scope) =
        aset {
            let! child = applicator.Child
            yield! child.RenderObjects(scope)
        }

    member x.RenderObjects(group : Group, scope : Ag.Scope) =
        aset {
            for c in group.Children do
                yield! c.RenderObjects(scope)
        }

[<Rule>]
type TrafoSemantics() = 
    member x.Trafo(t : Trafo, scope : Ag.Scope) =
        t.Child?Trafo <- t.Trafo * scope?Trafo
    member x.Trafo(r : Root<ISg>, scope : Ag.Scope) = 
        r.Child?Trafo <- AVal.constant Trafo3d.identity


module Sg = 
    let geometry (s : string) = RenderNode(s) :> ISg
    let ofASet (s : aset<ISg>) = Group(s) :> ISg
    let ofSeq (xs : seq<ISg>) = Group(ASet.ofSeq xs) :> ISg
    let transformed (t : aval<Trafo3d>) (child : aval<ISg>) = 
        Trafo(child, t)
    let transformed' (t : aval<Trafo3d>) (child : ISg) =
        Trafo(AVal.constant child, t)
    let empty = Group(ASet.empty) :> ISg

module Test = 

    let run () =
        // remember trafo values in order to modify later
        // in real world scenarios interaction code such as camera controllers
        // would modify modifiables accordingly.
        let trafo1 = AVal.init "5"
        let trafo2 = AVal.init "1"
        
        // another, for now empty list of scene graphs
        let additionalNodes = cset []
        let addtionalSg = Sg.ofASet additionalNodes

        let sg1 = 
            Sg.transformed' (AVal.constant "v") (
                Sg.ofSeq [
                    Sg.transformed' trafo1 (Sg.geometry "A")
                    Sg.transformed' trafo2 (Sg.geometry "B")
                    addtionalSg
                ]
            )

        let adaptiveSet = sg1.RenderObjects(Ag.Scope.Root)
        let currentState = adaptiveSet |> ASet.force |> HashSet.toList
        printfn "to render: %A" currentState

        transact (fun _ -> 
            trafo1.Value <- "100"
        )

        let currentState = adaptiveSet |> ASet.force |> HashSet.toList
        printfn "to render: %A" currentState

        // let us now structurally modify the scene

        transact (fun _ -> 
            additionalNodes.Add (Sg.geometry "C") |> ignore
        )

        let currentState = adaptiveSet |> ASet.force |> HashSet.toList
        printfn "to render: %A" currentState

