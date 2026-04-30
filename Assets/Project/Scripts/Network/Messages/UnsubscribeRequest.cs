using Mirror;

namespace Networking
{
    public struct UnsubscribeRequest : NetworkMessage
    {
        public string TypeName;

        /// <summary>
        /// Метод создания запроса на отписку от сообщений определенного типа.
        /// </summary>
        /// <typeparam name="T">Тип сообщения</typeparam>
        /// <returns>Созданный запрос на отписку</returns>
        public static UnsubscribeRequest Create<T>() where T : struct, NetworkMessage
        {
            return new UnsubscribeRequest() { TypeName = typeof(T).AssemblyQualifiedName };
        }
    }
}