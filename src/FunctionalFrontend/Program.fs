open FunctionalFrontend

open Aardium
open Aardvark.Service
open Aardvark.UI
open Suave
open Suave.WebPart
open Aardvark.Rendering.Vulkan
open Aardvark.Application.Slim
open Aardvark.Base
open System


[<EntryPoint>]
let main args =
    Ag.initialize()
    Aardvark.Init()
    Aardium.init()

    //use app = new HeadlessVulkanApplication(true)
    use app = new OpenGlApplication()

    WebPart.startServer 4321 [
        MutableApp.toWebPart' app.Runtime false (App.start App.app)
    ]
    
    Aardium.run {
        title "Aardvark rocks \\o/"
        width 1024
        height 768
        url "http://localhost:4321/"
    }

    0
