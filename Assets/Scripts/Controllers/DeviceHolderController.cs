using Controllers.Devices;
using Enums;
using GlobalManagers;
using UnityEngine;

namespace Controllers
{
    public class DeviceHolderController : MonoBehaviour
    {
        public bool CanBeSelected =>
            PointerStateManager.CurrentState == PointerStateType.BuySelection && device == null ||
            PointerStateManager.CurrentState == PointerStateType.SellSelection && device != null;

        private bool isSelected;
        private StarterController device;

        private ColorController colorController;

        private void Awake()
        {
            InitData();
        }

        private void InitData()
        {
            isSelected = false;

            colorController = GetComponentInChildren<ColorController>();
        }

        public void Select()
        {
            if (isSelected)
            {
                Deselect();
                return;
            }

            isSelected = true;

            switch (PointerStateManager.CurrentState)
            {
                case PointerStateType.BuySelection:
                    colorController.SetBuySelectColor();
                    break;
                case PointerStateType.SellSelection:
                    colorController.SetSellSelectColor();
                    break;
            }
        }

        public void Deselect()
        {
            isSelected = false;

            colorController.SetDefaultColor();
        }

        public void AddDevice(StarterController starterController)
        {
            device = starterController;
            
            Deselect();
        }

        public void RemoveDevice()
        {
            Destroy(device.gameObject);
            device = null;
            
            Deselect();
        }
    }
}