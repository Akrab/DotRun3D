using Akrab;
using DonRun3D.ECS.LevelContructor;
using DonRun3D.ECS.Player;
using DonRun3D.ECS.UI;
using Leopotam.EcsLite;

using Zenject;

namespace DonRun3D.System
{
    public class StartECS : CustomBehaviour
    {
        [Inject] private EcsWorld _worldGame;
        [Inject] private DiContainer _diContainer;

        private EcsSystems _gameUpdateSys;
        private EcsSystems _gameFixedUpdateSys;

        [Inject]
        public void Initialize()
        {
            _gameUpdateSys = new EcsSystems(_worldGame);

            _gameFixedUpdateSys = new EcsSystems(_worldGame);
#if UNITY_EDITOR
            _gameUpdateSys.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
            _gameFixedUpdateSys.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif

            _gameUpdateSys.Add(new EPlayGameSystem());
            _gameUpdateSys.Add(new ESpawnPlayerSystem(_diContainer));
            _gameUpdateSys.Add(new ELevelContructSystem(_diContainer));


            _gameUpdateSys.Init();
            _gameFixedUpdateSys.Init();
        }

        public override void CUpdate()
        {
            _gameUpdateSys?.Run();
        }

        public override void CFixedUpdate()
        {         
            _gameFixedUpdateSys?.Run();
        }
        void OnDestroy()
        {

            if (_gameUpdateSys != null)
            {
                _gameUpdateSys.Destroy();
                _gameUpdateSys = null;
            }

            if (_gameFixedUpdateSys != null)
            {
                _gameFixedUpdateSys.Destroy();
                _gameFixedUpdateSys = null;
            }

            if (_worldGame != null)
            {
                _worldGame.Destroy();
                _worldGame = null;
            }
        }
    }
}
