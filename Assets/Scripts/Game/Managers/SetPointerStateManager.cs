using Enums;
using GlobalManagers;
using UnityEngine;

namespace Game.Managers
{
    public class SetPointerStateManager : MonoBehaviour
    {
        [SerializeField] private PointerStateType pointerState;
        [SerializeField] private float startCurrencyValue;

        private void OnValidate()
        {
            PointerStateManager.CurrentState = pointerState;
        }

        private void Awake()
        {
            CurrencyManager.Initialize(startCurrencyValue);
        }
    }
}