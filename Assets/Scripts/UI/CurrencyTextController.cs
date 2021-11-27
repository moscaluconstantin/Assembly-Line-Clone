using GlobalManagers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CurrencyTextController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currencyText;

        private void Awake()
        {
            CurrencyManager.OnCurrencyValueChanged += UpdateText;
        }

        private void Start()
        {
            UpdateText(CurrencyManager.CurrencyValue);
        }

        private void OnDestroy()
        {
            CurrencyManager.OnCurrencyValueChanged -= UpdateText;
        }

        private void UpdateText(float newValue)
        {
            currencyText.text = "$ " + newValue;
        }
    }
}