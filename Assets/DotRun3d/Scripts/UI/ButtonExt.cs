using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PG.UI
{
    public class ButtonExt : Button
    {
        public float scalePress = 0.1f;
        public float duration = 0.1f;

        private float initScale = 1f;
        private float pressScale = 0.5f;

        private bool isPressed = false;

        public bool lockGameInput = false;

        protected override void Awake()
        {
            initScale = transform.localScale.x;
            pressScale = initScale > 0 ? initScale - scalePress : initScale + scalePress;

            base.Awake();

            onClick.AddListener(PlaySound);
        }

        private void PlaySound()
        {

        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (interactable == false)
                return;

            base.OnPointerDown(eventData);

            transform.DOScale(pressScale, duration);
            isPressed = true;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if (isPressed)
            {
                Sequence sqr = DOTween.Sequence();

                var scale = initScale;
                scale += 0.08f;
                sqr.Append(transform.DOScale(scale, duration * 0.9f));
                sqr.Append(transform.DOScale(initScale, duration * 0.4f));
            }

            isPressed = false;
        }


    }
}