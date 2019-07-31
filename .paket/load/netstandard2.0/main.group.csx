namespace PaketLoadScripts

#r "../../../packages/Suave/lib/netstandard1.6/Suave.dll" 
#r "../../../packages/System.Private.DataContractSerialization/lib/netstandard1.3/System.Private.DataContractSerialization.dll" 
#r "../../../packages/System.Xml.XmlSerializer/lib/netstandard1.3/System.Xml.XmlSerializer.dll" 
#r "../../../packages/System.Dynamic.Runtime/lib/netstandard1.3/System.Dynamic.Runtime.dll" 
#r "../../../packages/System.Xml.XDocument/lib/netstandard1.3/System.Xml.XDocument.dll" 
#r "../../../packages/System.Xml.XmlDocument/lib/netstandard1.3/System.Xml.XmlDocument.dll" 
#r "../../../packages/FsPickler/lib/netstandard2.0/FsPickler.dll" 
#r "../../../packages/System.Data.Common/lib/netstandard1.2/System.Data.Common.dll" 
#r "../../../packages/System.Linq.Expressions/lib/netstandard1.6/System.Linq.Expressions.dll" 
#r "../../../packages/System.Xml.ReaderWriter/lib/netstandard1.3/System.Xml.ReaderWriter.dll" 
#r "../../../packages/System.Collections.Concurrent/lib/netstandard1.3/System.Collections.Concurrent.dll" 
#r "../../../packages/System.Linq/lib/netstandard1.6/System.Linq.dll" 
#r "../../../packages/System.ObjectModel/lib/netstandard1.3/System.ObjectModel.dll" 
#r "../../../packages/System.Reflection.Emit/lib/netstandard1.3/System.Reflection.Emit.dll" 
#r "../../../packages/System.Reflection.Emit.Lightweight/lib/netstandard1.3/System.Reflection.Emit.Lightweight.dll" 
#r "../../../packages/System.Runtime.Numerics/lib/netstandard1.3/System.Runtime.Numerics.dll" 
#r "../../../packages/System.Runtime.Serialization.Primitives/lib/netstandard1.3/System.Runtime.Serialization.Primitives.dll" 
#r "../../../packages/System.Security.Claims/lib/netstandard1.3/System.Security.Claims.dll" 
#r "../../../packages/System.Security.Cryptography.Primitives/lib/netstandard1.3/System.Security.Cryptography.Primitives.dll" 
#r "../../../packages/System.Text.RegularExpressions/lib/netstandard1.6/System.Text.RegularExpressions.dll" 
#r "../../../packages/System.Reflection.Emit.ILGeneration/lib/netstandard1.3/System.Reflection.Emit.ILGeneration.dll" 
#r "../../../packages/System.Reactive/lib/netstandard2.0/System.Reactive.dll" 
#r "../../../packages/System.Threading/lib/netstandard1.3/System.Threading.dll" 
#r "../../../packages/System.Threading.ThreadPool/lib/netstandard1.3/System.Threading.ThreadPool.dll" 
#r "../../../packages/System.IO.FileSystem.Primitives/lib/netstandard1.3/System.IO.FileSystem.Primitives.dll" 
#r "../../../packages/System.Runtime.InteropServices.WindowsRuntime/lib/netstandard1.3/System.Runtime.InteropServices.WindowsRuntime.dll" 
#r "../../../packages/System.Security.AccessControl/lib/netstandard2.0/System.Security.AccessControl.dll" 
#r "../../../packages/System.Security.Principal/lib/netstandard1.0/System.Security.Principal.dll" 
#r "../../../packages/System.Threading.Thread/lib/netstandard1.3/System.Threading.Thread.dll" 
#r "../../../packages/Aardvark.Base.TypeProviders/lib/netstandard2.0/Aardvark.Base.TypeProviders.dll" 
#r "../../../packages/System.Memory/lib/netstandard2.0/System.Memory.dll" 
#r "../../../packages/System.Security.Cryptography.Cng/lib/netstandard2.0/System.Security.Cryptography.Cng.dll" 
#r "../../../packages/System.Security.Cryptography.OpenSsl/lib/netstandard2.0/System.Security.Cryptography.OpenSsl.dll" 
#r "../../../packages/System.Security.Principal.Windows/lib/netstandard2.0/System.Security.Principal.Windows.dll" 
#r "../../../packages/Aardvark.Base.Telemetry/lib/netstandard2.0/Aardvark.Base.Telemetry.dll" 
#r "../../../packages/CommonMark.NET/lib/netstandard1.0/CommonMark.dll" 
#r "../../../packages/FSharp.Core/lib/netstandard1.6/FSharp.Core.dll" 
#r "../../../packages/Newtonsoft.Json/lib/netstandard2.0/Newtonsoft.Json.dll" 
#r "../../../packages/System.Buffers/lib/netstandard2.0/System.Buffers.dll" 
#r "../../../packages/System.Collections.Immutable/lib/netstandard2.0/System.Collections.Immutable.dll" 
#r "../../../packages/System.Numerics.Vectors/lib/netstandard2.0/System.Numerics.Vectors.dll" 
#r "../../../packages/System.Reflection.TypeExtensions/lib/netstandard2.0/System.Reflection.TypeExtensions.dll" 
#r "../../../packages/System.Runtime.CompilerServices.Unsafe/lib/netstandard2.0/System.Runtime.CompilerServices.Unsafe.dll" 
#r "../../../packages/Unofficial.AssimpNet/lib/netstandard2.0/AssimpNet.dll" 
#r "../../../packages/Unofficial.LibTessDotNet/lib/netstandard2.0/LibTessDotNet.dll" 
#r "../../../packages/Unofficial.OpenTK/lib/netstandard2.0/OpenTK.dll" 
#r "../../../packages/Unofficial.OpenVR/lib/netstandard2.0/OpenVR.dll" 
#r "../../../packages/Unofficial.Typography/lib/netstandard2.0/Unofficial.Typography.dll" 
#r "../../../packages/System.Runtime.Serialization.Json/lib/netstandard1.3/System.Runtime.Serialization.Json.dll" 
#r "../../../packages/GLSLangSharp/lib/netstandard2.0/GLSLangSharp.dll" 
#r "../../../packages/Microsoft.Win32.Registry/lib/netstandard2.0/Microsoft.Win32.Registry.dll" 
#r "../../../packages/DevILSharp/lib/netstandard2.0/DevILSharp.dll" 
#r "../../../packages/FsPickler.Json/lib/netstandard2.0/FsPickler.Json.dll" 
#r "../../../packages/System.Threading.Tasks.Extensions/lib/netstandard2.0/System.Threading.Tasks.Extensions.dll" 
#r "../../../packages/Aardvark.Base/lib/netstandard2.0/Aardvark.Base.dll" 
#r "../../../packages/Aardvark.Base.FSharp/lib/netstandard2.0/Aardvark.Base.FSharp.dll" 
#r "../../../packages/Aardvark.Base.Essentials/lib/netstandard2.0/Aardvark.Base.Essentials.dll" 
#r "../../../packages/Aardium/lib/netstandard2.0/Aardium.dll" 
#r "../../../packages/Aardvark.Base.Incremental/lib/netstandard2.0/Aardvark.Base.Incremental.dll" 
#r "../../../packages/FShade.Imperative/lib/netstandard2.0/FShade.Imperative.dll" 
#r "../../../packages/Aardvark.Base.Runtime/lib/netstandard2.0/Aardvark.Base.Runtime.dll" 
#r "../../../packages/Aardvark.Compiler.DomainTypes/lib/netstandard2.0/Aardvark.Compiler.DomainTypes.dll" 
#r "../../../packages/FShade.Core/lib/netstandard2.0/FShade.Core.dll" 
#r "../../../packages/FShade.GLSL/lib/netstandard2.0/FShade.GLSL.dll" 
#r "../../../packages/FShade.SpirV/lib/netstandard2.0/FShade.SpirV.dll" 
#r "../../../packages/Aardvark.Base.Rendering/lib/netstandard2.0/Aardvark.Base.Rendering.dll" 
#r "../../../packages/Aardvark.Application/lib/netstandard2.0/Aardvark.Application.dll" 
#r "../../../packages/Aardvark.GPGPU/lib/netstandard2.0/Aardvark.GPGPU.dll" 
#r "../../../packages/Aardvark.Rendering.GL/lib/netstandard2.0/Aardvark.Rendering.GL.dll" 
#r "../../../packages/Aardvark.Rendering.Vulkan/lib/netstandard2.0/Aardvark.Rendering.Vulkan.dll" 
#r "../../../packages/Aardvark.SceneGraph/lib/netstandard2.0/Aardvark.SceneGraph.dll" 
#r "../../../packages/Aardvark.Application.OpenVR/lib/netstandard2.0/Aardvark.Application.OpenVR.dll" 
#r "../../../packages/Aardvark.Application.Slim/lib/netstandard2.0/Aardvark.Application.Slim.dll" 
#r "../../../packages/Aardvark.Application.Slim.GL/lib/netstandard2.0/Aardvark.Application.Slim.GL.dll" 
#r "../../../packages/Aardvark.Rendering.Text/lib/netstandard2.0/Aardvark.Rendering.Text.dll" 
#r "../../../packages/Aardvark.SceneGraph.IO/lib/netstandard2.0/Aardvark.SceneGraph.IO.dll" 
#r "../../../packages/Aardvark.Service/lib/netstandard2.0/Aardvark.Service.dll" 
#r "../../../packages/Aardvark.Application.OpenVR.GL/lib/netstandard2.0/Aardvark.Application.OpenVR.GL.dll" 
#r "../../../packages/Aardvark.Application.OpenVR.Vulkan/lib/netstandard2.0/Aardvark.Application.OpenVR.Vulkan.dll" 
#r "../../../packages/Aardvark.Application.Slim.Vulkan/lib/netstandard2.0/Aardvark.Application.Slim.Vulkan.dll" 
#r "../../../packages/Aardvark.UI/lib/netstandard2.0/Aardvark.UI.dll" 
#r "../../../packages/Aardvark.Application.Utilities/lib/netstandard2.0/Aardvark.Application.Utilities.dll" 
#r "../../../packages/Aardvark.UI.Primitives/lib/netstandard2.0/Aardvark.UI.Primitives.dll" 
#r "System" 
#r "mscorlib" 
#r "System.Numerics" 
#r "System.Core" 
#r "System.ComponentModel.Composition" 
#r "System.Windows" 
#r "System.Windows.Forms" 
#r "WindowsBase" 
#r "System.Runtime.Serialization" 
#r "System.Xml" 
#r "System.Data" 
#r "System.Xml.Linq" 
#r "netstandard" 