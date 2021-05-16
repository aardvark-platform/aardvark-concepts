namespace FunctionalFrontend

open FSharp.Data.Adaptive
open System
open Aardvark.Base
open Aardvark.UI
open Aardvark.UI.Primitives
open Aardvark.Rendering
open FunctionalFrontend.Model

type Message =
    | ToggleModel
    | CameraMessage of FreeFlyController.Message

module App =
    
    let initial = { currentModel = Box; cameraState = FreeFlyController.initial }

    let update (m : Model) (msg : Message) =
        match msg with
            | ToggleModel -> 
                match m.currentModel with
                    | Box -> { m with currentModel = Sphere }
                    | Sphere -> { m with currentModel = Box }

            | CameraMessage msg ->
                { m with cameraState = FreeFlyController.update m.cameraState msg }

    let view (m : AdaptiveModel) =

        let frustum = 
            Frustum.perspective 60.0 0.1 100.0 1.0 
                |> AVal.constant

        let sg =
            m.currentModel |> AVal.map (fun v ->
                match v with
                    | Box -> Sg.box (AVal.constant C4b.Red) (AVal.constant (Box3d(-V3d.III, V3d.III)))
                    | Sphere -> Sg.sphere 5 (AVal.constant C4b.Green) (AVal.constant 1.0)
            )
            |> Sg.dynamic
            |> Sg.shader {
                do! DefaultSurfaces.trafo
                do! DefaultSurfaces.simpleLighting
            }

        let att =
            [
                style "position: fixed; left: 0; top: 0; width: 100%; height: 100%"
            ]

        body [] [
            FreeFlyController.controlledControl m.cameraState CameraMessage frustum (AttributeMap.ofList att) sg

            div [style "position: fixed; left: 20px; top: 20px"] [
                button [onClick (fun _ -> ToggleModel)] [text "Toggle Model"]
            ]

        ]

    let app =
        {
            initial = initial
            update = update
            view = view
            threads = fun m -> m.cameraState |> FreeFlyController.threads |> ThreadPool.map CameraMessage
            unpersist = Unpersist.instance
        }