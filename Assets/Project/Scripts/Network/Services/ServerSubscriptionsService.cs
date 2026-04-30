using Mirror;
using System;
using System.Collections.Generic;

namespace Networking
{
    /// <summary>
    /// Сервис подписки на серверные сообщения
    /// </summary>
    public class ServerSubscriptionsService
    {
        private readonly Dictionary<Type, HashSet<NetworkConnectionToClient>> _messageListeners = new();

        public void Subscribe(Type type, NetworkConnectionToClient conn)
        {
            if (_messageListeners.TryGetValue(type, out var listeners))
            {
                if (!listeners.Contains(conn))
                    listeners.Add(conn);
            }
            else
            {
                _messageListeners.Add(type, new HashSet<NetworkConnectionToClient>() { conn });
            }
        }

        public void Unsubscribe(Type type, NetworkConnectionToClient conn)
        {
            if (_messageListeners.ContainsKey(type))
                _messageListeners[type].Remove(conn);
        }

        public void RemoveListener(NetworkConnectionToClient conn)
        {
            foreach (var listeners in _messageListeners.Values)
                listeners.Remove(conn);
        }

        public void Send<T>(T message) where T : struct, NetworkMessage
        {
            var type = typeof(T);

            if (_messageListeners.ContainsKey(type))
                foreach (var conn in _messageListeners[type])
                    conn.Send(message);
        }
    }
}