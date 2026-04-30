namespace Networking
{
    /// <summary>
    /// Интерфейс хендлера клиентских событий
    /// </summary>
    public interface IClientHandler
    {
        void OnClientConnect();
        void OnClientDisconnect();
    }
}