using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Engine
{
    public interface IBigWorldCellApp : IGrainWithGuidKey
    {
        Task Init();

        Task Destroy();
    }
}
