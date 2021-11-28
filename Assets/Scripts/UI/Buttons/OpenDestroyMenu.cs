using Systems;
using Enums;
using GlobalManagers;
using Scripts.Systems;
using UnityEngine;

namespace UI.Buttons
{
    public class OpenDestroyMenu : MonoBehaviour
    {
        private ClickUIButton clickUIButton;

        private void Awake()
        {
            clickUIButton = GetComponent<ClickUIButton>();
            clickUIButton.OnClickDone += Click;
        }

        private void OnDestroy() => clickUIButton.OnClickDone -= Click;

        private void Click()
        {
            PointerStateManager.CurrentState = PointerStateType.SellSelection;
            
            GameStateManager.CurrentState = GameState.Build;
        }
    }
}