using DG.Tweening;
using UnityEngine;

namespace UI.Controllers
{
    public abstract class ScreenElementsAnimationController : MonoBehaviour
    {
        [SerializeField] protected Transform elementsContainer;
		
		private int amountElements;
		private int amountHideElementsDone;
		private int amountShowElementsDone;

		private bool currentState;
		private bool isInitData;
		
		private TweenCallback deactivateScreenDoneCallback;
		
		private IScreenElementAnimationsController[] screenElementAnimationsControllers;

		private Tween activationDelayTween;

		private void InitData()
		{
			screenElementAnimationsControllers = elementsContainer.GetComponentsInChildren<IScreenElementAnimationsController>(true);
			amountElements = screenElementAnimationsControllers.Length;

			isInitData = true;
		}
		
		public void ForceClose()
		{
			gameObject.SetActive(false);
			currentState = false;

			if (!isInitData)
				InitData();
			
			foreach (IScreenElementAnimationsController screenElementAnimationsController in screenElementAnimationsControllers)
			{
				screenElementAnimationsController.ForceClose();
			}
		}
		
		public void ActivateScreen(float delay)
		{
			if(currentState)
				return;
			
			currentState = true;
			
			amountHideElementsDone = 0;
			amountShowElementsDone = 0;
			
			activationDelayTween.Kill();
			activationDelayTween = DOVirtual.DelayedCall(delay, () =>
			{
				gameObject.SetActive(true);
				
				foreach (var screenElementAnimationsController in screenElementAnimationsControllers)
				{
					screenElementAnimationsController.ShowElement(ElementShow);
				}
			});
		}

		public void DeactivateScreen(TweenCallback callback)
		{
			if(!currentState || screenElementAnimationsControllers.Length == 0)
			{
				callback?.Invoke();
				return;
			}
			
			currentState = false;
		
			activationDelayTween.Kill();
			
			deactivateScreenDoneCallback = callback;
			
			foreach (var screenElementAnimationsController in screenElementAnimationsControllers)
			{
				screenElementAnimationsController.HideElement(ElementHide);
			}
		}

		private void ElementShow()
		{
			amountShowElementsDone++;
			
			if (amountShowElementsDone < amountElements)
				return;

			OnElementShow();
		}

		protected abstract void OnElementShow();

		private void ElementHide()
		{
			amountHideElementsDone++;

			if (amountHideElementsDone < amountElements)
				return;
			
			gameObject.SetActive(false);
			deactivateScreenDoneCallback?.Invoke();

			OnElementHide();
		}

		protected abstract void OnElementHide();
    }
}