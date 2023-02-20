using UnityEngine;
using Zenject;

namespace Core
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private FirstPersonController _player;
        [SerializeField] private Transform _spawnPoint;

        public override void InstallBindings()
        {
            FirstPersonController firstPersonController = Container.
                InstantiatePrefabForComponent<FirstPersonController>(_player, _spawnPoint.position, Quaternion.identity, null);

            Container.
                Bind<FirstPersonController>().
                FromInstance(firstPersonController).
                AsSingle().
                NonLazy();
        }
    }
}
