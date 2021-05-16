/*
 * Stub setup for interactive demos (HelloWorld.cs is the final result)
 */
using System;
using Aardvark.Base;
using Aardvark.SceneGraph;
using Aardvark.SceneGraph.CSharp;
using Aardvark.Application.WinForms;
using Effects = Aardvark.Rendering.Effects;
using Aardvark.Rendering;
using FSharp.Data.Adaptive;

namespace PlainAardvarkRendering_NetFramework
{
    class Stub
    {
        public static void Run()
        {
            using (var app = /*new VulkanApplication() */ new OpenGlApplication())
            {
                var win = app.CreateSimpleRenderWindow(samples: 8);

                

                win.Run();
            }
        }
    }
    public static class ExtensionsStub
    {
        public static ISg WithVertexAttributeStub(this ISg sg, string semantic, Array data)
        {
            var bufferView = new BufferView(AValModule.constant((IBuffer)new ArrayBuffer(data)), data.GetType().GetElementType());
            return new Sg.VertexAttributeApplicator(Symbol.Create(semantic), bufferView, AValModule.constant(sg));
        }
    }

}
