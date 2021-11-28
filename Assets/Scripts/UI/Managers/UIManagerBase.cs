using System.Collections.Generic;
using System.Linq;
using Systems;
using Scripts.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Managers
{
    public class UIManagerBase : MonoBehaviour
    {
        [SerializeField] private GameState initialGameState;
        [SerializeField] private Image uiBlockImage;
		
        protected Dictionary<GameState, UIScreen> Screens;
		
        private UIScreen lastScreen;
        private UIScreen currentScreen;

        protected void Awake()
        {
            List<UIScreen> screensList = GetComponentsInChildren<UIScreen>(true).ToList();
            Screens = screensList.ToDictionary(screen => screen.State, screen => screen);

            GameStateManager.onGameStateChange += HandleGameStateChange;
            // FadeGameManager.onFadeComplete += FadeComplete;

            uiBlockImage.raycastTarget = false;
        }

        private void Start()
        {
            foreach (KeyValuePair<GameState, UIScreen> screen in Screens)
            {
                screen.Value.ForceClose();
            }
			
            GameStateManager.CurrentState = initialGameState;
        }

        // private void FadeComplete(FadeType fadeType)
        // {
        // 	if (fadeType != FadeType.FadeOut)
        // 		return;
        // 	
        // 	GameStateManager.CurrentState = GameState.Menu;	
        // }

        protected void HandleGameStateChange(GameState gameState)
        {
            if (Screens.ContainsKey(gameState))
            {
                currentScreen = Screens[gameState];

                uiBlockImage.raycastTarget = true;
				
                if (lastScreen)
                {
                    lastScreen.DeactivateScreen(ActivateTargetScreen);
                }
                else
                {
                    ActivateTargetScreen();
                }

                lastScreen = currentScreen;
            }
        }

        private void ActivateTargetScreen()
        {
            currentScreen.OnScreenStateChanged += ScreenStateChanged;
            currentScreen.ActivateScreen(0f);
        }

        private void ScreenStateChanged(bool state)
        {
            if(!state)
                return;
			
            uiBlockImage.raycastTarget = false;
            currentScreen.OnScreenStateChanged -= ScreenStateChanged;
        }
		
        protected void OnDestroy()
        {
            GameStateManager.onGameStateChange -= HandleGameStateChange;
            // FadeGameManager.onFadeComplete -= FadeComplete;
        }
    }
}