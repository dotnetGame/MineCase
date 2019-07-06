# Developer Guide

## Introduction



## Install (Build From Source)

- 1 . Download and install a `.NET Core sdk` from this [page](https://www.microsoft.com/net/download).

- 2 . Download and install a `MongoDB` from this [page](https://www.mongodb.com/download-center?jmp=nav#community).

- 3 . Download a `MineCase` archive from the [github page](https://github.com/dotnetGame/MineCase/archive/master.zip)  (or **clone:**)

  ```bash
  git clone git@github.com:dotnetGame/MineCase.git
  cd MineCase
  ```

- 4 . Un-zip `Minecase` archive.

- 5 . Build and run the `build_and_run`

  - **OSX** : Run the `build_and_run.sh`.
  - **Linux** : Run the `build_and_run.sh`.
  - **Win** : Double-click `build_and_run.bat`.

## Directory Introduction

* build - store the compiled binary and the released binary
* client - an implementation of the Unity-based Minecraft game client
* common - store some code common to both the client and the server
* data - data files, such as Minecraft synthetic formulas, etc.
* docker - server docker file
* private - not in use
* server - store the server code
* tests - store unit tests for the above code

## Project Introduction

#### Client folder

* MineCase.Client - store the unity client project file
* MineCase.Client.Engine - stores the game core structure, similar to the wpf attribute system
* MineCase.Client.Scripts - store game logic

#### Common folder

* MineCase.Algorithm - Stores logic code common to MineCase servers and clients, such as noise production, low generation, bio AI
* MineCase.Core - Stores MineCase core concepts and data structures such as Block, Chunk, etc.
* MineCase.Nbt - Minecraft NBT structure analysis
* MineCase.Protocol - Minecraft Network Protocol
* MineCase.Serialization - Serialization of some structures of MineCase

#### Server Folder

* MineCase.Gateway - MineCase's Gateway program, equivalent to the Client in Orleans, is used to communicate with the player's game and forward the information to the logic in the server to respond.
* MineCase.Server - MineCase's Server program, equivalent to Silo in Orleans, is responsible for the processing of game logic.
* MineCase.Server.Engine - The core ECS engine of MineCase, combined with WPF property system and ECS architecture.
* MineCase.Server.Grains - stores the Grain in Orleans, which is the Virtual Actor
* MineCase.Server.Interfaces - the interface to store the Virtual Actor



## Possible list of items

* Improve terrain generation and achieve The End and Nether
* Implement the furnace
* Implement chat
* Implement command parsing
* Implement lighting calculations
* Implement physics
* Realize Redstone
* Implement biological AI



#### Possible Project Guide