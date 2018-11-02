[![Join the chat at https://gitter.im/aardvark-platform/Lobby](https://img.shields.io/badge/gitter-join%20chat-blue.svg)](https://gitter.im/aardvark-platform/Lobby)
[![license](https://img.shields.io/github/license/aardvark-platform/template.svg)](https://github.com/aardvark-platform/template/blob/master/LICENSE)

[Wiki](https://github.com/aardvarkplatform/aardvark.docs/wiki) | 
[Gallery](https://github.com/aardvarkplatform/aardvark.docs/wiki/Gallery) | 
[Quickstart](https://github.com/aardvarkplatform/aardvark.docs/wiki/Quickstart-Windows) | 
[Status](https://github.com/aardvarkplatform/aardvark.docs/wiki/Status)

This repository is part of the open-source [Aardvark platform](https://github.com/aardvark-platform/aardvark.docs/wiki) for visual computing, real-time graphics and visualization and contains some simple demos using different parts of the aardvark platform and shows the interplay of the the platform repositories:
 - [aardvark.renering](https://github.com/aardvark-platform/aardvark.rendering) for efficient rendering.
 - [aardvark.media](https://github.com/aardvark-platform/aardvark.media) for purely functional high performance user interfaces and interactions.


This demo repository should be seen additionally to the more specific examples hosted in the respective repositories such as:
 - [Aardvark.Rendering](https://github.com/aardvark-platform/aardvark.rendering) provides most platform-independent examples for [.net core](https://github.com/aardvark-platform/aardvark.rendering/tree/master/src/Scratch%20(netcore)) but also some examples
   running in [.net framework](https://github.com/aardvark-platform/aardvark.rendering/tree/master/src/Scratch%20(net471)).
 - [Aardvark.Base](https://github.com/aardvark-platform/aardvark.base)'s documentation is scattered in the rest of the examples but cheatsheets for some important topics can be found in the [wiki](https://github.com/aardvark-platform/aardvark.docs/wiki) e.g.:
    * [Vectores and Matrices](https://github.com/aardvark-platform/aardvark.docs/wiki/Vectors-and-Matrices)
    * [Transformations](https://github.com/aardvark-platform/aardvark.docs/wiki/Transformations)
    * [Images](https://github.com/aardvark-platform/aardvark.docs/wiki/Images)
    * [Colors](https://github.com/aardvark-platform/aardvark.docs/wiki/Colors-and-Color-Spaces)
   Additionaly, there are examples on some important topics, in the [demo folder](https://github.com/aardvark-platform/aardvark.base/tree/master/src/Demo), e.g.:
    * [Incremental primitives for C#](https://github.com/aardvark-platform/aardvark.base/blob/master/src/Demo/IncrementalDemo.CSharp/Program.cs)
    * [Working with PixImages](https://github.com/aardvark-platform/aardvark.base/blob/master/src/Demo/PixImageDemo/Program.cs)
 - [Aardvark.Media](https://github.com/aardvark-platform/aardvark.media) contains [lots of examples](https://github.com/aardvark-platform/aardvark.media/tree/master/src/Examples%20(dotnetcore)) as well.

Topics concerning development environment, tooling, articles and papers can be found in the [wiki](https://github.com/aardvark-platform/aardvark.docs/wiki) as well.


## How to build

Setup your system according to general aardvark development recommendations, e.g. for [windows](https://github.com/aardvark-platform/aardvark.docs/wiki/Visual-Studio) and for
[linux](https://github.com/aardvark-platform/aardvark.docs/wiki/Linux-Support).
The setup we are using in all repositories is a F# + [paket](https://fsprojects.github.io/Paket/) standard setup with additional scripts for restoring
all packages and performing a build from the command line either using `build.cmd` or `./build.sh` (they internally perform `.paket/paket.exe` restore and `msbuild src/*.sln` using [fake](https://fake.build/) and provide special code for packing native dependencies and pushing packages to nuget).
Of course you can use [vscode](https://code.visualstudio.com/). Here we recomment a setup using [ionide](http://ionide.io/).