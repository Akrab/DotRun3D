using UnityEngine;

namespace Akrab
{
    public class UpdateController : MonoBehaviour
    {
        void Update()
        {
            for (var i = 0; i < UpdateBehaviour.allUpdate.Count; i++)
                UpdateBehaviour.allUpdate[i].TUpdate();
        }

        void LateUpdate()
        {
            for (var i = 0; i < UpdateBehaviour.allLateUpdate.Count; i++)
                UpdateBehaviour.allLateUpdate[i].CLateUpdate();
        }

        void FixedUpdate()
        {
            for (var i = 0; i < UpdateBehaviour.allFixedUpdate.Count; i++)
                UpdateBehaviour.allFixedUpdate[i].CFixedUpdate();
        }
    }
}
