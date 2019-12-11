# 用户指南

## 简介
MineCase的部署方式和普通服务器一样方便。MineCase的程序主要分为Gateway和Server，Gateway负责和用户的游戏客户端通信并将消息转发到Server，Server负责处理游戏的逻辑。Gateway和Server的数量并不受限制，你可以横向拓展，无限添加Gateway和Server以提高容纳用户的能力。

## 安装

#### 从源码安装
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

#### 使用二进制安装
* 1 . 暂不提供


#### 使用docker安装
* 1 . 已被移除


## 升级

* 目前暂不提供升级方案

## 使用方法



## 配置



## FAQ