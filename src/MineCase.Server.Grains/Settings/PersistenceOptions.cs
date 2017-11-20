using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Settings
{
    public sealed class PersistenceOptions : IOptions<PersistenceOptions>
    {
        public string ConnectionString { get; set; }

        PersistenceOptions IOptions<PersistenceOptions>.Value => this;
    }
}
