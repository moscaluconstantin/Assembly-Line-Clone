using UnityEngine;

namespace Controllers
{
    public class ColorController : MonoBehaviour
    {
        [SerializeField] private Color selectColor;

        private Color defaultColor;
        private Renderer colorControllerRenderer;

        private void Awake()
        {
            InitData();
        }

        private void InitData()
        {
            colorControllerRenderer = GetComponentInChildren<Renderer>();
            defaultColor = colorControllerRenderer.material.color;
        }

        public void SetSelectColor() => SetSpriteColor(selectColor);
        public void SetDefaultColor() => SetSpriteColor(defaultColor);

        private void SetSpriteColor(Color color) => colorControllerRenderer.material.color = color;
    }
}