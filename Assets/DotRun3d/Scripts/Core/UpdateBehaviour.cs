using System.Collections.Generic;
using UnityEngine;

namespace Akrab
{
    public abstract class UpdateBehaviour : MonoBehaviour
    {
        public static List<UpdateBehaviour> allUpdate = new List<UpdateBehaviour>(100);
        public static List<UpdateBehaviour> allLateUpdate = new List<UpdateBehaviour>(100);
        public static List<UpdateBehaviour> allFixedUpdate = new List<UpdateBehaviour>(100);

        protected virtual void OnEnable()
        {
            allUpdate.Add(this);
            allLateUpdate.Add(this);
            allFixedUpdate.Add(this);
        }

        protected virtual void OnDisable()
        {
            allUpdate.Remove(this);
            allLateUpdate.Remove(this);
            allFixedUpdate.Remove(this);
        }

        public void TUpdate()
        {
            CUpdate();
        }
        public virtual void CLateUpdate() { }

        public virtual void CFixedUpdate() { }

        public virtual void CUpdate() { }
    }
}
