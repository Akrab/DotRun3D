using Akrab;
using UnityEngine;
using Zenject;

namespace DonRun3D
{
    public class InputClick : CustomBehaviour
    {
        [Inject] Camera _camera;
        public override void CUpdate()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if(_camera == null)
                {
                    Debug.LogError("_camera = null");
                }
            }
        }
    }
}