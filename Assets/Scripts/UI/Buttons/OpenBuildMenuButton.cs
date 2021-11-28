using Systems;
using Enums;
using GlobalManagers;
using Scripts.Systems;
using UnityEngine;

namespace UI.Buttons
{
    public class OpenBuildMenuButton : MonoBehaviour
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
            PointerStateManager.CurrentState = PointerStateType.BuySelection;
            
            GameStateManager.CurrentState = GameState.Build;
        }
    }
}