using DonRun3D.ECS.UI;
using Leopotam.EcsLite;
using PG.UI;
using UnityEngine;

namespace Akrab.UI
{
    public class MainUI : BaseForm, IMainUI
    {

        [SerializeField] ButtonExt btnPlay;
        protected override void setup()
        {
            base.setup();

            btnPlay.onClick.AddListener(Play);
        }

        private void Play()
        {
            var pool = _ecsWorld.GetPool<EPlayGameClickComp>();
            
            if(ecsPackEntity.Unpack(_ecsWorld, out int unpacked))
                pool.Add(unpacked);
        }
    }
}