using Enums;
using GlobalManagers;
using UnityEngine;

namespace Controllers.Base
{
    public abstract class SelectableController : MonoBehaviour
    {
        [SerializeField] private PointerStateType selectableState;

        public bool CanBeSelected => PointerStateManager.CurrentState == selectableState;
        
        private bool isSelected;
        
        private ColorController colorController;

        protected virtual void Awake()
        {
            InitData();
        }

        private void InitData()
        {
            isSelected = false;
            
            colorController = GetComponent<ColorController>();
        }

        public void Select()
        {
            if (isSelected)
            {
                Deselect();
                return;
            }

            isSelected = true;
            
            colorController.SetSelectColor();
            
            OnSelected();
        }

        public void Deselect()
        {
            isSelected = false;
            
            colorController.SetDefaultColor();
            
            OnDeselected();
        }
        
        protected abstract void OnSelected();
        protected abstract void OnDeselected();
    }
}