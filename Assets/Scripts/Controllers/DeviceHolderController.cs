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

        public DeviceBaseController Device => device;

        private bool isSelected;
        private DeviceBaseController device;

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

        public void AddDevice(DeviceBaseController device)
        {
            this.device = device;

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