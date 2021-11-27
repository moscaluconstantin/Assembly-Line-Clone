using Controllers.Devices;
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

                selectedController.AddDevice(device);
            }
            
            selectionManager.ClearSelection();
        }
        
        [Button("Remove")]
        public void Remove()
        {
            foreach (var selectedController in selectionManager.SelectedControllers)
            {
                selectedController.RemoveDevice();
            }
            
            selectionManager.ClearSelection();
        }
    }
}