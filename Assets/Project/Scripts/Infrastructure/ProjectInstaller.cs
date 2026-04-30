using Mirror;
using Networking;
using UnityEngine;
using Zenject;

namespace Infrastructure.DI
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField]
        private NetworkManager _networkManagerPrefab;
        public override void InstallBindings()
        {
            // Регистрируем сервис подписок
            Container.Bind<ServerSubscriptionsService>()
                .FromNew()
                .AsSingle();

            // Регистрируем хендлеры событий для клиента и сервера
            Container.Bind<IClientHandler>()
                .To<ClientSubscriptionsHandler>()
                .FromNew()
                .AsSingle();

            Container.Bind<IServerHandler>()
                .To<ServerSubscriptionsHandler>()
                .FromNew()
                .AsSingle();

            // Регистрируем NetworkManager
            Container.Bind<NetworkManager>()
                .FromComponentInNewPrefab(_networkManagerPrefab)
                .AsSingle()
                .NonLazy();
        }
    }
}