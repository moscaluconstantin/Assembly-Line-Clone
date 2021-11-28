using Systems;
using Enums;
using Game.Managers;
using GlobalManagers;
using ScriptableObjects;
using Scripts.Systems;
using UnityEngine;

namespace UI.Buttons
{
    public class CancelSelectionButton : MonoBehaviour
    {
        private SelectionManager selectionManager;
        
        private ClickUIButton clickUIButton;

        private void Awake()
        {
            clickUIButton = GetComponent<ClickUIButton>();
            clickUIButton.OnClickDone += Click;
        }

        private void Start()
        {
            selectionManager = Resources.Load<ManagersHolder>("ManagersHolder").selectionManager;
        }

        private void OnDestroy() => clickUIButton.OnClickDone -= Click;

        private void Click()
        {
            selectionManager.ClearSelection();
            
            PointerStateManager.CurrentState = PointerStateType.None;
            GameStateManager.CurrentState = GameState.Game;
        }
    }
}