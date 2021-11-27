using System.Collections.Generic;
using Controllers.Base;
using Enums;
using GlobalManagers;
using NaughtyAttributes;
using UnityEngine;

namespace Managers
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private PointerStateType initialPointerState;
        [SerializeField] private LayerMask selectionLayerMask;
    
        [HorizontalLine] 
        [SerializeField] private Camera gameCamera;

        private List<SelectableController> selectedControllers;

        private void Awake()
        {
            InitData();
        }

        private void InitData()
        {
            PointerStateManager.CurrentState = initialPointerState;
            selectedControllers=new List<SelectableController>();
        }

        private void Update()
        {
            Vector2 inputPosition;

#if UNITY_EDITOR
        
            if (!Input.GetMouseButtonDown(0))
                return;

            inputPosition = Input.mousePosition;
        
#elif UNITY_ANDROID

        if (Input.touchCount == 0)
            return;
        
        if (Input.GetTouch(0).phase != TouchPhase.Began)
            return;

        inputPosition = Input.GetTouch(0).position;

#endif
            
            var touchWorldPosition = gameCamera.ScreenToWorldPoint(inputPosition);
            var touchPosition = new Vector2(touchWorldPosition.x, touchWorldPosition.y);

            var hit = Physics2D.Raycast(touchPosition, Vector2.zero, 20f, selectionLayerMask);

            if (!hit.collider)
                return;

            if (PointerStateManager.CurrentState == PointerStateType.None)
            {
                
            }
            else
            {
                if (!hit.collider.transform.parent.TryGetComponent(out SelectableController selectableController)) 
                    return;

                if (!selectableController.CanBeSelected)
                    return;
                
                selectableController.Select();
                HandleSelection(selectableController);
            }
        }

        private void HandleSelection(SelectableController selectableController)
        {
            if (!selectedControllers.Contains(selectableController))
            {
                selectedControllers.Add(selectableController);
                return;
            }

            selectedControllers.Remove(selectableController);
        }
    }
}