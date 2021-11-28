using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace UI.Controllers
{
    public class UIElementAnimationsController : MonoBehaviour, IScreenElementAnimationsController
    {
        [SerializeField] private RectTransform rectTransformElement;
        [SerializeField] private Vector2 hidePosition;

        [HorizontalLine]
        [SerializeField] private float showDelay;
        [SerializeField] private float showDuration;
        [SerializeField] private Ease showEase;

        [HorizontalLine] 
        [SerializeField] private float hideDuration;
        [SerializeField] private Ease hideEase;

        public bool IsObjectActive { get; set; } = true;
		
        private Vector2 initPosition;
        private bool currentState;

        private Tween moveTween;

        public void ForceClose()
        {
            initPosition = rectTransformElement.anchoredPosition;
            rectTransformElement.anchoredPosition = hidePosition;
        }

        public void ShowElement(TweenCallback callback)
        {
            if (!IsObjectActive)
            {
                callback?.Invoke();
                return;
            }
			
            RunAnimations(true, initPosition, showDuration, showDelay, showEase, callback);
        }

        public void HideElement(TweenCallback callback)
        {
            if (!IsObjectActive)
            {
                callback?.Invoke();
                return;
            }
			
            RunAnimations(false, hidePosition, hideDuration, 0f, hideEase, callback);
        }

        private void RunAnimations(bool newState, Vector2 startPosition, float duration, float tweenDelay, Ease ease, TweenCallback callback)
        {
            if (newState == currentState)
                return;

            currentState = newState;

            moveTween.Kill();
            moveTween = rectTransformElement.DOAnchorPos(startPosition, duration).SetDelay(tweenDelay).SetEase(ease);
            moveTween.OnComplete(callback);
        }
    }
}