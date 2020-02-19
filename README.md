MineCase 
======================================
#### [![Build Status](https://travis-ci.org/dotnetGame/MineCase.svg?branch=master)](https://travis-ci.org/dotnetGame/MineCase)   [![Build status](https://ci.appveyor.com/api/projects/status/w9h243k1lqee2ke5/branch/master?svg=true)](https://ci.appveyor.com/project/sunnycase/minecase/branch/master) 
<a href="https://www.patreon.com/SooChowJunWang"><img src="https://img.shields.io/endpoint.svg?url=https://shieldsio-patreon.herokuapp.com/SooChowJunWang&style=for-the-badge" alt="Patreon donate button" /></a>

![Logo](doc/logo/MineCaseLogo.png)

#### [English](https://github.com/dotnetGame/MineCase/blob/master/README.md) | [中文](https://github.com/dotnetGame/MineCase/blob/master/README-zh.md) 

`MineCase` is a `Minecraft` server implement in dotnet core. 
The project is designed to create a high-performance, distributed `Minecraft` server with virtual actor provided by Orleans distributed framework. 
Different chunks are managed on different servers so that more players can join in and play in the same world. This makes minecraft servers more scalable.
Servers like Anarchy servers can allow more players to join in without waiting in queue by using distributed server.
It written in `C#` with `.NET Core 3.1` env and based on `orleans` framework to work with released [1.15.2 protocol](https://www.minecraft.net/en-us/article/minecraft-java-edition-1-15-2). The [website](https://wiki.vg/) describes the Minecraft protocol clearly.

**MineCase is under refactoring, so branch refactor may not work.**

**MineCase is not stable and lack of many features now. Please don't use MineCase in production unless you know what you're doing.**

![Screenshots](screenshots/1.jpg)

## Run Requirements
* [.Net Core 3.1](https://www.microsoft.com/net/download)
* [MongoDB](https://www.mongodb.com/download-center/community)

## Install (Build From Source)
* 1 . Download and install a `.NET Core sdk` from this [page](https://www.microsoft.com/net/download).
* 2 . Download and install a `MongoDB` from this [page](https://www.mongodb.com/download-center?jmp=nav#community).
* 3 . Download a `MineCase` archive from the [github page](https://github.com/dotnetGame/MineCase/archive/master.zip)  (or **clone:**)
	
	```bash
	git clone git@github.com:dotnetGame/MineCase.git
	cd MineCase
	```
* 4 . Un-zip `Minecase` archive.
* 5 . Build and run the `build_and_run`
    * **OSX** : Run the `build_and_run.sh`.
    * **Linux** : Run the `build_and_run.sh`.
    * **Win** : Double-click `build_and_run.bat`.

## How it works
None



## Contributors
<div>
<span>
<a href="https://github.com/sunnycase">
<img style="border-radius:50%;width:55px;" src="https://avatars3.githubusercontent.com/u/3644879?s=460&v=4"  alt="sunnycase" />
</a>
</span>
<span>
<a href="https://github.com/jstzwj">
<img style="border-radius:50%;width:55px;" src="https://avatars0.githubusercontent.com/u/13167278?s=460&v=4"  alt="jstzwj" />
</a>
</span>
<span>
<a href="https://github.com/akemimadoka">
<img style="border-radius:50%;width:55px;" src="https://avatars3.githubusercontent.com/u/8528322?s=460&v=4"  alt="akemimadoka" />
</a>
</span>
<span>
<a href="https://github.com/Alinshans">
<img style="border-radius:50%;width:55px;" src="https://avatars1.githubusercontent.com/u/20122802?s=460&v=4"  alt="Alinshans" />
</a>
</span>
<span>
<a href="https://github.com/ray-cast">
<img style="border-radius:50%;width:55px;" src="https://avatars1.githubusercontent.com/u/6936875?s=460&v=4"  alt="ray-cast" />
</a>
</span>
<span>
<a href="https://github.com/Melonpi">
<img style="border-radius:50%;width:55px;" src="https://avatars2.githubusercontent.com/u/17426880?s=460&v=4"  alt="Melonpi" />
</a>
</span>
<span>
<a href="https://github.com/zaoqi">
<img style="border-radius:50%;width:55px;" src="https://avatars3.githubusercontent.com/u/19170231?s=460&v=4"  alt="zaoqi" />
</a>
</span>
</div>


## Credits

**Patreon supporters list**

Wooden Pickaxe Supporter:

<div>
<a href="https://www.patreon.com/acid_chicken">
<img style="border-radius:50%;width:55px;" src="https://c10.patreonusercontent.com/3/eyJ3IjoyMDB9/patreon-media/p/campaign/1299941/279827931eba4d3ebc5cc2586a106771/2.png?token-time=2145916800&token-hash=Fv8I_E9X9rxLgrnpW9dlYd5UuLKq4CEuFtCV4zmBJMc%3D"  alt="Acid Chicken" />
</a>Acid Chicken
</div>
<div>
<a href="https://www.patreon.com/user/creators?u=4934636">
<img style="border-radius:50%;width:55px;" src="https://c10.patreonusercontent.com/3/eyJ3IjoyMDB9/patreon-media/p/user/4934636/e1e75d594c234690a68fd3383bc377c2/1.jpg?token-time=2145916800&token-hash=JXAJzPnLZ5TmPij2y0SSOYgHg8Is_QQiOr6DjH9L_LU%3D"  alt="Balajanovski" />
</a>Balajanovski
</div>

## Get Involved

We need help to make MineCase better. You can help us by fixing bugs, developing new features, improving documents.  
Some new contributors wonder what to work. The project began with the love for Minecraft, so our answer is always "do what you love". 

## Contact
This project is still under development. 
You can submit code by using `Pull Requests` or Feel free to contact me via `e-mail` or `issues`, I'll add your profile to team members
and if you have any questions we can discuss together in the [Issues](https://github.com/dotnetGame/MineCase/issues).
and also any questions you may have while using this server, or any good suggestions, can be addressed to us in [Issues](https://github.com/dotnetGame/MineCase/issues).
we welcome and thank your contribution for this project.

* Reach me via e-mail: sunnycase@live.cn
* Discord : [MineCase](https://discord.gg/8Z5RSRn)
* QQ Group: 667481568

[License (MIT)](https://raw.githubusercontent.com/dotnetGame/MineCase/master/LICENSE)
-------------------------------------------------------------------------------
	MIT License
	
	Copyright (c) 2017-2020 MineCase
	
	Permission is hereby granted, free of charge, to any person obtaining a copy
	of this software and associated documentation files (the "Software"), to deal
	in the Software without restriction, including without limitation the rights
	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
	copies of the Software, and to permit persons to whom the Software is
	furnished to do so, subject to the following conditions:
	
	The above copyright notice and this permission notice shall be included in all
	copies or substantial portions of the Software.
	
	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
	SOFTWARE.
