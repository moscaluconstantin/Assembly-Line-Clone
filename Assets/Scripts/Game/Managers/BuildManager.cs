using System.Linq;
using Enums;
using Game.Controllers.Devices;
using GlobalManagers;
using NaughtyAttributes;
using ScriptableObjects;
using UnityEngine;

namespace Game.Managers
{
    public class BuildManager : MonoBehaviour
    {
        [SerializeField] private StarterController starterControllerPrefab;

        [HorizontalLine]
        [SerializeField] private SelectionManager selectionManager;

        private void Awake()
        {
            selectionManager.OnSelectionListModified += UpdateBufferCurrencyValue;
            Resources.Load<ManagersHolder>("ManagersHolder").buildManager = this;
        }

        private void OnDestroy()
        {
            selectionManager.OnSelectionListModified -= UpdateBufferCurrencyValue;
        }

        [Button("Build")]
        public void Build()
        {
            foreach (var selectedController in selectionManager.SelectedControllers)
            {
                var device = Instantiate(starterControllerPrefab, transform);
                device.transform.position = selectedController.transform.position;

                selectedController.AddDevice(device);
            }

            CurrencyManager.FinishTransaction();
            selectionManager.ClearSelection();
        }
        
        [Button("Remove")]
        public void Remove()
        {
            foreach (var selectedController in selectionManager.SelectedControllers)
            {
                selectedController.RemoveDevice();
            }

            CurrencyManager.FinishTransaction();
            selectionManager.ClearSelection();
        }

        private void UpdateBufferCurrencyValue()
        {
            if (PointerStateManager.CurrentState == PointerStateType.BuySelection)
            {
                CurrencyManager.BufferCurrencyValue = selectionManager.SelectedControllers.Count * -starterControllerPrefab.Price;
                return;
            }

            var totalValue = selectionManager.SelectedControllers.Sum(selectedController => selectedController.Device.SellPrice);

            CurrencyManager.BufferCurrencyValue = totalValue;
        }
    }
}