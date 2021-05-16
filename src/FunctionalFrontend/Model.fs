namespace FunctionalFrontend.Model

open Adaptify
open System
open Aardvark.Base
open Aardvark.UI.Primitives

type Primitive =
    | Box
    | Sphere


[<ModelType>]
type Model =
    {
        currentModel    : Primitive
        cameraState     : CameraControllerState
    }