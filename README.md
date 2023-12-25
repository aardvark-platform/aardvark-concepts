[![Discord](https://badgen.net/discord/online-members/UyecnhM)](https://discord.gg/UyecnhM)
[![license](https://img.shields.io/github/license/aardvark-platform/template.svg)](https://github.com/aardvark-platform/template/blob/master/LICENSE)

[The Aardvark Platform](https://aardvarkians.com/) |
[Gallery](https://github.com/aardvark-platform/aardvark.docs/wiki/Gallery) | 
[Packages&Repositories](https://github.com/aardvark-platform/aardvark.docs/wiki/Packages-and-Repositories)

This repository hosts guided walkthrough examples and lecture notes for the open-source [The Aardvark Platform](https://github.com/aardvark-platform/aardvark.docs/wiki) for visual computing, real-time graphics and visualization. You can find additional academic articles, paper concepts and videos related to Aardvark in [This repository's wiki](https://github.com/aardvark-platform/aardvark-concepts/wiki). For technical documentation regarding Aardvark, please [see the aardvark.docs wiki](https://github.com/aardvark-platform/aardvark.docs/wiki).

# The Aardvark ecosystem

Aardvark Platform consists of these central resources:
* [Aardvark.Base](https://github.com/aardvark-platform/aardvark.base): Aardvark.Base provides fundamental algorithms and datastructures for math and system interop, including Vectors, Matrices, and image loaders. Technical documentation for Aardvark.Base is hosted in [the Aardvark.Base wiki](https://github.com/aardvark-platform/aardvark.base/wiki). The packages built with Aardvark.Base are listed in [Packages&Repositories](https://github.com/aardvark-platform/aardvark.docs/wiki/Packages-and-Repositories). 
* [Aardvark.Rendering](https://github.com/aardvark-platform/aardvark.rendering): Aardvark.Rendering is high-performance incremental renderer with a functional input language and a very high degree of flexibility. You can find example projects in the [Gallery](https://github.com/aardvark-platform/aardvark.docs/wiki/Gallery), and tons of demo code in [the Aardvark.Rendering Examples folder](https://github.com/aardvark-platform/aardvark.rendering/tree/master/src/Examples%20(netcore)). The examples include [HelloWorld](https://github.com/aardvark-platform/aardvark.rendering/blob/master/src/Examples%20(netcore)/00%20-%20HelloWorld/Program.fs), [n-body using compute shaders](https://github.com/aardvark-platform/aardvark.rendering/blob/master/src/Examples%20(netcore)/10%20-%20NBodyCompute/Program.fs), [RTX Ray Tracing](https://github.com/aardvark-platform/aardvark.rendering/tree/master/src/Examples%20(netcore)/36%20-%20Raytracing) and [large scale rendering with reversed depth](https://github.com/aardvark-platform/aardvark.rendering/tree/master/src/Examples%20(netcore)/37%20-%20ReversedDepth). Technical documentation related to Aardvark.Rendering is located in [the Aardvark.Rendering wiki](https://github.com/aardvark-platform/aardvark.rendering/wiki).
* [Aardvark.Media](https://github.com/aardvark-platform/aardvark.media): Aardvark.Media is a purely functional web-based user interface library and application development system using the [Elm concept](https://guide.elm-lang.org/architecture/). Aardvark.Media includes Aardvark.Rendering to provide serverside rendering capability. Many examples projects in the [Gallery](https://github.com/aardvark-platform/aardvark.docs/wiki/Gallery) use Aardvark.Media. Tons of code examples and user interface primitives are presented in [the Aardvark.Media Examples folder](https://github.com/aardvark-platform/aardvark.media/tree/main/src/Examples%20(dotnetcore)). Documentation for Aardvark.Media is hosted in [the Aardvark.Media wiki](https://github.com/aardvark-platform/aardvark.media/wiki), including [a complete guided walkthrough through Aardvark.Media application development](https://github.com/aardvark-platform/aardvark.media/wiki/Guided-Aardvark.Media-Walkthrough).

# Purpose of this repository

The code in this repository showcases the interplay of the different Aardvark Platform repositories (which can be found in the Packages&Repositories at the top of this readme). This demo repository is considered supplementary documentation for the other repositories, which are more technically oriented. In this demo repository, you can find a guided Aardvark walkthrough (also in video form in the wiki) and more academic observations about various Aardvark components. 

Aardvark is used in industry and research projects, which are also linked [in the Gallery](https://github.com/aardvark-platform/aardvark.docs/wiki/Gallery). A selection of examples: [Hilite lighting design](https://www.youtube.com/watch?v=WPgy4ZZ_i2w&t=231s), [Hilite Architectural Visualization](https://www.youtube.com/watch?v=5JGXM7jDOFM), [Pro3D](http://pro3d.space/). Topics discussing technical aspects of Aardvark can be found in [the aardvark.docs wiki](https://github.com/aardvark-platform/aardvark.docs/wiki). Our lecture notes and videos can be found in [this repository's wiki](https://github.com/aardvark-platform/aardvark-concepts/wiki). We invite you to ask further questions [in our discord server](https://discord.gg/UyecnhM).

# How to build this demo repository

You need the [.NET SDK](https://dotnet.microsoft.com/en-us/download) installed. Your editor (Visual Studio, Jetbrains Rider, VS Code) should have F# language support installed. We use dotnet+[paket](https://fsprojects.github.io/Paket/) as build system.

Clone the repository and run `build.cmd` or `./build.sh`. 

# How to create a new Aardvark Project

Choose one of these options:
* Check out [the template repository](https://github.com/aardvark-platform/template) and follow the readme.
* Install [the Aardvark Dotnet Template](https://github.com/aardvark-platform/aardvark.templates) and run `dotnet new`.
* Clone and modify an existing example, such as [Aardvark.Rendering examples](https://github.com/aardvark-platform/aardvark.rendering/tree/master/src/Examples%20(netcore)) or [Aardvark.Media examples](https://github.com/aardvark-platform/aardvark.media/tree/main/src/Examples%20(dotnetcore)).

# Using aardvark libraries in your project

We deploy our packages on [Nuget](https://www.nuget.org/packages?q=aardvark) and [Github Packages](https://github.com/orgs/aardvark-platform/packages). For dependency management, we use [paket](https://fsprojects.github.io/Paket/). A list of Aardvark packages is located at [Packages&Repositories](https://github.com/aardvark-platform/aardvark.docs/wiki/Packages-and-Repositories). 
