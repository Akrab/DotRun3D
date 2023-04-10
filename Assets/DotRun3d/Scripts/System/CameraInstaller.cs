using UnityEngine;
using Zenject;

namespace DonRun3D
{
    public class CameraInstaller : MonoInstaller<CameraInstaller>
    {
        [SerializeField] private Camera _camera;
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(_camera).AsSingle().NonLazy(); 
        }

    }
}
