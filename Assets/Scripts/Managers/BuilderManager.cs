using Controllers.Devices;
using GlobalManagers;
using NaughtyAttributes;
using UnityEngine;

namespace Managers
{
    public class BuilderManager : MonoBehaviour
    {
        [SerializeField] private StarterController starterControllerPrefab;

        [HorizontalLine]
        [SerializeField] private SelectionManager selectionManager;
        
        [Button("Build")]
        public void Build()
        {
            foreach (var selectedController in selectionManager.SelectedControllers)
            {
                var device = Instantiate(starterControllerPrefab, transform);
                device.transform.position = selectedController.transform.position;

                CurrencyManager.AddCurrency(device.Price);

                selectedController.AddDevice(device);
            }
            
            selectionManager.ClearSelection();
        }
        
        [Button("Remove")]
        public void Remove()
        {
            foreach (var selectedController in selectionManager.SelectedControllers)
            {
                CurrencyManager.AddCurrency(-selectedController.Device.SellPrice);
                selectedController.RemoveDevice();
            }
            
            selectionManager.ClearSelection();
        }
    }
}