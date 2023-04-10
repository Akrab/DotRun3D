using System;
using UnityEngine;
using Zenject;

namespace DonRun3D
{
    [ExecuteInEditMode]
    public class CameraManager : MonoBehaviour
    {

        const float MIN_WIDTH = 8f;

        const float MIN_HEIGHT_CAMERA = 6f;

        [SerializeField] private Transform root;
        [SerializeField] private Camera m_Camera;


        public float StartSeePosition = 0f;
        public float EndSeePostion = 0f;

        [Inject]

        public void Initialized()
        {
            var cameraH = CalculateHeigthCameraPosition();
            cameraH = Math.Max(cameraH, MIN_HEIGHT_CAMERA);
            root.position = new Vector3(root.position.x, cameraH, root.position.z);

            StartSeePosition = CalculateStartPositionSeeCamera();
            EndSeePostion = CalculateEndSeePostionCamera();
        }

        public float CalculateEndSeePostionCamera()
        {
            var angle = m_Camera.fieldOfView + m_Camera.fieldOfView - root.rotation.eulerAngles.x;
            var h = root.position.y;
            var len = h * (float)Math.Tan(Mathf.Deg2Rad * angle);

            var pos = len + root.position.z;
            return pos;
        }

        public float CalculateStartPositionSeeCamera()
        {
            var angle = m_Camera.fieldOfView - root.rotation.eulerAngles.x;
            var h = root.position.y;
            var pos = h * (float)Math.Tan(Mathf.Deg2Rad * angle);
            pos += root.position.z;
            return pos;
        }

        private float CalculateHeigthCameraPosition()
        {

            var aspect = Screen.width / (float)Screen.height;
            var angle = m_Camera.fieldOfView + m_Camera.fieldOfView - root.rotation.eulerAngles.x;
            var DAngle = m_Camera.fieldOfView - root.rotation.eulerAngles.x;
            var DTg = Math.Tan(Mathf.Deg2Rad * angle) - Math.Tan(Mathf.Deg2Rad * DAngle);

            var D = MIN_WIDTH / aspect;
            var H = D / DTg;

            return (float)H * 2f;
        }

    }
}