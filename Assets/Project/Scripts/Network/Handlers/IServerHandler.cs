using Mirror;

namespace Networking
{
    /// <summary>
    /// Интерфейс хендлера серверных событий
    /// </summary>
    public interface IServerHandler
    {
        void OnServerConnect(NetworkConnectionToClient conn);
        void OnServerDisconnect(NetworkConnectionToClient conn);
    }
}