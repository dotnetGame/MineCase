MineCase 
=========================================
#### [English](https://github.com/dotnetGame/MineCase/blob/master/README.md) | [中文文档](https://github.com/dotnetGame/MineCase/blob/master/README-zh.md) 
#### [![Build Status](https://travis-ci.org/dotnetGame/MineCase.svg?branch=master)](https://travis-ci.org/dotnetGame/MineCase)   [![Build status](https://ci.appveyor.com/api/projects/status/w9h243k1lqee2ke5/branch/master?svg=true)](https://ci.appveyor.com/project/sunnycase/minecase/branch/master)
 
`MineCase` is a cross-platform application with distributed server of `Minecraft`. 
The project is designed to create a high-performance, distributed system by using isolating different components through actor mode. 
Different chunks are managed by different servers so that all players can play in a world. This brings minecraft servers more scalability.
It written in `C#` with `.NET Core 2.0` env and based on `orleans` framework to work with released [1.12 protocol](https://minecraft.net/en-us/article/minecraft-112-pre-release-6).

![Screenshots](screenshots/1.jpg)

## Requirements
* [Docker for linux](https://docs.docker.com/engine/installation/) (Only support Linux)
* [.Net Core 2.0](https://www.microsoft.com/net/download)
* Unity 2017.2.0 or later versions.

## Install (Docker)
* 1 . Download a `Docker for linux` from this [page](https://docs.docker.com/engine/installation/).
* 2 . Un-zip `Docker for linux` and run the `server` on Linux

	```bash
	url=https://raw.githubusercontent.com/dotnetGame/MineCase/master/build/docker/linux/docker-compose.yml
	curl -o docker-compose.yml $url
	docker-compose up
	```
	##### Tips:
	* You can stop the server by enter `docker-compose stop`.

## Install (Github)
* 1 . Download and install a `.NET Core sdk 2.0` from this [page](https://www.microsoft.com/net/download).
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

## Contact
　　This project is still under development. 
You can submit code by using `Pull Requests` or Feel free to contact me via `e-mail` or `issues`, I'll add your profile to team members
and if you have any questions we can discuss together in the [Issues](https://github.com/dotnetGame/MineCase/issues).
and also any questions you may have while using this server, or any good suggestions, can be addressed to us in [Issues](https://github.com/dotnetGame/MineCase/issues).
we welcome and thank your contribution for this project.

* Reach me via e-mail: sunnycase@live.cn

[License (MIT)](https://raw.githubusercontent.com/dotnetGame/MineCase/master/LICENSE)
-------------------------------------------------------------------------------
	MIT License

	Copyright (c) 2017 MineCase

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
