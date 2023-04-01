using DonRun3D.Player;
using DotRun3d.Player;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameContainerInstaller", menuName = "Installers/GameContainerInstaller")]
public class GameContainerInstaller : ScriptableObjectInstaller<GameContainerInstaller>
{
    [SerializeField] private PlayerView _playerView;

    public override void InstallBindings()
    {
        Container.Bind<IPlayerView>().FromComponentsInNewPrefab(_playerView).AsSingle();
    }
}