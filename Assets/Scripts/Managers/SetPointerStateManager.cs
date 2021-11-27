using Enums;
using GlobalManagers;
using UnityEngine;

namespace Managers
{
    public class SetPointerStateManager : MonoBehaviour
    {
        [SerializeField] private PointerStateType pointerState;

        private void OnValidate()
        {
            PointerStateManager.CurrentState = pointerState;
        }
    }
}