using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Akrab.UI
{
    public class CanvasRoot : CustomBehaviour
    {
        [SerializeField]
        private RectTransform rect;
        [SerializeField]
        private Canvas canvas;

        [SerializeField]
        private CanvasScaler canvasScaler;
        public List<IForm> SetupUI()
        {
            var forms = GetComponentsInChildren<IForm>();

            foreach (var form in forms)
            {
                form.InitUIRoot(rect);
                if (form as IMainUI == null)
                    form.Disable();
            }

            return new List<IForm>(forms);
        }


        public override void CLateUpdate()
        {
            //if(canvas.)
        }

    }
}
