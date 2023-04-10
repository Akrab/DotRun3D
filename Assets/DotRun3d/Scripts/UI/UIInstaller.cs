using Akrab.UI;
using DonRun3D.ECS;
using Leopotam.EcsLite;
using System.Collections;
using UnityEngine;
using Zenject;


namespace DonRun3D.UI
{
    public class UIInstaller : MonoInstaller
    {

        [SerializeField] CanvasRoot canvasRoot;
        public override void InstallBindings()
        {
            StartCoroutine(SetupUI());
        }

        IEnumerator SetupUI()
        {
            yield return null;
            var forms = canvasRoot.SetupUI();

            foreach (var form in forms)
                Container.Bind(form.GetType()).FromInstance(form).AsSingle().NonLazy();
        }
    }
}