using Mirror;
using UnityEngine;

namespace Networking
{
    public class ClientSubscriptionsHandler : IClientHandler
    {
        public void OnClientConnect()
        {
            NetworkClient.RegisterHandler<HelloMessage>(OnHelloMessage);
            NetworkClient.Send(SubscribeRequest.Create<HelloMessage>());
        }
        public void OnClientDisconnect()
        {
            NetworkClient.UnregisterHandler<HelloMessage>();
        }
        private void OnHelloMessage(HelloMessage msg)
        {
            Debug.Log($"Received {nameof(HelloMessage)} with text {msg.Text}");
        }
    }
}