using System.Linq;
using Enums;
using Game.Controllers.Devices;
using GlobalManagers;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Managers
{
    public class BuilderManager : MonoBehaviour
    {
        [SerializeField] private StarterController starterControllerPrefab;

        [HorizontalLine]
        [SerializeField] private SelectionManager selectionManager;

        private void Awake()
        {
            selectionManager.OnSelectionListModified += UpdateBufferCurrencyValue;
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
            
            selectionManager.ClearSelection();
            CurrencyManager.FinishTransaction();
        }
        
        [Button("Remove")]
        public void Remove()
        {
            foreach (var selectedController in selectionManager.SelectedControllers)
            {
                selectedController.RemoveDevice();
            }
            
            selectionManager.ClearSelection();
            CurrencyManager.FinishTransaction();
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