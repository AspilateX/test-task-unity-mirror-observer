using Mirror;

namespace Networking
{
    public struct SubscribeRequest : NetworkMessage
    {
        public string TypeName;

        /// <summary>
        /// Метод создания запроса на подписку на сообщения определенного типа.
        /// </summary>
        /// <typeparam name="T">Тип сообщения</typeparam>
        /// <returns>Созданный запрос на подписку</returns>
        public static SubscribeRequest Create<T>() where T : struct, NetworkMessage
        {
            return new SubscribeRequest() { TypeName = typeof(T).AssemblyQualifiedName };
        }
    }
}