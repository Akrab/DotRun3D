using Akrab;
using DonRun3D.ECS;
using DonRun3D.ECS.LevelContructor;
using DonRun3D.ECS.Player;
using DonRun3D.ECS.UI;
using Leopotam.EcsLite;

using Zenject;

namespace DonRun3D.System
{
    public class StartECS : CustomBehaviour
    {
        [Inject] private EcsWorld worldGame;
        [Inject] private DiContainer diContainer;

        private EcsSystems gameUpdateSys;
        private EcsSystems gameFixedUpdateSys;

        [Inject]
        public void Initialize()
        {
            gameUpdateSys = new EcsSystems(worldGame);

            gameFixedUpdateSys = new EcsSystems(worldGame);
#if UNITY_EDITOR
            gameUpdateSys.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
            gameFixedUpdateSys.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif
            
            gameUpdateSys.Add(new EPlayGameSystem());
            gameUpdateSys.Add( diContainer.Instantiate<ESpawnPlayerSystem>());
            gameUpdateSys.Add(diContainer.Instantiate<EcsGameManagerSystem>());
            gameUpdateSys.Add(diContainer.Instantiate<EcsLevelConstructSystem>());
            gameUpdateSys.Add(diContainer.Instantiate<EcsClickToObjSystem>());
            gameUpdateSys.Add(diContainer.Instantiate<EcsMoveToNextLineSystem>());
            gameUpdateSys.Add(diContainer.Instantiate<EcsLevelHideObjSystem>());
            gameUpdateSys.Add(diContainer.Instantiate<EcsLevelRuntimeUpdateSystem>());

            gameUpdateSys.Init();
            gameFixedUpdateSys.Init();

            var gameManager = worldGame.NewEntity();
            var pool = worldGame.GetPool<EcsGameManagerComponent>();
            ref var managerComp = ref pool.Add(gameManager);
            managerComp.gameData = new GameData() {currentColor = ColorType.GREEN};
        }

        public override void CUpdate()
        {
            gameUpdateSys?.Run();
        }

        public override void CFixedUpdate()
        {         
            gameFixedUpdateSys?.Run();
        }
        void OnDestroy()
        {

            if (gameUpdateSys != null)
            {
                gameUpdateSys.Destroy();
                gameUpdateSys = null;
            }

            if (gameFixedUpdateSys != null)
            {
                gameFixedUpdateSys.Destroy();
                gameFixedUpdateSys = null;
            }

            if (worldGame != null)
            {
                worldGame.Destroy();
                worldGame = null;
            }
        }
    }
}
