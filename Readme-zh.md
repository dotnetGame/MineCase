# MineCase [![Build Status](https://travis-ci.org/dotnetGame/MineCase.svg?branch=master)](https://travis-ci.org/dotnetGame/MineCase) [![Build status](https://ci.appveyor.com/api/projects/status/w9h243k1lqee2ke5/branch/master?svg=true)](https://ci.appveyor.com/project/sunnycase/minecase/branch/master)

![Screenshots](screenshots/1.jpg)

[English](https://github.com/dotnetGame/MineCase/blob/master/Readme.md) | [中文文档](https://github.com/dotnetGame/MineCase/blob/master/Readme-zh.md)

## 介绍

`MineCase` 是一个跨平台、分布式的 `Minecraft` 服务端应用。

本项目使用 `.NET Core` 编写，基于 [orleans](https://github.com/dotnet/orleans) 框架。它通过 Actor 模型将各个模块分离开来，从而构建一个高效的分布式系统。

目前仅支持 `Minecraft` [1.12](https://minecraft.net/en-us/article/minecraft-112-pre-release-6) 的版本。

## 安装

### 从 docker 中运行（仅支持 Linux 环境）

#### 安装 docker

可以根据 [这个指南](https://yeasy.gitbooks.io/docker_practice/content/install/) 来安装好 docker。

#### 从 docker 中运行应用

```bash
curl -o docker-compose.yml https://raw.githubusercontent.com/dotnetGame/MineCase/master/build/docker/linux/docker-compose.yml
docker-compose up
```
你可以使用 `docker-compose stop` 来终止应用。

### 构建源码运行

#### 前置要求

* 你需要先安装 **`.NET Core` sdk 2.0**，可以在 [这里](https://www.microsoft.com/net/download) 找到你需要的版本。
* **MongoDB**, 你可以按照 [这个文档](https://github.com/mongodb/mongo/blob/master/docs/building.md) 来安装。

#### 获取源码

```bash
git clone git@github.com:dotnetGame/MineCase.git
cd MineCase
```
或者 [下载ZIP](https://github.com/dotnetGame/MineCase/archive/master.zip) 并进入 `MineCase` 目录。

#### 构建并运行

* Windows

双击 `build_and_run.bat`。

* Linux 和 Mac

运行 `build_and_run.sh`。

## 贡献

本项目仍在开发阶段，我们非常欢迎并感谢你为这个项目来做贡献。

如果你是开发者，我们欢迎你 Fork 这个项目并提交你的修改，有什么问题我们可以在 [Issues](https://github.com/dotnetGame/MineCase/issues) 中一起讨论。

如果你是用户，在使用这个服务器的过程中遇到的任何问题，或者有什么好的建议，都可以在 [Issues](https://github.com/dotnetGame/MineCase/issues) 中向我们提出。

