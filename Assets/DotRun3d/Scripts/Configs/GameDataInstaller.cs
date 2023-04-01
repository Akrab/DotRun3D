using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameDataInstaller", menuName = "Installers/GameDataInstaller")]
public class GameDataInstaller : ScriptableObjectInstaller<GameDataInstaller>
{
    public override void InstallBindings()
    {
    }
}