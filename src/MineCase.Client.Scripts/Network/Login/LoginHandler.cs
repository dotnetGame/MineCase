using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol.Login;
using UnityEngine;

namespace MineCase.Client.Network.Login
{
    public interface ILoginHandler
    {
        void OnDisconnect(Guid sessionId, LoginDisconnect disconnect);

        void OnLoginSuccess(Guid sessionId, LoginSuccess loginSuccess);
    }

    internal class LoginHandler : ILoginHandler
    {
        public void OnLoginSuccess(Guid sessionId, LoginSuccess loginSuccess)
        {
            Debug.Log("Login Success: " + loginSuccess.Username + ", " + loginSuccess.UUID);
        }

        void ILoginHandler.OnDisconnect(Guid sessionId, LoginDisconnect disconnect)
        {
            Debug.LogWarning("Disconnected: " + disconnect.Reason);
        }
    }
}
