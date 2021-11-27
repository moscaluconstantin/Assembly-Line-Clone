using GlobalManagers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class BufferCurrencyTextController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currencyText;
        [SerializeField] private Transform containerTransform;

        private void Awake()
        {
            CurrencyManager.OnBufferCurrencyValueChanged += UpdateText;
        }

        private void Start()
        {
            UpdateText(CurrencyManager.BufferCurrencyValue);
        }

        private void OnDestroy()
        {
            CurrencyManager.OnBufferCurrencyValueChanged -= UpdateText;
        }

        private void UpdateText(float newValue)
        {
            currencyText.text = (newValue > 0 ? "+" : "") + newValue;

            containerTransform.gameObject.SetActive(Mathf.Abs(newValue) > .1f);
        }
    }
}