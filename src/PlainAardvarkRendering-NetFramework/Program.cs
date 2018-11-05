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

            //HelloWorld.Run();
            //HelloAnimation.Run();
            Stub.Run();
        }
    }
}
