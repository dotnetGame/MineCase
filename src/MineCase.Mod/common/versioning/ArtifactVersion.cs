using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Mod.common.versioning
{
    public interface ArtifactVersion : IComparable<ArtifactVersion>
    {
        string GetLabel();

        string GetVersionString();

        bool ContainsVersion(ArtifactVersion source);

        string GetRangeString();
    }
}
