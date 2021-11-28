using System;
using Systems;
using Enums;
using Game.Managers;
using GlobalManagers;
using ScriptableObjects;
using Scripts.Systems;
using UnityEngine;

namespace UI.Buttons
{
    public class ConfirmSelectionButton : MonoBehaviour
    {
        private BuildManager buildManager;
        
        private ClickUIButton clickUIButton;

        private void Awake()
        {
            clickUIButton = GetComponent<ClickUIButton>();
            clickUIButton.OnClickDone += Click;
        }

        private void Start()
        {
            buildManager = Resources.Load<ManagersHolder>("ManagersHolder").buildManager;
        }

        private void OnDestroy() => clickUIButton.OnClickDone -= Click;

        private void Click()
        {
            switch (PointerStateManager.CurrentState)
            {
                case PointerStateType.BuySelection:
                    buildManager.Build();
                    break;
                case PointerStateType.SellSelection:
                    buildManager.Remove();
                    break;
            }


            PointerStateManager.CurrentState = PointerStateType.None;
            GameStateManager.CurrentState = GameState.Game;
        }
    }
}