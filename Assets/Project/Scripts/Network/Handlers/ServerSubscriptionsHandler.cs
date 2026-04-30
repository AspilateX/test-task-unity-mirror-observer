using Mirror;
using System;
using UnityEngine;

namespace Networking
{
    /// <summary>
    /// Хендлер обработки запросов подписки и отписки
    /// </summary>
    public class ServerSubscriptionsHandler : IServerHandler, IDisposable
    {
        public const string HelloMessageText = "Hello Client!";
        private ServerSubscriptionsService _subscriptionsService;

        public ServerSubscriptionsHandler(ServerSubscriptionsService subscriptionsService)
        {
            _subscriptionsService = subscriptionsService;

            NetworkServer.RegisterHandler<SubscribeRequest>(OnSubscribeMessage);
            NetworkServer.RegisterHandler<UnsubscribeRequest>(OnUnsubscribeMessage);
        }

        public void Dispose()
        {
            NetworkServer.UnregisterHandler<SubscribeRequest>();
            NetworkServer.UnregisterHandler<UnsubscribeRequest>();
        }

        public void OnServerConnect(NetworkConnectionToClient conn)
        { }

        public void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            _subscriptionsService.RemoveListener(conn);
        }

        private void OnSubscribeMessage(NetworkConnectionToClient conn, SubscribeRequest msg)
        {
            Type type = Type.GetType(msg.TypeName);

            if (type == null)
            {
                Debug.LogError($"Failed to subscribe client {conn.connectionId} to type {msg.TypeName}. Type not found.");
                return;
            }

            _subscriptionsService.Subscribe(type, conn);
            _subscriptionsService.Send(new HelloMessage()
            {
                Text = HelloMessageText
            });
        }

        private void OnUnsubscribeMessage(NetworkConnectionToClient conn, UnsubscribeRequest msg)
        {
            Type type = Type.GetType(msg.TypeName);

            if (type == null)
            {
                Debug.LogError($"Failed to unsubscribe client {conn.connectionId} from type {msg.TypeName}. Type not found.");
                return;
            }

            _subscriptionsService.Unsubscribe(type, conn);
        }
    }
}