using DonRun3D;
using UnityEngine;
using Zenject;

namespace DonRun3D
{
    public class InputInstaller : MonoInstaller<InputInstaller>
    {
        [SerializeField] InputClick _inputClick;
        public override void InstallBindings()
        {
            Container.Bind<InputClick>().FromInstance(_inputClick).AsSingle().NonLazy();
        }
    }
}