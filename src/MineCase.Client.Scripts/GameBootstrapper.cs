using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Client.Game.Blocks;
using MineCase.Engine;
using UnityEngine;

namespace MineCase.Client
{
    public class GameBootstrapper : SmartBehaviour
    {
        public IBlockTextureLoader BlockTextureLoader { get; set; }

        public Material CubeMaterial;

        private void Start()
        {
            BlockTextureLoader.Initialize(CubeMaterial);

            FindObjectOfType<ServerManager>().ConnectServer(0, false);
        }

        private void OnGUI()
        {
            GUI.DrawTexture(new Rect(0, 0, 1024, 16), BlockTextureLoader.Texture);
        }
    }
}
