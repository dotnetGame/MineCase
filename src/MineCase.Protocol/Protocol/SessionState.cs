using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Protocol.Protocol
{
    public enum SessionState
    {
        /// <summary>
        /// Handshake state
        /// </summary>
        Handshake = -1,

        /// <summary>
        /// Play state
        /// </summary>
        Play = 0,

        /// <summary>
        /// Status state
        /// </summary>
        Status = 1,

        /// <summary>
        /// Login state
        /// </summary>
        Login = 2,
    }

}
