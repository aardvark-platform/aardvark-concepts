using Aardvark.Base;
using FSharp.Data.Adaptive;
using CSharp.Data.Adaptive;
using Aardvark.Rendering;
using Aardvark.Rendering.CSharp;
using Aardvark.SceneGraph;
using Aardvark.SceneGraph.CSharp;
using Aardvark.Application.Slim;
using Effects = Aardvark.Rendering.Effects;

namespace PlainAardvarkRendering
{
    class HelloAnimation
    {
        public static void Run()
        {
            using var app = new VulkanApplication();
            using var win = app.CreateGameWindow(samples: 8);

            var cone = IndexedGeometryPrimitives.Cone.solidCone(V3d.OOO, V3d.OOI, 1.0, 0.2, 48, C4b.Red).ToSg(); // build object from indexgeometry primitives
            var cube = SgPrimitives.Sg.box(AValModule.constant(C4b.Blue), AValModule.constant(Box3d.FromCenterAndSize(V3d.Zero, V3d.III))); // or directly using scene graph
            var initialViewTrafo = CameraView.LookAt(V3d.III * 3.0, V3d.OOO, V3d.OOI);
            var controlledViewTrafo = Aardvark.Application.DefaultCameraController.control(win.Mouse, win.Keyboard,
                    win.Time, initialViewTrafo);
            var frustum = win.Sizes.Map(size => FrustumModule.perspective(60.0, 0.1, 10.0, size.X / (float)size.Y));

            var whiteShader = Effects.SimpleLighting.Effect;
            var trafo = Effects.Trafo.Effect;

            var currentAngle = 0.0;
            var angle = win.Time.Map(t =>
            {
                return currentAngle += 0.001;
            });
            var rotatingTrafo = angle.Map(a => Trafo3d.RotationZ(a));

            var sg =
                new[] {
                    cone.Trafo(AValModule.constant(Trafo3d.Translation(1.0,1.0,0.0))),
                    cube.Trafo(rotatingTrafo)
                }
                .ToSg()
                .WithEffects([trafo, whiteShader])
                .ViewTrafo(controlledViewTrafo.Map(c => c.ViewTrafo))
                .ProjTrafo(frustum.Map(f => f.ProjTrafo()));

            win.RenderTask =
                RenderTask.ofArray(
                    [
                        app.Runtime.CompileClear(win.FramebufferSignature, Clear.Color(C4f.Gray10)),
                        app.Runtime.CompileRender(win.FramebufferSignature, sg)
                    ]
                    );

            win.Run();
        }
    }
}
