[![Discord](https://badgen.net/discord/online-members/UyecnhM)](https://discord.gg/UyecnhM)
[![license](https://img.shields.io/github/license/aardvark-platform/template.svg)](https://github.com/aardvark-platform/template/blob/master/LICENSE)

[The Aardvark Platform](https://aardvarkians.com/) |
[Gallery](https://github.com/aardvark-platform/aardvark.docs/wiki/Gallery) | 
[Packages&Repositories](https://github.com/aardvark-platform/aardvark.docs/wiki/Packages-and-Repositories)

This repository hosts documentation for the open-source Aardvark platform for visual computing, real-time graphics and visualization. [This repository's wiki](https://github.com/aardvark-platform/aardvark-concepts/wiki) hosts lecture notes, paper concepts and videos related to Aardvark. For technical documentation regarding Aardvark, please [see the aardvark.docs wiki](https://github.com/aardvark-platform/aardvark.docs/wiki).

# Detailed Walkthrough

This repository contains some simple demos using different parts of the aardvark platform and shows the interplay of the the platform repositories:
 - [aardvark.rendering](https://github.com/aardvark-platform/aardvark.rendering) for efficient rendering.
 - [aardvark.media](https://github.com/aardvark-platform/aardvark.media) for purely functional high performance user interfaces and interactions.

This demo repository should be seen additionally to the more specific examples hosted in the respective repositories such as:
 - [Aardvark.Rendering](https://github.com/aardvark-platform/aardvark.rendering) provides a huge set of nice and platform-independent examples for [.net core](https://github.com/aardvark-platform/aardvark.rendering/tree/master/src/Examples%20(netcore)) ranging from [HelloWorld](https://github.com/aardvark-platform/aardvark.rendering/blob/master/src/Examples%20(netcore)/00%20-%20HelloWorld/Program.fs) to more complex examples such as [n-body using compute](https://github.com/aardvark-platform/aardvark.rendering/blob/master/src/Examples%20(netcore)/10%20-%20NBodyCompute/Program.fs).
 - [Aardvark.Base](https://github.com/aardvark-platform/aardvark.base)'s documentation is scattered in the rest of the examples but cheatsheets for some important topics can be found in the [wiki](https://github.com/aardvark-platform/aardvark.docs/wiki) e.g.:
    * [Vectores and Matrices](https://github.com/aardvark-platform/aardvark.docs/wiki/Vectors-and-Matrices)
    * [Transformations](https://github.com/aardvark-platform/aardvark.docs/wiki/Transformations)
    * [Images](https://github.com/aardvark-platform/aardvark.docs/wiki/Images)
   
   Additionaly, there are examples on some important topics, in the [demo folder](https://github.com/aardvark-platform/aardvark.base/tree/master/src/Demo), e.g.:
    * [Incremental primitives for C#](https://github.com/aardvark-platform/aardvark.base/blob/master/src/Demo/IncrementalDemo.CSharp/Program.cs)
    * [Working with PixImages](https://github.com/aardvark-platform/aardvark.base/blob/master/src/Demo/PixImageDemo/Program.cs)
 - [Aardvark.Media](https://github.com/aardvark-platform/aardvark.media) contains [lots of examples](https://github.com/aardvark-platform/aardvark.media/tree/master/src/Examples%20(dotnetcore)) as well.

Topics concerning development environment, tooling, articles and papers can be found in the [wiki](https://github.com/aardvark-platform/aardvark.docs/wiki) as well. Additionally to [gitter](https://gitter.im/aardvark-platform/Lobby) chatrooms, we have a [QA-platform: http://ask.aardvark.graphics](http://ask.aardvark.graphics/).

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
