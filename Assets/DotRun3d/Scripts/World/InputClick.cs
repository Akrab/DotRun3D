using System.Collections.Generic;
using System.Linq;
using Akrab;
using DonRun3D.ECS;
using DonRun3D.World.Column;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

namespace DonRun3D
{
    public class InputClick : CustomBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [Inject] private Camera camera;
        [Inject] private EcsWorld ecsWorld;

        private RaycastHit hit;
        private Ray ray;


        protected override void setup()
        {
            playerInput.onActionTriggered += OnActionTrigger;
        }

        private void OnActionTrigger(InputAction.CallbackContext context)
        {
            //Debug.LogError(context);
            if(!context.performed || context.action.name != "Click")
                return;

            var position = playerInput.actions["ClickPosition"].ReadValue<Vector2>();
            
            if(InputSystemUtils.IsClickOnUI((position)))
                return;
             
            ray = camera.ScreenPointToRay(position);
            
            if (Physics.Raycast(ray, out hit)) {
                Transform objectHit = hit.transform;

                var clickable = objectHit.gameObject.GetComponent<IClickable>();
                if (clickable == null)
                    return;

                AddedClickTarget(clickable);
            }
        }

        private void AddedClickTarget(IClickable target)
        {
            var filter = ecsWorld.Filter<EcsGameManagerComponent>().End();
            var pool = ecsWorld.GetPool<EcsClick>();

            foreach (int entity in filter)
            {
                ref var d = ref pool.Add(entity);
                d.clickable = target;
            }
        }
        
        public static class InputSystemUtils
        {
            public static bool IsClickOnUI(Vector2 position)
            {
                var pointerEventData = new PointerEventData(EventSystem.current)
                {
                    position = position
                };
             
                var raycastResultsList = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);
             
                return raycastResultsList.Any(result => result.gameObject is GameObject);
            }
        }
        
    }
}