namespace FunctionalFrontend.Model

open Adaptify
open Aardvark.UI.Primitives

type Primitive =
    | Box
    | Sphere

[<ModelType>]
type Model =
    {
        currentModel : Primitive
        cameraState  : CameraControllerState
    }