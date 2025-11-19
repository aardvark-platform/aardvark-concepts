open FunctionalFrontend

open Aardium
open Aardvark.UI
open Suave
open Aardvark.Application.Slim
open Aardvark.Base

[<EntryPoint>]
let main _args =
    Aardvark.Init()
    Aardium.init()

    //use app = new HeadlessVulkanApplication(true)
    use app = new OpenGlApplication()

    WebPart.startServerLocalhost 4321 [
        MutableApp.toWebPart' app.Runtime false (App.start App.app)
    ] |> ignore
    
    Aardium.run {
        title "Aardvark rocks \\o/"
        width 1024
        height 768
        url "http://localhost:4321/"
    }

    0
