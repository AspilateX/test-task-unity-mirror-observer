using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Networking
{
    /// <summary>
    /// Менеджер с поддержкой кастомных клиентских и серверных хендлеров
    /// </summary>
    public class DefaultNetworkManager : NetworkManager
    {
        private IEnumerable<IClientHandler> _clientHandlers;
        private IEnumerable<IServerHandler> _serverHandlers;


        [Inject]
        private void Construct(IEnumerable<IClientHandler> clientHandlers, IEnumerable<IServerHandler> serverHandlers)
        {
            _clientHandlers = clientHandlers;
            _serverHandlers = serverHandlers;
        }

        #region Client
        public override void OnClientConnect()
        {
            base.OnClientConnect();

            foreach (var clientHandler in _clientHandlers)
            {
                try
                {
                    clientHandler.OnClientConnect();
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }

        public override void OnClientDisconnect()
        {
            foreach (var clientHandler in _clientHandlers)
            {
                try
                {
                    clientHandler.OnClientDisconnect();
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }
        #endregion

        #region Server
        public override void OnServerConnect(NetworkConnectionToClient conn)
        {
            foreach (var serverHandler in _serverHandlers)
            {
                try
                {
                    serverHandler.OnServerConnect(conn);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }

        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            foreach (var serverHandler in _serverHandlers)
            {
                try
                {
                    serverHandler.OnServerDisconnect(conn);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }
        #endregion
    }
}