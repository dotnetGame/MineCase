MineCase 
======================================
#### [![Financial Contributors on Open Collective](https://opencollective.com/MineCase/all/badge.svg?label=financial+contributors)](https://opencollective.com/MineCase) [![Build Status](https://travis-ci.org/dotnetGame/MineCase.svg?branch=master)](https://travis-ci.org/dotnetGame/MineCase)   [![Build status](https://ci.appveyor.com/api/projects/status/w9h243k1lqee2ke5/branch/master?svg=true)](https://ci.appveyor.com/project/sunnycase/minecase/branch/master) 
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
[![sunnycase](https://i.loli.net/2020/02/19/QWGu4759qeUam8c.png)](https://github.com/sunnycase)[![jstzwj](https://i.loli.net/2020/02/19/kSqmT7cFfp5Qi4L.png)](https://github.com/jstzwj)[![akemimadoka](https://i.loli.net/2020/02/19/s2GmUF7SwqzC9ER.png)](https://github.com/akemimadoka)[![Alinshans](https://i.loli.net/2020/02/19/yt9DE4LT1RkweQb.png)](https://github.com/Alinshans)[![ray-cast](https://i.loli.net/2020/02/19/r42VmKzjlpaQPCc.png)](https://github.com/ray-cast)[![Melonpi](https://i.loli.net/2020/02/19/KcW4pes71AR5bqH.png)](https://github.com/Melonpi)[![zaoqi](https://i.loli.net/2020/02/19/15ByH8UoICESudh.png)](https://github.com/zaoqi)

## Credits

**Patreon backers**

Wooden Pickaxe backers:

* [![https://www.patreon.com/acid_chicken](https://i.loli.net/2020/02/19/qoR9anvrkNLtSY8.png)](https://www.patreon.com/acid_chicken) Acid Chicken
* [![https://www.patreon.com/user/creators?u=4934636](https://i.loli.net/2020/02/19/KCRsaH4JTSIow29.png)](https://www.patreon.com/user/creators?u=4934636) Balajanovski
* [![https://www.patreon.com/Gongo/creators](https://i.loli.net/2020/02/22/gH68tvU4S2JrOPL.png)](https://www.patreon.com/Gongo/creators) Gongo

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

## Contributors

### Code Contributors

This project exists thanks to all the people who contribute. [[Contribute](CONTRIBUTING.md)].
<a href="https://github.com/dotnetGame/MineCase/graphs/contributors"><img src="https://opencollective.com/MineCase/contributors.svg?width=890&button=false" /></a>

### Financial Contributors

Become a financial contributor and help us sustain our community. [[Contribute](https://opencollective.com/MineCase/contribute)]

#### Individuals

<a href="https://opencollective.com/MineCase"><img src="https://opencollective.com/MineCase/individuals.svg?width=890"></a>

#### Organizations

Support this project with your organization. Your logo will show up here with a link to your website. [[Contribute](https://opencollective.com/MineCase/contribute)]

<a href="https://opencollective.com/MineCase/organization/0/website"><img src="https://opencollective.com/MineCase/organization/0/avatar.svg"></a>
<a href="https://opencollective.com/MineCase/organization/1/website"><img src="https://opencollective.com/MineCase/organization/1/avatar.svg"></a>
<a href="https://opencollective.com/MineCase/organization/2/website"><img src="https://opencollective.com/MineCase/organization/2/avatar.svg"></a>
<a href="https://opencollective.com/MineCase/organization/3/website"><img src="https://opencollective.com/MineCase/organization/3/avatar.svg"></a>
<a href="https://opencollective.com/MineCase/organization/4/website"><img src="https://opencollective.com/MineCase/organization/4/avatar.svg"></a>
<a href="https://opencollective.com/MineCase/organization/5/website"><img src="https://opencollective.com/MineCase/organization/5/avatar.svg"></a>
<a href="https://opencollective.com/MineCase/organization/6/website"><img src="https://opencollective.com/MineCase/organization/6/avatar.svg"></a>
<a href="https://opencollective.com/MineCase/organization/7/website"><img src="https://opencollective.com/MineCase/organization/7/avatar.svg"></a>
<a href="https://opencollective.com/MineCase/organization/8/website"><img src="https://opencollective.com/MineCase/organization/8/avatar.svg"></a>
<a href="https://opencollective.com/MineCase/organization/9/website"><img src="https://opencollective.com/MineCase/organization/9/avatar.svg"></a>
