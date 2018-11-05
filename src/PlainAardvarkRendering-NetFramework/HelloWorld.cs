/*
 * This example shows how to use the C# to construct a scene graph from first principles in order to finally
 * render a quad.
 * Note that this is for sake of demonstration in order to show the expressive but composable primitives.
 * In real-world scenarios one would use convience functions which internally use this low level
 * scene graph API.
 */
using System;
using Aardvark.Base;
using Aardvark.Base.Incremental.CSharp;
using Aardvark.SceneGraph;
using Aardvark.SceneGraph.CSharp;
using Aardvark.Application.WinForms;
using Effects = Aardvark.Base.Rendering.Effects;

namespace PlainAardvarkRendering_NetFramework
{
    class HelloWorld
    {
        public static void Run()
        {
            using (var app = /*new VulkanApplication() */ new OpenGlApplication())
            {
                var win = app.CreateSimpleRenderWindow(samples: 8);

                // create CPU side array
                var indices = new int[] { 0, 1, 2, 0, 2, 3 };
                // wrap it into cpu buffer. ArrayBuffer is a CPU buffer (which will be uploaded on demand),
                // In contrast, BackendBuffer would be a buffer prepared for a specific backend.
                // both implement the IBuffer interface.
                var indexBuffer = (IBuffer)new ArrayBuffer(indices);

                // same applies for vertex data. Here we do not explicitly create an ArrayBuffer since
                // we use convinience functions which internally create the ArrayBuffer for us
                var vertices = new V3f[] {
                    new V3f(-1, -1, 0), new V3f(1, -1, 0), new V3f(1, 1, 0), new V3f(-1, 1, 0) };
                var colors = new C4b[] {
                    C4b.Green, C4b.Red, C4b.Blue, C4b.White };

                // In this low level API, we manually construct a drawCallInfo which essentially map
                // to the arguments of glDrawElements etc.
                var drawCallInfo = new DrawCallInfo()
                {
                    FaceVertexCount = 6,
                    InstanceCount = 1, // DrawCallInfo is a struct and is initialized with zeros. make sure to set instanceCount to 1
                    FirstIndex = 0,
                };

                // next we create a scene graph node which describes a simple scene which, when rendered
                // uses the supplied drawCallInfo to render geometry of type TriangleList (in constrast to points, linestrip etc)
                var drawNode = new Sg.RenderNode(drawCallInfo, IndexedGeometryMode.TriangleList);
                // the main principle is to use scene graph nodes as small building blocks to build together the
                // complete scene description - a bit like lego ;)
                // the same applies for applying geometry data. just like any other attribute (e.g. model trafos),
                // vertex data can be inherited along the edges in the scene graph. thus the scene graph would look like this
                //             VertexIndexApplicator   (applies index buffer to sub graph)
                //                 ^
                //                 |
                //              drawNode               (performs draw call using attributes inherited along scene graph edges)
                var sceneWithIndexBuffer =
                        new Sg.VertexIndexApplicator(
                                new BufferView(Mod.Constant(indexBuffer), typeof(int)), 
                                drawNode
                        );

                // of course constructing scene graph nodes manually is tedious. therefore we use 
                // convinience extension functions which can be chaned together, each
                // wrapping a node around the previously constructed scene graph
                var scene =
                    sceneWithIndexBuffer
                    .WithVertexAttribute("Positions", vertices)
                    // there are a lot such extension functions defined to conviniently work with scene graphs
                    .VertexAttribute(DefaultSemantic.Colors, colors) 
                    // next, we apply the shaders (this way, the shader becomes the root node -> all children now use
                    // this so called effect (a pipeline shader which combines all shader stages into one object)
                    .WithEffects(new[] { Aardvark.Base.Rendering.Effects.VertexColor.Effect });

                // next we use the aardvark scene graph compiler to construct a so called render task,
                // an optimized representation of the scene graph.
                var renderTask = app.Runtime.CompileRender(win.FramebufferSignature, scene);

                // next, we assign the rendertask to our render window.
                win.RenderTask = renderTask;

                win.Run();
            }
        }
    }

    public static class Extensions
    {
        public static ISg WithVertexAttribute(this ISg sg, string semantic, Array data)
        {
            var bufferView = new BufferView(Mod.Constant((IBuffer)new ArrayBuffer(data)), data.GetType().GetElementType());
            return new Sg.VertexAttributeApplicator(Symbol.Create(semantic), bufferView, Mod.Constant(sg));
        }
    }
}
