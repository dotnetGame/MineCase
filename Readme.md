# MineCase [![Build Status](https://travis-ci.org/dotnetGame/MineCase.svg?branch=master)](https://travis-ci.org/dotnetGame/MineCase) [![Build status](https://ci.appveyor.com/api/projects/status/w9h243k1lqee2ke5/branch/master?svg=true)](https://ci.appveyor.com/project/sunnycase/minecase/branch/master)

![Screenshots](screenshots/1.jpg)

[English](https://github.com/dotnetGame/MineCase/blob/master/Readme.md) | [中文文档](https://github.com/dotnetGame/MineCase/blob/master/Readme-zh.md)

## Introduction

`MineCase` is a cross-platform application with distributed server of `Minecraft`. 

It's written in `.NET Core` and based on the [orleans](https://github.com/dotnet/orleans) framework. It constructs a high-performance, distributed system by isolating different components through actor mode.

We currently support [Release 1.12](https://minecraft.net/en-us/article/minecraft-112-pre-release-6) Minecraft protocol version.

## Setup

### From docker(Only support Linux)

#### Install docker

You can install docker according to [this guide](https://docs.docker.com/engine/installation/).

#### Run the application

```bash
curl -o docker-compose.yml https://raw.githubusercontent.com/dotnetGame/MineCase/master/build/docker/linux/docker-compose.yml
docker-compose up
```
You can use `docker-compose stop` to stop the application。

### From source

#### Pre-requirement

* You need to install **`.NET Core` sdk 2.0** first, you can find the version you need from [here](https://www.microsoft.com/net/download).
* **MongoDB**, you can download it from [here](https://www.mongodb.com/download-center?jmp=nav#community).

#### Get source

```bash
git clone git@github.com:dotnetGame/MineCase.git
cd MineCase
```
or [Download ZIP](https://github.com/dotnetGame/MineCase/archive/master.zip) and entry the `MineCase` directory.

#### Build and run

* Windows

Double click the `build_and_run.bat`.

* Linux or Mac

Run the `build_and_run.sh`.

## Contributing

This project is still under development. We welcome and thank you for your contribution to this project.

If you are a developer, we welcome you to fork this project and submit a `Pull Request` with your changes, and if you have any questions we can discuss together in the [Issues](https://github.com/dotnetGame/MineCase/issues).

If you are a user, any questions you may have while using this server, or any good suggestions, can be addressed to us in [Issues](https://github.com/dotnetGame/MineCase/issues).
