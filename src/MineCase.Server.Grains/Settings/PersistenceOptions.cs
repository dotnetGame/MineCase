using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace MineCase.Server.Settings
{
    public sealed class PersistenceOptions : IOptions<PersistenceOptions>
    {
        public string ConnectionString { get; set; }

        PersistenceOptions IOptions<PersistenceOptions>.Value => this;
    }
}
