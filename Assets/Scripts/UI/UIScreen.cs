using System;
using Systems;
using UI.Controllers;
using UnityEngine;

namespace UI
{
	public class UIScreen : ScreenElementsAnimationController
	{
		[SerializeField] protected GameState state;
		public GameState State => state;
		
		public Action<bool> OnScreenStateChanged { get; set; }

		protected override void OnElementShow()
		{
			OnScreenStateChanged?.Invoke(true);
		}

		protected override void OnElementHide()
		{
			OnScreenStateChanged?.Invoke(false);
		}
	}
}