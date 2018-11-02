using Aardvark.Base;
using Aardvark.Base.Incremental.CSharp;
using Aardvark.SceneGraph;
using Aardvark.SceneGraph.CSharp;
using Aardvark.Application.WinForms;
using Effects = Aardvark.Base.Rendering.Effects;

namespace PlainAardvarkRendering_NetFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Aardvark.Base.Aardvark.Init();

            using (var app = /*new VulkanApplication() */ new OpenGlApplication() )
            {
                var win = app.CreateSimpleRenderWindow(samples : 8);

                var cone = IndexedGeometryPrimitives.Cone.solidCone(V3d.OOO, V3d.OOI, 1.0, 0.2, 48, C4b.Red).ToSg(); // build object from indexgeometry primitives
                var cube = SgPrimitives.Sg.box(Mod.Init(C4b.Blue), Mod.Init(Box3d.FromCenterAndSize(V3d.Zero,V3d.III))); // or directly using scene graph
                var initialViewTrafo = CameraView.LookAt(V3d.III * 3.0, V3d.OOO, V3d.OOI);
                var controlledViewTrafo = Aardvark.Application.DefaultCameraController.control(win.Mouse, win.Keyboard,
                        win.Time, initialViewTrafo);
                var frustum = win.Sizes.Map(size => FrustumModule.perspective(60.0, 0.1, 10.0, size.X / (float)size.Y));

                var whiteShader = Aardvark.Base.Rendering.Effects.SimpleLighting.Effect;
                var trafo = Effects.Trafo.Effect;

                var currentAngle = 0.0;
                var angle = win.Time.Map(t =>
                {
                    return currentAngle += 0.001;
                });
                var rotatingTrafo = angle.Map(a => Trafo3d.RotationZ(a));

                var sg =
                    new[] {
                        cone.Trafo(Mod.Constant(Trafo3d.Translation(1.0,1.0,0.0))),
                        cube.Trafo(rotatingTrafo)
                    }
                    .ToSg()
                    .WithEffects(new[] { trafo, whiteShader })
                    .ViewTrafo(controlledViewTrafo.Map(c => c.ViewTrafo))
                    .ProjTrafo(frustum.Map(f => f.ProjTrafo()));

                win.RenderTask =
                        Aardvark.Base.RenderTask.ofArray(
                                new[] {
                                    app.Runtime.CompileClear(win.FramebufferSignature, Mod.Init(C4f.Gray10)),
                                    app.Runtime.CompileRender(win.FramebufferSignature,sg)
                                 }
                         );

                win.Run();
            }
        }
    }
}
