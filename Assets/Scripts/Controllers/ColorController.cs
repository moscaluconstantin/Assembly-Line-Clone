using UnityEngine;

namespace Controllers
{
    public class ColorController : MonoBehaviour
    {
        [SerializeField] private Color defaultSelectionColor;
        [SerializeField] private Color buySelectionColor;
        [SerializeField] private Color sellSelectionColor;

        private Renderer colorControllerRenderer;

        private void Awake()
        {
            colorControllerRenderer = GetComponentInChildren<Renderer>();
            SetDefaultColor();
        }

        public void SetBuySelectColor() => SetSpriteColor(buySelectionColor);
        public void SetSellSelectColor() => SetSpriteColor(sellSelectionColor);
        public void SetDefaultColor() => SetSpriteColor(defaultSelectionColor);

        private void SetSpriteColor(Color color) => colorControllerRenderer.material.color = color;
    }
}