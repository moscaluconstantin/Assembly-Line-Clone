using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
	public class ClickUIButton : MonoBehaviour
	{
		[SerializeField] private int scaleDirection = 1;
		[SerializeField] private Button button;
		[SerializeField] private Transform buttonTransform;
		[SerializeField] private Transform buttonIconTransform;

		[SerializeField] private bool clickOnce;
		[SerializeField] private bool doPunchButtonIcon;

		public event Action OnClickDone;
		public event Action OnClickStarted;
		
		private bool canClick;
		
		private Tween tweenScale;
		private Tween buttonIconPunchTween;
		
		private void Awake()
		{
			if (button == null)
			{
				button = GetComponent<Button>();
			}
			
			button.onClick.AddListener(Click);
			
			if(buttonTransform == null)
			{
				buttonTransform = transform;
			}
		}

		private void OnEnable()
		{
			ResetData();
		}

		private void ResetData()
		{
			canClick = true;
		}

		public void Click()
		{
			if(!canClick)
				return;

			canClick = false;
			
			if (!clickOnce)
			{
				DOVirtual.DelayedCall(0.15f, () => canClick = true);
			}
			
			OnClickStarted?.Invoke();
			
			tweenScale.Kill();
			buttonTransform.localScale = Vector3.one;
			
			tweenScale = buttonTransform.DOScale(Vector3.one + Vector3.one * (0.1f * scaleDirection), 0.1f).OnComplete(() => buttonTransform.DOScale(Vector3.one, 0.05f).OnComplete(() => OnClickDone?.Invoke()));
			tweenScale.SetEase(Ease.InOutQuad);

			if (!doPunchButtonIcon) 
				return;
			
			buttonIconPunchTween.Kill();
			buttonIconTransform.localScale = Vector3.one;
			buttonIconPunchTween = buttonIconTransform.DOPunchScale(Vector3.one * -0.2f, 0.5f, 3).SetEase(Ease.InOutQuad);
		}
	}
}