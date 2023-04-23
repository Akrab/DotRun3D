using DG.Tweening;
using DonRun3D.ECS.UI;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Akrab.UI
{

    public interface IForm
    {
        void InitUIRoot(RectTransform rect);
        void Disable();
        void Enable();
        void Show(object data = null);
        void Hide(UnityAction callback = null);
        bool isShow { get; }

    }

    public interface IMainUI { }

    public class BaseForm : CustomBehaviour, IForm
    {

        public bool isShow => gameObject.activeSelf;

        protected Canvas canvas;
        protected CanvasGroup canvasGroup;
        [Inject] protected EcsWorld _ecsWorld;
        protected EcsPackedEntity ecsPackEntity;
        public void Disable()
        {
            gameObject.SetActive(false);
        }
        public void Enable()
        {
            gameObject.SetActive(true);
        }

        [Inject]
        public void Initialize()
        {
            var entity = _ecsWorld.NewEntity();
            ecsPackEntity = _ecsWorld.PackEntity( entity);
            var pool = _ecsWorld.GetPool<EFormComp>();
            ref EFormComp c1 = ref pool.Add(entity);
            c1.form = this;
        }

        public void InitUIRoot(RectTransform rectData)
        {
            canvas = GetComponentInChildren<Canvas>(true);
            canvasGroup = transform.GetChild(0).gameObject.AddComponent<CanvasGroup>();

            var _rect = canvas.GetComponent<RectTransform>();
            _rect.sizeDelta = rectData.sizeDelta;
            gameObject.transform.localPosition = Vector3.zero;

        }

        public void Show(object data = null)
        {
            Enable();

            canvasGroup.DOFade(1f, 0.1f).OnComplete(() => {  });

        }

        public void Hide(UnityAction callback = null)
        {

            canvasGroup.DOFade(0f, 0.1f).OnComplete(() => { 
                Disable();
                callback?.Invoke();
            });
        }
    }
}