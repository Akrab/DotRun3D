using DonRun3D.Player;
using DonRun3D.World.Column;
using DotRun3d.Player;
using System;
using System.Collections.Generic;
using DotRun3d.Scripts.World;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameContainerInstaller", menuName = "Installers/GameContainerInstaller")]
public class GameContainerInstaller : ScriptableObjectInstaller<GameContainerInstaller>
{
    [SerializeField] private PlayerView _playerView;


    [SerializeField] private WorldData worldData;

    public override void InstallBindings()
    {
        Container.Bind<IPlayerView>().FromComponentsInNewPrefab(_playerView).AsSingle();
        
        Container.Bind<WorldData>().FromInstance(worldData).AsSingle();
    }

}