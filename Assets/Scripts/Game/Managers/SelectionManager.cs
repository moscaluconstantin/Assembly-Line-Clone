using System;
using System.Collections.Generic;
using Enums;
using Game.Controllers;
using GlobalManagers;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Managers
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private PointerStateType initialPointerState;
        [SerializeField] private LayerMask selectionLayerMask;
    
        [HorizontalLine] 
        [SerializeField] private Camera gameCamera;

        public event Action OnSelectionListModified; 
        
        public List<DeviceHolderController> SelectedControllers => selectedControllers;

        private List<DeviceHolderController> selectedControllers;
        
        private void Awake()
        {
            InitData();
        }

        private void InitData()
        {
            PointerStateManager.CurrentState = initialPointerState;
            selectedControllers=new List<DeviceHolderController>();
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

            if (!hit.collider.transform.parent.TryGetComponent(out DeviceHolderController deviceHolderController)) 
                return;
            
            if (PointerStateManager.CurrentState == PointerStateType.None)
            {
                
            }
            else
            {
                if (!deviceHolderController.CanBeSelected)
                    return;
                
                deviceHolderController.Select();
                HandleSelection(deviceHolderController);
            }
        }

        public void ClearSelection() => selectedControllers.Clear();

        private void HandleSelection(DeviceHolderController selectableController)
        {
            if (!selectedControllers.Contains(selectableController))
            {
                selectedControllers.Add(selectableController);
            }
            else
            {
                selectedControllers.Remove(selectableController);
            }

            OnSelectionListModified?.Invoke();
        }
    }
}