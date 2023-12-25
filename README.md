[![Discord](https://badgen.net/discord/online-members/UyecnhM)](https://discord.gg/UyecnhM)
[![license](https://img.shields.io/github/license/aardvark-platform/template.svg)](https://github.com/aardvark-platform/template/blob/master/LICENSE)

[The Aardvark Platform](https://aardvarkians.com/) |
[Gallery](https://github.com/aardvark-platform/aardvark.docs/wiki/Gallery) | 
[Packages&Repositories](https://github.com/aardvark-platform/aardvark.docs/wiki/Packages-and-Repositories)

This repository hosts guided walkthrough examples and lecture notes for the open-source [The Aardvark Platform](https://github.com/aardvark-platform/aardvark.docs/wiki) for visual computing, real-time graphics and visualization. You can find additional scientific articles, paper concepts and videos related to Aardvark in [This repository's wiki](https://github.com/aardvark-platform/aardvark-concepts/wiki). For technical documentation regarding Aardvark, please [see the aardvark.docs wiki](https://github.com/aardvark-platform/aardvark.docs/wiki).

# The Aardvark ecosystem

Aardvark Platform consists of these central resources:
* [Aardvark.Base](https://github.com/aardvark-platform/aardvark.base): Aardvark.Base provides fundamental algorithms and datastructures for math and system interop, including Vectors, Matrices, and image loaders. Technical documentation for Aardvark.Base is hosted in [the Aardvark.Base wiki](https://github.com/aardvark-platform/aardvark.base/wiki). The packages built with Aardvark.Base are listed in [Packages&Repositories](https://github.com/aardvark-platform/aardvark.docs/wiki/Packages-and-Repositories). 
* [Aardvark.Rendering](https://github.com/aardvark-platform/aardvark.rendering): Aardvark.Rendering is high-performance incremental renderer with a functional input language and a very high degree of flexibility. You can find example projects in the [Gallery](https://github.com/aardvark-platform/aardvark.docs/wiki/Gallery), and tons of demo code in [the Aardvark.Rendering Examples folder](https://github.com/aardvark-platform/aardvark.rendering/tree/master/src/Examples%20(netcore)). The examples include [HelloWorld](https://github.com/aardvark-platform/aardvark.rendering/blob/master/src/Examples%20(netcore)/00%20-%20HelloWorld/Program.fs), [n-body using compute shaders](https://github.com/aardvark-platform/aardvark.rendering/blob/master/src/Examples%20(netcore)/10%20-%20NBodyCompute/Program.fs), [RTX Ray Tracing](https://github.com/aardvark-platform/aardvark.rendering/tree/master/src/Examples%20(netcore)/36%20-%20Raytracing) and [large scale rendering with reversed depth](https://github.com/aardvark-platform/aardvark.rendering/tree/master/src/Examples%20(netcore)/37%20-%20ReversedDepth). Technical documentation related to Aardvark.Rendering is located in [the Aardvark.Rendering wiki](https://github.com/aardvark-platform/aardvark.rendering/wiki).

# Purpose of this repository

The code in this repository showcases the interplay of the different Aardvark Platform repositories (which can be found in the Packages&Repositories at the top of this readme). This demo repository is considered supplementary documentation for the other repositories, which are more technically oriented. In this demo repository, you can find a guided Aardvark walkthrough (also in video form in the wiki) and more academic observations about various Aardvark components. 

Aardvark is used in industry and research projects, which are also linked [in the Gallery](https://github.com/aardvark-platform/aardvark.docs/wiki/Gallery). A selection of examples: [Hilite lighting design](https://www.youtube.com/watch?v=WPgy4ZZ_i2w&t=231s), [Hilite Architectural Visualization](https://www.youtube.com/watch?v=5JGXM7jDOFM), [Pro3D](http://pro3d.space/). Topics concerning development environment, tooling, articles and papers can be found in [the aardvark.docs wiki](https://github.com/aardvark-platform/aardvark.docs/wiki). Our own lecture notes and videos can be found in [this repository's wiki](https://github.com/aardvark-platform/aardvark-concepts/wiki). We invite you to ask further questions [in our discord server](https://discord.gg/UyecnhM).

# Detailed Walkthrough

 - [aardvark.media](https://github.com/aardvark-platform/aardvark.media) for purely functional high performance user interfaces and interactions.

   Additionaly, there are examples on some important topics, in the [demo folder](https://github.com/aardvark-platform/aardvark.base/tree/master/src/Demo), e.g.: ----- media walkthrough
   
 - [Aardvark.Media](https://github.com/aardvark-platform/aardvark.media) contains [lots of examples](https://github.com/aardvark-platform/aardvark.media/tree/master/src/Examples%20(dotnetcore)) as well. Documentation and a guided walkthrough is [in the Aardvark.Media wiki](https://github.com/aardvark-platform/aardvark.media/wiki).

Topics concerning development environment, tooling, articles and papers can be found in [the aardvark.docs wiki](https://github.com/aardvark-platform/aardvark.docs/wiki). Our own lecture notes and videos can be found in [this repository's wiki](https://github.com/aardvark-platform/aardvark-concepts/wiki). Ask further questions in our discord server, linked at the top of this readme.

Aardvark.Rendering is used in industry and research projects, e.g. [Hilite](). Videos showcasing aardvark functionality can be found here:
 - [Hilite lighting design](https://www.youtube.com/watch?v=WPgy4ZZ_i2w&t=231s), [Hilite Architectural Visualization](https://www.youtube.com/watch?v=5JGXM7jDOFM)
 - [Pro3D](http://pro3d.space/)
 - [Aardvark programming showcase](https://www.youtube.com/watch?v=QjVRJworUOw)

## How to build this demo repository

Setup your system according to general aardvark development recommendations, e.g. for [windows](https://github.com/aardvark-platform/aardvark.docs/wiki/Visual-Studio) and for
[linux](https://github.com/aardvark-platform/aardvark.docs/wiki/Linux-Support).

The setup we are using in all repositories is a F# + [paket](https://fsprojects.github.io/Paket/) standard setup with additional scripts for restoring
all packages and performing a build from the command line either using `build.cmd` or `./build.sh` (they internally perform `.paket/paket.exe` restore and `msbuild src/*.sln` using [fake](https://fake.build/) and provide special code for packing native dependencies and pushing packages to nuget).

Of course you can use [vscode](https://code.visualstudio.com/). Here we recomment a setup using [ionide](http://ionide.io/).

## Quickstart for using aardvark platform libraries

We provide a [template repository](https://github.com/aardvark-platform/template). It provides a script which creates a repository with ready to go aardvark dependencies. The setup can be seen [here](https://www.youtube.com/watch?v=OEvQH0Yy1iM).

## Using aardvark libraries in your project

We use a standard deployment mechanism via nuget packages maintained by the aardvark open source community.
The open source aardvark libraries can be [queried on nuget](https://www.nuget.org/packages?q=aardvark). Of course you can use nuget or visual studio to download the packages, though we recommend using [paket](https://fsprojects.github.io/Paket/).
