# 开发者指南

## 简介


## 安装

### 从源码安装
* 1 . 下载并安装 [.NET Core sdk 2.0](https://www.microsoft.com/net/download)。
* 2 . 下载并安装 [MongoDB](https://www.mongodb.com/download-center?jmp=nav#community)。
* 3 . 从 [github page](https://github.com/dotnetGame/MineCase/archive/master.zip) 下载`MineCase`(或者使用 **clone:** 指令)。
	```bash
	git clone git@github.com:dotnetGame/MineCase.git
	cd MineCase
	```
* 4 . 解压 `Minecase` 压缩包.
* 5 . 构建并运行 `build_and_run`
    * **OSX** : 运行 `build_and_run.sh`.
    * **Linux** : 运行 `build_and_run.sh`.
    * **Win** : 双击 `build_and_run.bat`.

### 使用二进制安装
* 1 . 暂不提供


## 文件夹介绍

* build - 存放编译后的二进制文件和发布后的二进制文件

* client - 一个基于Unity的Minecraft游戏客户端的实现
* common - 存放一些客户端和服务器公用的代码
* data - 数据文件，例如Minecraft合成配方等
* docker - 服务器docker文件
* private - 暂不使用
* server - 存放服务器代码
* tests - 存放对上述代码的单元测试

## 项目介绍

#### Client文件夹

* MineCase.Client - 存放unity项目文件
* MineCase.Client.Engine - 存放游戏核心结构，类似wpf属性系统
* MineCase.Client.Scripts - 存放游戏逻辑

#### Common文件夹

* MineCase.Algorithm - 存放MineCase服务器和客户端公用的逻辑代码，例如噪声生产、低下生成、生物AI
* MineCase.Core - 存放MineCase核心概念和数据结构，例如Block、Chunk等
* MineCase.Nbt - Minecraft NBT结构解析
* MineCase.Protocol - Minecraft 网络协议
* MineCase.Serialization - MineCase一些结构的序列化

#### Server文件夹

* MineCase.Gateway - MineCase的Gateway程序，相当于Orleans中的Client，用于和玩家的游戏通信，并转发信息到Server中的逻辑进行响应。
* MineCase.Server - MineCase的Server程序，相当于Orleans中的Silo，负责游戏逻辑的处理。
* MineCase.Server.Engine - MineCase的核心结构，结合WPF属性系统和ECS架构。
* MineCase.Server.Grains - 存放Orleans中的Grain，也就是Virtual Actor
* MineCase.Server.Interfaces - 存放Virtual Actor的接口



## 可能的项目列表

* 完善地形生成，实现The End和Nether
* 实现熔炉
* 实现聊天
* 实现命令解析
* 实现灯光计算
采用洪水填充算法，为chunk中每个block赋予skylight和blocklight值
* 实现物理
* 实现红石
* 实现生物AI



#### 可能的项目指南