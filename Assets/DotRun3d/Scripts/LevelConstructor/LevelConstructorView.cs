using Akrab;
using DonRun3D.ECS.LevelContructor;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace DonRun3D.LevelConstructor
{

    public interface ILevelConstructorView
    {
        public Transform trmParent { get; }
    }

    public class LevelConstructorView : CustomBehaviour, ILevelConstructorView
    {

        [Inject] readonly EcsWorld _ecsWorld;

        private int ecsIndex = -1;

        [SerializeField] private Transform _worldRoot;


        public Transform trmParent => _worldRoot;


        [Inject]
        public void Initialize()
        {
            ecsIndex = _ecsWorld.NewEntity();
            var pool = _ecsWorld.GetPool<ELevelConstrComp>();
            ref ELevelConstrComp c1 = ref pool.Add(ecsIndex);
            c1.view = this;
        }
    }

}